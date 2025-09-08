using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    /// <summary>
    /// Representa una forma de pago del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class FormaPago
    {
        /// <summary>
        /// Identificador único de la forma de pago.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la forma de pago.
        /// Obligatorio, máximo 45 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre de la forma de pago es obligatorio")]
        [StringLength(45, ErrorMessage = "La forma de pago no puede exceder los 45 caracteres")]
        public string Nombre { get; set; } = string.Empty;

    }
}

