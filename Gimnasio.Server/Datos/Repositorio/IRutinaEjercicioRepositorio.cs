using Gimnasio.Server.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IRutinaEjercicioRepositorio
    {
        Task<IEnumerable<RutinaEjercicio>> GetAll();
        Task<RutinaEjercicio?> GetById(int id);
        Task<RutinaEjercicio> Create(RutinaEjercicio rutinaEjercicio);
        Task<bool> Update(RutinaEjercicio rutinaEjercicio);
        Task<bool> Delete(int id);
    }
}