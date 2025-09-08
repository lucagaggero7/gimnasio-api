using Gimnasio.Server.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IPagosRepositorio
    {
        Task<IEnumerable<Pago>> GetAll();

        Task<Pago?> GetById(int id);

        Task<Pago> Create(Pago pago);

        Task<bool> Update(Pago pago);

        Task<bool> Delete(int id);
    }
}
