using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace CRUD_PracticaProf.Controllers
{
    [Route("evaluaciones")]
    [ApiController]
    public class EvaluacionesController : ControllerBase
    {
        private readonly IEvaluacionesRepositorio _evaluacionesRepositorio;

        public EvaluacionesController(IEvaluacionesRepositorio evaluacionesRepositorio)
        {
            _evaluacionesRepositorio = evaluacionesRepositorio;
        }

        /// <summary>
        /// Obtiene todas las evaluaciones registradas en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de las evaluaciones.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de evaluaciones.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _evaluacionesRepositorio.GetAll());
        }

        /// <summary>
        /// Busca una evaluacion por su identificador.
        /// </summary>
        /// <param name="id">Id de la evaluacion a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la evaluacion encontrada,  
        /// o HTTP 404 si no existe la evaluacion con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evaluacion = await _evaluacionesRepositorio.GetById(id);

            if (evaluacion == null)
            {
                return NotFound(new { mensaje = $"No existe una evaluacion con ID {id}" });
            }

            return Ok(evaluacion);
        }

        /// <summary>
        /// Registra una nueva evaluacion en el sistema.
        /// </summary>
        /// <param name="evaluacion">Objeto evaluacion con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la evaluacion creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Evaluacion evaluacion)
        {
            if (evaluacion == null)
            {
                return BadRequest(new { mensaje = $"La evaluacion no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _evaluacionesRepositorio.Create(evaluacion);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una evaluacion existente.
        /// </summary>
        /// <param name="id">Id de la evaluacion a actualizar (en la URL).</param>
        /// <param name="evaluacion">Objeto evaluacion con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la evaluacion no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Evaluacion evaluacion)
        {
            if (evaluacion == null)
            {
                return BadRequest(new { mensaje = $"La evaluacion no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (evaluacion.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id de la evaluacion es obligatorio." });
            }

            if (evaluacion.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _evaluacionesRepositorio.Update(evaluacion);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Evaluacion no encontrada." });
            }

            return Ok(new { mensaje = $"Evaluacion actualizada con éxito" });
        }

        /// <summary>
        /// Elimina una evaluacion por su identificador.
        /// </summary>
        /// <param name="id">Id de la evaluacion a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si la evaluacion no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _evaluacionesRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Evaluacion no encontrada." });
            }

            return Ok(new { mensaje = "Evaluacion eliminada con éxito" });
        }
    }
}

