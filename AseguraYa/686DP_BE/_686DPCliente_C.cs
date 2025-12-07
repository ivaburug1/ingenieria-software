using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _686DP_BE
{
    public class _686DPCliente_C
    {
        public int ID { get; set; }                            
        public bool DP686_Estado { get; set; }                 
        public int DP686_DNI { get; set; }                     
        public string DP686_Nombre { get; set; }               
        public string DP686_Apellido { get; set; }             
        public string DP686_Email { get; set; }                
        public string DP686_Domicilio { get; set; }           
        public int DP686DP_CodigoPostal { get; set; }          
        public DateTime DP686_Fecha { get; set; }              
        public bool DP686_Activo { get; set; }                 
        public _686DPCliente_C() { }

        public _686DPCliente_C(
            int id,
            bool estado,
            int dni,
            string nombre,
            string apellido,
            string email,
            string domicilio,
            int codigoPostal,
            DateTime fecha,
            bool activo)
        {
            ID = id;
            DP686_Estado = estado;
            DP686_DNI = dni;
            DP686_Nombre = nombre;
            DP686_Apellido = apellido;
            DP686_Email = email;
            DP686_Domicilio = domicilio;
            DP686DP_CodigoPostal = codigoPostal;
            DP686_Fecha = fecha;
            DP686_Activo = activo;
        }
    }
}
