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
        /// Nombre del contacto de emergencia.
        /// Obligatorio, máximo 30 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre del contacto de emergencia es obligatorio")]
        [StringLength(30, ErrorMessage = "El nombre del contacto no puede exceder los 30 caracteres")]
        public string NombreContacto { get; set; } = string.Empty;

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

        /// <summary>
        /// Identificador de clave foranea. Obligatorio
        /// Tabla rutina.
        /// </summary>
        [Required(ErrorMessage = "El ID de la rutina es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID de la rutina debe ser mayor a 0.")]
        [ForeignKey("Rutina")]
        public int FkIdRutina { get; set; }
    }
}