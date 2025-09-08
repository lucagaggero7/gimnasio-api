using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Route("tipos-rutina")]
    [ApiController]
    public class TiposRutinaController : ControllerBase
    {
        private readonly ITiposRutinaRepositorio _tiposRutinaRepositorio;

        public TiposRutinaController(ITiposRutinaRepositorio tiposRutinaRepositorio)
        {
            _tiposRutinaRepositorio = tiposRutinaRepositorio;
        }

        /// <summary>
        /// Obtiene todos los tipos de rutinas registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los tipos de rutinas.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de los tipos de rutinas.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tiposRutinaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un tipo de rutina por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de rutina a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el tipo de rutina encontrado,  
        /// o HTTP 404 si no existe el tipo de rutina con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoRutina = await _tiposRutinaRepositorio.GetById(id);

            if (tipoRutina == null)
            {
                return NotFound(new { mensaje = $"No existe un tipo de rutina con ID {id}" });
            }

            return Ok(tipoRutina);
        }

        /// <summary>
        /// Registra un nuevo tipo de rutina en el sistema.
        /// </summary>
        /// <param name="tipoRutina">Objeto tipo de rutina con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el tipo de rutina creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoRutina tipoRutina)
        {
            if (tipoRutina == null)
            {
                return BadRequest(new { mensaje = $"El tipo de rutina no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _tiposRutinaRepositorio.Create(tipoRutina);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de un tipo de rutina existente.
        /// </summary>
        /// <param name="id">Id del tipo de rutina a actualizar (en la URL).</param>
        /// <param name="tipoRutina">Objeto tipo de rutina con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el tipo de rutina no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoRutina tipoRutina)
        {
            if (tipoRutina == null)
            {
                return BadRequest(new { mensaje = $"El tipo de rutina no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (tipoRutina.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id del tipo de rutina es obligatorio." });
            }

            if (tipoRutina.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _tiposRutinaRepositorio.Update(tipoRutina);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Tipo de rutina no encontrado." });
            }

            return Ok(new { mensaje = $"Tipo de rutina actualizado con éxito" });
        }

        /// <summary>
        /// Elimina un tipo de rutina por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de rutina a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el tipo de rutina no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _tiposRutinaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Cliente no encontrado." });
            }

            return Ok(new { mensaje = $"Tipo de rutina eliminado con éxito" });
        }
    }
}