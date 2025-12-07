using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_MPP;
using _686DP_BE;
using System.Net;

namespace _686DP_BLL
{
    public class _686DP_BLLEvento
    {
        private _686DP_MPPEvento mpp = new _686DP_MPPEvento();
        private int ultimoContador = 0;

        public List<_686DP_Evento> Filtrar(DateTime? fechaDesde, DateTime? fechaHasta, string modulo, int? criticidad, int? dni)
        {
            return mpp.Filtrar(fechaDesde, fechaHasta, modulo, criticidad, dni);
        }

        public void RegistrarEvento(int dNI, string modulo, string descripcion, int criticidad)
        {
            DateTime fecha = DateTime.Now;
            DateTime fechaUltimoRegistro = TraerUltimoRegistro();
            if (fecha.Date != fechaUltimoRegistro.Date)
            {
                ultimoContador = 1;
            }
            else
            {
                ultimoContador++;
            }
            string codEvento = $"{fecha:yyyyMMdd}_{ultimoContador:D4}";

            _686DP_Evento evento = new _686DP_Evento(dNI, codEvento, fecha, modulo, descripcion, criticidad);
            mpp.RegistrarEvento(evento);

        }

        public List<_686DP_Evento> TraerEventos()
        {
            return mpp.traerEventos();
        }

        private DateTime TraerUltimoRegistro()
        {
            _686DP_Evento e = mpp.TraerUltimoEvento();
            if (e != null)
            {
                string[] partes = e.DP686_CodEvento.Split('_');
                if (partes.Length > 1 && int.TryParse(partes[1], out int contador))
                {
                    ultimoContador = contador;
                }
                return e.DP686_Fecha;
            }
            return DateTime.MinValue; 
        }
    }
}
