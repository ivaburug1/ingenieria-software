namespace StageLink
{
    partial class ReporteRFN2
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
            this.DGVReporteRFN2 = new System.Windows.Forms.DataGridView();
            this.LBLTipoProducto = new System.Windows.Forms.Label();
            this.LBLNombreProducto = new System.Windows.Forms.Label();
            this.LBLNombreProveedor = new System.Windows.Forms.Label();
            this.CBTipoProducto = new System.Windows.Forms.ComboBox();
            this.CBNombreProducto = new System.Windows.Forms.ComboBox();
            this.CBNombreProveedor = new System.Windows.Forms.ComboBox();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltro = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.BTNImprimirReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteRFN2)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVReporteRFN2
            // 
            this.DGVReporteRFN2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReporteRFN2.Location = new System.Drawing.Point(11, 9);
            this.DGVReporteRFN2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DGVReporteRFN2.Name = "DGVReporteRFN2";
            this.DGVReporteRFN2.RowTemplate.Height = 28;
            this.DGVReporteRFN2.Size = new System.Drawing.Size(872, 404);
            this.DGVReporteRFN2.TabIndex = 0;
            // 
            // LBLTipoProducto
            // 
            this.LBLTipoProducto.AutoSize = true;
            this.LBLTipoProducto.Location = new System.Drawing.Point(884, 10);
            this.LBLTipoProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLTipoProducto.Name = "LBLTipoProducto";
            this.LBLTipoProducto.Size = new System.Drawing.Size(89, 13);
            this.LBLTipoProducto.TabIndex = 1;
            this.LBLTipoProducto.Tag = "LBLTipoProducto";
            this.LBLTipoProducto.Text = "Tipo de Producto";
            // 
            // LBLNombreProducto
            // 
            this.LBLNombreProducto.AutoSize = true;
            this.LBLNombreProducto.Location = new System.Drawing.Point(884, 48);
            this.LBLNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLNombreProducto.Name = "LBLNombreProducto";
            this.LBLNombreProducto.Size = new System.Drawing.Size(90, 13);
            this.LBLNombreProducto.TabIndex = 2;
            this.LBLNombreProducto.Tag = "LBLNombreProducto";
            this.LBLNombreProducto.Text = "Nombre Producto";
            // 
            // LBLNombreProveedor
            // 
            this.LBLNombreProveedor.AutoSize = true;
            this.LBLNombreProveedor.Location = new System.Drawing.Point(884, 90);
            this.LBLNombreProveedor.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LBLNombreProveedor.Name = "LBLNombreProveedor";
            this.LBLNombreProveedor.Size = new System.Drawing.Size(96, 13);
            this.LBLNombreProveedor.TabIndex = 3;
            this.LBLNombreProveedor.Tag = "LBLNombreProveedor";
            this.LBLNombreProveedor.Text = "Nombre Proveedor";
            // 
            // CBTipoProducto
            // 
            this.CBTipoProducto.FormattingEnabled = true;
            this.CBTipoProducto.Location = new System.Drawing.Point(887, 25);
            this.CBTipoProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBTipoProducto.Name = "CBTipoProducto";
            this.CBTipoProducto.Size = new System.Drawing.Size(125, 21);
            this.CBTipoProducto.TabIndex = 4;
            // 
            // CBNombreProducto
            // 
            this.CBNombreProducto.FormattingEnabled = true;
            this.CBNombreProducto.Location = new System.Drawing.Point(887, 63);
            this.CBNombreProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBNombreProducto.Name = "CBNombreProducto";
            this.CBNombreProducto.Size = new System.Drawing.Size(125, 21);
            this.CBNombreProducto.TabIndex = 5;
            // 
            // CBNombreProveedor
            // 
            this.CBNombreProveedor.FormattingEnabled = true;
            this.CBNombreProveedor.Location = new System.Drawing.Point(887, 105);
            this.CBNombreProveedor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBNombreProveedor.Name = "CBNombreProveedor";
            this.CBNombreProveedor.Size = new System.Drawing.Size(125, 21);
            this.CBNombreProveedor.TabIndex = 6;
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(887, 217);
            this.BTNFiltrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(123, 21);
            this.BTNFiltrar.TabIndex = 7;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // BTNLimpiarFiltro
            // 
            this.BTNLimpiarFiltro.Location = new System.Drawing.Point(887, 242);
            this.BTNLimpiarFiltro.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNLimpiarFiltro.Name = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Size = new System.Drawing.Size(123, 21);
            this.BTNLimpiarFiltro.TabIndex = 8;
            this.BTNLimpiarFiltro.Tag = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Text = "Limpiar Filtros";
            this.BTNLimpiarFiltro.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltro.Click += new System.EventHandler(this.BTNLimpiarFiltro_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(887, 390);
            this.BTNSalir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(123, 23);
            this.BTNSalir.TabIndex = 9;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // BTNImprimirReporte
            // 
            this.BTNImprimirReporte.Location = new System.Drawing.Point(887, 365);
            this.BTNImprimirReporte.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTNImprimirReporte.Name = "BTNImprimirReporte";
            this.BTNImprimirReporte.Size = new System.Drawing.Size(123, 21);
            this.BTNImprimirReporte.TabIndex = 10;
            this.BTNImprimirReporte.Tag = "BTNImprimirReporte";
            this.BTNImprimirReporte.Text = "Imprimir Reporte";
            this.BTNImprimirReporte.UseVisualStyleBackColor = true;
            this.BTNImprimirReporte.Click += new System.EventHandler(this.BTNImprimirReporte_Click);
            // 
            // ReporteRFN2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orchid;
            this.ClientSize = new System.Drawing.Size(1017, 424);
            this.Controls.Add(this.BTNImprimirReporte);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNLimpiarFiltro);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.CBNombreProveedor);
            this.Controls.Add(this.CBNombreProducto);
            this.Controls.Add(this.CBTipoProducto);
            this.Controls.Add(this.LBLNombreProveedor);
            this.Controls.Add(this.LBLNombreProducto);
            this.Controls.Add(this.LBLTipoProducto);
            this.Controls.Add(this.DGVReporteRFN2);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ReporteRFN2";
            this.Text = "ReporteRFN2";
            this.Load += new System.EventHandler(this.ReporteRFN2_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteRFN2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVReporteRFN2;
        private System.Windows.Forms.Label LBLTipoProducto;
        private System.Windows.Forms.Label LBLNombreProducto;
        private System.Windows.Forms.Label LBLNombreProveedor;
        private System.Windows.Forms.ComboBox CBTipoProducto;
        private System.Windows.Forms.ComboBox CBNombreProducto;
        private System.Windows.Forms.ComboBox CBNombreProveedor;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button BTNLimpiarFiltro;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.Button BTNImprimirReporte;
    }
}