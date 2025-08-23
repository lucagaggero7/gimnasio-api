using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IMembresiasRepositorio
    {
        Task<IEnumerable<Membresia>> GetAll();
        Task<Membresia?> GetById(int id);
        Task<bool> Create(Membresia membresia);
        Task<bool> Update(Membresia membresia);
        Task<bool> Delete(int id);
    }
}