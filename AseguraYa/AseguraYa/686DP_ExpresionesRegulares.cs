using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AseguraYa
{
    public class _686DP_ExpresionesRegulares
    {
        public bool _686DPEsNumero(string texto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(texto))
                    return false;

                if (!Regex.IsMatch(texto, @"^\d+$"))
                    return false;

                if (!int.TryParse(texto, out _))
                    throw new Exception("El número es demasiado grande. Máximo permitido: 2.147.483.647.");

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Validación fallida: " + ex.Message);
            }
        }
        public bool _686DPEsEmail(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            try
            {
                return Regex.IsMatch(texto, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                throw new Exception("La validación del email excedió el tiempo permitido.");
            }
        }

        public bool _686DPEsCuitCuil(string texto)
        {
            return Regex.IsMatch(texto, @"^\d{11}$");
        }

        public bool _686DPEsTarjetaCredito(string texto)
        {
            return Regex.IsMatch(texto, @"^\d{13,19}$");
        }

        public bool _686DPEsSoloLetras(string texto)
        {
            try
            {
                if (string.IsNullOrEmpty(texto)) return false;
                return Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
            }
            catch (Exception)
            {
                throw new Exception("Ingresar solo letras.");
            }
        }

    }
}
