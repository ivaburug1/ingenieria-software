using DAL_391IAU;
using BE_391IAU;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL_391IAU
{
    public class ProductoAltaProveedor
    {
        public int IdProducto { get; set; } // 0 si es nuevo
        public string Nombre { get; set; }
        public int PrecioPallet { get; set; }
        public string TipoProducto { get; set; }
    }

    public class BLLProveedor
    {
        private static readonly HashSet<string> TIPOS_VALIDOS = new HashSet<string>
        {
            "Bebida",
            "Comida",
            "Golosinas",
            "Refrigerados (Helados)",
            "Otros"
        };

        public int RegistrarProveedorConProductos(string cuit, string nombre, string correo, List<ProductoAltaProveedor> productos)
        {
            if (string.IsNullOrWhiteSpace(cuit) ||
                string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrWhiteSpace(correo))
                throw new Exception("No se puede registrar el Proveedor porque sus campos no estan completos.");

            if (productos == null || productos.Count == 0)
                throw new Exception("No se puede agregar un proveedor nuevo sin ningun producto asociado.");

            if (!long.TryParse(cuit, out long cuitNum) || cuitNum <= 0)
                throw new Exception("El CUIT ingresado no es válido.");

            if (!correo.Contains("@") || !correo.Contains("."))
                throw new Exception("El correo ingresado no es válido.");

            foreach (var p in productos)
            {
                if (string.IsNullOrWhiteSpace(p.Nombre) || p.PrecioPallet <= 0 || string.IsNullOrWhiteSpace(p.TipoProducto))
                    throw new Exception("Completar todos los campos del DataGridView");

                if (!TIPOS_VALIDOS.Contains(p.TipoProducto.Trim()))
                    throw new Exception("Tipo de producto inválido.");
            }

            var productosDal = productos.Select(p =>
                (NombreProducto: p.Nombre.Trim(),
                 Precio: p.PrecioPallet,
                 TipoProducto: p.TipoProducto.Trim())
            ).ToList();

            return DAL.RegistrarProveedorConProductosVentaExtras(
                cuitNum,
                nombre.Trim(),
                correo.Trim(),
                productosDal
            );
        }

        public DataTable ListarProveedores()
        {
            return DAL.ObtenerProveedoresVentaExtras();
        }

        public BEProveedorEdicion TraerProveedorPorCuit(long cuit)
        {
            return DAL.ObtenerProveedorPorCuitVentaExtras(cuit);
        }

        public List<BEProductoProveedorEdicion> ListarProductosDeProveedor(long cuit)
        {
            return DAL.ObtenerProductosDeProveedorVentaExtras(cuit);
        }

        public void ActualizarProveedorConProductos(long cuit, string nombre, string correo, List<ProductoAltaProveedor> productos)
        {
            if (cuit <= 0) throw new Exception("CUIT inválido.");

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo))
                throw new Exception("No se puede actualizar el Proveedor porque sus campos no estan completos.");

            if (productos == null || productos.Count == 0)
                throw new Exception("No se puede dejar un proveedor sin ningun producto asociado.");

            if (!correo.Contains("@") || !correo.Contains("."))
                throw new Exception("El correo ingresado no es válido.");

            foreach (var p in productos)
            {
                if (string.IsNullOrWhiteSpace(p.Nombre) || p.PrecioPallet <= 0 || string.IsNullOrWhiteSpace(p.TipoProducto))
                    throw new Exception("Completar todos los campos del DataGridView");

                if (!TIPOS_VALIDOS.Contains(p.TipoProducto.Trim()))
                    throw new Exception("Tipo de producto inválido.");
            }

            var productosDal = productos.Select(p => new DAL.ProductoProveedorUpsert
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre.Trim(),
                Precio = p.PrecioPallet,
                TipoProducto = p.TipoProducto.Trim()
            }).ToList();

            DAL.ActualizarProveedorConProductosVentaExtras(cuit, nombre.Trim(), correo.Trim(), productosDal);
        }
    }
}