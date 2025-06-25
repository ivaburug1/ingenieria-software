namespace StageLink
{
    partial class GestionDeUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionDeUsuarios));
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLFiltros = new System.Windows.Forms.Label();
            this.LBLActivoNoActivo = new System.Windows.Forms.Label();
            this.LBLUsuariosBloqueados = new System.Windows.Forms.Label();
            this.LBLRol = new System.Windows.Forms.Label();
            this.LBLBuscar = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLNombre = new System.Windows.Forms.Label();
            this.LBLApellido = new System.Windows.Forms.Label();
            this.LBLCorreo = new System.Windows.Forms.Label();
            this.LBLIdioma = new System.Windows.Forms.Label();
            this.CBActivoNoActivo = new System.Windows.Forms.ComboBox();
            this.CBUsuariosBloqueados = new System.Windows.Forms.ComboBox();
            this.CBRol = new System.Windows.Forms.ComboBox();
            this.CBIdioma = new System.Windows.Forms.ComboBox();
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.TXTApellido = new System.Windows.Forms.TextBox();
            this.TXTCorreo = new System.Windows.Forms.TextBox();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.DGVMuestraUsuarios = new System.Windows.Forms.DataGridView();
            this.BTNActivar = new System.Windows.Forms.Button();
            this.BTNDesactivar = new System.Windows.Forms.Button();
            this.BTNModificar = new System.Windows.Forms.Button();
            this.BTNDesbloquear = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNBuscar = new System.Windows.Forms.Button();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltros = new System.Windows.Forms.Button();
            this.BTNLimpiarBusqueda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraUsuarios)).BeginInit();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(225, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(207, 25);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Text = "Gestion de Usuarios";
            // 
            // LBLFiltros
            // 
            this.LBLFiltros.AutoSize = true;
            this.LBLFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(116)))), ((int)(((byte)(255)))));
            this.LBLFiltros.Location = new System.Drawing.Point(12, 70);
            this.LBLFiltros.Name = "LBLFiltros";
            this.LBLFiltros.Size = new System.Drawing.Size(34, 13);
            this.LBLFiltros.TabIndex = 1;
            this.LBLFiltros.Text = "Filtros";
            // 
            // LBLActivoNoActivo
            // 
            this.LBLActivoNoActivo.AutoSize = true;
            this.LBLActivoNoActivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLActivoNoActivo.Location = new System.Drawing.Point(133, 48);
            this.LBLActivoNoActivo.Name = "LBLActivoNoActivo";
            this.LBLActivoNoActivo.Size = new System.Drawing.Size(95, 13);
            this.LBLActivoNoActivo.TabIndex = 2;
            this.LBLActivoNoActivo.Text = "Activo / No Activo";
            // 
            // LBLUsuariosBloqueados
            // 
            this.LBLUsuariosBloqueados.AutoSize = true;
            this.LBLUsuariosBloqueados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLUsuariosBloqueados.Location = new System.Drawing.Point(260, 48);
            this.LBLUsuariosBloqueados.Name = "LBLUsuariosBloqueados";
            this.LBLUsuariosBloqueados.Size = new System.Drawing.Size(107, 13);
            this.LBLUsuariosBloqueados.TabIndex = 3;
            this.LBLUsuariosBloqueados.Text = "Usuarios Bloqueados";
            // 
            // LBLRol
            // 
            this.LBLRol.AutoSize = true;
            this.LBLRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLRol.Location = new System.Drawing.Point(390, 48);
            this.LBLRol.Name = "LBLRol";
            this.LBLRol.Size = new System.Drawing.Size(23, 13);
            this.LBLRol.TabIndex = 4;
            this.LBLRol.Text = "Rol";
            // 
            // LBLBuscar
            // 
            this.LBLBuscar.AutoSize = true;
            this.LBLBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLBuscar.Location = new System.Drawing.Point(12, 420);
            this.LBLBuscar.Name = "LBLBuscar";
            this.LBLBuscar.Size = new System.Drawing.Size(40, 13);
            this.LBLBuscar.TabIndex = 5;
            this.LBLBuscar.Text = "Buscar";
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLDNI.Location = new System.Drawing.Point(71, 396);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(26, 13);
            this.LBLDNI.TabIndex = 6;
            this.LBLDNI.Text = "DNI";
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLNombre.Location = new System.Drawing.Point(177, 396);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(44, 13);
            this.LBLNombre.TabIndex = 7;
            this.LBLNombre.Text = "Nombre";
            // 
            // LBLApellido
            // 
            this.LBLApellido.AutoSize = true;
            this.LBLApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLApellido.Location = new System.Drawing.Point(283, 396);
            this.LBLApellido.Name = "LBLApellido";
            this.LBLApellido.Size = new System.Drawing.Size(44, 13);
            this.LBLApellido.TabIndex = 8;
            this.LBLApellido.Text = "Apellido";
            // 
            // LBLCorreo
            // 
            this.LBLCorreo.AutoSize = true;
            this.LBLCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLCorreo.Location = new System.Drawing.Point(389, 396);
            this.LBLCorreo.Name = "LBLCorreo";
            this.LBLCorreo.Size = new System.Drawing.Size(35, 13);
            this.LBLCorreo.TabIndex = 9;
            this.LBLCorreo.Text = "e-Mail";
            // 
            // LBLIdioma
            // 
            this.LBLIdioma.AutoSize = true;
            this.LBLIdioma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLIdioma.Location = new System.Drawing.Point(495, 396);
            this.LBLIdioma.Name = "LBLIdioma";
            this.LBLIdioma.Size = new System.Drawing.Size(38, 13);
            this.LBLIdioma.TabIndex = 10;
            this.LBLIdioma.Text = "Idioma";
            // 
            // CBActivoNoActivo
            // 
            this.CBActivoNoActivo.FormattingEnabled = true;
            this.CBActivoNoActivo.Items.AddRange(new object[] {
            "Activo",
            "No Activo"});
            this.CBActivoNoActivo.Location = new System.Drawing.Point(136, 70);
            this.CBActivoNoActivo.Name = "CBActivoNoActivo";
            this.CBActivoNoActivo.Size = new System.Drawing.Size(121, 21);
            this.CBActivoNoActivo.TabIndex = 11;
            // 
            // CBUsuariosBloqueados
            // 
            this.CBUsuariosBloqueados.FormattingEnabled = true;
            this.CBUsuariosBloqueados.Items.AddRange(new object[] {
            "Bloqueado",
            "Desbloqueado"});
            this.CBUsuariosBloqueados.Location = new System.Drawing.Point(263, 70);
            this.CBUsuariosBloqueados.Name = "CBUsuariosBloqueados";
            this.CBUsuariosBloqueados.Size = new System.Drawing.Size(121, 21);
            this.CBUsuariosBloqueados.TabIndex = 12;
            // 
            // CBRol
            // 
            this.CBRol.FormattingEnabled = true;
            this.CBRol.Location = new System.Drawing.Point(390, 70);
            this.CBRol.Name = "CBRol";
            this.CBRol.Size = new System.Drawing.Size(121, 21);
            this.CBRol.TabIndex = 13;
            // 
            // CBIdioma
            // 
            this.CBIdioma.FormattingEnabled = true;
            this.CBIdioma.Items.AddRange(new object[] {
            "Español",
            "Ingles",
            "Japones",
            "Portugues",
            "Coreano"});
            this.CBIdioma.Location = new System.Drawing.Point(498, 417);
            this.CBIdioma.Name = "CBIdioma";
            this.CBIdioma.Size = new System.Drawing.Size(117, 21);
            this.CBIdioma.TabIndex = 14;
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(74, 417);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(96, 20);
            this.TXTDNI.TabIndex = 15;
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(180, 417);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(96, 20);
            this.TXTNombre.TabIndex = 16;
            // 
            // TXTApellido
            // 
            this.TXTApellido.Location = new System.Drawing.Point(286, 417);
            this.TXTApellido.Name = "TXTApellido";
            this.TXTApellido.Size = new System.Drawing.Size(96, 20);
            this.TXTApellido.TabIndex = 17;
            // 
            // TXTCorreo
            // 
            this.TXTCorreo.Location = new System.Drawing.Point(392, 417);
            this.TXTCorreo.Name = "TXTCorreo";
            this.TXTCorreo.Size = new System.Drawing.Size(96, 20);
            this.TXTCorreo.TabIndex = 18;
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(517, 68);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(75, 23);
            this.BTNFiltrar.TabIndex = 19;
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // DGVMuestraUsuarios
            // 
            this.DGVMuestraUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMuestraUsuarios.Location = new System.Drawing.Point(12, 106);
            this.DGVMuestraUsuarios.Name = "DGVMuestraUsuarios";
            this.DGVMuestraUsuarios.Size = new System.Drawing.Size(841, 273);
            this.DGVMuestraUsuarios.TabIndex = 20;
            // 
            // BTNActivar
            // 
            this.BTNActivar.Location = new System.Drawing.Point(859, 147);
            this.BTNActivar.Name = "BTNActivar";
            this.BTNActivar.Size = new System.Drawing.Size(75, 23);
            this.BTNActivar.TabIndex = 21;
            this.BTNActivar.Text = "Activar";
            this.BTNActivar.UseVisualStyleBackColor = true;
            this.BTNActivar.Click += new System.EventHandler(this.BTNActivar_Click);
            // 
            // BTNDesactivar
            // 
            this.BTNDesactivar.Location = new System.Drawing.Point(859, 176);
            this.BTNDesactivar.Name = "BTNDesactivar";
            this.BTNDesactivar.Size = new System.Drawing.Size(75, 23);
            this.BTNDesactivar.TabIndex = 22;
            this.BTNDesactivar.Text = "Desactivar";
            this.BTNDesactivar.UseVisualStyleBackColor = true;
            this.BTNDesactivar.Click += new System.EventHandler(this.BTNDesactivar_Click);
            // 
            // BTNModificar
            // 
            this.BTNModificar.Location = new System.Drawing.Point(859, 205);
            this.BTNModificar.Name = "BTNModificar";
            this.BTNModificar.Size = new System.Drawing.Size(75, 23);
            this.BTNModificar.TabIndex = 23;
            this.BTNModificar.Text = "Modificar";
            this.BTNModificar.UseVisualStyleBackColor = true;
            this.BTNModificar.Click += new System.EventHandler(this.BTNModificar_Click);
            // 
            // BTNDesbloquear
            // 
            this.BTNDesbloquear.Location = new System.Drawing.Point(859, 234);
            this.BTNDesbloquear.Name = "BTNDesbloquear";
            this.BTNDesbloquear.Size = new System.Drawing.Size(75, 23);
            this.BTNDesbloquear.TabIndex = 24;
            this.BTNDesbloquear.Text = "Desbloquear";
            this.BTNDesbloquear.UseVisualStyleBackColor = true;
            this.BTNDesbloquear.Click += new System.EventHandler(this.BTNDesbloquear_Click);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(859, 292);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 26;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNBuscar
            // 
            this.BTNBuscar.Location = new System.Drawing.Point(624, 415);
            this.BTNBuscar.Name = "BTNBuscar";
            this.BTNBuscar.Size = new System.Drawing.Size(75, 23);
            this.BTNBuscar.TabIndex = 27;
            this.BTNBuscar.Text = "Buscar";
            this.BTNBuscar.UseVisualStyleBackColor = true;
            this.BTNBuscar.Click += new System.EventHandler(this.BTNBuscar_Click);
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(859, 263);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(75, 23);
            this.BTNAplicar.TabIndex = 25;
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click);
            // 
            // BTNLimpiarFiltros
            // 
            this.BTNLimpiarFiltros.Location = new System.Drawing.Point(598, 68);
            this.BTNLimpiarFiltros.Name = "BTNLimpiarFiltros";
            this.BTNLimpiarFiltros.Size = new System.Drawing.Size(101, 23);
            this.BTNLimpiarFiltros.TabIndex = 28;
            this.BTNLimpiarFiltros.Text = "Limpiar Filtros";
            this.BTNLimpiarFiltros.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltros.Click += new System.EventHandler(this.BTNLimpiarFiltros_Click);
            // 
            // BTNLimpiarBusqueda
            // 
            this.BTNLimpiarBusqueda.Location = new System.Drawing.Point(705, 415);
            this.BTNLimpiarBusqueda.Name = "BTNLimpiarBusqueda";
            this.BTNLimpiarBusqueda.Size = new System.Drawing.Size(112, 23);
            this.BTNLimpiarBusqueda.TabIndex = 29;
            this.BTNLimpiarBusqueda.Text = "Limpiar Busqueda";
            this.BTNLimpiarBusqueda.UseVisualStyleBackColor = true;
            this.BTNLimpiarBusqueda.Click += new System.EventHandler(this.BTNLimpiarBusqueda_Click);
            // 
            // GestionDeUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(957, 450);
            this.Controls.Add(this.BTNLimpiarBusqueda);
            this.Controls.Add(this.BTNLimpiarFiltros);
            this.Controls.Add(this.BTNBuscar);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAplicar);
            this.Controls.Add(this.BTNDesbloquear);
            this.Controls.Add(this.BTNModificar);
            this.Controls.Add(this.BTNDesactivar);
            this.Controls.Add(this.BTNActivar);
            this.Controls.Add(this.DGVMuestraUsuarios);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.TXTCorreo);
            this.Controls.Add(this.TXTApellido);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.TXTDNI);
            this.Controls.Add(this.CBIdioma);
            this.Controls.Add(this.CBRol);
            this.Controls.Add(this.CBUsuariosBloqueados);
            this.Controls.Add(this.CBActivoNoActivo);
            this.Controls.Add(this.LBLIdioma);
            this.Controls.Add(this.LBLCorreo);
            this.Controls.Add(this.LBLApellido);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLBuscar);
            this.Controls.Add(this.LBLRol);
            this.Controls.Add(this.LBLUsuariosBloqueados);
            this.Controls.Add(this.LBLActivoNoActivo);
            this.Controls.Add(this.LBLFiltros);
            this.Controls.Add(this.LBLTitulo);
            this.Name = "GestionDeUsuarios";
            this.Text = "GestionDeUsuarios";
            this.Load += new System.EventHandler(this.GestionDeUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraUsuarios)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLFiltros;
        private System.Windows.Forms.Label LBLActivoNoActivo;
        private System.Windows.Forms.Label LBLUsuariosBloqueados;
        private System.Windows.Forms.Label LBLRol;
        private System.Windows.Forms.Label LBLBuscar;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLNombre;
        private System.Windows.Forms.Label LBLApellido;
        private System.Windows.Forms.Label LBLCorreo;
        private System.Windows.Forms.Label LBLIdioma;
        private System.Windows.Forms.ComboBox CBActivoNoActivo;
        private System.Windows.Forms.ComboBox CBUsuariosBloqueados;
        private System.Windows.Forms.ComboBox CBRol;
        private System.Windows.Forms.ComboBox CBIdioma;
        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.TextBox TXTApellido;
        private System.Windows.Forms.TextBox TXTCorreo;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.DataGridView DGVMuestraUsuarios;
        private System.Windows.Forms.Button BTNActivar;
        private System.Windows.Forms.Button BTNDesactivar;
        private System.Windows.Forms.Button BTNModificar;
        private System.Windows.Forms.Button BTNDesbloquear;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNBuscar;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.Button BTNLimpiarFiltros;
        private System.Windows.Forms.Button BTNLimpiarBusqueda;
    }
}