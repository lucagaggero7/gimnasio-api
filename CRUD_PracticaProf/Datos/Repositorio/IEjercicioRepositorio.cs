<<<<<<< HEAD
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
=======
﻿using CRUD_PracticaProf.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos.Repositorio
{
    public interface IEjercicioRepositorio
    {
        Task<IEnumerable<Ejercicio>> GetAll();
        Task<Ejercicio?> GetById(int id);
        Task<bool> Create(Ejercicio ejercicio);
        Task<bool> Update(Ejercicio ejercicio);
        Task<bool> Delete(int id);
    }
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
}
