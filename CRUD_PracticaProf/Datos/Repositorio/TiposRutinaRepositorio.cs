using CRUD_PracticaProf.Modelos; 
using CRUD_PracticaProf.Datos.Repositorio; 
using CRUD_PracticaProf; 
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD_PracticaProf.Datos.Repositorio
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
        var sql = "SELECT * FROM tiposrutina";
        return await db.QueryAsync<TipoRutina>(sql);
    }

    public async Task<TipoRutina?> GetById(int id)
    {
        using var db = DbConnection();
        var sql = "SELECT * FROM tiposrutina WHERE Id = @Id";
        return await db.QueryFirstOrDefaultAsync<TipoRutina>(sql, new { Id = id });
    }

    public async Task<bool> Create(TipoRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = "INSERT INTO tiposrutina (Nombre) VALUES (@Nombre)";
        var result = await db.ExecuteAsync(sql, tipoRutina);
        return result > 0;
    }

    public async Task<bool> Update(TipoRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = "UPDATE tiposrutina SET Nombre = @Nombre WHERE Id = @Id";
        var result = await db.ExecuteAsync(sql, tipoRutina);
        return result > 0;
    }

    public async Task<bool> Delete(int id)
    {
        using var db = DbConnection();
        var sql = "DELETE FROM tiposrutina WHERE Id = @Id";
        var result = await db.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}
}