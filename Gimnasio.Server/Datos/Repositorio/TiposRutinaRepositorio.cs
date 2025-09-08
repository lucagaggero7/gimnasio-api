using Gimnasio.Server.Modelos; 
using Gimnasio.Server.Datos.Repositorio; 
using Gimnasio.Server; 
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gimnasio.Server.Datos.Repositorio
{
    public class TiposRutinaRepositorio : ITiposRutinaRepositorio
{
    private readonly MySQLConfig _connectionString;

    public TiposRutinaRepositorio(MySQLConfig connectionString)
    {
        _connectionString = connectionString;
    }

    protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

    public async Task<IEnumerable<TipoRutina>> GetAll()
    {
        using var db = DbConnection();
        var sql = "SELECT * FROM tipos_rutina";
        return await db.QueryAsync<TipoRutina>(sql);
    }

    public async Task<TipoRutina?> GetById(int id)
    {
        using var db = DbConnection();
        var sql = "SELECT * FROM tipos_rutina WHERE id = @id";
        return await db.QueryFirstOrDefaultAsync<TipoRutina>(sql, new { Id = id });
    }

    public async Task<TipoRutina> Create(TipoRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = @"INSERT INTO tipos_rutina (nombre) VALUES (@nombre);
                  SELECT LAST_INSERT_ID(); ";

        var id = await db.ExecuteScalarAsync<int>(sql, new
        {
            nombre = tipoRutina.Nombre
        });

        tipoRutina.Id = id;
        return tipoRutina;
    }

    public async Task<bool> Update(TipoRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = "UPDATE tipos_rutina SET nombre = @nombre WHERE id = @id";
            var result = await db.ExecuteAsync(sql, new
            {
                nombre = tipoRutina.Nombre,
                id = tipoRutina.Id
            });
            return result > 0;
    }

    public async Task<bool> Delete(int id)
    {
        using var db = DbConnection();
        var sql = "DELETE FROM tipos_rutina WHERE id = @id";
        var result = await db.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}
}