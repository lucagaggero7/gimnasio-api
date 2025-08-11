using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IEjerciciosPorRutinaRepositorio
    {
        Task<IEnumerable<EjerciciosPorRutina>> GetAll();
        Task<EjerciciosPorRutina?> GetById(int id);
        Task<bool> Create(EjerciciosPorRutina ejercicioPorRutina);
        Task<bool> Update(EjerciciosPorRutina ejercicioPorRutina);
        Task<bool> Delete(int id);
    }
}