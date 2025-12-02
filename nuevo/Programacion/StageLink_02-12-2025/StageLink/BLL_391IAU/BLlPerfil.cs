using BE_391IAU;
using DAL_391IAU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_391IAU
{
    public class BLLPerfil
    {
        public List<BEPerfil> TraerPerfiles()
        {
            try
            {
                return DAL.ObtenerPerfiles();
            }
            catch (Exception ex)
            {
                throw new Exception("Error obteniendo los perfiles: " + ex.Message);
            }
        }
    }
}
