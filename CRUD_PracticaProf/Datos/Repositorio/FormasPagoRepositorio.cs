using CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class FormasPagoRepositorio : IFormasPagoRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public FormasPagoRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<FormasPago>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM formaspago";
            return await db.QueryAsync<FormasPago>(sql);
        }

        public async Task<FormasPago?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM formaspago WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<FormasPago>(sql, new { Id = id });
        }

        public async Task<bool> Create(FormasPago formaPago)
        {
            using var db = DbConnection();
            var sql = "INSERT INTO formaspago (Nombre) VALUES (@Nombre)";
            var result = await db.ExecuteAsync(sql, formaPago);
            return result > 0;
        }

        public async Task<bool> Update(FormasPago formaPago)
        {
            using var db = DbConnection();
            var sql = "UPDATE formaspago SET Nombre = @Nombre WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, formaPago);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM formaspago WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}