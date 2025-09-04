using CRUD_PracticaProf.DTO;
using CRUD_PracticaProf.Modelos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_PracticaProf.Entidades
{
    /// <summary>
    /// Representa una evaluacion del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Evaluacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        [StringLength(45, ErrorMessage = "El Tipo no puede exceder los 45 caracteres")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "La Hora es obligatoria")]
        public TimeOnly Hora { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int FkIdCliente { get; set; }

        public ClienteMostrarDTO Cliente { get; set; }
    }
}
