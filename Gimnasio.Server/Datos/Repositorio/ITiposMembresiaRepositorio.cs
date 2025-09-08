using Gimnasio.Server.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface ITiposMembresiaRepositorio
    {
        Task<IEnumerable<TipoMembresia>> GetAll();
        Task<TipoMembresia?> GetById(int id);
        Task<TipoMembresia> Create(TipoMembresia tipoMembresia);
        Task<bool> Update(TipoMembresia tipoMembresia);
        Task<bool> Delete(int id);
    }
}