namespace StageLink
{
    partial class AuditoriaDeCambios
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
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.DGVCargaProductoCambio = new System.Windows.Forms.DataGridView();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.LBLNombre = new System.Windows.Forms.Label();
            this.DTPFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.DTPFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.LBLFechaDesde = new System.Windows.Forms.Label();
            this.LBLFechaHasta = new System.Windows.Forms.Label();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltro = new System.Windows.Forms.Button();
            this.BTNActivarProducto = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVCargaProductoCambio)).BeginInit();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.Location = new System.Drawing.Point(8, 6);
            this.LBLTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(106, 13);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Auditoria de Cambios";
            // 
            // DGVCargaProductoCambio
            // 
            this.DGVCargaProductoCambio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVCargaProductoCambio.Location = new System.Drawing.Point(11, 31);
            this.DGVCargaProductoCambio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DGVCargaProductoCambio.Name = "DGVCargaProductoCambio";
            this.DGVCargaProductoCambio.RowTemplate.Height = 28;
            this.DGVCargaProductoCambio.Size = new System.Drawing.Size(577, 216);
            this.DGVCargaProductoCambio.TabIndex = 1;
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(11, 277);
            this.TXTNombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(207, 20);
            this.TXTNombre.TabIndex = 2;
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.Location = new System.Drawing.Point(8, 262);
            this.LBLNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(90, 13);
            this.LBLNombre.TabIndex = 3;
            this.LBLNombre.Tag = "LBLNombre";
            this.LBLNombre.Text = "Nombre Producto";
            // 
            // DTPFechaDesde
            // 
            this.DTPFechaDesde.Location = new System.Drawing.Point(11, 314);
            this.DTPFechaDesde.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DTPFechaDesde.Name = "DTPFechaDesde";
            this.DTPFechaDesde.Size = new System.Drawing.Size(207, 20);
            this.DTPFechaDesde.TabIndex = 4;
            // 
            // DTPFechaHasta
            // 
            this.DTPFechaHasta.Location = new System.Drawing.Point(11, 351);
            this.DTPFechaHasta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DTPFechaHasta.Name = "DTPFechaHasta";
            this.DTPFechaHasta.Size = new System.Drawing.Size(207, 20);
            this.DTPFechaHasta.TabIndex = 5;
            // 
            // LBLFechaDesde
            // 
            this.LBLFechaDesde.AutoSize = true;
            this.LBLFechaDesde.Location = new System.Drawing.Point(8, 299);
            this.LBLFechaDesde.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLFechaDesde.Name = "LBLFechaDesde";
            this.LBLFechaDesde.Size = new System.Drawing.Size(71, 13);
            this.LBLFechaDesde.TabIndex = 6;
            this.LBLFechaDesde.Tag = "LBLFechaDesde";
            this.LBLFechaDesde.Text = "Fecha Desde";
            // 
            // LBLFechaHasta
            // 
            this.LBLFechaHasta.AutoSize = true;
            this.LBLFechaHasta.Location = new System.Drawing.Point(8, 336);
            this.LBLFechaHasta.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLFechaHasta.Name = "LBLFechaHasta";
            this.LBLFechaHasta.Size = new System.Drawing.Size(68, 13);
            this.LBLFechaHasta.TabIndex = 7;
            this.LBLFechaHasta.Tag = "LBLFechaHasta";
            this.LBLFechaHasta.Text = "Fecha Hasta";
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(220, 315);
            this.BTNFiltrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(68, 25);
            this.BTNFiltrar.TabIndex = 8;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "FIltrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // BTNLimpiarFiltro
            // 
            this.BTNLimpiarFiltro.Location = new System.Drawing.Point(292, 315);
            this.BTNLimpiarFiltro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNLimpiarFiltro.Name = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Size = new System.Drawing.Size(89, 25);
            this.BTNLimpiarFiltro.TabIndex = 9;
            this.BTNLimpiarFiltro.Tag = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Text = "Limpiar Filtro";
            this.BTNLimpiarFiltro.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltro.Click += new System.EventHandler(this.BTNLimpiarFiltro_Click);
            // 
            // BTNActivarProducto
            // 
            this.BTNActivarProducto.Location = new System.Drawing.Point(220, 344);
            this.BTNActivarProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNActivarProducto.Name = "BTNActivarProducto";
            this.BTNActivarProducto.Size = new System.Drawing.Size(161, 25);
            this.BTNActivarProducto.TabIndex = 10;
            this.BTNActivarProducto.Tag = "BTNActivarProducto";
            this.BTNActivarProducto.Text = "Activar Producto";
            this.BTNActivarProducto.UseVisualStyleBackColor = true;
            this.BTNActivarProducto.Click += new System.EventHandler(this.BTNActivarProducto_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(519, 344);
            this.BTNSalir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(68, 25);
            this.BTNSalir.TabIndex = 11;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // AuditoriaDeCambios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(595, 374);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNActivarProducto);
            this.Controls.Add(this.BTNLimpiarFiltro);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.LBLFechaHasta);
            this.Controls.Add(this.LBLFechaDesde);
            this.Controls.Add(this.DTPFechaHasta);
            this.Controls.Add(this.DTPFechaDesde);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.DGVCargaProductoCambio);
            this.Controls.Add(this.LBLTitulo);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AuditoriaDeCambios";
            this.Text = "AuditoriaDeCambios";
            this.Load += new System.EventHandler(this.AuditoriaDeCambios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVCargaProductoCambio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.DataGridView DGVCargaProductoCambio;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.Label LBLNombre;
        private System.Windows.Forms.DateTimePicker DTPFechaDesde;
        private System.Windows.Forms.DateTimePicker DTPFechaHasta;
        private System.Windows.Forms.Label LBLFechaDesde;
        private System.Windows.Forms.Label LBLFechaHasta;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button BTNLimpiarFiltro;
        private System.Windows.Forms.Button BTNActivarProducto;
        private System.Windows.Forms.Button BTNSalir;
    }
}