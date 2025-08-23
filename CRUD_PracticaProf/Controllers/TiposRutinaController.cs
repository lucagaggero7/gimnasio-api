using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tiposRutinaRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoRutina = await _tiposRutinaRepositorio.GetById(id);
            if (tipoRutina == null)
            {
                return NotFound($"No existe un tipo de rutina con ID {id}");
            }
            return Ok(tipoRutina);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoRutina tipoRutina)
        {
            if (tipoRutina == null) return BadRequest("El tipo de rutina no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _tiposRutinaRepositorio.Create(tipoRutina);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TipoRutina tipoRutina)
        {
            if (tipoRutina == null) return BadRequest("El tipo de rutina no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _tiposRutinaRepositorio.GetById(tipoRutina.Id);
            if (existing == null)
            {
                return NotFound($"No se encontró el tipo de rutina con ID {tipoRutina.Id} para actualizar.");
            }

            await _tiposRutinaRepositorio.Update(tipoRutina);
            return Ok(new { mensaje = "Tipo de rutina actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _tiposRutinaRepositorio.GetById(id);
            if (existing == null)
            {
                return NotFound($"No se encontró el tipo de rutina con ID {id} para eliminar.");
            }

            await _tiposRutinaRepositorio.Delete(id);
            return Ok(new { mensaje = "Tipo de rutina eliminado con éxito" });
        }
    }
}