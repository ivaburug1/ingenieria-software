namespace StageLink
{
    partial class CambiarIdioma
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
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLSeleccionarIdioma = new System.Windows.Forms.Label();
            this.CBSeleccionIdiomas = new System.Windows.Forms.ComboBox();
            this.BTNCambiarIdioma = new System.Windows.Forms.Button();
            this.BTNSalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.125F);
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(161, 25);
            this.LBLTitulo.TabIndex = 0;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Cambiar Idioma";
            // 
            // LBLSeleccionarIdioma
            // 
            this.LBLSeleccionarIdioma.AutoSize = true;
            this.LBLSeleccionarIdioma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F);
            this.LBLSeleccionarIdioma.Location = new System.Drawing.Point(14, 41);
            this.LBLSeleccionarIdioma.Name = "LBLSeleccionarIdioma";
            this.LBLSeleccionarIdioma.Size = new System.Drawing.Size(139, 17);
            this.LBLSeleccionarIdioma.TabIndex = 2;
            this.LBLSeleccionarIdioma.Tag = "LBLSeleccionarIdioma";
            this.LBLSeleccionarIdioma.Text = "Sleccionar un idioma";
            // 
            // CBSeleccionIdiomas
            // 
            this.CBSeleccionIdiomas.FormattingEnabled = true;
            this.CBSeleccionIdiomas.Location = new System.Drawing.Point(17, 61);
            this.CBSeleccionIdiomas.Name = "CBSeleccionIdiomas";
            this.CBSeleccionIdiomas.Size = new System.Drawing.Size(146, 21);
            this.CBSeleccionIdiomas.TabIndex = 3;
            // 
            // BTNCambiarIdioma
            // 
            this.BTNCambiarIdioma.Location = new System.Drawing.Point(169, 59);
            this.BTNCambiarIdioma.Name = "BTNCambiarIdioma";
            this.BTNCambiarIdioma.Size = new System.Drawing.Size(146, 23);
            this.BTNCambiarIdioma.TabIndex = 4;
            this.BTNCambiarIdioma.Tag = "BTNCambiarIdioma";
            this.BTNCambiarIdioma.Text = "Cambiar Idioma";
            this.BTNCambiarIdioma.UseVisualStyleBackColor = true;
            this.BTNCambiarIdioma.Click += new System.EventHandler(this.BTNCambiarIdioma_Click);
            // 
            // BTNSalir
            // 
            this.BTNSalir.Location = new System.Drawing.Point(169, 88);
            this.BTNSalir.Name = "BTNSalir";
            this.BTNSalir.Size = new System.Drawing.Size(146, 23);
            this.BTNSalir.TabIndex = 5;
            this.BTNSalir.Tag = "BTNSalir";
            this.BTNSalir.Text = "Salir";
            this.BTNSalir.UseVisualStyleBackColor = true;
            this.BTNSalir.Click += new System.EventHandler(this.BTNSalir_Click);
            // 
            // CambiarIdioma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(324, 120);
            this.Controls.Add(this.BTNSalir);
            this.Controls.Add(this.BTNCambiarIdioma);
            this.Controls.Add(this.CBSeleccionIdiomas);
            this.Controls.Add(this.LBLSeleccionarIdioma);
            this.Controls.Add(this.LBLTitulo);
            this.Name = "CambiarIdioma";
            this.Text = " ";
            this.Load += new System.EventHandler(this.CambiarIdioma_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLSeleccionarIdioma;
        private System.Windows.Forms.ComboBox CBSeleccionIdiomas;
        private System.Windows.Forms.Button BTNCambiarIdioma;
        private System.Windows.Forms.Button BTNSalir;
    }
}