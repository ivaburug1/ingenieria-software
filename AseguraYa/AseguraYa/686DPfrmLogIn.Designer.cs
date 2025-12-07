namespace AseguraYa
{
    partial class _686DPfrmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_686DPfrmLogIn));
            this.DP_TXTUsuario = new System.Windows.Forms.TextBox();
            this.DP_TXTContraseña = new System.Windows.Forms.TextBox();
            this.DP_BTNIniciarSesion = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DP_TXTUsuario
            // 
            this.DP_TXTUsuario.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.DP_TXTUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DP_TXTUsuario.Location = new System.Drawing.Point(70, 257);
            this.DP_TXTUsuario.Name = "DP_TXTUsuario";
            this.DP_TXTUsuario.Size = new System.Drawing.Size(220, 22);
            this.DP_TXTUsuario.TabIndex = 0;
            this.DP_TXTUsuario.TextChanged += new System.EventHandler(this.DP_TXTUsuario_TextChanged);
            // 
            // DP_TXTContraseña
            // 
            this.DP_TXTContraseña.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.DP_TXTContraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DP_TXTContraseña.Location = new System.Drawing.Point(70, 336);
            this.DP_TXTContraseña.Name = "DP_TXTContraseña";
            this.DP_TXTContraseña.PasswordChar = '*';
            this.DP_TXTContraseña.Size = new System.Drawing.Size(220, 22);
            this.DP_TXTContraseña.TabIndex = 1;
            this.DP_TXTContraseña.TextChanged += new System.EventHandler(this.DP_TXTContraseña_TextChanged);
            // 
            // DP_BTNIniciarSesion
            // 
            this.DP_BTNIniciarSesion.BackColor = System.Drawing.Color.DarkSlateGray;
            this.DP_BTNIniciarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DP_BTNIniciarSesion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DP_BTNIniciarSesion.Location = new System.Drawing.Point(70, 397);
            this.DP_BTNIniciarSesion.Name = "DP_BTNIniciarSesion";
            this.DP_BTNIniciarSesion.Size = new System.Drawing.Size(220, 32);
            this.DP_BTNIniciarSesion.TabIndex = 2;
            this.DP_BTNIniciarSesion.Tag = "IniciarSesion";
            this.DP_BTNIniciarSesion.Text = "Iniciar Sesion";
            this.DP_BTNIniciarSesion.UseVisualStyleBackColor = false;
            this.DP_BTNIniciarSesion.Click += new System.EventHandler(this.DP_BTNIniciarSesion_Click);
            this.DP_BTNIniciarSesion.MouseLeave += new System.EventHandler(this.DP_BTNIniciarSesion_MouseLeave);
            this.DP_BTNIniciarSesion.MouseHover += new System.EventHandler(this.DP_BTNIniciarSesion_MouseHover);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(318, 331);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 32);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Contraseña";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.PaleTurquoise;
            this.button3.Location = new System.Drawing.Point(276, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 27);
            this.button3.TabIndex = 7;
            this.button3.Tag = "Ayuda";
            this.button3.Text = "Ayuda";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // _686DPfrmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(363, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DP_BTNIniciarSesion);
            this.Controls.Add(this.DP_TXTContraseña);
            this.Controls.Add(this.DP_TXTUsuario);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_686DPfrmLogIn";
            this.Tag = "Iniciar Sesion";
            this.Text = "Iniciar Sesion";
            this.Load += new System.EventHandler(this._686DPfrmLogIn_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DP_TXTUsuario;
        private System.Windows.Forms.TextBox DP_TXTContraseña;
        private System.Windows.Forms.Button DP_BTNIniciarSesion;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
    }
}