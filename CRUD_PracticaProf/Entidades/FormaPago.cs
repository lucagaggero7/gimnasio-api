using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class FormaPago
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la forma de pago es obligatorio")]
        public string Nombre { get; set; } = string.Empty; 

    }
}

