using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Entidades;
using Gimnasio.Server.Modelos;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using System.Threading.Tasks;

namespace Gimnasio.Server.Controllers
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

        /// <summary>
        /// Obtiene todas las formas de pago registradas en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de las formas de pago.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de formas de pago.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _formasPagoRepositorio.GetAll());
        }

        /// <summary>
        /// Busca una forma de pago por su identificador.
        /// </summary>
        /// <param name="id">Id de la forma de pago a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la forma de pago encontrada,  
        /// o HTTP 404 si no existe la forma de pago con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var formaPago = await _formasPagoRepositorio.GetById(id);

            if (formaPago == null)
            {
                return NotFound(new { mensaje = $"No existe una forma de pago con ID {id}" });
            }

            return Ok(formaPago);
        }

        /// <summary>
        /// Registra una nueva forma de pago en el sistema.
        /// </summary>
        /// <param name="formaPago">Objeto forma de pago con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la forma de pago creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FormaPago formaPago)
        {
            if (formaPago == null)
            {
                return BadRequest(new { mensaje = $"La forma de pago no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _formasPagoRepositorio.Create(formaPago);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una forma de pago existente.
        /// </summary>
        /// <param name="id">Id de la forma de pago a actualizar (en la URL).</param>
        /// <param name="formaPago">Objeto forma de pago con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la forma de pago no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FormaPago formaPago)
        {
            if (formaPago == null)
            {
                return BadRequest(new { mensaje = $"La forma de pago no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (formaPago.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id de la forma de pago es obligatorio." });
            }

            if (formaPago.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _formasPagoRepositorio.Update(formaPago);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Forma de pago no encontrada." });
            }

            return Ok(new { mensaje = $"Forma de pago actualizada con éxito" });
        }

        /// <summary>
        /// Elimina una forma de pago por su identificador.
        /// </summary>
        /// <param name="id">Id de la forma de pago a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si la forma de pago no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _formasPagoRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Forma de pago no encontrada." });
            } 
       
            return Ok(new { mensaje = $"Forma de pago eliminada con éxito" });
        }
    }
}