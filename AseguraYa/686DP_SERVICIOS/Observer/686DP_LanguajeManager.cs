using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using Newtonsoft.Json;



namespace _686DP_SERVICIOS.Observer
{
    public class _686DP_LanguajeManager: _686DP_IObserver
    {
        public List<Form> Forms = new List<Form>();
        private Dictionary<string, string> Diccionario;
        private Dictionary<string, string> _mensajesGlobales;

        public _686DP_LanguajeManager()
        {
            Diccionario = new Dictionary<string, string>();
            _mensajesGlobales = new Dictionary<string, string>();
        }
        public void RegistrarForm(Form form)
        {
            Forms.Add(form);
        }

        public void ActualizarIdioma(string idioma)
        {
            foreach (var formulario in Forms)
            {
                string nombreform = formulario.Name;
                string ArchivoIdioma = $"{nombreform}_{idioma}.json";
                string RutaArchivo = Path.Combine(Application.StartupPath, ArchivoIdioma);

                if(File.Exists(RutaArchivo))
                {
                    var traducciones = LeerTraduccionesDesdeJSON(RutaArchivo);
                    CargarIdiomaControles(formulario, traducciones);
                }
                else
                {
                    MessageBox.Show(Traducir("ArchivoNoEncontrado") + RutaArchivo);
                }
            }
        }

        private void CargarIdiomaControles(Form formulario, Dictionary<string, string> traducciones)
        {
            try
            {

                foreach (Control control in formulario.Controls)
                {
                    if (control != null && control.Tag != null)
                    {
                        string clave = control.Tag.ToString();
                        if (traducciones.ContainsKey(clave))
                        {
                            control.Text = traducciones[clave];
                        }
                    }

                    if (control is MenuStrip menuStrip)
                    {
                        foreach (ToolStripItem item in menuStrip.Items)
                        {
                            ActualizarIdiomaMenuStripItems(item, traducciones);
                        }
                    }
                    if (control.Controls.Count > 0)
                    {
                        CargarIdiomaControlesRecursivo(control, traducciones);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Traducir("ErrorTraduccionControles") + ex.Message);
            }
        }

        private void CargarIdiomaControlesRecursivo(Control parent, Dictionary<string, string> traducciones)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Tag != null && traducciones.ContainsKey(control.Tag.ToString()))
                {
                    control.Text = traducciones[control.Tag.ToString()];
                }

                if (control is MenuStrip menuStrip)
                {
                    foreach (ToolStripItem item in menuStrip.Items)
                    {
                        ActualizarIdiomaMenuStripItems(item, traducciones);
                    }
                }

                if (control.Controls.Count > 0)
                {
                    CargarIdiomaControlesRecursivo(control, traducciones); 
                }
            }
        }

        private void ActualizarIdiomaMenuStripItems(ToolStripItem item, Dictionary<string, string> traducciones)
        {
            if (item.Tag != null && traducciones.ContainsKey(item.Tag.ToString()))
            { 
                item.Text = traducciones[item.Tag.ToString()]; 
            }

            if(item is ToolStripMenuItem menuItem)
            {
                foreach(ToolStripMenuItem subItem in menuItem.DropDownItems)
                {
                    ActualizarIdiomaMenuStripItems(subItem, traducciones);
                }
            }
        }

        private Dictionary<string, string> LeerTraduccionesDesdeJSON(string rutaArchivo)
        {
            try
            {
                string json = File.ReadAllText(rutaArchivo);
                var diccionario = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                return diccionario ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Traducir("ErrorDeJSON") + ex.Message);
                return new Dictionary<string, string>();
            }
        }

        public void CargarMensajesGlobales(string idioma)
        { 
            string archivo = $"Mensajes_{idioma}.json";
            string ruta = Path.Combine(Application.StartupPath, archivo);

            if (File.Exists(ruta))
            {
                _mensajesGlobales = LeerTraduccionesDesdeJSON(ruta);
            }
            else
                _mensajesGlobales = new Dictionary<string, string>();
        }

        public string Traducir(string clave)
        {
            if (_mensajesGlobales.ContainsKey(clave))
                return _mensajesGlobales[clave];
            return $"[{clave}]";
        }
        public string ObtenerClaveDesdeValor(string valorTraducido)
        {
            foreach (var par in _mensajesGlobales)
            {
                if (par.Value == valorTraducido)
                    return par.Key;
            }
            return null; 
        }

    }
}
