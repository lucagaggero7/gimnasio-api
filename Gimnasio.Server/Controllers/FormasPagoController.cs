using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Controllers
{
    [Route("formas-pago")]
    [ApiController]
    public class FormasPagoController : ControllerBase
    {
        private readonly IFormasPagoRepositorio _formasPagoRepositorio;

        public FormasPagoController(IFormasPagoRepositorio formasPagoRepositorio)
        {
            _formasPagoRepositorio = formasPagoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _formasPagoRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var formaPago = await _formasPagoRepositorio.GetById(id);
            if (formaPago == null)
            {
                return NotFound($"No existe una forma de pago con ID {id}");
            }
            return Ok(formaPago);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FormaPago formaPago)
        {
            if (formaPago == null) return BadRequest("La forma de pago no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _formasPagoRepositorio.Create(formaPago);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FormaPago formaPago)
        {
            if (formaPago == null) return BadRequest("La forma de pago no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _formasPagoRepositorio.GetById(formaPago.Id);
            if (existing == null) return NotFound($"No se encontró la forma de pago con ID {formaPago.Id}.");

            if (formaPago.Id == 0)
                return BadRequest("El Id de la forma de pago es obligatorio.");

            if (formaPago.Id != id)
                return BadRequest("El Id del body debe coincidir con el Id de la URL.");

            var filasAfectadas = await _formasPagoRepositorio.Update(formaPago);

            if (filasAfectadas == false)
                return NotFound("Forma de pago no encontrada.");

            return Ok(new { mensaje = "Forma de pago actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _formasPagoRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró la forma de pago con ID {id}.");

            var filasAfectadas = await _formasPagoRepositorio.Delete(id);

            if (filasAfectadas == false)
                return NotFound("Forma de pago no encontrada.");

            return Ok(new { mensaje = "Forma de pago eliminada con éxito" });
        }
    }
}