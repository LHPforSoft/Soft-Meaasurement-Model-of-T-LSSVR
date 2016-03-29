namespace SSmodel
{
    partial class Form5
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DataSheetLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TagcomboBox = new System.Windows.Forms.ComboBox();
            this.AnalyLSSVMModel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AnalyGain = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MVcomboBox = new System.Windows.Forms.ComboBox();
            this.CVcomboBox = new System.Windows.Forms.ComboBox();
            this.AnalyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ModelLabel
            // 
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Location = new System.Drawing.Point(83, 91);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(11, 12);
            this.ModelLabel.TabIndex = 43;
            this.ModelLabel.Text = " ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "模  型：";
            // 
            // DataSheetLabel
            // 
            this.DataSheetLabel.AutoSize = true;
            this.DataSheetLabel.Location = new System.Drawing.Point(83, 69);
            this.DataSheetLabel.Name = "DataSheetLabel";
            this.DataSheetLabel.Size = new System.Drawing.Size(11, 12);
            this.DataSheetLabel.TabIndex = 41;
            this.DataSheetLabel.Text = " ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "数据表：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("华文彩云", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(281, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(379, 41);
            this.label3.TabIndex = 44;
            this.label3.Text = "LS-SVM稳态模型分析";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TagcomboBox);
            this.groupBox3.Controls.Add(this.AnalyLSSVMModel);
            this.groupBox3.Location = new System.Drawing.Point(14, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(123, 87);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "预测VS实际";
            // 
            // TagcomboBox
            // 
            this.TagcomboBox.FormattingEnabled = true;
            this.TagcomboBox.Location = new System.Drawing.Point(11, 22);
            this.TagcomboBox.Name = "TagcomboBox";
            this.TagcomboBox.Size = new System.Drawing.Size(106, 20);
            this.TagcomboBox.TabIndex = 2;
            // 
            // AnalyLSSVMModel
            // 
            this.AnalyLSSVMModel.Location = new System.Drawing.Point(11, 48);
            this.AnalyLSSVMModel.Name = "AnalyLSSVMModel";
            this.AnalyLSSVMModel.Size = new System.Drawing.Size(106, 21);
            this.AnalyLSSVMModel.TabIndex = 1;
            this.AnalyLSSVMModel.Text = "测试模型";
            this.AnalyLSSVMModel.UseVisualStyleBackColor = true;
            this.AnalyLSSVMModel.Click += new System.EventHandler(this.AnalyLSSVMModel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.AnalyGain);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.MVcomboBox);
            this.groupBox2.Controls.Add(this.CVcomboBox);
            this.groupBox2.Location = new System.Drawing.Point(14, 254);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(123, 107);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系统增益分析";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "CV：";
            // 
            // AnalyGain
            // 
            this.AnalyGain.Location = new System.Drawing.Point(11, 72);
            this.AnalyGain.Name = "AnalyGain";
            this.AnalyGain.Size = new System.Drawing.Size(106, 21);
            this.AnalyGain.TabIndex = 3;
            this.AnalyGain.Text = "分析增益";
            this.AnalyGain.UseVisualStyleBackColor = true;
            this.AnalyGain.Click += new System.EventHandler(this.AnalyGain_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "MV：";
            // 
            // MVcomboBox
            // 
            this.MVcomboBox.FormattingEnabled = true;
            this.MVcomboBox.Location = new System.Drawing.Point(53, 20);
            this.MVcomboBox.Name = "MVcomboBox";
            this.MVcomboBox.Size = new System.Drawing.Size(64, 20);
            this.MVcomboBox.TabIndex = 4;
            // 
            // CVcomboBox
            // 
            this.CVcomboBox.FormattingEnabled = true;
            this.CVcomboBox.Location = new System.Drawing.Point(53, 46);
            this.CVcomboBox.Name = "CVcomboBox";
            this.CVcomboBox.Size = new System.Drawing.Size(64, 20);
            this.CVcomboBox.TabIndex = 5;
            // 
            // AnalyChart
            // 
            this.AnalyChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            chartArea10.Name = "ChartArea1";
            this.AnalyChart.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.AnalyChart.Legends.Add(legend10);
            this.AnalyChart.Location = new System.Drawing.Point(15, 16);
            this.AnalyChart.Name = "AnalyChart";
            this.AnalyChart.Size = new System.Drawing.Size(705, 403);
            this.AnalyChart.TabIndex = 0;
            this.AnalyChart.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.AnalyChart);
            this.groupBox1.Location = new System.Drawing.Point(143, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(737, 430);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "分析曲线显示";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 566);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DataSheetLabel);
            this.Controls.Add(this.label5);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ModelLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DataSheetLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox TagcomboBox;
        private System.Windows.Forms.Button AnalyLSSVMModel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AnalyGain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox MVcomboBox;
        private System.Windows.Forms.ComboBox CVcomboBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart AnalyChart;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}