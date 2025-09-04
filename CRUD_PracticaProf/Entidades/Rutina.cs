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
    namespace CRUD_PracticaProf.Modelos
    {
        /// <summary>
        /// Representa una rutina del sistema.
        /// Se serializa en camelCase.
        /// </summary>
        public class Rutina
        {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre de la rutina es obligatorio")]
            [StringLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
            public DateOnly FechaInicio { get; set; }

            [Required(ErrorMessage = "La duración es obligatoria")]
            [StringLength(45, ErrorMessage = "La duración no puede exceder los 45 caracteres")]
            public string Duracion { get; set; } 

            [Required(ErrorMessage = "La frecuencia semanal es obligatoria")]
            [StringLength(45, ErrorMessage = "La frecuencia semanal no puede exceder los 45 caracteres")]
            public string FrecuenciaSem { get; set; } 

            [Required(ErrorMessage = "El objetivo es obligatorio")]
            [StringLength(45, ErrorMessage = "El objetivo no puede exceder los 45 caracteres")]
            public string Objetivo { get; set; } 

            [Required(ErrorMessage = "El ID del tipo de rutina es obligatorio")]
            [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de rutina debe ser mayor a 0.")]
            [ForeignKey("TipoRutina")]
            public int FkIdTipoRutina { get; set; }
        }
    }
}