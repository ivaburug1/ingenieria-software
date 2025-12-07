namespace AseguraYa
{
    partial class _686DP_frmPolizas
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CMBEstado = new System.Windows.Forms.ComboBox();
            this.CMBSeguro = new System.Windows.Forms.ComboBox();
            this.CMBPlan = new System.Windows.Forms.ComboBox();
            this.CMBPrima = new System.Windows.Forms.ComboBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DTFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.TXTPoliza = new System.Windows.Forms.TextBox();
            this.BTNFiltrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 12);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(420, 138);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 170);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(880, 150);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // CMBEstado
            // 
            this.CMBEstado.FormattingEnabled = true;
            this.CMBEstado.Location = new System.Drawing.Point(12, 342);
            this.CMBEstado.Name = "CMBEstado";
            this.CMBEstado.Size = new System.Drawing.Size(184, 21);
            this.CMBEstado.TabIndex = 2;
            // 
            // CMBSeguro
            // 
            this.CMBSeguro.FormattingEnabled = true;
            this.CMBSeguro.Location = new System.Drawing.Point(248, 342);
            this.CMBSeguro.Name = "CMBSeguro";
            this.CMBSeguro.Size = new System.Drawing.Size(184, 21);
            this.CMBSeguro.TabIndex = 3;
            // 
            // CMBPlan
            // 
            this.CMBPlan.FormattingEnabled = true;
            this.CMBPlan.Location = new System.Drawing.Point(472, 342);
            this.CMBPlan.Name = "CMBPlan";
            this.CMBPlan.Size = new System.Drawing.Size(184, 21);
            this.CMBPlan.TabIndex = 4;
            // 
            // CMBPrima
            // 
            this.CMBPrima.FormattingEnabled = true;
            this.CMBPrima.Location = new System.Drawing.Point(708, 342);
            this.CMBPrima.Name = "CMBPrima";
            this.CMBPrima.Size = new System.Drawing.Size(184, 21);
            this.CMBPrima.TabIndex = 5;
            // 
            // chart2
            // 
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(472, 12);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(420, 120);
            this.chart2.TabIndex = 6;
            this.chart2.Text = "chart2";
            // 
            // DTFechaVencimiento
            // 
            this.DTFechaVencimiento.Location = new System.Drawing.Point(12, 388);
            this.DTFechaVencimiento.Name = "DTFechaVencimiento";
            this.DTFechaVencimiento.Size = new System.Drawing.Size(184, 20);
            this.DTFechaVencimiento.TabIndex = 7;
            this.DTFechaVencimiento.ValueChanged += new System.EventHandler(this.DTFechaVencimiento_ValueChanged);
            // 
            // TXTPoliza
            // 
            this.TXTPoliza.Location = new System.Drawing.Point(248, 388);
            this.TXTPoliza.Name = "TXTPoliza";
            this.TXTPoliza.Size = new System.Drawing.Size(184, 20);
            this.TXTPoliza.TabIndex = 8;
            this.TXTPoliza.TextChanged += new System.EventHandler(this.TXTPoliza_TextChanged);
            // 
            // BTNFiltrar
            // 
            this.BTNFiltrar.Location = new System.Drawing.Point(472, 389);
            this.BTNFiltrar.Name = "BTNFiltrar";
            this.BTNFiltrar.Size = new System.Drawing.Size(184, 23);
            this.BTNFiltrar.TabIndex = 9;
            this.BTNFiltrar.Tag = "BTNFiltrar";
            this.BTNFiltrar.Text = "Filtrar";
            this.BTNFiltrar.UseVisualStyleBackColor = true;
            this.BTNFiltrar.Click += new System.EventHandler(this.BTNFiltrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 326);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Tag = "Estado";
            this.label1.Text = "Estado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 326);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 11;
            this.label2.Tag = "Seguro";
            this.label2.Text = "Seguro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(469, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Tag = "Plan";
            this.label3.Text = "Franquicia";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(705, 326);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 13;
            this.label4.Tag = "Prima";
            this.label4.Text = "Prima";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 14;
            this.label5.Tag = "Fecha vencimiento";
            this.label5.Text = "Fecha vencimiento";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(245, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 15;
            this.label6.Tag = "Poliza";
            this.label6.Text = "Poliza";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(708, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 23);
            this.button1.TabIndex = 16;
            this.button1.Tag = "Imprimir";
            this.button1.Text = "Imprimir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(737, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 26);
            this.button2.TabIndex = 17;
            this.button2.Tag = "ImprimirSeleccion";
            this.button2.Text = "Imprimir seleccion";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // _686DP_frmPolizas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 443);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTNFiltrar);
            this.Controls.Add(this.TXTPoliza);
            this.Controls.Add(this.DTFechaVencimiento);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.CMBPrima);
            this.Controls.Add(this.CMBPlan);
            this.Controls.Add(this.CMBSeguro);
            this.Controls.Add(this.CMBEstado);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart1);
            this.Name = "_686DP_frmPolizas";
            this.Text = "_686DP_frmPolizas";
            this.Load += new System.EventHandler(this._686DP_frmPolizas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox CMBEstado;
        private System.Windows.Forms.ComboBox CMBSeguro;
        private System.Windows.Forms.ComboBox CMBPlan;
        private System.Windows.Forms.ComboBox CMBPrima;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DateTimePicker DTFechaVencimiento;
        private System.Windows.Forms.TextBox TXTPoliza;
        private System.Windows.Forms.Button BTNFiltrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}