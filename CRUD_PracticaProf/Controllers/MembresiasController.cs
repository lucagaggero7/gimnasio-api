using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("membresias")]
    [ApiController]
    public class MembresiasController : ControllerBase
    {
        private readonly IMembresiasRepositorio _membresiaRepositorio;

        public MembresiasController(IMembresiasRepositorio membresiaRepositorio)
        {
            _membresiaRepositorio = membresiaRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _membresiaRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var membresia = await _membresiaRepositorio.GetById(id);
            if (membresia == null)
            {
                return NotFound($"No existe una membresía con ID {id}");
            }
            return Ok(membresia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Membresia membresia)
        {
            if (membresia == null) return BadRequest("La membresía no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _membresiaRepositorio.Create(membresia);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Membresia membresia)
        {
            if (membresia == null) return BadRequest("La membresía no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _membresiaRepositorio.GetById(membresia.Id);
            if (existing == null) return NotFound($"No se encontró la membresía con ID {membresia.Id} para actualizar.");

            await _membresiaRepositorio.Update(membresia);
            return Ok(new { mensaje = "Membresía actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _membresiaRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró la membresía con ID {id} para eliminar.");

            await _membresiaRepositorio.Delete(id);
            return Ok(new { mensaje = "Membresía eliminada con éxito" });
        }
    }
}