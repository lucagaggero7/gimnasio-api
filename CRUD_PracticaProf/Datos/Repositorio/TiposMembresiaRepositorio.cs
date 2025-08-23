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
    public class TiposMembresiaRepositorio : ITiposMembresiaRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public TiposMembresiaRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<TipoMembresia>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM tiposmembresia";
            return await db.QueryAsync<TipoMembresia>(sql);
        }

        public async Task<TipoMembresia?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM tiposmembresia WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<TipoMembresia>(sql, new { Id = id });
        }

        public async Task<bool> Create(TipoMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "INSERT INTO tiposmembresia (Nombre) VALUES (@Nombre)";
            var result = await db.ExecuteAsync(sql, tipoMembresia);
            return result > 0;
        }

        public async Task<bool> Update(TipoMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "UPDATE tiposmembresia SET Nombre = @Nombre WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, tipoMembresia);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM tiposmembresia WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}