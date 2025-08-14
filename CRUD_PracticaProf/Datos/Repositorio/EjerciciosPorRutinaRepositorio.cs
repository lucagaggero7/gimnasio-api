using CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class EjerciciosPorRutinaRepositorio : IEjerciciosPorRutinaRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public EjerciciosPorRutinaRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<EjerciciosPorRutina>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM ejerciciosporrutina";
            return await db.QueryAsync<EjerciciosPorRutina>(sql);
        }

        public async Task<EjerciciosPorRutina?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM ejerciciosporrutina WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<EjerciciosPorRutina>(sql, new { Id = id });
        }

        public async Task<bool> Create(EjerciciosPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO ejerciciosporrutina (fk_idRutinas, fk_idEjercicios)
                        VALUES (@fk_idRutinas, @fk_idEjercicios)";
            var result = await db.ExecuteAsync(sql, ejercicioPorRutina);
            return result > 0;
        }

        public async Task<bool> Update(EjerciciosPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"UPDATE ejerciciosporrutina SET
                        fk_idRutinas = @fk_idRutinas,
                        fk_idEjercicios = @fk_idEjercicios
                        WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, ejercicioPorRutina);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM ejerciciosporrutina WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}