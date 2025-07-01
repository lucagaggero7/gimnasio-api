using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string? Apellido { get; set; }

        public int? Dni { get; set; }

        public string? Email { get; set; }

        public string? Telefono { get; set; }

        public string? Direccion { get; set; }

        public DateTime? Fecha_nacimiento { get; set; }

    }
}

