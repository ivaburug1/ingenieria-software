namespace StageLink
{
    partial class VenderBoleto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VenderBoleto));
            this.TXTDNICliente = new System.Windows.Forms.TextBox();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLDNICliente = new System.Windows.Forms.Label();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNSeleccionarEvento = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXTDNICliente
            // 
            this.TXTDNICliente.Location = new System.Drawing.Point(12, 83);
            this.TXTDNICliente.Name = "TXTDNICliente";
            this.TXTDNICliente.Size = new System.Drawing.Size(121, 20);
            this.TXTDNICliente.TabIndex = 6;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(71)))), ((int)(((byte)(254)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(148, 25);
            this.LBLTitulo.TabIndex = 7;
            this.LBLTitulo.Text = "Vender Boleto";
            // 
            // LBLDNICliente
            // 
            this.LBLDNICliente.AutoSize = true;
            this.LBLDNICliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(71)))), ((int)(((byte)(254)))));
            this.LBLDNICliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.LBLDNICliente.Location = new System.Drawing.Point(9, 63);
            this.LBLDNICliente.Name = "LBLDNICliente";
            this.LBLDNICliente.Size = new System.Drawing.Size(366, 17);
            this.LBLDNICliente.TabIndex = 8;
            this.LBLDNICliente.Text = "Ingresar el DNI del cliente al que le va a vender el Boleto";
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(180, 263);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 12;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNSeleccionarEvento
            // 
            this.BTNSeleccionarEvento.Location = new System.Drawing.Point(261, 263);
            this.BTNSeleccionarEvento.Name = "BTNSeleccionarEvento";
            this.BTNSeleccionarEvento.Size = new System.Drawing.Size(122, 23);
            this.BTNSeleccionarEvento.TabIndex = 13;
            this.BTNSeleccionarEvento.Text = "Seleccionar Evento";
            this.BTNSeleccionarEvento.UseVisualStyleBackColor = true;
            this.BTNSeleccionarEvento.Click += new System.EventHandler(this.BTNSeleccionarEvento_Click);
            // 
            // VenderBoleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(395, 298);
            this.Controls.Add(this.BTNSeleccionarEvento);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.LBLDNICliente);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.TXTDNICliente);
            this.Name = "VenderBoleto";
            this.Text = "VenderBoleto";
            this.Load += new System.EventHandler(this.VenderBoleto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TXTDNICliente;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLDNICliente;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNSeleccionarEvento;
    }
}