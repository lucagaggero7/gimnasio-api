using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Server.Modelos.Entidades
{
    /// <summary>
    /// Representa un ejercicio del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Ejercicio
    {
        /// <summary>
        /// Identificador único del ejercicio.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del ejercicio.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de series.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "La/s serie/s son obligatorias.")]
        [StringLength(50, ErrorMessage = "La/s serie/s no pueden superar los 50 caracteres.")]
        public string Series { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de repeticiones.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "La/s repeticion/es son obligatorias.")]
        [StringLength(50, ErrorMessage = "La/s repeticion/es no pueden superar los 50 caracteres.")]
        public string Repeticiones { get; set; } = string.Empty;

        /// <summary>
        /// Notas complementarias.
        /// NO Obliagtorio, máximo 50 caracteres.
        /// </summary>
        [StringLength(50, ErrorMessage = "La/s nota/s no pueden superar los 50 caracteres.")]
        public string? Notas { get; set; }

    }
}
