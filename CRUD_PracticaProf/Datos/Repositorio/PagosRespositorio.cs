using CRUD_PracticaProf.Modelos; 
using CRUD_PracticaProf.Datos.Repositorio; 
using CRUD_PracticaProf; 
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
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
            var sql = "SELECT * FROM pagos";
            return await db.QueryAsync<Pago>(sql);
        }

        public async Task<Pago?> GetById(int id)
        {
            using var db = dbConnection();
            var sql = "SELECT * FROM pagos WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Pago>(sql, new { Id = id });
        }

        public async Task<bool> Create(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"INSERT INTO pagos (Monto, Fecha, fk_idFormasPago, fk_idMembresias, fk_idClientes)
                        VALUES (@Monto, @Fecha, @fk_idFormasPago, @fk_idMembresias, @fk_idClientes)";

            var result = await db.ExecuteAsync(sql, new { pago.Monto, pago.Fecha, pago.FkIdFormaPago, pago.FkIdMembresia, pago.FkIdCliente });
            return result > 0;
        }

        public async Task<bool> Update(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"UPDATE pagos SET
                        Monto = @Monto,
                        Fecha = @Fecha,
                        fk_idFormasPago = @fk_idFormasPago,
                        fk_idMembresias = @fk_idMembresias,
                        fk_idClientes = @fk_idClientes
                        WHERE Id = @idPagos";

            var result = await db.ExecuteAsync(sql, new { pago.Monto, pago.Fecha, pago.FkIdFormaPago, pago.FkIdMembresia, pago.FkIdCliente, pago.Id });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = dbConnection();
            var sql = "DELETE FROM pagos WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}