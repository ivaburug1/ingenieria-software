using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_MPP;


namespace _686DP_BLL
{
    public class _686DP_BLLCobertura
    {
        _686DP_MPPCobertura mpp = new _686DP_MPPCobertura();

        public List<_686DP_Cobertura> traerCoberturas()
        {
            List<_686DP_Cobertura> coberturas = mpp.TraerCoberturas();
            return coberturas;
        }

        public List<_686DP_Cobertura> TraerCoberturasFiltrado(int codigoPlan)
        {
            List<_686DP_Cobertura> coberturas = mpp.TraerCoberturasFiltrado(codigoPlan);
            return coberturas;
        }

        public bool ExisteCoberturaEnPlan(int codPlan, string descripcion, decimal suma)
        {
            return mpp.ExisteCoberturaEnPlan(codPlan, descripcion, suma);
        }

        public int CrearCobertura(string descripcion, decimal suma)
        {
            int codCobertura = mpp.CrearCobertura(descripcion, suma);
            return codCobertura;
        }
    }
}
