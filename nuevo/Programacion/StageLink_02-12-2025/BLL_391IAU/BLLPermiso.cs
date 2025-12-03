using DAL_391IAU;
using BE_391IAU;
using System.Collections.Generic;

namespace BLL_391IAU
{
    public class BLLPermiso
    {
        public List<BEPermiso> TraerPermisosSimples()
        {
            return DAL.ObtenerTodosLosPermisosSimples();
        }

        public BEPermiso TraerPermiso(int id)
        {
            return DAL.ObtenerPermisoPorID(id);
        }
    }
}
