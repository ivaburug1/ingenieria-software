using Servicios_391IAU.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLFamilia_391IAU
    {
        public List<Familia_391IAU> TraerTodasLasFamilias()
        {
            return DAL.ObtenerTodasLasFamilias();
        }

        public List<Familia_391IAU> TraerSubfamilias(int idFamilia)
        {
            var ids = DAL.ObtenerSubfamilias(idFamilia);
            var lista = new List<Familia_391IAU>();

            foreach (var id in ids)
            {
                var nombre = DAL.ObtenerNombreFamilia(id);
                lista.Add(new Familia_391IAU(id, nombre));
            }

            return lista;
        }

        public List<PermisoSimple_391IAU> TraerPermisosDeFamilia(int idFamilia)
        {
            var listaBE = DAL.ObtenerPermisosDeFamilia(idFamilia);
            var listaFinal = new List<PermisoSimple_391IAU>();

            foreach (var p in listaBE)
            {
                listaFinal.Add(new PermisoSimple_391IAU(
                    p.IDPermiso_391IAU,
                    p.NombrePermiso_391IAU
                ));
            }

            return listaFinal;
        }
        public void AsociarPermisoAFamilia(int idFamilia, int idPermiso)
        {
            DAL.AsociarPermisoAFamilia(idFamilia, idPermiso);
        }
        public void EliminarPermisosDeFamilia(int idFamilia)
        {
            DAL.EliminarPermisosDeFamilia(idFamilia);
        }
        public int CrearFamilia(string nombre)
        {
            return DAL.CrearFamilia(nombre);
        }
        public bool EliminarFamiliaCompleta(int idFamilia)
        {
            try
            {
                DAL.EliminarPermisosDeFamilia(idFamilia);
                DAL.EliminarFamiliaDeTodosLosPerfiles(idFamilia);
                DAL.EliminarFamilia(idFamilia);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool EliminarRelacionFamiliaSubfamilia(int idPadre, int idHija)
        {
            return DAL.EliminarRelacionFamiliaSubfamilia(idPadre, idHija);
        }
        public bool EliminarPermisoDeFamilia(int idFamilia, int idPermiso)
        {
            return DAL.EliminarPermisoDeFamilia(idFamilia, idPermiso);
        }

    }
}
