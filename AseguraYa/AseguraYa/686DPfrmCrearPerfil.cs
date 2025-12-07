using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _686DP_BE;
using _686DP_BLL;
using System.Windows.Forms;
using _686DP_SERVICIOS;
using _686DP_SERVICIOS.Composite;
using static System.Net.Mime.MediaTypeNames;
using _686DP_SERVICIOS.Observer;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using _686DP_SERVICIOS.Singleton;

namespace AseguraYa
{
    public partial class _686DPfrmCrearPerfil : Form
    {
        string idi = "";
        public _686DPfrmCrearPerfil(string idiomaLocal)
        {
            InitializeComponent();
            idi = idiomaLocal;
        }
        TreeNode Raiz;
        string Perfil;
        _686DP_BLLPermisoSimple bllps = new _686DP_BLLPermisoSimple();
        _686DP_BLLPerfil bllp = new _686DP_BLLPerfil();
        _686DP_BLLFamilia bllf = new _686DP_BLLFamilia();
        List<_686DP_PermisoSimple> permisos = new List<_686DP_PermisoSimple>();
        List<_686DP_Familia> FamiliasSeleccionada = new List<_686DP_Familia>();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();

        _686DP_Perfil perfil = null;
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void _686DPfrmCrearPerfil_Load(object sender, EventArgs e)
        {
            LMG.CargarMensajesGlobales(idi);
            iniciar();
        }

        public void iniciar()
        {
            Cargar();
            CargarPerfiles();
            TraducirTreeView(TV_Familia);
            TraducirTreeView(TV_Perfiles);
            TraducirTreeView(TV_PerfilPorCrear);
            TraducirList(LSTPermisos);
            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idi);
        }

        private void TraducirList(ListBox lSTPermisos)
        {
            for (int i = 0; i < lSTPermisos.Items.Count; i++)
            {
                string original = lSTPermisos.Items[i].ToString();
                string traducido = LMG.Traducir(original);
                string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                if(limpio == lSTPermisos.Items[i].ToString())
                {
                    lSTPermisos.Items[i] = limpio;
                }
                else
                {
                    lSTPermisos.Items[i] = traducido;
                }
            }
        }

