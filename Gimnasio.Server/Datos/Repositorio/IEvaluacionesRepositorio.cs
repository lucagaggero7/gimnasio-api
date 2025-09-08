using Gimnasio.Server.Entidades;
using Gimnasio.Server.Modelos.CRUD_PracticaProf.Modelos;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IEvaluacionesRepositorio
    {
        Task<IEnumerable<Evaluacion>> GetAll();
        Task<Evaluacion?> GetById(int id);
        Task<Evaluacion> Create(Evaluacion evaluacion);
        Task<bool> Update(Evaluacion evaluacion);
        Task<bool> Delete(int id);
    }
}