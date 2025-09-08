using Gimnasio.Server.Entidades;

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