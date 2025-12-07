using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DP_Poliza
    {
        public int DP686_NPoliza { get; set; }
        public bool DP686_Estado { get; set; }
        public decimal DP686_valorTotal { get; set; }
        public DateTime DP686_FechaVencimiento { get; set; }
        public int DP686_Endoso { get; set; }
        public int DP686_CodSeguro { get; set; }
        public int DP686_CodPlan { get; set; }



        public _686DP_Poliza(int nPoliza, bool estado, decimal valorTotal, DateTime fechaVencimiento, int endoso, int codSeguro, int codPlan)
        {
            DP686_NPoliza = nPoliza;
            DP686_Estado = estado;
            DP686_valorTotal = valorTotal;
            DP686_FechaVencimiento = fechaVencimiento;
            DP686_Endoso = endoso;
            DP686_CodSeguro = codSeguro;
            DP686_CodPlan = codPlan;
        }
    }
}
