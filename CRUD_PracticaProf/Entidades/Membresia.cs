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
        public DateOnly FechaInicio { get; set; }

        public DateOnly FechaVencimiento { get; set; } // Puede ser nulo

        public long ContactoEmergencia { get; set; } // Puede ser nulo

        [StringLength(30, ErrorMessage = "El nombre del contacto no puede exceder los 30 caracteres")]
        public string NombreContacto { get; set; } // Puede ser nulo

        // Foráneas
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int FkIdCliente { get; set; }

        [Required(ErrorMessage = "El ID del tipo de membresía es obligatorio")]
        public int FkIdTipoMembresia { get; set; }

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        public int FkIdRutina { get; set; }
    }
}