using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace CRUD_PracticaProf.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejerciciosPorRutinaRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ejercicioPorRutina = await _ejerciciosPorRutinaRepositorio.GetById(id);
            if (ejercicioPorRutina == null)
            {
                return NotFound($"No existe un registro de ejercicio por rutina con ID {id}");
            }
            return Ok(ejercicioPorRutina);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EjercicioPorRutina ejercicioPorRutina)
        {
            if (ejercicioPorRutina == null) return BadRequest("El objeto de ejercicio por rutina no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _ejerciciosPorRutinaRepositorio.Create(ejercicioPorRutina);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EjercicioPorRutina ejercicioPorRutina)
        {
            if (ejercicioPorRutina == null) return BadRequest("El objeto de ejercicio por rutina no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _ejerciciosPorRutinaRepositorio.GetById(ejercicioPorRutina.Id);
            if (existing == null)
            {
                return NotFound($"No se encontró el registro con ID {ejercicioPorRutina.Id} para actualizar.");
            }

            if (ejercicioPorRutina.Id == 0)
                return BadRequest("El Id del ejercicio por rutina es obligatorio.");

            if (ejercicioPorRutina.Id != id)
                return BadRequest("El Id del body debe coincidir con el Id de la URL.");

            var filasAfectadas = await _ejerciciosPorRutinaRepositorio.Update(ejercicioPorRutina);

            if (filasAfectadas == false)
                return NotFound("Ejercicio por rutina no encontrado.");

            return Ok(new { mensaje = "Registro de ejercicio por rutina actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _ejerciciosPorRutinaRepositorio.GetById(id);
            if (existing == null)
            {
                return NotFound($"No se encontró el registro con ID {id} para eliminar.");
            }

            var filasAfectadas = await _ejerciciosPorRutinaRepositorio.Delete(id);

            if (filasAfectadas == false)
                return NotFound("Ejercicio por rutina no encontrado.");

            return Ok(new { mensaje = "Registro de ejercicio por rutina eliminado con éxito" });
        }
    }
}