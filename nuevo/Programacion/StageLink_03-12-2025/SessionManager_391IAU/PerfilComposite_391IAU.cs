using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_391IAU.Composite
{
    public class PerfilComposite_391IAU
    {
        public int IDRol { get; set; }
        public string Nombre { get; set; }
        public List<IComponentePermiso_391IAU> Permisos { get; set; } = new List<IComponentePermiso_391IAU>();
    }
}
