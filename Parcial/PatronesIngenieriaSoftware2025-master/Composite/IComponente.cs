using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public interface IComponente<T>
    {
        void Adicionar(IComponente<T> elemento);
        IComponente<T> Eliminar(T elemento);
        IComponente<T> Buscar(T elemento);
        string mostrar(int profundidad);

        T nombre { get; set; }
    }
}
