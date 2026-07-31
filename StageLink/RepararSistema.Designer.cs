namespace StageLink
{
    partial class RepararSistema
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
            this.BTNRecalcularDV = new System.Windows.Forms.Button();
            this.BTNRestaurarBD = new System.Windows.Forms.Button();
            this.BTNDetectarCambios = new System.Windows.Forms.Button();
            this.LBListaDVForms = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // BTNRecalcularDV
            // 
            this.BTNRecalcularDV.Location = new System.Drawing.Point(1006, 12);
            this.BTNRecalcularDV.Name = "BTNRecalcularDV";
            this.BTNRecalcularDV.Size = new System.Drawing.Size(159, 54);
            this.BTNRecalcularDV.TabIndex = 0;
            this.BTNRecalcularDV.Tag = "BTNRecalcularDV";
            this.BTNRecalcularDV.Text = "Recalcular Digito Verificador";
            this.BTNRecalcularDV.UseVisualStyleBackColor = true;
            this.BTNRecalcularDV.Click += new System.EventHandler(this.BTNRecalcularDV_Click);
            // 
            // BTNRestaurarBD
            // 
            this.BTNRestaurarBD.Location = new System.Drawing.Point(1006, 72);
            this.BTNRestaurarBD.Name = "BTNRestaurarBD";
            this.BTNRestaurarBD.Size = new System.Drawing.Size(159, 54);
            this.BTNRestaurarBD.TabIndex = 1;
            this.BTNRestaurarBD.Tag = "BTNRestaurarBD";
            this.BTNRestaurarBD.Text = "Restaurar Base de Datos";
            this.BTNRestaurarBD.UseVisualStyleBackColor = true;
            this.BTNRestaurarBD.Click += new System.EventHandler(this.BTNRestaurarBD_Click);
            //
            // BTNDetectarCambios
            //
            this.BTNDetectarCambios.Location = new System.Drawing.Point(1006, 132);
            this.BTNDetectarCambios.Name = "BTNDetectarCambios";
            this.BTNDetectarCambios.Size = new System.Drawing.Size(159, 54);
            this.BTNDetectarCambios.TabIndex = 3;
            this.BTNDetectarCambios.Text = "Detectar Cambios";
            this.BTNDetectarCambios.UseVisualStyleBackColor = true;
            this.BTNDetectarCambios.Click += new System.EventHandler(this.BTNDetectarCambios_Click);
            //
            // LBListaDVForms
            //
            this.LBListaDVForms.FormattingEnabled = true;
            this.LBListaDVForms.ItemHeight = 20;
            this.LBListaDVForms.Location = new System.Drawing.Point(12, 12);
            this.LBListaDVForms.Name = "LBListaDVForms";
            this.LBListaDVForms.Size = new System.Drawing.Size(988, 264);
            this.LBListaDVForms.TabIndex = 2;
            this.LBListaDVForms.SelectedIndexChanged += new System.EventHandler(this.LBListaDVForms_SelectedIndexChanged);
            // 
            // RepararSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1177, 296);
            this.Controls.Add(this.LBListaDVForms);
            this.Controls.Add(this.BTNDetectarCambios);
            this.Controls.Add(this.BTNRestaurarBD);
            this.Controls.Add(this.BTNRecalcularDV);
            this.Name = "RepararSistema";
            this.Text = "RepararSistema";
            this.Load += new System.EventHandler(this.RepararSistema_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTNRecalcularDV;
        private System.Windows.Forms.Button BTNRestaurarBD;
        private System.Windows.Forms.Button BTNDetectarCambios;
        private System.Windows.Forms.ListBox LBListaDVForms;
    }
}