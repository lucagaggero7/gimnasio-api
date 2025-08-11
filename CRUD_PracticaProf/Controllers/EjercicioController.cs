using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("Ejercicios")]
    [ApiController]
    public class EjerciciosController : ControllerBase
    {
        private readonly IEjercicioRepositorio _ejerciciosRepositorio;

        public EjerciciosController(IEjercicioRepositorio ejerciciosRepositorio)
        {
            _ejerciciosRepositorio = ejerciciosRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejerciciosRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ejercicio = await _ejerciciosRepositorio.GetById(id);
            if (ejercicio == null)
            {
                return NotFound($"No existe un ejercicio con ID {id}");
            }
            return Ok(ejercicio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null) return BadRequest("El ejercicio no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _ejerciciosRepositorio.Create(ejercicio);
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null) return BadRequest("El ejercicio no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _ejerciciosRepositorio.GetById(ejercicio.idEjercicio);
            if (existing == null) return NotFound($"No se encontró el ejercicio con ID {ejercicio.idEjercicio} para actualizar.");

            await _ejerciciosRepositorio.Update(ejercicio);
            return Ok(new { mensaje = "Ejercicio actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _ejerciciosRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró el ejercicio con ID {id} para eliminar.");

            await _ejerciciosRepositorio.Delete(id);
            return Ok(new { mensaje = "Ejercicio eliminado con éxito" });
        }
    }
}