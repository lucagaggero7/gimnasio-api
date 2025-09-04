
using System.ComponentModel.DataAnnotations;

namespace CRUD_PracticaProf.Entidades
{
    /// <summary>
    /// Representa un ejercicio del sistema.
    /// Se serializa en camelCase.
    /// </summary>
    public class Ejercicio
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La/s serie/s son obligatorias.")]
        [StringLength(50, ErrorMessage = "La/s serie/s no pueden superar los 50 caracteres.")]
        public string Series { get; set; }

        [Required(ErrorMessage = "La/s repeticion/es son obligatorias.")]
        [StringLength(50, ErrorMessage = "La/s repeticion/es no pueden superar los 50 caracteres.")]
        public string Repeticiones { get; set; }

        [StringLength(50, ErrorMessage = "La/s nota/s no pueden superar los 50 caracteres.")]
        public string? Notas { get; set; }

    }
}
