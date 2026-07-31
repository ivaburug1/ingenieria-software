using System;
using System.Data;
using DAL_391IAU;

namespace BLL_391IAU
{
    public class BLLStock
    {
        public DataTable ListarStockProductos()
        {
            return DAL.ObtenerStockProductos();
        }

        public DataTable ObtenerNombresProductos()
        {
            return DAL.ObtenerNombresProductosVentaExtras();
        }

        public DataTable ObtenerTiposProductos()
        {
            return DAL.ObtenerTiposProductosVentaExtras();
        }

        public DataTable FiltrarStock(string nombreSeleccionado, string tipoSeleccionado)
        {
            string nombre = string.IsNullOrWhiteSpace(nombreSeleccionado) ? null : nombreSeleccionado;
            string tipo = string.IsNullOrWhiteSpace(tipoSeleccionado) ? null : tipoSeleccionado;

            return DAL.ObtenerStockProductosFiltrado(nombre, tipo);
        }
        public DataTable ListarProveedores()
        {
            return DAL.ObtenerProveedoresVentaExtras();
        }

        public DataTable ListarProductosPorProveedor(long proveedorId)
        {
            return DAL.ObtenerProductosPorProveedorVentaExtras(proveedorId);
        }
            
        public (int StockActual, int PrecioPallet) TraerDetalleProducto(int productoId)
        {
            return DAL.ObtenerStockYPrecioProductoVentaExtras(productoId);
        }

        public int ComprarProducto(long proveedorId, int productoId, int pallets)
        {
            if (proveedorId <= 0) throw new Exception("Proveedor inválido.");
            if (productoId <= 0) throw new Exception("Producto inválido.");
            if (pallets < 1 || pallets > 20) throw new Exception("Cantidad de pallets inválida.");

            return DAL.SumarStockProductoVentaExtras(productoId, pallets);
        }
    }
}