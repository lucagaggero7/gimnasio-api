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
using Gimnasio.Server.Modelos.DTO;

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

        public async Task<IEnumerable<RutinaListaDTO>> GetAll()
        {
            using var db = DbConnection();
            db.Open();

            var sqlRutinas = @"SELECT id AS Id,
                       nombre AS Nombre,
                       fecha_inicio AS FechaInicio,
                       duracion AS Duracion,
                       frecuencia_sem AS FrecuenciaSem,
                       objetivo AS Objetivo,
                       fk_id_tipo_rutina AS FkIdTipoRutina,
                       fk_id_cliente AS FkIdCliente
                       FROM rutinas";

            var rutinas = (await db.QueryAsync<RutinaListaDTO>(sqlRutinas)).ToList();

            if (!rutinas.Any())
                return rutinas;

            var ids = rutinas.Select(r => r.Id).ToArray();

            var sqlEjercicios = @"
            SELECT 
            re.fk_id_rutina AS RutinaId,
            e.id AS Id,
            e.nombre AS Nombre,
            e.series AS Series,
            e.repeticiones AS Repeticiones,
            e.notas AS Notas
            FROM rutina_ejercicio re
            INNER JOIN ejercicios e ON e.id = re.fk_id_ejercicio
            WHERE re.fk_id_rutina IN @ids";

            var ejercicios = await db.QueryAsync<int, Ejercicio, (int RutinaId, Ejercicio Ej)>(
                sqlEjercicios,
                map: (rutinaId, ejercicio) => (rutinaId, ejercicio),
                splitOn: "Id",      // IMPORTANTE → acá empieza el objeto Ejercicio
                param: new { ids }
            );

            foreach (var r in rutinas)
            {
                r.Ejercicios = ejercicios
                    .Where(x => x.RutinaId == r.Id)
                    .Select(x => x.Ej)
                    .ToList();
            }

            return rutinas;
        }

        public async Task<RutinaListaDTO?> GetById(int id)
        {
            using var db = DbConnection();
            db.Open();

            var sqlRutina = @"SELECT id AS Id,
                      nombre AS Nombre,
                      fecha_inicio AS FechaInicio,
                      duracion AS Duracion,
                      frecuencia_sem AS FrecuenciaSem,
                      objetivo AS Objetivo,
                      fk_id_tipo_rutina AS FkIdTipoRutina,
                      fk_id_cliente AS FkIdCliente
                      FROM rutinas
                      WHERE id = @id";

            var rutina = await db.QueryFirstOrDefaultAsync<RutinaListaDTO>(sqlRutina, new { id });

            if (rutina == null)
                return null;

            var sqlEjercicios = @"SELECT e.*
                          FROM rutina_ejercicio re
                          INNER JOIN ejercicios e ON e.id = re.fk_id_ejercicio
                          WHERE re.fk_id_rutina = @id";

            rutina.Ejercicios = (await db.QueryAsync<Ejercicio>(sqlEjercicios, new { id })).ToList();

            return rutina;
        }


        public async Task<Rutina> Create(Rutina rutina, List<int> ejercicios)
        {
            using var db = DbConnection();
            db.Open();
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

        public async Task<bool> Update(Rutina rutina, List<int> ejercicios)
        {
            using var db = DbConnection();
            db.Open();

            using var transaction = db.BeginTransaction();

            try
            {
                // 1️⃣ Actualizar los datos de la rutina
                var sqlRutina = @"UPDATE rutinas SET
                        nombre = @nombre,
                        fecha_inicio = @fecha_inicio,
                        duracion = @duracion,
                        frecuencia_sem = @frecuencia_sem,
                        objetivo = @objetivo,
                        fk_id_tipo_rutina = @fk_id_tipo_rutina,
                        fk_id_cliente = @fk_id_cliente
                        WHERE id = @id";

                var result = await db.ExecuteAsync(sqlRutina, new
                {
                    nombre = rutina.Nombre,
                    fecha_inicio = rutina.FechaInicio,
                    duracion = rutina.Duracion,
                    frecuencia_sem = rutina.FrecuenciaSem,
                    objetivo = rutina.Objetivo,
                    fk_id_tipo_rutina = rutina.FkIdTipoRutina,
                    fk_id_cliente = rutina.FkIdCliente,
                    id = rutina.Id
                }, transaction);

                if (result == 0)
                {
                    transaction.Rollback();
                    return false; // No existe la rutina
                }

                // 2️⃣ Borrar ejercicios actuales
                var sqlDelete = @"DELETE FROM rutina_ejercicio WHERE fk_id_rutina = @idRutina";
                await db.ExecuteAsync(sqlDelete, new { idRutina = rutina.Id }, transaction);

                // 3️⃣ Insertar los nuevos ejercicios
                var sqlInsert = @"INSERT INTO rutina_ejercicio (fk_id_rutina, fk_id_ejercicio)
                          VALUES (@fk_id_rutina, @fk_id_ejercicio)";

                foreach (var e in ejercicios)
                {
                    await db.ExecuteAsync(sqlInsert, new
                    {
                        fk_id_rutina = rutina.Id,
                        fk_id_ejercicio = e
                    }, transaction);
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            db.Open();
            using var tx = db.BeginTransaction();

            try
            {
                // Borrar de la tabla intermedia
                await db.ExecuteAsync(
                    "DELETE FROM rutina_ejercicio WHERE fk_id_rutina = @id",
                    new { id }, tx);

                // Borrar la rutina
                var rows = await db.ExecuteAsync(
                    "DELETE FROM rutinas WHERE id = @id",
                    new { id }, tx);

                tx.Commit();
                return rows > 0;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

    }
}