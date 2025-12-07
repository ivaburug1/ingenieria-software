namespace AseguraYa
{
    partial class _686DPfrmModificarSeguro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_686DPfrmModificarSeguro));
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNAceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.DGPlan = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.TXTProducto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.DGCoberura = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCoberura)).BeginInit();
            this.SuspendLayout();
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(18, 114);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(123, 23);
            this.BTNCancelar.TabIndex = 17;
            this.BTNCancelar.Tag = "Cancelar";
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            // 
            // BTNAceptar
            // 
            this.BTNAceptar.Location = new System.Drawing.Point(18, 85);
            this.BTNAceptar.Name = "BTNAceptar";
            this.BTNAceptar.Size = new System.Drawing.Size(123, 23);
            this.BTNAceptar.TabIndex = 16;
            this.BTNAceptar.Tag = "Aceptar";
            this.BTNAceptar.Text = "Aceptar";
            this.BTNAceptar.UseVisualStyleBackColor = true;
            this.BTNAceptar.Click += new System.EventHandler(this.BTNAceptar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(284, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 14;
            this.label3.Tag = "Planes";
            this.label3.Text = "Planes";
            // 
            // DGPlan
            // 
            this.DGPlan.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGPlan.Location = new System.Drawing.Point(287, 77);
            this.DGPlan.Name = "DGPlan";
            this.DGPlan.Size = new System.Drawing.Size(465, 128);
            this.DGPlan.TabIndex = 13;
            this.DGPlan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGPlan_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(286, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 12;
            this.label2.Tag = "Producto";
            this.label2.Text = "Producto";
            // 
            // TXTProducto
            // 
            this.TXTProducto.Location = new System.Drawing.Point(287, 30);
            this.TXTProducto.Name = "TXTProducto";
            this.TXTProducto.ReadOnly = true;
            this.TXTProducto.Size = new System.Drawing.Size(465, 20);
            this.TXTProducto.TabIndex = 11;
            this.TXTProducto.MouseLeave += new System.EventHandler(this.TXTProducto_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 10;
            this.label1.Tag = "NPoliza";
            this.label1.Text = "Numero de polza";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 54);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // DGCoberura
            // 
            this.DGCoberura.BackgroundColor = System.Drawing.Color.CadetBlue;
            this.DGCoberura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGCoberura.Location = new System.Drawing.Point(287, 229);
            this.DGCoberura.Name = "DGCoberura";
            this.DGCoberura.Size = new System.Drawing.Size(465, 198);
            this.DGCoberura.TabIndex = 18;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(147, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 26);
            this.button1.TabIndex = 19;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // _686DPfrmModificarSeguro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DGCoberura);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAceptar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGPlan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TXTProducto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "_686DPfrmModificarSeguro";
            this.Tag = "_686DPfrmModificarSeguro";
            this.Text = "_686DPfrmModificarSeguro";
            this.Load += new System.EventHandler(this._686DPfrmModificarSeguro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGCoberura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNAceptar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DGPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TXTProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView DGCoberura;
        private System.Windows.Forms.Button button1;
    }
}