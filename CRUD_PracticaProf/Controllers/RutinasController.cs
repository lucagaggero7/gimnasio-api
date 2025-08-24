using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _rutinasRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rutina = await _rutinasRepositorio.GetById(id);
            if (rutina == null)
            {
                return NotFound($"No existe una rutina con ID {id}");
            }
            return Ok(rutina);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Rutina rutina)
        {
            if (rutina == null) return BadRequest("La rutina no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _rutinasRepositorio.Create(rutina);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Rutina rutina)
        {
            if (rutina == null) return BadRequest("La rutina no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _rutinasRepositorio.GetById(rutina.Id);
            if (existing == null)
            {
                return NotFound($"No se encontró la rutina con ID {rutina.Id} para actualizar.");
            }

            await _rutinasRepositorio.Update(rutina);
            return Ok(new { mensaje = "Rutina actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _rutinasRepositorio.GetById(id);
            if (existing == null)
            {
                return NotFound($"No se encontró la rutina con ID {id} para eliminar.");
            }

            await _rutinasRepositorio.Delete(id);
            return Ok(new { mensaje = "Rutina eliminada con éxito" });
        }
    }
}