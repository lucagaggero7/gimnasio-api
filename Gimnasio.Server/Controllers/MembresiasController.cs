using Gimnasio.Server.Datos.Repositorio;
using Gimnasio.Server.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;

namespace Gimnasio.Server.Controllers
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

        /// <summary>
        /// Obtiene todas las membresias registradas en el sistema.
        /// </summary>
        /// <remarks>
        /// Devuelve una lista completa con los datos de las membresias.
        /// </remarks>
        /// <returns>
        /// Respuesta HTTP 200 con la lista de membresias.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _membresiaRepositorio.GetAll());
        }

        /// <summary>
        /// Busca una membresia por su identificador.
        /// </summary>
        /// <param name="id">Id de la membresia a buscar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con la membresia encontrada,  
        /// o HTTP 404 si no existe la membresia con ese Id.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var membresia = await _membresiaRepositorio.GetById(id);

            if (membresia == null)
            {
                return NotFound(new { mensaje = $"No existe una membresía con ID {id}" });
            }

            return Ok(membresia);
        }

        /// <summary>
        /// Registra una nueva membresia en el sistema.
        /// </summary>
        /// <param name="membresia">Objeto membresia con la información a crear.</param>
        /// <returns>
        /// Respuesta HTTP 201 con la membresia creada,  
        /// o HTTP 400 si el modelo no es válido.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Membresia membresia)
        {
            if (membresia == null)
            {
                return BadRequest(new { mensaje = $"La membresía no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            var created = await _membresiaRepositorio.Create(membresia);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Actualiza los datos de una membresia existente.
        /// </summary>
        /// <param name="id">Id de la membresia a actualizar (en la URL).</param>
        /// <param name="membresia">Objeto membresia con los nuevos datos.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// HTTP 400 si hay inconsistencias en los datos,  
        /// o HTTP 404 si la membresia no existe.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Membresia membresia)
        {
            if (membresia == null)
            {
                return BadRequest(new { mensaje = $"La membresia no puede ser nula." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { mensaje = $"Faltan datos obligatorios" });
            }

            if (membresia.Id == 0)
            {
                return BadRequest(new { mensaje = $"El Id de la membresia es obligatorio." });
            }

            if (membresia.Id != id)
            {
                return BadRequest(new { mensaje = $"El Id del body debe coincidir con el Id de la URL." });
            }

            var filasAfectadas = await _membresiaRepositorio.Update(membresia);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Membresia no encontrada." });
            }

            return Ok(new { mensaje = $"Membresia actualizada con éxito" });
        }

        /// <summary>
        /// Elimina una membresia por su identificador.
        /// </summary>
        /// <param name="id">Id de la membresia a eliminar.</param>
        /// <returns>
        /// Respuesta HTTP 200 con un mensaje de éxito,  
        /// o HTTP 404 si la membresia no existe.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _membresiaRepositorio.Delete(id);

            if (filasAfectadas == false)
            {
                return NotFound(new { mensaje = $"Membresía no encontrada." });
            }

            return Ok(new { mensaje = $"Membresía eliminada con éxito" });
        }
    }
}