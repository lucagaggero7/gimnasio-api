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
    public class MembresiasRepositorio : IMembresiasRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public MembresiasRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Membresia>> GetAll()
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        estado AS Estado,
                        fecha_inicio AS FechaInicio,
                        fecha_vencimiento AS FechaVencimiento,
                        contacto_emergencia AS ContactoEmergencia,
                        total AS Total,
                        saldo AS Saldo,
                        fk_id_cliente AS FkIdCliente, 
                        fk_id_tipo_membresia AS FkIdTipoMembresia
                        FROM membresias";

            return await db.QueryAsync<Membresia>(sql);
        }

        public async Task<Membresia?> GetById(int id)
        {
            using var db = DbConnection();

            var sqlMembresia = @"SELECT id AS Id,
                                estado AS Estado,
                                fecha_inicio AS FechaInicio,
                                fecha_vencimiento AS FechaVencimiento,
                                contacto_emergencia AS ContactoEmergencia,
                                total AS Total,
                                saldo AS Saldo,
                                fk_id_cliente AS FkIdCliente, 
                                fk_id_tipo_membresia AS FkIdTipoMembresia
                         FROM membresias WHERE id = @id";

            var membresia = await db.QueryFirstOrDefaultAsync<Membresia>(sqlMembresia, new { Id = id });

            if (membresia == null)
                return null;

            var sqlPagos = @"SELECT id AS Id,
                            monto AS Monto,
                            fecha AS Fecha,
                            fk_id_forma_pago AS FkIdFormaPago,
                            fk_id_membresia AS FkIdMembresia,
                            fk_id_cliente AS FkIdCliente
                     FROM pagos
                     WHERE fk_id_membresia = @id";

            var pagos = (await db.QueryAsync<Pago>(sqlPagos, new { Id = id })).ToList();

            membresia.Pagos = pagos;

            if (!pagos.Any())
                membresia.Estado = "SIN PAGO";
            else if (membresia.Saldo > 0)
                membresia.Estado = "PENDIENTE";
            else
                membresia.Estado = "PAGADO";

            return membresia;
        }


        public async Task<Membresia> Create(Membresia membresia)
        {
            using var db = DbConnection();
            var sql = @"
                    INSERT INTO membresias (estado, fecha_inicio, fecha_vencimiento, contacto_emergencia, total, saldo, fk_id_cliente, fk_id_tipo_membresia)
                    VALUES (@estado, @fecha_inicio, @fecha_vencimiento, @contacto_emergencia, @total, @saldo, @fk_id_cliente, @fk_id_tipo_membresia);
                    SELECT LAST_INSERT_ID();";

            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                estado = "SIN PAGO",
                fecha_inicio = membresia.FechaInicio,
                fecha_vencimiento = membresia.FechaVencimiento,
                contacto_emergencia = membresia.ContactoEmergencia,
                total = membresia.Total,
                saldo = membresia.Saldo,
                fk_id_cliente = membresia.FkIdCliente,
                fk_id_tipo_membresia = membresia.FkIdTipoMembresia
            });

            membresia.Id = id;
            membresia.Estado = "SIN PAGO";

            var sqlUpdateCliente = @"
                 UPDATE clientes
                     SET contacto_emergencia = @contacto_emergencia
                        WHERE id = @id_cliente;";

            await db.ExecuteAsync(sqlUpdateCliente, new
            {
                contacto_emergencia = membresia.ContactoEmergencia,
                id_cliente = membresia.FkIdCliente
            });

            return membresia;
        }

        public async Task<bool> Update(Membresia membresia)
        {
            using var db = DbConnection();
            var sql = @"UPDATE membresias SET
                        estado = @estado,
                        fecha_inicio = @fecha_inicio,
                        fecha_vencimiento = @fecha_vencimiento,
                        contacto_emergencia = @contacto_emergencia,
                        total= @total,
                        saldo = @saldo,
                        fk_id_cliente = @fk_id_cliente,
                        fk_id_tipo_membresia = @fk_id_tipo_membresia
                        WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new
            {
                estado = membresia.Estado,
                fecha_inicio = membresia.FechaInicio,
                fecha_vencimiento = membresia.FechaVencimiento,
                contacto_emergencia = membresia.ContactoEmergencia,
                total = membresia.Total,
                saldo = membresia.Saldo,
                fk_id_cliente = membresia.FkIdCliente,
                fk_id_tipo_membresia = membresia.FkIdTipoMembresia,
                id = membresia.Id
            });


            var sqlUpdateCliente = @"
                 UPDATE clientes
                     SET contacto_emergencia = @contacto_emergencia
                        WHERE id = @id_cliente;";

            await db.ExecuteAsync(sqlUpdateCliente, new
            {
                contacto_emergencia = membresia.ContactoEmergencia,
                id_cliente = membresia.FkIdCliente
            });

            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM membresias WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}