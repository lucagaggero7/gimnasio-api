using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Controllers
{
    [Route("Formas_pago")]
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
        public async Task<IActionResult> Create([FromBody] FormasPago formaPago)
        {
            if (formaPago == null) return BadRequest("La forma de pago no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _formasPagoRepositorio.Create(formaPago);
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FormasPago formaPago)
        {
            if (formaPago == null) return BadRequest("La forma de pago no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _formasPagoRepositorio.GetById(formaPago.idFormas_pago);
            if (existing == null) return NotFound($"No se encontró la forma de pago con ID {formaPago.idFormas_pago}.");

            await _formasPagoRepositorio.Update(formaPago);
            return Ok(new { mensaje = "Forma de pago actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _formasPagoRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró la forma de pago con ID {id}.");

            await _formasPagoRepositorio.Delete(id);
            return Ok(new { mensaje = "Forma de pago eliminada con éxito" });
        }
    }
}