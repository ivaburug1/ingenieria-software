using System;
using System.Data;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLProductosCambio
    {
        public DataTable ListarCambios()
        {
            return DAL.ObtenerProductosCambios(null, null, null);
        }

        public DataTable FiltrarCambios(DateTime? fechaDesde, DateTime? fechaHasta, string nombre)
        {
            string nombreFiltro = string.IsNullOrWhiteSpace(nombre) ? null : nombre.Trim();
            return DAL.ObtenerProductosCambios(fechaDesde, fechaHasta, nombreFiltro);
        }

        public bool ExisteOtroActivoMismoId(int idProducto, DateTime fechaSel, string nombreSel, string stockSel, int precioSel, string tipoSel)
        {
            return DAL.ExisteActivoDistintoAlSeleccionado(idProducto, fechaSel, nombreSel, stockSel, precioSel, tipoSel);
        }

        public void ActivarProductoDesdeBitacora(int idProducto, DateTime fechaSel, string nombreSel, string stockSel, int precioSel, string tipoSel)
        {
            if (idProducto <= 0) throw new Exception("Producto inválido.");

            DAL.ActivarVersionProductoDesdeBitacora(
                idProducto,
                fechaSel,
                nombreSel?.Trim(),
                stockSel?.Trim(),
                precioSel,
                tipoSel?.Trim()
            );
        }
    }
}