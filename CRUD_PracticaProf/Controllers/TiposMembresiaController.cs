using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("tipos-membresia")]
    [ApiController]
    public class TiposMembresiaController : ControllerBase
    {
        private readonly ITiposMembresiaRepositorio _tiposMembresiaRepositorio;

        public TiposMembresiaController(ITiposMembresiaRepositorio tiposMembresiaRepositorio)
        {
            _tiposMembresiaRepositorio = tiposMembresiaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tiposMembresiaRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoMembresia = await _tiposMembresiaRepositorio.GetById(id);
            if (tipoMembresia == null)
            {
                return NotFound($"No existe un tipo de membresía con ID {id}");
            }
            return Ok(tipoMembresia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoMembresia tipoMembresia)
        {
            if (tipoMembresia == null) return BadRequest("El tipo de membresía no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _tiposMembresiaRepositorio.Create(tipoMembresia);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TipoMembresia tipoMembresia)
        {
            if (tipoMembresia == null) return BadRequest("El tipo de membresía no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _tiposMembresiaRepositorio.GetById(tipoMembresia.Id);
            if (existing == null) return NotFound($"No se encontró el tipo de membresía con ID {tipoMembresia.Id} para actualizar.");

            await _tiposMembresiaRepositorio.Update(tipoMembresia);
            return Ok(new { mensaje = "Tipo de membresía actualizado con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _tiposMembresiaRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró el tipo de membresía con ID {id} para eliminar.");

            await _tiposMembresiaRepositorio.Delete(id);
            return Ok(new { mensaje = "Tipo de membresía eliminado con éxito" });
        }
    }
}