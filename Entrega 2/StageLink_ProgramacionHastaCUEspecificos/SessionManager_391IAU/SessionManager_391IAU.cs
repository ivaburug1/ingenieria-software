using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;

namespace BLL_391IAU
{
    public class SessionManager_391IAU
    {
        private static SessionManager_391IAU instancia;
        private static readonly object candado = new object();

        public BEUsuario UsuarioActual { get; private set; }

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
        }
    }
}
