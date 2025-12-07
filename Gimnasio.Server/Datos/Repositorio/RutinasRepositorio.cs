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
                        fk_id_tipo_rutina AS FkIdTipoRutina,
                        fk_id_cliente AS FkIdCliente
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
                        fk_id_tipo_rutina AS FkIdTipoRutina,
                        fk_id_cliente AS FkIdCliente
                        FROM rutinas WHERE id = @id";
            return await db.QueryFirstOrDefaultAsync<Rutina>(sql, new { Id = id });
        }

        public async Task<Rutina> Create(Rutina rutina, List<int> ejercicios)
        {
            using var db = DbConnection();
            using var transaction = db.BeginTransaction();

            try
            {
                // Crear la rutina
                var sqlRutina = @"INSERT INTO rutinas 
                          (nombre, fecha_inicio, duracion, frecuencia_sem, objetivo, fk_id_tipo_rutina, fk_id_cliente)
                          VALUES (@nombre, @fecha_inicio, @duracion, @frecuencia_sem, @objetivo, @fk_id_tipo_rutina, @fk_id_cliente);
                          SELECT LAST_INSERT_ID();";

                var idRutina = await db.ExecuteScalarAsync<int>(sqlRutina, new
                {
                    nombre = rutina.Nombre,
                    fecha_inicio = rutina.FechaInicio,
                    duracion = rutina.Duracion,
                    frecuencia_sem = rutina.FrecuenciaSem,
                    objetivo = rutina.Objetivo,
                    fk_id_tipo_rutina = rutina.FkIdTipoRutina,
                    fk_id_cliente = rutina.FkIdCliente
                }, transaction);

                rutina.Id = idRutina;

                // Insertar ejercicios asociados
                var sqlEjercicio = @"INSERT INTO rutina_ejercicio (fk_id_rutina, fk_id_ejercicio)
                             VALUES (@fk_id_rutina, @fk_id_ejercicio)";

                foreach (var e in ejercicios)
                {
                    await db.ExecuteAsync(sqlEjercicio, new
                    {
                        fk_id_rutina = idRutina,
                        fk_id_ejercicio = e
                    }, transaction);
                }

                transaction.Commit();
                return rutina;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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
                        fk_id_tipo_rutina = @fk_id_tipo_rutina,
                        fk_id_membresia = @fk_id_membresia
                        WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new
            {
                nombre = rutina.Nombre,
                fecha_inicio = rutina.FechaInicio,
                duracion = rutina.Duracion,
                frecuencia_sem = rutina.FrecuenciaSem,
                objetivo = rutina.Objetivo,
                fk_id_tipo_rutina = rutina.FkIdTipoRutina,
                fk_id_cliente = rutina.FkIdCliente,
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