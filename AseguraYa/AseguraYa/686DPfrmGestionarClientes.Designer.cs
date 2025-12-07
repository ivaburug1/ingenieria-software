namespace AseguraYa
{
    partial class _686DPfrmGestionarClientes
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DP_TXTApellido = new System.Windows.Forms.TextBox();
            this.DP_TXTNombre = new System.Windows.Forms.TextBox();
            this.DP_TXTDNI = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.Label();
            this.TXTCodigoPostal = new System.Windows.Forms.TextBox();
            this.TXTDomicilio = new System.Windows.Forms.TextBox();
            this.TXTEmail = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(194, 70);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1109, 311);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(136, 39);
            this.button1.TabIndex = 1;
            this.button1.Tag = "Registrar";
            this.button1.Text = "Registrar Cliente";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 39);
            this.button2.TabIndex = 2;
            this.button2.Tag = "Modificar";
            this.button2.Text = "Modificar Ciente";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 297);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 11;
            this.label3.Tag = "Apellido";
            this.label3.Text = "Apellido";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 10;
            this.label2.Tag = "Nombre";
            this.label2.Text = "Nombre";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 9;
            this.label1.Tag = "DNI";
            this.label1.Text = "DNI";
            // 
            // DP_TXTApellido
            // 
            this.DP_TXTApellido.Location = new System.Drawing.Point(12, 313);
            this.DP_TXTApellido.Name = "DP_TXTApellido";
            this.DP_TXTApellido.Size = new System.Drawing.Size(136, 20);
            this.DP_TXTApellido.TabIndex = 8;
            this.DP_TXTApellido.TextChanged += new System.EventHandler(this.DP_TXTApellido_TextChanged);
            // 
            // DP_TXTNombre
            // 
            this.DP_TXTNombre.Location = new System.Drawing.Point(12, 262);
            this.DP_TXTNombre.Name = "DP_TXTNombre";
            this.DP_TXTNombre.Size = new System.Drawing.Size(136, 20);
            this.DP_TXTNombre.TabIndex = 7;
            this.DP_TXTNombre.TextChanged += new System.EventHandler(this.DP_TXTNombre_TextChanged);
            // 
            // DP_TXTDNI
            // 
            this.DP_TXTDNI.Location = new System.Drawing.Point(12, 213);
            this.DP_TXTDNI.Name = "DP_TXTDNI";
            this.DP_TXTDNI.Size = new System.Drawing.Size(136, 20);
            this.DP_TXTDNI.TabIndex = 6;
            this.DP_TXTDNI.TextChanged += new System.EventHandler(this.DP_TXTDNI_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 155);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(136, 39);
            this.button3.TabIndex = 12;
            this.button3.Tag = "Eliminar";
            this.button3.Text = "Eliminar Cliente";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(15, 487);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(136, 39);
            this.button4.TabIndex = 13;
            this.button4.Tag = "Aplicar";
            this.button4.Text = "Aplicar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(15, 551);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(136, 39);
            this.button5.TabIndex = 14;
            this.button5.Tag = "Mostrar";
            this.button5.Text = "Mostrar Mails";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 423);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 20;
            this.label4.Tag = "CodigoPostal";
            this.label4.Text = "Codigo Postal";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 384);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 19;
            this.label5.Tag = "Domicilio";
            this.label5.Text = "Domicilio";
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(12, 345);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(32, 13);
            this.Email.TabIndex = 18;
            this.Email.Tag = "Email";
            this.Email.Text = "Email";
            // 
            // TXTCodigoPostal
            // 
            this.TXTCodigoPostal.Location = new System.Drawing.Point(12, 439);
            this.TXTCodigoPostal.Name = "TXTCodigoPostal";
            this.TXTCodigoPostal.Size = new System.Drawing.Size(136, 20);
            this.TXTCodigoPostal.TabIndex = 17;
            this.TXTCodigoPostal.TextChanged += new System.EventHandler(this.TXTCodigoPostal_TextChanged);
            // 
            // TXTDomicilio
            // 
            this.TXTDomicilio.Location = new System.Drawing.Point(12, 399);
            this.TXTDomicilio.Name = "TXTDomicilio";
            this.TXTDomicilio.Size = new System.Drawing.Size(136, 20);
            this.TXTDomicilio.TabIndex = 16;
            this.TXTDomicilio.TextChanged += new System.EventHandler(this.TXTDomicilio_TextChanged);
            // 
            // TXTEmail
            // 
            this.TXTEmail.Location = new System.Drawing.Point(12, 361);
            this.TXTEmail.Name = "TXTEmail";
            this.TXTEmail.Size = new System.Drawing.Size(136, 20);
            this.TXTEmail.TabIndex = 15;
            this.TXTEmail.TextChanged += new System.EventHandler(this.TXTEmail_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(194, 33);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(275, 20);
            this.textBox1.TabIndex = 21;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(194, 389);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(967, 235);
            this.dataGridView2.TabIndex = 22;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1167, 397);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(136, 39);
            this.button6.TabIndex = 23;
            this.button6.Tag = "Serializar";
            this.button6.Text = "Serializar";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1170, 455);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(136, 39);
            this.button7.TabIndex = 24;
            this.button7.Tag = "Deserializar";
            this.button7.Text = "Deserializar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // _686DPfrmGestionarClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(1318, 636);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.TXTCodigoPostal);
            this.Controls.Add(this.TXTDomicilio);
            this.Controls.Add(this.TXTEmail);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DP_TXTApellido);
            this.Controls.Add(this.DP_TXTNombre);
            this.Controls.Add(this.DP_TXTDNI);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "_686DPfrmGestionarClientes";
            this.Tag = "_686DPfrmGestionarClientes";
            this.Text = "_686DPfrmGestionarClientes";
            this.Load += new System.EventHandler(this._686DPfrmGestionarClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DP_TXTApellido;
        private System.Windows.Forms.TextBox DP_TXTNombre;
        private System.Windows.Forms.TextBox DP_TXTDNI;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.TextBox TXTCodigoPostal;
        private System.Windows.Forms.TextBox TXTDomicilio;
        private System.Windows.Forms.TextBox TXTEmail;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}