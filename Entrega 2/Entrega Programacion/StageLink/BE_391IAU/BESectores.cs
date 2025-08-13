using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class BESector
{
    public int Codigo { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }

    public BESector(int codigo, string nombre, decimal precio)
    {
        Codigo = codigo;
        Nombre = nombre;
        Precio = precio;
    }
}
