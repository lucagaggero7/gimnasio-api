using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class EvaluacionRepositorio : IEvaluacionRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public EvaluacionRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Evaluacion>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM evaluaciones";
            return await db.QueryAsync<Evaluacion>(sql);
        }

        public async Task<Evaluacion?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM evaluaciones WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Evaluacion>(sql, new { Id = id });
        }

        public async Task<bool> Create(Evaluacion evaluacion)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO evaluaciones (Tipo, Fecha, Hora, fk_idClientes)
                 VALUES (@Tipo, @Fecha, @Hora, @fk_idClientes)";

            var result = await db.ExecuteAsync(sql, evaluacion);
            return result > 0;
        }

        public async Task<bool> Update(Evaluacion evaluacion)
        {
            using var db = DbConnection();
            var sql = @"UPDATE evaluaciones SET
                 Tipo = @Tipo,
                 Fecha = @Fecha,
                 Hora = @Hora,
                 fk_idClientes = @fk_idClientes
                 WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql, evaluacion);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM evaluaciones WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
