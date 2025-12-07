using _686DP_SERVICIOS.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _686DP_MPP;
using System.Threading.Tasks;

namespace _686DP_BLL
{
    public class _686DP_BLLFamilia
    {
        _686DP_MPPFamilia mpp = new _686DP_MPPFamilia();
        _686DP_MPPPermisoSimple mppPermiso = new _686DP_MPPPermisoSimple();
        List<_686DP_Familia> Familias = new List<_686DP_Familia>();

        public int CrearFamilia(_686DP_Familia familia)
        {
            try
            {
                int familiaID = mpp.crearFamilia(familia);
                familia.idFamilia = familiaID;

                foreach (var componente in familia.ObtenerHijos())
                {
                    if (componente is _686DP_PermisoSimple permiso)
                    {
                        mpp.AsociarPermisoAFamilia(familiaID, permiso.DP686_PermisoSimpleID);
                    }
                    else if (componente is _686DP_Familia subfamilia)
                    {
                        if (subfamilia.idFamilia == familiaID)
                            throw new Exception("Una familia no puede contenerse a sí misma.");

                        mpp.ModificarProfundidad(familiaID);
                        mpp.AsociarSubfamiliaAFamilia(familiaID, subfamilia.idFamilia);
                    }
                }

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la familia: " + ex.Message);
                return 0;
            }
        }



        public void DesasignarFamiliaDesdeFamilia(_686DP_Familia fam, _686DP_Familia familia)
        {
            try
            {
                mpp.EliminarRelacionFamiliaFamilia(fam, familia.idFamilia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasignar familia del perfil: " + ex.Message);
            }
        }

        public void DesasignarPermisoDesdeFamilia(_686DP_Familia familia, _686DP_PermisoSimple permiso)
        {
            try
            {
                mpp.EliminarRelacionFamiliaPermiso(familia.idFamilia, permiso.DP686_PermisoSimpleID);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desasignar permiso de la familia: " + ex.Message);
            }
        }

        public void EliminarFamilia(_686DP_Familia familia)
        {
            try
            {
                mpp.EliminarFamiliaPorID(familia.idFamilia);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar familia: " + ex.Message);
            }
        }

        public List<_686DP_Familia> TraerFamilia()
        {
            List<_686DP_Familia> familias = mpp.TraerFamilia();

            foreach (_686DP_Familia familia in familias)
            {
                CargarComponentesRecursivos(familia);
                Familias.Add(familia);
            }

            return familias;
        }

        public _686DP_Familia TraerFamiliaPorID(int idFamilia)
        {
            var familia = mpp.TraerFamiliaPorID(idFamilia);
            var hijos = mpp.TraerHijosFamilia(idFamilia);

            foreach (_686DP_Composite hijo in hijos)
                familia.Agregar(hijo);

            return familia;
        }

        public bool validarUnico(Func<string> value)
        {
            bool unico = mpp.ValidarUnico(value);
            return unico;
        }

        private void CargarComponentesRecursivos(_686DP_Familia familia, HashSet<int> visitados = null)
        {
            if (visitados == null)
                visitados = new HashSet<int>();


            if (visitados.Contains(familia.idFamilia))
                return;

            visitados.Add(familia.idFamilia);

            List<_686DP_PermisoSimple> permisos = mppPermiso.TraerPermisoDeFamilia(familia.idFamilia);
            foreach (var permiso in permisos)
            {
                familia.Agregar(permiso);
            }

            List<_686DP_Familia> familiasHijas = mpp.TraerFamiliasDeFamilia(familia.idFamilia);
            foreach (var subFamilia in familiasHijas)
            {
                CargarComponentesRecursivos(subFamilia, visitados);
                familia.Agregar(subFamilia);
            }
        }
        private bool ContieneCiclo(int padreID, int posibleHijoID)
        {
            if (padreID == posibleHijoID)
                return true;

            List<_686DP_Familia> hijos = mpp.TraerFamiliasDeFamilia(posibleHijoID);
            foreach (var hijo in hijos)
            {
                if (ContieneCiclo(padreID, hijo.idFamilia))
                    return true;
            }

            return false;
        }
    }
}
