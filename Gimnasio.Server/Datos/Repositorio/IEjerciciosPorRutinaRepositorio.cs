using Gimnasio.Server.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IEjerciciosPorRutinaRepositorio
    {
        Task<IEnumerable<EjercicioPorRutina>> GetAll();
        Task<EjercicioPorRutina?> GetById(int id);
        Task<EjercicioPorRutina> Create(EjercicioPorRutina ejercicioPorRutina);
        Task<bool> Update(EjercicioPorRutina ejercicioPorRutina);
        Task<bool> Delete(int id);
    }
}