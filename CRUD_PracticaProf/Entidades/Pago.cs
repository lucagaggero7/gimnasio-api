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
        public int idPagos { get; set; }
        public long Monto { get; set; }

        public DateTime Fecha { get; set; }

        public int fk_idFormasPago { get; set; }
        public int fk_idMembresia { get; set; }
        public long fk_idClientes { get; set; }

    }
}

