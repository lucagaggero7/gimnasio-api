using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IFormasPagoRepositorio
    {
        Task<IEnumerable<FormasPago>> GetAll();
        Task<FormasPago?> GetById(int id);
        Task<bool> Create(FormasPago formaPago);
        Task<bool> Update(FormasPago formaPago);
        Task<bool> Delete(int id);
    }
}
