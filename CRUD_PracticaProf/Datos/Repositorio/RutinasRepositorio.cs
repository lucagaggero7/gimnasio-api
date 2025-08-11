using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
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
            var sql = "SELECT * FROM Rutinas";
            return await db.QueryAsync<Rutina>(sql);
        }

        public async Task<Rutina?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Rutinas WHERE idRutinas = @Id";
            return await db.QueryFirstOrDefaultAsync<Rutina>(sql, new { Id = id });
        }

        public async Task<bool> Create(Rutina rutina)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO Rutinas (Nombre, Fecha_inicio, Duracion, Frecuencia_sem, Objetivo, fk_idTiposRutina)
                        VALUES (@Nombre, @Fecha_inicio, @Duracion, @Frecuencia_sem, @Objetivo, @fk_idTiposRutina)";

            var result = await db.ExecuteAsync(sql, rutina);
            return result > 0;
        }

        public async Task<bool> Update(Rutina rutina)
        {
            using var db = DbConnection();
            var sql = @"UPDATE Rutinas SET
                        Nombre = @Nombre,
                        Fecha_inicio = @Fecha_inicio,
                        Duracion = @Duracion,
                        Frecuencia_sem = @Frecuencia_sem,
                        Objetivo = @Objetivo,
                        fk_idTiposRutina = @fk_idTiposRutina
                        WHERE idRutinas = @idRutinas";

            var result = await db.ExecuteAsync(sql, rutina);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM Rutinas WHERE idRutinas = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}