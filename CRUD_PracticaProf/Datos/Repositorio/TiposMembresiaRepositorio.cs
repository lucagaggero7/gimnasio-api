using CRUD_PracticaProf; 
using CRUD_PracticaProf.Datos.Repositorio; 
using CRUD_PracticaProf.Modelos; 
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
            var sql = "SELECT * FROM tipos_membresia";
            return await db.QueryAsync<TipoMembresia>(sql);
        }

        public async Task<TipoMembresia> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM tipos_membresia WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<TipoMembresia>(sql, new { Id = id });
        }

        public async Task<bool> Create(TipoMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "INSERT INTO tipos_membresia (nombre) VALUES (@nombre)";
            var result = await db.ExecuteAsync(sql, new
            {
                nombre = tipoMembresia.Nombre
            });
            return result > 0;
        }

        public async Task<bool> Update(TipoMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "UPDATE tipos_membresia SET nombre = @nombre WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new
            {
                nombre = tipoMembresia.Nombre,
                id = tipoMembresia.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM tipos_membresia WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}