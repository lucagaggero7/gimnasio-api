using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IEjercicioRepositorio
    {
        Task<IEnumerable<Ejercicio>> GetAll();
        Task<Ejercicio?> GetById(int id);
        Task<bool> Create(Ejercicio ejercicio);
        Task<bool> Update(Ejercicio ejercicio);
        Task<bool> Delete(int id);
    }
}
