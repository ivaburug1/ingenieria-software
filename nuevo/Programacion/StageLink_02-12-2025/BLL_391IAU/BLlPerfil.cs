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
        public BEPerfil TraerPerfil(int idPerfil)
        {
            return DAL.ObtenerPerfilPorID(idPerfil);
        }

        public List<BEPermiso> ObtenerPermisosSimples(int idPerfil)
        {
            return DAL.ObtenerPermisosDePerfil(idPerfil);
        }

        public List<BEPerfil> TraerPerfiles()
        {
            return DAL.ObtenerPerfiles();
        }
    }
}