using CRUD_PracticaProf.Datos.Repositorio;
using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_PracticaProf.Controllers
{
    [Route("evaluacion")]
    [ApiController]
    public class EvaluacionesController : ControllerBase
    {
        private readonly IEvaluacionesRepositorio _evaluacionesRepositorio;

        public EvaluacionesController(IEvaluacionesRepositorio evaluacionesRepositorio)
        {
            _evaluacionesRepositorio = evaluacionesRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _evaluacionesRepositorio.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evaluacion = await _evaluacionesRepositorio.GetById(id);
            if (evaluacion == null)
            {
                return NotFound($"No existe una evaluacion con ID {id}");
            }
            return Ok(evaluacion);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Evaluacion evaluacion)
        {
            if (evaluacion == null) return BadRequest("La evaluacion no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _evaluacionesRepositorio.Create(evaluacion);
            return Created("created", created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Evaluacion evaluacion)
        {
            if (evaluacion == null) return BadRequest("La evaluacion no puede ser nula.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existing = await _evaluacionesRepositorio.GetById(evaluacion.Id);
            if (existing == null)
            {
                return NotFound($"No se encontró la evaluacion con ID {evaluacion.Id} para actualizar.");
            }

            await _evaluacionesRepositorio.Update(evaluacion);
            return Ok(new { mensaje = "Evaluacion actualizada con éxito" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _evaluacionesRepositorio.GetById(id);
            if (existing == null)
            {
                return NotFound($"No se encontró la evaluacion con ID {id} para eliminar.");
            }

            await _evaluacionesRepositorio.Delete(id);
            return Ok(new { mensaje = "Evaluacion eliminada con éxito" });
        }
    }
}

