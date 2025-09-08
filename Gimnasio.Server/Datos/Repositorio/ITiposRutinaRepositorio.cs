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
        Task<IEnumerable<TipoRutina>> GetAll();
        Task<TipoRutina?> GetById(int id);
        Task<TipoRutina> Create(TipoRutina tipoRutina);
        Task<bool> Update(TipoRutina tipoRutina);
        Task<bool> Delete(int id);
    }
}