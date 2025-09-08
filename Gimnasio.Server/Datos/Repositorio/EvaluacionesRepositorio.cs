using CRUD_PracticaProf.DTO;
using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System.Data;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class EvaluacionesRepositorio : IEvaluacionesRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public EvaluacionesRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Evaluacion>> GetAll()
        {
            using var db = DbConnection();

            var sql = @"SELECT 
                        e.id AS Id, e.tipo AS Tipo, e.fecha AS Fecha, e.hora AS Hora, e.fk_id_cliente AS FkIdCliente,
                         c.id AS Id, c.nombre AS Nombre, c.apellido AS Apellido
                        FROM evaluaciones e
                        INNER JOIN clientes c ON e.fk_id_cliente = c.id";


            var evaluaciones = await db.QueryAsync<Evaluacion, ClienteMostrarDTO, Evaluacion>(
                sql,
                (evaluacion, cliente) =>
                {
                    evaluacion.Cliente = cliente;
                    return evaluacion;
                },
                splitOn: "Id"
            );

            return evaluaciones;
        }

        public async Task<Evaluacion?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = @"SELECT 
                        e.id AS Id, e.tipo AS Tipo, e.fecha AS Fecha, e.hora AS Hora, e.fk_id_cliente AS FkIdCliente,
                         c.id AS Id, c.nombre AS Nombre, c.apellido AS Apellido
                        FROM evaluaciones e
                        INNER JOIN clientes c ON e.fk_id_cliente = c.id
                        WHERE e.id = @id";

            var evaluacion = await db.QueryAsync<Evaluacion, ClienteMostrarDTO, Evaluacion>(
                    sql,
                    (evaluacion, cliente) =>
                    {
                         evaluacion.Cliente = cliente;
                          return evaluacion;
                     },
                         new { id },
                     splitOn: "Id"
                    );

            return evaluacion.FirstOrDefault();
        }

        public async Task<Evaluacion> Create(Evaluacion evaluacion)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO evaluaciones (tipo, fecha, hora, fk_id_cliente)
                 VALUES (@tipo, @fecha, @hora, @fk_id_cliente);
                  SELECT LAST_INSERT_ID(); ";

            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                tipo = evaluacion.Tipo,
                fecha = evaluacion.Fecha,
                hora = evaluacion.Hora,
                fk_id_cliente = evaluacion.FkIdCliente
            });

            evaluacion.Id = id;
            return evaluacion;
        }

        public async Task<bool> Update(Evaluacion evaluacion)
        {
            using var db = DbConnection();
            var sql = @"UPDATE evaluaciones SET
                 tipo = @tipo,
                 fecha = @fecha,
                 hora = @hora,
                 fk_id_cliente = @fk_id_cliente
                 WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new
            {
                tipo = evaluacion.Tipo,
                fecha = evaluacion.Fecha,
                hora = evaluacion.Hora,
                fk_id_cliente = evaluacion.FkIdCliente,
                id = evaluacion.Id
            });
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM evaluaciones WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}
