using BE_391IAU;
using DAL_391IAU;
using Servicios_391IAU.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_391IAU
{
    public class BLLPerfil
    {
        public BEPerfil TraerPerfil(int idPerfil)
        {
            return DAL.ObtenerPerfilPorID(idPerfil);
        }

        public List<BEPermiso> ObtenerPermisosSimples(int idPerfil)
        {
            return DAL.ObtenerPermisosDePerfil(idPerfil);
        }

        public List<BEPerfil> TraerPerfiles()
        {
            return DAL.ObtenerPerfiles();
        }
        public PerfilComposite_391IAU TraerPerfilPermisos(int idRol)
        {
            BEPerfil perfilBE = DAL.ObtenerPerfilPorID(idRol);

            PerfilComposite_391IAU perfil = new PerfilComposite_391IAU
            {
                IDRol = idRol,
                Nombre = perfilBE?.Nombre_391IAU ?? ""
            };

            var permisosSimples = DAL.ObtenerPermisosDePerfil(idRol);
            foreach (var p in permisosSimples)
            {
                var permiso = new PermisoSimple_391IAU(p.IDPermiso_391IAU, p.NombrePermiso_391IAU);
                perfil.Permisos.Add(permiso);
            }

            var familiasIDs = DAL.ObtenerFamiliasDePerfil(idRol);
            foreach (int idFamilia in familiasIDs)
            {
                var familia = ConstruirFamilia(idFamilia);
                perfil.Permisos.Add(familia);
            }

            return perfil;
        }

        private Familia_391IAU ConstruirFamilia(int idFamilia)
        {
            string nombreFamilia = DAL.ObtenerNombreFamilia(idFamilia);
            var familia = new Familia_391IAU(idFamilia, nombreFamilia);

            var permisosSimples = DAL.ObtenerPermisosDeFamilia(idFamilia);
            foreach (var p in permisosSimples)
            {
                var permiso = new PermisoSimple_391IAU(p.IDPermiso_391IAU, p.NombrePermiso_391IAU);
                familia.Agregar(permiso);
            }

            var subfamilias = DAL.ObtenerSubfamilias(idFamilia);
            foreach (int idSub in subfamilias)
            {
                var famHija = ConstruirFamilia(idSub);
                familia.Agregar(famHija);
            }

            return familia;
        }
    }
}