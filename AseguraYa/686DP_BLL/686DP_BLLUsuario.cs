using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_MPP;
using _686DP_SERVICIOS;
using static System.Net.Mime.MediaTypeNames;

namespace _686DP_BLL
{
    public class _686DP_BLLUsuario
    {
        _686DPMPPUsuarios mpp = new _686DPMPPUsuarios();
        public List<_686DP_Usuarios> ListaDeUsuarios { get; private set; }
        public List<string> lista { get; private set; }
        public _686DP_BLLUsuario()
        {
            ListaDeUsuarios = new List<_686DP_Usuarios>();
            lista = new List<string>();
        }

        public void _686DPBloquearUsuario(int dni)
        {
            try
            {
                mpp._686DPBloquearUsuario(dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al bloquear el usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al bloquear el usuario: " + ex.Message, ex);
            }
        }

        public object _686DPFiltrarGridFlexible(string rol, bool? activo, bool? bloqueado)
        {
            try
            {
                return mpp._686DPFiltrarUsuarios(rol, activo, bloqueado);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al filtrar usuarios: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al filtrar usuarios: " + ex.Message, ex);
            }
        }

        public _686DP_Usuario _686DPGenerarUsuarioSingleton(string nombreUsuario, string contraseña, int Dni, string idioma)
        {
            return new _686DP_Usuario
            {
                _686DPNombreUsuario = nombreUsuario,
                _686DPPassword = contraseña,
                _686DPDNI = Dni,
                _686DPIdioma = idioma
            };
        }

        public string _686DPTraerContraseña(int dni)
        {
            try
            {
                return mpp._686DPBuscarContraseña(dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer la contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer la contraseña: " + ex.Message, ex);
            }
        }

        public bool _686DPTraerEstado(int dni)
        {
            try
            {
                return mpp._686DPTraerEstado(dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer el estado del usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer el estado del usuario: " + ex.Message, ex);
            }
        }

        public List<string> _686DPtraerRoles()
        {
            return mpp._686DPTraerRoles();
        }

        public object _686DPTraerTodos()
        {
            try
            {
                ListaDeUsuarios = mpp._686DPTraerTodos();
                return ListaDeUsuarios;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer todos los usuarios: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer todos los usuarios: " + ex.Message, ex);
            }
        }

        public string _686DPTraerUsuario(int dni)
        {
            try
            {
                return mpp._686DPBuscarUsuario(dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer el usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer el usuario: " + ex.Message, ex);
            }
        }

        public void _686DPActualizarUsuarioExistente(_686DP_Usuarios emp)
        {
            try
            {
                mpp._686DPActualizarUsuarioExistente(emp);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al actualizar el Usuario: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al actualizar el Usuario: " + ex.Message, ex);
            }
        }

        public void _686DPVerificarContraseñas(int Dni)
        {
            try
            {
                lista = mpp._686DPVerificarContraseñas(Dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al verificar contraseñas: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al verificar contraseñas: " + ex.Message, ex);
            }
        }

        public bool _686DPCompararContraseñas(string nuevaContraseña, string contraseñaActual, int dni)
        {
            try
            {
                foreach (string anterior in lista)
                {
                    if (nuevaContraseña == anterior)
                        throw new Exception("La nueva contraseña no puede ser igual a una ya utilizada anteriormente.");
                }

                mpp._686DPGrabarContraseñaNueva(nuevaContraseña, dni);
                mpp._686DPAgregarALita(contraseñaActual, dni);

                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al comparar o guardar la contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al validar o guardar la contraseña: " + ex.Message, ex);
            }
        }

        public string TraerRol(int DNI)
        {
            try
            {
                return mpp._686DPTraerRol(DNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer el rol: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al obtener el rol: " + ex.Message, ex);
            }
        }

        public void GuardarContraseña(string contraseñaAnterior, int dni)
        {
            try
            {
                mpp._686DPGrabarContraseñaNueva(contraseñaAnterior, dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al guardar la nueva contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar la nueva contraseña: " + ex.Message, ex);
            }
        }

        public void RegistrarError(int dNI)
        {
            try
            {
                mpp._686DPAgregarIntento(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al registrar intento: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al registrar intento: " + ex.Message, ex);
            }
        }

        public int _686DPTraerIntentos(int dNI)
        {
            try
            {
                return mpp._686DPTraerIntentos(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al traer intentos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al traer intentos: " + ex.Message, ex);
            }
        }

        public bool _686DPCambiarContraseña(int dNI)
        {
            try
            {
                return mpp._686DPCambiarcontraseña(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al consultar cambio de contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al consultar cambio de contraseña: " + ex.Message, ex);
            }
        }

        public void _686DPReestablecerIntentos(int dNI)
        {
            try
            {
                mpp._686DPReestablecerIntentos(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al reestablecer intentos: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al reestablecer intentos: " + ex.Message, ex);
            }
        }

        public bool _686DPCuentaBloqueada(int dNI)
        {
            try
            {
                return mpp._686DPCuentaBloqueada(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al verificar cuenta bloqueada: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al verificar cuenta bloqueada: " + ex.Message, ex);
            }
        }

        public void _Cambiarcontraobligatorio(int dni)
        {
            try
            {
                mpp._686DPCambiarcontraseñaObligatori(dni);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al forzar cambio de contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al forzar cambio de contraseña: " + ex.Message, ex);
            }
        }

        public void _686DPNuevaContra(string nuevacontra, int dNI)
        {
            try
            {
                mpp._686DPGrabarContraseñaNueva(nuevacontra, dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al guardar nueva contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al guardar nueva contraseña: " + ex.Message, ex);
            }
        }

        public void _ReestablecerObligatoriedadeContraseña(int dNI)
        {
            try
            {
                mpp._ReestablecerObligatoriedadeContraseña(dNI);
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al forzar cambio de contraseña: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al forzar cambio de contraseña: " + ex.Message, ex);
            }
        }

        public string TraerIdiomaUsuario(int dNI)
        {
            return mpp.TraerIdiomaUsuario(dNI);
        }

        public void GuardarIdioma(string _686DPIdioma)
        {
            mpp.GuardarIdioma(_686DPIdioma);
        }

        public _686DP_Usuarios TraerUsuarioCompleto(int dni)
        {
            return mpp.TraerUsuarioCompleto(dni);
        }
    }
}
