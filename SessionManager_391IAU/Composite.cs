using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_391IAU.Composite
{
    public interface IComponentePermiso_391IAU
    {
        string Nombre { get; set; }

        void Agregar(IComponentePermiso_391IAU c);
        void Quitar(IComponentePermiso_391IAU c);
        List<IComponentePermiso_391IAU> ObtenerHijos();
    }
}

