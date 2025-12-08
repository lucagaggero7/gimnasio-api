using Gimnasio.Server.Modelos.Entidades;

namespace Gimnasio.Server.Modelos.DTO
{
    public class RutinaListaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public DateOnly FechaInicio { get; set; }
        public string Duracion { get; set; } = string.Empty;
        public string Objetivo { get; set; } = string.Empty;
        public int FrecuenciaSem { get; set; }
        public int FkIdTipoRutina { get; set; }
        public int? FkIdCliente { get; set; }

        public List<Ejercicio> Ejercicios { get; set; } = new();
    }
}
