namespace AseguraYa
{
    partial class _686DPfrmAuditarSiniestro
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
            this.BTNAprobar = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1306, 310);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // BTNAprobar
            // 
            this.BTNAprobar.BackColor = System.Drawing.SystemColors.Control;
            this.BTNAprobar.Location = new System.Drawing.Point(1169, 356);
            this.BTNAprobar.Name = "BTNAprobar";
            this.BTNAprobar.Size = new System.Drawing.Size(149, 23);
            this.BTNAprobar.TabIndex = 6;
            this.BTNAprobar.Tag = "BTNAprobar";
            this.BTNAprobar.Text = "Aprobar";
            this.BTNAprobar.UseVisualStyleBackColor = false;
            this.BTNAprobar.Click += new System.EventHandler(this.BTNAprobar_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1238, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "CALIFICAN";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // _686DPfrmAuditarSiniestro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(147)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(1358, 393);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.BTNAprobar);
            this.Controls.Add(this.dataGridView1);
            this.Name = "_686DPfrmAuditarSiniestro";
            this.Text = "_686DPfrmAuditarSiniestro";
            this.Load += new System.EventHandler(this._686DPfrmAuditarSiniestro_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BTNAprobar;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}