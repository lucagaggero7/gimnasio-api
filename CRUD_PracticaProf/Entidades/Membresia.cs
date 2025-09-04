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
    /// Representa una membresia del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Membresia
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(10, ErrorMessage = "El estado no puede exceder los 10 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateOnly FechaInicio { get; set; }

        public DateOnly? FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El contacto de emergencia es obligatorio")]
        [StringLength(20, ErrorMessage = "El teléfono no puede superar los 20 caracteres.")]
        public string ContactoEmergencia { get; set; }

        [Required(ErrorMessage = "El nombre del contacto de emergencia es obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre del contacto no puede exceder los 30 caracteres")]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int FkIdCliente { get; set; }

        [Required(ErrorMessage = "El ID del tipo de membresía es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de membresia debe ser mayor a 0.")]
        [ForeignKey("TipoMembresia")]
        public int FkIdTipoMembresia { get; set; }

        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la rutina debe ser mayor a 0.")]
        [ForeignKey("Rutina")]
        public int FkIdRutina { get; set; }
    }
}