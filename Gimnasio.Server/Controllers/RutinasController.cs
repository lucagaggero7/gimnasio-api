using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos.DTO;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Servicios.Validaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Authorize]
    [Route("rutinas")]
    [ApiController]
    public class RutinasController : ControllerBase
    {
        private readonly IRutinasRepositorio _rutinasRepositorio;
        private readonly IOutputCacheStore outputCacheStore;

        private const string cacheKey = "Rutinas";

        public RutinasController(IRutinasRepositorio rutinasRepositorio, IOutputCacheStore outputCacheStore)
        {
            _rutinasRepositorio = rutinasRepositorio;
            this.outputCacheStore = outputCacheStore;
        }

        /// <summary>
        /// Obtiene todas las rutinas registradas en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de las rutinas.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de rutinas.
        /// </returns>
        [HttpGet]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rutinasRepositorio.GetAll());
        }

        /// <summary>
        /// Busca una rutina por su identificador.
        /// </summary>
        /// <param name="id">Id de la rutina a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la rutina encontrada,  
        /// o HTTP 404 si no existe la rutina con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        [OutputCache(PolicyName = "Default", Tags = [cacheKey])]
        public async Task<IActionResult> GetById(int id)
        {
            var rutina = await _rutinasRepositorio.GetById(id);

            if (rutina == null)
            {
                return NotFound(new { mensaje = $"No existe una rutina con ID {id}" });
            }

            return Ok(rutina);
        }

        /// <summary>
        /// Registra una nueva rutina en el sistema.
        /// </summary>
        /// <param name="rutinaCrearDto">Objeto rutina con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la rutina creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RutinaCrearDto rutinaCrearDto)
        {
            // Mapear dto -> entidad Rutina
            var rutina = new Rutina
            {
                Nombre = rutinaCrearDto.Nombre,
                FechaInicio = rutinaCrearDto.FechaInicio,
                Duracion = rutinaCrearDto.Duracion,
                Objetivo = rutinaCrearDto.Objetivo,
                FrecuenciaSem = rutinaCrearDto.FrecuenciaSem,
                FkIdTipoRutina = rutinaCrearDto.FkIdTipoRutina,
                FkIdCliente = rutinaCrearDto.FkIdCliente
            };

            // Crear rutina + ejercicios
            var created = await _rutinasRepositorio.Create(rutina, rutinaCrearDto.Ejercicios);

            await outputCacheStore.EvictByTagAsync(cacheKey, default);
            await outputCacheStore.EvictByTagAsync("RutinaEjercicio", default);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una rutina existente.
        /// </summary>
        /// <param name="id">Id de la rutina a actualizar (en la URL).</param>
        /// <param name="rutinaEditarDTO">Objeto rutina con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la rutina no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RutinaEditarDTO rutinaEditarDTO)
        {
            // Mapear DTO → entidad
            var rutina = new Rutina
            {
                Id = id,
                Nombre = rutinaEditarDTO.Nombre,
                FechaInicio = rutinaEditarDTO.FechaInicio,
                Duracion = rutinaEditarDTO.Duracion,
                Objetivo = rutinaEditarDTO.Objetivo,
                FrecuenciaSem = rutinaEditarDTO.FrecuenciaSem,
                FkIdTipoRutina = rutinaEditarDTO.FkIdTipoRutina,
                FkIdCliente = rutinaEditarDTO.FkIdCliente
            };

            var filasAfectadas = await _rutinasRepositorio.Update(rutina, rutinaEditarDTO.Ejercicios);

            if (!filasAfectadas)
                return NotFound(new { mensaje = "Rutina no encontrada." });

            await outputCacheStore.EvictByTagAsync(cacheKey, default);
            await outputCacheStore.EvictByTagAsync("RutinaEjercicio", default);

            return Ok(new { mensaje = "Rutina actualizada con éxito" });
        }

        /// <summary>
        /// Elimina una rutina por su identificador.
        /// </summary>
        /// <param name="id">Id de la rutina a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si la rutina no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _rutinasRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Rutina no encontrada." });
            }

            await outputCacheStore.EvictByTagAsync(cacheKey, default);

            return Ok(new { mensaje = "Rutina eliminada con éxito" });
        }
    }
}