using _686DP_SERVICIOS.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _686DP_MPP;
using System.Threading.Tasks;

namespace _686DP_BLL
{
    public class _686DP_BLLPerfil
    {
        _686DP_MPPFamilia mppFlia = new _686DP_MPPFamilia();
        _686DP_MPPPermisoSimple mppPS = new _686DP_MPPPermisoSimple();
        _686DP_MPPPerfil mpp = new _686DP_MPPPerfil();

        public void CrearPerfil(_686DP_Perfil perfil)
        {
            int perfilID = mpp.CrearPerfilBase(perfil);

            foreach (var componente in perfil.ObtenerPermisos())
            {
                if (componente is _686DP_PermisoSimple permiso)
                {
                    mpp.AsociarPermisoAlPerfil(perfilID, permiso.DP686_PermisoSimpleID);
                }
                else if (componente is _686DP_Familia familia)
                {
                    mpp.AsociarFamiliaAlPerfil(perfilID, familia.idFamilia);
                }
            }
        }

        public void DesasignarFamiliaDesdePerfil(_686DP_Perfil perfil, _686DP_Familia familia)
        {
            try
            {
                mpp.EliminarRelacionPerfilFamilia(perfil.Nombre, familia.idFamilia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasignar familia del perfil: " + ex.Message);
            }
        }

        public void DesasignarPermisoDesdePerfil(_686DP_Perfil perfil, _686DP_PermisoSimple permiso)
        {
            try
            {
                mpp.EliminarRelacionPerfilPermiso(perfil.Nombre, permiso.DP686_PermisoSimpleID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasignar permiso del perfil: " + ex.Message);
            }
        }

        public void EliminarPerfil(_686DP_Perfil perfil)
        {
            try
            {
                mpp.EliminarPerfil(perfil.Nombre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el perfil: " + ex.Message, ex);
            }
        }

        public _686DP_Perfil TraerPerfil(string nombre)
        {
            int id = mpp.traerCodigoPerfil(nombre);
            var perfil = mpp.TraerPerfil(nombre);

            var permisosSimples = mpp.TraerPermisosSimplesDelPerfil(id);
            foreach (_686DP_PermisoSimple permiso in permisosSimples)
                perfil.AgregarPermiso(permiso);

            var familias = mpp.TraerFamiliasDelPerfil(id);
            foreach (var familia in familias)
            {
                CargarFamiliaRecursiva(familia);
                perfil.AgregarPermiso(familia);
            }

            return perfil;
        }

        private void CargarFamiliaRecursiva(_686DP_Familia familia)
        {
            var hijos = mppFlia.TraerHijosFamilia(familia.idFamilia);
            foreach (_686DP_Composite hijo in hijos)
            {
                if (hijo is _686DP_PermisoSimple permiso)
                {
                    familia.Agregar(permiso);
                }
                else if (hijo is _686DP_Familia subFamilia)
                {
                    CargarFamiliaRecursiva(subFamilia);
                    familia.Agregar(subFamilia);
                }
            }
        }

        public List<_686DP_Perfil> TraerPerfiles()
        {
            List<_686DP_Perfil> Perfiles = mpp.TraerPerfiles();
            return Perfiles;
        }

        public bool ValidarUnico(Func<string> toLower)
        {
            bool unico = mpp.ValidarUnico(toLower);
            return unico;
        }

        public _686DP_Perfil TraerPerfilDelUsuario(int Dni)
        {
            return mpp.TraerPerfilUsuario(Dni);
        }

        public List<_686DP_Perfil> TraerPerfilesConFamilia(int idFamilia)
        {
            List<_686DP_Perfil> perfiles = mpp.TraerPerfilesDeFamilia(idFamilia);
            return perfiles;
        }

        public List<_686DP_PermisoSimple> TraerPermisosDelPerfil(int dP686_PerfilID)
        {
            List<_686DP_PermisoSimple> permisosSimples = mpp.TraerPermisosSimplesDelPerfil(dP686_PerfilID);
            return permisosSimples;
        }

        public int TraerCodigoPerfil(_686DP_Perfil perfil)
        {
            int id = mpp.traerCodigoPerfil(perfil.Nombre);
            return id;
        }
    }
}
