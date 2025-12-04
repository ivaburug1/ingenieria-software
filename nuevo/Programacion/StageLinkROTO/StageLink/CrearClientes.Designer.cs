namespace StageLink
{
    partial class CrearClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrearClientes));
            this.TXTDNICliente = new System.Windows.Forms.TextBox();
            this.TXTNombreCliente = new System.Windows.Forms.TextBox();
            this.TXTApellidoCliente = new System.Windows.Forms.TextBox();
            this.TXTCorreoCliente = new System.Windows.Forms.TextBox();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLNombre = new System.Windows.Forms.Label();
            this.LBLApellido = new System.Windows.Forms.Label();
            this.LBLCorreo = new System.Windows.Forms.Label();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNCrearCliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXTDNICliente
            // 
            this.TXTDNICliente.Location = new System.Drawing.Point(124, 198);
            this.TXTDNICliente.Name = "TXTDNICliente";
            this.TXTDNICliente.Size = new System.Drawing.Size(100, 20);
            this.TXTDNICliente.TabIndex = 0;
            // 
            // TXTNombreCliente
            // 
            this.TXTNombreCliente.Location = new System.Drawing.Point(124, 244);
            this.TXTNombreCliente.Name = "TXTNombreCliente";
            this.TXTNombreCliente.Size = new System.Drawing.Size(100, 20);
            this.TXTNombreCliente.TabIndex = 1;
            // 
            // TXTApellidoCliente
            // 
            this.TXTApellidoCliente.Location = new System.Drawing.Point(124, 292);
            this.TXTApellidoCliente.Name = "TXTApellidoCliente";
            this.TXTApellidoCliente.Size = new System.Drawing.Size(100, 20);
            this.TXTApellidoCliente.TabIndex = 2;
            // 
            // TXTCorreoCliente
            // 
            this.TXTCorreoCliente.Location = new System.Drawing.Point(124, 338);
            this.TXTCorreoCliente.Name = "TXTCorreoCliente";
            this.TXTCorreoCliente.Size = new System.Drawing.Size(174, 20);
            this.TXTCorreoCliente.TabIndex = 3;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(316, 6);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(138, 25);
            this.LBLTitulo.TabIndex = 4;
            this.LBLTitulo.Text = "Crear Cliente";
            this.LBLTitulo.Click += new System.EventHandler(this.LBLTitulo_Click);
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLDNI.Location = new System.Drawing.Point(121, 182);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(165, 13);
            this.LBLDNI.TabIndex = 5;
            this.LBLDNI.Text = "Ingresar el DNI del cliente a crear";
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLNombre.Location = new System.Drawing.Point(121, 228);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(183, 13);
            this.LBLNombre.TabIndex = 6;
            this.LBLNombre.Text = "Ingresar el Nombre del cliente a crear";
            // 
            // LBLApellido
            // 
            this.LBLApellido.AutoSize = true;
            this.LBLApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLApellido.Location = new System.Drawing.Point(121, 276);
            this.LBLApellido.Name = "LBLApellido";
            this.LBLApellido.Size = new System.Drawing.Size(183, 13);
            this.LBLApellido.TabIndex = 7;
            this.LBLApellido.Text = "Ingresar el Apellido del cliente a crear";
            // 
            // LBLCorreo
            // 
            this.LBLCorreo.AutoSize = true;
            this.LBLCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLCorreo.Location = new System.Drawing.Point(121, 322);
            this.LBLCorreo.Name = "LBLCorreo";
            this.LBLCorreo.Size = new System.Drawing.Size(177, 13);
            this.LBLCorreo.TabIndex = 8;
            this.LBLCorreo.Text = "Ingresar el Correo del cliente a crear";
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(379, 412);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 9;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNCrearCliente
            // 
            this.BTNCrearCliente.Location = new System.Drawing.Point(298, 412);
            this.BTNCrearCliente.Name = "BTNCrearCliente";
            this.BTNCrearCliente.Size = new System.Drawing.Size(75, 23);
            this.BTNCrearCliente.TabIndex = 10;
            this.BTNCrearCliente.Text = "Crear Cliente";
            this.BTNCrearCliente.UseVisualStyleBackColor = true;
            this.BTNCrearCliente.Click += new System.EventHandler(this.BTNCrearCliente_Click);
            // 
            // CrearClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(459, 450);
            this.Controls.Add(this.BTNCrearCliente);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.LBLCorreo);
            this.Controls.Add(this.LBLApellido);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.TXTCorreoCliente);
            this.Controls.Add(this.TXTApellidoCliente);
            this.Controls.Add(this.TXTNombreCliente);
            this.Controls.Add(this.TXTDNICliente);
            this.Name = "CrearClientes";
            this.Text = "CrearClientes";
            this.Load += new System.EventHandler(this.CrearClientes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTDNICliente;
        private System.Windows.Forms.TextBox TXTNombreCliente;
        private System.Windows.Forms.TextBox TXTApellidoCliente;
        private System.Windows.Forms.TextBox TXTCorreoCliente;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLNombre;
        private System.Windows.Forms.Label LBLApellido;
        private System.Windows.Forms.Label LBLCorreo;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNCrearCliente;
    }
}