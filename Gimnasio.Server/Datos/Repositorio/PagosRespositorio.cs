using Gimnasio.Server;
using Gimnasio.Server.Datos.Repositorio;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Gimnasio.Server.Modelos.Entidades;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class PagosRepositorio : IPagosRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public PagosRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Pago>> GetAll()
        {
            using var db = dbConnection();
            var sql = @"SELECT id AS Id,
                        monto AS Monto,
                        fecha AS Fecha,
                        fk_id_forma_pago AS FkIdFormaPago, 
                        fk_id_membresia AS FkIdMembresia, 
                        fk_id_cliente AS FkIdCliente
                        FROM pagos";
            return await db.QueryAsync<Pago>(sql);
        }

        public async Task<Pago?> GetById(int id)
        {
            using var db = dbConnection();
            var sql = @"SELECT id AS Id,
                        monto AS Monto,
                        fecha AS Fecha,
                        fk_id_forma_pago AS FkIdFormaPago, 
                        fk_id_membresia AS FkIdMembresia, 
                        fk_id_cliente AS FkIdCliente
                        FROM pagos WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<Pago>(sql, new { Id = id });
        }

        public async Task<Pago> Create(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"INSERT INTO pagos (monto, fecha, fk_id_forma_pago, fk_id_membresia, fk_id_cliente)
                        VALUES (@monto, @fecha, @fk_id_forma_pago, @fk_id_membresia, @fk_id_cliente);
                        SELECT LAST_INSERT_ID(); ";

            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                monto = pago.Monto,
                fecha = pago.Fecha,
                fk_id_forma_pago = pago.FkIdFormaPago,
                fk_id_membresia = pago.FkIdMembresia,
                fk_id_cliente = pago.FkIdCliente
            });

            pago.Id = id;
            return pago;
        }

        public async Task<bool> Update(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"UPDATE pagos SET
                        monto = @monto,
                        fecha = @fecha,
                        fk_id_forma_pago = @fk_id_forma_pago,
                        fk_id_membresia = @fk_id_membresia,
                        fk_id_cliente = @fk_id_cliente
                        WHERE Id = @idPagos";

            var result = await db.ExecuteAsync(sql, new
            {
                monto = pago.Monto,
                fecha = pago.Fecha,
                fk_id_forma_pago = pago.FkIdFormaPago,
                fk_id_membresia = pago.FkIdMembresia,
                fk_id_cliente = pago.FkIdCliente,
                id = pago.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = dbConnection();
            var sql = "DELETE FROM pagos WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}