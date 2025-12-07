using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Gimnasio.Server.Modelos.Entidades;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class RutinaEjercicioRepositorio : IRutinaEjercicioRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public RutinaEjercicioRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<RutinaEjercicio>> GetAll()
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        fk_id_rutina AS FkIdRutina,
                        fK_id_ejercicio AS FkIdEjercicio
                       FROM ejercicios_por_rutina";
            return await db.QueryAsync<RutinaEjercicio>(sql);
        }

        public async Task<RutinaEjercicio?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        fk_id_rutina AS FkIdRutina,
                        fK_id_ejercicio AS FkIdEjercicio
                       FROM ejercicios_por_rutina WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<RutinaEjercicio>(sql, new { Id = id });
        }

        public async Task<RutinaEjercicio> Create(RutinaEjercicio ejercicioPorRutina)
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

        public async Task<bool> Update(RutinaEjercicio ejercicioPorRutina)
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