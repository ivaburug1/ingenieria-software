using _686DP_SERVICIOS.Composite;
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
using _686DP_SERVICIOS.Observer;
using System.Text.RegularExpressions;
using _686DP_SERVICIOS.Singleton;

namespace AseguraYa
{
    public partial class _686DPfrmFamilias : Form
    {
        private _686DPfrmCrearPerfil padre;
        string idioma = "";
        _686DP_LanguajeManager LMG = new _686DP_LanguajeManager();
        _686DP_Idioma IdiomaClase = new _686DP_Idioma();
        _686DP_BLLPerfil bllper = new _686DP_BLLPerfil();
        _686DP_BLLEvento blle = new _686DP_BLLEvento();
        public _686DPfrmFamilias(_686DPfrmCrearPerfil frmPadre, string idi)
        {
            InitializeComponent();
            padre = frmPadre;
            idioma= idi;
            cambiarIdioma();
            this.FormClosed += _686DPfrmFamilias_FormClosed;

        }
        List<_686DP_PermisoSimple> permisos = null;
        _686DP_BLLPermisoSimple bllps = new _686DP_BLLPermisoSimple();
        _686DP_BLLFamilia bllf = new _686DP_BLLFamilia();
        _686DP_Familia familia = null;
        string fam;
        TreeNode Raiz;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TV_FamiliaEnCreacion.Nodes == null || TV_FamiliaEnCreacion.Nodes.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_NoComponentes"), LMG.Traducir("Titulo_Error") );
                return;
            }

