using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {

            _clienteRepositorio = clienteRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _clienteRepositorio.GetAll());

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

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("El cliente no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _clienteRepositorio.Update(cliente);

            return Ok("Cliente actualizado con exito");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        { 
            await _clienteRepositorio.Delete(new Cliente { Id = id });

            return Ok("Cliente eliminado con exito");
        }

    }
}
