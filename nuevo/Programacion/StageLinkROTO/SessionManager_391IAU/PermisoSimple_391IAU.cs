using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_391IAU.Composite
{
    public class PermisoSimple_391IAU : IComponentePermiso_391IAU
    {
        public int IDPermiso { get; private set; }
        public string Nombre { get; set; }

        public PermisoSimple_391IAU(int id, string nombre)
        {
            IDPermiso = id;
            Nombre = nombre;
        }

        public void Agregar(IComponentePermiso_391IAU c)
        {
            throw new NotSupportedException("Un permiso simple no puede tener hijos.");
        }

        public void Quitar(IComponentePermiso_391IAU c)
        {
            throw new NotSupportedException("Un permiso simple no puede tener hijos.");
        }

        public List<IComponentePermiso_391IAU> ObtenerHijos()
        {
            return new List<IComponentePermiso_391IAU>();
        }

        public override string ToString() => Nombre;
    }
}
