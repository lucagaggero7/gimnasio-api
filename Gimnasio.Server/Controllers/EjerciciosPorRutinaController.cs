using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Modelos.Entidades;
using Gimnasio.Server.Servicios.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
{
    [Route("ejercicios-por-rutina")]
    [ApiController]
    public class EjerciciosPorRutinaController : ControllerBase
    {
        private readonly IEjerciciosPorRutinaRepositorio _ejerciciosPorRutinaRepositorio;

        public EjerciciosPorRutinaController(IEjerciciosPorRutinaRepositorio ejerciciosPorRutinaRepositorio)
        {
            _ejerciciosPorRutinaRepositorio = ejerciciosPorRutinaRepositorio;
        }

        /// <summary>
        /// Obtiene todos los ejercicios por rutina registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los ejercicios por rutina.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de ejercicios por rutina.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejerciciosPorRutinaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un ejercicio por rutina por su identificador.
        /// </summary>
        /// <param name="id">Id del ejercicio por rutina a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el ejercicio por rutina encontrado,  
        /// o HTTP 404 si no existe un ejercicio por rutina con ese Id.
        /// /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ejercicioPorRutina = await _ejerciciosPorRutinaRepositorio.GetById(id);

            if (ejercicioPorRutina == null)
            {
                return NotFound(new { mensaje = $"No existe un registro de ejercicio por rutina con ID {id}" });
            }

            return Ok(ejercicioPorRutina);
        }

        /// <summary>
        /// Registra un nuevo ejercicio por rutina en el sistema.
        /// </summary>
        /// <param name="ejercicioPorRutina">Objeto ejercicio por rutina con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el ejercicio por rutina creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EjercicioPorRutina ejercicioPorRutina)
        {
            var validation = ApiValidaciones.ValidarEntidad(ejercicioPorRutina, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var created = await _ejerciciosPorRutinaRepositorio.Create(ejercicioPorRutina);

            return Created("created", created);
        }

        /// <summary>
        /// Actualiza los datos de un ejercicio por rutina existente.
        /// </summary>
        /// <param name="id">Id del ejercicio por rutina a actualizar (en la URL).</param>
        /// <param name="ejercicioPorRutina">Objeto ejercicio por rutina con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el ejercicio por rutina no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EjercicioPorRutina ejercicioPorRutina)
        {
            var validation = ApiValidaciones.ValidarEntidadConId(id, ejercicioPorRutina, ModelState);

            if (validation != null)
            {
                return validation;
            }

            var filasAfectadas = await _ejerciciosPorRutinaRepositorio.Update(ejercicioPorRutina);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Ejercicio por rutina no encontrado." });
            }

            return Ok(new { mensaje = "Ejercicio por rutina actualizado con exito" });
        }

        /// <summary>
        /// Elimina un ejercicio por rutina por su identificador.
        /// </summary>
        /// <param name="id">Id del ejercicio por rutina a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el ejercicio por rutina no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _ejerciciosPorRutinaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Ejercicio por rutina no encontrado." });
            }

            return Ok(new { mensaje = $"Registro de ejercicio por rutina eliminado con éxito" });
        }
    }
}