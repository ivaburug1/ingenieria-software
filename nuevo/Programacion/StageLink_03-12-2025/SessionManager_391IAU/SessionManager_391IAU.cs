using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using Servicios_391IAU.Composite;

namespace BLL_391IAU
{
    public class SessionManager_391IAU
    {
        private static SessionManager_391IAU instancia;
        private static readonly object candado = new object();

        public BEUsuario UsuarioActual { get; private set; }

        public List<IComponentePermiso_391IAU> Permisos { get; set; }
            = new List<IComponentePermiso_391IAU>();

        private SessionManager_391IAU() { }

        public static SessionManager_391IAU ObtenerInstancia()
        {
            if (instancia == null)
            {
                lock (candado)
                {
                    if (instancia == null)
                        instancia = new SessionManager_391IAU();
                }
            }
            return instancia;
        }
        public static SessionManager_391IAU Instancia => ObtenerInstancia();

        public static SessionManager_391IAU ObtenerSesion()
        {
            return ObtenerInstancia();
        }
        public bool HaySesionActiva()
        {
            return UsuarioActual != null;
        }

        public void IniciarSesion(BEUsuario usuario)
        {
            UsuarioActual = usuario;
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            Permisos.Clear();
        }
    }
}
