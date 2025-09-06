using System.ComponentModel.DataAnnotations;

namespace CRUD_PracticaProf.DTO
{
    /// <summary>
    /// Representa un cliente en la tabla evaluacion.
    /// Se serializa en camelCase.
    /// </summary>
    public class ClienteMostrarDTO
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
        public string Nombre { get; set; }

        /// <summary>
        /// Apellido del cliente.
        /// Obligatorio, máximo 50 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        public string Apellido { get; set; }
    }
}
