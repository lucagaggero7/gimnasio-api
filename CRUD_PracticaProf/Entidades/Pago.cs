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

        public DateTime Fecha { get; set; }

        public int fk_idFormasPago { get; set; }
        public int fk_idMembresias { get; set; }
        public int fk_idClientes { get; set; }

    }
}

