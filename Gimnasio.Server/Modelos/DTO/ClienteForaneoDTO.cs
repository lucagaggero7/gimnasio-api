using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Server.Modelos.DTO
{
    /// <summary>
    /// Representa un cliente en la tabla evaluacion.
    /// Se serializa en camelCase.
    /// </summary>
    public class ClienteForaneoDTO
    {
        /// <summary>
        /// Identificador único del cliente.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del cliente.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; } = string.Empty;
    }
}
