using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Observer
{
    public class _686DP_Idioma: _686DP_ISubject
    {
        public string IdiomaActual { get; private set; }
        private List<_686DP_IObserver> observadores = new List<_686DP_IObserver>();

        public void AgregarObsevador(_686DP_IObserver observador)
        {
            if(!observadores.Contains(observador))
            {
                observadores.Add(observador);
            }
        }

        public bool ContieneObservador(_686DP_IObserver observador)
        {
            return observadores.Contains(observador);
        }

        public void EliminarObservador (_686DP_IObserver observador)
        {
            if(observadores.Contains(observador))
            {
                observadores.Remove(observador);
            }
        }

        public void notificarObservador()
        {
            foreach (var obs  in observadores)
            {
                try
                {
                    obs.ActualizarIdioma(IdiomaActual);
                }
                catch(Exception ex) 
                {
                    throw new Exception("error al notificar el observador:" + ex.Message);
                }
            }
        }

        public void CambiarIdioma(string nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;
            notificarObservador();
        }
    }
}
