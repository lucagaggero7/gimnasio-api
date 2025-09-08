using Gimnasio.Server.Modelos.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Server.Modelos.Entidades
{
        /// <summary>
        /// Representa una rutina del sistema.
        /// Se serializa en camelCase.
        /// </summary>
        public class Rutina : IHasId
    {
            /// <summary>
            /// Identificador único de la rutina.
            /// Clave primaria en la base de datos.
            /// </summary>
            [Key]
            public int Id { get; set; }

            /// <summary>
            /// Nombre de la rutina.
            /// Obligatorio, máximo 45 caracteres.
            /// </summary>
            [Required(ErrorMessage = "El nombre de la rutina es obligatorio")]
            [StringLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
            public string Nombre { get; set; } = string.Empty;

            /// <summary>
            /// Fecha de inicio de la rutina (dd/mm/yyyy).
            /// Obligatoria.
            /// </summary>
            [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
            public DateOnly FechaInicio { get; set; }

            /// <summary>
            /// Duracion de la rutina.
            /// Obligatorio, máximo 45 caracteres.
            /// </summary>
            [Required(ErrorMessage = "La duración es obligatoria")]
            [StringLength(45, ErrorMessage = "La duración no puede exceder los 45 caracteres")]
            public string Duracion { get; set; } = string.Empty;

            /// <summary>
            /// Frecuencia semanal de la rutina.
            /// Obligatorio, máximo 45 caracteres.
            /// </summary>
            [Required(ErrorMessage = "La frecuencia semanal es obligatoria")]
            [StringLength(45, ErrorMessage = "La frecuencia semanal no puede exceder los 45 caracteres")]
            public string FrecuenciaSem { get; set; } = string.Empty;

            /// <summary>
            /// Objetivo de la rutina.
            /// Obligatorio, máximo 45 caracteres.
            /// </summary>
            [Required(ErrorMessage = "El objetivo es obligatorio")]
            [StringLength(45, ErrorMessage = "El objetivo no puede exceder los 45 caracteres")]
            public string Objetivo { get; set; } = string.Empty;

            /// <summary>
            /// Identificador de clave foranea. Obligatorio
            /// Tabla tipo de rutina.
            /// </summary>
            [Required(ErrorMessage = "El ID del tipo de rutina es obligatorio")]
            [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de rutina debe ser mayor a 0.")]
            [ForeignKey("TipoRutina")]
            public int FkIdTipoRutina { get; set; }
        }
}
