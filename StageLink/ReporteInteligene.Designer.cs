namespace StageLink
{
    partial class ReporteInteligene
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
            this.BTNImprimirReporte = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltro = new System.Windows.Forms.Button();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.CBEvento = new System.Windows.Forms.ComboBox();
            this.LBLNombreEvento = new System.Windows.Forms.Label();
            this.DGVReporteInteligente = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteInteligente)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNImprimirReporte
            // 
            this.BTNImprimirReporte.Location = new System.Drawing.Point(622, 300);
            this.BTNImprimirReporte.Name = "BTNImprimirReporte";
            this.BTNImprimirReporte.Size = new System.Drawing.Size(185, 33);
            this.BTNImprimirReporte.TabIndex = 21;
            this.BTNImprimirReporte.Tag = "BTNImprimirReporte";
            this.BTNImprimirReporte.Text = "Imprimir Reporte";
            this.BTNImprimirReporte.UseVisualStyleBackColor = true;
            this.BTNImprimirReporte.Click += new System.EventHandler(this.BTNImprimirReporte_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(622, 339);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(185, 36);
            this.BTNSalir.TabIndex = 20;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // BTNLimpiarFiltro
            // 
            this.BTNLimpiarFiltro.Location = new System.Drawing.Point(622, 201);
            this.BTNLimpiarFiltro.Name = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Size = new System.Drawing.Size(185, 33);
            this.BTNLimpiarFiltro.TabIndex = 19;
            this.BTNLimpiarFiltro.Tag = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Text = "Limpiar Filtros";
            this.BTNLimpiarFiltro.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltro.Click += new System.EventHandler(this.BTNLimpiarFiltro_Click);
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(622, 162);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(185, 33);
            this.BTNFiltrar.TabIndex = 18;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // CBEvento
            // 
            this.CBEvento.FormattingEnabled = true;
            this.CBEvento.Location = new System.Drawing.Point(622, 35);
            this.CBEvento.Name = "CBEvento";
            this.CBEvento.Size = new System.Drawing.Size(185, 28);
            this.CBEvento.TabIndex = 15;
            this.CBEvento.SelectedIndexChanged += new System.EventHandler(this.CBEvento_SelectedIndexChanged);
            // 
            // LBLNombreEvento
            // 
            this.LBLNombreEvento.AutoSize = true;
            this.LBLNombreEvento.Location = new System.Drawing.Point(618, 12);
            this.LBLNombreEvento.Name = "LBLNombreEvento";
            this.LBLNombreEvento.Size = new System.Drawing.Size(59, 20);
            this.LBLNombreEvento.TabIndex = 12;
            this.LBLNombreEvento.Tag = "LBLNombreEvento";
            this.LBLNombreEvento.Text = "Evento";
            // 
            // DGVReporteInteligente
            // 
            this.DGVReporteInteligente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReporteInteligente.Location = new System.Drawing.Point(12, 12);
            this.DGVReporteInteligente.Name = "DGVReporteInteligente";
            this.DGVReporteInteligente.RowTemplate.Height = 28;
            this.DGVReporteInteligente.Size = new System.Drawing.Size(600, 363);
            this.DGVReporteInteligente.TabIndex = 11;
            this.DGVReporteInteligente.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReporteInteligente_CellContentClick);
            // 
            // ReporteInteligene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(833, 433);
            this.Controls.Add(this.BTNImprimirReporte);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNLimpiarFiltro);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.CBEvento);
            this.Controls.Add(this.LBLNombreEvento);
            this.Controls.Add(this.DGVReporteInteligente);
            this.Name = "ReporteInteligene";
            this.Text = "ReporteInteligene";
            this.Load += new System.EventHandler(this.ReporteInteligene_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteInteligente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNImprimirReporte;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.Button BTNLimpiarFiltro;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.ComboBox CBEvento;
        private System.Windows.Forms.Label LBLNombreEvento;
        private System.Windows.Forms.DataGridView DGVReporteInteligente;
    }
}