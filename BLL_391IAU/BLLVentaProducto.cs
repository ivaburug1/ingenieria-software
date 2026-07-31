using DAL_391IAU;
using System;
using System.Data;

namespace BLL_391IAU
{
    public class BLLVentaProducto
    {
        public DataTable ListarProductosParaVenta()
        {
            return DAL.ObtenerProductosVentaExtrasParaCombo();
        }

        public DataTable ListarEventos()
        {
            return DAL.ObtenerEventosParaCombo();
        }

        public void VenderProductoAEvento(int idProducto, int codigoEvento, int cantidad)
        {
            if (idProducto <= 0) throw new Exception("Debe seleccionar un producto.");
            if (codigoEvento <= 0) throw new Exception("Debe seleccionar un evento.");
            if (cantidad <= 0) throw new Exception("La cantidad a vender debe ser mayor a 0.");

            DAL.VenderProductoAEventoVentaExtras(idProducto, codigoEvento, cantidad);
        }
    }
}