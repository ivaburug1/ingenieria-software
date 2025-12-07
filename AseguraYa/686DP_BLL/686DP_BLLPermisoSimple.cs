using _686DP_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using _686DP_MPP;
using System.Text;
using System.Threading.Tasks;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Composite;

namespace _686DP_BLL
{
    public class _686DP_BLLPermisoSimple
    {
        _686DP_MPPPermisoSimple mpp = new _686DP_MPPPermisoSimple();
        public List<_686DP_PermisoSimple> TraerPermisos()
        {
            return mpp.TraerPermisos();
        }
    }
}
