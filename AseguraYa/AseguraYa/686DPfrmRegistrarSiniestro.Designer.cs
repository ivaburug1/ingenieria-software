namespace AseguraYa
{
    partial class _686DPfrmRegistrarSiniestro
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
            this.NPoliza = new System.Windows.Forms.Label();
            this.TXTNpoliza = new System.Windows.Forms.TextBox();
            this.BTNBuscar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.ValorBien = new System.Windows.Forms.Label();
            this.ValorRepara = new System.Windows.Forms.Label();
            this.Fecha = new System.Windows.Forms.Label();
            this.BTNRegistrar = new System.Windows.Forms.Button();
            this.cmbCobreturas = new System.Windows.Forms.ComboBox();
            this.Coberturas = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NPoliza
            // 
            this.NPoliza.AutoSize = true;
            this.NPoliza.Location = new System.Drawing.Point(34, 31);
            this.NPoliza.Name = "NPoliza";
            this.NPoliza.Size = new System.Drawing.Size(89, 13);
            this.NPoliza.TabIndex = 0;
            this.NPoliza.Tag = "Npoliza";
            this.NPoliza.Text = "Numero de poliza";
            // 
            // TXTNpoliza
            // 
            this.TXTNpoliza.Location = new System.Drawing.Point(37, 47);
            this.TXTNpoliza.Name = "TXTNpoliza";
            this.TXTNpoliza.Size = new System.Drawing.Size(149, 20);
            this.TXTNpoliza.TabIndex = 1;
            this.TXTNpoliza.TextChanged += new System.EventHandler(this.TXTNpoliza_TextChanged);
            // 
            // BTNBuscar
            // 
            this.BTNBuscar.Location = new System.Drawing.Point(192, 47);
            this.BTNBuscar.Name = "BTNBuscar";
            this.BTNBuscar.Size = new System.Drawing.Size(92, 20);
            this.BTNBuscar.TabIndex = 2;
            this.BTNBuscar.Tag = "BTNBuscar";
            this.BTNBuscar.Text = "Buscar";
            this.BTNBuscar.UseVisualStyleBackColor = true;
            this.BTNBuscar.Click += new System.EventHandler(this.BTNBuscar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(37, 155);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(37, 204);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(247, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(37, 100);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(247, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // ValorBien
            // 
            this.ValorBien.AutoSize = true;
            this.ValorBien.Location = new System.Drawing.Point(34, 139);
            this.ValorBien.Name = "ValorBien";
            this.ValorBien.Size = new System.Drawing.Size(71, 13);
            this.ValorBien.TabIndex = 6;
            this.ValorBien.Tag = "ValorBien";
            this.ValorBien.Text = "Valor del bien";
            // 
            // ValorRepara
            // 
            this.ValorRepara.AutoSize = true;
            this.ValorRepara.Location = new System.Drawing.Point(34, 188);
            this.ValorRepara.Name = "ValorRepara";
            this.ValorRepara.Size = new System.Drawing.Size(106, 13);
            this.ValorRepara.TabIndex = 7;
            this.ValorRepara.Tag = "ValorReparacion";
            this.ValorRepara.Text = "Valor De Reparacion";
            // 
            // Fecha
            // 
            this.Fecha.AutoSize = true;
            this.Fecha.Location = new System.Drawing.Point(34, 84);
            this.Fecha.Name = "Fecha";
            this.Fecha.Size = new System.Drawing.Size(37, 13);
            this.Fecha.TabIndex = 8;
            this.Fecha.Tag = "Fecha";
            this.Fecha.Text = "Fecha";
            // 
            // BTNRegistrar
            // 
            this.BTNRegistrar.Location = new System.Drawing.Point(37, 303);
            this.BTNRegistrar.Name = "BTNRegistrar";
            this.BTNRegistrar.Size = new System.Drawing.Size(92, 20);
            this.BTNRegistrar.TabIndex = 9;
            this.BTNRegistrar.Tag = "BTNRegistrar";
            this.BTNRegistrar.Text = "Registrar Siniestro";
            this.BTNRegistrar.UseVisualStyleBackColor = true;
            this.BTNRegistrar.Click += new System.EventHandler(this.BTNRegistrar_Click);
            // 
            // cmbCobreturas
            // 
            this.cmbCobreturas.FormattingEnabled = true;
            this.cmbCobreturas.Location = new System.Drawing.Point(37, 256);
            this.cmbCobreturas.Name = "cmbCobreturas";
            this.cmbCobreturas.Size = new System.Drawing.Size(247, 21);
            this.cmbCobreturas.TabIndex = 10;
            // 
            // Coberturas
            // 
            this.Coberturas.AutoSize = true;
            this.Coberturas.Location = new System.Drawing.Point(34, 240);
            this.Coberturas.Name = "Coberturas";
            this.Coberturas.Size = new System.Drawing.Size(58, 13);
            this.Coberturas.TabIndex = 11;
            this.Coberturas.Tag = "Coberturas";
            this.Coberturas.Text = "Coberturas";
            // 
            // _686DPfrmRegistrarSiniestro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(328, 348);
            this.Controls.Add(this.Coberturas);
            this.Controls.Add(this.cmbCobreturas);
            this.Controls.Add(this.BTNRegistrar);
            this.Controls.Add(this.Fecha);
            this.Controls.Add(this.ValorRepara);
            this.Controls.Add(this.ValorBien);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.BTNBuscar);
            this.Controls.Add(this.TXTNpoliza);
            this.Controls.Add(this.NPoliza);
            this.Name = "_686DPfrmRegistrarSiniestro";
            this.Text = "_686DPfrmRegistrarSiniestro";
            this.Load += new System.EventHandler(this._686DPfrmRegistrarSiniestro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NPoliza;
        private System.Windows.Forms.TextBox TXTNpoliza;
        private System.Windows.Forms.Button BTNBuscar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label ValorBien;
        private System.Windows.Forms.Label ValorRepara;
        private System.Windows.Forms.Label Fecha;
        private System.Windows.Forms.Button BTNRegistrar;
        private System.Windows.Forms.ComboBox cmbCobreturas;
        private System.Windows.Forms.Label Coberturas;
    }
}