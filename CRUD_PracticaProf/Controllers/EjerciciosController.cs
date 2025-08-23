using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("ejercicios")]
    [ApiController]
    public class EjerciciosController : ControllerBase
    {
        ////////////////////
        ///

        private readonly IEjerciciosRepositorio _ejercicioRepositorio;

        public EjerciciosController(IEjerciciosRepositorio ejercicioRepositorio)
        {

            _ejercicioRepositorio = ejercicioRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejercicioRepositorio.GetAll());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            //return Ok(await _clienteRepositorio.GetById(id));

            var ejercicio = await _ejercicioRepositorio.GetById(id);

            if (ejercicio == null)
            {
                return NotFound($"No existen ejercicios con ID {id}");
            }

            return Ok(ejercicio);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null)
            {
                return BadRequest("El ejercicio no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _ejercicioRepositorio.Create(ejercicio);

            return Created("created", created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null)
            {
                return BadRequest("El ejercicio no puede ser nulo.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ejercicioRepositorio.Update(ejercicio);

            return Ok(new { mensaje = "Ejercicio actualizado con exito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ejercicioRepositorio.Delete(new Ejercicio { Id = id });
            return Ok(new { mensaje = "Ejercicio eliminado con éxito" });
        }

    }
}
