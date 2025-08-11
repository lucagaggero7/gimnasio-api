using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface ITiposRutinaRepositorio
    {
        Task<IEnumerable<TiposRutina>> GetAll();
        Task<TiposRutina?> GetById(int id);
        Task<bool> Create(TiposRutina tipoRutina);
        Task<bool> Update(TiposRutina tipoRutina);
        Task<bool> Delete(int id);
    }
}