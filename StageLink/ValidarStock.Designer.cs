namespace StageLink
{
    partial class ValidarStock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidarStock));
            this.LBLNoHaySuficiente = new System.Windows.Forms.Label();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.DGVStockProductos = new System.Windows.Forms.DataGridView();
            this.CBNombreProducto = new System.Windows.Forms.ComboBox();
            this.CBTipoProducto = new System.Windows.Forms.ComboBox();
            this.BTNFiltro = new System.Windows.Forms.Button();
            this.LBLNombreProducto = new System.Windows.Forms.Label();
            this.LBLTipoProducto = new System.Windows.Forms.Label();
            this.BTNComprarProducto = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVStockProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // LBLNoHaySuficiente
            // 
            this.LBLNoHaySuficiente.AutoSize = true;
            this.LBLNoHaySuficiente.Location = new System.Drawing.Point(990, 562);
            this.LBLNoHaySuficiente.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLNoHaySuficiente.Name = "LBLNoHaySuficiente";
            this.LBLNoHaySuficiente.Size = new System.Drawing.Size(187, 20);
            this.LBLNoHaySuficiente.TabIndex = 1;
            this.LBLNoHaySuficiente.Tag = "LBLNoHaySuficiente";
            this.LBLNoHaySuficiente.Text = "No hay Stock Suficiente?";
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(198)))), ((int)(((byte)(151)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LBLTitulo.Location = new System.Drawing.Point(18, 14);
            this.LBLTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(127, 20);
            this.LBLTitulo.TabIndex = 2;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Validacion Stock";
            // 
            // DGVStockProductos
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVStockProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVStockProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVStockProductos.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVStockProductos.Location = new System.Drawing.Point(18, 49);
            this.DGVStockProductos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGVStockProductos.Name = "DGVStockProductos";
            this.DGVStockProductos.Size = new System.Drawing.Size(1164, 500);
            this.DGVStockProductos.TabIndex = 3;
            this.DGVStockProductos.Tag = "DGVStockProductos";
            // 
            // CBNombreProducto
            // 
            this.CBNombreProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBNombreProducto.FormattingEnabled = true;
            this.CBNombreProducto.Location = new System.Drawing.Point(18, 589);
            this.CBNombreProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBNombreProducto.Name = "CBNombreProducto";
            this.CBNombreProducto.Size = new System.Drawing.Size(376, 28);
            this.CBNombreProducto.TabIndex = 4;
            // 
            // CBTipoProducto
            // 
            this.CBTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTipoProducto.FormattingEnabled = true;
            this.CBTipoProducto.Location = new System.Drawing.Point(405, 589);
            this.CBTipoProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBTipoProducto.Name = "CBTipoProducto";
            this.CBTipoProducto.Size = new System.Drawing.Size(376, 28);
            this.CBTipoProducto.TabIndex = 5;
            // 
            // BTNFiltro
            // 
            this.BTNFiltro.Location = new System.Drawing.Point(792, 586);
            this.BTNFiltro.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNFiltro.Name = "BTNFiltro";
            this.BTNFiltro.Size = new System.Drawing.Size(112, 35);
            this.BTNFiltro.TabIndex = 6;
            this.BTNFiltro.Tag = "BTNFiltro";
            this.BTNFiltro.Text = "Filtro";
            this.BTNFiltro.UseVisualStyleBackColor = true;
            this.BTNFiltro.Click += new System.EventHandler(this.BTNFiltro_Click);
            // 
            // LBLNombreProducto
            // 
            this.LBLNombreProducto.AutoSize = true;
            this.LBLNombreProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLNombreProducto.Location = new System.Drawing.Point(16, 562);
            this.LBLNombreProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLNombreProducto.Name = "LBLNombreProducto";
            this.LBLNombreProducto.Size = new System.Drawing.Size(155, 20);
            this.LBLNombreProducto.TabIndex = 7;
            this.LBLNombreProducto.Tag = "LBLNombreProducto";
            this.LBLNombreProducto.Text = "Nombre de Producto";
            // 
            // LBLTipoProducto
            // 
            this.LBLTipoProducto.AutoSize = true;
            this.LBLTipoProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(253)))));
            this.LBLTipoProducto.Location = new System.Drawing.Point(400, 562);
            this.LBLTipoProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLTipoProducto.Name = "LBLTipoProducto";
            this.LBLTipoProducto.Size = new System.Drawing.Size(129, 20);
            this.LBLTipoProducto.TabIndex = 8;
            this.LBLTipoProducto.Tag = "LBLTipoProducto";
            this.LBLTipoProducto.Text = "Tipo de Producto";
            // 
            // BTNComprarProducto
            // 
            this.BTNComprarProducto.Location = new System.Drawing.Point(1017, 586);
            this.BTNComprarProducto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNComprarProducto.Name = "BTNComprarProducto";
            this.BTNComprarProducto.Size = new System.Drawing.Size(165, 35);
            this.BTNComprarProducto.TabIndex = 0;
            this.BTNComprarProducto.Tag = "BTNComprarProducto";
            this.BTNComprarProducto.Text = "Comprar Producto";
            this.BTNComprarProducto.UseVisualStyleBackColor = true;
            this.BTNComprarProducto.Click += new System.EventHandler(this.BTNComprarProducto_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(1070, 643);
            this.BTNSalir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(112, 35);
            this.BTNSalir.TabIndex = 9;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // ValidarStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(193)))), ((int)(((byte)(144)))));
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.LBLTipoProducto);
            this.Controls.Add(this.LBLNombreProducto);
            this.Controls.Add(this.BTNFiltro);
            this.Controls.Add(this.CBTipoProducto);
            this.Controls.Add(this.CBNombreProducto);
            this.Controls.Add(this.DGVStockProductos);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.LBLNoHaySuficiente);
            this.Controls.Add(this.BTNComprarProducto);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ValidarStock";
            this.Text = "ValidarStock";
            this.Load += new System.EventHandler(this.ValidarStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVStockProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LBLNoHaySuficiente;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.DataGridView DGVStockProductos;
        private System.Windows.Forms.ComboBox CBNombreProducto;
        private System.Windows.Forms.ComboBox CBTipoProducto;
        private System.Windows.Forms.Button BTNFiltro;
        private System.Windows.Forms.Label LBLNombreProducto;
        private System.Windows.Forms.Label LBLTipoProducto;
        private System.Windows.Forms.Button BTNComprarProducto;
        private System.Windows.Forms.Button BTNSalir;
    }
}