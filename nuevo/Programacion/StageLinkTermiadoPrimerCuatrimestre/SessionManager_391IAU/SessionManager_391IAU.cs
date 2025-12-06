using BE_391IAU;
using Servicios_391IAU.Composite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SessionManager_391IAU
{
    public partial class SessionManager_391IAU : ISubject_391IAU
    {

        private static SessionManager_391IAU instancia;
        private static readonly object candado = new object();
        public BEUsuario UsuarioActual { get; private set; }

        private SessionManager_391IAU() { }

        public static SessionManager_391IAU ObtenerInstancia()
        {
            if (instancia == null)
            {
                lock (candado)
                {
                    if (instancia == null)
                        instancia = new SessionManager_391IAU();
                }
            }
            return instancia;
        }
        private Dictionary<string, string> _mensajesGlobales = new Dictionary<string, string>();
        public void CargarMensajes(string idioma)
        {
            string archivo = $"{idioma}_Mensajes.json";

            string ruta = Path.Combine(
                Application.StartupPath,
                "Idiomas",
                idioma,
                archivo
            );

            if (File.Exists(ruta))
            {
                string json = File.ReadAllText(ruta);
                _mensajesGlobales = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                                   ?? new Dictionary<string, string>();
            }
            else
            {
                MessageBox.Show($"No se encontró el archivo de mensajes: {ruta}");
                _mensajesGlobales = new Dictionary<string, string>();
            }
        }
        public string Traducir(string clave)
        {
            if (_mensajesGlobales.ContainsKey(clave))
                return _mensajesGlobales[clave];

            return $"[{clave}]";
        }

        public static SessionManager_391IAU Instancia => ObtenerInstancia();

        public bool HaySesionActiva() => UsuarioActual != null;
        public BEUsuario getUser() => UsuarioActual;
        public void IniciarSesion(BEUsuario usuario) => UsuarioActual = usuario;
        public void CerrarSesion() => UsuarioActual = null;

        private List<IObserver_391IAU> observadores = new List<IObserver_391IAU>();
        public string IdiomaActual { get; private set; } = "Español";

        public void AgregarObservador(IObserver_391IAU observer)
        {
            if (!observadores.Contains(observer))
                observadores.Add(observer);
        }

        public void EliminarObservador(IObserver_391IAU observer)
        {
            if (observadores.Contains(observer))
                observadores.Remove(observer);
        }

        public void NotificarObservadores()
        {
            foreach (var obs in observadores)
                obs.ActualizarIdioma(IdiomaActual);
        }

        private List<Form> FormsRegistrados = new List<Form>();

        public void RegistrarFormulario(Form form)
        {
            if (!FormsRegistrados.Contains(form))
            {
                FormsRegistrados.Add(form);
            }
        }
        public void CambiarIdioma(string nuevoIdioma)
        {
            IdiomaActual = nuevoIdioma;

            CargarMensajes(nuevoIdioma);

            foreach (var form in FormsRegistrados)
                TraducirFormulario(form, nuevoIdioma);

            NotificarObservadores();
        }

        private void TraducirFormulario(Form form, string idioma)
        {
            string archivo = $"{idioma}_{form.Name}.json";
            string ruta = Path.Combine(Application.StartupPath, "Idiomas", idioma, archivo);

            if (!File.Exists(ruta))
                return;

            var dic = CargarDiccionarioJSON(ruta);
            TraducirControl(form, dic);
        }


        private void AplicarTraduccionesEnControles(Control control, Dictionary<string, string> dic)
        {
            if (control.Tag != null)
            {
                string clave = control.Tag.ToString();
                if (dic.ContainsKey(clave))
                    control.Text = dic[clave];
            }

            if (control is MenuStrip menu)
            {
                foreach (ToolStripItem item in menu.Items)
                    TraducirMenuItem(item, dic);
            }

            foreach (Control hijo in control.Controls)
                AplicarTraduccionesEnControles(hijo, dic);
        }


        private void TraducirMenuItem(ToolStripItem item, Dictionary<string, string> dic)
        {
            if (item.Tag != null)
            {
                string clave = item.Tag.ToString();
                if (dic.ContainsKey(clave))
                    item.Text = dic[clave];
            }

            if (item is ToolStripMenuItem menuItem)
            {
                foreach (ToolStripItem sub in menuItem.DropDownItems)
                    TraducirMenuItem(sub, dic);
            }
        }

        private Dictionary<string, string> CargarDiccionarioJSON(string ruta)
        {
            try
            {
                string json = File.ReadAllText(ruta);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                       ?? new Dictionary<string, string>();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error leyendo JSON de idioma: " + ex.Message);
                return new Dictionary<string, string>();
            }
        }
        private void TraducirControl(object obj, Dictionary<string, string> dic)
        {
            if (obj is MenuStrip menu)
            {
                foreach (ToolStripItem item in menu.Items)
                    TraducirControl(item, dic);
                return;
            }

            if (obj is ToolStripItem menuItem)
            {
                if (menuItem.Tag != null)
                {
                    string clave = menuItem.Tag.ToString();
                    if (dic.ContainsKey(clave))
                        menuItem.Text = dic[clave];
                }

                if (menuItem is ToolStripMenuItem ts)
                {
                    foreach (ToolStripItem sub in ts.DropDownItems)
                        TraducirControl(sub, dic);
                }

                return;
            }

            if (obj is Form form)
            {
                if (dic.ContainsKey("TituloForm"))
                    form.Text = dic["TituloForm"];

                foreach (Control c in form.Controls)
                    TraducirControl(c, dic);
                return;
            }

            if (obj is Control control)
            {
                if (control.Tag != null)
                {
                    string clave = control.Tag.ToString();
                    if (dic.ContainsKey(clave))
                        control.Text = dic[clave];
                }

                foreach (Control c in control.Controls)
                    TraducirControl(c, dic);

                return;
            }
        }

        public void TraducirFormulario(Form form)
        {
            TraducirFormulario(form, IdiomaActual);
        }
    }
}
