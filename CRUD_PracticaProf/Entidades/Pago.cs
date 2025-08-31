using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Modelos
{
    public class Pago
    {
        public int Id { get; set; }
        public long Monto { get; set; }

        public DateOnly Fecha { get; set; }

        public int FkIdFormaPago { get; set; }
        public int FkIdMembresia { get; set; }
        public int FkIdCliente { get; set; }

    }
}

