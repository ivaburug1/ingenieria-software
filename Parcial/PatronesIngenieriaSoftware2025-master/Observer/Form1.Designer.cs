namespace Observer
{
    partial class Form1
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
            this.lstNotificaciones = new System.Windows.Forms.ListBox();
            this.btnDesuscribir = new System.Windows.Forms.Button();
            this.btnSuscribir = new System.Windows.Forms.Button();
            this.lstUsuarios = new System.Windows.Forms.ListBox();
            this.lstProductos = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstNotificaciones
            // 
            this.lstNotificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstNotificaciones.FormattingEnabled = true;
            this.lstNotificaciones.ItemHeight = 16;
            this.lstNotificaciones.Location = new System.Drawing.Point(389, 6);
            this.lstNotificaciones.Name = "lstNotificaciones";
            this.lstNotificaciones.Size = new System.Drawing.Size(576, 388);
            this.lstNotificaciones.TabIndex = 10;
            // 
            // btnDesuscribir
            // 
            this.btnDesuscribir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesuscribir.Location = new System.Drawing.Point(667, 400);
            this.btnDesuscribir.Name = "btnDesuscribir";
            this.btnDesuscribir.Size = new System.Drawing.Size(198, 45);
            this.btnDesuscribir.TabIndex = 9;
            this.btnDesuscribir.Text = "Desuscribir";
            this.btnDesuscribir.UseVisualStyleBackColor = true;
            this.btnDesuscribir.Click += new System.EventHandler(this.btnDesuscribir_Click);
            // 
            // btnSuscribir
            // 
            this.btnSuscribir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuscribir.Location = new System.Drawing.Point(454, 400);
            this.btnSuscribir.Name = "btnSuscribir";
            this.btnSuscribir.Size = new System.Drawing.Size(198, 45);
            this.btnSuscribir.TabIndex = 8;
            this.btnSuscribir.Text = "Suscribir";
            this.btnSuscribir.UseVisualStyleBackColor = true;
            this.btnSuscribir.Click += new System.EventHandler(this.btnSuscribir_Click);
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstUsuarios.FormattingEnabled = true;
            this.lstUsuarios.ItemHeight = 16;
            this.lstUsuarios.Location = new System.Drawing.Point(197, 6);
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(173, 436);
            this.lstUsuarios.TabIndex = 7;
            this.lstUsuarios.SelectedValueChanged += new System.EventHandler(this.lstUsuarios_SelectedValueChanged);
            // 
            // lstProductos
            // 
            this.lstProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstProductos.FormattingEnabled = true;
            this.lstProductos.ItemHeight = 16;
            this.lstProductos.Location = new System.Drawing.Point(12, 6);
            this.lstProductos.Name = "lstProductos";
            this.lstProductos.Size = new System.Drawing.Size(168, 436);
            this.lstProductos.TabIndex = 6;
            this.lstProductos.SelectedValueChanged += new System.EventHandler(this.lstProductos_SelectedValueChanged);
            this.lstProductos.DoubleClick += new System.EventHandler(this.lstProductos_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 521);
            this.Controls.Add(this.lstNotificaciones);
            this.Controls.Add(this.btnDesuscribir);
            this.Controls.Add(this.btnSuscribir);
            this.Controls.Add(this.lstUsuarios);
            this.Controls.Add(this.lstProductos);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstNotificaciones;
        private System.Windows.Forms.Button btnDesuscribir;
        private System.Windows.Forms.Button btnSuscribir;
        private System.Windows.Forms.ListBox lstUsuarios;
        private System.Windows.Forms.ListBox lstProductos;
    }
}