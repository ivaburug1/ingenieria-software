namespace StageLink
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.TXTContraseña = new System.Windows.Forms.TextBox();
            this.LBLogin = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLContraseña = new System.Windows.Forms.Label();
            this.BTNLogin = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNMostrarContraseña = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(39, 115);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(100, 20);
            this.TXTDNI.TabIndex = 0;
            // 
            // TXTContraseña
            // 
            this.TXTContraseña.Location = new System.Drawing.Point(39, 162);
            this.TXTContraseña.Name = "TXTContraseña";
            this.TXTContraseña.Size = new System.Drawing.Size(100, 20);
            this.TXTContraseña.TabIndex = 1;
            this.TXTContraseña.UseSystemPasswordChar = true;
            this.TXTContraseña.TextChanged += new System.EventHandler(this.TXTContraseña_TextChanged);
            // 
            // LBLogin
            // 
            this.LBLogin.AutoSize = true;
            this.LBLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(79)))), ((int)(((byte)(211)))));
            this.LBLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLogin.Location = new System.Drawing.Point(138, 9);
            this.LBLogin.Name = "LBLogin";
            this.LBLogin.Size = new System.Drawing.Size(65, 25);
            this.LBLogin.TabIndex = 2;
            this.LBLogin.Text = "Login";
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(79)))), ((int)(((byte)(211)))));
            this.LBLDNI.Location = new System.Drawing.Point(36, 99);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(26, 13);
            this.LBLDNI.TabIndex = 3;
            this.LBLDNI.Text = "DNI";
            // 
            // LBLContraseña
            // 
            this.LBLContraseña.AutoSize = true;
            this.LBLContraseña.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(79)))), ((int)(((byte)(211)))));
            this.LBLContraseña.Location = new System.Drawing.Point(36, 146);
            this.LBLContraseña.Name = "LBLContraseña";
            this.LBLContraseña.Size = new System.Drawing.Size(61, 13);
            this.LBLContraseña.TabIndex = 4;
            this.LBLContraseña.Text = "Contraseña";
            // 
            // BTNLogin
            // 
            this.BTNLogin.Location = new System.Drawing.Point(267, 484);
            this.BTNLogin.Name = "BTNLogin";
            this.BTNLogin.Size = new System.Drawing.Size(75, 23);
            this.BTNLogin.TabIndex = 5;
            this.BTNLogin.Text = "Login";
            this.BTNLogin.UseVisualStyleBackColor = true;
            this.BTNLogin.Click += new System.EventHandler(this.BTNLogin_Click);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(267, 520);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 6;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNMostrarContraseña
            // 
            this.BTNMostrarContraseña.Location = new System.Drawing.Point(39, 188);
            this.BTNMostrarContraseña.Name = "BTNMostrarContraseña";
            this.BTNMostrarContraseña.Size = new System.Drawing.Size(100, 35);
            this.BTNMostrarContraseña.TabIndex = 7;
            this.BTNMostrarContraseña.Text = "Mostrar Contraseña";
            this.BTNMostrarContraseña.UseVisualStyleBackColor = true;
            this.BTNMostrarContraseña.Click += new System.EventHandler(this.BTNMostrarContraseña_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(354, 555);
            this.Controls.Add(this.BTNMostrarContraseña);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNLogin);
            this.Controls.Add(this.LBLContraseña);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLogin);
            this.Controls.Add(this.TXTContraseña);
            this.Controls.Add(this.TXTDNI);
            this.Name = "Login";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.TextBox TXTContraseña;
        private System.Windows.Forms.Label LBLogin;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLContraseña;
        private System.Windows.Forms.Button BTNLogin;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNMostrarContraseña;
    }
}