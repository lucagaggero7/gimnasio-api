using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class TipoMembresia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de membresía es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string Nombre { get; set; } = string.Empty;
    }
}

