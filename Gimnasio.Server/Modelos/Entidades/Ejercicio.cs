using Gimnasio.Server.Modelos.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Server.Modelos.Entidades
{
    /// <summary>
    /// Representa un ejercicio del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Ejercicio : IHasId
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
        /// Obligatorio, valor minimo 1, máximo 100.
        /// </summary>
        [Required(ErrorMessage = "La/s serie/s son obligatorias.")]
        [Range(0, 100, ErrorMessage = "Valor permitidos de serie/s entre 1 y 100.")]
        public int Series { get; set; }

        /// <summary>
        /// Cantidad de repeticiones.
        /// Obligatorio, valor minimo 1, máximo 100.
        /// </summary>
        [Required(ErrorMessage = "La/s repeticion/es son obligatorias.")]
        [Range(0, 100, ErrorMessage = "Valor permitidos de repeticion/es entre 1 y 100.")]
        public int Repeticiones { get; set; }

        /// <summary>
        /// Notas complementarias.
        /// NO Obliagtorio, máximo 50 caracteres.
        /// </summary>
        [StringLength(50, ErrorMessage = "La/s nota/s no pueden superar los 50 caracteres.")]
        public string? Notas { get; set; }

    }
}
