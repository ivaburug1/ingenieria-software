namespace StageLink
{
    partial class Reportes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes));
            this.DGVReporteVentas = new System.Windows.Forms.DataGridView();
            this.LBLReportes = new System.Windows.Forms.Label();
            this.LBLFiltros = new System.Windows.Forms.Label();
            this.CMBFechaEvento = new System.Windows.Forms.ComboBox();
            this.LBLFechaDelEvento = new System.Windows.Forms.Label();
            this.LBLNombreComprador = new System.Windows.Forms.Label();
            this.CMBNombreComprador = new System.Windows.Forms.ComboBox();
            this.LBLListaArtistas = new System.Windows.Forms.Label();
            this.CMBListaArtistas = new System.Windows.Forms.ComboBox();
            this.BTNAplicarFiltro = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltros = new System.Windows.Forms.Button();
            this.LBLResultados = new System.Windows.Forms.Label();
            this.BTNPDFReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteVentas)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVReporteVentas
            // 
            this.DGVReporteVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReporteVentas.Location = new System.Drawing.Point(7, 51);
            this.DGVReporteVentas.Name = "DGVReporteVentas";
            this.DGVReporteVentas.Size = new System.Drawing.Size(1005, 440);
            this.DGVReporteVentas.TabIndex = 0;
            this.DGVReporteVentas.Tag = "DGVReporteVentas";
            this.DGVReporteVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReporteVentas_CellContentClick);
            // 
            // LBLReportes
            // 
            this.LBLReportes.AutoSize = true;
            this.LBLReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.LBLReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.LBLReportes.Location = new System.Drawing.Point(0, 0);
            this.LBLReportes.Name = "LBLReportes";
            this.LBLReportes.Size = new System.Drawing.Size(155, 39);
            this.LBLReportes.TabIndex = 1;
            this.LBLReportes.Tag = "LBLReportes";
            this.LBLReportes.Text = "Reportes";
            // 
            // LBLFiltros
            // 
            this.LBLFiltros.AutoSize = true;
            this.LBLFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(96)))), ((int)(((byte)(238)))));
            this.LBLFiltros.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.LBLFiltros.Location = new System.Drawing.Point(1018, 51);
            this.LBLFiltros.Name = "LBLFiltros";
            this.LBLFiltros.Size = new System.Drawing.Size(64, 25);
            this.LBLFiltros.TabIndex = 2;
            this.LBLFiltros.Tag = "LBLFiltros";
            this.LBLFiltros.Text = "Filtros";
            // 
            // CMBFechaEvento
            // 
            this.CMBFechaEvento.FormattingEnabled = true;
            this.CMBFechaEvento.Location = new System.Drawing.Point(1023, 95);
            this.CMBFechaEvento.Name = "CMBFechaEvento";
            this.CMBFechaEvento.Size = new System.Drawing.Size(204, 21);
            this.CMBFechaEvento.TabIndex = 3;
            this.CMBFechaEvento.Tag = "CMBFechaEvento";
            // 
            // LBLFechaDelEvento
            // 
            this.LBLFechaDelEvento.AutoSize = true;
            this.LBLFechaDelEvento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(96)))), ((int)(((byte)(238)))));
            this.LBLFechaDelEvento.Location = new System.Drawing.Point(1020, 79);
            this.LBLFechaDelEvento.Name = "LBLFechaDelEvento";
            this.LBLFechaDelEvento.Size = new System.Drawing.Size(91, 13);
            this.LBLFechaDelEvento.TabIndex = 4;
            this.LBLFechaDelEvento.Tag = "LBLFechaDelEvento";
            this.LBLFechaDelEvento.Text = "Fecha del Evento";
            // 
            // LBLNombreComprador
            // 
            this.LBLNombreComprador.AutoSize = true;
            this.LBLNombreComprador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(96)))), ((int)(((byte)(238)))));
            this.LBLNombreComprador.Location = new System.Drawing.Point(1020, 124);
            this.LBLNombreComprador.Name = "LBLNombreComprador";
            this.LBLNombreComprador.Size = new System.Drawing.Size(98, 13);
            this.LBLNombreComprador.TabIndex = 6;
            this.LBLNombreComprador.Tag = "LBLNombreComprador";
            this.LBLNombreComprador.Text = "Nombre Comprador";
            // 
            // CMBNombreComprador
            // 
            this.CMBNombreComprador.FormattingEnabled = true;
            this.CMBNombreComprador.Location = new System.Drawing.Point(1023, 140);
            this.CMBNombreComprador.Name = "CMBNombreComprador";
            this.CMBNombreComprador.Size = new System.Drawing.Size(204, 21);
            this.CMBNombreComprador.TabIndex = 5;
            this.CMBNombreComprador.Tag = "CMBNombreComprador";
            // 
            // LBLListaArtistas
            // 
            this.LBLListaArtistas.AutoSize = true;
            this.LBLListaArtistas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(96)))), ((int)(((byte)(238)))));
            this.LBLListaArtistas.Location = new System.Drawing.Point(1020, 172);
            this.LBLListaArtistas.Name = "LBLListaArtistas";
            this.LBLListaArtistas.Size = new System.Drawing.Size(81, 13);
            this.LBLListaArtistas.TabIndex = 8;
            this.LBLListaArtistas.Tag = "LBLListaArtistas";
            this.LBLListaArtistas.Text = "Lista de Artistas";
            // 
            // CMBListaArtistas
            // 
            this.CMBListaArtistas.FormattingEnabled = true;
            this.CMBListaArtistas.Location = new System.Drawing.Point(1023, 188);
            this.CMBListaArtistas.Name = "CMBListaArtistas";
            this.CMBListaArtistas.Size = new System.Drawing.Size(204, 21);
            this.CMBListaArtistas.TabIndex = 7;
            this.CMBListaArtistas.Tag = "CMBListaArtistas";
            // 
            // BTNAplicarFiltro
            // 
            this.BTNAplicarFiltro.Location = new System.Drawing.Point(1023, 230);
            this.BTNAplicarFiltro.Name = "BTNAplicarFiltro";
            this.BTNAplicarFiltro.Size = new System.Drawing.Size(204, 23);
            this.BTNAplicarFiltro.TabIndex = 9;
            this.BTNAplicarFiltro.Tag = "BTNAplicarFiltro";
            this.BTNAplicarFiltro.Text = "Aplicar Filtros al Reporte";
            this.BTNAplicarFiltro.UseVisualStyleBackColor = true;
            this.BTNAplicarFiltro.Click += new System.EventHandler(this.BTNAplicarFiltro_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(1175, 468);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(75, 23);
            this.BTNSalir.TabIndex = 10;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // BTNLimpiarFiltros
            // 
            this.BTNLimpiarFiltros.Location = new System.Drawing.Point(1023, 259);
            this.BTNLimpiarFiltros.Name = "BTNLimpiarFiltros";
            this.BTNLimpiarFiltros.Size = new System.Drawing.Size(103, 23);
            this.BTNLimpiarFiltros.TabIndex = 11;
            this.BTNLimpiarFiltros.Tag = "BTNLimpiarFiltros";
            this.BTNLimpiarFiltros.Text = "Limpiar Filtros ";
            this.BTNLimpiarFiltros.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltros.Click += new System.EventHandler(this.BTNLimpiarFiltros_Click);
            // 
            // LBLResultados
            // 
            this.LBLResultados.AutoSize = true;
            this.LBLResultados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.LBLResultados.Location = new System.Drawing.Point(207, 31);
            this.LBLResultados.Name = "LBLResultados";
            this.LBLResultados.Size = new System.Drawing.Size(0, 13);
            this.LBLResultados.TabIndex = 12;
            this.LBLResultados.Tag = "LBLResultados";
            // 
            // BTNPDFReporte
            // 
            this.BTNPDFReporte.Location = new System.Drawing.Point(1132, 259);
            this.BTNPDFReporte.Name = "BTNPDFReporte";
            this.BTNPDFReporte.Size = new System.Drawing.Size(95, 23);
            this.BTNPDFReporte.TabIndex = 13;
            this.BTNPDFReporte.Tag = "BTNPDFReporte";
            this.BTNPDFReporte.Text = "Imprimir Reporte";
            this.BTNPDFReporte.UseVisualStyleBackColor = true;
            this.BTNPDFReporte.Click += new System.EventHandler(this.BTNPDFReporte_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1262, 503);
            this.Controls.Add(this.BTNPDFReporte);
            this.Controls.Add(this.LBLResultados);
            this.Controls.Add(this.BTNLimpiarFiltros);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNAplicarFiltro);
            this.Controls.Add(this.LBLListaArtistas);
            this.Controls.Add(this.CMBListaArtistas);
            this.Controls.Add(this.LBLNombreComprador);
            this.Controls.Add(this.CMBNombreComprador);
            this.Controls.Add(this.LBLFechaDelEvento);
            this.Controls.Add(this.CMBFechaEvento);
            this.Controls.Add(this.LBLFiltros);
            this.Controls.Add(this.LBLReportes);
            this.Controls.Add(this.DGVReporteVentas);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Reportes_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteVentas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVReporteVentas;
        private System.Windows.Forms.Label LBLReportes;
        private System.Windows.Forms.Label LBLFiltros;
        private System.Windows.Forms.ComboBox CMBFechaEvento;
        private System.Windows.Forms.Label LBLFechaDelEvento;
        private System.Windows.Forms.Label LBLNombreComprador;
        private System.Windows.Forms.ComboBox CMBNombreComprador;
        private System.Windows.Forms.Label LBLListaArtistas;
        private System.Windows.Forms.ComboBox CMBListaArtistas;
        private System.Windows.Forms.Button BTNAplicarFiltro;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.Button BTNLimpiarFiltros;
        private System.Windows.Forms.Label LBLResultados;
        private System.Windows.Forms.Button BTNPDFReporte;
    }
}