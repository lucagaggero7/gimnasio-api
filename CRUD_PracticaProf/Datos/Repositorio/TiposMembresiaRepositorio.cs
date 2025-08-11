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

        public async Task<IEnumerable<TiposMembresia>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Tipos_Membresia";
            return await db.QueryAsync<TiposMembresia>(sql);
        }

        public async Task<TiposMembresia?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Tipos_Membresia WHERE idTipos_Membresia = @Id";
            return await db.QueryFirstOrDefaultAsync<TiposMembresia>(sql, new { Id = id });
        }

        public async Task<bool> Create(TiposMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "INSERT INTO Tipos_Membresia (Nombre) VALUES (@Nombre)";
            var result = await db.ExecuteAsync(sql, tipoMembresia);
            return result > 0;
        }

        public async Task<bool> Update(TiposMembresia tipoMembresia)
        {
            using var db = DbConnection();
            var sql = "UPDATE Tipos_Membresia SET Nombre = @Nombre WHERE idTipos_Membresia = @idTipos_Membresia";
            var result = await db.ExecuteAsync(sql, tipoMembresia);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM Tipos_Membresia WHERE idTipos_Membresia = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}