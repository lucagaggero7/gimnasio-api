using CRUD_PracticaProf.DTO;
using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IClientesRepositorio
    {
        Task<IEnumerable<Cliente>> GetAll();

        Task<IEnumerable<ClienteMostrarDTO>> GetAllDTO();

        Task<Cliente> GetById(int id);

        Task<Cliente> Create(Cliente cliente);

        Task<bool> Update(Cliente cliente);

        Task<bool> Delete(Cliente cliente);
    }
}
