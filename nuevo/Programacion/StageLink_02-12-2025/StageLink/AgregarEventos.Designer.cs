namespace StageLink
{
    partial class AgregarEventos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarEventos));
            this.DTPFechaEvento = new System.Windows.Forms.DateTimePicker();
            this.TXTNombreDelArtista = new System.Windows.Forms.TextBox();
            this.CBSeleccionEsatio = new System.Windows.Forms.ComboBox();
            this.LBLTitulo = new System.Windows.Forms.Label();
            this.LBLFecha = new System.Windows.Forms.Label();
            this.LBLArtista = new System.Windows.Forms.Label();
            this.LBLEstadio = new System.Windows.Forms.Label();
            this.BTNCancelar = new System.Windows.Forms.Button();
            this.BTNConfirmar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DTPFechaEvento
            // 
            this.DTPFechaEvento.Location = new System.Drawing.Point(12, 101);
            this.DTPFechaEvento.Name = "DTPFechaEvento";
            this.DTPFechaEvento.Size = new System.Drawing.Size(197, 20);
            this.DTPFechaEvento.TabIndex = 0;
            // 
            // TXTNombreDelArtista
            // 
            this.TXTNombreDelArtista.Location = new System.Drawing.Point(12, 157);
            this.TXTNombreDelArtista.Name = "TXTNombreDelArtista";
            this.TXTNombreDelArtista.Size = new System.Drawing.Size(241, 20);
            this.TXTNombreDelArtista.TabIndex = 1;
            // 
            // CBSeleccionEsatio
            // 
            this.CBSeleccionEsatio.FormattingEnabled = true;
            this.CBSeleccionEsatio.Items.AddRange(new object[] {
            "Monumental River Plate",
            "Complejo Art Media",
            "Estadio Huracan"});
            this.CBSeleccionEsatio.Location = new System.Drawing.Point(12, 210);
            this.CBSeleccionEsatio.Name = "CBSeleccionEsatio";
            this.CBSeleccionEsatio.Size = new System.Drawing.Size(281, 21);
            this.CBSeleccionEsatio.TabIndex = 2;
            // 
            // LBLTitulo
            // 
            this.LBLTitulo.AutoSize = true;
            this.LBLTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.LBLTitulo.Location = new System.Drawing.Point(12, 9);
            this.LBLTitulo.Name = "LBLTitulo";
            this.LBLTitulo.Size = new System.Drawing.Size(172, 25);
            this.LBLTitulo.TabIndex = 3;
            this.LBLTitulo.Text = "Agregar Eventos";
            // 
            // LBLFecha
            // 
            this.LBLFecha.AutoSize = true;
            this.LBLFecha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLFecha.Location = new System.Drawing.Point(9, 85);
            this.LBLFecha.Name = "LBLFecha";
            this.LBLFecha.Size = new System.Drawing.Size(186, 13);
            this.LBLFecha.TabIndex = 4;
            this.LBLFecha.Text = "Fecha en la que transcurrira el evento";
            // 
            // LBLArtista
            // 
            this.LBLArtista.AutoSize = true;
            this.LBLArtista.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLArtista.Location = new System.Drawing.Point(9, 141);
            this.LBLArtista.Name = "LBLArtista";
            this.LBLArtista.Size = new System.Drawing.Size(244, 13);
            this.LBLArtista.TabIndex = 5;
            this.LBLArtista.Text = "Agregar el nombre del artista que va a presentarse";
            // 
            // LBLEstadio
            // 
            this.LBLEstadio.AutoSize = true;
            this.LBLEstadio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.LBLEstadio.Location = new System.Drawing.Point(9, 194);
            this.LBLEstadio.Name = "LBLEstadio";
            this.LBLEstadio.Size = new System.Drawing.Size(284, 13);
            this.LBLEstadio.TabIndex = 6;
            this.LBLEstadio.Text = "Seleccione el estadio en el cual se va a presentar el artista";
            // 
            // BTNCancelar
            // 
            this.BTNCancelar.Location = new System.Drawing.Point(616, 415);
            this.BTNCancelar.Name = "BTNCancelar";
            this.BTNCancelar.Size = new System.Drawing.Size(75, 23);
            this.BTNCancelar.TabIndex = 7;
            this.BTNCancelar.Text = "Cancelar";
            this.BTNCancelar.UseVisualStyleBackColor = true;
            this.BTNCancelar.Click += new System.EventHandler(this.BTNCancelar_Click);
            // 
            // BTNConfirmar
            // 
            this.BTNConfirmar.Location = new System.Drawing.Point(697, 415);
            this.BTNConfirmar.Name = "BTNConfirmar";
            this.BTNConfirmar.Size = new System.Drawing.Size(91, 23);
            this.BTNConfirmar.TabIndex = 8;
            this.BTNConfirmar.Text = "Agregar Evento";
            this.BTNConfirmar.UseVisualStyleBackColor = true;
            this.BTNConfirmar.Click += new System.EventHandler(this.BTNConfirmar_Click);
            // 
            // AgregarEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BTNConfirmar);
            this.Controls.Add(this.BTNCancelar);
            this.Controls.Add(this.LBLEstadio);
            this.Controls.Add(this.LBLArtista);
            this.Controls.Add(this.LBLFecha);
            this.Controls.Add(this.LBLTitulo);
            this.Controls.Add(this.CBSeleccionEsatio);
            this.Controls.Add(this.TXTNombreDelArtista);
            this.Controls.Add(this.DTPFechaEvento);
            this.Name = "AgregarEventos";
            this.Text = "AgregarEventos";
            this.Load += new System.EventHandler(this.AgregarEventos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DTPFechaEvento;
        private System.Windows.Forms.TextBox TXTNombreDelArtista;
        private System.Windows.Forms.ComboBox CBSeleccionEsatio;
        private System.Windows.Forms.Label LBLTitulo;
        private System.Windows.Forms.Label LBLFecha;
        private System.Windows.Forms.Label LBLArtista;
        private System.Windows.Forms.Label LBLEstadio;
        private System.Windows.Forms.Button BTNCancelar;
        private System.Windows.Forms.Button BTNConfirmar;
    }
}