
﻿using CRUD_PracticaProf.Entidades;
using CRUD_PracticaProf.Modelos;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IEjercicioRepositorio
    {
        Task<IEnumerable<Ejercicio>> GetAll();

        Task<Ejercicio> GetById(int id);

        Task<bool> Create(Ejercicio ejercicio);

        Task<bool> Update(Ejercicio ejercicio);

        Task<bool> Delete(Ejercicio ejercicio);
    }
}
