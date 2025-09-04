using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    /// <summary>
    /// Representa la relación muchos a muchos entre Rutinas y Ejercicios.
    /// Se serializa en camelCase.
    /// </summary>
    public class EjercicioPorRutina
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la rutina debe ser mayor a 0.")]
        [ForeignKey("Rutina")] 
        public int FkIdRutina { get; set; }

        [Required(ErrorMessage = "El ID del ejercicio es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del ejercicio debe ser mayor a 0.")]
        [ForeignKey("Ejercicio")] 
        public int FkIdEjercicio { get; set; }
    }
}
