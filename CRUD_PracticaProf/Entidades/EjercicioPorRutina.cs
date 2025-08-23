using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class EjercicioPorRutina
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        public int FkIdRutina { get; set; }

        [Required(ErrorMessage = "El ID del ejercicio es obligatorio")]
        public int FkIdEjercicio { get; set; }
    }
}
