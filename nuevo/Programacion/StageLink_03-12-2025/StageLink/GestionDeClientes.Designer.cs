namespace StageLink
{
    partial class GestionDeClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionDeClientes));
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.DGVMuestraClientes = new System.Windows.Forms.DataGridView();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLBuscar = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLApellido = new System.Windows.Forms.Label();
            this.LBLCorreo = new System.Windows.Forms.Label();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.TXTApellido = new System.Windows.Forms.TextBox();
            this.TXTCorreo = new System.Windows.Forms.TextBox();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNBuscar = new System.Windows.Forms.Button();
            this.LBLNombre = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(40, 412);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(100, 20);
            this.TXTDNI.TabIndex = 0;
            // 
            // DGVMuestraClientes
            // 
            this.DGVMuestraClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMuestraClientes.Location = new System.Drawing.Point(37, 92);
            this.DGVMuestraClientes.Name = "DGVMuestraClientes";
            this.DGVMuestraClientes.Size = new System.Drawing.Size(495, 262);
            this.DGVMuestraClientes.TabIndex = 1;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.LBLTitulo.Location = new System.Drawing.Point(33, 35);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(298, 39);
            this.LBLTitulo.TabIndex = 2;
            this.LBLTitulo.Text = "Gestor de Clientes";
            // 
            // LBLBuscar
            // 
            this.LBLBuscar.AutoSize = true;
            this.LBLBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.LBLBuscar.Location = new System.Drawing.Point(34, 357);
            this.LBLBuscar.Name = "LBLBuscar";
            this.LBLBuscar.Size = new System.Drawing.Size(99, 31);
            this.LBLBuscar.TabIndex = 3;
            this.LBLBuscar.Text = "Buscar";
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLDNI.Location = new System.Drawing.Point(37, 396);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(26, 13);
            this.LBLDNI.TabIndex = 4;
            this.LBLDNI.Text = "DNI";
            // 
            // LBLApellido
            // 
            this.LBLApellido.AutoSize = true;
            this.LBLApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.LBLApellido.Location = new System.Drawing.Point(249, 396);
            this.LBLApellido.Name = "LBLApellido";
            this.LBLApellido.Size = new System.Drawing.Size(44, 13);
            this.LBLApellido.TabIndex = 6;
            this.LBLApellido.Text = "Apellido";
            // 
            // LBLCorreo
            // 
            this.LBLCorreo.AutoSize = true;
            this.LBLCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.LBLCorreo.Location = new System.Drawing.Point(355, 396);
            this.LBLCorreo.Name = "LBLCorreo";
            this.LBLCorreo.Size = new System.Drawing.Size(35, 13);
            this.LBLCorreo.TabIndex = 7;
            this.LBLCorreo.Text = "e-Mail";
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(146, 412);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(100, 20);
            this.TXTNombre.TabIndex = 8;
            // 
            // TXTApellido
            // 
            this.TXTApellido.Location = new System.Drawing.Point(252, 412);
            this.TXTApellido.Name = "TXTApellido";
            this.TXTApellido.Size = new System.Drawing.Size(100, 20);
            this.TXTApellido.TabIndex = 9;
            // 
            // TXTCorreo
            // 
            this.TXTCorreo.Location = new System.Drawing.Point(361, 412);
            this.TXTCorreo.Name = "TXTCorreo";
            this.TXTCorreo.Size = new System.Drawing.Size(100, 20);
            this.TXTCorreo.TabIndex = 10;
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(538, 92);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(75, 23);
            this.BTNAplicar.TabIndex = 11;
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click_1);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(619, 92);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 12;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click_1);
            // 
            // BTNBuscar
            // 
            this.BTNBuscar.Location = new System.Drawing.Point(467, 412);
            this.BTNBuscar.Name = "BTNBuscar";
            this.BTNBuscar.Size = new System.Drawing.Size(75, 23);
            this.BTNBuscar.TabIndex = 13;
            this.BTNBuscar.Text = "Buscar";
            this.BTNBuscar.UseVisualStyleBackColor = true;
            this.BTNBuscar.Click += new System.EventHandler(this.BTNBuscar_Click_1);
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLNombre.Location = new System.Drawing.Point(143, 396);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(44, 13);
            this.LBLNombre.TabIndex = 14;
            this.LBLNombre.Text = "Nombre";
            // 
            // GestionDeClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(784, 468);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.BTNBuscar);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAplicar);
            this.Controls.Add(this.TXTCorreo);
            this.Controls.Add(this.TXTApellido);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.LBLCorreo);
            this.Controls.Add(this.LBLApellido);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLBuscar);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.DGVMuestraClientes);
            this.Controls.Add(this.TXTDNI);
            this.Name = "GestionDeClientes";
            this.Text = "GestionDeClientes";
            this.Load += new System.EventHandler(this.GestionDeClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.DataGridView DGVMuestraClientes;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLBuscar;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLApellido;
        private System.Windows.Forms.Label LBLCorreo;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.TextBox TXTApellido;
        private System.Windows.Forms.TextBox TXTCorreo;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNBuscar;
        private System.Windows.Forms.Label LBLNombre;
    }
}