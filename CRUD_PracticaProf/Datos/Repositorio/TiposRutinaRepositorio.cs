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

    public async Task<IEnumerable<TiposRutina>> GetAll()
    {
        using var db = DbConnection();
        var sql = "SELECT * FROM TiposRutina";
        return await db.QueryAsync<TiposRutina>(sql);
    }

    public async Task<TiposRutina?> GetById(int id)
    {
        using var db = DbConnection();
        var sql = "SELECT * FROM TiposRutina WHERE idTipos_Rutina = @Id";
        return await db.QueryFirstOrDefaultAsync<TiposRutina>(sql, new { Id = id });
    }

    public async Task<bool> Create(TiposRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = "INSERT INTO TiposRutina (Nombre) VALUES (@Nombre)";
        var result = await db.ExecuteAsync(sql, tipoRutina);
        return result > 0;
    }

    public async Task<bool> Update(TiposRutina tipoRutina)
    {
        using var db = DbConnection();
        var sql = "UPDATE TiposRutina SET Nombre = @Nombre WHERE idTipos_Rutina = @idTipos_Rutina";
        var result = await db.ExecuteAsync(sql, tipoRutina);
        return result > 0;
    }

    public async Task<bool> Delete(int id)
    {
        using var db = DbConnection();
        var sql = "DELETE FROM TiposRutina WHERE idTipos_Rutina = @Id";
        var result = await db.ExecuteAsync(sql, new { Id = id });
        return result > 0;
    }
}
}