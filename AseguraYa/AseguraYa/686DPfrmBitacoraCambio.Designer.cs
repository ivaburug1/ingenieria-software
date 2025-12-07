namespace AseguraYa
{
    partial class _686DPfrmBitacoraCambio
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.DNI = new System.Windows.Forms.Label();
            this.LBLFechaDesde = new System.Windows.Forms.Label();
            this.LBLFechaHasta = new System.Windows.Forms.Label();
            this.Filtrar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Desbloquear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1095, 232);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(1116, 139);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(1116, 192);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // DNI
            // 
            this.DNI.AutoSize = true;
            this.DNI.Location = new System.Drawing.Point(1118, 67);
            this.DNI.Name = "DNI";
            this.DNI.Size = new System.Drawing.Size(26, 13);
            this.DNI.TabIndex = 5;
            this.DNI.Tag = "DNI";
            this.DNI.Text = "DNI";
            // 
            // LBLFechaDesde
            // 
            this.LBLFechaDesde.AutoSize = true;
            this.LBLFechaDesde.Location = new System.Drawing.Point(1118, 123);
            this.LBLFechaDesde.Name = "LBLFechaDesde";
            this.LBLFechaDesde.Size = new System.Drawing.Size(69, 13);
            this.LBLFechaDesde.TabIndex = 6;
            this.LBLFechaDesde.Tag = "LBLFechaDesde";
            this.LBLFechaDesde.Text = "Fecha desde";
            // 
            // LBLFechaHasta
            // 
            this.LBLFechaHasta.AutoSize = true;
            this.LBLFechaHasta.Location = new System.Drawing.Point(1113, 176);
            this.LBLFechaHasta.Name = "LBLFechaHasta";
            this.LBLFechaHasta.Size = new System.Drawing.Size(66, 13);
            this.LBLFechaHasta.TabIndex = 7;
            this.LBLFechaHasta.Tag = "LBLFechaHasta";
            this.LBLFechaHasta.Text = "Fecha hasta";
            // 
            // Filtrar
            // 
            this.Filtrar.Location = new System.Drawing.Point(1113, 232);
            this.Filtrar.Name = "Filtrar";
            this.Filtrar.Size = new System.Drawing.Size(90, 25);
            this.Filtrar.TabIndex = 9;
            this.Filtrar.Tag = "Filtrar";
            this.Filtrar.Text = "Filtrar";
            this.Filtrar.UseVisualStyleBackColor = true;
            this.Filtrar.Click += new System.EventHandler(this.Aplicar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1113, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 25);
            this.button2.TabIndex = 10;
            this.button2.Tag = "Limpiar";
            this.button2.Text = "Limpiar filtro";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Desbloquear
            // 
            this.Desbloquear.Location = new System.Drawing.Point(1226, 232);
            this.Desbloquear.Name = "Desbloquear";
            this.Desbloquear.Size = new System.Drawing.Size(90, 55);
            this.Desbloquear.TabIndex = 12;
            this.Desbloquear.Tag = "Desbloquear";
            this.Desbloquear.Text = "Volverlo vigente";
            this.Desbloquear.UseVisualStyleBackColor = true;
            this.Desbloquear.Click += new System.EventHandler(this.Desbloquear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 25);
            this.label1.TabIndex = 13;
            this.label1.Tag = "LBLBitacoraDeCambio";
            this.label1.Text = "Bitacora de cambios";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1116, 83);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 20);
            this.textBox2.TabIndex = 14;
            // 
            // _686DPfrmBitacoraCambio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1328, 304);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Desbloquear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Filtrar);
            this.Controls.Add(this.LBLFechaHasta);
            this.Controls.Add(this.LBLFechaDesde);
            this.Controls.Add(this.DNI);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "_686DPfrmBitacoraCambio";
            this.Text = "_686DPfrmBitacoraCambio";
            this.Load += new System.EventHandler(this._686DPfrmBitacoraCambio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label DNI;
        private System.Windows.Forms.Label LBLFechaDesde;
        private System.Windows.Forms.Label LBLFechaHasta;
        private System.Windows.Forms.Button Filtrar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button Desbloquear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
    }
}