using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Singleton
{
    public class _686DPExcepcionesLogIn:Exception
    {
        public _686DPResultadosLogIn Result;
        public _686DPExcepcionesLogIn(_686DPResultadosLogIn result)
        {
            Result = result;
        }
    }
}
