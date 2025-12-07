namespace AseguraYa
{
    partial class _686DPfrmPagar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_686DPfrmPagar));
            this.txtCodSiniestro = new System.Windows.Forms.TextBox();
            this.txtNroPoliza = new System.Windows.Forms.TextBox();
            this.txtValor = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtEvaluacion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BTNPagar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCodSiniestro
            // 
            this.txtCodSiniestro.Enabled = false;
            this.txtCodSiniestro.Location = new System.Drawing.Point(168, 221);
            this.txtCodSiniestro.Name = "txtCodSiniestro";
            this.txtCodSiniestro.ReadOnly = true;
            this.txtCodSiniestro.Size = new System.Drawing.Size(197, 20);
            this.txtCodSiniestro.TabIndex = 0;
            // 
            // txtNroPoliza
            // 
            this.txtNroPoliza.Enabled = false;
            this.txtNroPoliza.Location = new System.Drawing.Point(168, 259);
            this.txtNroPoliza.Name = "txtNroPoliza";
            this.txtNroPoliza.ReadOnly = true;
            this.txtNroPoliza.Size = new System.Drawing.Size(197, 20);
            this.txtNroPoliza.TabIndex = 1;
            // 
            // txtValor
            // 
            this.txtValor.Enabled = false;
            this.txtValor.Location = new System.Drawing.Point(168, 333);
            this.txtValor.Name = "txtValor";
            this.txtValor.ReadOnly = true;
            this.txtValor.Size = new System.Drawing.Size(197, 20);
            this.txtValor.TabIndex = 3;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Enabled = false;
            this.txtDescripcion.Location = new System.Drawing.Point(168, 295);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ReadOnly = true;
            this.txtDescripcion.Size = new System.Drawing.Size(197, 20);
            this.txtDescripcion.TabIndex = 2;
            // 
            // txtEvaluacion
            // 
            this.txtEvaluacion.Enabled = false;
            this.txtEvaluacion.Location = new System.Drawing.Point(168, 369);
            this.txtEvaluacion.Name = "txtEvaluacion";
            this.txtEvaluacion.ReadOnly = true;
            this.txtEvaluacion.Size = new System.Drawing.Size(197, 20);
            this.txtEvaluacion.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.label1.Location = new System.Drawing.Point(12, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Tag = "CosSiniestro";
            this.label1.Text = "Codigo Siniestro";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.label2.Location = new System.Drawing.Point(12, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 6;
            this.label2.Tag = "Npoliza";
            this.label2.Text = "Numero de poliza";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.label3.Location = new System.Drawing.Point(12, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 7;
            this.label3.Tag = "Descripcion";
            this.label3.Text = "Descripcion";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.label4.Location = new System.Drawing.Point(12, 333);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 8;
            this.label4.Tag = "Monto";
            this.label4.Text = "Monto";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.label5.Location = new System.Drawing.Point(12, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 9;
            this.label5.Tag = "Evaluacion";
            this.label5.Text = "Evaluacion";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // BTNPagar
            // 
            this.BTNPagar.Location = new System.Drawing.Point(15, 419);
            this.BTNPagar.Name = "BTNPagar";
            this.BTNPagar.Size = new System.Drawing.Size(350, 33);
            this.BTNPagar.TabIndex = 10;
            this.BTNPagar.Text = "Pagar";
            this.BTNPagar.UseVisualStyleBackColor = true;
            this.BTNPagar.Click += new System.EventHandler(this.BTNPagar_Click);
            // 
            // _686DPfrmPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(377, 480);
            this.Controls.Add(this.BTNPagar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEvaluacion);
            this.Controls.Add(this.txtValor);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtNroPoliza);
            this.Controls.Add(this.txtCodSiniestro);
            this.Name = "_686DPfrmPagar";
            this.Text = "_686DPfrmPagar";
            this.Load += new System.EventHandler(this._686DPfrmPagar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodSiniestro;
        private System.Windows.Forms.TextBox txtNroPoliza;
        private System.Windows.Forms.TextBox txtValor;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtEvaluacion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BTNPagar;
    }
}