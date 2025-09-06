using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_PracticaProf.Datos
{
    public class MySQLConfig
    {
        public string ConnectionString { get; set; }
        public MySQLConfig(string connectionString)
        {
            ConnectionString = connectionString;
        }
     
    }
}
