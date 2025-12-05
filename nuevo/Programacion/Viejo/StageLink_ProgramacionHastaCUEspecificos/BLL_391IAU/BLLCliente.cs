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
    public class BLLCliente
    {
        public bool InsertarCliente(BECliente cliente)
        {
            try
            {
                string query = "INSERT INTO Clientes_391IAU (DNI_391IAU, Nombre_391IAU, Apellido_391IAU, Correo_391IAU) " +
                               "VALUES (@DNI, @Nombre, @Apellido, @Correo)";

                SqlParameter[] parametros =
                {
                    new SqlParameter("@DNI", cliente.DNI_391IAU),
                    new SqlParameter("@Nombre", cliente.Nombre_391IAU),
                    new SqlParameter("@Apellido", cliente.Apellido_391IAU),
                    new SqlParameter("@Correo", cliente.Correo_391IAU)
                };

                return DAL.EjecutarInsert(query, parametros) > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool ExisteClientePorDNI(int dni)
        {
            string query = "SELECT COUNT(*) FROM Clientes_391IAU WHERE DNI_391IAU = @DNI";
            SqlParameter[] parametros = { new SqlParameter("@DNI", dni) };
            int cantidad = DAL.EjecutarScalar(query, parametros);
            return cantidad > 0;
        }
    }
}
