namespace StageLink
{
    partial class RegistrarProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrarProveedor));
            this.LBLTituloProveedor = new System.Windows.Forms.Label();
            this.LBLRegistrarProductoProveedor = new System.Windows.Forms.Label();
            this.LBLProveeorCUIT = new System.Windows.Forms.Label();
            this.TXTProveedorCUIT = new System.Windows.Forms.TextBox();
            this.LBLNombreProveedor = new System.Windows.Forms.Label();
            this.TXTNombreProveedor = new System.Windows.Forms.TextBox();
            this.TXTCorreoProveedor = new System.Windows.Forms.TextBox();
            this.LBLCorreoProveedor = new System.Windows.Forms.Label();
            this.DGVCargarProductos = new System.Windows.Forms.DataGridView();
            this.NombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BTNRegistrarProveedor = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.LBLTituloEditarProveedor = new System.Windows.Forms.Label();
            this.LBLTituloEditarProveedor2 = new System.Windows.Forms.Label();
            this.CBElegirProveedor = new System.Windows.Forms.ComboBox();
            this.BTNActualizarProveedor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVCargarProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // LBLTituloProveedor
            // 
            this.LBLTituloProveedor.AutoSize = true;
            this.LBLTituloProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(179)))), ((int)(((byte)(118)))));
            this.LBLTituloProveedor.Location = new System.Drawing.Point(12, 9);
            this.LBLTituloProveedor.Name = "LBLTituloProveedor";
            this.LBLTituloProveedor.Size = new System.Drawing.Size(150, 20);
            this.LBLTituloProveedor.TabIndex = 0;
            this.LBLTituloProveedor.Tag = "LBLTituloProveedor";
            this.LBLTituloProveedor.Text = "Registrar Proveedor";
            // 
            // LBLRegistrarProductoProveedor
            // 
            this.LBLRegistrarProductoProveedor.AutoSize = true;
            this.LBLRegistrarProductoProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLRegistrarProductoProveedor.Location = new System.Drawing.Point(517, 43);
            this.LBLRegistrarProductoProveedor.Name = "LBLRegistrarProductoProveedor";
            this.LBLRegistrarProductoProveedor.Size = new System.Drawing.Size(288, 20);
            this.LBLRegistrarProductoProveedor.TabIndex = 1;
            this.LBLRegistrarProductoProveedor.Tag = "LBLRegistrarProductoProveedor";
            this.LBLRegistrarProductoProveedor.Text = "Registrar Productos a Proveedor Nuevo";
            // 
            // LBLProveeorCUIT
            // 
            this.LBLProveeorCUIT.AutoSize = true;
            this.LBLProveeorCUIT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(179)))), ((int)(((byte)(118)))));
            this.LBLProveeorCUIT.Location = new System.Drawing.Point(12, 66);
            this.LBLProveeorCUIT.Name = "LBLProveeorCUIT";
            this.LBLProveeorCUIT.Size = new System.Drawing.Size(122, 20);
            this.LBLProveeorCUIT.TabIndex = 2;
            this.LBLProveeorCUIT.Tag = "LBLProveeorCUIT";
            this.LBLProveeorCUIT.Text = "CUIT Proveedor";
            // 
            // TXTProveedorCUIT
            // 
            this.TXTProveedorCUIT.Location = new System.Drawing.Point(16, 89);
            this.TXTProveedorCUIT.Name = "TXTProveedorCUIT";
            this.TXTProveedorCUIT.Size = new System.Drawing.Size(146, 26);
            this.TXTProveedorCUIT.TabIndex = 3;
            // 
            // LBLNombreProveedor
            // 
            this.LBLNombreProveedor.AutoSize = true;
            this.LBLNombreProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(236)))), ((int)(((byte)(217)))));
            this.LBLNombreProveedor.Location = new System.Drawing.Point(12, 137);
            this.LBLNombreProveedor.Name = "LBLNombreProveedor";
            this.LBLNombreProveedor.Size = new System.Drawing.Size(141, 20);
            this.LBLNombreProveedor.TabIndex = 4;
            this.LBLNombreProveedor.Tag = "LBLNombreProveedor";
            this.LBLNombreProveedor.Text = "Nombre Proveedor";
            // 
            // TXTNombreProveedor
            // 
            this.TXTNombreProveedor.Location = new System.Drawing.Point(16, 160);
            this.TXTNombreProveedor.Name = "TXTNombreProveedor";
            this.TXTNombreProveedor.Size = new System.Drawing.Size(146, 26);
            this.TXTNombreProveedor.TabIndex = 5;
            // 
            // TXTCorreoProveedor
            // 
            this.TXTCorreoProveedor.Location = new System.Drawing.Point(16, 236);
            this.TXTCorreoProveedor.Name = "TXTCorreoProveedor";
            this.TXTCorreoProveedor.Size = new System.Drawing.Size(146, 26);
            this.TXTCorreoProveedor.TabIndex = 7;
            // 
            // LBLCorreoProveedor
            // 
            this.LBLCorreoProveedor.AutoSize = true;
            this.LBLCorreoProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(236)))), ((int)(((byte)(217)))));
            this.LBLCorreoProveedor.Location = new System.Drawing.Point(12, 213);
            this.LBLCorreoProveedor.Name = "LBLCorreoProveedor";
            this.LBLCorreoProveedor.Size = new System.Drawing.Size(133, 20);
            this.LBLCorreoProveedor.TabIndex = 6;
            this.LBLCorreoProveedor.Tag = "LBLCorreoProveedor";
            this.LBLCorreoProveedor.Text = "Correo Proveedor";
            // 
            // DGVCargarProductos
            // 
            this.DGVCargarProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVCargarProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreProducto,
            this.Precio,
            this.TipoProducto});
            this.DGVCargarProductos.Location = new System.Drawing.Point(233, 66);
            this.DGVCargarProductos.Name = "DGVCargarProductos";
            this.DGVCargarProductos.RowTemplate.Height = 28;
            this.DGVCargarProductos.Size = new System.Drawing.Size(938, 486);
            this.DGVCargarProductos.TabIndex = 8;
            // 
            // NombreProducto
            // 
            this.NombreProducto.HeaderText = "Nombre Producto";
            this.NombreProducto.Name = "NombreProducto";
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            // 
            // TipoProducto
            // 
            this.TipoProducto.HeaderText = "Tipo de Producto";
            this.TipoProducto.Name = "TipoProducto";
            // 
            // BTNRegistrarProveedor
            // 
            this.BTNRegistrarProveedor.Location = new System.Drawing.Point(1003, 558);
            this.BTNRegistrarProveedor.Name = "BTNRegistrarProveedor";
            this.BTNRegistrarProveedor.Size = new System.Drawing.Size(168, 32);
            this.BTNRegistrarProveedor.TabIndex = 9;
            this.BTNRegistrarProveedor.Tag = "BTNRegistrarProveedor";
            this.BTNRegistrarProveedor.Text = "Registrar Proveedor";
            this.BTNRegistrarProveedor.UseVisualStyleBackColor = true;
            this.BTNRegistrarProveedor.Click += new System.EventHandler(this.BTNRegistrarProveedor_Click_1);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(233, 558);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(113, 32);
            this.BTNSalir.TabIndex = 10;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // LBLTituloEditarProveedor
            // 
            this.LBLTituloEditarProveedor.AutoSize = true;
            this.LBLTituloEditarProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLTituloEditarProveedor.Location = new System.Drawing.Point(12, 593);
            this.LBLTituloEditarProveedor.Name = "LBLTituloEditarProveedor";
            this.LBLTituloEditarProveedor.Size = new System.Drawing.Size(266, 20);
            this.LBLTituloEditarProveedor.TabIndex = 11;
            this.LBLTituloEditarProveedor.Tag = "LBLTituloEditarProveedor";
            this.LBLTituloEditarProveedor.Text = "Tambien podes Editar un Proveedor.";
            // 
            // LBLTituloEditarProveedor2
            // 
            this.LBLTituloEditarProveedor2.AutoSize = true;
            this.LBLTituloEditarProveedor2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLTituloEditarProveedor2.Location = new System.Drawing.Point(12, 613);
            this.LBLTituloEditarProveedor2.Name = "LBLTituloEditarProveedor2";
            this.LBLTituloEditarProveedor2.Size = new System.Drawing.Size(320, 20);
            this.LBLTituloEditarProveedor2.TabIndex = 12;
            this.LBLTituloEditarProveedor2.Tag = "LBLTituloEditarProveedor2";
            this.LBLTituloEditarProveedor2.Text = "Elegir el Proveedor que desea editar debajo:";
            // 
            // CBElegirProveedor
            // 
            this.CBElegirProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBElegirProveedor.FormattingEnabled = true;
            this.CBElegirProveedor.Location = new System.Drawing.Point(16, 636);
            this.CBElegirProveedor.Name = "CBElegirProveedor";
            this.CBElegirProveedor.Size = new System.Drawing.Size(316, 28);
            this.CBElegirProveedor.TabIndex = 13;
            this.CBElegirProveedor.SelectedIndexChanged += new System.EventHandler(this.CBElegirProveedor_SelectedIndexChanged);
            // 
            // BTNActualizarProveedor
            // 
            this.BTNActualizarProveedor.Location = new System.Drawing.Point(16, 670);
            this.BTNActualizarProveedor.Name = "BTNActualizarProveedor";
            this.BTNActualizarProveedor.Size = new System.Drawing.Size(171, 32);
            this.BTNActualizarProveedor.TabIndex = 14;
            this.BTNActualizarProveedor.Tag = "BTNActualizarProveedor";
            this.BTNActualizarProveedor.Text = "Actualizar Proveedor";
            this.BTNActualizarProveedor.UseVisualStyleBackColor = true;
            this.BTNActualizarProveedor.Click += new System.EventHandler(this.BTNActualizarProveedor_Click);
            // 
            // RegistrarProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1218, 712);
            this.Controls.Add(this.BTNActualizarProveedor);
            this.Controls.Add(this.CBElegirProveedor);
            this.Controls.Add(this.LBLTituloEditarProveedor2);
            this.Controls.Add(this.LBLTituloEditarProveedor);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNRegistrarProveedor);
            this.Controls.Add(this.DGVCargarProductos);
            this.Controls.Add(this.TXTCorreoProveedor);
            this.Controls.Add(this.LBLCorreoProveedor);
            this.Controls.Add(this.TXTNombreProveedor);
            this.Controls.Add(this.LBLNombreProveedor);
            this.Controls.Add(this.TXTProveedorCUIT);
            this.Controls.Add(this.LBLProveeorCUIT);
            this.Controls.Add(this.LBLRegistrarProductoProveedor);
            this.Controls.Add(this.LBLTituloProveedor);
            this.Name = "RegistrarProveedor";
            this.Text = "Registrar Proveedor";
            this.Load += new System.EventHandler(this.RegistrarProveedor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVCargarProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTituloProveedor;
        private System.Windows.Forms.Label LBLRegistrarProductoProveedor;
        private System.Windows.Forms.Label LBLProveeorCUIT;
        private System.Windows.Forms.TextBox TXTProveedorCUIT;
        private System.Windows.Forms.Label LBLNombreProveedor;
        private System.Windows.Forms.TextBox TXTNombreProveedor;
        private System.Windows.Forms.TextBox TXTCorreoProveedor;
        private System.Windows.Forms.Label LBLCorreoProveedor;
        private System.Windows.Forms.DataGridView DGVCargarProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoProducto;
        private System.Windows.Forms.Button BTNRegistrarProveedor;
        private System.Windows.Forms.Button BTNSalir;
        private System.Windows.Forms.Label LBLTituloEditarProveedor;
        private System.Windows.Forms.Label LBLTituloEditarProveedor2;
        private System.Windows.Forms.ComboBox CBElegirProveedor;
        private System.Windows.Forms.Button BTNActualizarProveedor;
    }
}