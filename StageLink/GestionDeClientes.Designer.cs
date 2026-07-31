namespace StageLink
{
    partial class GestionDeClientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionDeClientes));
            this.TXTDNI = new System.Windows.Forms.TextBox();
            this.DGVMuestraClientes = new System.Windows.Forms.DataGridView();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLBuscar = new System.Windows.Forms.Label();
            this.LBLDNI = new System.Windows.Forms.Label();
            this.LBLApellido = new System.Windows.Forms.Label();
            this.LBLCorreo = new System.Windows.Forms.Label();
            this.TXTNombre = new System.Windows.Forms.TextBox();
            this.TXTApellido = new System.Windows.Forms.TextBox();
            this.TXTCorreo = new System.Windows.Forms.TextBox();
            this.BTNAplicar = new System.Windows.Forms.Button();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNBuscar = new System.Windows.Forms.Button();
            this.LBLNombre = new System.Windows.Forms.Label();
            this.DGVClientesSerializacion = new System.Windows.Forms.DataGridView();
            this.BTNDeserializar = new System.Windows.Forms.Button();
            this.BTNSerializar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientesSerializacion)).BeginInit();
            this.SuspendLayout();
            // 
            // TXTDNI
            // 
            this.TXTDNI.Location = new System.Drawing.Point(60, 634);
            this.TXTDNI.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TXTDNI.Name = "TXTDNI";
            this.TXTDNI.Size = new System.Drawing.Size(148, 26);
            this.TXTDNI.TabIndex = 0;
            // 
            // DGVMuestraClientes
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMuestraClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMuestraClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVMuestraClientes.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVMuestraClientes.Location = new System.Drawing.Point(56, 142);
            this.DGVMuestraClientes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGVMuestraClientes.Name = "DGVMuestraClientes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMuestraClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVMuestraClientes.Size = new System.Drawing.Size(715, 403);
            this.DGVMuestraClientes.TabIndex = 1;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.LBLTitulo.Location = new System.Drawing.Point(50, 54);
            this.LBLTitulo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(298, 39);
            this.LBLTitulo.TabIndex = 2;
            this.LBLTitulo.Tag = "LBLTitulo";
            this.LBLTitulo.Text = "Gestor de Clientes";
            // 
            // LBLBuscar
            // 
            this.LBLBuscar.AutoSize = true;
            this.LBLBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.LBLBuscar.Location = new System.Drawing.Point(51, 549);
            this.LBLBuscar.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLBuscar.Name = "LBLBuscar";
            this.LBLBuscar.Size = new System.Drawing.Size(99, 31);
            this.LBLBuscar.TabIndex = 3;
            this.LBLBuscar.Tag = "LBLBuscar";
            this.LBLBuscar.Text = "Buscar";
            // 
            // LBLDNI
            // 
            this.LBLDNI.AutoSize = true;
            this.LBLDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLDNI.Location = new System.Drawing.Point(56, 609);
            this.LBLDNI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLDNI.Name = "LBLDNI";
            this.LBLDNI.Size = new System.Drawing.Size(37, 20);
            this.LBLDNI.TabIndex = 4;
            this.LBLDNI.Tag = "LBLDNI";
            this.LBLDNI.Text = "DNI";
            // 
            // LBLApellido
            // 
            this.LBLApellido.AutoSize = true;
            this.LBLApellido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.LBLApellido.Location = new System.Drawing.Point(374, 609);
            this.LBLApellido.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLApellido.Name = "LBLApellido";
            this.LBLApellido.Size = new System.Drawing.Size(65, 20);
            this.LBLApellido.TabIndex = 6;
            this.LBLApellido.Tag = "LBLApellido";
            this.LBLApellido.Text = "Apellido";
            // 
            // LBLCorreo
            // 
            this.LBLCorreo.AutoSize = true;
            this.LBLCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.LBLCorreo.Location = new System.Drawing.Point(532, 609);
            this.LBLCorreo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLCorreo.Name = "LBLCorreo";
            this.LBLCorreo.Size = new System.Drawing.Size(51, 20);
            this.LBLCorreo.TabIndex = 7;
            this.LBLCorreo.Tag = "LBLCorreo";
            this.LBLCorreo.Text = "e-Mail";
            // 
            // TXTNombre
            // 
            this.TXTNombre.Location = new System.Drawing.Point(219, 634);
            this.TXTNombre.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TXTNombre.Name = "TXTNombre";
            this.TXTNombre.Size = new System.Drawing.Size(148, 26);
            this.TXTNombre.TabIndex = 8;
            // 
            // TXTApellido
            // 
            this.TXTApellido.Location = new System.Drawing.Point(378, 634);
            this.TXTApellido.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TXTApellido.Name = "TXTApellido";
            this.TXTApellido.Size = new System.Drawing.Size(148, 26);
            this.TXTApellido.TabIndex = 9;
            // 
            // TXTCorreo
            // 
            this.TXTCorreo.Location = new System.Drawing.Point(542, 634);
            this.TXTCorreo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TXTCorreo.Name = "TXTCorreo";
            this.TXTCorreo.Size = new System.Drawing.Size(148, 26);
            this.TXTCorreo.TabIndex = 10;
            // 
            // BTNAplicar
            // 
            this.BTNAplicar.Location = new System.Drawing.Point(807, 142);
            this.BTNAplicar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNAplicar.Name = "BTNAplicar";
            this.BTNAplicar.Size = new System.Drawing.Size(112, 35);
            this.BTNAplicar.TabIndex = 11;
            this.BTNAplicar.Tag = "BTNAplicar";
            this.BTNAplicar.Text = "Aplicar";
            this.BTNAplicar.UseVisualStyleBackColor = true;
            this.BTNAplicar.Click += new System.EventHandler(this.BTNAplicar_Click_1);
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(928, 142);
            this.BTNCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(112, 35);
            this.BTNCancelar.TabIndex = 12;
            this.BTNCancelar.Tag = "BTNCancelar";
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click_1);
            // 
            // BTNBuscar
            // 
            this.BTNBuscar.Location = new System.Drawing.Point(700, 634);
            this.BTNBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNBuscar.Name = "BTNBuscar";
            this.BTNBuscar.Size = new System.Drawing.Size(112, 35);
            this.BTNBuscar.TabIndex = 13;
            this.BTNBuscar.Tag = "BTNBuscar";
            this.BTNBuscar.Text = "Buscar";
            this.BTNBuscar.UseVisualStyleBackColor = true;
            this.BTNBuscar.Click += new System.EventHandler(this.BTNBuscar_Click_1);
            // 
            // LBLNombre
            // 
            this.LBLNombre.AutoSize = true;
            this.LBLNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(115)))), ((int)(((byte)(230)))));
            this.LBLNombre.Location = new System.Drawing.Point(214, 609);
            this.LBLNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBLNombre.Name = "LBLNombre";
            this.LBLNombre.Size = new System.Drawing.Size(65, 20);
            this.LBLNombre.TabIndex = 14;
            this.LBLNombre.Tag = "LBLNombre";
            this.LBLNombre.Text = "Nombre";
            // 
            // DGVClientesSerializacion
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientesSerializacion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVClientesSerializacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVClientesSerializacion.DefaultCellStyle = dataGridViewCellStyle5;
            this.DGVClientesSerializacion.Location = new System.Drawing.Point(778, 247);
            this.DGVClientesSerializacion.Name = "DGVClientesSerializacion";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientesSerializacion.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVClientesSerializacion.RowTemplate.Height = 28;
            this.DGVClientesSerializacion.Size = new System.Drawing.Size(350, 252);
            this.DGVClientesSerializacion.TabIndex = 15;
            // 
            // BTNDeserializar
            // 
            this.BTNDeserializar.Location = new System.Drawing.Point(928, 510);
            this.BTNDeserializar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNDeserializar.Name = "BTNDeserializar";
            this.BTNDeserializar.Size = new System.Drawing.Size(112, 35);
            this.BTNDeserializar.TabIndex = 17;
            this.BTNDeserializar.Tag = "BTNDeserializar";
            this.BTNDeserializar.Text = "Deserializar";
            this.BTNDeserializar.UseVisualStyleBackColor = true;
            this.BTNDeserializar.Click += new System.EventHandler(this.BTNDeserializar_Click);
            // 
            // BTNSerializar
            // 
            this.BTNSerializar.Location = new System.Drawing.Point(807, 510);
            this.BTNSerializar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BTNSerializar.Name = "BTNSerializar";
            this.BTNSerializar.Size = new System.Drawing.Size(112, 35);
            this.BTNSerializar.TabIndex = 16;
            this.BTNSerializar.Tag = "BTNSerializar";
            this.BTNSerializar.Text = "Serializar";
            this.BTNSerializar.UseVisualStyleBackColor = true;
            this.BTNSerializar.Click += new System.EventHandler(this.BTNSerializar_Click);
            // 
            // GestionDeClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1176, 720);
            this.Controls.Add(this.BTNDeserializar);
            this.Controls.Add(this.BTNSerializar);
            this.Controls.Add(this.DGVClientesSerializacion);
            this.Controls.Add(this.LBLNombre);
            this.Controls.Add(this.BTNBuscar);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.BTNAplicar);
            this.Controls.Add(this.TXTCorreo);
            this.Controls.Add(this.TXTApellido);
            this.Controls.Add(this.TXTNombre);
            this.Controls.Add(this.LBLCorreo);
            this.Controls.Add(this.LBLApellido);
            this.Controls.Add(this.LBLDNI);
            this.Controls.Add(this.LBLBuscar);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.DGVMuestraClientes);
            this.Controls.Add(this.TXTDNI);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "GestionDeClientes";
            this.Text = "GestionDeClientes";
            this.Load += new System.EventHandler(this.GestionDeClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMuestraClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientesSerializacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTDNI;
        private System.Windows.Forms.DataGridView DGVMuestraClientes;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLBuscar;
        private System.Windows.Forms.Label LBLDNI;
        private System.Windows.Forms.Label LBLApellido;
        private System.Windows.Forms.Label LBLCorreo;
        private System.Windows.Forms.TextBox TXTNombre;
        private System.Windows.Forms.TextBox TXTApellido;
        private System.Windows.Forms.TextBox TXTCorreo;
        private System.Windows.Forms.Button BTNAplicar;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNBuscar;
        private System.Windows.Forms.Label LBLNombre;
        private System.Windows.Forms.DataGridView DGVClientesSerializacion;
        private System.Windows.Forms.Button BTNDeserializar;
        private System.Windows.Forms.Button BTNSerializar;
    }
}