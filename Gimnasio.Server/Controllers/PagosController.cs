using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Entidades;
using Gimnasio.Server.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
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

        /// <summary>
        /// Obtiene todos los pagos registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los pagos.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de pagos.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pagosRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un pago por su identificador.
        /// </summary>
        /// <param name="id">Id del pago a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el pago encontrado,  
        /// o HTTP 404 si no existe un pago con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _pagosRepositorio.GetById(id);

            if (pago == null)
            {
                return NotFound(new { mensaje = $"No existen pagos con ID {id}" });
            }

            return Ok(pago);
        }

        /// <summary>
        /// Registra un nuevo pago en el sistema.
        /// </summary>
        /// <param name="pago">Objeto pago con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el pago creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest(new { mensaje = $"El pago no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _pagosRepositorio.Create(pago);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de un pago existente.
        /// </summary>
        /// <param name="id">Id del pago a actualizar (en la URL).</param>
        /// <param name="pago">Objeto pago con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el pago no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest(new { mensaje = $"El pago no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (pago.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id del pago es obligatorio." });
            }

            if (pago.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _pagosRepositorio.Update(pago);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Pago no encontrado." });
            }

            return Ok(new { mensaje = "Pago actualizado con exito" });
        }

        /// <summary>
        /// Elimina un pago por su identificador.
        /// </summary>
        /// <param name="id">Id del pago a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el pago no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _pagosRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Pago no encontrado." });
            }

            return Ok(new { mensaje = "Pago eliminado con éxito" });
        }
    }
}