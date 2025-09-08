using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gimnasio.Server.Modelos.Entidades;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class RutinasRepositorio : IRutinasRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public RutinasRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Rutina>> GetAll()
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        nombre AS Nombre,
                        fecha_inicio AS FechaInicio,
                        duracion AS Duracion, 
                        objetivo AS Objetivo,
                        frecuencia_sem AS FrecuenciaSem,
                        fk_id_tipo_rutina AS FkIdTipoRutina
                        FROM rutinas";
            return await db.QueryAsync<Rutina>(sql);
        }

        public async Task<Rutina?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = @"SELECT id AS Id,
                        nombre AS Nombre,
                        fecha_inicio AS FechaInicio,
                        duracion AS Duracion, 
                        objetivo AS Objetivo,
                        frecuencia_sem AS FrecuenciaSem,
                        fk_id_tipo_rutina AS FkIdTipoRutina
                        FROM rutinas WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<Rutina>(sql, new { Id = id });
        }

        public async Task<Rutina> Create(Rutina rutina)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO rutinas (nombre, fecha_inicio, duracion, frecuencia_sem, objetivo, fk_id_tipo_rutina)
                        VALUES (@nombre, @fecha_inicio, @duracion, @frecuencia_sem, @objetivo, @fk_id_tipo_rutina);
                        SELECT LAST_INSERT_ID(); ";

            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                nombre = rutina.Nombre,
                fecha_inicio = rutina.FechaInicio,
                duracion = rutina.Duracion,
                frecuencia_sem = rutina.FrecuenciaSem,
                objetivo = rutina.Objetivo,
                fk_id_tipo_rutina = rutina.FkIdTipoRutina
            });

            rutina.Id = id;
            return rutina;
        }

        public async Task<bool> Update(Rutina rutina)
        {
            using var db = DbConnection();
            var sql = @"UPDATE rutinas SET
                        nombre = @nombre,
                        fecha_inicio = @fecha_inicio,
                        duracion = @duracion,
                        frecuencia_sem = @frecuencia_sem,
                        objetivo = @objetivo,
                        fk_id_tipo_rutina = @fk_id_tipo_rutina
                        WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new
            {
                nombre = rutina.Nombre,
                fecha_inicio = rutina.FechaInicio,
                duracion = rutina.Duracion,
                frecuencia_sem = rutina.FrecuenciaSem,
                objetivo = rutina.Objetivo,
                fk_id_tipo_rutina = rutina.FkIdTipoRutina,
                id = rutina.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM rutinas WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}