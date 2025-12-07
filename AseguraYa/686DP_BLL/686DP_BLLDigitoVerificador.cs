using _686DP_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_MPP;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace _686DP_BLL
{
    public class _686DP_BLLDigitoVerificador
    {
        private static List<_686DP_DigitoVerificador> DVS = new List<_686DP_DigitoVerificador>();
        private static List<_686DP_DigitoVerificador> DVSBD = new List<_686DP_DigitoVerificador>();
        _686DP_MPPDigitoVerificador mpp = new _686DP_MPPDigitoVerificador();
        public static List<string> MppErrores = new List<string>();
        public static List<string> errores = new List<string>();
        public void CalcularDigitoVerificador(string NombreTabla)
        {
            _686DP_DigitoVerificador dv = null;
            if(NombreTabla == "Polizas")
            {
                dv = mpp.CalcularDVPolizas();
                mpp.ActualizarDVHFilaPorTabla("686DP_Poliza");
            }
            else if(NombreTabla == "Plan")
            {
                dv = mpp.CalcularDVPlan();
                mpp.ActualizarDVHFilaPorTabla("686DP_Plan");
            }
            else if( NombreTabla =="Cobertura")
            {
                dv = mpp.CalcularDVCobertura();
                mpp.ActualizarDVHFilaPorTabla("686DP_Cobertura");
            }
            else if (NombreTabla == "Seguro")
            {
                dv = mpp.CalcularDVSeguro();
                mpp.ActualizarDVHFilaPorTabla("686DP_Seguro");
            }
            else if (NombreTabla == "Cliente")
            {
                dv = mpp.CalcularDVCliente();
                mpp.ActualizarDVHFilaPorTabla("[686DP_Cliente].[686DP_Clientes]");
            }
            else if(NombreTabla == "Siniestro")
            {
                dv = mpp.CalcularDVSiniestro();
                mpp.ActualizarDVHFilaPorTabla("686DP_Siniestro");
            }
            else if(NombreTabla == "Factura")
            {
                dv = mpp.CalcularDVFactura();
                mpp.ActualizarDVHFilaPorTabla("686DP_Factura");
            }
            else
            {
                throw new Exception("Tabla no encontrada");
            }

            DVS.Add(mpp.CalcularPlanesCoberturas());
            DVS.Add(mpp.CalcularSeguroPlan());
            DVS.Add(mpp.CalcularClientePoliza());
            DVS.Add(mpp.CalcularPolizaSiniestro());
            DVS.Add(mpp.CalcularPolizaCancelacion());
            mpp.ActualizarDVHFilaPorTabla("686DP_PlanesCoberturas");
            mpp.ActualizarDVHFilaPorTabla("686DP_SeguroPlan");
            mpp.ActualizarDVHFilaPorTabla("686DPClientePoliza");
            mpp.ActualizarDVHFilaPorTabla("686DP_PolizaSiniestro");
            mpp.ActualizarDVHFilaPorTabla("686DPPolizaCancelacion");


            //Carga
            var existente = DVS.FirstOrDefault(x => x.DP686NombreTabla == dv.DP686NombreTabla);

            if (existente != null)
            {
                DVS.Remove(existente);
            }
            DVS.Add(dv);
            grabarTodosDV();
        }
        public void grabarTodosDV()
        {
            if(DVS.Count > 0)
            {
                foreach (_686DP_DigitoVerificador dv in  DVS)
                {
                    mpp.Grabar(dv);
                }
                DVS.Clear();
            }
        }

        public bool CalcularTodos()
        {
            try
            {
                DVS.Clear();
                DVS.Add(mpp.CalcularDVPolizas());
                DVS.Add(mpp.CalcularDVPlan());
                DVS.Add(mpp.CalcularDVCobertura());
                DVS.Add(mpp.CalcularDVSeguro());
                DVS.Add(mpp.CalcularDVCliente());
                DVS.Add(mpp.CalcularDVSiniestro());
                DVS.Add(mpp.CalcularDVFactura());
                DVS.Add(mpp.CalcularPlanesCoberturas());
                DVS.Add(mpp.CalcularSeguroPlan());
                DVS.Add(mpp.CalcularClientePoliza());
                DVS.Add(mpp.CalcularPolizaSiniestro());
                DVS.Add(mpp.CalcularPolizaCancelacion());
                mpp.RepararDVHDeTodasLasTablas();

                return Comparar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular/verificar los dígitos verificadores:\n" + ex.Message, ex);
            }
        }

        private bool Comparar()
        {
            MppErrores.Clear();
            bool valor = true;
            DVSBD = mpp.TraerDVs();

            if (DVS == null || DVS.Count == 0)
                throw new Exception("No hay dígitos verificadores calculados en memoria.");

            if (DVSBD == null || DVSBD.Count == 0)
                grabarTodosDV();

            foreach (var dvLocal in DVS)
            {
                var dvBD = DVSBD.FirstOrDefault(x => x.DP686NombreTabla == dvLocal.DP686NombreTabla);

                if (dvBD == null)
                {
                    throw new Exception($"❌ No se encontró en la base el registro de la tabla '{dvLocal.DP686NombreTabla}'.");
                }

                bool coincideDVH = dvLocal.DP686DVH == dvBD.DP686DVH;
                bool coincideDVV = dvLocal.DP686DVV == dvBD.DP686DVV;
                List<string> erorTabla = new List<string>();
                erorTabla.Clear();

                if (!coincideDVH || !coincideDVV)
                {
                    
                    errores.Add($"Inconsistencia detectada en '{dvLocal.DP686NombreTabla}'.\n");

                    erorTabla = mpp.VerificarFilaPorTabla(dvLocal.DP686NombreTabla);
                    valor = false;
                }
                foreach(string item in  erorTabla)
                {
                    MppErrores.Add(item);
                }
            }

            return valor;
        }
    }
}
