using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface ITiposMembresiaRepositorio
    {
        Task<IEnumerable<TiposMembresia>> GetAll();
        Task<TiposMembresia?> GetById(int id);
        Task<bool> Create(TiposMembresia tipoMembresia);
        Task<bool> Update(TiposMembresia tipoMembresia);
        Task<bool> Delete(int id);
    }
}