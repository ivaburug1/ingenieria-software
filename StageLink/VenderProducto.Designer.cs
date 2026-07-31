namespace StageLink
{
    partial class VenderProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VenderProducto));
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLProductoAVender = new System.Windows.Forms.Label();
            this.LBLEventoParaProducto = new System.Windows.Forms.Label();
            this.LBLCantidadAVender = new System.Windows.Forms.Label();
            this.LBLStockActual = new System.Windows.Forms.Label();
            this.LBLCantStockActual = new System.Windows.Forms.Label();
            this.CBProducto = new System.Windows.Forms.ComboBox();
            this.CBEvento = new System.Windows.Forms.ComboBox();
            this.TXTCantVender = new System.Windows.Forms.TextBox();
            this.BTNVenderProducto = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(200, 20);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Vender Producto a Evento ";
            // 
            // LBLProductoAVender
            // 
            this.LBLProductoAVender.AutoSize = true;
            this.LBLProductoAVender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLProductoAVender.Location = new System.Drawing.Point(8, 40);
            this.LBLProductoAVender.Name = "LBLProductoAVender";
            this.LBLProductoAVender.Size = new System.Drawing.Size(142, 20);
            this.LBLProductoAVender.TabIndex = 1;
            this.LBLProductoAVender.Tag = "LBLProductoAVender";
            this.LBLProductoAVender.Text = "Producto a Vender";
            // 
            // LBLEventoParaProducto
            // 
            this.LBLEventoParaProducto.AutoSize = true;
            this.LBLEventoParaProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLEventoParaProducto.Location = new System.Drawing.Point(280, 40);
            this.LBLEventoParaProducto.Name = "LBLEventoParaProducto";
            this.LBLEventoParaProducto.Size = new System.Drawing.Size(408, 20);
            this.LBLEventoParaProducto.TabIndex = 2;
            this.LBLEventoParaProducto.Tag = "LBLEventoParaProducto";
            this.LBLEventoParaProducto.Text = "Seleccionar el Evento al que le quiere vender el producto";
            // 
            // LBLCantidadAVender
            // 
            this.LBLCantidadAVender.AutoSize = true;
            this.LBLCantidadAVender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLCantidadAVender.Location = new System.Drawing.Point(8, 112);
            this.LBLCantidadAVender.Name = "LBLCantidadAVender";
            this.LBLCantidadAVender.Size = new System.Drawing.Size(142, 20);
            this.LBLCantidadAVender.TabIndex = 3;
            this.LBLCantidadAVender.Tag = "LBLCantidadAVender";
            this.LBLCantidadAVender.Text = "Cantidad a Vender";
            // 
            // LBLStockActual
            // 
            this.LBLStockActual.AutoSize = true;
            this.LBLStockActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLStockActual.Location = new System.Drawing.Point(8, 132);
            this.LBLStockActual.Name = "LBLStockActual";
            this.LBLStockActual.Size = new System.Drawing.Size(107, 20);
            this.LBLStockActual.TabIndex = 4;
            this.LBLStockActual.Tag = "LBLStockActual";
            this.LBLStockActual.Text = "Stock Actual: ";
            // 
            // LBLCantStockActual
            // 
            this.LBLCantStockActual.AutoSize = true;
            this.LBLCantStockActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(200)))), ((int)(((byte)(153)))));
            this.LBLCantStockActual.Location = new System.Drawing.Point(110, 132);
            this.LBLCantStockActual.Name = "LBLCantStockActual";
            this.LBLCantStockActual.Size = new System.Drawing.Size(14, 20);
            this.LBLCantStockActual.TabIndex = 5;
            this.LBLCantStockActual.Tag = "LBLCantStockActual";
            this.LBLCantStockActual.Text = "-";
            // 
            // CBProducto
            // 
            this.CBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBProducto.FormattingEnabled = true;
            this.CBProducto.Location = new System.Drawing.Point(16, 73);
            this.CBProducto.Name = "CBProducto";
            this.CBProducto.Size = new System.Drawing.Size(249, 28);
            this.CBProducto.TabIndex = 6;
            this.CBProducto.SelectedIndexChanged += new System.EventHandler(this.CBProducto_SelectedIndexChanged);
            // 
            // CBEvento
            // 
            this.CBEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBEvento.FormattingEnabled = true;
            this.CBEvento.Location = new System.Drawing.Point(284, 73);
            this.CBEvento.Name = "CBEvento";
            this.CBEvento.Size = new System.Drawing.Size(404, 28);
            this.CBEvento.TabIndex = 7;
            // 
            // TXTCantVender
            // 
            this.TXTCantVender.Location = new System.Drawing.Point(12, 155);
            this.TXTCantVender.Name = "TXTCantVender";
            this.TXTCantVender.Size = new System.Drawing.Size(163, 26);
            this.TXTCantVender.TabIndex = 8;
            // 
            // BTNVenderProducto
            // 
            this.BTNVenderProducto.Location = new System.Drawing.Point(533, 151);
            this.BTNVenderProducto.Name = "BTNVenderProducto";
            this.BTNVenderProducto.Size = new System.Drawing.Size(155, 35);
            this.BTNVenderProducto.TabIndex = 9;
            this.BTNVenderProducto.Tag = "BTNVenderProducto";
            this.BTNVenderProducto.Text = "Vender Producto";
            this.BTNVenderProducto.UseVisualStyleBackColor = true;
            this.BTNVenderProducto.Click += new System.EventHandler(this.BTNVenderProducto_Click_1);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(372, 151);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(155, 35);
            this.BTNSalir.TabIndex = 10;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // VenderProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(705, 202);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNVenderProducto);
            this.Controls.Add(this.TXTCantVender);
            this.Controls.Add(this.CBEvento);
            this.Controls.Add(this.CBProducto);
            this.Controls.Add(this.LBLCantStockActual);
            this.Controls.Add(this.LBLStockActual);
            this.Controls.Add(this.LBLCantidadAVender);
            this.Controls.Add(this.LBLEventoParaProducto);
            this.Controls.Add(this.LBLProductoAVender);
            this.Controls.Add(this.LBLTitulo);
            this.Name = "VenderProducto";
            this.Text = "VenderProducto";
            this.Load += new System.EventHandler(this.VenderProducto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLProductoAVender;
        private System.Windows.Forms.Label LBLEventoParaProducto;
        private System.Windows.Forms.Label LBLCantidadAVender;
        private System.Windows.Forms.Label LBLStockActual;
        private System.Windows.Forms.Label LBLCantStockActual;
        private System.Windows.Forms.ComboBox CBProducto;
        private System.Windows.Forms.ComboBox CBEvento;
        private System.Windows.Forms.TextBox TXTCantVender;
        private System.Windows.Forms.Button BTNVenderProducto;
        private System.Windows.Forms.Button BTNSalir;
    }
}