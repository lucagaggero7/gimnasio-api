using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Server.Entidades
{
    /// <summary>
    /// Representa un tipo de rutina del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class TipoRutina
    {
        /// <summary>
        /// Identificador único del tipo de rutina.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del tipo de rutina.
        /// Obligatorio, máximo 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre del tipo de membresía es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}
