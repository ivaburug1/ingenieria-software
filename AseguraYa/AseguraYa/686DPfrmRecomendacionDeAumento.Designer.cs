namespace AseguraYa
{
    partial class _686DPfrmRecomendacionDeAumento
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.AumentarCuota = new System.Windows.Forms.Button();
            this.TXTDni = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TXTCuotaDesde = new System.Windows.Forms.TextBox();
            this.TXTCuotaHasta = new System.Windows.Forms.TextBox();
            this.CantSiniestro = new System.Windows.Forms.NumericUpDown();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.BTNLimpiarFiltro = new System.Windows.Forms.Button();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.CuotaDesde = new System.Windows.Forms.Label();
            this.CuotaHasta = new System.Windows.Forms.Label();
            this.CantDeSiniestro = new System.Windows.Forms.Label();
            this.Imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CantSiniestro)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 214);
            this.dataGridView1.TabIndex = 0;
            // 
            // AumentarCuota
            // 
            this.AumentarCuota.Location = new System.Drawing.Point(623, 335);
            this.AumentarCuota.Name = "AumentarCuota";
            this.AumentarCuota.Size = new System.Drawing.Size(165, 32);
            this.AumentarCuota.TabIndex = 1;
            this.AumentarCuota.Tag = "AumentarCuota";
            this.AumentarCuota.Text = "Aumentar cuota";
            this.AumentarCuota.UseVisualStyleBackColor = true;
            this.AumentarCuota.Click += new System.EventHandler(this.AumentarCuota_Click);
            // 
            // TXTDni
            // 
            this.TXTDni.Location = new System.Drawing.Point(14, 63);
            this.TXTDni.Name = "TXTDni";
            this.TXTDni.Size = new System.Drawing.Size(138, 20);
            this.TXTDni.TabIndex = 2;
            this.TXTDni.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // TXTCuotaDesde
            // 
            this.TXTCuotaDesde.Location = new System.Drawing.Point(180, 63);
            this.TXTCuotaDesde.Name = "TXTCuotaDesde";
            this.TXTCuotaDesde.Size = new System.Drawing.Size(138, 20);
            this.TXTCuotaDesde.TabIndex = 4;
            this.TXTCuotaDesde.TextChanged += new System.EventHandler(this.TXTCuotaDesde_TextChanged);
            // 
            // TXTCuotaHasta
            // 
            this.TXTCuotaHasta.Location = new System.Drawing.Point(340, 63);
            this.TXTCuotaHasta.Name = "TXTCuotaHasta";
            this.TXTCuotaHasta.Size = new System.Drawing.Size(138, 20);
            this.TXTCuotaHasta.TabIndex = 5;
            this.TXTCuotaHasta.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // CantSiniestro
            // 
            this.CantSiniestro.Location = new System.Drawing.Point(494, 63);
            this.CantSiniestro.Name = "CantSiniestro";
            this.CantSiniestro.Size = new System.Drawing.Size(120, 20);
            this.CantSiniestro.TabIndex = 6;
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(647, 22);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(141, 23);
            this.BTNFiltrar.TabIndex = 7;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // BTNLimpiarFiltro
            // 
            this.BTNLimpiarFiltro.Location = new System.Drawing.Point(647, 60);
            this.BTNLimpiarFiltro.Name = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Size = new System.Drawing.Size(141, 23);
            this.BTNLimpiarFiltro.TabIndex = 8;
            this.BTNLimpiarFiltro.Tag = "BTNLimpiarFiltro";
            this.BTNLimpiarFiltro.Text = "Limpiar Filtro";
            this.BTNLimpiarFiltro.UseVisualStyleBackColor = true;
            this.BTNLimpiarFiltro.Click += new System.EventHandler(this.BTNLimpiarFiltro_Click);
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.Location = new System.Drawing.Point(12, 32);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(26, 13);
            this.LBLDNI.TabIndex = 9;
            this.LBLDNI.Tag = "DNI";
            this.LBLDNI.Text = "DNI";
            this.LBLDNI.Click += new System.EventHandler(this.LBLDNI_Click);
            // 
            // CuotaDesde
            // 
            this.CuotaDesde.AutoSize = true;
            this.CuotaDesde.Location = new System.Drawing.Point(177, 32);
            this.CuotaDesde.Name = "CuotaDesde";
            this.CuotaDesde.Size = new System.Drawing.Size(109, 13);
            this.CuotaDesde.TabIndex = 10;
            this.CuotaDesde.Tag = "CuotaDesde";
            this.CuotaDesde.Text = "Cuota mensual desde";
            this.CuotaDesde.Click += new System.EventHandler(this.label1_Click);
            // 
            // CuotaHasta
            // 
            this.CuotaHasta.AutoSize = true;
            this.CuotaHasta.Location = new System.Drawing.Point(337, 32);
            this.CuotaHasta.Name = "CuotaHasta";
            this.CuotaHasta.Size = new System.Drawing.Size(106, 13);
            this.CuotaHasta.TabIndex = 11;
            this.CuotaHasta.Tag = "CuotaHasta";
            this.CuotaHasta.Text = "Cuota mensual hasta";
            // 
            // CantDeSiniestro
            // 
            this.CantDeSiniestro.AutoSize = true;
            this.CantDeSiniestro.Location = new System.Drawing.Point(491, 32);
            this.CantDeSiniestro.Name = "CantDeSiniestro";
            this.CantDeSiniestro.Size = new System.Drawing.Size(110, 13);
            this.CantDeSiniestro.TabIndex = 12;
            this.CantDeSiniestro.Tag = "CantDeSiniestro";
            this.CantDeSiniestro.Text = "Cantidad de siniestros";
            // 
            // Imprimir
            // 
            this.Imprimir.Location = new System.Drawing.Point(436, 335);
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.Size = new System.Drawing.Size(165, 32);
            this.Imprimir.TabIndex = 13;
            this.Imprimir.Tag = "Imprimir";
            this.Imprimir.Text = "Imprimir";
            this.Imprimir.UseVisualStyleBackColor = true;
            this.Imprimir.Click += new System.EventHandler(this.Imprimir_Click);
            // 
            // _686DPfrmRecomendacionDeAumento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(800, 371);
            this.Controls.Add(this.Imprimir);
            this.Controls.Add(this.CantDeSiniestro);
            this.Controls.Add(this.CuotaHasta);
            this.Controls.Add(this.CuotaDesde);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.BTNLimpiarFiltro);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.CantSiniestro);
            this.Controls.Add(this.TXTCuotaHasta);
            this.Controls.Add(this.TXTCuotaDesde);
            this.Controls.Add(this.TXTDni);
            this.Controls.Add(this.AumentarCuota);
            this.Controls.Add(this.dataGridView1);
            this.Name = "_686DPfrmRecomendacionDeAumento";
            this.Text = "_686DPfrmRecomendacionDeAumento";
            this.Load += new System.EventHandler(this._686DPfrmRecomendacionDeAumento_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CantSiniestro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button AumentarCuota;
        private System.Windows.Forms.TextBox TXTDni;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox TXTCuotaDesde;
        private System.Windows.Forms.TextBox TXTCuotaHasta;
        private System.Windows.Forms.NumericUpDown CantSiniestro;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button BTNLimpiarFiltro;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label CuotaDesde;
        private System.Windows.Forms.Label CuotaHasta;
        private System.Windows.Forms.Label CantDeSiniestro;
        private System.Windows.Forms.Button Imprimir;
    }
}