        private void TraducirTreeView(TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                string traducir = LMG.Traducir(node.Text);
                string limpio = traducir.Replace("[", "").Replace("]", "").Trim();
                if(limpio==node.Text)
                {
                    node.Text = limpio;
                }
                else
                {
                    node.Text = traducir;
                }
                TraducirNodosHijos(node);
            }
        }

        private void TraducirNodosHijos(TreeNode parent)
        {
            foreach (TreeNode child in parent.Nodes)
            {
                string children = LMG.Traducir(child.Text);
                string limpio = children.Replace("[", "").Replace("]", "").Trim();
                if (limpio == child.Text)
                {
                    child.Text = limpio;
                }
                else
                {
                    child.Text = children;
                }
                TraducirNodosHijos(child);
            }
        }

        private void CargarPerfiles()
        {

            TV_Perfiles.Nodes.Clear();

            List<_686DP_Perfil> perfiles = bllp.TraerPerfiles();

            foreach (var perfil in perfiles)
            {
                TreeNode nodoPerfil = new TreeNode(perfil.Nombre);
                nodoPerfil.Tag = perfil;

                foreach (var componente in perfil.ObtenerPermisos())
                {
                    TreeNode nodoComponente = new TreeNode(componente.Nombre);
                    nodoComponente.Tag = componente;
                    CargarComponentesRecursivamente(componente, nodoComponente);

                    nodoPerfil.Nodes.Add(nodoComponente);
                }

                TV_Perfiles.Nodes.Add(nodoPerfil);
            }

            TV_Perfiles.ExpandAll();
        }

        private void CargarComponentesRecursivamente(_686DP_Composite componente, TreeNode nodoPadre)
        {
            if (componente is _686DP_Familia familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                    nodoHijo.Tag = hijo;

                    if (hijo is _686DP_Familia)
                    {
                        CargarComponentesRecursivamente(hijo, nodoHijo);
                    }

                    nodoPadre.Nodes.Add(nodoHijo);
                }
            }
        }
        public void Cargar()
        {
            LSTPermisos.Items.Clear();
            permisos = bllps.TraerPermisos();

            foreach (var permiso in permisos)
            {
                LSTPermisos.Items.Add(permiso.Nombre);
            }

            List<_686DP_Familia> familias = bllf.TraerFamilia();
            CargarTreeViewFamilias(familias);

        }

        private void CargarTreeViewFamilias(List<_686DP_Familia> familias)
        {
            TV_Familia.Nodes.Clear();

            foreach (var familia in familias)
            {
                TreeNode nodoPadre = new TreeNode(familia.Nombre);
                nodoPadre.Tag = familia;

                CargarHijosRecursivamente(familia, nodoPadre);

                TV_Familia.Nodes.Add(nodoPadre);
            }

            TV_Familia.ExpandAll();
        }

        private void CargarHijosRecursivamente(_686DP_Composite componente, TreeNode nodoPadre)
        {
            if (componente is _686DP_Familia familia)
            {
                foreach (var hijo in familia.ObtenerHijos())
                {
                    TreeNode nodoHijo = new TreeNode(hijo.Nombre);
                    nodoHijo.Tag = hijo;

                    if (hijo is _686DP_Familia) 
                    {
                        CargarHijosRecursivamente(hijo, nodoHijo);
                    }
                    nodoPadre.Nodes.Add(nodoHijo);
                }
            }
        }


        private void TV_Familia_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            Perfil = textBox1.Text.Trim();

            bool existe = bllp.ValidarUnico(() => Perfil.ToLower());

            if (existe)
            {
                MessageBox.Show(LMG.Traducir("PerfilYaExiste"));
                return;
            }

            TV_PerfilPorCrear.Nodes.Clear();

            Raiz = new TreeNode(Perfil);
            Raiz.Tag = Perfil;
            TV_PerfilPorCrear.Nodes.Add(Raiz);

            perfil = new _686DP_Perfil(Perfil);
        }

        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            TreeNode nodoSeleccionado = TV_Familia.SelectedNode;
            if (TV_PerfilPorCrear.Nodes.Count==0)
            {
                MessageBox.Show(LMG.Traducir("NombrePerfilFaltante"));
                return;
            }

            if (nodoSeleccionado == null || nodoSeleccionado.Tag == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionaFamilia"));
                return;
            }

            if (nodoSeleccionado.Tag is _686DP_Familia familiaSeleccionada)
            {
                foreach (TreeNode nodo in Raiz.Nodes)
                {
                    if (nodo.Tag is _686DP_Familia famExistente &&
                        famExistente.idFamilia == familiaSeleccionada.idFamilia)
                    {
                        MessageBox.Show(LMG.Traducir("PermisoYaIncluido"));
                        return;
                    }
                }

                var permisosNuevos = ObtenerPermisosRecursivos(familiaSeleccionada);
                var permisosYaCargados = ObtenerPermisosDelPerfil();

                foreach (var permiso in permisosNuevos)
                {
                    if (permisosYaCargados.Any(p => p.DP686_PermisoSimpleID == permiso.DP686_PermisoSimpleID))
                    {
                        MessageBox.Show(LMG.Traducir("PermisoYaIncluido"));
                        return;
                    }
                }

                try
                {
                    string nombre = familiaSeleccionada.Nombre;
                    string traducido = LMG.Traducir(nombre);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                    TreeNode nodoPerfil = (nombre == limpio) ? new TreeNode(limpio) : new TreeNode(traducido);

                    CargarHijosRecursivamente(familiaSeleccionada, nodoPerfil);
                    perfil.AgregarPermiso(familiaSeleccionada);
                    Raiz.Nodes.Add(nodoPerfil);
                    TraducirTreeView(TV_PerfilPorCrear);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("ErrorAgregarFamilia") + ex.Message);
                }
            }
            else
            {
                MessageBox.Show(LMG.Traducir("SoloFamilias"));
            }
        }

        private List<_686DP_PermisoSimple> ObtenerPermisosRecursivos(_686DP_Familia familia)
        {
            List<_686DP_PermisoSimple> permisos = new List<_686DP_PermisoSimple>();

            foreach (var hijo in familia.ObtenerHijos())
            {
                if (hijo is _686DP_PermisoSimple permiso)
                {
                    permisos.Add(permiso);
                }
                else if (hijo is _686DP_Familia subFamilia)
                {
                    permisos.AddRange(ObtenerPermisosRecursivos(subFamilia));
                }
            }

            return permisos;
        }

        private List<_686DP_PermisoSimple> ObtenerPermisosDelPerfil()
        {
            List<_686DP_PermisoSimple> permisos = new List<_686DP_PermisoSimple>();

            foreach (TreeNode nodo in Raiz.Nodes)
            {
                if (nodo.Tag is _686DP_Familia familia)
                {
                    permisos.AddRange(ObtenerPermisosRecursivos(familia));
                }
                else if (nodo.Tag is _686DP_PermisoSimple permiso)
                {
                    permisos.Add(permiso);
                }
            }

            return permisos;
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if (TV_PerfilPorCrear.Nodes.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("NombrePerfilFaltante"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (LSTPermisos.SelectedItem == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionaPermiso"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombrePermisoSeleccionado = LSTPermisos.SelectedItem.ToString();

            _686DP_PermisoSimple permisoSeleccionado = permisos.FirstOrDefault(p => p.Nombre == nombrePermisoSeleccionado);

            if (permisoSeleccionado == null)
            {
                MessageBox.Show(LMG.Traducir("PermisoNoEncontrado"), LMG.Traducir("Titulo_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (TreeNode nodo in Raiz.Nodes)
            {
                if (nodo.Tag is _686DP_PermisoSimple permisoExistente &&
                    permisoExistente.DP686_PermisoSimpleID == permisoSeleccionado.DP686_PermisoSimpleID)
                {
                    MessageBox.Show(LMG.Traducir("PermisoYaDirecto"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var permisosYaCargados = ObtenerPermisosDelPerfil();
            if (permisosYaCargados.Any(p => p.DP686_PermisoSimpleID == permisoSeleccionado.DP686_PermisoSimpleID))
            {
                MessageBox.Show(LMG.Traducir("PermisoYaIncluidoFamilia"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TreeNode nodoPermiso = new TreeNode(permisoSeleccionado.Nombre)
                {
                    Tag = permisoSeleccionado
                };

                perfil.AgregarPermiso(permisoSeleccionado);
                Raiz.Nodes.Add(nodoPermiso);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorAgregarPermiso") + ": " + ex.Message, LMG.Traducir("Titulo_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BTNCrearPerfil_Click(object sender, EventArgs e)
        {
            if (TV_PerfilPorCrear.Nodes == null || TV_PerfilPorCrear.Nodes.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("PerfilSinComponentes"));
                return;
            }
            if(perfil == null)
            {
                MessageBox.Show(LMG.Traducir("PerfilNoInicializado"));
                return;
            }
            if (perfil.ObtenerPermisos() == null || perfil.ObtenerPermisos().Count == 0)
            {
                MessageBox.Show(LMG.Traducir("PerfilSinPermisos"));
                return;
            }
            try
            {
                MessageBox.Show(LMG.Traducir("PerfilCreadoOK"));
                bllp.CrearPerfil(perfil);
                TV_PerfilPorCrear.Nodes.Clear();
                CargarPerfiles();
                int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dni, this.Name, "Se creo un perfil", 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorCrearPerfil") + ex.Message);
            }
        }

        private void btnCrearFamilia_Click(object sender, EventArgs e)
        {
            _686DPfrmFamilias f = new _686DPfrmFamilias(this, idi);
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TV_Perfiles.SelectedNode == null || TV_Perfiles.SelectedNode.Parent == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarNodo"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = TV_Perfiles.SelectedNode;
            var permiso = nodo.Tag as _686DP_Composite;
            var nodoPadre = nodo.Parent;
            var perfil = nodoPadre.Tag as _686DP_Perfil;

            if (!(permiso is _686DP_Familia familia) || perfil == null)
            {
                MessageBox.Show(LMG.Traducir("SoloFamilias"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllp.DesasignarFamiliaDesdePerfil(perfil, familia);

                if (perfil.ObtenerPermisos().Count == 1)
                {
                    bllp.EliminarPerfil(perfil);
                    MessageBox.Show(LMG.Traducir("PerfilEliminado"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dnipermiso = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dnipermiso, this.Name, "Se eliminó el perfil " + perfil.Nombre + " por falta de componentes", 1);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("DesasignacionExitosa"), LMG.Traducir("TituloExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Se desasignó una familia al perfil" + perfil.Nombre, 1);
                }

                CargarPerfiles();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDesasignar") + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TV_Perfiles.SelectedNode == null || TV_Perfiles.SelectedNode.Parent == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarNodoPermiso"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = TV_Perfiles.SelectedNode;
            var permiso = nodo.Tag as _686DP_PermisoSimple;
            var nodoPadre = nodo.Parent;
            var perfil = nodoPadre.Tag as _686DP_Perfil;

            if (permiso == null || perfil == null)
            {
                MessageBox.Show(LMG.Traducir("SoloPermisosSimples"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllp.DesasignarPermisoDesdePerfil(perfil, permiso);

                if (perfil.ObtenerPermisos().Count == 1) 
                {
                    bllp.EliminarPerfil(perfil);
                    MessageBox.Show(LMG.Traducir("PerfilEliminado"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dnipermiso = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dnipermiso, this.Name, "Se eliminó el perfil " + perfil.Nombre + " por falta de componentes", 1);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("DesasignacionExitosa"), LMG.Traducir("TituloExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Se desasigno un permiso del perfil" + perfil.Nombre, 1);
                }

                CargarPerfiles();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDesasignar") + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TV_Perfiles.SelectedNode == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarPerfil"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var nodo = TV_Perfiles.SelectedNode;
            perfil = nodo.Tag as _686DP_Perfil;

            if(perfil == null)
            {
                MessageBox.Show(LMG.Traducir("SoloPerfil"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                _686DP_Perfil PerfilBD = bllp.TraerPerfil(perfil.Nombre);

                TV_PerfilPorCrear.Nodes.Clear();

                perfil = PerfilBD;
                TV_PerfilPorCrear.Nodes.Clear();
                Raiz = new TreeNode(perfil.Nombre) { Tag = perfil };

                foreach (_686DP_Composite componente in perfil.ObtenerPermisos())
                {
                    nodo = CrearNodoRecursivo(componente);
                    Raiz.Nodes.Add(nodo);
                }

                TV_PerfilPorCrear.Nodes.Add(Raiz);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar familia desde BD: " + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TreeNode CrearNodoRecursivo(_686DP_Composite permiso)
        {
            string texto = LMG.Traducir(permiso.Nombre).Replace("[", "").Replace("]", "").Trim();
            TreeNode nodo = new TreeNode(texto) { Tag = permiso };

            foreach (var hijo in permiso.ObtenerHijos())
            {
                if (hijo is _686DP_Composite comp)
                {
                    nodo.Nodes.Add(CrearNodoRecursivo(comp));
                }
                else if (hijo is _686DP_PermisoSimple permisoSimple)
                {
                    string nombreTraducido = LMG.Traducir(permisoSimple.Nombre).Replace("[", "").Replace("]", "").Trim();
                    TreeNode nodoPermiso = new TreeNode(nombreTraducido) { Tag = permisoSimple };
                    nodo.Nodes.Add(nodoPermiso);
                }
            }

            return nodo;
        }
        private TreeNode CrearNodoRecursivoDesdePerfil(_686DP_Perfil perfil)
        {
            TreeNode nodoRaiz = new TreeNode(perfil.Nombre) { Tag = perfil };

            foreach (_686DP_Composite componente in perfil.ObtenerPermisos())
            {
                TreeNode nodo = CrearNodoRecursivo(componente);
                nodoRaiz.Nodes.Add(nodo);
            }

            return nodoRaiz;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            if (!Regex.IsMatch(texto, @"^[a-zA-Z\s]*$"))
            {
                MessageBox.Show("Solo se permiten letras.");
                textBox1.Text = Regex.Replace(texto, @"[^a-zA-Z\s]", "");
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TV_PerfilPorCrear.Nodes.Clear();
            textBox1.Text = "";
        }
    }
}
