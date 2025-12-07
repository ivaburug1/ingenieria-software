using _686DP_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _686DP_MPP;
using System.Threading.Tasks;

namespace _686DP_BLL
{
    public class _686DPBLLClienteC
    {
        _686DPMPPCliente_C mpp = new _686DPMPPCliente_C();

        public void ActualizarClienteC(_686DPCliente_C duplicadoActivo)
        {
            mpp.ActualizarClienteC(duplicadoActivo);
        }

        public List<_686DPCliente_C> TraerCambios()
        {
            return mpp.TraerCambios();
        }
    }
}
