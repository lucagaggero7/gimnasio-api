using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class TiposRutina
    {
        [Key]
        public int idTipos_Rutina { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de rutina es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
    }
}
