using Gimnasio.Server.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IMembresiasRepositorio
    {
        Task<IEnumerable<Membresia>> GetAll();
        Task<Membresia?> GetById(int id);
        Task<IEnumerable<Membresia>> GetByClienteId(int id);
        Task<Membresia> Create(Membresia membresia);
        Task<bool> Update(Membresia membresia);
        Task<bool> Delete(int id);
    }
}