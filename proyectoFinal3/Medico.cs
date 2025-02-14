using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace proyectoFinal3
{
    public class Medico
    {
        [PrimaryKey, AutoIncrement]
        public int idMedico { get; set; }

        public string usuario { get; set; }
        public string password { get; set; }

        public string listaClientes { get; set; }
    }
}
