using System.Collections.Generic;
using System.Linq;
using BE_391IAU;
using BLL_391IAU;
using Servicios_391IAU.Composite;

namespace BLL_391IAU
{
    public class SessionManager_391IAU
    {
        private static SessionManager_391IAU instancia;
        private static readonly object candado = new object();

        public BEUsuario UsuarioActual { get; private set; }

        public List<IComponentePermiso_391IAU> Permisos { get; private set; }
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

        public bool HaySesionActiva() => UsuarioActual != null;

        public void IniciarSesion(BEUsuario usuario)
        {
            UsuarioActual = usuario;
            CargarPermisos();
        }

        public void CerrarSesion()
        {
            UsuarioActual = null;
            Permisos.Clear();
        }

        private void CargarPermisos()
        {
            Permisos.Clear();

            if (UsuarioActual == null || UsuarioActual.IDRol_391IAU == null)
                return;

            var bll = new BLLPerfil();
            var perfil = bll.TraerPerfilCompleto(UsuarioActual.IDRol_391IAU.Value);

            if (perfil != null)
                Permisos = perfil.ObtenerHijos();
        }

        public bool TienePermiso(string permisoBuscado)
        {
            if (UsuarioActual == null)
                return false;

            return PermisoRecursivo(Permisos, permisoBuscado);
        }

        private bool PermisoRecursivo(List<IComponentePermiso_391IAU> lista, string permiso)
        {
            foreach (var comp in lista)
            {
                if (comp is PermisoSimple_391IAU p)
                {
                    if (p.Nombre == permiso)
                        return true;
                }
                else if (comp is Familia_391IAU f)
                {
                    if (PermisoRecursivo(f.ObtenerHijos(), permiso))
                        return true;
                }
            }

            return false;
        }
    }
}
