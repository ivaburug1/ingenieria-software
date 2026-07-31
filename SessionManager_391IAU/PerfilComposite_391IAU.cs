using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios_391IAU.Composite
{
    public class PerfilComposite_391IAU : IComponentePermiso_391IAU
    {
        public int IDRol { get; set; }
        public string Nombre { get; set; }

        public List<IComponentePermiso_391IAU> Permisos { get; set; } = new List<IComponentePermiso_391IAU>();

        public void Agregar(IComponentePermiso_391IAU componente)
        {
            if (!Permisos.Contains(componente))
                Permisos.Add(componente);
        }

        public void Quitar(IComponentePermiso_391IAU componente)
        {
            if (Permisos.Contains(componente))
                Permisos.Remove(componente);
        }
        public List<IComponentePermiso_391IAU> ObtenerHijos()
        {
            return Permisos;
        }

        public override string ToString() => Nombre;
    }
}
