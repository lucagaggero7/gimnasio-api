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
            var sql = "SELECT * FROM Pagos";
            return await db.QueryAsync<Pago>(sql);
        }

        public async Task<Pago?> GetById(int id)
        {
            using var db = dbConnection();
            var sql = "SELECT * FROM Pagos WHERE idPagos = @Id";
            return await db.QueryFirstOrDefaultAsync<Pago>(sql, new { Id = id });
        }

        public async Task<bool> Create(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"INSERT INTO Pagos (Monto, Fecha, fk_idFormasPago, fk_idMembresia, fk_idClientes)
                        VALUES (@Monto, @Fecha, @fk_idFormasPago, @fk_idMembresia, @fk_idClientes)";

            var result = await db.ExecuteAsync(sql, new { pago.Monto, pago.Fecha, pago.fk_idFormasPago, pago.fk_idMembresia, pago.fk_idClientes });
            return result > 0;
        }

        public async Task<bool> Update(Pago pago)
        {
            using var db = dbConnection();
            var sql = @"UPDATE Pagos SET
                        Monto = @Monto,
                        Fecha = @Fecha,
                        fk_idFormasPago = @fk_idFormasPago,
                        fk_idMembresia = @fk_idMembresia,
                        fk_idClientes = @fk_idClientes
                        WHERE idPagos = @idPagos";

            var result = await db.ExecuteAsync(sql, new { pago.Monto, pago.Fecha, pago.fk_idFormasPago, pago.fk_idMembresia, pago.fk_idClientes, pago.idPagos });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = dbConnection();
            var sql = "DELETE FROM Pagos WHERE idPagos = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}