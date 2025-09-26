using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Servicios.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Route("membresias")]
    [ApiController]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiasRepositorio _membresiaRepositorio;
        private readonly IOutputCacheStore outputCacheStore;

        private const string cacheKey = "Membresias";

        public MembresiasController(IMembresiasRepositorio membresiaRepositorio, IOutputCacheStore outputCacheStore)
        {
            _membresiaRepositorio = membresiaRepositorio;
            this.outputCacheStore = outputCacheStore;
        }

        /// <summary>
        /// Obtiene todas las membresias registradas en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de las membresias.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de membresias.
        /// </returns>
        [HttpGet]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _membresiaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca una membresia por su identificador.
        /// </summary>
        /// <param name="id">Id de la membresia a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la membresia encontrada,  
        /// o HTTP 404 si no existe la membresia con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetById(int id)
        {
            var membresia = await _membresiaRepositorio.GetById(id);

            if (membresia == null)
            {
                return NotFound(new { mensaje = $"No existe una membresía con ID {id}" });
            }

            return Ok(membresia);
        }

        /// <summary>
        /// Registra una nueva membresia en el sistema.
        /// </summary>
        /// <param name="membresia">Objeto membresia con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la membresia creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Membresia membresia)
        {
            var validation = ApiValidaciones.ValidarEntidad(membresia, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var created = await _membresiaRepositorio.Create(membresia);

            await outputCacheStore.EvictByTagAsync(cacheKey, default);
            await outputCacheStore.EvictByTagAsync("Clientes", default);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una membresia existente.
        /// </summary>
        /// <param name="id">Id de la membresia a actualizar (en la URL).</param>
        /// <param name="membresia">Objeto membresia con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la membresia no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Membresia membresia)
        {
            var validation = ApiValidaciones.ValidarEntidadConId(id, membresia, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var filasAfectadas = await _membresiaRepositorio.Update(membresia);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Membresia no encontrada." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = $"Membresia actualizada con éxito" });
        }

        /// <summary>
        /// Elimina una membresia por su identificador.
        /// </summary>
        /// <param name="id">Id de la membresia a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si la membresia no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _membresiaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Membresía no encontrada." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = $"Membresía eliminada con éxito" });
        }
    }
}