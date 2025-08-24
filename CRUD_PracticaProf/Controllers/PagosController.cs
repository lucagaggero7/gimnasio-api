using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace CRUD_PracticaProf.Controllers
{
    [Route("pagos")] 
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagosRepositorio _pagosRepositorio;

        public PagosController(IPagosRepositorio pagosRepositorio)
        {
            _pagosRepositorio = pagosRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pagosRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _pagosRepositorio.GetById(id);

            if (pago == null)
            {
                return NotFound($"No existen pagos con ID {id}");
            }

            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _pagosRepositorio.Create(pago);

            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pago.Id == 0)
                return BadRequest("El Id del pago es obligatorio.");

            if (pago.Id != id)
                return BadRequest("El Id del body debe coincidir con el Id de la URL.");

            var existingPago = await _pagosRepositorio.GetById(pago.Id);
            if (existingPago == null)
            {
                return NotFound($"No se encontró el pago con ID {pago.Id} para actualizar.");
            }

            var filasAfectadas = await _pagosRepositorio.Update(pago);

            if (filasAfectadas == false)
                return NotFound("Pago no encontrado.");

            return Ok(new { mensaje = "Pago actualizado con exito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var existingPago = await _pagosRepositorio.GetById(id);
            if (existingPago == null)
            {
                return NotFound($"No se encontró el pago con ID {id} para eliminar.");
            }

            var filasAfectadas = await _pagosRepositorio.Delete(id);

            if (filasAfectadas == false)
                return NotFound("Pago no encontrado.");

            return Ok(new { mensaje = "Pago eliminado con éxito" });
        }
    }
}