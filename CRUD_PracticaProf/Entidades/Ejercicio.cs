<<<<<<< HEAD
﻿namespace CRUD_PracticaProf.Entidades
{
    public class Ejercicio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Series { get; set; }

        public string Repeticiones { get; set; }

        public string? Notas { get; set; }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class Ejercicio
    {
        [Key]
        public int idEjercicio { get; set; }

        [Required(ErrorMessage = "El nombre del ejercicio es obligatorio")]
        [StringLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La serie es obligatoria")]
        [StringLength(45, ErrorMessage = "La serie no puede exceder los 45 caracteres")]
        public string Series { get; set; } = string.Empty;

        [Required(ErrorMessage = "Las repeticiones son obligatorias")]
        [StringLength(45, ErrorMessage = "Las repeticiones no pueden exceder los 45 caracteres")]
        public string Repeticiones { get; set; } = string.Empty;

        [StringLength(45, ErrorMessage = "Las notas no pueden exceder los 45 caracteres")]
        public string? Notas { get; set; }
    }
}
>>>>>>> 6959d4913ba7e333d784f684ff672bdd73136aa3
