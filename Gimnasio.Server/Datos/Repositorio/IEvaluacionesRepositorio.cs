using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IEvaluacionesRepositorio
    {
        Task<IEnumerable<Evaluacion>> GetAll();
        Task<Evaluacion> GetById(int id);
        Task<Evaluacion> Create(Evaluacion evaluacion);
        Task<bool> Update(Evaluacion evaluacion);
        Task<bool> Delete(int id);
    }
}