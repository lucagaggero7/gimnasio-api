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

        public int Formas_pago_idFormas_pago { get; set; }
        public int Membresia_idMembresia { get; set; }
        public long Membresia_Clientes_idClientes { get; set; }

    }
}

