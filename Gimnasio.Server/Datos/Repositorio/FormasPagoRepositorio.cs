using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Gimnasio.Server.Modelos.Entidades;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class FormasPagoRepositorio : IFormasPagoRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public FormasPagoRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<FormaPago>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM formas_pago";
            return await db.QueryAsync<FormaPago>(sql);
        }

        public async Task<FormaPago?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM formas_pago WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<FormaPago>(sql, new { Id = id });
        }

        public async Task<FormaPago> Create(FormaPago formaPago)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO formas_pago (nombre) VALUES (@nombre);
                         SELECT LAST_INSERT_ID(); ";


            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                nombre = formaPago.Nombre
            });

            formaPago.Id = id;
            return formaPago;
        }

        public async Task<bool> Update(FormaPago formaPago)
        {
            using var db = DbConnection();
            var sql = "UPDATE formas_pago SET nombre = @nombre WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new
            {
                nombre = formaPago.Nombre,
                id = formaPago.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM formas_pago WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}