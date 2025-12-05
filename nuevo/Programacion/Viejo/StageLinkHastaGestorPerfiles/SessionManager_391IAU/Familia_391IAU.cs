using BE_391IAU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Servicios_391IAU.Composite
{
    public class Familia_391IAU : IComponentePermiso_391IAU
    {
        public int IDFamilia { get; private set; }
        public string Nombre { get; set; }

        private readonly List<IComponentePermiso_391IAU> hijos = new List<IComponentePermiso_391IAU>();

        public Familia_391IAU(int idFamilia, string nombre)
        {
            IDFamilia = idFamilia;
            Nombre = nombre;
        }

        public void Agregar(IComponentePermiso_391IAU componente)
        {
            if (!hijos.Contains(componente))
                hijos.Add(componente);
        }

        public void Quitar(IComponentePermiso_391IAU componente)
        {
            if (hijos.Contains(componente))
                hijos.Remove(componente);
        }

        public List<IComponentePermiso_391IAU> ObtenerHijos()
        {
            return hijos.ToList();
        }

        public override string ToString() => Nombre;
    }
}
