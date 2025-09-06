using CRUD_PracticaProf.Modelos;
using CRUD_PracticaProf.Modelos.CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IRutinasRepositorio
    {
        Task<IEnumerable<Rutina>> GetAll();
        Task<Rutina> GetById(int id);
        Task<bool> Create(Rutina rutina);
        Task<bool> Update(Rutina rutina);
        Task<bool> Delete(int id);
    }
}