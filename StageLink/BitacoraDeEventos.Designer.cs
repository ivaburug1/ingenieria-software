namespace StageLink
{
    partial class but
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(but));
            this.DTPFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.DGVMuestraBitacora = new System.Windows.Forms.DataGridView();
            this.DTPFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.CBModulo = new System.Windows.Forms.ComboBox();
            this.CBCriticidad = new System.Windows.Forms.ComboBox();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.TXTApellido = new System.Windows.Forms.TextBox();
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.BTNLimpiarFiltro = new System.Windows.Forms.Button();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLFechaDesde = new System.Windows.Forms.Label();
            this.LBLModulo = new System.Windows.Forms.Label();
            this.LBLNombre = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLFechaHasta = new System.Windows.Forms.Label();
            this.LBLCantidad = new System.Windows.Forms.Label();
            this.LBLApellido = new System.Windows.Forms.Label();
            this.LBLCariticidad = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraBitacora)).BeginInit();
            this.SuspendLayout();
            // 
            // DTPFechaDesde
            // 
            this.DTPFechaDesde.Location = new System.Drawing.Point(8, 251);
            this.DTPFechaDesde.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DTPFechaDesde.Name = "DTPFechaDesde";
            this.DTPFechaDesde.Size = new System.Drawing.Size(207, 20);
            this.DTPFechaDesde.TabIndex = 0;
            // 
            // DGVMuestraBitacora
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMuestraBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMuestraBitacora.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVMuestraBitacora.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVMuestraBitacora.Location = new System.Drawing.Point(8, 25);
            this.DGVMuestraBitacora.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DGVMuestraBitacora.Name = "DGVMuestraBitacora";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMuestraBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVMuestraBitacora.RowTemplate.Height = 28;
            this.DGVMuestraBitacora.Size = new System.Drawing.Size(768, 207);
            this.DGVMuestraBitacora.TabIndex = 1;
            this.DGVMuestraBitacora.SelectionChanged += new System.EventHandler(this.DGVMuestraBitacora_SelectionChanged);
            // 
            // DTPFechaHasta
            // 
            this.DTPFechaHasta.Location = new System.Drawing.Point(8, 303);
            this.DTPFechaHasta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DTPFechaHasta.Name = "DTPFechaHasta";
            this.DTPFechaHasta.Size = new System.Drawing.Size(207, 20);
            this.DTPFechaHasta.TabIndex = 2;
            // 
            // CBModulo
            // 
            this.CBModulo.FormattingEnabled = true;
            this.CBModulo.Location = new System.Drawing.Point(217, 251);
            this.CBModulo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBModulo.Name = "CBModulo";
            this.CBModulo.Size = new System.Drawing.Size(89, 21);
            this.CBModulo.TabIndex = 3;
            // 
            // CBCriticidad
            // 
            this.CBCriticidad.FormattingEnabled = true;
            this.CBCriticidad.Location = new System.Drawing.Point(217, 302);
            this.CBCriticidad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBCriticidad.Name = "CBCriticidad";
            this.CBCriticidad.Size = new System.Drawing.Size(89, 21);
            this.CBCriticidad.TabIndex = 4;
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(309, 252);
            this.TXTNombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(96, 20);
            this.TXTNombre.TabIndex = 5;
            // 
            // TXTApellido
            // 
            this.TXTApellido.Location = new System.Drawing.Point(309, 302);
            this.TXTApellido.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TXTApellido.Name = "TXTApellido";
            this.TXTApellido.Size = new System.Drawing.Size(96, 20);
            this.TXTApellido.TabIndex = 6;
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(408, 252);
            this.TXTDNI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(96, 20);
            this.TXTDNI.TabIndex = 7;
            // 
            // BTNLimpiarFiltro
            // 
            this.BTNLimpiarFiltro.Location = new System.Drawing.Point(508, 296);
            this.BTNLimpiarFiltro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNLimpiarFiltro.Name = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Size = new System.Drawing.Size(81, 30);
            this.BTNLimpiarFiltro.TabIndex = 8;
            this.BTNLimpiarFiltro.Tag = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Text = "Limpiar Filtro";
            this.BTNLimpiarFiltro.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltro.Click += new System.EventHandler(this.BTNLimpiarFiltro_Click);
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(409, 296);
            this.BTNFiltrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(95, 30);
            this.BTNFiltrar.TabIndex = 9;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click_1);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(695, 296);
            this.BTNSalir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(81, 30);
            this.BTNSalir.TabIndex = 10;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(50)))), ((int)(((byte)(41)))));
            this.LBLTitulo.Location = new System.Drawing.Point(8, 6);
            this.LBLTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(105, 13);
            this.LBLTitulo.TabIndex = 11;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Auditoria de Eventos";
            // 
            // LBLFechaDesde
            // 
            this.LBLFechaDesde.AutoSize = true;
            this.LBLFechaDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLFechaDesde.Location = new System.Drawing.Point(8, 236);
            this.LBLFechaDesde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLFechaDesde.Name = "LBLFechaDesde";
            this.LBLFechaDesde.Size = new System.Drawing.Size(71, 13);
            this.LBLFechaDesde.TabIndex = 12;
            this.LBLFechaDesde.Tag = "LBLFechaDesde";
            this.LBLFechaDesde.Text = "Fecha Desde";
            // 
            // LBLModulo
            // 
            this.LBLModulo.AutoSize = true;
            this.LBLModulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLModulo.Location = new System.Drawing.Point(217, 236);
            this.LBLModulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLModulo.Name = "LBLModulo";
            this.LBLModulo.Size = new System.Drawing.Size(42, 13);
            this.LBLModulo.TabIndex = 13;
            this.LBLModulo.Tag = "LBLModulo";
            this.LBLModulo.Text = "Modulo";
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLNombre.Location = new System.Drawing.Point(309, 237);
            this.LBLNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(44, 13);
            this.LBLNombre.TabIndex = 14;
            this.LBLNombre.Tag = "LBLNombre";
            this.LBLNombre.Text = "Nombre";
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLDNI.Location = new System.Drawing.Point(405, 236);
            this.LBLDNI.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(26, 13);
            this.LBLDNI.TabIndex = 15;
            this.LBLDNI.Tag = "LBLDNI";
            this.LBLDNI.Text = "DNI";
            // 
            // LBLFechaHasta
            // 
            this.LBLFechaHasta.AutoSize = true;
            this.LBLFechaHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLFechaHasta.Location = new System.Drawing.Point(8, 288);
            this.LBLFechaHasta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLFechaHasta.Name = "LBLFechaHasta";
            this.LBLFechaHasta.Size = new System.Drawing.Size(68, 13);
            this.LBLFechaHasta.TabIndex = 16;
            this.LBLFechaHasta.Tag = "LBLFechaHasta";
            this.LBLFechaHasta.Text = "Fecha Hasta";
            // 
            // LBLCantidad
            // 
            this.LBLCantidad.AutoSize = true;
            this.LBLCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLCantidad.Location = new System.Drawing.Point(439, 6);
            this.LBLCantidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLCantidad.Name = "LBLCantidad";
            this.LBLCantidad.Size = new System.Drawing.Size(49, 13);
            this.LBLCantidad.TabIndex = 17;
            this.LBLCantidad.Tag = "LBLCantidad";
            this.LBLCantidad.Text = "Cantidad";
            // 
            // LBLApellido
            // 
            this.LBLApellido.AutoSize = true;
            this.LBLApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLApellido.Location = new System.Drawing.Point(309, 287);
            this.LBLApellido.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLApellido.Name = "LBLApellido";
            this.LBLApellido.Size = new System.Drawing.Size(44, 13);
            this.LBLApellido.TabIndex = 18;
            this.LBLApellido.Tag = "LBLApellido";
            this.LBLApellido.Text = "Apellido";
            // 
            // LBLCariticidad
            // 
            this.LBLCariticidad.AutoSize = true;
            this.LBLCariticidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLCariticidad.Location = new System.Drawing.Point(217, 287);
            this.LBLCariticidad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLCariticidad.Name = "LBLCariticidad";
            this.LBLCariticidad.Size = new System.Drawing.Size(50, 13);
            this.LBLCariticidad.TabIndex = 19;
            this.LBLCariticidad.Tag = "LBLCariticidad";
            this.LBLCariticidad.Text = "Criticidad";
            // 
            // but
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(787, 340);
            this.Controls.Add(this.LBLCariticidad);
            this.Controls.Add(this.LBLApellido);
            this.Controls.Add(this.LBLCantidad);
            this.Controls.Add(this.LBLFechaHasta);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.LBLModulo);
            this.Controls.Add(this.LBLFechaDesde);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.BTNLimpiarFiltro);
            this.Controls.Add(this.TXTDNI);
            this.Controls.Add(this.TXTApellido);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.CBCriticidad);
            this.Controls.Add(this.CBModulo);
            this.Controls.Add(this.DTPFechaHasta);
            this.Controls.Add(this.DGVMuestraBitacora);
            this.Controls.Add(this.DTPFechaDesde);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "but";
            this.Text = "BitacoraDeEventos";
            this.Load += new System.EventHandler(this.but_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraBitacora)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DTPFechaDesde;
        private System.Windows.Forms.DataGridView DGVMuestraBitacora;
        private System.Windows.Forms.DateTimePicker DTPFechaHasta;
        private System.Windows.Forms.ComboBox CBModulo;
        private System.Windows.Forms.ComboBox CBCriticidad;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.TextBox TXTApellido;
        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.Button BTNLimpiarFiltro;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLFechaDesde;
        private System.Windows.Forms.Label LBLModulo;
        private System.Windows.Forms.Label LBLNombre;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLFechaHasta;
        private System.Windows.Forms.Label LBLCantidad;
        private System.Windows.Forms.Label LBLApellido;
        private System.Windows.Forms.Label LBLCariticidad;
    }
}