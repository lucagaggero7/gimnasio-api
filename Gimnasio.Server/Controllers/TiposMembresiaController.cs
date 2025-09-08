using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
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

        /// <summary>
        /// Obtiene todos los tipos de membresias registrados en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de los tipos de membresias.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de los tipos de membresias.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tiposMembresiaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca un tipo de membresia por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con el tipo de membresia encontrado,  
        /// o HTTP 404 si no existe el tipo de membresia con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoMembresia = await _tiposMembresiaRepositorio.GetById(id);

            if (tipoMembresia == null)
            {
                return NotFound(new { mensaje = $"No existe un tipo de membresía con ID {id}" });
            }

            return Ok(tipoMembresia);
        }

        /// <summary>
        /// Registra un nuevo tipo de membresia en el sistema.
        /// </summary>
        /// <param name="tipoMembresia">Objeto tipo de membresia con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con el tipo de membresia creado,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoMembresia tipoMembresia)
        {
            if (tipoMembresia == null)
            {
                return BadRequest(new { mensaje = $"El tipo de membresía no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _tiposMembresiaRepositorio.Create(tipoMembresia);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de un tipo de membresia existente.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a actualizar (en la URL).</param>
        /// <param name="tipoMembresia">Objeto tipo de membresia con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si el tipo de membresia no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoMembresia tipoMembresia)
        {
            if (tipoMembresia == null)
            {
                return BadRequest(new { mensaje = $"El tipo de membresia no puede ser nulo." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (tipoMembresia.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id del tipo de membresia es obligatorio." });
            }

            if (tipoMembresia.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _tiposMembresiaRepositorio.Update(tipoMembresia);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Tipo de membresia no encontrado." });
            }

            return Ok(new { mensaje = $"Tipo de membresia actualizado con éxito" });
        }

        /// <summary>
        /// Elimina un tipo de membresia por su identificador.
        /// </summary>
        /// <param name="id">Id del tipo de membresia a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si el tipo de membresia no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _tiposMembresiaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Tipo de membresia no encontrada." });
            }

            return Ok(new { mensaje = "Tipo de membresía eliminado con éxito" });
        }
    }
}