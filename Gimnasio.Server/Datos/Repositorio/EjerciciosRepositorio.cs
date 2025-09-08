using Gimnasio.Server.Entidades;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gimnasio.Server.Datos.Repositorio.EjerciciosRepositorio;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class EjerciciosRepositorio : IEjerciciosRepositorio
    {

            private readonly MySQLConfig _connectionString;

            public EjerciciosRepositorio(MySQLConfig connectionString)
            {
                _connectionString = connectionString;
            }

            protected MySqlConnection dbConnection()
            {
                return new MySqlConnection(_connectionString.ConnectionString);
            }


            public async Task<IEnumerable<Ejercicio>> GetAll()
            {
                var db = dbConnection();

                var sql = @"SELECT * FROM ejercicios";

                return await db.QueryAsync<Ejercicio>(sql, new { });
            }

            public async Task<Ejercicio?> GetById(int id)
            {
                var db = dbConnection();

                var sql = @"SELECT * FROM ejercicios WHERE id = @id";

                return await db.QueryFirstOrDefaultAsync<Ejercicio>(sql, new { Id = id });
            }

            public async Task<Ejercicio> Create(Ejercicio ejercicio)
            {
                var db = dbConnection();

                var sql = @"INSERT INTO ejercicios (nombre, series, repeticiones, notas)
                        VALUES (@nombre, @series, @repeticiones, @notas);
                        SELECT LAST_INSERT_ID(); ";

                var id = await db.ExecuteScalarAsync<int>(sql, new
                {
                    nombre = ejercicio.Nombre,
                    series = ejercicio.Series,
                    repeticiones = ejercicio.Repeticiones,
                    notas = ejercicio.Notas
                });

                ejercicio.Id = id;
                 return ejercicio;
            }

            public async Task<bool> Update(Ejercicio ejercicio)
            {
                var db = dbConnection();

                var sql = @"UPDATE ejercicios SET 
                        nombre = @nombre,
                        series = @series,
                        repeticiones = @repeticiones,
                        notas = @notas
                        WHERE id = @id";


            var result = await db.ExecuteAsync(sql, new
            {
                nombre = ejercicio.Nombre,
                series = ejercicio.Series,
                repeticiones = ejercicio.Repeticiones,
                notas = ejercicio.Notas,
                id = ejercicio.Id
            });

                return result > 0; // Returns true if one or more rows were affected
            }

            public async Task<bool> Delete(int id)
            {
                var db = dbConnection();

                var sql = @"DELETE FROM ejercicios WHERE id = @id";

                var result = await db.ExecuteAsync(sql, new { Id = id });

                return result > 0; // Returns true if one or more rows were affected
            }
        
    }
}
