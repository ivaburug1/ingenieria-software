using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Composite
{
    public class _686DP_Perfil
    {
        public string Nombre { get; set; }
        private List<_686DP_Composite> _permisos = new List<_686DP_Composite>();

        public _686DP_Perfil(string nombre)
        {
            Nombre = nombre;
        }

        public void AgregarPermiso(_686DP_Composite nuevoPermiso)
        {
            if (TienePermiso(nuevoPermiso.Nombre))
                throw new Exception($"El permiso '{nuevoPermiso.Nombre}' ya está incluido en el perfil o en alguna familia.");

            _permisos.Add(nuevoPermiso);
        }

        public List<_686DP_Composite> ObtenerPermisos()
        {
            return _permisos;
        }

        public bool TienePermiso(string nombre)
        {
            foreach (var permiso in _permisos)
            {
                if (BuscarPermiso(permiso, nombre))
                    return true;
            }
            return false;
        }

        private bool BuscarPermiso(_686DP_Composite permiso, string nombre)
        {
            if (permiso.Nombre == nombre)
                return true;

            foreach (var hijo in permiso.ObtenerHijos())
            {
                if (BuscarPermiso(hijo, nombre))
                    return true;
            }

            return false;
        }
    }
}
