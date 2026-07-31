namespace StageLink
{
    partial class GestorRespaldo
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
            this.BTNBackUP = new System.Windows.Forms.Button();
            this.BTNRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTNBackUP
            // 
            this.BTNBackUP.Location = new System.Drawing.Point(12, 12);
            this.BTNBackUP.Name = "BTNBackUP";
            this.BTNBackUP.Size = new System.Drawing.Size(145, 50);
            this.BTNBackUP.TabIndex = 0;
            this.BTNBackUP.Tag = "BTNBackUP";
            this.BTNBackUP.Text = "Backup";
            this.BTNBackUP.UseVisualStyleBackColor = true;
            this.BTNBackUP.Click += new System.EventHandler(this.BTNBackUP_Click);
            // 
            // BTNRestore
            // 
            this.BTNRestore.Location = new System.Drawing.Point(163, 12);
            this.BTNRestore.Name = "BTNRestore";
            this.BTNRestore.Size = new System.Drawing.Size(145, 50);
            this.BTNRestore.TabIndex = 1;
            this.BTNRestore.Tag = "BTNRestore";
            this.BTNRestore.Text = "Restore";
            this.BTNRestore.UseVisualStyleBackColor = true;
            this.BTNRestore.Click += new System.EventHandler(this.BTNRestore_Click);
            // 
            // GestorRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orchid;
            this.ClientSize = new System.Drawing.Size(321, 75);
            this.Controls.Add(this.BTNRestore);
            this.Controls.Add(this.BTNBackUP);
            this.Name = "GestorRespaldo";
            this.Text = "GestorRespaldo";
            this.Load += new System.EventHandler(this.GestorRespaldo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BTNBackUP;
        private System.Windows.Forms.Button BTNRestore;
    }
}