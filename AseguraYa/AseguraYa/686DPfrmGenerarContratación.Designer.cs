namespace AseguraYa
{
    partial class _686DPfrmGenerarContratación
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DGPlan = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.BTNAceptar = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.CMBProducto = new System.Windows.Forms.ComboBox();
            this.DGCoberturas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCoberturas)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Tag = "DNI";
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 3;
            this.label2.Tag = "Producto";
            this.label2.Text = "Producto";
            // 
            // DGPlan
            // 
            this.DGPlan.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGPlan.Location = new System.Drawing.Point(186, 89);
            this.DGPlan.Name = "DGPlan";
            this.DGPlan.Size = new System.Drawing.Size(465, 102);
            this.DGPlan.TabIndex = 4;
            this.DGPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGPlan_CellClick);
            this.DGPlan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGPlan_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Planes";
            // 
            // BTNAceptar
            // 
            this.BTNAceptar.Location = new System.Drawing.Point(25, 89);
            this.BTNAceptar.Name = "BTNAceptar";
            this.BTNAceptar.Size = new System.Drawing.Size(123, 23);
            this.BTNAceptar.TabIndex = 7;
            this.BTNAceptar.Tag = "Aceptar";
            this.BTNAceptar.Text = "Aceptar";
            this.BTNAceptar.UseVisualStyleBackColor = true;
            this.BTNAceptar.Click += new System.EventHandler(this.BTNAceptar_Click);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(25, 118);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(123, 23);
            this.BTNCancelar.TabIndex = 8;
            this.BTNCancelar.Tag = "Cancelar";
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // CMBProducto
            // 
            this.CMBProducto.FormattingEnabled = true;
            this.CMBProducto.Location = new System.Drawing.Point(188, 40);
            this.CMBProducto.Name = "CMBProducto";
            this.CMBProducto.Size = new System.Drawing.Size(463, 21);
            this.CMBProducto.TabIndex = 9;
            this.CMBProducto.SelectedIndexChanged += new System.EventHandler(this.CMBProducto_SelectedIndexChanged);
            this.CMBProducto.SelectedValueChanged += new System.EventHandler(this.CMBProducto_SelectedValueChanged);
            // 
            // DGCoberturas
            // 
            this.DGCoberturas.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGCoberturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGCoberturas.Location = new System.Drawing.Point(186, 213);
            this.DGCoberturas.Name = "DGCoberturas";
            this.DGCoberturas.Size = new System.Drawing.Size(465, 214);
            this.DGCoberturas.TabIndex = 10;
            // 
            // _686DPfrmGenerarContratación
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(668, 450);
            this.Controls.Add(this.DGCoberturas);
            this.Controls.Add(this.CMBProducto);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAceptar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGPlan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "_686DPfrmGenerarContratación";
            this.Tag = "_686DPfrmGenerarContratación";
            this.Text = "_686DPfrmGenerarContratación";
            this.Load += new System.EventHandler(this._686DPfrmGenerarContratación_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCoberturas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView DGPlan;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BTNAceptar;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.ComboBox CMBProducto;
        private System.Windows.Forms.DataGridView DGCoberturas;
    }
}