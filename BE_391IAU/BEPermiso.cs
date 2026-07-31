using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_391IAU
{
    public class BEPermiso
    {
        public int IDPermiso_391IAU { get; set; }
        public string NombrePermiso_391IAU { get; set; }
        public bool EsFamilia_391IAU { get; set; }
        public List<BEPermiso> Hijos { get; set; } = new List<BEPermiso>();
    }
}
