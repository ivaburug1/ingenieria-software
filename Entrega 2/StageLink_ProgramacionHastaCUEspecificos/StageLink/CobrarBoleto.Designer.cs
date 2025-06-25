namespace StageLink
{
    partial class CobrarBoleto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CobrarBoleto));
            this.LVMostrarRecibo = new System.Windows.Forms.ListView();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLMedioDePago = new System.Windows.Forms.Label();
            this.LBLNumeroDeTajeta = new System.Windows.Forms.Label();
            this.LBLCantCuotas = new System.Windows.Forms.Label();
            this.TXTNumeroDeTarjeta = new System.Windows.Forms.TextBox();
            this.CBMedioDePago = new System.Windows.Forms.ComboBox();
            this.CBCantCuotas = new System.Windows.Forms.ComboBox();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNConfirmarCobro = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LVMostrarRecibo
            // 
            this.LVMostrarRecibo.HideSelection = false;
            this.LVMostrarRecibo.Location = new System.Drawing.Point(47, 42);
            this.LVMostrarRecibo.Name = "LVMostrarRecibo";
            this.LVMostrarRecibo.Size = new System.Drawing.Size(342, 334);
            this.LVMostrarRecibo.TabIndex = 0;
            this.LVMostrarRecibo.UseCompatibleStateImageBehavior = false;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(72)))), ((int)(((byte)(250)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(144, 25);
            this.LBLTitulo.TabIndex = 1;
            this.LBLTitulo.Text = "Cobrar Boleto";
            // 
            // LBLMedioDePago
            // 
            this.LBLMedioDePago.AutoSize = true;
            this.LBLMedioDePago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.LBLMedioDePago.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.LBLMedioDePago.Location = new System.Drawing.Point(44, 393);
            this.LBLMedioDePago.Name = "LBLMedioDePago";
            this.LBLMedioDePago.Size = new System.Drawing.Size(201, 17);
            this.LBLMedioDePago.TabIndex = 2;
            this.LBLMedioDePago.Text = "Seleccionar un Medio de Pago";
            // 
            // LBLNumeroDeTajeta
            // 
            this.LBLNumeroDeTajeta.AutoSize = true;
            this.LBLNumeroDeTajeta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.LBLNumeroDeTajeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.LBLNumeroDeTajeta.Location = new System.Drawing.Point(44, 438);
            this.LBLNumeroDeTajeta.Name = "LBLNumeroDeTajeta";
            this.LBLNumeroDeTajeta.Size = new System.Drawing.Size(455, 17);
            this.LBLNumeroDeTajeta.TabIndex = 3;
            this.LBLNumeroDeTajeta.Text = "Ingrese el numero de su Tarjeta en formato \"XXXX-XXXX-XXXX-XXXX\"";
            // 
            // LBLCantCuotas
            // 
            this.LBLCantCuotas.AutoSize = true;
            this.LBLCantCuotas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(254)))));
            this.LBLCantCuotas.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.LBLCantCuotas.Location = new System.Drawing.Point(44, 483);
            this.LBLCantCuotas.Name = "LBLCantCuotas";
            this.LBLCantCuotas.Size = new System.Drawing.Size(375, 17);
            this.LBLCantCuotas.TabIndex = 4;
            this.LBLCantCuotas.Text = "Ingrese la cantidad de cuotas en las que le gustaria pagar";
            // 
            // TXTNumeroDeTarjeta
            // 
            this.TXTNumeroDeTarjeta.Location = new System.Drawing.Point(47, 458);
            this.TXTNumeroDeTarjeta.Name = "TXTNumeroDeTarjeta";
            this.TXTNumeroDeTarjeta.Size = new System.Drawing.Size(198, 20);
            this.TXTNumeroDeTarjeta.TabIndex = 5;
            // 
            // CBMedioDePago
            // 
            this.CBMedioDePago.FormattingEnabled = true;
            this.CBMedioDePago.Items.AddRange(new object[] {
            "Mercado Pago",
            "Tarjeta Debito",
            "Tarjeta Credito",
            "Efectivo"});
            this.CBMedioDePago.Location = new System.Drawing.Point(47, 413);
            this.CBMedioDePago.Name = "CBMedioDePago";
            this.CBMedioDePago.Size = new System.Drawing.Size(121, 21);
            this.CBMedioDePago.TabIndex = 6;
            // 
            // CBCantCuotas
            // 
            this.CBCantCuotas.FormattingEnabled = true;
            this.CBCantCuotas.Items.AddRange(new object[] {
            "1",
            "3",
            "6",
            "9",
            "12"});
            this.CBCantCuotas.Location = new System.Drawing.Point(47, 503);
            this.CBCantCuotas.Name = "CBCantCuotas";
            this.CBCantCuotas.Size = new System.Drawing.Size(121, 21);
            this.CBCantCuotas.TabIndex = 7;
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(342, 575);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 8;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNConfirmarCobro
            // 
            this.BTNConfirmarCobro.Location = new System.Drawing.Point(423, 575);
            this.BTNConfirmarCobro.Name = "BTNConfirmarCobro";
            this.BTNConfirmarCobro.Size = new System.Drawing.Size(99, 23);
            this.BTNConfirmarCobro.TabIndex = 9;
            this.BTNConfirmarCobro.Text = "Confirmar Cobro";
            this.BTNConfirmarCobro.UseVisualStyleBackColor = true;
            this.BTNConfirmarCobro.Click += new System.EventHandler(this.BTNConfirmarCobro_Click);
            // 
            // CobrarBoleto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(534, 610);
            this.Controls.Add(this.BTNConfirmarCobro);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.CBCantCuotas);
            this.Controls.Add(this.CBMedioDePago);
            this.Controls.Add(this.TXTNumeroDeTarjeta);
            this.Controls.Add(this.LBLCantCuotas);
            this.Controls.Add(this.LBLNumeroDeTajeta);
            this.Controls.Add(this.LBLMedioDePago);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.LVMostrarRecibo);
            this.Name = "CobrarBoleto";
            this.Text = "CobrarBoleto";
            this.Load += new System.EventHandler(this.CobrarBoleto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView LVMostrarRecibo;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLMedioDePago;
        private System.Windows.Forms.Label LBLNumeroDeTajeta;
        private System.Windows.Forms.Label LBLCantCuotas;
        private System.Windows.Forms.TextBox TXTNumeroDeTarjeta;
        private System.Windows.Forms.ComboBox CBMedioDePago;
        private System.Windows.Forms.ComboBox CBCantCuotas;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNConfirmarCobro;
    }
}