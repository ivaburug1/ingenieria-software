using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_MPP;
using _686DP_BE;

namespace _686DP_BLL
{
    public class _686DPBLLSeguro
    {
        List<string> productos = new List<string>();
        _686DPMPPSeguro mpp = new _686DPMPPSeguro();

        public string buscarProducto(string producto)
        {
            int codproducto = mpp.ObtenerCodSeguroPorProducto(producto);
            _686DP_Seguro seguro = mpp.TraerDatosSeguro(codproducto);

            if (seguro == null)
                return "Producto no encontrado";

            return seguro.DP686_TipoProducto;
        }

        public void CrearProucto(string nProducto)
        {
            try
            {
                mpp.CrearProducto(nProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el producto: " + ex.Message, ex);
            }
            _686DP_Seguro producto = new _686DP_Seguro(nProducto);
            productos.Add(nProducto);
        }

       

        public int ObtenerCodSeguroPorProducto(string producto)
        {
            return mpp.ObtenerCodSeguroPorProducto(producto);
        }

        public List<_686DP_Seguro> Top3Productos()
        {
            List<_686DP_Seguro> Seguros = mpp.Top3Productos();
            return Seguros;
        }

        public List<string> TraerProductos()
        {
            productos = mpp.TraerProductos();
            return productos;
        }

        public _686DP_Seguro TraerProductosPorID(int codSeguro)
        {
            _686DP_Seguro seguro = mpp.TraerDatosSeguro(codSeguro);
            return seguro;
        }

        public bool VaidarProducto(string nProducto)
        {
            bool existe = false;
            try
            {
                existe = mpp.ValidarProducto(nProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el producto: " + ex.Message, ex);
            }
            return existe;
        }

        public bool YaExisteRelacionSeguroPlan(int codSeguro, int codigoPlan)
        {
            return mpp.YaExisteRelacionSeguroPlan(codSeguro, codigoPlan);
        }


    }
}
