using Gimnasio.Server.Modelos.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Server.Modelos.Entidades
{
    /// <summary>
    /// Representa un pago del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Pago : IHasId
    {
        /// <summary>
        /// Identificador único del pago.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Monto del pago.
        /// Obligatorio, minimo 2 digitos, máximo 7 digitos.
        /// </summary>
        [Required(ErrorMessage = "El Monto es obligatorio.")]
        [Range(10, 9999999, ErrorMessage = "El Monto debe tener entre 2 y 7 dígitos.")]
        public long Monto { get; set; }

        /// <summary>
        /// Fecha del pago (dd/mm/yyyy). 
        /// Obligatoria.
        /// </summary>
        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateOnly Fecha { get; set; }

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla forma de pago.
        /// </summary>
        [Required(ErrorMessage = "El ID del tipo de la forma de pago es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de la forma de pago debe ser mayor a 0.")]
        [ForeignKey("FormaPago")]
        public int FkIdFormaPago { get; set; }

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla membresia.
        /// </summary>
        [Required(ErrorMessage = "El ID de la membresía es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la membresia debe ser mayor a 0.")]
        [ForeignKey("Membresia")]
        public int FkIdMembresia { get; set; }

        /// <summary>
        /// Identificador de clave foranea. No obligatorio
        /// Tabla cliente.
        /// </summary>
        [ForeignKey("Cliente")]
        public int? FkIdCliente { get; set; }

    }
}