            if (familia == null)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_NoFamilia"), LMG.Traducir("Titulo_Error") );
                return;
            }

            if (familia.ObtenerHijos() == null || familia.ObtenerHijos().Count == 0)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_FamiliaSinComponentes"), LMG.Traducir("Titulo_Error") );
                return;
            }

            try
            {
                int operacion = bllf.CrearFamilia(familia);
                if (operacion == 0)
                {
                    MessageBox.Show(LMG.Traducir("BucleFamilia"));
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("Mensaje_FamiliaCreada"), LMG.Traducir("Titulo_Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                TV_FamiliaEnCreacion.Nodes.Clear();
                Cargar();
                padre.iniciar();
                int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                blle.RegistrarEvento(dni, this.Name, "Se creó la familia " + familia.Nombre, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_ErrorCrearFamilia") + ex.Message, LMG.Traducir("Titulo_Error") );
            }
        } 

        private void _686DPfrmFamilias_Load(object sender, EventArgs e)
        {
            Cargar();
        }

        private void _686DPfrmFamilias_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.iniciar();
        }

        private void Cargar()
        { LMG.CargarMensajesGlobales(idioma);
            LSTPermisos.Items.Clear();
            permisos = bllps.TraerPermisos();

            foreach (var permiso in permisos)
            {
                LSTPermisos.Items.Add(permiso.Nombre);
            }

            List<_686DP_Familia> familias = bllf.TraerFamilia();
            CargarTreeViewFamilias(familias);
            //TraducirTreeView(TV_Familia);
            //TraducirTreeView(TV_FamiliaEnCreacion); 
            //TraducirList(LSTPermisos);
            cambiarIdioma();
        }

        private void cambiarIdioma()
        {
            Form fi = this;
            LMG.RegistrarForm(fi);
            IdiomaClase.AgregarObsevador(LMG);
            IdiomaClase.CambiarIdioma(idioma);
        }
        private void TraducirList(ListBox lSTPermisos)
        {
            for (int i = 0; i < lSTPermisos.Items.Count; i++)
            {
                string original = lSTPermisos.Items[i].ToString();
                string traducido = LMG.Traducir(original);
                string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                if (limpio == lSTPermisos.Items[i].ToString())
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
                if (limpio == node.Text)
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

        private void AgregarPermiso_Click(object sender, EventArgs e)
        {
            
            if (TV_FamiliaEnCreacion.Nodes.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_IngreseNombreFamilia"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (LSTPermisos.SelectedItem == null)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_SeleccionePermiso"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombrePermisoSeleccionado = LSTPermisos.SelectedItem.ToString();

            if (familia != null && familia.idFamilia != 0)
            {
                List<_686DP_Perfil> perfilesConFamilia = bllper.TraerPerfilesConFamilia(familia.idFamilia);

                foreach (_686DP_Perfil perfil in perfilesConFamilia)
                {
                    int codigoPerfil = bllper.TraerCodigoPerfil(perfil);
                    List<_686DP_PermisoSimple> permisosEnPerfil = bllper.TraerPermisosDelPerfil(codigoPerfil);

                    if (permisosEnPerfil.Any(p => p.Nombre == nombrePermisoSeleccionado))
                    {
                        MessageBox.Show(LMG.Traducir("Mensaje_PermisoYaEnPerfilRelacionado"), LMG.Traducir("Titulo_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TV_FamiliaEnCreacion.Nodes.Clear();
                        familia = null;
                        Raiz = null;
                        return;
                    }
                }
            }

            _686DP_PermisoSimple permisoSeleccionado = permisos.FirstOrDefault(p => p.Nombre == nombrePermisoSeleccionado);

            if (permisoSeleccionado == null)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_PermisoNoEncontrado"), LMG.Traducir("Titulo_Error"));
                return;
            }

            foreach (TreeNode nodo in Raiz.Nodes)
            {
                if (nodo.Tag is _686DP_PermisoSimple permisoExistente &&
                    permisoExistente.DP686_PermisoSimpleID == permisoSeleccionado.DP686_PermisoSimpleID)
                {
                    MessageBox.Show(LMG.Traducir("Mensaje_PermisoYaAgregadoDirecto"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var permisosYaCargados = ObtenerPermisosDelPerfil();
            if (permisosYaCargados.Any(p => p.DP686_PermisoSimpleID == permisoSeleccionado.DP686_PermisoSimpleID))
            {
                MessageBox.Show(LMG.Traducir("Mensaje_PermisoIncluidoEnFamilia"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                TreeNode nodoPermiso = new TreeNode(permisoSeleccionado.Nombre)
                {
                    Tag = permisoSeleccionado
                };

                familia.Agregar(permisoSeleccionado);
                Raiz.Nodes.Add(nodoPermiso);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_ErrorAgregarPermiso") + ": " + ex.Message, LMG.Traducir("Titulo_Error"));
            }
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


        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
                return;
            string text = textBox1.Text;
            bool existe = bllf.validarUnico(() => text.ToLower());
            if(existe)
            {
                MessageBox.Show(LMG.Traducir("Familia existente"));
                return;
            }
            else
            {
                _686DP_Familia nuevaFamilia = new _686DP_Familia(textBox1.Text, 0);

                familia = nuevaFamilia;

                Raiz = new TreeNode(nuevaFamilia.Nombre);
                Raiz.Tag = nuevaFamilia;

                TV_FamiliaEnCreacion.Nodes.Clear();
                TV_FamiliaEnCreacion.Nodes.Add(Raiz);
            }
            
        }

        private void TV_FamiliaEnCreacion_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void AgregarFamilia_Click(object sender, EventArgs e)
        {
            TreeNode nodoSeleccionado = TV_Familia.SelectedNode;

            if (TV_FamiliaEnCreacion.Nodes.Count == 0)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_IngreseNombreFamilia"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nodoSeleccionado == null || nodoSeleccionado.Tag == null)
            {
                MessageBox.Show(LMG.Traducir("Mensaje_SeleccioneFamilia"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nodoSeleccionado.Tag is _686DP_Familia familiaSeleccionada)
            {
                foreach (TreeNode nodo in Raiz.Nodes)
                {
                    if (nodo.Tag is _686DP_Familia famExistente &&
                        famExistente.idFamilia == familiaSeleccionada.idFamilia)
                    {
                        MessageBox.Show(LMG.Traducir("Mensaje_FamiliaYaAgregada"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var permisosNuevos = ObtenerPermisosRecursivos(familiaSeleccionada);
                var permisosYaCargados = ObtenerPermisosDelPerfil();

                foreach (var permiso in permisosNuevos)
                {
                    if (permisosYaCargados.Any(p => p.DP686_PermisoSimpleID == permiso.DP686_PermisoSimpleID))
                    {
                        MessageBox.Show(LMG.Traducir("Mensaje_PermisoYaAgregadoPerfil") + permiso.Nombre + "'", LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                try
                {
                    string nombre = familiaSeleccionada.Nombre;
                    string traducido = LMG.Traducir(nombre);
                    string limpio = traducido.Replace("[", "").Replace("]", "").Trim();
                    TreeNode nodoFamilia = (nombre == limpio) ? new TreeNode(limpio) : new TreeNode(traducido);

                    CargarHijosRecursivamente(familiaSeleccionada, nodoFamilia);
                    familia.Agregar(familiaSeleccionada);
                    Raiz.Nodes.Add(nodoFamilia);
                    TraducirTreeView(TV_FamiliaEnCreacion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(LMG.Traducir("Mensaje_ErrorAgregarFamilia") + ex.Message, LMG.Traducir("Titulo_Error") );
                }
            }
            else
            {
                MessageBox.Show(LMG.Traducir("Mensaje_SoloAgregarFamilias"), LMG.Traducir("Titulo_Advertencia"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (TV_Familia.SelectedNode == null || TV_Familia.SelectedNode.Parent == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarNodo"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = TV_Familia.SelectedNode;
            var permiso = nodo.Tag as _686DP_PermisoSimple;
            var nodoPadre = nodo.Parent;
            var familia = nodoPadre.Tag as _686DP_Familia;

            if (permiso == null || familia == null)
            {
                MessageBox.Show(LMG.Traducir("SoloPermisosSimples"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllf.DesasignarPermisoDesdeFamilia(familia, permiso);

                if (familia.ObtenerHijos().Count == 1) 
                {
                    bllf.EliminarFamilia(familia);
                    MessageBox.Show(LMG.Traducir("FamiliaEliminada"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Se la familia " + familia.Nombre + "Por falta de componentes", 1);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("DesasignacionExitosa"), LMG.Traducir("TituloExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Se desasigno un permiso de la familia " + familia.Nombre, 1);
                }

                Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDesasignar") + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (TV_Familia.SelectedNode == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarFamilia"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = TV_Familia.SelectedNode;
            familia = nodo.Tag as _686DP_Familia;

            if (familia == null)
            {
                MessageBox.Show(LMG.Traducir("SoloFamilias"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _686DP_Familia familiaBD = bllf.TraerFamiliaPorID(familia.idFamilia);

                TV_FamiliaEnCreacion.Nodes.Clear();

                Raiz = CrearNodoRecursivo(familiaBD);
                TV_FamiliaEnCreacion.Nodes.Add(Raiz);
                TV_FamiliaEnCreacion.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar familia desde BD: " + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TreeNode CrearNodoRecursivo(_686DP_Composite permiso)
        {
            TreeNode nodo = new TreeNode(permiso.Nombre) { Tag = permiso };

            if (permiso is _686DP_Composite comp)
            {
                foreach (var hijo in comp.ObtenerHijos())
                {
                    nodo.Nodes.Add(CrearNodoRecursivo(hijo));
                }
            }

            return nodo;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (TV_Familia.SelectedNode == null || TV_Familia.SelectedNode.Parent == null)
            {
                MessageBox.Show(LMG.Traducir("SeleccionarNodo"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var nodo = TV_Familia.SelectedNode;
            var permiso = nodo.Tag as _686DP_Composite;
            var nodoPadre = nodo.Parent;
            var fam = nodoPadre.Tag as _686DP_Familia;

            if (!(permiso is _686DP_Familia familia) || fam == null)
            {
                MessageBox.Show(LMG.Traducir("SoloFamilias"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bllf.DesasignarFamiliaDesdeFamilia(fam, familia);

                if (fam.ObtenerHijos().Count == 1)
                {
                    bllf.EliminarFamilia(fam);
                    MessageBox.Show(LMG.Traducir("FamiliaEliminada"), LMG.Traducir("TituloAviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "la familia " + familia.Nombre + " fue eliminada por falta de componentes", 1);
                }
                else
                {
                    MessageBox.Show(LMG.Traducir("DesasignacionExitosa"), LMG.Traducir("TituloExito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    int dni = _686DP_Singleton.Instancia.Usuario._686DPDNI;
                    blle.RegistrarEvento(dni, this.Name, "Se desasigno una familia de la la familia " + familia.Nombre, 1);
                }

                Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LMG.Traducir("ErrorDesasignar") + ex.Message, LMG.Traducir("TituloError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string texto = textBox1.Text;
            if (!Regex.IsMatch(texto, @"^[a-zA-Z_]*$"))
            {
                MessageBox.Show("Solo se permiten letras.");
                textBox1.Text = Regex.Replace(texto, @"^[a-zA-Z_]*$", "");
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TV_FamiliaEnCreacion.Nodes.Clear();
            textBox1.Text = "";
        }
    }
}
