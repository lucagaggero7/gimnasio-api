using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class Membresia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(10, ErrorMessage = "El estado no puede exceder los 10 caracteres")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime Fecha_inicio { get; set; }

        public DateTime? Fecha_vencimiento { get; set; } // Puede ser nulo

        public long? Contacto_emergencia { get; set; } // Puede ser nulo

        [StringLength(30, ErrorMessage = "El nombre del contacto no puede exceder los 30 caracteres")]
        public string? Nombre_contacto { get; set; } // Puede ser nulo

        // Foráneas
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int fk_idClientes { get; set; }

        [Required(ErrorMessage = "El ID del tipo de membresía es obligatorio")]
        public int fk_idTiposMembresia { get; set; }

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        public int fk_idRutinas { get; set; }
    }
}