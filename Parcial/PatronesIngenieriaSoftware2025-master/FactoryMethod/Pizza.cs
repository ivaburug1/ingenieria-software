using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    public abstract class Pizza
    {
        protected string _descripcion;
        protected string _origen;

        public override string ToString()
        {
            return string.Format("- Tipo '{0}' hecha en {1}", _descripcion, _origen);
        }
    }
}
