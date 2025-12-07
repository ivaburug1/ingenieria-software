namespace AseguraYa
{
    partial class _686DP_frmReporteSiniestro
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DTFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(768, 214);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // DTFechaVencimiento
            // 
            this.DTFechaVencimiento.Location = new System.Drawing.Point(12, 281);
            this.DTFechaVencimiento.Name = "DTFechaVencimiento";
            this.DTFechaVencimiento.Size = new System.Drawing.Size(200, 20);
            this.DTFechaVencimiento.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(236, 280);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(177, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(437, 278);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(75, 23);
            this.BTNFiltrar.TabIndex = 3;
            this.BTNFiltrar.Tag = "Filtrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(527, 278);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Tag = "LimpiarFiltros";
            this.button1.Text = "LimpiarFiltros";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Imprimir
            // 
            this.Imprimir.Location = new System.Drawing.Point(618, 278);
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.Size = new System.Drawing.Size(162, 23);
            this.Imprimir.TabIndex = 5;
            this.Imprimir.Tag = "Imprimir";
            this.Imprimir.Text = "Imprimir";
            this.Imprimir.UseVisualStyleBackColor = true;
            this.Imprimir.Click += new System.EventHandler(this.Imprimir_Click);
            // 
            // _686DP_frmReporteSiniestro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(800, 321);
            this.Controls.Add(this.Imprimir);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.DTFechaVencimiento);
            this.Controls.Add(this.dataGridView1);
            this.Name = "_686DP_frmReporteSiniestro";
            this.Text = "_686DP_frmReporteSiniestro";
            this.Load += new System.EventHandler(this._686DP_frmReporteSiniestro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker DTFechaVencimiento;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Imprimir;
    }
}