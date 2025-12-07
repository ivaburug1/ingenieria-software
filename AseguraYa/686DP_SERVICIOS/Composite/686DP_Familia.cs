using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Composite
{
    public class _686DP_Familia:_686DP_Composite
    {
        public int idFamilia {  get; set; }
        public string Nombre { get; set; }
        private List<_686DP_Composite> _hijos = new List<_686DP_Composite>();

        public _686DP_Familia(string nombre, int idFamilia)
        {
            Nombre = nombre;
            this.idFamilia = idFamilia;
        }

        public void Agregar(_686DP_Composite componente)
        {
            if (ContienePermiso(componente.Nombre))
                throw new Exception($"El permiso '{componente.Nombre}' ya está contenido en la familia '{Nombre}'.");
            else
            {
                _hijos.Add(componente);
            }
        }

        private bool ContienePermiso(string nombre)
        {
            if (this.Nombre == nombre)
                return true;

            foreach (var hijo in _hijos)
            {
                if (hijo.Nombre == nombre)
                    return true;

                foreach (var nieto in hijo.ObtenerHijos())
                {
                    if (ContienePermisoRecursivo(nieto, nombre))
                        return true;
                }
            }

            return false;
        }

        private bool ContienePermisoRecursivo(_686DP_Composite permiso, string nombre)
        {
            if (permiso.Nombre == nombre)
                return true;

            foreach (var hijo in permiso.ObtenerHijos())
            {
                if (ContienePermisoRecursivo(hijo, nombre))
                    return true;
            }

            return false;
        }
        public void Quitar(_686DP_Composite componente)
        {
            _hijos.Remove(componente);
        }

        public List<_686DP_Composite> ObtenerHijos()
        {
            return _hijos;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
