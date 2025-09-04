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
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la forma de pago es obligatorio")]
        [StringLength(45, ErrorMessage = "La forma de pago no puede exceder los 45 caracteres")]
        public string Nombre { get; set; } 

    }
}

