using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE_391IAU;
using DAL_391IAU;
using System.IO;
using System.Xml.Serialization;

namespace BLL_391IAU
{
    public class BLLCliente
    {
        public bool InsertarCliente(BECliente cliente)
        {
            try
            {
                return DAL.InsertarCliente(cliente);
            }
            catch
            {
                return false;
            }
        }
        public static bool ExisteClientePorDNI(int dni)
        {
            return DAL.ExisteClientePorDNI(dni);
        }
        public List<BECliente> ObtenerTodosLosClientes()
        {
            return DAL.ObtenerTodosLosClientes();
        }

        public bool ModificarCliente(BECliente cli)
        {
            return DAL.ModificarCliente(cli);
        }
        public bool SerializarClientesXml(List<BECliente> clientes, string filePath)
        {
            try
            {
                if (clientes == null || clientes.Count == 0)
                    throw new Exception("No hay clientes para serializar.");

                if (string.IsNullOrWhiteSpace(filePath))
                    throw new Exception("Ruta inválida.");

                if (!filePath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    filePath += ".xml";

                XmlSerializer serializer = new XmlSerializer(typeof(List<BECliente>));

                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, clientes);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<BECliente> DeserializarClientesXml(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new Exception("Ruta inválida.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("No se encontró el archivo XML.", filePath);

            XmlSerializer serializer = new XmlSerializer(typeof(List<BECliente>));

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var obj = serializer.Deserialize(fs);
                return obj as List<BECliente> ?? new List<BECliente>();
            }
        }
    }
}
