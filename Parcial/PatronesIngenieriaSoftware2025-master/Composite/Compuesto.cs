using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public class Compuesto<T> : IComponente<T>
    {
        private List<IComponente<T>> elementos;

        public Compuesto(T nombre)
        {
            this.nombre = nombre;
            elementos = new List<IComponente<T>>();
        }


        public void Adicionar(IComponente<T> elemento)
        {
            elementos.Add(elemento);
        }

        public IComponente<T> Buscar(T elemento)
        {
            if (nombre.Equals(elemento))
            {
                return this;
            }

            IComponente<T> encontrado = null;

            foreach (IComponente<T> elementoItem in elementos)
            {
                encontrado = elementoItem.Buscar(elemento);
                if (encontrado != null)
                {
                    break;
                }
            }

            return encontrado;
        }

        public IComponente<T> Eliminar(T elemento)
        {
            IComponente<T> elementoBuscado = this.Buscar(elemento);

            if (elementoBuscado != null)
            {
                (this as Compuesto<T>).elementos.Remove(elementoBuscado);
            }

            return this;
        }

        public string mostrar(int profundidad)
        {
            StringBuilder infoElemento = new StringBuilder(new string('-', profundidad));

            infoElemento.Append($"Compuesto: {nombre} elementos: {elementos.Count}" + "\r\n");

            foreach (var elemento in elementos)
            {
                infoElemento.Append(elemento.mostrar(profundidad + 1));
            }

            return infoElemento.ToString();
        }

        public T nombre { get; set; }
    }
}
