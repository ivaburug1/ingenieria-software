namespace AseguraYa
{
    partial class _686DP_frmCrearProducto
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
            this.cmbProductos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BTNCrearProducto = new System.Windows.Forms.Button();
            this.BTNCrearPlan = new System.Windows.Forms.Button();
            this.BTNCrearCobertura = new System.Windows.Forms.Button();
            this.DGCobertura = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TXTFranquicia = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.BTNModificarPlan = new System.Windows.Forms.Button();
            this.TXTDescripcionCobertura = new System.Windows.Forms.TextBox();
            this.TXTSumaAsegurada = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.BTNAsociarPlan = new System.Windows.Forms.Button();
            this.TXTProductos = new System.Windows.Forms.TextBox();
            this.RBCrearProducto = new System.Windows.Forms.RadioButton();
            this.RBAgruparSeguro = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.DGPlan = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TXTPrima = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGCobertura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProductos
            // 
            this.cmbProductos.FormattingEnabled = true;
            this.cmbProductos.Location = new System.Drawing.Point(313, 50);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new System.Drawing.Size(200, 21);
            this.cmbProductos.TabIndex = 0;
            this.cmbProductos.SelectedIndexChanged += new System.EventHandler(this.cmbProductos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(310, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "Productos";
            this.label1.Text = "Productos";
            // 
            // BTNCrearProducto
            // 
            this.BTNCrearProducto.Location = new System.Drawing.Point(35, 109);
            this.BTNCrearProducto.Name = "BTNCrearProducto";
            this.BTNCrearProducto.Size = new System.Drawing.Size(108, 32);
            this.BTNCrearProducto.TabIndex = 9;
            this.BTNCrearProducto.Tag = "CrearProducto";
            this.BTNCrearProducto.Text = "Crear producto";
            this.BTNCrearProducto.UseVisualStyleBackColor = true;
            this.BTNCrearProducto.Click += new System.EventHandler(this.BTNCrearProducto_Click);
            // 
            // BTNCrearPlan
            // 
            this.BTNCrearPlan.Location = new System.Drawing.Point(35, 276);
            this.BTNCrearPlan.Name = "BTNCrearPlan";
            this.BTNCrearPlan.Size = new System.Drawing.Size(108, 32);
            this.BTNCrearPlan.TabIndex = 10;
            this.BTNCrearPlan.Tag = "CrearPlan";
            this.BTNCrearPlan.Text = "Crear Plan";
            this.BTNCrearPlan.UseVisualStyleBackColor = true;
            this.BTNCrearPlan.Click += new System.EventHandler(this.BTNCrearPlan_Click);
            // 
            // BTNCrearCobertura
            // 
            this.BTNCrearCobertura.Location = new System.Drawing.Point(34, 504);
            this.BTNCrearCobertura.Name = "BTNCrearCobertura";
            this.BTNCrearCobertura.Size = new System.Drawing.Size(104, 32);
            this.BTNCrearCobertura.TabIndex = 11;
            this.BTNCrearCobertura.Tag = "CrearCobertura";
            this.BTNCrearCobertura.Text = "Crear Cobertura";
            this.BTNCrearCobertura.UseVisualStyleBackColor = true;
            this.BTNCrearCobertura.Click += new System.EventHandler(this.BTNCrearCobertura_Click);
            // 
            // DGCobertura
            // 
            this.DGCobertura.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGCobertura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGCobertura.Location = new System.Drawing.Point(638, 109);
            this.DGCobertura.Name = "DGCobertura";
            this.DGCobertura.Size = new System.Drawing.Size(398, 427);
            this.DGCobertura.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(635, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 16;
            this.label3.Tag = "Coberturas";
            this.label3.Text = "Coberturas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 172);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 17;
            this.label4.Tag = "GestionPlan";
            this.label4.Text = "Gestion de planes";
            // 
            // TXTFranquicia
            // 
            this.TXTFranquicia.Location = new System.Drawing.Point(35, 210);
            this.TXTFranquicia.Name = "TXTFranquicia";
            this.TXTFranquicia.Size = new System.Drawing.Size(219, 20);
            this.TXTFranquicia.TabIndex = 18;
            this.TXTFranquicia.TextChanged += new System.EventHandler(this.TXTFranquicia_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 19;
            this.label5.Tag = "Franquicia";
            this.label5.Text = "Franquicia";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 23;
            this.label7.Tag = "Productos";
            this.label7.Text = "Productos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 24;
            this.label8.Tag = "GesestionSeguros";
            this.label8.Text = "Gestion de seguros";
            // 
            // BTNModificarPlan
            // 
            this.BTNModificarPlan.Location = new System.Drawing.Point(149, 277);
            this.BTNModificarPlan.Name = "BTNModificarPlan";
            this.BTNModificarPlan.Size = new System.Drawing.Size(108, 31);
            this.BTNModificarPlan.TabIndex = 26;
            this.BTNModificarPlan.Tag = "AsociarCobertura";
            this.BTNModificarPlan.Text = "Asociar Cobertura";
            this.BTNModificarPlan.UseVisualStyleBackColor = true;
            this.BTNModificarPlan.Click += new System.EventHandler(this.BTNModificarPlan_Click);
            // 
            // TXTDescripcionCobertura
            // 
            this.TXTDescripcionCobertura.Location = new System.Drawing.Point(36, 428);
            this.TXTDescripcionCobertura.MaxLength = 100;
            this.TXTDescripcionCobertura.Name = "TXTDescripcionCobertura";
            this.TXTDescripcionCobertura.Size = new System.Drawing.Size(219, 20);
            this.TXTDescripcionCobertura.TabIndex = 27;
            this.TXTDescripcionCobertura.TextChanged += new System.EventHandler(this.TXTDescripcionCobertura_TextChanged);
            // 
            // TXTSumaAsegurada
            // 
            this.TXTSumaAsegurada.Location = new System.Drawing.Point(36, 478);
            this.TXTSumaAsegurada.Name = "TXTSumaAsegurada";
            this.TXTSumaAsegurada.Size = new System.Drawing.Size(219, 20);
            this.TXTSumaAsegurada.TabIndex = 28;
            this.TXTSumaAsegurada.TextChanged += new System.EventHandler(this.TXTSumaAsegurada_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 462);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 29;
            this.label9.Tag = "SumaAsegurada";
            this.label9.Text = "Suma Asegurada";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(36, 407);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 30;
            this.label10.Tag = "Descripcion";
            this.label10.Text = "Descripcion";
            // 
            // BTNAsociarPlan
            // 
            this.BTNAsociarPlan.Location = new System.Drawing.Point(149, 109);
            this.BTNAsociarPlan.Name = "BTNAsociarPlan";
            this.BTNAsociarPlan.Size = new System.Drawing.Size(108, 32);
            this.BTNAsociarPlan.TabIndex = 31;
            this.BTNAsociarPlan.Tag = "AsociarPlan";
            this.BTNAsociarPlan.Text = "Asociar Plan";
            this.BTNAsociarPlan.UseVisualStyleBackColor = true;
            this.BTNAsociarPlan.Click += new System.EventHandler(this.BTNAsociarPlan_Click);
            // 
            // TXTProductos
            // 
            this.TXTProductos.Location = new System.Drawing.Point(35, 74);
            this.TXTProductos.Name = "TXTProductos";
            this.TXTProductos.Size = new System.Drawing.Size(219, 20);
            this.TXTProductos.TabIndex = 33;
            this.TXTProductos.TextChanged += new System.EventHandler(this.TXTProductos_TextChanged);
            // 
            // RBCrearProducto
            // 
            this.RBCrearProducto.AutoSize = true;
            this.RBCrearProducto.Location = new System.Drawing.Point(570, 48);
            this.RBCrearProducto.Name = "RBCrearProducto";
            this.RBCrearProducto.Size = new System.Drawing.Size(94, 17);
            this.RBCrearProducto.TabIndex = 35;
            this.RBCrearProducto.TabStop = true;
            this.RBCrearProducto.Tag = "crearproducto";
            this.RBCrearProducto.Text = "crear producto";
            this.RBCrearProducto.UseVisualStyleBackColor = true;
            this.RBCrearProducto.CheckedChanged += new System.EventHandler(this.RBCrearProducto_CheckedChanged);
            // 
            // RBAgruparSeguro
            // 
            this.RBAgruparSeguro.AutoSize = true;
            this.RBAgruparSeguro.Location = new System.Drawing.Point(717, 48);
            this.RBAgruparSeguro.Name = "RBAgruparSeguro";
            this.RBAgruparSeguro.Size = new System.Drawing.Size(99, 17);
            this.RBAgruparSeguro.TabIndex = 36;
            this.RBAgruparSeguro.TabStop = true;
            this.RBAgruparSeguro.Tag = "AgruparSeguro";
            this.RBAgruparSeguro.Text = "Agrupar Seguro";
            this.RBAgruparSeguro.UseVisualStyleBackColor = true;
            this.RBAgruparSeguro.CheckedChanged += new System.EventHandler(this.RBAgruparSeguro_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 384);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 13);
            this.label11.TabIndex = 37;
            this.label11.Tag = "GestionCobertura";
            this.label11.Text = "Gestion de coberturas";
            // 
            // DGPlan
            // 
            this.DGPlan.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGPlan.Location = new System.Drawing.Point(313, 109);
            this.DGPlan.Name = "DGPlan";
            this.DGPlan.Size = new System.Drawing.Size(309, 427);
            this.DGPlan.TabIndex = 13;
            this.DGPlan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGPlan_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 15;
            this.label2.Tag = "Planes";
            this.label2.Text = "Planes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 41;
            this.label6.Tag = "Prima";
            this.label6.Text = "Prima";
            // 
            // TXTPrima
            // 
            this.TXTPrima.Location = new System.Drawing.Point(36, 250);
            this.TXTPrima.Name = "TXTPrima";
            this.TXTPrima.Size = new System.Drawing.Size(219, 20);
            this.TXTPrima.TabIndex = 40;
            this.TXTPrima.TextChanged += new System.EventHandler(this.TXTPrima_TextChanged);
            // 
            // _686DP_frmCrearProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1071, 604);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TXTPrima);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.RBAgruparSeguro);
            this.Controls.Add(this.RBCrearProducto);
            this.Controls.Add(this.TXTProductos);
            this.Controls.Add(this.BTNAsociarPlan);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.TXTSumaAsegurada);
            this.Controls.Add(this.TXTDescripcionCobertura);
            this.Controls.Add(this.BTNModificarPlan);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TXTFranquicia);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DGCobertura);
            this.Controls.Add(this.DGPlan);
            this.Controls.Add(this.BTNCrearCobertura);
            this.Controls.Add(this.BTNCrearPlan);
            this.Controls.Add(this.BTNCrearProducto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbProductos);
            this.Name = "_686DP_frmCrearProducto";
            this.Tag = "_686DP_frmCrearProducto";
            this.Text = "_686DP_frmCrearProducto";
            this.Load += new System.EventHandler(this._686DP_frmCrearProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGCobertura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProductos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTNCrearProducto;
        private System.Windows.Forms.Button BTNCrearPlan;
        private System.Windows.Forms.Button BTNCrearCobertura;
        private System.Windows.Forms.DataGridView DGCobertura;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TXTFranquicia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button BTNModificarPlan;
        private System.Windows.Forms.TextBox TXTDescripcionCobertura;
        private System.Windows.Forms.TextBox TXTSumaAsegurada;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button BTNAsociarPlan;
        private System.Windows.Forms.TextBox TXTProductos;
        private System.Windows.Forms.RadioButton RBCrearProducto;
        private System.Windows.Forms.RadioButton RBAgruparSeguro;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView DGPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TXTPrima;
    }
}