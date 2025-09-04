using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    /// <summary>
    /// Representa un pago del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Monto es obligatorio.")]
        [Range(10, 9999999, ErrorMessage = "El Monto debe tener entre 2 y 7 dígitos.")]
        public long Monto { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "El ID del tipo de la forma de pago es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de la forma de pago debe ser mayor a 0.")]
        [ForeignKey("FormaPago")]
        public int FkIdFormaPago { get; set; }

        [Required(ErrorMessage = "El ID del tipo de membresía es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de membresia debe ser mayor a 0.")]
        [ForeignKey("TipoMembresia")]
        public int FkIdMembresia { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int FkIdCliente { get; set; }

    }
}

