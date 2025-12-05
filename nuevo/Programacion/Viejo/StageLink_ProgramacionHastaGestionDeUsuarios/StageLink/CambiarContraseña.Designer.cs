namespace StageLink
{
    partial class CambiarContraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CambiarContraseña));
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLContraseñaConfirmacion = new System.Windows.Forms.Label();
            this.LBLContraseñaNueva = new System.Windows.Forms.Label();
            this.LBLContraseñaActual = new System.Windows.Forms.Label();
            this.TXTContraseñaActual = new System.Windows.Forms.TextBox();
            this.TXTContraseñaNueva = new System.Windows.Forms.TextBox();
            this.TXTContraseñaConfirmacion = new System.Windows.Forms.TextBox();
            this.BTNCambiarContraseña = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(129, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(181, 24);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Text = "Cambiar Contraseña";
            // 
            // LBLContraseñaConfirmacion
            // 
            this.LBLContraseñaConfirmacion.AutoSize = true;
            this.LBLContraseñaConfirmacion.Location = new System.Drawing.Point(211, 220);
            this.LBLContraseñaConfirmacion.Name = "LBLContraseñaConfirmacion";
            this.LBLContraseñaConfirmacion.Size = new System.Drawing.Size(68, 13);
            this.LBLContraseñaConfirmacion.TabIndex = 1;
            this.LBLContraseñaConfirmacion.Text = "Confirmacion";
            // 
            // LBLContraseñaNueva
            // 
            this.LBLContraseñaNueva.AutoSize = true;
            this.LBLContraseñaNueva.Location = new System.Drawing.Point(240, 162);
            this.LBLContraseñaNueva.Name = "LBLContraseñaNueva";
            this.LBLContraseñaNueva.Size = new System.Drawing.Size(39, 13);
            this.LBLContraseñaNueva.TabIndex = 2;
            this.LBLContraseñaNueva.Text = "Nueva";
            // 
            // LBLContraseñaActual
            // 
            this.LBLContraseñaActual.AutoSize = true;
            this.LBLContraseñaActual.Location = new System.Drawing.Point(242, 107);
            this.LBLContraseñaActual.Name = "LBLContraseñaActual";
            this.LBLContraseñaActual.Size = new System.Drawing.Size(37, 13);
            this.LBLContraseñaActual.TabIndex = 3;
            this.LBLContraseñaActual.Text = "Actual";
            // 
            // TXTContraseñaActual
            // 
            this.TXTContraseñaActual.Location = new System.Drawing.Point(179, 123);
            this.TXTContraseñaActual.Name = "TXTContraseñaActual";
            this.TXTContraseñaActual.Size = new System.Drawing.Size(100, 20);
            this.TXTContraseñaActual.TabIndex = 4;
            // 
            // TXTContraseñaNueva
            // 
            this.TXTContraseñaNueva.Location = new System.Drawing.Point(179, 178);
            this.TXTContraseñaNueva.Name = "TXTContraseñaNueva";
            this.TXTContraseñaNueva.Size = new System.Drawing.Size(100, 20);
            this.TXTContraseñaNueva.TabIndex = 5;
            // 
            // TXTContraseñaConfirmacion
            // 
            this.TXTContraseñaConfirmacion.Location = new System.Drawing.Point(179, 236);
            this.TXTContraseñaConfirmacion.Name = "TXTContraseñaConfirmacion";
            this.TXTContraseñaConfirmacion.Size = new System.Drawing.Size(100, 20);
            this.TXTContraseñaConfirmacion.TabIndex = 6;
            // 
            // BTNCambiarContraseña
            // 
            this.BTNCambiarContraseña.Location = new System.Drawing.Point(194, 415);
            this.BTNCambiarContraseña.Name = "BTNCambiarContraseña";
            this.BTNCambiarContraseña.Size = new System.Drawing.Size(116, 23);
            this.BTNCambiarContraseña.TabIndex = 7;
            this.BTNCambiarContraseña.Text = "Cambiar Contraseña";
            this.BTNCambiarContraseña.UseVisualStyleBackColor = true;
            this.BTNCambiarContraseña.Click += new System.EventHandler(this.BTNCambiarContraseña_Click_1);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(235, 386);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 8;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // CambiarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(322, 450);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNCambiarContraseña);
            this.Controls.Add(this.TXTContraseñaConfirmacion);
            this.Controls.Add(this.TXTContraseñaNueva);
            this.Controls.Add(this.TXTContraseñaActual);
            this.Controls.Add(this.LBLContraseñaActual);
            this.Controls.Add(this.LBLContraseñaNueva);
            this.Controls.Add(this.LBLContraseñaConfirmacion);
            this.Controls.Add(this.LBLTitulo);
            this.Name = "CambiarContraseña";
            this.Text = "CambiarContraseña";
            //((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            //this.ResumeLayout(false);
            //this.PerformLayout();

        }

        #endregion

        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLContraseñaConfirmacion;
        private System.Windows.Forms.Label LBLContraseñaNueva;
        private System.Windows.Forms.Label LBLContraseñaActual;
        private System.Windows.Forms.TextBox TXTContraseñaActual;
        private System.Windows.Forms.TextBox TXTContraseñaNueva;
        private System.Windows.Forms.TextBox TXTContraseñaConfirmacion;
        private System.Windows.Forms.Button BTNCambiarContraseña;
        private System.Windows.Forms.Button BTNCancelar;
    }
}