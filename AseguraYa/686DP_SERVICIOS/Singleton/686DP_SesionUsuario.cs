
namespace _686DP_SERVICIOS.Singleton
{
    
    public class _686DP_SesionUsuario
    {
       
        public _686DP_Usuario _usuario { get; set; }

        
        public _686DP_Usuario Usuario
        {
            get
            {
                return _usuario;
            }
        }

        
        public void _686DPLogIN(_686DP_Usuario usuario)
        {
            _usuario = usuario;
        }

        
        public void _686DPLogOut()
        {
            _usuario = null;
        }

        
        public bool _686DPIsLogged()
        {
            return _usuario != null;
        }
    }
}
