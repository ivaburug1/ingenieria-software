using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Plan
    {
        public int DP686_CodigoPlan { get; set; }
        public decimal DP686_Franquicia { get; set; }

        public decimal DP686_Prima { get; set; }

        public _686DP_Plan(int codPlan, decimal franquicia, decimal prima)
        {
            DP686_CodigoPlan = codPlan;
            DP686_Franquicia = franquicia;
            DP686_Prima = prima;
        }
        public List<_686DP_Cobertura> Coberturas { get; set; } = new List<_686DP_Cobertura>();
    }
}
