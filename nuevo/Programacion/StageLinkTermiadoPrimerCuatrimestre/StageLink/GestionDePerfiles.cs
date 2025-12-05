using BLL_391IAU;
using Servicios_391IAU.Composite;
using SessionManager_391IAU;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StageLink
{
    public partial class GestionDePerfiles : Form, IObserver_391IAU
    {
        public GestionDePerfiles()
        {
            InitializeComponent();
        }
        public void ActualizarIdioma(string idioma)
        {
            SessionManager_391IAU.SessionManager_391IAU.Instancia.TraducirFormulario(this);
        }

        public event Action PermisosActualizados;
        private void GestionDePerfiles_Load(object sender, EventArgs e)
        {
            CargarFamiliasTreeView();
            CargarTreeViewPerfiles();
            CargarPermisosListBox();
            CargarComboSubFamilias();

            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            sm.AgregarObservador(this);
            sm.RegistrarFormulario(this);
            sm.TraducirFormulario(this);
        }
        private bool hayCambios = false;
        private void CargarPermisosListBox()
        {
            LBPermisos.Items.Clear();
            LBPermisos.DisplayMember = "NombreVisual";
            LBPermisos.ValueMember = "ID";

            BLLPermiso bll = new BLLPermiso();
            var permisos = bll.TraerPermisosSimples();

            var listaFormateada = permisos.Select(p => new
            {
                ID = p.IDPermiso_391IAU,
                NombreVisual = $"[P] {p.NombrePermiso_391IAU}"
            }).ToList();

            LBPermisos.DataSource = listaFormateada;
        }
        private void BTNCancelar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            if (hayCambios)
            {
                var r = MessageBox.Show(
                    sm.Traducir("GestionPerfiles_Cancelar_Advertencia"),
                    sm.Traducir("GestionPerfiles_Cancelar_Titulo"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (r != DialogResult.Yes)
                    return;
            }

            PermisosActualizados?.Invoke();
            this.Close();
        }
        private void CargarFamiliasTreeView()
        {
            TVFamilias.Nodes.Clear();
            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            var familias = bll.TraerTodasLasFamilias();

            foreach (var fam in familias)
            {
                TreeNode nodoPadre = new TreeNode($"[F] {fam.Nombre}")
                {
                    Tag = fam.IDFamilia
                };

                CargarSubfamiliasNodo(nodoPadre, fam.IDFamilia);

                TVFamilias.Nodes.Add(nodoPadre);
            }
            TVFamilias.ExpandAll();
        }

        private void CargarSubfamiliasNodo(TreeNode nodo, int idFamiliaPadre)
        {
            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            var subfamilias = bll.TraerSubfamilias(idFamiliaPadre);

            foreach (var sub in subfamilias)
            {
                TreeNode nodoHijo = new TreeNode($"[SF] {sub.Nombre}")
                {
                    Tag = sub.IDFamilia
                };

                CargarSubfamiliasNodo(nodoHijo, sub.IDFamilia);

                CargarPermisosNodo(nodoHijo, sub.IDFamilia);

                nodo.Nodes.Add(nodoHijo);
            }
            CargarPermisosNodo(nodo, idFamiliaPadre);
        }

        private void CargarPermisosNodo(TreeNode nodo, int idFamilia)
        {
            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            var permisos = bll.TraerPermisosDeFamilia(idFamilia);

            foreach (var p in permisos)
            {
                TreeNode nodoPermiso = new TreeNode($"[P] {p.Nombre}")
                {
                    Tag = p.IDPermiso
                };
                nodo.Nodes.Add(nodoPermiso);
            }
        }
        private void CargarTreeViewPerfiles()
        {
            TVPerfiles.Nodes.Clear();

            BLLPerfil bll = new BLLPerfil();
            var perfiles = bll.TraerPerfiles();

            foreach (var p in perfiles)
            {
                TreeNode nodoPerfil = new TreeNode($"[PERFIL] {p.Nombre_391IAU}")
                {
                    Tag = p.IDRol_391IAU
                };

                var perfilCompleto = bll.TraerPerfilCompleto(p.IDRol_391IAU);

                CargarComponentesPerfil(nodoPerfil, perfilCompleto.ObtenerHijos());

                TVPerfiles.Nodes.Add(nodoPerfil);
            }

            TVPerfiles.ExpandAll();
        }

        private void CargarComponentesPerfil(TreeNode nodoPadre, List<IComponentePermiso_391IAU> componentes)
        {
            foreach (var comp in componentes)
            {
                TreeNode nodo;

                if (comp is Familia_391IAU fam)
                {
                    nodo = new TreeNode($"[F] {fam.Nombre}");
                    nodo.Tag = fam.IDFamilia;

                    CargarComponentesPerfil(nodo, fam.ObtenerHijos());
                }
                else if (comp is PermisoSimple_391IAU perm)
                {
                    nodo = new TreeNode($"[P] {perm.Nombre}");
                    nodo.Tag = perm.IDPermiso;
                }
                else
                {
                    continue;
                }

                nodoPadre.Nodes.Add(nodo);
            }
        }
        private TreeNode ObtenerNodoPerfilNuevo()
        {
            return TVPerfiles.Nodes
                             .Cast<TreeNode>()
                             .FirstOrDefault(n => n.Tag is string s && s == "PERFIL_NEW");
        }
        private void BTNAgregarPerfil_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;
            hayCambios = true;

            string nombre = TXTNuevoPerfilNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPerfil_NombreVacio"));
                return;
            }

            BLLPerfil bll = new BLLPerfil();
            var perfilesExistentes = bll.TraerPerfiles();

            if (perfilesExistentes.Any(p =>
                p.Nombre_391IAU.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPerfil_YaExiste"));
                return;
            }

            if (ObtenerNodoPerfilNuevo() != null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPerfil_PreliminarExistente"));
                return;
            }

            TreeNode root = new TreeNode($"[PERFIL] {nombre}")
            {
                Tag = "PERFIL_NEW"
            };

            TVPerfiles.Nodes.Add(root);
            root.Expand();
        }
        private enum TipoNodoPerfil
        {
            Ninguno,
            PerfilExistente,
            PerfilNuevo,
            Familia,
            Subfamilia,
            Permiso
        }
        private TipoNodoPerfil ObtenerTipoNodoPerfil(TreeNode nodo)
        {
            if (nodo == null) return TipoNodoPerfil.Ninguno;

            string texto = nodo.Text;

            if (nodo.Tag is string s && s == "PERFIL_NEW")
                return TipoNodoPerfil.PerfilNuevo;

            if (texto.StartsWith("[PERFIL]"))
                return TipoNodoPerfil.PerfilExistente;

            if (texto.StartsWith("[F]"))
                return TipoNodoPerfil.Familia;

            if (texto.StartsWith("[SF]"))
                return TipoNodoPerfil.Subfamilia;

            if (texto.StartsWith("[P]"))
                return TipoNodoPerfil.Permiso;

            return TipoNodoPerfil.Ninguno;
        }
        private void BTNAgregarPermiso_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            if (LBPermisos.SelectedItem == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermiso_SeleccionePermiso"));
                return;
            }

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarPermiso_SeleccionNodo"),
                    sm.Traducir("GestionPerfiles_AgregarPermiso_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TipoNodoPerfil tipo = ObtenerTipoNodoPerfil(seleccionado);

            if (tipo == TipoNodoPerfil.Permiso)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermiso_NoDentroDePermiso"));
                return;
            }

            dynamic sel = LBPermisos.SelectedItem;
            int idPermiso = sel.ID;
            string nombrePermiso = $"[P] {sel.NombreVisual.Replace("[P] ", "")}";

            if (seleccionado.Nodes.Cast<TreeNode>()
                .Any(n => n.Tag is int id && id == idPermiso))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermiso_YaAsignado"));
                return;
            }

            TreeNode permisoNode = new TreeNode(nombrePermiso)
            {
                Tag = idPermiso
            };

            seleccionado.Nodes.Add(permisoNode);
            seleccionado.Expand();
        }
        private void BTNAplicar_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = false;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_Aplicar_SeleccionarPerfil"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TreeNode root = seleccionado;
            while (root.Parent != null && !root.Text.StartsWith("[PERFIL]"))
            {
                root = root.Parent;
            }

            if (!root.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_Aplicar_SeleccionPerfilONodo"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            bool esNuevo = root.Tag is string s && s == "PERFIL_NEW";
            string nombrePerfil;

            if (esNuevo)
            {
                nombrePerfil = TXTNuevoPerfilNombre.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombrePerfil))
                {
                    MessageBox.Show(sm.Traducir("GestionPerfiles_Aplicar_NombreVacio"));
                    return;
                }

                BLLPerfil bll = new BLLPerfil();
                var existentes = bll.TraerPerfiles();

                if (existentes.Any(p =>
                    p.Nombre_391IAU.Equals(nombrePerfil, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show(sm.Traducir("GestionPerfiles_Aplicar_YaExiste"));
                    return;
                }
            }
            else
            {
                nombrePerfil = root.Text.Replace("[PERFIL] ", "").Trim();
            }

            if (root.Nodes.Count == 0)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_Aplicar_AgregarPermisoOFamilia"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            BLLPerfil bPerfil = new BLLPerfil();
            int idPerfil;

            if (esNuevo)
            {
                idPerfil = bPerfil.CrearPerfil(nombrePerfil);
                root.Tag = idPerfil;
            }
            else
            {
                idPerfil = (int)root.Tag;
                bPerfil.EliminarPermisosDePerfil(idPerfil);
                bPerfil.EliminarFamiliasDePerfil(idPerfil);
            }

            foreach (TreeNode n in root.Nodes)
            {
                if (n.Text.StartsWith("[P]"))
                {
                    bPerfil.AsociarPermisoAPerfil(idPerfil, (int)n.Tag);
                }
                else if (n.Text.StartsWith("[F]") || n.Text.StartsWith("[SF]"))
                {
                    bPerfil.AsociarFamiliaAPerfil(idPerfil, (int)n.Tag);
                }
            }

            MessageBox.Show(sm.Traducir("GestionPerfiles_Aplicar_Exito"));

            PermisosActualizados?.Invoke();

            TXTNuevoPerfilNombre.Clear();
            CargarTreeViewPerfiles();
        }
        private void BTNAgregarPermisosFamilias_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            if (LBPermisos.SelectedItem == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermisoFamilia_SeleccionePermiso"));
                return;
            }

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermisoFamilia_SeleccioneFamilia"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermisoFamilia_FamiliaOSubfamilia"));
                return;
            }

            dynamic sel = LBPermisos.SelectedItem;
            int idPermiso = sel.ID;
            string nombrePermiso = sel.NombreVisual;

            if (seleccionado.Nodes.Cast<TreeNode>().Any(n => n.Tag is int id && id == idPermiso))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarPermisoFamilia_YaAsignado"));
                return;
            }

            TreeNode permisoNode = new TreeNode(nombrePermiso)
            {
                Tag = idPermiso
            };

            seleccionado.Nodes.Add(permisoNode);
            seleccionado.Expand();
        }
        private void BTNAplicarFamilias_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = false;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AplicarFamilias_SeleccionarFamilia"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AplicarFamilias_NoPermiso"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool tieneContenido = seleccionado.Nodes
                .Cast<TreeNode>()
                .Any(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[SF]") || n.Text.StartsWith("[F]"));

            if (!tieneContenido)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AplicarFamilias_FamiliaVacia"),
                    sm.Traducir("GestionPerfiles_TituloError"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            bool esNueva = seleccionado.Tag is string s && s == "FAM_NEW";

            string nombreFamilia = seleccionado.Text
                .Replace("[F] ", "")
                .Replace("[SF] ", "")
                .Trim();

            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            int idFamilia;

            if (esNueva)
            {
                idFamilia = bll.CrearFamilia(nombreFamilia);
                seleccionado.Tag = idFamilia;
            }
            else
            {
                try
                {
                    idFamilia = (int)seleccionado.Tag;
                }
                catch
                {
                    MessageBox.Show(sm.Traducir("GestionPerfiles_AplicarFamilias_ErrorID"));
                    return;
                }

                bll.EliminarPermisosDeFamilia(idFamilia);

                bll.EliminarTodasLasRelacionesFamiliaSubfamilia(idFamilia);

                foreach (TreeNode nodo in seleccionado.Nodes)
                {
                    if (nodo.Text.StartsWith("[SF]") || nodo.Text.StartsWith("[F]"))
                    {
                        int idHija = (int)nodo.Tag;
                        bll.AsociarSubfamilia(idFamilia, idHija);
                    }
                }
            }

            foreach (TreeNode nodo in seleccionado.Nodes)
            {
                if (nodo.Text.StartsWith("[P]"))
                {
                    int idPermiso = (int)nodo.Tag;
                    bll.AsociarPermisoAFamilia(idFamilia, idPermiso);
                }
            }

            MessageBox.Show(sm.Traducir("GestionPerfiles_AplicarFamilias_Exito"));

            PermisosActualizados?.Invoke();

            CargarFamiliasTreeView();
        }
        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            string nombre = TXTNuevaFamiliaNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_Familia_NombreVacio"));
                return;
            }

            bool existeEnTV = TVFamilias.Nodes
                .Cast<TreeNode>()
                .Any(n => n.Text.Equals("[F] " + nombre, StringComparison.OrdinalIgnoreCase));

            if (existeEnTV)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_Familia_YaExiste"));
                return;
            }

            TreeNode nuevaFam = new TreeNode("[F] " + nombre)
            {
                Tag = "FAM_NEW"
            };

            TVFamilias.Nodes.Add(nuevaFam);
            nuevaFam.Expand();

            TXTNuevaFamiliaNombre.Clear();
        }
        private void BTNEliminarFamilia_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFam_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFam_Invalida"));
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarFam_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarFam_Titulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes) return;

            if (seleccionado.Tag is string s && s == "FAM_NEW")
            {
                seleccionado.Remove();
                return;
            }

            int idFamilia = (int)seleccionado.Tag;

            BLLFamilia_391IAU bllf = new BLLFamilia_391IAU();
            bool ok = bllf.EliminarFamiliaCompleta(idFamilia);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFam_Error"));
                return;
            }

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFam_Exito"));
            CargarFamiliasTreeView();
        }
        private void BTNEliminarPerfil_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPerfil_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarPerfil_Invalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarPerfil_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarPerfil_Titulo"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                return;

            if (seleccionado.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPerfil_Exito"));
                return;
            }

            int idPerfil;

            try
            {
                idPerfil = (int)seleccionado.Tag;
            }
            catch
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPerfil_NoID"));
                return;
            }

            BLLPerfil bll = new BLLPerfil();

            bll.EliminarPermisosDePerfil(idPerfil);
            bll.EliminarFamiliasDePerfil(idPerfil);

            bool ok = bll.EliminarPerfil(idPerfil);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPerfil_ErrorBD"));
                return;
            }

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPerfil_Exito"));

            CargarTreeViewPerfiles();
        }
        private void BTNEliminarFamiliaDePerfil_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFamPerfil_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarFamPerfil_Invalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TreeNode root = seleccionado;
            while (root.Parent != null && !root.Text.StartsWith("[PERFIL]"))
                root = root.Parent;

            if (!root.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFamPerfil_NoPerfil"));
                return;
            }

            int cantidadElementos = root.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarFamPerfil_NoPuedeEliminar"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarFamPerfil_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarFamPerfil_TituloConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes)
                return;

            int idFamilia = (int)seleccionado.Tag;

            if (root.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFamPerfil_Quitado"));
                return;
            }

            int idPerfil = (int)root.Tag;

            BLLPerfil bll = new BLLPerfil();

            bool ok = bll.EliminarUnaFamiliaDePerfil(idPerfil, idFamilia);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFamPerfil_ErrorBD"));
                return;
            }

            seleccionado.Remove();

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarFamPerfil_Exito"));
        }
        private void BTNEliminarPermisoDePerfil_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermPerfil_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[P]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarPermPerfil_Invalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TreeNode root = seleccionado;
            while (root.Parent != null && !root.Text.StartsWith("[PERFIL]"))
                root = root.Parent;

            if (!root.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermPerfil_NoPerfil"));
                return;
            }

            int cantidadPermisos = root.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]"));

            if (cantidadPermisos <= 1)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarPermPerfil_NoPuedeEliminar"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarPermPerfil_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarPermPerfil_TituloConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes)
                return;

            int idPermiso = (int)seleccionado.Tag;

            if (root.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermPerfil_Quitado"));
                return;
            }

            int idPerfil = (int)root.Tag;

            BLLPerfil bll = new BLLPerfil();

            bool ok = bll.EliminarUnPermisoDePerfil(idPerfil, idPermiso);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermPerfil_ErrorBD"));
                return;
            }

            seleccionado.Remove();

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermPerfil_Exito"));
        }
        private void BTNEliminarFamiliaDeFamilia_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[SF]") && !seleccionado.Text.StartsWith("[F]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarSubfamilia_Invalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TreeNode nodoPadre = seleccionado.Parent;

            if (nodoPadre == null || !(nodoPadre.Text.StartsWith("[F]") || nodoPadre.Text.StartsWith("[SF]")))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_NoPadre"));
                return;
            }

            int cantidadElementos = nodoPadre.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]") || n.Text.StartsWith("[SF]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarSubfamilia_NoPuedeEliminar"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarSubfamilia_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarSubfamilia_TituloConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes)
                return;

            int idSubfamilia;
            int idFamiliaPadre;

            try
            {
                idSubfamilia = (int)seleccionado.Tag;
                idFamiliaPadre = (int)nodoPadre.Tag;
            }
            catch
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_NoID"));
                return;
            }

            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            if ((seleccionado.Tag is string s1 && s1 == "FAM_NEW") ||
                (nodoPadre.Tag is string s2 && s2 == "FAM_NEW"))
            {
                seleccionado.Remove();
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_Exito"));
                return;
            }

            bool ok = bll.EliminarRelacionFamiliaSubfamilia(idFamiliaPadre, idSubfamilia);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_ErrorBD"));
                return;
            }

            seleccionado.Remove();

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarSubfamilia_Exito"));
        }
        private void BTNEliminarPermisoDeFamilia_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_Seleccionar"));
                return;
            }

            if (!seleccionado.Text.StartsWith("[P]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarPermFam_Invalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TreeNode nodoPadre = seleccionado.Parent;

            if (nodoPadre == null || !(nodoPadre.Text.StartsWith("[F]") || nodoPadre.Text.StartsWith("[SF]")))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_NoPadre"));
                return;
            }

            int cantidadElementos = nodoPadre.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]") || n.Text.StartsWith("[SF]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_EliminarPermFam_NoPuedeEliminar"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                sm.Traducir("GestionPerfiles_EliminarPermFam_Confirmar"),
                sm.Traducir("GestionPerfiles_EliminarPermFam_TituloConfirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (r != DialogResult.Yes)
                return;

            int idPermiso;
            int idFamiliaPadre;

            try
            {
                idPermiso = (int)seleccionado.Tag;
                idFamiliaPadre = (int)nodoPadre.Tag;
            }
            catch
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_NoID"));
                return;
            }

            if (nodoPadre.Tag is string s && s == "FAM_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_Exito"));
                return;
            }

            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();
            bool ok = bll.EliminarPermisoDeFamilia(idFamiliaPadre, idPermiso);

            if (!ok)
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_ErrorBD"));
                return;
            }

            seleccionado.Remove();

            MessageBox.Show(sm.Traducir("GestionPerfiles_EliminarPermFam_Exito"));
        }
        private void TXTNuevoPerfilNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void TXTNuevoPerfilNombre_MouseClick(object sender, MouseEventArgs e)
        {
            TXTNuevoPerfilNombre.Text = "";
            TXTNuevoPerfilNombre.Focus();
        }

        private void TXTNuevoPerfilNombre_MouseLeave(object sender, EventArgs e)
        {
            if (TXTNuevoPerfilNombre.Text == "")
            {
                TXTNuevoPerfilNombre.Text = "Nuevo Perfil";
            }
        }

        private void TXTNuevaFamiliaNombre_MouseClick(object sender, MouseEventArgs e)
        {
            TXTNuevaFamiliaNombre.Text = "";
            TXTNuevaFamiliaNombre.Focus();
        }

        private void TXTNuevaFamiliaNombre_MouseCaptureChanged(object sender, EventArgs e)
        {
        }

        private void TXTNuevaFamiliaNombre_MouseLeave(object sender, EventArgs e)
        {
            if (TXTNuevaFamiliaNombre.Text == "")
            {
                TXTNuevaFamiliaNombre.Text = "Nuevo Perfil";
            }
        }
        private void BTNAgregarFamiliaAPerfil_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode familiaSel = TVFamilias.SelectedNode;
            if (familiaSel == null ||
                (!familiaSel.Text.StartsWith("[F]") && !familiaSel.Text.StartsWith("[SF]")))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamPerfil_SeleccionarFamilia"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode seleccionadoPerfil = TVPerfiles.SelectedNode;
            if (seleccionadoPerfil == null)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamPerfil_SeleccionarPerfil"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode nodoPerfil = seleccionadoPerfil;
            while (nodoPerfil.Parent != null && !nodoPerfil.Text.StartsWith("[PERFIL]"))
                nodoPerfil = nodoPerfil.Parent;

            if (!nodoPerfil.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamPerfil_PerfilInvalido"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int idFamilia = (int)familiaSel.Tag;
            string nombreFamilia = familiaSel.Text;

            if (nodoPerfil.Nodes.Cast<TreeNode>().Any(n => n.Tag is int id && id == idFamilia))
            {
                MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarFamPerfil_YaAsignada"));
                return;
            }

            TreeNode nuevaFamiliaEnPerfil = new TreeNode(nombreFamilia)
            {
                Tag = idFamilia
            };

            nodoPerfil.Nodes.Add(nuevaFamiliaEnPerfil);
            nodoPerfil.Expand();

            MessageBox.Show(sm.Traducir("GestionPerfiles_AgregarFamPerfil_Exito"));
        }

        private bool EsAncestro(TreeNode posiblePadre, int idBuscado)
        {
            foreach (TreeNode hijo in posiblePadre.Nodes)
            {
                if (hijo.Tag is int id && id == idBuscado)
                    return true;

                if (EsAncestro(hijo, idBuscado))
                    return true;
            }
            return false;
        }

        private void BTNAgregarFamiliaAFamilia_Click(object sender, EventArgs e)
        {
            var sm = SessionManager_391IAU.SessionManager_391IAU.Instancia;

            hayCambios = true;

            TreeNode familiaPadre = TVFamilias.SelectedNode;

            if (familiaPadre == null ||
                (!familiaPadre.Text.StartsWith("[F]") && !familiaPadre.Text.StartsWith("[SF]")))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamFam_SeleccionarPadre"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (CMBSubFamilia.SelectedItem == null)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamFam_SeleccionarHija"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            dynamic sel = CMBSubFamilia.SelectedItem;

            int idHija = sel.IDFamilia;
            string nombreHija = "[SF] " + sel.Nombre;

            int idPadre = (familiaPadre.Tag is int)
                ? (int)familiaPadre.Tag
                : -1;

            if (idPadre == idHija)
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamFam_NoASiMisma"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (EsAncestro(familiaPadre, idHija))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamFam_CicloDetectado"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (familiaPadre.Nodes.Cast<TreeNode>()
                .Any(n => n.Tag is int id && id == idHija))
            {
                MessageBox.Show(
                    sm.Traducir("GestionPerfiles_AgregarFamFam_YaAsignada"),
                    sm.Traducir("GestionPerfiles_ErrorTitulo"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode nodoHijo = new TreeNode(nombreHija)
            {
                Tag = idHija
            };

            familiaPadre.Nodes.Add(nodoHijo);

            familiaPadre.Expand();

            MessageBox.Show(
                sm.Traducir("GestionPerfiles_AgregarFamFam_Exito"),
                sm.Traducir("GestionPerfiles_ExitoTitulo")
            );

        }
        private void CargarComboSubFamilias()
        {
            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();
            var familias = bll.TraerTodasLasFamilias();

            CMBSubFamilia.DataSource = familias;
            CMBSubFamilia.DisplayMember = "Nombre";
            CMBSubFamilia.ValueMember = "IDFamilia";
        }
    }
}
