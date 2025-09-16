using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Modelos.DTO;

namespace Gimnasio.Server.Datos.Repositorio
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

            var sql = @"SELECT 
               id,
               nombre,
               apellido,
               dni,
               email,
               telefono,
               direccion,
               fecha_nacimiento AS FechaNacimiento,
               contacto_emergencia AS ContactoEmergencia
            FROM clientes";

            return await  db.QueryAsync<Cliente>(sql, new { });
        }

        public async Task<IEnumerable<ClienteMostrarDTO>> GetAllDTO()
        {
            using var db = dbConnection();
            var sql = "SELECT id, nombre, apellido FROM clientes";

            return await db.QueryAsync<ClienteMostrarDTO>(sql, new { });
        }

        public async Task<Cliente?> GetById(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT 
               id,
               nombre,
               apellido,
               dni,
               email,
               telefono,
               direccion,
               fecha_nacimiento AS FechaNacimiento,
               contacto_emergencia AS ContactoEmergencia
               FROM clientes WHERE id = @id";

            return await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO clientes (nombre, apellido, dni, email, telefono, direccion, fecha_nacimiento, contacto_emergencia)
                        VALUES (@nombre, @apellido, @dni, @email, @telefono, @direccion, @fecha_nacimiento, @contacto_emergencia);
                         SELECT LAST_INSERT_ID(); ";

            var id = await db.ExecuteScalarAsync<int>(sql, new
            {
                nombre = cliente.Nombre,
                apellido = cliente.Apellido,
                dni = cliente.Dni,
                email = cliente.Email,
                telefono = cliente.Telefono,
                direccion = cliente.Direccion,
                fecha_nacimiento = cliente.FechaNacimiento,
                contacto_emergencia = cliente.ContactoEmergencia
            });

            cliente.Id = id;
            return cliente; 
        }

        public async Task<bool> Update(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"UPDATE clientes SET 
                        nombre = @nombre,
                        apellido = @apellido,
                        dni = @dni,
                        email = @email,
                        telefono = @telefono,
                        direccion = @direccion,
                        fecha_nacimiento = @fecha_nacimiento,
                        contacto_emergencia = @contacto_emergencia
                        WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new
            {
                nombre = cliente.Nombre,
                apellido = cliente.Apellido,
                dni = cliente.Dni,
                email = cliente.Email,
                telefono = cliente.Telefono,
                direccion = cliente.Direccion,
                fecha_nacimiento = cliente.FechaNacimiento,
                contacto_emergencia = cliente.ContactoEmergencia,
                id = cliente.Id
            });

            return result > 0; // Returns true if one or more rows were affected
        }

        public async Task<bool> Delete(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM clientes WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0; // Returns true if one or more rows were affected
        }
    }
}
