using CRUD_PracticaProf.Datos.Repositorio;
<<<<<<< HEAD
using CRUD_PracticaProf.Entidades;
=======
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
using CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("Ejercicios")]
    [ApiController]
<<<<<<< HEAD
    public class EjercicioController : ControllerBase
    {
        ////////////////////
        ///

        private readonly IEjercicioRepositorio _ejercicioRepositorio;

        public EjercicioController(IEjercicioRepositorio ejercicioRepositorio)
        {

            _ejercicioRepositorio = ejercicioRepositorio;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejercicioRepositorio.GetAll());

=======
    public class EjerciciosController : ControllerBase
    {
        private readonly IEjercicioRepositorio _ejerciciosRepositorio;

        public EjerciciosController(IEjercicioRepositorio ejerciciosRepositorio)
        {
            _ejerciciosRepositorio = ejerciciosRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ejerciciosRepositorio.GetAll());
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
<<<<<<< HEAD
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

=======
            var ejercicio = await _ejerciciosRepositorio.GetById(id);
            if (ejercicio == null)
            {
                return NotFound($"No existe un ejercicio con ID {id}");
            }
            return Ok(ejercicio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ejercicio ejercicio)
        {
            if (ejercicio == null) return BadRequest("El ejercicio no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _ejerciciosRepositorio.Create(ejercicio);
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Ejercicio ejercicio)
        {
<<<<<<< HEAD
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

=======
            if (ejercicio == null) return BadRequest("El ejercicio no puede ser nulo.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _ejerciciosRepositorio.GetById(ejercicio.idEjercicio);
            if (existing == null) return NotFound($"No se encontró el ejercicio con ID {ejercicio.idEjercicio} para actualizar.");

            await _ejerciciosRepositorio.Update(ejercicio);
            return Ok(new { mensaje = "Ejercicio actualizado con éxito" });
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
<<<<<<< HEAD
            await _ejercicioRepositorio.Delete(new Ejercicio { Id = id });
            return Ok(new { mensaje = "Ejercicio eliminado con éxito" });
        }

        //////////////////////////////
    }
}
=======
            var existing = await _ejerciciosRepositorio.GetById(id);
            if (existing == null) return NotFound($"No se encontró el ejercicio con ID {id} para eliminar.");

            await _ejerciciosRepositorio.Delete(id);
            return Ok(new { mensaje = "Ejercicio eliminado con éxito" });
        }
    }
}
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
