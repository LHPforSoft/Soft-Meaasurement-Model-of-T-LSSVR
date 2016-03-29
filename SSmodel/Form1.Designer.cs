﻿namespace SSmodel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.labelOriginalDt = new System.Windows.Forms.Label();
            this.labeldt = new System.Windows.Forms.Label();
            this.labeltitle = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCreatNewSheet = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnSavePra = new System.Windows.Forms.Button();
            this.labelTaoR = new System.Windows.Forms.Label();
            this.labelTaoD = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLoadOriginalDt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnIdentifyTaoDPara = new System.Windows.Forms.Button();
            this.textBoxTaoDCorrLength = new System.Windows.Forms.TextBox();
            this.combOutputVar = new System.Windows.Forms.ComboBox();
            this.combInptuVar = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTaoRCorrLength = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxVarLength = new System.Windows.Forms.TextBox();
            this.btnIdentifyTaoRPara = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelInputVar = new System.Windows.Forms.Label();
            this.labelOutputVar = new System.Windows.Forms.Label();
            this.btnNewTParModel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTParpath = new System.Windows.Forms.Label();
            this.btnLoadTParModel = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelOriginalDt
            // 
            this.labelOriginalDt.AutoSize = true;
            this.labelOriginalDt.Location = new System.Drawing.Point(125, 70);
            this.labelOriginalDt.Name = "labelOriginalDt";
            this.labelOriginalDt.Size = new System.Drawing.Size(11, 12);
            this.labelOriginalDt.TabIndex = 41;
            this.labelOriginalDt.Text = "_";
            this.labelOriginalDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labeldt
            // 
            this.labeldt.AutoSize = true;
            this.labeldt.Location = new System.Drawing.Point(30, 70);
            this.labeldt.Name = "labeldt";
            this.labeldt.Size = new System.Drawing.Size(77, 12);
            this.labeldt.TabIndex = 40;
            this.labeldt.Tag = "";
            this.labeldt.Text = "原始数据表：";
            this.labeldt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labeltitle
            // 
            this.labeltitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labeltitle.AutoSize = true;
            this.labeltitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labeltitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labeltitle.Location = new System.Drawing.Point(197, 15);
            this.labeltitle.Name = "labeltitle";
            this.labeltitle.Size = new System.Drawing.Size(578, 46);
            this.labeltitle.TabIndex = 56;
            this.labeltitle.Text = "回转窑延时参数辨识系统（T）";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.chart1);
            this.groupBox2.Location = new System.Drawing.Point(32, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(617, 447);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据显示曲线";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            chartArea5.Name = "ChartArea1";
            chartArea6.Name = "ChartArea2";
            this.chart1.ChartAreas.Add(chartArea5);
            this.chart1.ChartAreas.Add(chartArea6);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(6, 17);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(605, 424);
            this.chart1.TabIndex = 80;
            this.chart1.Text = "chart1";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnCreatNewSheet);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Location = new System.Drawing.Point(655, 419);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(295, 183);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "延时参数辨识结果";
            // 
            // btnCreatNewSheet
            // 
            this.btnCreatNewSheet.Location = new System.Drawing.Point(165, 150);
            this.btnCreatNewSheet.Name = "btnCreatNewSheet";
            this.btnCreatNewSheet.Size = new System.Drawing.Size(100, 27);
            this.btnCreatNewSheet.TabIndex = 28;
            this.btnCreatNewSheet.Text = "生成新数据表";
            this.btnCreatNewSheet.UseVisualStyleBackColor = true;
            this.btnCreatNewSheet.Click += new System.EventHandler(this.btnCreatNewSheet_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnSavePra);
            this.groupBox7.Controls.Add(this.labelTaoR);
            this.groupBox7.Controls.Add(this.labelTaoD);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(18, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(253, 123);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "辨识结果";
            // 
            // btnSavePra
            // 
            this.btnSavePra.Location = new System.Drawing.Point(147, 90);
            this.btnSavePra.Name = "btnSavePra";
            this.btnSavePra.Size = new System.Drawing.Size(100, 27);
            this.btnSavePra.TabIndex = 32;
            this.btnSavePra.Text = "保存辨识结果";
            this.btnSavePra.UseVisualStyleBackColor = true;
            this.btnSavePra.Click += new System.EventHandler(this.btnSavePra_Click);
            // 
            // labelTaoR
            // 
            this.labelTaoR.AutoSize = true;
            this.labelTaoR.Location = new System.Drawing.Point(99, 56);
            this.labelTaoR.Name = "labelTaoR";
            this.labelTaoR.Size = new System.Drawing.Size(11, 12);
            this.labelTaoR.TabIndex = 31;
            this.labelTaoR.Text = "_";
            // 
            // labelTaoD
            // 
            this.labelTaoD.AutoSize = true;
            this.labelTaoD.Location = new System.Drawing.Point(99, 30);
            this.labelTaoD.Name = "labelTaoD";
            this.labelTaoD.Size = new System.Drawing.Size(11, 12);
            this.labelTaoD.TabIndex = 30;
            this.labelTaoD.Text = "_";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "静态响应延时:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 29;
            this.label10.Text = "动态响应延时:";
            // 
            // btnLoadOriginalDt
            // 
            this.btnLoadOriginalDt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadOriginalDt.Location = new System.Drawing.Point(811, 87);
            this.btnLoadOriginalDt.Name = "btnLoadOriginalDt";
            this.btnLoadOriginalDt.Size = new System.Drawing.Size(129, 31);
            this.btnLoadOriginalDt.TabIndex = 0;
            this.btnLoadOriginalDt.Text = "载入原始数据表";
            this.btnLoadOriginalDt.UseVisualStyleBackColor = true;
            this.btnLoadOriginalDt.Click += new System.EventHandler(this.btnLoadOriginalDt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnIdentifyTaoDPara);
            this.groupBox1.Controls.Add(this.textBoxTaoDCorrLength);
            this.groupBox1.Controls.Add(this.combOutputVar);
            this.groupBox1.Controls.Add(this.combInptuVar);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxTaoRCorrLength);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxVarLength);
            this.groupBox1.Controls.Add(this.btnIdentifyTaoRPara);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(655, 195);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(295, 214);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "延时参数辨识组态";
            // 
            // btnIdentifyTaoDPara
            // 
            this.btnIdentifyTaoDPara.Location = new System.Drawing.Point(12, 182);
            this.btnIdentifyTaoDPara.Name = "btnIdentifyTaoDPara";
            this.btnIdentifyTaoDPara.Size = new System.Drawing.Size(116, 23);
            this.btnIdentifyTaoDPara.TabIndex = 86;
            this.btnIdentifyTaoDPara.Text = "辨识静态延时参数";
            this.btnIdentifyTaoDPara.UseVisualStyleBackColor = true;
            this.btnIdentifyTaoDPara.Click += new System.EventHandler(this.btnIdentifyTaoDPara_Click);
            // 
            // textBoxTaoDCorrLength
            // 
            this.textBoxTaoDCorrLength.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTaoDCorrLength.ForeColor = System.Drawing.Color.Black;
            this.textBoxTaoDCorrLength.Location = new System.Drawing.Point(170, 117);
            this.textBoxTaoDCorrLength.Name = "textBoxTaoDCorrLength";
            this.textBoxTaoDCorrLength.Size = new System.Drawing.Size(89, 21);
            this.textBoxTaoDCorrLength.TabIndex = 84;
            this.textBoxTaoDCorrLength.Text = "20";
            this.textBoxTaoDCorrLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // combOutputVar
            // 
            this.combOutputVar.FormattingEnabled = true;
            this.combOutputVar.Location = new System.Drawing.Point(89, 54);
            this.combOutputVar.Name = "combOutputVar";
            this.combOutputVar.Size = new System.Drawing.Size(172, 20);
            this.combOutputVar.TabIndex = 83;
            // 
            // combInptuVar
            // 
            this.combInptuVar.FormattingEnabled = true;
            this.combInptuVar.Location = new System.Drawing.Point(89, 20);
            this.combInptuVar.Name = "combInptuVar";
            this.combInptuVar.Size = new System.Drawing.Size(172, 20);
            this.combInptuVar.TabIndex = 82;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 12);
            this.label8.TabIndex = 85;
            this.label8.Text = "静态延时最大相关步数(n1)：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 80;
            this.label1.Text = "输入变量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 81;
            this.label2.Text = "输出变量：";
            // 
            // textBoxTaoRCorrLength
            // 
            this.textBoxTaoRCorrLength.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTaoRCorrLength.ForeColor = System.Drawing.Color.Black;
            this.textBoxTaoRCorrLength.Location = new System.Drawing.Point(170, 148);
            this.textBoxTaoRCorrLength.Name = "textBoxTaoRCorrLength";
            this.textBoxTaoRCorrLength.Size = new System.Drawing.Size(91, 21);
            this.textBoxTaoRCorrLength.TabIndex = 12;
            this.textBoxTaoRCorrLength.Text = "100";
            this.textBoxTaoRCorrLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTaoRCorrLength.MouseEnter += new System.EventHandler(this.textBoxCorrLength_MouseEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 78;
            this.label3.Text = "相关变量的长度(N)：";
            // 
            // textBoxVarLength
            // 
            this.textBoxVarLength.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxVarLength.ForeColor = System.Drawing.Color.Black;
            this.textBoxVarLength.Location = new System.Drawing.Point(170, 88);
            this.textBoxVarLength.Name = "textBoxVarLength";
            this.textBoxVarLength.Size = new System.Drawing.Size(91, 21);
            this.textBoxVarLength.TabIndex = 11;
            this.textBoxVarLength.Text = "1000";
            this.textBoxVarLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxVarLength.MouseEnter += new System.EventHandler(this.textBoxVarLength_MouseEnter);
            // 
            // btnIdentifyTaoRPara
            // 
            this.btnIdentifyTaoRPara.Location = new System.Drawing.Point(148, 182);
            this.btnIdentifyTaoRPara.Name = "btnIdentifyTaoRPara";
            this.btnIdentifyTaoRPara.Size = new System.Drawing.Size(123, 23);
            this.btnIdentifyTaoRPara.TabIndex = 13;
            this.btnIdentifyTaoRPara.Text = "辨识动态延时参数";
            this.btnIdentifyTaoRPara.UseVisualStyleBackColor = true;
            this.btnIdentifyTaoRPara.Click += new System.EventHandler(this.btnIdentifyPara_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "动态延时最大相关步数(n2)：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 42;
            this.label5.Text = "输入变量：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "输出变量：";
            // 
            // labelInputVar
            // 
            this.labelInputVar.AutoSize = true;
            this.labelInputVar.Location = new System.Drawing.Point(95, 116);
            this.labelInputVar.Name = "labelInputVar";
            this.labelInputVar.Size = new System.Drawing.Size(11, 12);
            this.labelInputVar.TabIndex = 43;
            this.labelInputVar.Text = "_";
            this.labelInputVar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOutputVar
            // 
            this.labelOutputVar.AutoSize = true;
            this.labelOutputVar.Location = new System.Drawing.Point(95, 140);
            this.labelOutputVar.Name = "labelOutputVar";
            this.labelOutputVar.Size = new System.Drawing.Size(11, 12);
            this.labelOutputVar.TabIndex = 45;
            this.labelOutputVar.Text = "_";
            this.labelOutputVar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNewTParModel
            // 
            this.btnNewTParModel.Location = new System.Drawing.Point(669, 163);
            this.btnNewTParModel.Name = "btnNewTParModel";
            this.btnNewTParModel.Size = new System.Drawing.Size(123, 25);
            this.btnNewTParModel.TabIndex = 66;
            this.btnNewTParModel.Text = "新建延时参数表";
            this.btnNewTParModel.UseVisualStyleBackColor = true;
            this.btnNewTParModel.Click += new System.EventHandler(this.btnNewTParModel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 92);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 67;
            this.label7.Text = "延时参数模型：";
            // 
            // labelTParpath
            // 
            this.labelTParpath.AutoSize = true;
            this.labelTParpath.Location = new System.Drawing.Point(125, 92);
            this.labelTParpath.Name = "labelTParpath";
            this.labelTParpath.Size = new System.Drawing.Size(11, 12);
            this.labelTParpath.TabIndex = 68;
            this.labelTParpath.Text = "_";
            // 
            // btnLoadTParModel
            // 
            this.btnLoadTParModel.Location = new System.Drawing.Point(817, 163);
            this.btnLoadTParModel.Name = "btnLoadTParModel";
            this.btnLoadTParModel.Size = new System.Drawing.Size(123, 26);
            this.btnLoadTParModel.TabIndex = 69;
            this.btnLoadTParModel.Text = "载入延时参数表";
            this.btnLoadTParModel.UseVisualStyleBackColor = true;
            this.btnLoadTParModel.Click += new System.EventHandler(this.btnLoadTParModel_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(796, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 70;
            this.label11.Text = "或";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 622);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnLoadTParModel);
            this.Controls.Add(this.labelTParpath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnNewTParModel);
            this.Controls.Add(this.labelOutputVar);
            this.Controls.Add(this.labelInputVar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLoadOriginalDt);
            this.Controls.Add(this.labelOriginalDt);
            this.Controls.Add(this.labeldt);
            this.Controls.Add(this.labeltitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOriginalDt;
        private System.Windows.Forms.Label labeldt;
        private System.Windows.Forms.Label labeltitle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnLoadOriginalDt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnIdentifyTaoRPara;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label labelTaoR;
        private System.Windows.Forms.Label labelTaoD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelInputVar;
        private System.Windows.Forms.Label labelOutputVar;
        private System.Windows.Forms.Button btnSavePra;
        private System.Windows.Forms.TextBox textBoxTaoRCorrLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVarLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combOutputVar;
        private System.Windows.Forms.ComboBox combInptuVar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewTParModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelTParpath;
        private System.Windows.Forms.TextBox textBoxTaoDCorrLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnIdentifyTaoDPara;
        private System.Windows.Forms.Button btnLoadTParModel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCreatNewSheet;
    }
}

