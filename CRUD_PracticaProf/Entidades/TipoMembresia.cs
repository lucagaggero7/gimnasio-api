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
    /// Representa un tipo de membresia del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class TipoMembresia
    {
        /// <summary>
        /// Identificador único del tipo de membresia.
        /// Clave primaria en la base de datos.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del tipo de membresia.
        /// Obligatorio, máximo 20 caracteres.
        /// </summary>
        [Required(ErrorMessage = "El nombre del tipo de membresía es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string Nombre { get; set; }
    }
}

