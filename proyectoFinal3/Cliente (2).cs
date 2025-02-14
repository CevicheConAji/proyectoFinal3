using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace proyectoFinal3
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int idCliente { get; set; }

        public string usuario { get; set; }
        public string password { get; set; }
        public int medico { get; set; }

        // Citas y recetas se almacenan como cadenas separadas por comas
        public string citas { get; set; }
        public string recetas { get; set; }
    }
}
