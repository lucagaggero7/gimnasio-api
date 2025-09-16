using Gimnasio.Server.Modelos.DTO;
using Gimnasio.Server.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IClientesRepositorio
    {
        Task<IEnumerable<ClienteDTO>> GetAll();

        Task<IEnumerable<ClienteForaneoDTO>> GetFkDTO();

        Task<ClienteDTO?> GetById(int id);

        Task<Cliente> Create(Cliente cliente);

        Task<bool> Update(Cliente cliente);

        Task<bool> Delete(int id);
    }
}
