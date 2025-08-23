using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    namespace CRUD_PracticaProf.Modelos
    {
        public class Rutina
        {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre de la rutina es obligatorio")]
            [StringLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
            public string Nombre { get; set; } = string.Empty;

            [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
            public DateTime FechaInicio { get; set; }

            [Required(ErrorMessage = "La duración es obligatoria")]
            [StringLength(45, ErrorMessage = "La duración no puede exceder los 45 caracteres")]
            public string Duracion { get; set; } = string.Empty;

            [Required(ErrorMessage = "La frecuencia semanal es obligatoria")]
            [StringLength(45, ErrorMessage = "La frecuencia semanal no puede exceder los 45 caracteres")]
            public string FrecuenciaSem { get; set; } = string.Empty;

            [Required(ErrorMessage = "El objetivo es obligatorio")]
            [StringLength(45, ErrorMessage = "El objetivo no puede exceder los 45 caracteres")]
            public string Objetivo { get; set; } = string.Empty;

            // Llave foránea a TiposRutina
            [Required(ErrorMessage = "El ID del tipo de rutina es obligatorio")]
            public int FkIdTipoRutina { get; set; }
        }
    }
}