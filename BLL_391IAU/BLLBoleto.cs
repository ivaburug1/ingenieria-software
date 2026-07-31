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

            int capacidad = DAL.ObtenerCapacidadPorCodigoSector(sector);
            int vendidas = DAL.ObtenerEntradasVendidas(codigDeEvento, sector);
            int disponibles = Math.Max(0, capacidad - vendidas);
            if (cantidad > disponibles)
                throw new InvalidOperationException($"Solo quedan {disponibles} entradas disponibles para ese sector.");

            return DAL.GuardarEntradasYRelaciones(lista);
        }
    }
}
