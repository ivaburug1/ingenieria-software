using BE_391IAU;
using DAL_391IAU;
using Servicios_391IAU.Composite;
using System;
using System.Collections.Generic;

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

        public PerfilComposite_391IAU TraerPerfilCompleto(int idPerfil)
        {
            var bePerfil = DAL.ObtenerPerfilPorID(idPerfil);
            if (bePerfil == null)
                return null;

            PerfilComposite_391IAU perfil = new PerfilComposite_391IAU
            {
                IDRol = bePerfil.IDRol_391IAU,
                Nombre = bePerfil.Nombre_391IAU
            };

            var permisosSimples = DAL.ObtenerPermisosDePerfil(idPerfil);
            foreach (var p in permisosSimples)
            {
                perfil.Agregar(new PermisoSimple_391IAU(
                    p.IDPermiso_391IAU,
                    p.NombrePermiso_391IAU
                ));
            }

            var familiasID = DAL.ObtenerFamiliasDePerfil(idPerfil);
            foreach (int idFamilia in familiasID)
            {
                var familia = ConstruirFamilia(idFamilia);
                perfil.Agregar(familia);
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
                familia.Agregar(new PermisoSimple_391IAU(
                    p.IDPermiso_391IAU,
                    p.NombrePermiso_391IAU
                ));
            }

            var subfamilias = DAL.ObtenerSubfamilias(idFamilia);
            foreach (int idSub in subfamilias)
            {
                var famHija = ConstruirFamilia(idSub);
                familia.Agregar(famHija);
            }

            return familia;
        }
        public void EliminarPermisosDePerfil(int idPerfil)
        {
            DAL.EliminarPermisosDePerfil(idPerfil);
        }

        public void EliminarFamiliasDePerfil(int idPerfil)
        {
            DAL.EliminarFamiliasDePerfil(idPerfil);
        }

        public void AsociarPermisoAPerfil(int idPerfil, int idPermiso)
        {
            DAL.AsociarPermisoAPerfil(idPerfil, idPermiso);
        }

        public void AsociarFamiliaAPerfil(int idPerfil, int idFamilia)
        {
            DAL.AsociarFamiliaAPerfil(idPerfil, idFamilia);
        }
        public int CrearPerfil(string nombrePerfil)
        {
            return DAL.CrearPerfil(nombrePerfil);
        }
        public bool EliminarPerfil(int idPerfil)
        {
            return DAL.EliminarPerfil(idPerfil);
        }
        public bool EliminarUnaFamiliaDePerfil(int idPerfil, int idFamilia)
        {
            return DAL.EliminarUnaFamiliaDePerfil(idPerfil, idFamilia);
        }
        public bool EliminarUnPermisoDePerfil(int idPerfil, int idPermiso)
        {
            return DAL.EliminarUnPermisoDePerfil(idPerfil, idPermiso);
        }

    }
}
