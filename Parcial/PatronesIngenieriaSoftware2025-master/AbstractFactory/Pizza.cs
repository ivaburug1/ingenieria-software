using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public class Pizza
    {
        protected string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
        }
        public override string ToString()
        {
            return $"Pizza:{_descripcion}";
        }
    }
}
