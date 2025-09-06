using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

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
        public async Task<IActionResult> Update(int id,[FromBody] Rutina rutina)
        {
            if (rutina == null) return BadRequest("La rutina no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (rutina.Id == 0)
                return BadRequest("El Id de la rutina es obligatorio.");

            if (rutina.Id != id)
                return BadRequest("El Id del body debe coincidir con el Id de la URL.");

            var existing = await _rutinasRepositorio.GetById(rutina.Id);
            if (existing == null)
            {
                return NotFound($"No se encontró la rutina con ID {rutina.Id} para actualizar.");
            }

            var filasAfectadas = await _rutinasRepositorio.Update(rutina);

            if (filasAfectadas == false)
                return NotFound("Rutina no encontrada.");

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

            var filasAfectadas = await _rutinasRepositorio.Delete(id);

            if (filasAfectadas == false)
                return NotFound("Rutina no encontrada.");

            return Ok(new { mensaje = "Rutina eliminada con éxito" });
        }
    }
}