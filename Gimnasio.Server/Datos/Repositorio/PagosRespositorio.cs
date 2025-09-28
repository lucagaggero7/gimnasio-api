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
            await db.OpenAsync();
            using var transaction = await db.BeginTransactionAsync();

            try
            {
                // 1. Obtener id_cliente de la membresía
                var sqlSelectCliente = @"SELECT fk_id_cliente 
                                 FROM membresias 
                                 WHERE id = @idMembresia";
                var idCliente = await db.ExecuteScalarAsync<int>(
                    sqlSelectCliente,
                    new { idMembresia = pago.FkIdMembresia },
                    transaction);

                if (idCliente == 0)
                    throw new Exception("No se encontró la membresía o no tiene cliente asignado.");

                // 2. Insertar pago usando el idCliente obtenido
                var sqlInsert = @"INSERT INTO pagos 
                          (monto, fecha, fk_id_forma_pago, fk_id_membresia, fk_id_cliente)
                          VALUES (@monto, @fecha, @fk_id_forma_pago, @fk_id_membresia, @fk_id_cliente);
                          SELECT LAST_INSERT_ID();";

                var id = await db.ExecuteScalarAsync<int>(sqlInsert, new
                {
                    monto = pago.Monto,
                    fecha = pago.Fecha,
                    fk_id_forma_pago = pago.FkIdFormaPago,
                    fk_id_membresia = pago.FkIdMembresia,
                    fk_id_cliente = idCliente
                }, transaction);

                pago.Id = id;
                pago.FkIdCliente = idCliente; // opcional, para devolver el pago completo

                // 3. Actualizar saldo de la membresía
                var sqlSelectMembresia = @"SELECT id, total, saldo, estado 
                                   FROM membresias 
                                   WHERE id = @idMembresia";
                var membresiaEntity = await db.QueryFirstOrDefaultAsync<Membresia>(
                    sqlSelectMembresia, new { idMembresia = pago.FkIdMembresia }, transaction);

                if (membresiaEntity != null)
                {
                    membresiaEntity.Saldo -= (int)pago.Monto;

                    if (membresiaEntity.Saldo == membresiaEntity.Total)
                        membresiaEntity.Estado = "SIN PAGO";
                    else if (membresiaEntity.Saldo > 0)
                        membresiaEntity.Estado = "PENDIENTE";
                    else
                        membresiaEntity.Estado = "PAGADO";

                    var sqlUpdate = @"UPDATE membresias 
                              SET saldo = @saldo, estado = @estado 
                              WHERE id = @id";
                    await db.ExecuteAsync(sqlUpdate, new
                    {
                        saldo = membresiaEntity.Saldo,
                        estado = membresiaEntity.Estado,
                        id = membresiaEntity.Id
                    }, transaction);
                }

                await transaction.CommitAsync();
                return pago;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<bool> Update(Pago pago)
        {
            using var db = dbConnection();
            await db.OpenAsync(); // Abrir la conexión
            using var transaction = db.BeginTransaction();

            try
            {
                var sqlUpdatePago = @"UPDATE pagos SET
                                monto = @monto,
                                fecha = @fecha,
                                fk_id_forma_pago = @fk_id_forma_pago,
                                fk_id_membresia = @fk_id_membresia,
                                fk_id_cliente = @fk_id_cliente
                              WHERE id = @id";

                var result = await db.ExecuteAsync(sqlUpdatePago, new
                {
                    monto = pago.Monto,
                    fecha = pago.Fecha,
                    fk_id_forma_pago = pago.FkIdFormaPago,
                    fk_id_membresia = pago.FkIdMembresia,
                    fk_id_cliente = pago.FkIdCliente,
                    id = pago.Id
                }, transaction);

                if (result == 0)
                {
                    transaction.Rollback();
                    return false;
                }

                var sqlPagos = @"SELECT IFNULL(SUM(monto),0) 
                         FROM pagos 
                         WHERE fk_id_membresia = @idMembresia";

                var totalPagado = await db.ExecuteScalarAsync<decimal>(
                    sqlPagos,
                    new { idMembresia = pago.FkIdMembresia },
                    transaction
                );

                var sqlMembresia = @"SELECT total 
                             FROM membresias 
                             WHERE id = @idMembresia";

                var totalMembresia = await db.ExecuteScalarAsync<decimal>(
                    sqlMembresia,
                    new { idMembresia = pago.FkIdMembresia },
                    transaction
                );

                var saldo = totalMembresia - totalPagado;

                string estado;
                if (totalPagado == 0)
                    estado = "SIN PAGO";
                else if (saldo > 0)
                    estado = "PENDIENTE";
                else
                    estado = "PAGADO";

                var sqlUpdateMembresia = @"UPDATE membresias 
                                   SET saldo = @saldo, estado = @estado
                                   WHERE id = @idMembresia";

                await db.ExecuteAsync(sqlUpdateMembresia, new
                {
                    saldo,
                    estado,
                    idMembresia = pago.FkIdMembresia
                }, transaction);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task<bool> Delete(int id)
        {
            using var db = dbConnection();
            await db.OpenAsync(); // Abrir la conexión
            using var transaction = db.BeginTransaction();

            try
            {
                var sqlGetMembresia = @"SELECT fk_id_membresia 
                                FROM pagos 
                                WHERE id = @idPago";

                var fkIdMembresia = await db.ExecuteScalarAsync<int?>(sqlGetMembresia, new { idPago = id }, transaction);

                if (fkIdMembresia == null)
                {
                    transaction.Rollback();
                    return false;
                }

                var sqlDelete = @"DELETE FROM pagos WHERE id = @id";
                var result = await db.ExecuteAsync(sqlDelete, new { id }, transaction);

                if (result == 0)
                {
                    transaction.Rollback();
                    return false;
                }

                var sqlPagos = @"SELECT IFNULL(SUM(monto),0) 
                         FROM pagos 
                         WHERE fk_id_membresia = @idMembresia";

                var totalPagado = await db.ExecuteScalarAsync<decimal>(
                    sqlPagos,
                    new { idMembresia = fkIdMembresia },
                    transaction
                );

                var sqlMembresiaTotal = @"SELECT total 
                                  FROM membresias 
                                  WHERE id = @idMembresia";

                var totalMembresia = await db.ExecuteScalarAsync<decimal>(
                    sqlMembresiaTotal,
                    new { idMembresia = fkIdMembresia },
                    transaction
                );

                var saldo = totalMembresia - totalPagado;

                string estado;
                if (totalPagado == 0)
                    estado = "SIN PAGO";
                else if (saldo > 0)
                    estado = "PENDIENTE";
                else
                    estado = "PAGADO";

                var sqlUpdateMembresia = @"UPDATE membresias 
                                   SET saldo = @saldo, estado = @estado
                                   WHERE id = @idMembresia";

                await db.ExecuteAsync(sqlUpdateMembresia, new
                {
                    saldo,
                    estado,
                    idMembresia = fkIdMembresia
                }, transaction);

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}