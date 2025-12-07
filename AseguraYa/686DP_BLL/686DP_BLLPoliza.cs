using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_MPP;
using _686DP_BE;

namespace _686DP_BLL
{
    public class _686DP_BLLPoliza
    {
        _686DP_MPPPoliza mpp = new _686DP_MPPPoliza();
        _686DPMPPSeguro mpps = new _686DPMPPSeguro();
        _686DP_MPPCobertura mppc = new _686DP_MPPCobertura();
        public List<_686DP_Poliza> polizas = new List<_686DP_Poliza>();
        public int CrearPoliza(int codSeguro, decimal prima, int dNI, int codigoPlan)
        {
            decimal valorfinal = prima * 0.21m;
            DateTime fechaVencimiento = DateTime.Today.AddMonths(1);
            bool estado = true;
            int endoso = 0;

            int NumeroPoliza = mpp.CrearPoliza(estado, valorfinal, fechaVencimiento, endoso, codSeguro, codigoPlan);

            _686DP_Poliza poliza = new _686DP_Poliza(NumeroPoliza,estado,valorfinal,fechaVencimiento,endoso,codSeguro,codigoPlan);
            polizas.Add(poliza);

            mpp.AsociarClientePoliza(dNI, NumeroPoliza);
            return NumeroPoliza;
        }

        public bool BuscarPoliza(int numeroDePoliza)
        {
            bool existe = mpp.BuscarPoliza(numeroDePoliza);
            return existe;
        }

        public _686DP_Poliza traerDatosPoliza(int numeroDePoliza)
        {
            _686DP_Poliza poliza = mpp.TraerDatosPoliza(numeroDePoliza);
            _686DP_Seguro seguro = TraerSeguro(poliza.DP686_CodSeguro);
            _686DP_Plan Plan = TraerPlan(poliza.DP686_CodPlan);
            Plan.Coberturas = mppc.TraerCoberturasFiltrado(poliza.DP686_CodPlan);

            return poliza;
        }

        public _686DP_Seguro TraerSeguro(int dP686_CodSeguro)
        {
            _686DP_Seguro seguro = mpps.TraerDatosSeguro(dP686_CodSeguro);
            return seguro;
        }

        public _686DP_Plan TraerPlan(int dP686_CodPlan)
        {
            _686DP_Plan Plan = mpps.TraerPlan(dP686_CodPlan);
            return Plan;
        }

        public void ModificarPoliza(_686DP_Poliza poliza)
        {
            mpp.ModificarPoliza(poliza);
        }

        public void eliminarPoliza(string motivo, _686DP_Poliza poliza)
        {
            int npoliza = poliza.DP686_NPoliza;
            mpp.eliminarPoliza(motivo, npoliza);
        }

        public List<_686DP_Poliza> TraerPolizas()
        {
            return mpp.TraerPolizas();
        }

        public List<_686DP_Poliza> Filtrar(int? codpoliza, string seguro, decimal? prima, decimal? franquicia, bool? estado, DateTime? fecha)
        {
            return mpp.Filtrar(codpoliza, seguro, prima, franquicia, estado, fecha);
        }
    }
}
