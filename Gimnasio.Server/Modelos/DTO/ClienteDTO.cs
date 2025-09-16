using System.ComponentModel.DataAnnotations;

namespace Gimnasio.Server.Modelos.DTO
{
    public class ClienteDTO
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

        /// <summary>
        /// DNI del cliente.
        /// Obligatorio, entre 7 y 8 dígitos.
        /// </summary>
        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [Range(1000000, 99999999, ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos.")]
        public int Dni { get; set; }

        /// <summary>
        /// Email del cliente.
        /// Obligatorio y con formato válido.
        /// </summary>
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(100, ErrorMessage = "El email no puede superar los 100 caracteres.")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono del cliente.
        /// Obligatorio, máximo 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El Telefono es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        /// <summary>
        /// Dirección del cliente.
        /// Obligatoria, máximo 200 caracteres.
        /// </summary>
        [Required(ErrorMessage = "La Direccion es obligatoria")]
        [StringLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres.")]
        public string Direccion { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento del cliente.
        /// Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateOnly FechaNacimiento { get; set; }

        /// <summary>
        /// Numero del contacto de emergencia.
        /// Obligatorio, máximo 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El contacto de emergencia es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string ContactoEmergencia { get; set; } = string.Empty;

        /// <summary>
        /// Estado del cliente.
        /// No obligatorio, booleano.
        /// </summary>
        public bool Estado { get; set; } = false;
    }
}
