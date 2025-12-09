using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Servicios.Validaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Gimnasio.Server.Controllers
{
    [Authorize]
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepositorio _clienteRepositorio;
        private readonly IMembresiasRepositorio _membresiaRepositorio;
        private readonly IOutputCacheStore outputCacheStore;
        private readonly ILogger<ClientesController> _logger;

        private const string cacheKey = "Clientes";
        private const string cacheKey2 = "Membresias";

        public ClientesController(ILogger<ClientesController> logger, IClientesRepositorio clienteRepositorio, IMembresiasRepositorio membresiaRepositorio, IOutputCacheStore outputCacheStore)
        {

            _clienteRepositorio = clienteRepositorio;
            _membresiaRepositorio = membresiaRepositorio;
            this.outputCacheStore = outputCacheStore;
            _logger = logger;
        }

        // ENDPOINT DE PRUEBA 
        [AllowAnonymous]
        [HttpGet("test-cache")]
        [OutputCache(PolicyName = "Authenticated")]
        public IActionResult TestCache()
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _logger.LogInformation($"TestCache ejecutado a las: {timestamp}");
            return Ok(new { mensaje = "Cache test", timestamp });
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
        [OutputCache(PolicyName = "Authenticated", Tags = [cacheKey])]
        public async Task<IActionResult> GetAll()
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            _logger.LogInformation($"GetAll ejecutado a las: {timestamp}");

            var result = await _clienteRepositorio.GetAll();

            // Agregar header personalizado para debugging
            Response.Headers.Append("X-Timestamp", timestamp);

            return Ok(new { timestamp, data = result });
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
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetFkDTO()
        {
            var clientes = await _clienteRepositorio.GetFkDTO();
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
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _clienteRepositorio.GetById(id);

            if (cliente is null)
            {
                return NotFound(new { mensaje = $"No existen clientes con ID {id}" });
            }

            return Ok(cliente);
        }

        /// <summary>
        /// Busca una lista de membresias por su cliente.
        /// </summary>
        /// <param name="id">Id del cliente a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de membresias encontradas,  
        /// o HTTP 404 si no existen membresias que pertenezcan a ese cliente.
        /// </returns>
        [HttpGet("{id}/membresias")]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey2])]
        public async Task<IActionResult> GetByCliente(int id)
        {
            var membresias = await _membresiaRepositorio.GetByClienteId(id);

            if (!membresias.Any())
                return Ok(new { mensaje = "El cliente no tiene membresías." });

            return Ok(membresias);
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
            var validation = ApiValidaciones.ValidarEntidad(cliente, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var created = await _clienteRepositorio.Create(cliente);

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

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
            var validation = ApiValidaciones.ValidarEntidadConId(id, cliente, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var filasAfectadas = await _clienteRepositorio.Update(cliente);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Cliente no encontrado." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

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
            // var validation = ApiValidations.ValidateId(id);
            // if (validation != null) return validation;

            var filasAfectadas = await _clienteRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Cliente no encontrado." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = "Cliente eliminado con éxito" });
        }

    }
}
