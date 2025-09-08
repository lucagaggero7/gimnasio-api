
﻿using Gimnasio.Server.Entidades;
using Gimnasio.Server.Modelos;

namespace Gimnasio.Server.Datos.Repositorio
{
    public interface IEjerciciosRepositorio
    {
        Task<IEnumerable<Ejercicio>> GetAll();

        Task<Ejercicio?> GetById(int id);

        Task<Ejercicio> Create(Ejercicio ejercicio);

        Task<bool> Update(Ejercicio ejercicio);

        Task<bool> Delete(int  id);
    }
}
