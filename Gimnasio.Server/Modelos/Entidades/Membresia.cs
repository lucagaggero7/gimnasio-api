using Gimnasio.Server.Modelos.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Server.Modelos.Entidades
{
    /// <summary>
    /// Representa una membresia del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Membresia : IHasId
    {
        /// <summary>
        /// Identificador único de la membresia.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Estado de la membresia.
        /// Obligatorio, máximo 10 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(10, ErrorMessage = "El estado no puede exceder los 10 caracteres")]
        public string Estado { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de inicio de la membresia (dd/mm/yyyy). 
        /// Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateOnly FechaInicio { get; set; }

        /// <summary>
        /// Fecha de vencimiento de la membresia (dd/mm/yyyy). 
        /// NO Obligatoria.
        /// </summary>
        public DateOnly? FechaVencimiento { get; set; }

        /// <summary>
        /// Numero del contacto de emergencia.
        /// Obligatorio, máximo 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El contacto de emergencia es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string ContactoEmergencia { get; set; } = string.Empty;

        /// <summary>
        /// Total a pagar del cliente.
        /// Obligatorio, entre 1 y 10 dígitos.
        /// </summary>
        [Required(ErrorMessage = "El Total a pagar es obligatorio.")]
        [Range(0, 9999999999, ErrorMessage = "El Total debe tener entre 1 y 10 dígitos.")]
        public int Total { get; set; }

        /// <summary>
        /// Saldo restante del cliente.
        /// Obligatorio, entre 1 y 10 dígitos.
        /// </summary>
        [Required(ErrorMessage = "El Saldo a pagar es obligatorio.")]
        [Range(0, 9999999999, ErrorMessage = "El Saldo debe tener entre 1 y 10 dígitos.")]
        public int Saldo { get; set; }

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla cliente.
        /// </summary>
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int FkIdCliente { get; set; }

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla tipo membresia.
        /// </summary>
        [Required(ErrorMessage = "El ID del tipo de membresía es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de membresia debe ser mayor a 0.")]
        [ForeignKey("TipoMembresia")]
        public int FkIdTipoMembresia { get; set; }
    }
}