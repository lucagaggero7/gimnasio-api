using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gimnasio.Server.Modelos.DTO
{
  public class RutinaCrearDto
  {

        [Required(ErrorMessage = "El nombre de la rutina es obligatorio")]
        [StringLength(45, ErrorMessage = "El nombre no puede exceder los 45 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateOnly FechaInicio { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [StringLength(45, ErrorMessage = "La duración no puede exceder los 45 caracteres")]
        public string Duracion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La frecuencia semanal es obligatoria")]
        [Range(1, 10, ErrorMessage = "La frecuencia semanal debe ser mayor a 0.")]
        public int FrecuenciaSem { get; set; }

        [Required(ErrorMessage = "El objetivo es obligatorio")]
        [StringLength(45, ErrorMessage = "El objetivo no puede exceder los 45 caracteres")]
        public string Objetivo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del tipo de rutina es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del tipo de rutina debe ser mayor a 0.")]
        [ForeignKey("TipoRutina")]
        public int FkIdTipoRutina { get; set; }

        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del cliente debe ser mayor a 0.")]
        [ForeignKey("Cliente")]
        public int? FkIdCliente { get; set; }

        public List<int> Ejercicios { get; set; } = new();
  }
}
