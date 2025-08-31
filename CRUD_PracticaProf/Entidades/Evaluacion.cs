using CRUD_PracticaProf.DTO;
using CRUD_PracticaProf.Modelos;
using System.ComponentModel.DataAnnotations;

namespace CRUD_PracticaProf.Entidades
{
    public class Evaluacion
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Tipo es obligatorio")]
        [StringLength(45, ErrorMessage = "El Tipo no puede exceder los 45 caracteres")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateOnly Fecha { get; set; }

        [Required(ErrorMessage = "La Hora es obligatoria")]
        public TimeOnly Hora { get; set; }

        // Llave foránea a Clientes
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int FkIdCliente { get; set; }

        public ClienteMostrarDTO Cliente { get; set; }
    }
}
