using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class SessionManager
    {
        private static object _lock = new Object();
        private static SessionManager _session;

        public Usuario Usuario { get; set; }
        public DateTime FechaInicio { get; set; }

        public static SessionManager GetInstance
        {
            get
            {
                if (_session == null) throw new Exception("Sesión no iniciada");
                return _session;
            }
        }

        public static void Login(Usuario usuario)
        {

            lock (_lock)
            {
                if (_session == null)
                {
                    _session = new SessionManager();
                    _session.Usuario = usuario;
                    _session.FechaInicio = DateTime.Now;
                    Console.WriteLine($"{_session.FechaInicio} - Se inicio la sesión de {_session.Usuario.Nombre}");
                }
                else
                {
                    throw new Exception("Sesión ya iniciada");
                }
            }
        }

        public static void Logout()
        {
            lock (_lock)
            {
                if (_session != null)
                {
                    _session = null;
                    Console.WriteLine($"{DateTime.Now} - Se cerro sesión");
                }
                else
                {
                    throw new Exception("Sesión no iniciada");
                }
            }
        }
    }

}
