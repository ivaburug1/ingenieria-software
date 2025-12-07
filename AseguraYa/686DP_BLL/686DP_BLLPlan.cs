using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_MPP;

namespace _686DP_BLL
{
    public class _686DP_BLLPlan
    {
        _686DPBLLSeguro seg = new _686DPBLLSeguro();
        _686DP_MPPPlan mpp = new _686DP_MPPPlan();
        public List<_686DP_Plan> TraerPlanesFiltrado(string producto)
        {
            int codProducto = seg.ObtenerCodSeguroPorProducto(producto);
            List<_686DP_Plan> planes = mpp.TraerPlanesFiltrados(codProducto);
            return planes;
        }

        public void AsociarCoberturaAPlan(int codigoPlan, int codigoCobertura)
        {
            mpp.AsociarCobertura(codigoPlan, codigoCobertura);
        }

        public void AsociarPlanASeguro(int codigoPlan, int codSeguro)
        {
            mpp.AsociarPlanASeguro(codigoPlan, codSeguro);
        }

        public void CrearPlan(string producto, decimal franquicia, decimal prima)
        {
            int codigoPlan = mpp.CrearPlan(franquicia, prima);
            int codSeguro = seg.ObtenerCodSeguroPorProducto(producto);
            mpp.AsociarPlanASeguro(codigoPlan, codSeguro);
        }

        public List<_686DP_Plan> TraerPlanes()
        {
            List<_686DP_Plan> planes = mpp.TraerPlanes();
            return planes;
        }


        public bool YaExisteRelacionCoberturaPlan(int codigoPlanSeleccionado, int codigoCobertura)
        {
            return mpp.YaExisteRelacionCoberturaPlan(codigoPlanSeleccionado, codigoCobertura);
        }

        public _686DP_Plan TraerPlanPorID(int codplan)
        {
            _686DP_Plan plan = mpp.TraerPlanPorID(codplan);
            return plan;
        }
    }
}
