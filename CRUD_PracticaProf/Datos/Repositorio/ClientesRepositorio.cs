using CRUD_PracticaProf.DTO;
using CRUD_PracticaProf.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly MySQLConfig _connectionString;

        public ClientesRepositorio(MySQLConfig connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString); 
        }


        public async Task<IEnumerable<Cliente>> GetAll()
        {
            var db = dbConnection();
            
            var sql = @"SELECT * FROM clientes";

            return await  db.QueryAsync<Cliente>(sql, new { });
        }

        public async Task<IEnumerable<ClienteMostrarDTO>> GetAllDTO()
        {
            using var db = dbConnection();
            var sql = "SELECT Id, Nombre, Apellido FROM Clientes";

            return await db.QueryAsync<ClienteMostrarDTO>(sql, new { });
        }

        public async Task<Cliente> GetById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM clientes WHERE Id = @Id";

            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
        }

        public async Task<bool> Create(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO clientes (Nombre, Apellido, Dni, Email, Telefono, Direccion, Fecha_nacimiento)
                        VALUES (@Nombre, @Apellido, @Dni, @Email, @Telefono, @Direccion, @Fecha_nacimiento) ";

            var result = await db.ExecuteAsync(sql, new
            {
                cliente.Nombre,
                cliente.Apellido,
                cliente.Dni,
                cliente.Email,
                cliente.Telefono,
                cliente.Direccion,
                cliente.Fecha_nacimiento
            });

            return result > 0; // Returns true if one or more rows were affected
        }

        public async Task<bool> Update(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"UPDATE clientes SET 
                        Nombre = @Nombre,
                        Apellido = @Apellido,
                        Dni = @Dni,
                        Email = @Email,
                        Telefono = @Telefono,
                        Direccion = @Direccion,
                        Fecha_nacimiento = @Fecha_nacimiento
                        WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql, new
            {
                cliente.Nombre,
                cliente.Apellido,
                cliente.Dni,
                cliente.Email,
                cliente.Telefono,
                cliente.Direccion,
                cliente.Fecha_nacimiento,
                cliente.Id
            });

            return result > 0; // Returns true if one or more rows were affected
        }

        public async Task<bool> Delete(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM clientes WHERE Id = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = cliente.Id });

            return result > 0; // Returns true if one or more rows were affected
        }
    }
}
