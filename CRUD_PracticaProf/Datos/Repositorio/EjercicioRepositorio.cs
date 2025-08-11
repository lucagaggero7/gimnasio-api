using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using static CRUD_PracticaProf.Datos.Repositorio.EjercicioRepositorio;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class EjercicioRepositorio : IEjercicioRepositorio
    {

            private readonly MySQLConfig _connectionString;

            public EjercicioRepositorio(MySQLConfig connectionString)
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

            public async Task<Ejercicio> GetById(int id)
            {
                var db = dbConnection();

                var sql = @"SELECT * FROM ejercicios WHERE Id = @Id";

                return await db.QueryFirstOrDefaultAsync<Ejercicio>(sql, new { Id = id });
            }

            public async Task<bool> Create(Ejercicio ejercicio)
            {
                var db = dbConnection();

                var sql = @"INSERT INTO ejercicios (Nombre, Series, Repeticiones, Notas)
                        VALUES (@Nombre, @Series, @Repeticiones, @Notas) ";

                var result = await db.ExecuteAsync(sql, new
                {
                    ejercicio.Nombre,
                    ejercicio.Series,
                    ejercicio.Repeticiones,
                    ejercicio.Notas
                });

                return result > 0; // Returns true if one or more rows were affected
            }

            public async Task<bool> Update(Ejercicio ejercicio)
            {
                var db = dbConnection();

                var sql = @"UPDATE ejercicios SET 
                        Nombre = @Nombre,
                        Series = @Series,
                        Repeticiones = @Repeticiones,
                        Notas = @Notas";

                var result = await db.ExecuteAsync(sql, new
                {
                    ejercicio.Nombre,
                    ejercicio.Series,
                    ejercicio.Repeticiones,
                    ejercicio.Notas
                });

                return result > 0; // Returns true if one or more rows were affected
            }

            public async Task<bool> Delete(Ejercicio ejercicio)
            {
                var db = dbConnection();

                var sql = @"DELETE FROM ejercicios WHERE Id = @Id";

                var result = await db.ExecuteAsync(sql, new { Id = ejercicio.Id });

                return result > 0; // Returns true if one or more rows were affected
            }
        
    }
}
