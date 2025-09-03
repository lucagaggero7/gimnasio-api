using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepositorio _clienteRepositorio;

        public ClientesController(IClientesRepositorio clienteRepositorio)
        {

            _clienteRepositorio = clienteRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clienteRepositorio.GetAll());

        }

        [HttpGet("mostrar")]
        public async Task<IActionResult> GetAllDTO()
        {
            var clientes = await _clienteRepositorio.GetAllDTO();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //return Ok(await _clienteRepositorio.GetById(id));

            var cliente = await _clienteRepositorio.GetById(id);

            if (cliente == null)
            {
                return NotFound($"No existen clientes con ID {id}");
            }

            return Ok(cliente);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El cliente no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _clienteRepositorio.Create(cliente);

            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("El cliente no puede ser nulo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cliente.Id == 0)
                return BadRequest("El Id del cliente es obligatorio.");

            if (cliente.Id != id)
                return BadRequest("El Id del body debe coincidir con el Id de la URL.");

            var filasAfectadas = await _clienteRepositorio.Update(cliente);

            if (filasAfectadas == false)
                return NotFound("Cliente no encontrado.");

            return Ok(new { mensaje = "Cliente actualizado con éxito" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filasAfectadas = await _clienteRepositorio.Delete(new Cliente { Id = id });

            if (filasAfectadas == false)
                return NotFound("Cliente no encontrado.");

            return Ok(new { mensaje = "Cliente eliminado con éxito" });
        }

    }
}
