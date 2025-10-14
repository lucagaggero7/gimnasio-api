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


        public async Task<IEnumerable<ClienteDTO>> GetAll()
        {
            var db = dbConnection();

            var sql = @"
            SELECT c.id, c.nombre, c.apellido, c.dni, c.email,c.telefono, c.direccion, c.fecha_nacimiento AS FechaNacimiento, c.contacto_emergencia AS ContactoEmergencia, c.fecha AS Fecha,
            CASE 
                WHEN CURDATE() BETWEEN m.fecha_inicio AND m.fecha_vencimiento THEN TRUE
                ELSE FALSE
                END AS Estado
                FROM clientes c
                LEFT JOIN (
                -- Subconsulta que devuelve la última membresía de cada cliente por id
                SELECT m.*
                FROM membresias m
                INNER JOIN (SELECT fk_id_cliente, MAX(id) AS last_membresia_id FROM membresias 
                GROUP BY fk_id_cliente) last_m ON m.id = last_m.last_membresia_id) m ON m.fk_id_cliente = c.id;";

            return await  db.QueryAsync<ClienteDTO>(sql);
        }

        public async Task<IEnumerable<ClienteForaneoDTO>> GetFkDTO()
        {
            using var db = dbConnection();
            var sql = "SELECT id, nombre, apellido FROM clientes";

            return await db.QueryAsync<ClienteForaneoDTO>(sql, new { });
        }

        public async Task<ClienteDTO?> GetById(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT c.id, c.nombre, c.apellido, c.dni, c.email,c.telefono, c.direccion, c.fecha_nacimiento AS FechaNacimiento, c.contacto_emergencia AS ContactoEmergencia, c.fecha AS Fecha,
            CASE 
                WHEN CURDATE() BETWEEN m.fecha_inicio AND m.fecha_vencimiento THEN TRUE
                ELSE FALSE
                END AS Estado
                FROM clientes c
                LEFT JOIN (
                -- Subconsulta que devuelve la última membresía de cada cliente por id
                SELECT m.*
                FROM membresias m
                INNER JOIN (SELECT fk_id_cliente, MAX(id) AS last_membresia_id FROM membresias 
                GROUP BY fk_id_cliente) last_m ON m.id = last_m.last_membresia_id) m ON m.fk_id_cliente = c.id
                WHERE c.id = @id;";

            return await db.QueryFirstOrDefaultAsync<ClienteDTO>(sql, new { id });
        }
            

        public async Task<Cliente> Create(Cliente cliente)
        {
            var db = dbConnection();

            var sql = @"INSERT INTO clientes (nombre, apellido, dni, email, telefono, direccion, fecha_nacimiento, contacto_emergencia, fecha)
                        VALUES (@nombre, @apellido, @dni, @email, @telefono, @direccion, @fecha_nacimiento, @contacto_emergencia, @fecha);
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
                contacto_emergencia = cliente.ContactoEmergencia,
                fecha = cliente.Fecha
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
                contacto_emergencia = cliente.ContactoEmergencia
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
