using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Composite
{
    public interface _686DP_Composite
    {
        string Nombre { get; }
        void Agregar(_686DP_Composite componente);
        void Quitar(_686DP_Composite componente);
        List<_686DP_Composite> ObtenerHijos();
    }
}
