using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BEBoleto
    {
        public string Artista { get; set; }
        public int DNICliente { get; set; }
        public int CodigoDeEvento { get; set; }
        public int Sector { get; set; } 
        public DateTime FechaEvento { get; set; }
        public decimal Precio { get; set; }
    }

}
