namespace AseguraYa
{
    partial class _686DPfrmRegistrarCliente
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
            this.DP_TXTDNI = new System.Windows.Forms.TextBox();
            this.DP_TXTNombre = new System.Windows.Forms.TextBox();
            this.DP_TXTApellido = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DP_TXTDNI
            // 
            this.DP_TXTDNI.Location = new System.Drawing.Point(23, 34);
            this.DP_TXTDNI.Name = "DP_TXTDNI";
            this.DP_TXTDNI.ReadOnly = true;
            this.DP_TXTDNI.Size = new System.Drawing.Size(126, 20);
            this.DP_TXTDNI.TabIndex = 0;
            // 
            // DP_TXTNombre
            // 
            this.DP_TXTNombre.Location = new System.Drawing.Point(23, 83);
            this.DP_TXTNombre.Name = "DP_TXTNombre";
            this.DP_TXTNombre.Size = new System.Drawing.Size(126, 20);
            this.DP_TXTNombre.TabIndex = 1;
            // 
            // DP_TXTApellido
            // 
            this.DP_TXTApellido.Location = new System.Drawing.Point(23, 134);
            this.DP_TXTApellido.Name = "DP_TXTApellido";
            this.DP_TXTApellido.Size = new System.Drawing.Size(126, 20);
            this.DP_TXTApellido.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Tag = "DNI";
            this.label1.Text = "DNI";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Tag = "Nombre";
            this.label2.Text = "Nombre";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Tag = "Apellido";
            this.label3.Text = "Apellido";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 30);
            this.button1.TabIndex = 6;
            this.button1.Tag = "Registrar";
            this.button1.Text = "Registrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 30);
            this.button2.TabIndex = 7;
            this.button2.Tag = "Cancelar";
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // _686DPfrmRegistrarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(181, 281);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DP_TXTApellido);
            this.Controls.Add(this.DP_TXTNombre);
            this.Controls.Add(this.DP_TXTDNI);
            this.Name = "_686DPfrmRegistrarCliente";
            this.Tag = "_686DPfrmRegistrarCliente";
            this.Text = "_686DPfrmRegistrarCliente";
            this.Load += new System.EventHandler(this._686DPfrmRegistrarCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DP_TXTDNI;
        private System.Windows.Forms.TextBox DP_TXTNombre;
        private System.Windows.Forms.TextBox DP_TXTApellido;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}