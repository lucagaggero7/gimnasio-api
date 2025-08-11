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
            var sql = "SELECT * FROM Ejercicios_por_Rutina";
            return await db.QueryAsync<EjerciciosPorRutina>(sql);
        }

        public async Task<EjerciciosPorRutina?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Ejercicios_por_Rutina WHERE idEjercicios_por_rutina = @Id";
            return await db.QueryFirstOrDefaultAsync<EjerciciosPorRutina>(sql, new { Id = id });
        }

        public async Task<bool> Create(EjerciciosPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO Ejercicios_por_Rutina (fk_idRutinas, fk_idEjercicios)
                        VALUES (@fk_idRutinas, @fk_idEjercicios)";
            var result = await db.ExecuteAsync(sql, ejercicioPorRutina);
            return result > 0;
        }

        public async Task<bool> Update(EjerciciosPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"UPDATE Ejercicios_por_Rutina SET
                        fk_idRutinas = @fk_idRutinas,
                        fk_idEjercicios = @fk_idEjercicios
                        WHERE idEjercicios_por_rutina = @idEjercicios_por_rutina";
            var result = await db.ExecuteAsync(sql, ejercicioPorRutina);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM Ejercicios_por_Rutina WHERE idEjercicios_por_rutina = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}