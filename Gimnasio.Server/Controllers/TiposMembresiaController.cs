using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Servicios.Validaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Route("tipos-membresia")]
    [ApiController]
    public class TiposMembresiaController : ControllerBase
    {
        private readonly ITiposMembresiaRepositorio _tiposMembresiaRepositorio;
        private readonly IOutputCacheStore outputCacheStore;

        private const string cacheKey = "TiposMembresia";

        public TiposMembresiaController(ITiposMembresiaRepositorio tiposMembresiaRepositorio, IOutputCacheStore outputCacheStore)
        {
            _tiposMembresiaRepositorio = tiposMembresiaRepositorio;
            this.outputCacheStore = outputCacheStore;
        }

        /// <summary>
        /// Obtiene todos los tipos de membresias registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los tipos de membresias.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de los tipos de membresias.
        /// </returns>
        [HttpGet]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tiposMembresiaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un tipo de membresia por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el tipo de membresia encontrado,  
        /// o HTTP 404 si no existe el tipo de membresia con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoMembresia = await _tiposMembresiaRepositorio.GetById(id);

            if (tipoMembresia == null)
            {
                return NotFound(new { mensaje = $"No existe un tipo de membresía con ID {id}" });
            }

            return Ok(tipoMembresia);
        }

        /// <summary>
        /// Registra un nuevo tipo de membresia en el sistema.
        /// </summary>
        /// <param name="tipoMembresia">Objeto tipo de membresia con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el tipo de membresia creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoMembresia tipoMembresia)
        {
            var validation = ApiValidaciones.ValidarEntidad(tipoMembresia, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var created = await _tiposMembresiaRepositorio.Create(tipoMembresia);

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de un tipo de membresia existente.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a actualizar (en la URL).</param>
        /// <param name="tipoMembresia">Objeto tipo de membresia con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el tipo de membresia no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoMembresia tipoMembresia)
        {
            var validation = ApiValidaciones.ValidarEntidadConId(id, tipoMembresia, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var filasAfectadas = await _tiposMembresiaRepositorio.Update(tipoMembresia);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Tipo de membresia no encontrado." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = $"Tipo de membresia actualizado con éxito" });
        }

        /// <summary>
        /// Elimina un tipo de membresia por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el tipo de membresia no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _tiposMembresiaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Tipo de membresia no encontrada." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = "Tipo de membresía eliminado con éxito" });
        }
    }
}