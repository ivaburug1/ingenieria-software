using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_SERVICIOS.Observer
{
    public interface _686DP_ISubject
    {
        void AgregarObsevador(_686DP_IObserver observer);
        void notificarObservador();
        void EliminarObservador(_686DP_IObserver observer);

    }
}
