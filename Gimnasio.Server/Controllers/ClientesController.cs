using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepositorio _clienteRepositorio;

        public ClientesController(IClientesRepositorio clienteRepositorio)
        {

            _clienteRepositorio = clienteRepositorio;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los clientes.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de clientes.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clienteRepositorio.GetAll());

        }

        /// <summary>
        /// Obtiene todos los clientes en formato DTO.
        /// </summary>
        /// <remarks>
        /// Se devuelve una lista simplificada de clientes, con campos seleccionados.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de clientes DTO.
        /// </returns>
        [HttpGet("mostrar")]
        public async Task<IActionResult> GetAllDTO()
        {
            var clientes = await _clienteRepositorio.GetAllDTO();
            return Ok(clientes);
        }


        /// <summary>
        /// Busca un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el cliente encontrado,  
        /// o HTTP 404 si no existe un cliente con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //return Ok(await _clienteRepositorio.GetById(id));

            var cliente = await _clienteRepositorio.GetById(id);

            if (cliente == null)
            {
                return NotFound(new { mensaje = $"No existen clientes con ID {id}" });
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Registra un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="cliente">Objeto cliente con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el cliente creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest(new { mensaje = $"El cliente no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _clienteRepositorio.Create(cliente);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

        }

        /// <summary>
        /// Actualiza los datos de un cliente existente.
        /// </summary>
        /// <param name="id">Id del cliente a actualizar (en la URL).</param>
        /// <param name="cliente">Objeto cliente con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el cliente no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(new { mensaje = $"El cliente no puede ser nulo." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cliente.Id == 0)
                return BadRequest(new { mensaje = $"El Id del cliente es obligatorio." });

            if (cliente.Id != id)
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL."});

            var filasAfectadas = await _clienteRepositorio.Update(cliente);

            if (filasAfectadas == false)
                return NotFound(new { mensaje = $"Cliente no encontrado."});

            return Ok(new { mensaje = "Cliente actualizado con éxito" });
        }

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="id">Id del cliente a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el cliente no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _clienteRepositorio.Delete(new Cliente { Id = id });

            if (filasAfectadas == false)
                return NotFound(new { mensaje = $"Cliente no encontrado."});

            return Ok(new { mensaje = "Cliente eliminado con éxito" });
        }

    }
}
