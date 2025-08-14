using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class EjerciciosPorRutina
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        public int fk_idRutinas { get; set; }

        [Required(ErrorMessage = "El ID del ejercicio es obligatorio")]
        public int fk_idEjercicios { get; set; }
    }
}
