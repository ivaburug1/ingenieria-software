namespace StageLink
{
    partial class LBLProductoCantidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LBLProductoCantidad));
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLProveedor = new System.Windows.Forms.Label();
            this.LBLProducto = new System.Windows.Forms.Label();
            this.LBLPrecioTotal = new System.Windows.Forms.Label();
            this.LBLPrecioTotalCantidad = new System.Windows.Forms.Label();
            this.LBLCantidadProducto = new System.Windows.Forms.Label();
            this.LBLCantidadActual = new System.Windows.Forms.Label();
            this.LBLCantPallets = new System.Windows.Forms.Label();
            this.CBProveedor = new System.Windows.Forms.ComboBox();
            this.CBCantComprar = new System.Windows.Forms.ComboBox();
            this.CBProducto = new System.Windows.Forms.ComboBox();
            this.BTNComprar = new System.Windows.Forms.Button();
            this.BTNRegistrarProveedor = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(195)))), ((int)(((byte)(132)))));
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(146, 20);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Comprar Productos";
            // 
            // LBLProveedor
            // 
            this.LBLProveedor.AutoSize = true;
            this.LBLProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(195)))), ((int)(((byte)(132)))));
            this.LBLProveedor.Location = new System.Drawing.Point(12, 41);
            this.LBLProveedor.Name = "LBLProveedor";
            this.LBLProveedor.Size = new System.Drawing.Size(81, 20);
            this.LBLProveedor.TabIndex = 1;
            this.LBLProveedor.Tag = "LBLProveedor";
            this.LBLProveedor.Text = "Proveedor";
            // 
            // LBLProducto
            // 
            this.LBLProducto.AutoSize = true;
            this.LBLProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLProducto.Location = new System.Drawing.Point(35, 111);
            this.LBLProducto.Name = "LBLProducto";
            this.LBLProducto.Size = new System.Drawing.Size(73, 20);
            this.LBLProducto.TabIndex = 2;
            this.LBLProducto.Tag = "LBLProducto";
            this.LBLProducto.Text = "Producto";
            // 
            // LBLPrecioTotal
            // 
            this.LBLPrecioTotal.AutoSize = true;
            this.LBLPrecioTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLPrecioTotal.Location = new System.Drawing.Point(12, 243);
            this.LBLPrecioTotal.Name = "LBLPrecioTotal";
            this.LBLPrecioTotal.Size = new System.Drawing.Size(105, 20);
            this.LBLPrecioTotal.TabIndex = 3;
            this.LBLPrecioTotal.Tag = "LBLPrecioTotal";
            this.LBLPrecioTotal.Text = "Precio Total =";
            // 
            // LBLPrecioTotalCantidad
            // 
            this.LBLPrecioTotalCantidad.AutoSize = true;
            this.LBLPrecioTotalCantidad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLPrecioTotalCantidad.Location = new System.Drawing.Point(123, 243);
            this.LBLPrecioTotalCantidad.Name = "LBLPrecioTotalCantidad";
            this.LBLPrecioTotalCantidad.Size = new System.Drawing.Size(27, 20);
            this.LBLPrecioTotalCantidad.TabIndex = 6;
            this.LBLPrecioTotalCantidad.Tag = "LBLPrecioTotalCantidad";
            this.LBLPrecioTotalCantidad.Text = "$ -";
            // 
            // LBLCantidadProducto
            // 
            this.LBLCantidadProducto.AutoSize = true;
            this.LBLCantidadProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLCantidadProducto.Location = new System.Drawing.Point(366, 137);
            this.LBLCantidadProducto.Name = "LBLCantidadProducto";
            this.LBLCantidadProducto.Size = new System.Drawing.Size(14, 20);
            this.LBLCantidadProducto.TabIndex = 5;
            this.LBLCantidadProducto.Tag = "LBLCantidadProducto";
            this.LBLCantidadProducto.Text = "-";
            // 
            // LBLCantidadActual
            // 
            this.LBLCantidadActual.AutoSize = true;
            this.LBLCantidadActual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLCantidadActual.Location = new System.Drawing.Point(225, 137);
            this.LBLCantidadActual.Name = "LBLCantidadActual";
            this.LBLCantidadActual.Size = new System.Drawing.Size(135, 20);
            this.LBLCantidadActual.TabIndex = 4;
            this.LBLCantidadActual.Tag = "LBLCantidadActual";
            this.LBLCantidadActual.Text = "Cantidad Actual =";
            // 
            // LBLCantPallets
            // 
            this.LBLCantPallets.AutoSize = true;
            this.LBLCantPallets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLCantPallets.Location = new System.Drawing.Point(12, 184);
            this.LBLCantPallets.Name = "LBLCantPallets";
            this.LBLCantPallets.Size = new System.Drawing.Size(315, 20);
            this.LBLCantPallets.TabIndex = 7;
            this.LBLCantPallets.Tag = "LBLCantPallets";
            this.LBLCantPallets.Text = "Seleccionar Cantidad de Pallets a Comprar.";
            // 
            // CBProveedor
            // 
            this.CBProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBProveedor.FormattingEnabled = true;
            this.CBProveedor.Location = new System.Drawing.Point(12, 64);
            this.CBProveedor.Name = "CBProveedor";
            this.CBProveedor.Size = new System.Drawing.Size(210, 28);
            this.CBProveedor.TabIndex = 8;
            this.CBProveedor.SelectedIndexChanged += new System.EventHandler(this.CBProveedor_SelectedIndexChanged);
            // 
            // CBCantComprar
            // 
            this.CBCantComprar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBCantComprar.FormattingEnabled = true;
            this.CBCantComprar.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.CBCantComprar.Location = new System.Drawing.Point(333, 181);
            this.CBCantComprar.Name = "CBCantComprar";
            this.CBCantComprar.Size = new System.Drawing.Size(57, 28);
            this.CBCantComprar.TabIndex = 9;
            this.CBCantComprar.SelectedIndexChanged += new System.EventHandler(this.CBCantComprar_SelectedIndexChanged);
            // 
            // CBProducto
            // 
            this.CBProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBProducto.FormattingEnabled = true;
            this.CBProducto.Location = new System.Drawing.Point(12, 134);
            this.CBProducto.Name = "CBProducto";
            this.CBProducto.Size = new System.Drawing.Size(210, 28);
            this.CBProducto.TabIndex = 10;
            this.CBProducto.SelectedIndexChanged += new System.EventHandler(this.CBProducto_SelectedIndexChanged);
            // 
            // BTNComprar
            // 
            this.BTNComprar.Location = new System.Drawing.Point(12, 266);
            this.BTNComprar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNComprar.Name = "BTNComprar";
            this.BTNComprar.Size = new System.Drawing.Size(116, 33);
            this.BTNComprar.TabIndex = 11;
            this.BTNComprar.Tag = "BTNComprar";
            this.BTNComprar.Text = "Comprar";
            this.BTNComprar.UseVisualStyleBackColor = true;
            this.BTNComprar.Click += new System.EventHandler(this.BTNComprar_Click_1);
            // 
            // BTNRegistrarProveedor
            // 
            this.BTNRegistrarProveedor.Location = new System.Drawing.Point(171, 266);
            this.BTNRegistrarProveedor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNRegistrarProveedor.Name = "BTNRegistrarProveedor";
            this.BTNRegistrarProveedor.Size = new System.Drawing.Size(219, 33);
            this.BTNRegistrarProveedor.TabIndex = 12;
            this.BTNRegistrarProveedor.Tag = "BTNRegistrarProveedor";
            this.BTNRegistrarProveedor.Text = "Registrar Nuevo Proveedor";
            this.BTNRegistrarProveedor.UseVisualStyleBackColor = true;
            this.BTNRegistrarProveedor.Click += new System.EventHandler(this.BTNRegistrarProveedor_Click_1);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(12, 307);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(116, 33);
            this.BTNSalir.TabIndex = 13;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // LBLProductoCantidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(404, 450);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNRegistrarProveedor);
            this.Controls.Add(this.BTNComprar);
            this.Controls.Add(this.CBProducto);
            this.Controls.Add(this.CBCantComprar);
            this.Controls.Add(this.CBProveedor);
            this.Controls.Add(this.LBLCantPallets);
            this.Controls.Add(this.LBLPrecioTotalCantidad);
            this.Controls.Add(this.LBLCantidadProducto);
            this.Controls.Add(this.LBLCantidadActual);
            this.Controls.Add(this.LBLPrecioTotal);
            this.Controls.Add(this.LBLProducto);
            this.Controls.Add(this.LBLProveedor);
            this.Controls.Add(this.LBLTitulo);
            this.Name = "LBLProductoCantidad";
            this.Text = "ComprarProducto";
            this.Load += new System.EventHandler(this.LBLProductoCantidad_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLProveedor;
        private System.Windows.Forms.Label LBLProducto;
        private System.Windows.Forms.Label LBLPrecioTotal;
        private System.Windows.Forms.Label LBLPrecioTotalCantidad;
        private System.Windows.Forms.Label LBLCantidadProducto;
        private System.Windows.Forms.Label LBLCantidadActual;
        private System.Windows.Forms.Label LBLCantPallets;
        private System.Windows.Forms.ComboBox CBProveedor;
        private System.Windows.Forms.ComboBox CBCantComprar;
        private System.Windows.Forms.ComboBox CBProducto;
        private System.Windows.Forms.Button BTNComprar;
        private System.Windows.Forms.Button BTNRegistrarProveedor;
        private System.Windows.Forms.Button BTNSalir;
    }
}