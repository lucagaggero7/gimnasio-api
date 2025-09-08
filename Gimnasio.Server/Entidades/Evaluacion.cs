using Gimnasio.Server.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Server.Entidades
{
    /// <summary>
    /// Representa una evaluacion del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Evaluacion
    {
        /// <summary>
        /// Identificador único de la evaluacion.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Tipo de evaluacion.
        /// Obligatorio, máximo 45 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El Tipo es obligatorio")]
        [StringLength(45, ErrorMessage = "El Tipo no puede exceder los 45 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de la evaluacion (dd/mm/yyyy).
        /// Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; }

        /// <summary>
        /// Hora de la evaluacion (00:00).
        /// Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La Hora es obligatoria")]
        public TimeOnly Hora { get; set; }

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla cliente.
        /// </summary>
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int FkIdCliente { get; set; }

        /// <summary>
        /// DTO para mostrar al cliente en la evaluacion
        /// Obligatorio
        /// </summary>
        public ClienteMostrarDTO Cliente { get; set; } = new ();
    }
}
