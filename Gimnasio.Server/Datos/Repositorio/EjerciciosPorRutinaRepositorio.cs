using Gimnasio.Server.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class EjerciciosPorRutinaRepositorio : IEjerciciosPorRutinaRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public EjerciciosPorRutinaRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<EjercicioPorRutina>> GetAll()
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        fk_id_rutina AS FkIdRutina,
                        fK_id_ejercicio AS FkIdEjercicio
                       FROM ejercicios_por_rutina";
            return await db.QueryAsync<EjercicioPorRutina>(sql);
        }

        public async Task<EjercicioPorRutina?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        fk_id_rutina AS FkIdRutina,
                        fK_id_ejercicio AS FkIdEjercicio
                       FROM ejercicios_por_rutina WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<EjercicioPorRutina>(sql, new { Id = id });
        }

        public async Task<EjercicioPorRutina> Create(EjercicioPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO ejercicios_por_rutina (fk_id_rutina, fk_id_ejercicio)
                        VALUES (@fk_id_rutina, @fk_id_ejercicio);
                        SELECT LAST_INSERT_ID(); ";


            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                fk_id_rutina = ejercicioPorRutina.FkIdRutina,
                fk_id_ejercicio = ejercicioPorRutina.FkIdEjercicio
            });

            ejercicioPorRutina.Id = id;
            return ejercicioPorRutina;
        }

        public async Task<bool> Update(EjercicioPorRutina ejercicioPorRutina)
        {
            using var db = DbConnection();
            var sql = @"UPDATE ejercicios_por_rutina SET
                        fk_id_rutina = @fk_id_rutina,
                        fk_id_ejercicio = @fk_id_ejercicio
                        WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new
            {
                fk_id_rutina = ejercicioPorRutina.FkIdRutina,
                fk_id_ejercicio = ejercicioPorRutina.FkIdEjercicio,
                id = ejercicioPorRutina.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM ejercicios_por_rutina WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}