namespace AseguraYa
{
    partial class _686DPfrmFamilias
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
            this.LSTPermisos = new System.Windows.Forms.ListBox();
            this.TV_Familia = new System.Windows.Forms.TreeView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TV_FamiliaEnCreacion = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.AgregarFamilia = new System.Windows.Forms.Button();
            this.AgregarPermiso = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LSTPermisos
            // 
            this.LSTPermisos.FormattingEnabled = true;
            this.LSTPermisos.Location = new System.Drawing.Point(598, 85);
            this.LSTPermisos.Name = "LSTPermisos";
            this.LSTPermisos.Size = new System.Drawing.Size(164, 342);
            this.LSTPermisos.TabIndex = 0;
            // 
            // TV_Familia
            // 
            this.TV_Familia.Location = new System.Drawing.Point(398, 85);
            this.TV_Familia.Name = "TV_Familia";
            this.TV_Familia.Size = new System.Drawing.Size(179, 342);
            this.TV_Familia.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Tag = "Aceptar";
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(301, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Tag = "cancelar";
            this.button2.Text = "cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 5;
            this.label1.Tag = "Nombre";
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(595, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Tag = "Permisos";
            this.label2.Text = "Permisos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 7;
            this.label3.Tag = "Familias";
            this.label3.Text = "Familias";
            // 
            // TV_FamiliaEnCreacion
            // 
            this.TV_FamiliaEnCreacion.Location = new System.Drawing.Point(15, 85);
            this.TV_FamiliaEnCreacion.Name = "TV_FamiliaEnCreacion";
            this.TV_FamiliaEnCreacion.Size = new System.Drawing.Size(361, 342);
            this.TV_FamiliaEnCreacion.TabIndex = 8;
            this.TV_FamiliaEnCreacion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_FamiliaEnCreacion_AfterSelect);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 9;
            this.label4.Tag = "FamiliaC";
            this.label4.Text = "Familia en creacion";
            // 
            // AgregarFamilia
            // 
            this.AgregarFamilia.Location = new System.Drawing.Point(398, 433);
            this.AgregarFamilia.Name = "AgregarFamilia";
            this.AgregarFamilia.Size = new System.Drawing.Size(127, 23);
            this.AgregarFamilia.TabIndex = 10;
            this.AgregarFamilia.Tag = "AFamilia";
            this.AgregarFamilia.Text = "Agregar Familia";
            this.AgregarFamilia.UseVisualStyleBackColor = true;
            this.AgregarFamilia.Click += new System.EventHandler(this.AgregarFamilia_Click);
            // 
            // AgregarPermiso
            // 
            this.AgregarPermiso.Location = new System.Drawing.Point(598, 433);
            this.AgregarPermiso.Name = "AgregarPermiso";
            this.AgregarPermiso.Size = new System.Drawing.Size(112, 23);
            this.AgregarPermiso.TabIndex = 11;
            this.AgregarPermiso.Tag = "APermiso";
            this.AgregarPermiso.Text = "Agregar permiso";
            this.AgregarPermiso.UseVisualStyleBackColor = true;
            this.AgregarPermiso.Click += new System.EventHandler(this.AgregarPermiso_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(398, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 23);
            this.button3.TabIndex = 19;
            this.button3.Tag = "DesasignarPermiso";
            this.button3.Text = "Desasignar Permiso";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(660, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 23);
            this.button4.TabIndex = 20;
            this.button4.Tag = "Editar";
            this.button4.Text = "Editar Familia";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(527, 27);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(117, 23);
            this.button5.TabIndex = 21;
            this.button5.Tag = "DesasignarFamilia";
            this.button5.Text = "Desasignar familia";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // _686DPfrmFamilias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 465);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.AgregarPermiso);
            this.Controls.Add(this.AgregarFamilia);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TV_FamiliaEnCreacion);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TV_Familia);
            this.Controls.Add(this.LSTPermisos);
            this.Name = "_686DPfrmFamilias";
            this.Tag = "_686DPfrmFamilias";
            this.Text = "_686DPfrmFamilias";
            this.Load += new System.EventHandler(this._686DPfrmFamilias_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LSTPermisos;
        private System.Windows.Forms.TreeView TV_Familia;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView TV_FamiliaEnCreacion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button AgregarFamilia;
        private System.Windows.Forms.Button AgregarPermiso;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}