using Gimnasio.Server.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IRutinasRepositorio
    {
        Task<IEnumerable<Rutina>> GetAll();
        Task<Rutina?> GetById(int id);
        Task<Rutina> Create(Rutina rutina, List<int> ejercicios);
        Task<bool> Update(Rutina rutina, List<int> ejercicios);
        Task<bool> Delete(int id);
    }
}