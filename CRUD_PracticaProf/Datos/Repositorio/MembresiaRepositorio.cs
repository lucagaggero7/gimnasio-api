using CRUD_PracticaProf.Modelos; 
using CRUD_PracticaProf.Datos.Repositorio; 
using CRUD_PracticaProf; 
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class MembresiaRepositorio : IMembresiaRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public MembresiaRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected IDbConnection DbConnection() => new MySqlConnection(_connectionString.ConnectionString);

        public async Task<IEnumerable<Membresia>> GetAll()
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Membresia";
            return await db.QueryAsync<Membresia>(sql);
        }

        public async Task<Membresia?> GetById(int id)
        {
            using var db = DbConnection();
            var sql = "SELECT * FROM Membresia WHERE idMembresia = @Id";
            return await db.QueryFirstOrDefaultAsync<Membresia>(sql, new { Id = id });
        }

        public async Task<bool> Create(Membresia membresia)
        {
            using var db = DbConnection();
            var sql = @"INSERT INTO Membresia (Estado, Fecha_inicio, Fecha_vencimiento, Contacto_emergencia, Nombre_contacto, fk_idClientes, fk_idTiposMembresia, fk_idRutina)
                        VALUES (@Estado, @Fecha_inicio, @Fecha_vencimiento, @Contacto_emergencia, @Nombre_contacto, @fk_idClientes, @fk_idTiposMembresia, @fk_idRutina)";

            var result = await db.ExecuteAsync(sql, membresia);
            return result > 0;
        }

        public async Task<bool> Update(Membresia membresia)
        {
            using var db = DbConnection();
            var sql = @"UPDATE Membresia SET
                        Estado = @Estado,
                        Fecha_inicio = @Fecha_inicio,
                        Fecha_vencimiento = @Fecha_vencimiento,
                        Contacto_emergencia = @Contacto_emergencia,
                        Nombre_contacto = @Nombre_contacto,
                        fk_idClientes = @fk_idClientes,
                        fk_idTiposMembresia = @fk_idTiposMembresia,
                        fk_idRutina = @fk_idRutina
                        WHERE idMembresia = @idMembresia";

            var result = await db.ExecuteAsync(sql, membresia);
            return result > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var db = DbConnection();
            var sql = "DELETE FROM Membresia WHERE idMembresia = @Id";
            var result = await db.ExecuteAsync(sql, new { Id = id });
            return result > 0;
        }
    }
}