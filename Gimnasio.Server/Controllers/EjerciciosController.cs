using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace CRUD_PracticaProf.Controllers
{
    [Route("ejercicios")]
    [ApiController]
    public class EjerciciosController : ControllerBase
    {
        private readonly IEjerciciosRepositorio _ejercicioRepositorio;

        public EjerciciosController(IEjerciciosRepositorio ejercicioRepositorio)
        {

            _ejercicioRepositorio = ejercicioRepositorio;
        }

        /// <summary>
        /// Obtiene todos los ejercicios registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los ejercicios.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de ejercicios.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejercicioRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un ejercicio por su identificador.
        /// </summary>
        /// <param name="id">Id del ejercicio a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el ejercicio encontrado,  
        /// o HTTP 404 si no existe un ejercicio con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ejercicio = await _ejercicioRepositorio.GetById(id);

            if (ejercicio == null)
            {
                return NotFound(new { mensaje = $"No existen ejercicios con ID {id}" });
            }

            return Ok(ejercicio);
        }

        /// <summary>
        /// Registra un nuevo ejercicio en el sistema.
        /// </summary>
        /// <param name="ejercicio">Objeto ejercicio con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el ejercicio creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null)
            {
                return BadRequest(new { mensaje = $"El ejercicio no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _ejercicioRepositorio.Create(ejercicio);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de un ejercicio existente.
        /// </summary>
        /// <param name="id">Id del ejercicio a actualizar (en la URL).</param>
        /// <param name="ejercicio">Objeto ejercicio con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el ejercicio no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null)
            {
                return BadRequest(new { mensaje = $"El ejercicio no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (ejercicio.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id del ejercicio es obligatorio." });
            }

            if (ejercicio.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _ejercicioRepositorio.Update(ejercicio);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Ejercicio no encontrado." });
            }

            return Ok(new { mensaje = "Ejercicio actualizado con exito" });
        }

        /// <summary>
        /// Elimina un ejercicio por su identificador.
        /// </summary>
        /// <param name="id">Id del ejercicio a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el ejercicio no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _ejercicioRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Ejercicio no encontrado." });
            }

            return Ok(new { mensaje = $"Ejercicio eliminado con éxito" });
        }

    }
}
