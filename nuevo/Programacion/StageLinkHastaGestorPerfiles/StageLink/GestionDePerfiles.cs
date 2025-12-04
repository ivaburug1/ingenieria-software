using BLL_391IAU;
using Servicios_391IAU.Composite;
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
    public partial class GestionDePerfiles : Form
    {
        public GestionDePerfiles()
        {
            InitializeComponent();
        }

        public event Action PermisosActualizados;
        private void GestionDePerfiles_Load(object sender, EventArgs e)
        {
            CargarFamiliasTreeView();
            CargarTreeViewPerfiles();
            CargarPermisosListBox();

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
            if (hayCambios)
            {
                var r = MessageBox.Show(
                    "Hay cambios sin guardar.\nSi cierra ahora se perderán definitivamente.\n\n¿Desea salir de todos modos?",
                    "Cambios sin guardar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

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
            hayCambios = true;

            string nombre = TXTNuevoPerfilNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del perfil no puede estar vacío.");
                return;
            }

            BLLPerfil bll = new BLLPerfil();
            var perfilesExistentes = bll.TraerPerfiles();

            if (perfilesExistentes.Any(p =>
                p.Nombre_391IAU.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un perfil con ese nombre.");
                return;
            }

            if (ObtenerNodoPerfilNuevo() != null)
            {
                MessageBox.Show("Ya existe un perfil preliminar cargado. Aplique o borre antes de crear uno nuevo.");
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
            hayCambios = true;

            if (LBPermisos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un permiso.");
                return;
            }

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un perfil o una familia dentro del perfil.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            TipoNodoPerfil tipo = ObtenerTipoNodoPerfil(seleccionado);

            if (tipo == TipoNodoPerfil.Permiso)
            {
                MessageBox.Show("No se pueden agregar permisos dentro de otro permiso.");
                return;
            }

            dynamic sel = LBPermisos.SelectedItem;
            int idPermiso = sel.ID;
            string nombrePermiso = $"[P] {sel.NombreVisual.Replace("[P] ", "")}";

            if (seleccionado.Nodes.Cast<TreeNode>()
                .Any(n => n.Tag is int id && id == idPermiso))
            {
                MessageBox.Show("Este permiso ya está asignado en este nodo.");
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
            hayCambios = false;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar un perfil para aplicar los cambios.",
                    "Error",
                    MessageBoxButtons.OK,
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
                    "Debe seleccionar un perfil o un nodo dentro de un perfil.",
                    "Error",
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
                    MessageBox.Show("El nombre del perfil no puede estar vacío.");
                    return;
                }

                BLLPerfil bll = new BLLPerfil();
                var existentes = bll.TraerPerfiles();

                if (existentes.Any(p =>
                    p.Nombre_391IAU.Equals(nombrePerfil, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Ya existe un perfil con ese nombre.");
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
                    "Debe agregar al menos un permiso o familia al perfil.",
                    "Error",
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

            MessageBox.Show("Perfil actualizado correctamente.");

            PermisosActualizados?.Invoke();

            TXTNuevoPerfilNombre.Clear();
            CargarTreeViewPerfiles();
        }

        private void BTNAgregarPermisosFamilias_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            if (LBPermisos.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un permiso.");
                return;
            }

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar una familia.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show("Debe seleccionar una familia o subfamilia, no un permiso.");
                return;
            }

            dynamic sel = LBPermisos.SelectedItem;
            int idPermiso = sel.ID;
            string nombrePermiso = sel.NombreVisual;

            if (seleccionado.Nodes.Cast<TreeNode>().Any(n => n.Tag is int id && id == idPermiso))
            {
                MessageBox.Show("Este permiso ya está asignado a esta familia.");
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
            hayCambios = false;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show(
                    "Debe seleccionar una familia o subfamilia para aplicar los cambios.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(
                    "Debe seleccionar una familia o subfamilia. No puede aplicar cambios sobre permisos.",
                    "Error",
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
                    "No se puede crear o modificar una familia vacía. Debe agregar al menos un permiso o subfamilia.",
                    "Error",
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
                    MessageBox.Show("No se pudo determinar el ID de la familia seleccionada.");
                    return;
                }

                bll.EliminarPermisosDeFamilia(idFamilia);
            }

            foreach (TreeNode nodo in seleccionado.Nodes)
            {
                if (nodo.Text.StartsWith("[P]"))
                {
                    int idPermiso = (int)nodo.Tag;
                    bll.AsociarPermisoAFamilia(idFamilia, idPermiso);
                }
            }

            MessageBox.Show("Cambios aplicados correctamente.");

            PermisosActualizados?.Invoke();

            CargarFamiliasTreeView();
        }



        private void BTNAgregarFamilia_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            string nombre = TXTNuevaFamiliaNombre.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre de la familia no puede estar vacío.");
                return;
            }

            bool existeEnTV = TVFamilias.Nodes
                .Cast<TreeNode>()
                .Any(n => n.Text.Equals("[F] " + nombre, StringComparison.OrdinalIgnoreCase));

            if (existeEnTV)
            {
                MessageBox.Show("Ya existe una familia con ese nombre en la vista.");
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
            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar una familia para eliminar.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show("Debe seleccionar una familia válida.");
                return;
            }

            var bll = new BLLPerfil();

            var r = MessageBox.Show(
                "¿Confirma que desea eliminar esta familia?",
                "Eliminar",
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
                MessageBox.Show("No se pudo eliminar la familia.");
                return;
            }

            MessageBox.Show("Familia eliminada correctamente.");
            CargarFamiliasTreeView();
        }

        private void BTNEliminarPerfil_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un perfil para eliminar.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show(
                    "Debe seleccionar un perfil (no una familia o permiso dentro del perfil).",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var r = MessageBox.Show(
                "¿Está seguro que desea eliminar este perfil?",
                "Eliminar Perfil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                return;

            if (seleccionado.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show("Perfil eliminado correctamente.");
                return;
            }

            int idPerfil;

            try
            {
                idPerfil = (int)seleccionado.Tag;
            }
            catch
            {
                MessageBox.Show("No se pudo determinar el ID del perfil seleccionado.");
                return;
            }

            BLLPerfil bll = new BLLPerfil();

            bll.EliminarPermisosDePerfil(idPerfil);
            bll.EliminarFamiliasDePerfil(idPerfil);

            bool ok = bll.EliminarPerfil(idPerfil);

            if (!ok)
            {
                MessageBox.Show("No se pudo eliminar el perfil en la base de datos.");
                return;
            }

            MessageBox.Show("Perfil eliminado correctamente.");

            CargarTreeViewPerfiles();
        }

        private void BTNEliminarFamiliaDePerfil_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar una familia dentro del perfil para eliminarla.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[F]") && !seleccionado.Text.StartsWith("[SF]"))
            {
                MessageBox.Show(
                    "Seleccione una familia o subfamilia dentro del perfil. No puede eliminar permisos con este botón.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode root = seleccionado;
            while (root.Parent != null && !root.Text.StartsWith("[PERFIL]"))
                root = root.Parent;

            if (!root.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show("No se encontró el perfil asociado a esta familia.");
                return;
            }

            int cantidadElementos = root.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    "No puede eliminar esta familia porque el perfil quedaría vacío.\nDebe conservar al menos un permiso o familia.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                "¿Confirma que desea quitar esta familia del perfil?",
                "Eliminar familia del perfil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                return;

            int idFamilia = (int)seleccionado.Tag;

            if (root.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show("Familia quitada del perfil.");
                return;
            }

            int idPerfil = (int)root.Tag;

            BLLPerfil bll = new BLLPerfil();

            bool ok = bll.EliminarUnaFamiliaDePerfil(idPerfil, idFamilia);

            if (!ok)
            {
                MessageBox.Show("No se pudo eliminar la familia del perfil en la base de datos.");
                return;
            }

            seleccionado.Remove();

            MessageBox.Show("Familia eliminada correctamente del perfil.");
        }

        private void BTNEliminarPermisoDePerfil_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            TreeNode seleccionado = TVPerfiles.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un permiso dentro del perfil para eliminarlo.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[P]"))
            {
                MessageBox.Show(
                    "Seleccione un permiso dentro del perfil. No puede eliminar familias con este botón.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode root = seleccionado;
            while (root.Parent != null && !root.Text.StartsWith("[PERFIL]"))
                root = root.Parent;

            if (!root.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show("No se encontró el perfil asociado a este permiso.");
                return;
            }
            int cantidadPermisos = root.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]"));

            if (cantidadPermisos <= 1)
            {
                MessageBox.Show(
                    "No puede eliminar este permiso porque el perfil quedaría vacío.\nDebe conservar al menos un permiso.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                "¿Confirma que desea quitar este permiso del perfil?",
                "Eliminar permiso del perfil",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r != DialogResult.Yes)
                return;

            int idPermiso = (int)seleccionado.Tag;

            if (root.Tag is string s && s == "PERFIL_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show("Permiso quitado del perfil.");
                return;
            }

            int idPerfil = (int)root.Tag;

            BLLPerfil bll = new BLLPerfil();

            bool ok = bll.EliminarUnPermisoDePerfil(idPerfil, idPermiso);

            if (!ok)
            {
                MessageBox.Show("No se pudo eliminar el permiso del perfil en la base de datos.");
                return;
            }

            seleccionado.Remove();

            MessageBox.Show("Permiso eliminado correctamente del perfil.");
        }

        private void BTNEliminarFamiliaDeFamilia_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar una subfamilia para eliminarla.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[SF]") && !seleccionado.Text.StartsWith("[F]"))
            {
                MessageBox.Show(
                    "Este botón solo elimina subfamilias. No puede eliminar permisos.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode nodoPadre = seleccionado.Parent;

            if (nodoPadre == null || !(nodoPadre.Text.StartsWith("[F]") || nodoPadre.Text.StartsWith("[SF]")))
            {
                MessageBox.Show("Debe seleccionar una subfamilia dentro de otra familia.");
                return;
            }

            int cantidadElementos = nodoPadre.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]") || n.Text.StartsWith("[SF]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    "No puede eliminar esta subfamilia porque la familia quedaría vacía.\nDebe conservar al menos un permiso o subfamilia.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                "¿Confirma que desea quitar esta subfamilia de la familia?",
                "Eliminar subfamilia",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

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
                MessageBox.Show("No se pudo determinar el ID de la familia seleccionada.");
                return;
            }

            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();

            if ((seleccionado.Tag is string s1 && s1 == "FAM_NEW") ||
                (nodoPadre.Tag is string s2 && s2 == "FAM_NEW"))
            {
                seleccionado.Remove();
                MessageBox.Show("Subfamilia eliminada correctamente.");
                return;
            }

            bool ok = bll.EliminarRelacionFamiliaSubfamilia(idFamiliaPadre, idSubfamilia);

            if (!ok)
            {
                MessageBox.Show("No se pudo eliminar la subfamilia en la base de datos.");
                return;
            }

            seleccionado.Remove();

            MessageBox.Show("Subfamilia eliminada correctamente.");
        }

        private void BTNEliminarPermisoDeFamilia_Click(object sender, EventArgs e)
        {
            hayCambios = true;

            TreeNode seleccionado = TVFamilias.SelectedNode;

            if (seleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un permiso dentro de una familia.");
                return;
            }

            if (!seleccionado.Text.StartsWith("[P]"))
            {
                MessageBox.Show(
                    "Este botón solo elimina permisos. No puede eliminar familias o subfamilias.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            TreeNode nodoPadre = seleccionado.Parent;

            if (nodoPadre == null || !(nodoPadre.Text.StartsWith("[F]") || nodoPadre.Text.StartsWith("[SF]")))
            {
                MessageBox.Show("Debe seleccionar un permiso dentro de una familia o subfamilia.");
                return;
            }

            int cantidadElementos = nodoPadre.Nodes
                .Cast<TreeNode>()
                .Count(n => n.Text.StartsWith("[P]") || n.Text.StartsWith("[F]") || n.Text.StartsWith("[SF]"));

            if (cantidadElementos <= 1)
            {
                MessageBox.Show(
                    "No puede eliminar este permiso porque la familia quedaría vacía.\nDebe conservar al menos un permiso o subfamilia.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var r = MessageBox.Show(
                "¿Confirma que desea quitar este permiso de la familia?",
                "Eliminar permiso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

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
                MessageBox.Show("No se pudo determinar el ID del permiso o familia.");
                return;
            }

            if (nodoPadre.Tag is string s && s == "FAM_NEW")
            {
                seleccionado.Remove();
                MessageBox.Show("Permiso eliminado correctamente.");
                return;
            }

            BLLFamilia_391IAU bll = new BLLFamilia_391IAU();
            bool ok = bll.EliminarPermisoDeFamilia(idFamiliaPadre, idPermiso);

            if (!ok)
            {
                MessageBox.Show("No se pudo eliminar el permiso de la base de datos.");
                return;
            }

            seleccionado.Remove();

            MessageBox.Show("Permiso eliminado correctamente.");
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
            hayCambios = true;

            TreeNode familiaSel = TVFamilias.SelectedNode;
            if (familiaSel == null ||
                (!familiaSel.Text.StartsWith("[F]") && !familiaSel.Text.StartsWith("[SF]")))
            {
                MessageBox.Show("Debe elegir una Familia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TreeNode seleccionadoPerfil = TVPerfiles.SelectedNode;
            if (seleccionadoPerfil == null)
            {
                MessageBox.Show("Debe elegir un Perfil.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TreeNode nodoPerfil = seleccionadoPerfil;
            while (nodoPerfil.Parent != null && !nodoPerfil.Text.StartsWith("[PERFIL]"))
                nodoPerfil = nodoPerfil.Parent;

            if (!nodoPerfil.Text.StartsWith("[PERFIL]"))
            {
                MessageBox.Show("Debe elegir un Perfil válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idFamilia = (int)familiaSel.Tag;
            string nombreFamilia = familiaSel.Text;

            if (nodoPerfil.Nodes.Cast<TreeNode>().Any(n => n.Tag is int id && id == idFamilia))
            {
                MessageBox.Show("Esta familia ya está asignada al perfil.");
                return;
            }

            TreeNode nuevaFamiliaEnPerfil = new TreeNode(nombreFamilia)
            {
                Tag = idFamilia
            };

            nodoPerfil.Nodes.Add(nuevaFamiliaEnPerfil);
            nodoPerfil.Expand();

            MessageBox.Show("Familia agregada correctamente al perfil.");
        }

    }
}
