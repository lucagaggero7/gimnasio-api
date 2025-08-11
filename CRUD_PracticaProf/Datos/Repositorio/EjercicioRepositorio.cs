using CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class EjercicioRepositorio : IEjercicioRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public EjercicioRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Ejercicio>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Ejercicios";
            return await db.QueryAsync<Ejercicio>(sql);
        }

        public async Task<Ejercicio?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Ejercicios WHERE idEjercicios = @Id";
            return await db.QueryFirstOrDefaultAsync<Ejercicio>(sql, new { Id = id });
        }

        public async Task<bool> Create(Ejercicio ejercicio)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO Ejercicios (Nombre, Series, Repeticiones, Notas)
                        VALUES (@Nombre, @Series, @Repeticiones, @Notas)";
            var result = await db.ExecuteAsync(sql, ejercicio);
            return result > 0;
        }

        public async Task<bool> Update(Ejercicio ejercicio)
        {
            using var db = DbConnection();
            var sql = @"UPDATE Ejercicios SET
                        Nombre = @Nombre,
                        Series = @Series,
                        Repeticiones = @Repeticiones,
                        Notas = @Notas
                        WHERE idEjercicios = @idEjercicios";
            var result = await db.ExecuteAsync(sql, ejercicio);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM Ejercicios WHERE idEjercicios = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}