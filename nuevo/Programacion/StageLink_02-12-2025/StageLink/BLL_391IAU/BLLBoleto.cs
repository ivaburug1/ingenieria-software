using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using DAL_391IAU;
using System.Data.SqlClient;


namespace BLL_391IAU
{
    public class BLLBoleto
    {
        public bool RegistrarCompra(string artista, DateTime fecha, int sector, decimal precio, int cantidad, int dniCliente, int codigDeEvento)
        {
            List<BEBoleto> lista = new List<BEBoleto>();

            for (int i = 0; i < cantidad; i++)
            {
                BEBoleto b = new BEBoleto
                {
                    Artista = artista,
                    FechaEvento = fecha,
                    Sector = sector,
                    Precio = precio,
                    DNICliente = dniCliente,
                    CodigoDeEvento = codigDeEvento
                };
                lista.Add(b);
            }

            return DAL.GuardarEntradasYRelaciones(lista);
        }
    }
}
