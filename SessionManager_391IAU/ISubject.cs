using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionManager_391IAU
{
    public interface ISubject_391IAU
    {
        void AgregarObservador(IObserver_391IAU observer);
        void EliminarObservador(IObserver_391IAU observer);
        void NotificarObservadores();
    }
}
