using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Composite
{
    public class _686DP_PermisoSimple: _686DP_Composite
    {
        public int DP686_PermisoSimpleID { get; set; }
        public string Nombre { get; set; }

        public _686DP_PermisoSimple(int id, string nombre)
        {
            DP686_PermisoSimpleID = id;
            Nombre = nombre;
        }

        public void Agregar(_686DP_Composite componente)
        {
            throw new NotImplementedException("No se puede agregar a un permiso simple.");
        }

        public void Quitar(_686DP_Composite componente)
        {
            throw new NotImplementedException("No se puede quitar de un permiso simple.");
        }

        public List<_686DP_Composite> ObtenerHijos()
        {
            return new List<_686DP_Composite>();
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
