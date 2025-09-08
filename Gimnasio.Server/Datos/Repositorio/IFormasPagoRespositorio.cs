using Gimnasio.Server.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IFormasPagoRepositorio
    {
        Task<IEnumerable<FormaPago>> GetAll();
        Task<FormaPago?> GetById(int id);
        Task<FormaPago> Create(FormaPago formaPago);
        Task<bool> Update(FormaPago formaPago);
        Task<bool> Delete(int id);
    }
}
