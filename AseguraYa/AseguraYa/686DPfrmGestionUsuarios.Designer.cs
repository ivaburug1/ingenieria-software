namespace AseguraYa
{
    partial class _686DPfrmGestionUsuarios
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
            this.DP_Datagrid = new System.Windows.Forms.DataGridView();
            this.DP_BTNCrear = new System.Windows.Forms.Button();
            this.DP_BTNDesbloquear = new System.Windows.Forms.Button();
            this.BTNModificar = new System.Windows.Forms.Button();
            this.DP_BTNActivarEliminar = new System.Windows.Forms.Button();
            this.DP_BTNAplicar = new System.Windows.Forms.Button();
            this.DP_BTNSalir = new System.Windows.Forms.Button();
            this.DP_TXTDni = new System.Windows.Forms.TextBox();
            this.DP_TXTApellido = new System.Windows.Forms.TextBox();
            this.DP_TXTNombre = new System.Windows.Forms.TextBox();
            this.DP_TXTEmail = new System.Windows.Forms.TextBox();
            this.DP_TXTMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DP_CMBRol = new System.Windows.Forms.ComboBox();
            this.DP_CMBActDact = new System.Windows.Forms.ComboBox();
            this.DP_CMBBloqueados = new System.Windows.Forms.ComboBox();
            this.DP_CMBRolesFiltro = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DP_BTNFiltrar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.DP_BTNCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DP_Datagrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DP_Datagrid
            // 
            this.DP_Datagrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(176)))), ((int)(((byte)(179)))));
            this.DP_Datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DP_Datagrid.Location = new System.Drawing.Point(39, 74);
            this.DP_Datagrid.Name = "DP_Datagrid";
            this.DP_Datagrid.Size = new System.Drawing.Size(987, 318);
            this.DP_Datagrid.TabIndex = 0;
            this.DP_Datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DP_Datagrid_CellClick_1);
            this.DP_Datagrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DP_Datagrid_CellContentClick);
            // 
            // DP_BTNCrear
            // 
            this.DP_BTNCrear.Location = new System.Drawing.Point(1058, 74);
            this.DP_BTNCrear.Name = "DP_BTNCrear";
            this.DP_BTNCrear.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNCrear.TabIndex = 1;
            this.DP_BTNCrear.Tag = "Crear";
            this.DP_BTNCrear.Text = "Crear";
            this.DP_BTNCrear.UseVisualStyleBackColor = true;
            this.DP_BTNCrear.Click += new System.EventHandler(this.DP_BTNCrear_Click);
            // 
            // DP_BTNDesbloquear
            // 
            this.DP_BTNDesbloquear.Location = new System.Drawing.Point(1058, 123);
            this.DP_BTNDesbloquear.Name = "DP_BTNDesbloquear";
            this.DP_BTNDesbloquear.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNDesbloquear.TabIndex = 2;
            this.DP_BTNDesbloquear.Tag = "Desbloquear";
            this.DP_BTNDesbloquear.Text = "Desbloquear";
            this.DP_BTNDesbloquear.UseVisualStyleBackColor = true;
            this.DP_BTNDesbloquear.Click += new System.EventHandler(this.DP_BTNDesbloquear_Click);
            // 
            // BTNModificar
            // 
            this.BTNModificar.Location = new System.Drawing.Point(1058, 166);
            this.BTNModificar.Name = "BTNModificar";
            this.BTNModificar.Size = new System.Drawing.Size(75, 37);
            this.BTNModificar.TabIndex = 3;
            this.BTNModificar.Tag = "Modificar";
            this.BTNModificar.Text = "Modificar";
            this.BTNModificar.UseVisualStyleBackColor = true;
            this.BTNModificar.Click += new System.EventHandler(this.BTNModificar_Click);
            // 
            // DP_BTNActivarEliminar
            // 
            this.DP_BTNActivarEliminar.Location = new System.Drawing.Point(1058, 209);
            this.DP_BTNActivarEliminar.Name = "DP_BTNActivarEliminar";
            this.DP_BTNActivarEliminar.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNActivarEliminar.TabIndex = 4;
            this.DP_BTNActivarEliminar.Tag = "Activar";
            this.DP_BTNActivarEliminar.Text = "Activar / Eliminar";
            this.DP_BTNActivarEliminar.UseVisualStyleBackColor = true;
            this.DP_BTNActivarEliminar.Click += new System.EventHandler(this.DP_BTNActivarEliminar_Click);
            // 
            // DP_BTNAplicar
            // 
            this.DP_BTNAplicar.Location = new System.Drawing.Point(1058, 252);
            this.DP_BTNAplicar.Name = "DP_BTNAplicar";
            this.DP_BTNAplicar.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNAplicar.TabIndex = 5;
            this.DP_BTNAplicar.Tag = "Aplicar";
            this.DP_BTNAplicar.Text = "Aplicar";
            this.DP_BTNAplicar.UseVisualStyleBackColor = true;
            this.DP_BTNAplicar.Click += new System.EventHandler(this.DP_BTNAplicar_Click);
            // 
            // DP_BTNSalir
            // 
            this.DP_BTNSalir.Location = new System.Drawing.Point(1058, 338);
            this.DP_BTNSalir.Name = "DP_BTNSalir";
            this.DP_BTNSalir.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNSalir.TabIndex = 7;
            this.DP_BTNSalir.Tag = "Salir";
            this.DP_BTNSalir.Text = "Salir";
            this.DP_BTNSalir.UseVisualStyleBackColor = true;
            this.DP_BTNSalir.Click += new System.EventHandler(this.DP_BTNSalir_Click);
            // 
            // DP_TXTDni
            // 
            this.DP_TXTDni.Location = new System.Drawing.Point(142, 404);
            this.DP_TXTDni.Name = "DP_TXTDni";
            this.DP_TXTDni.Size = new System.Drawing.Size(358, 20);
            this.DP_TXTDni.TabIndex = 8;
            this.DP_TXTDni.TextChanged += new System.EventHandler(this.DP_TXTDni_TextChanged);
            // 
            // DP_TXTApellido
            // 
            this.DP_TXTApellido.Location = new System.Drawing.Point(142, 430);
            this.DP_TXTApellido.Name = "DP_TXTApellido";
            this.DP_TXTApellido.Size = new System.Drawing.Size(358, 20);
            this.DP_TXTApellido.TabIndex = 9;
            this.DP_TXTApellido.TextChanged += new System.EventHandler(this.DP_TXTApellido_TextChanged);
            // 
            // DP_TXTNombre
            // 
            this.DP_TXTNombre.Location = new System.Drawing.Point(142, 456);
            this.DP_TXTNombre.Name = "DP_TXTNombre";
            this.DP_TXTNombre.Size = new System.Drawing.Size(358, 20);
            this.DP_TXTNombre.TabIndex = 10;
            this.DP_TXTNombre.TextChanged += new System.EventHandler(this.DP_TXTNombre_TextChanged);
            // 
            // DP_TXTEmail
            // 
            this.DP_TXTEmail.Location = new System.Drawing.Point(142, 482);
            this.DP_TXTEmail.Name = "DP_TXTEmail";
            this.DP_TXTEmail.Size = new System.Drawing.Size(358, 20);
            this.DP_TXTEmail.TabIndex = 11;
            this.DP_TXTEmail.TextChanged += new System.EventHandler(this.DP_TXTEmail_TextChanged);
            // 
            // DP_TXTMessage
            // 
            this.DP_TXTMessage.Location = new System.Drawing.Point(586, 398);
            this.DP_TXTMessage.Multiline = true;
            this.DP_TXTMessage.Name = "DP_TXTMessage";
            this.DP_TXTMessage.ReadOnly = true;
            this.DP_TXTMessage.Size = new System.Drawing.Size(440, 157);
            this.DP_TXTMessage.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 16);
            this.label1.TabIndex = 14;
            this.label1.Tag = "DNI";
            this.label1.Text = "DNI";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 434);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 16);
            this.label2.TabIndex = 15;
            this.label2.Tag = "Apellido";
            this.label2.Text = "Apellido/s";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 460);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 16;
            this.label3.Tag = "Nombre";
            this.label3.Text = "Nombre/s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(39, 486);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 17;
            this.label4.Tag = "Email";
            this.label4.Text = "Email";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(39, 513);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 16);
            this.label5.TabIndex = 18;
            this.label5.Tag = "Rol";
            this.label5.Text = "Rol";
            // 
            // DP_CMBRol
            // 
            this.DP_CMBRol.FormattingEnabled = true;
            this.DP_CMBRol.Items.AddRange(new object[] {
            "General"});
            this.DP_CMBRol.Location = new System.Drawing.Point(142, 508);
            this.DP_CMBRol.Name = "DP_CMBRol";
            this.DP_CMBRol.Size = new System.Drawing.Size(358, 21);
            this.DP_CMBRol.TabIndex = 19;
            this.DP_CMBRol.SelectedIndexChanged += new System.EventHandler(this.DP_CMBRol_SelectedIndexChanged);
            // 
            // DP_CMBActDact
            // 
            this.DP_CMBActDact.FormattingEnabled = true;
            this.DP_CMBActDact.Items.AddRange(new object[] {
            "Activados",
            "Desactivados"});
            this.DP_CMBActDact.Location = new System.Drawing.Point(246, 25);
            this.DP_CMBActDact.Name = "DP_CMBActDact";
            this.DP_CMBActDact.Size = new System.Drawing.Size(121, 21);
            this.DP_CMBActDact.TabIndex = 20;
            // 
            // DP_CMBBloqueados
            // 
            this.DP_CMBBloqueados.FormattingEnabled = true;
            this.DP_CMBBloqueados.Location = new System.Drawing.Point(418, 25);
            this.DP_CMBBloqueados.Name = "DP_CMBBloqueados";
            this.DP_CMBBloqueados.Size = new System.Drawing.Size(121, 21);
            this.DP_CMBBloqueados.TabIndex = 21;
            // 
            // DP_CMBRolesFiltro
            // 
            this.DP_CMBRolesFiltro.FormattingEnabled = true;
            this.DP_CMBRolesFiltro.Location = new System.Drawing.Point(585, 25);
            this.DP_CMBRolesFiltro.Name = "DP_CMBRolesFiltro";
            this.DP_CMBRolesFiltro.Size = new System.Drawing.Size(121, 21);
            this.DP_CMBRolesFiltro.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(141, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 16);
            this.label6.TabIndex = 23;
            this.label6.Tag = "Filtros";
            this.label6.Text = "Filtros";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(243, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 24;
            this.label7.Tag = "Act";
            this.label7.Text = "Activados /Desactivados";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(415, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 25;
            this.label8.Tag = "Bloqueados";
            this.label8.Text = "Usuarios Bloqueados";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(582, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 26;
            this.label9.Tag = "Rol";
            this.label9.Text = "Rol";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // DP_BTNFiltrar
            // 
            this.DP_BTNFiltrar.Location = new System.Drawing.Point(724, 25);
            this.DP_BTNFiltrar.Name = "DP_BTNFiltrar";
            this.DP_BTNFiltrar.Size = new System.Drawing.Size(135, 21);
            this.DP_BTNFiltrar.TabIndex = 27;
            this.DP_BTNFiltrar.Tag = "Filtrar";
            this.DP_BTNFiltrar.Text = "Filtrar";
            this.DP_BTNFiltrar.UseVisualStyleBackColor = true;
            this.DP_BTNFiltrar.Click += new System.EventHandler(this.DP_BTNFiltrar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(874, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 29;
            // 
            // DP_BTNCancelar
            // 
            this.DP_BTNCancelar.Location = new System.Drawing.Point(1058, 295);
            this.DP_BTNCancelar.Name = "DP_BTNCancelar";
            this.DP_BTNCancelar.Size = new System.Drawing.Size(75, 37);
            this.DP_BTNCancelar.TabIndex = 6;
            this.DP_BTNCancelar.Tag = "Cancelar";
            this.DP_BTNCancelar.Text = "Cancelar";
            this.DP_BTNCancelar.UseVisualStyleBackColor = true;
            this.DP_BTNCancelar.Click += new System.EventHandler(this.DP_BTNCancelar_Click);
            // 
            // _686DPfrmGestionUsuarios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(1145, 565);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.DP_BTNFiltrar);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DP_CMBRolesFiltro);
            this.Controls.Add(this.DP_CMBBloqueados);
            this.Controls.Add(this.DP_CMBActDact);
            this.Controls.Add(this.DP_CMBRol);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DP_TXTMessage);
            this.Controls.Add(this.DP_TXTEmail);
            this.Controls.Add(this.DP_TXTNombre);
            this.Controls.Add(this.DP_TXTApellido);
            this.Controls.Add(this.DP_TXTDni);
            this.Controls.Add(this.DP_BTNSalir);
            this.Controls.Add(this.DP_BTNCancelar);
            this.Controls.Add(this.DP_BTNAplicar);
            this.Controls.Add(this.DP_BTNActivarEliminar);
            this.Controls.Add(this.BTNModificar);
            this.Controls.Add(this.DP_BTNDesbloquear);
            this.Controls.Add(this.DP_BTNCrear);
            this.Controls.Add(this.DP_Datagrid);
            this.Name = "_686DPfrmGestionUsuarios";
            this.Tag = "Gestion de usuarios";
            this.Text = "Gestion de usuarios";
            this.Load += new System.EventHandler(this._686DPfrmGestionUsuarios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DP_Datagrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DP_Datagrid;
        private System.Windows.Forms.Button DP_BTNCrear;
        private System.Windows.Forms.Button DP_BTNDesbloquear;
        private System.Windows.Forms.Button BTNModificar;
        private System.Windows.Forms.Button DP_BTNActivarEliminar;
        private System.Windows.Forms.Button DP_BTNAplicar;
        private System.Windows.Forms.Button DP_BTNSalir;
        private System.Windows.Forms.TextBox DP_TXTDni;
        private System.Windows.Forms.TextBox DP_TXTApellido;
        private System.Windows.Forms.TextBox DP_TXTNombre;
        private System.Windows.Forms.TextBox DP_TXTEmail;
        private System.Windows.Forms.TextBox DP_TXTMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox DP_CMBRol;
        private System.Windows.Forms.ComboBox DP_CMBActDact;
        private System.Windows.Forms.ComboBox DP_CMBBloqueados;
        private System.Windows.Forms.ComboBox DP_CMBRolesFiltro;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button DP_BTNFiltrar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button DP_BTNCancelar;
    }
}