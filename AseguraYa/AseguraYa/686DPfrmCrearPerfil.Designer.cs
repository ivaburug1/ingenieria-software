namespace AseguraYa
{
    partial class _686DPfrmCrearPerfil
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.BTNCrearPerfil = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TV_Familia = new System.Windows.Forms.TreeView();
            this.LSTPermisos = new System.Windows.Forms.ListBox();
            this.TV_Perfiles = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.BTNAgregarFamilia = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.TV_PerfilPorCrear = new System.Windows.Forms.TreeView();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCrearFamilia = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 15;
            this.label3.Tag = "Familias";
            this.label3.Text = "Familias";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(845, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 14;
            this.label2.Tag = "Permisos";
            this.label2.Text = "Permisos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Tag = "Nombre";
            this.label1.Text = "Nombre";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(350, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 23);
            this.button2.TabIndex = 12;
            this.button2.Tag = "DesasignarFamilia";
            this.button2.Text = "Desasignar Familia";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BTNCrearPerfil
            // 
            this.BTNCrearPerfil.Location = new System.Drawing.Point(260, 27);
            this.BTNCrearPerfil.Name = "BTNCrearPerfil";
            this.BTNCrearPerfil.Size = new System.Drawing.Size(75, 23);
            this.BTNCrearPerfil.TabIndex = 11;
            this.BTNCrearPerfil.Tag = "CrearPerfil";
            this.BTNCrearPerfil.Text = "Crear Perfil";
            this.BTNCrearPerfil.UseVisualStyleBackColor = true;
            this.BTNCrearPerfil.Click += new System.EventHandler(this.BTNCrearPerfil_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(171, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
            // 
            // TV_Familia
            // 
            this.TV_Familia.Location = new System.Drawing.Point(648, 85);
            this.TV_Familia.Name = "TV_Familia";
            this.TV_Familia.Size = new System.Drawing.Size(179, 342);
            this.TV_Familia.TabIndex = 9;
            this.TV_Familia.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TV_Familia_AfterSelect);
            // 
            // LSTPermisos
            // 
            this.LSTPermisos.FormattingEnabled = true;
            this.LSTPermisos.Location = new System.Drawing.Point(848, 85);
            this.LSTPermisos.Name = "LSTPermisos";
            this.LSTPermisos.Size = new System.Drawing.Size(164, 342);
            this.LSTPermisos.TabIndex = 8;
            // 
            // TV_Perfiles
            // 
            this.TV_Perfiles.Location = new System.Drawing.Point(15, 85);
            this.TV_Perfiles.Name = "TV_Perfiles";
            this.TV_Perfiles.Size = new System.Drawing.Size(304, 342);
            this.TV_Perfiles.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 17;
            this.label4.Tag = "Perfiles";
            this.label4.Text = "Perfiles";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(473, 27);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 23);
            this.button3.TabIndex = 18;
            this.button3.Tag = "DesasignarPermiso";
            this.button3.Text = "Desasignar Permiso";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // BTNAgregarFamilia
            // 
            this.BTNAgregarFamilia.Location = new System.Drawing.Point(648, 433);
            this.BTNAgregarFamilia.Name = "BTNAgregarFamilia";
            this.BTNAgregarFamilia.Size = new System.Drawing.Size(125, 23);
            this.BTNAgregarFamilia.TabIndex = 19;
            this.BTNAgregarFamilia.Tag = "AgregarFamilia";
            this.BTNAgregarFamilia.Text = "Agregar Familia";
            this.BTNAgregarFamilia.UseVisualStyleBackColor = true;
            this.BTNAgregarFamilia.Click += new System.EventHandler(this.BTNAgregarFamilia_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(848, 433);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 20;
            this.button5.Tag = "AgregarPermiso";
            this.button5.Text = "Agregar Permiso";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // TV_PerfilPorCrear
            // 
            this.TV_PerfilPorCrear.Location = new System.Drawing.Point(325, 85);
            this.TV_PerfilPorCrear.Name = "TV_PerfilPorCrear";
            this.TV_PerfilPorCrear.Size = new System.Drawing.Size(304, 342);
            this.TV_PerfilPorCrear.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(322, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 22;
            this.label5.Tag = "PerfilC";
            this.label5.Text = "Perfil por crear ";
            // 
            // btnCrearFamilia
            // 
            this.btnCrearFamilia.Location = new System.Drawing.Point(895, 9);
            this.btnCrearFamilia.Name = "btnCrearFamilia";
            this.btnCrearFamilia.Size = new System.Drawing.Size(117, 23);
            this.btnCrearFamilia.TabIndex = 23;
            this.btnCrearFamilia.Tag = "CrearFamilia";
            this.btnCrearFamilia.Text = "Crear Familia";
            this.btnCrearFamilia.UseVisualStyleBackColor = true;
            this.btnCrearFamilia.Click += new System.EventHandler(this.btnCrearFamilia_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(615, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 24;
            this.button1.Tag = "Editar";
            this.button1.Text = "Editar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(192, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(62, 23);
            this.button4.TabIndex = 25;
            this.button4.Tag = "Cancelar";
            this.button4.Text = "Cancelar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // _686DPfrmCrearPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(1024, 482);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCrearFamilia);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TV_PerfilPorCrear);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.BTNAgregarFamilia);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TV_Perfiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BTNCrearPerfil);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TV_Familia);
            this.Controls.Add(this.LSTPermisos);
            this.Name = "_686DPfrmCrearPerfil";
            this.Tag = "_686DPfrmCrearPerfil";
            this.Text = "_686DPfrmCrearPerfil";
            this.Load += new System.EventHandler(this._686DPfrmCrearPerfil_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button BTNCrearPerfil;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TreeView TV_Familia;
        private System.Windows.Forms.ListBox LSTPermisos;
        private System.Windows.Forms.TreeView TV_Perfiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BTNAgregarFamilia;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TreeView TV_PerfilPorCrear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCrearFamilia;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
    }
}