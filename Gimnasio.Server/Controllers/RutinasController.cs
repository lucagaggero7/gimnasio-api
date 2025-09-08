using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos;
using Gimnasio.Server.Modelos.CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Route("rutinas")]
    [ApiController]
    public class RutinasController : ControllerBase
    {
        private readonly IRutinasRepositorio _rutinasRepositorio;

        public RutinasController(IRutinasRepositorio rutinasRepositorio)
        {
            _rutinasRepositorio = rutinasRepositorio;
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
        /// <param name="rutina">Objeto rutina con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la rutina creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rutina rutina)
        {
            if (rutina == null)
            {
                return BadRequest(new { mensaje = $"La rutina no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _rutinasRepositorio.Create(rutina);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una rutina existente.
        /// </summary>
        /// <param name="id">Id de la rutina a actualizar (en la URL).</param>
        /// <param name="rutina">Objeto rutina con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la rutina no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] Rutina rutina)
        {
            if (rutina == null)
            {
                return BadRequest(new { mensaje = $"La rutina no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (rutina.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id de la rutina es obligatorio." });
            }

            if (rutina.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _rutinasRepositorio.Update(rutina);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Rutina no encontrada." });
            }

            return Ok(new { mensaje = $"Rutina actualizada con éxito" });
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

            return Ok(new { mensaje = "Rutina eliminada con éxito" });
        }
    }
}