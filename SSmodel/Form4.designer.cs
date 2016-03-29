using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SSmodel
{
    partial class Form4
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.label18 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DataSheetLabel = new System.Windows.Forms.Label();
            this.ModelLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AnalyButton = new System.Windows.Forms.Button();
            this.TrainButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.TrainDataPre = new System.Windows.Forms.TextBox();
            this.CvNameLabel = new System.Windows.Forms.Label();
            this.MvNameLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.CText = new System.Windows.Forms.TextBox();
            this.ParLabel = new System.Windows.Forms.Label();
            this.OutputNumText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.InputNumText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DetaText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TestDataPre = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.TestModel = new System.Windows.Forms.Button();
            this.LoadModel = new System.Windows.Forms.Button();
            this.NewModel = new System.Windows.Forms.Button();
            this.LoadDataSheet = new System.Windows.Forms.Button();
            this.SaveModel = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label18
            // 
            this.label18.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label18.Location = new System.Drawing.Point(183, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(445, 46);
            this.label18.TabIndex = 47;
            this.label18.Text = "LS-SVR模型(训练系统)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "数据表：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 53;
            this.label2.Text = "模  型：";
            // 
            // DataSheetLabel
            // 
            this.DataSheetLabel.AutoSize = true;
            this.DataSheetLabel.Location = new System.Drawing.Point(83, 68);
            this.DataSheetLabel.Name = "DataSheetLabel";
            this.DataSheetLabel.Size = new System.Drawing.Size(11, 12);
            this.DataSheetLabel.TabIndex = 54;
            this.DataSheetLabel.Text = " ";
            // 
            // ModelLabel
            // 
            this.ModelLabel.AutoSize = true;
            this.ModelLabel.Location = new System.Drawing.Point(83, 90);
            this.ModelLabel.Name = "ModelLabel";
            this.ModelLabel.Size = new System.Drawing.Size(11, 12);
            this.ModelLabel.TabIndex = 55;
            this.ModelLabel.Text = " ";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.chart2);
            this.groupBox3.Location = new System.Drawing.Point(3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(539, 211);
            this.groupBox3.TabIndex = 68;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "归一化测试曲线";
            // 
            // chart2
            // 
            this.chart2.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.chart2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.chart2.BorderlineColor = System.Drawing.Color.DarkRed;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(3, 17);
            this.chart2.Name = "chart2";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Lime;
            series1.Legend = "Legend1";
            series1.Name = "测试";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Blue;
            series2.Legend = "Legend1";
            series2.Name = "原始";
            this.chart2.Series.Add(series1);
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(533, 191);
            this.chart2.TabIndex = 0;
            this.chart2.Text = "chart2";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chart1);
            this.groupBox2.Location = new System.Drawing.Point(3, 218);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(539, 217);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试曲线";
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(3, 17);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Lime;
            series3.Legend = "Legend1";
            series3.Name = "原始";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Blue;
            series4.Legend = "Legend1";
            series4.Name = "测试";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(533, 197);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(26, 121);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 435);
            this.panel1.TabIndex = 72;
            // 
            // AnalyButton
            // 
            this.AnalyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AnalyButton.Location = new System.Drawing.Point(796, 439);
            this.AnalyButton.Name = "AnalyButton";
            this.AnalyButton.Size = new System.Drawing.Size(75, 24);
            this.AnalyButton.TabIndex = 81;
            this.AnalyButton.Text = "分析模型";
            this.AnalyButton.UseVisualStyleBackColor = true;
            this.AnalyButton.Click += new System.EventHandler(this.AnalyButton_Click);
            // 
            // TrainButton
            // 
            this.TrainButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TrainButton.Location = new System.Drawing.Point(696, 439);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(75, 23);
            this.TrainButton.TabIndex = 80;
            this.TrainButton.Text = "训练模型";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(665, 482);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(71, 20);
            this.comboBox1.TabIndex = 79;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.TrainDataPre);
            this.groupBox1.Controls.Add(this.CvNameLabel);
            this.groupBox1.Controls.Add(this.MvNameLabel);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.CText);
            this.groupBox1.Controls.Add(this.ParLabel);
            this.groupBox1.Controls.Add(this.OutputNumText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.InputNumText);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.DetaText);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TestDataPre);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Location = new System.Drawing.Point(577, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 223);
            this.groupBox1.TabIndex = 77;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模型参数组态";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(266, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 14);
            this.label8.TabIndex = 72;
            this.label8.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(121, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 71;
            this.label4.Text = "%";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 70;
            this.label3.Text = "修改变量参数：";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(103, 121);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(71, 20);
            this.comboBox2.TabIndex = 69;
            this.comboBox2.Click += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // TrainDataPre
            // 
            this.TrainDataPre.Location = new System.Drawing.Point(75, 75);
            this.TrainDataPre.Name = "TrainDataPre";
            this.TrainDataPre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TrainDataPre.Size = new System.Drawing.Size(44, 21);
            this.TrainDataPre.TabIndex = 2;
            this.TrainDataPre.Text = "85";
            this.TrainDataPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CvNameLabel
            // 
            this.CvNameLabel.AutoSize = true;
            this.CvNameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CvNameLabel.Location = new System.Drawing.Point(125, 46);
            this.CvNameLabel.Name = "CvNameLabel";
            this.CvNameLabel.Size = new System.Drawing.Size(11, 12);
            this.CvNameLabel.TabIndex = 68;
            this.CvNameLabel.Text = " ";
            // 
            // MvNameLabel
            // 
            this.MvNameLabel.AutoSize = true;
            this.MvNameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MvNameLabel.Location = new System.Drawing.Point(125, 21);
            this.MvNameLabel.Name = "MvNameLabel";
            this.MvNameLabel.Size = new System.Drawing.Size(11, 12);
            this.MvNameLabel.TabIndex = 47;
            this.MvNameLabel.Text = " ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 27);
            this.button1.TabIndex = 42;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(114, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 14;
            this.label9.Text = "C：";
            // 
            // CText
            // 
            this.CText.Location = new System.Drawing.Point(140, 151);
            this.CText.Name = "CText";
            this.CText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CText.Size = new System.Drawing.Size(49, 21);
            this.CText.TabIndex = 13;
            this.CText.Text = "1000";
            this.CText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ParLabel
            // 
            this.ParLabel.AutoSize = true;
            this.ParLabel.Location = new System.Drawing.Point(17, 154);
            this.ParLabel.Name = "ParLabel";
            this.ParLabel.Size = new System.Drawing.Size(41, 12);
            this.ParLabel.TabIndex = 12;
            this.ParLabel.Text = "参数：";
            // 
            // OutputNumText
            // 
            this.OutputNumText.Location = new System.Drawing.Point(75, 43);
            this.OutputNumText.Name = "OutputNumText";
            this.OutputNumText.ReadOnly = true;
            this.OutputNumText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.OutputNumText.Size = new System.Drawing.Size(44, 21);
            this.OutputNumText.TabIndex = 11;
            this.OutputNumText.Text = "0";
            this.OutputNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "输出变量：";
            // 
            // InputNumText
            // 
            this.InputNumText.Location = new System.Drawing.Point(75, 18);
            this.InputNumText.Name = "InputNumText";
            this.InputNumText.ReadOnly = true;
            this.InputNumText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.InputNumText.Size = new System.Drawing.Size(44, 21);
            this.InputNumText.TabIndex = 9;
            this.InputNumText.Text = "0";
            this.InputNumText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "输入变量：";
            // 
            // DetaText
            // 
            this.DetaText.Location = new System.Drawing.Point(244, 151);
            this.DetaText.Name = "DetaText";
            this.DetaText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DetaText.Size = new System.Drawing.Size(50, 21);
            this.DetaText.TabIndex = 7;
            this.DetaText.Text = "1";
            this.DetaText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "Deta：";
            // 
            // TestDataPre
            // 
            this.TestDataPre.Location = new System.Drawing.Point(220, 76);
            this.TestDataPre.Name = "TestDataPre";
            this.TestDataPre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TestDataPre.Size = new System.Drawing.Size(44, 21);
            this.TestDataPre.TabIndex = 5;
            this.TestDataPre.Text = "15";
            this.TestDataPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(158, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 4;
            this.label19.Text = "测试数据：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(13, 79);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 3;
            this.label21.Text = "训练数据：";
            // 
            // TestModel
            // 
            this.TestModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TestModel.Location = new System.Drawing.Point(735, 480);
            this.TestModel.Name = "TestModel";
            this.TestModel.Size = new System.Drawing.Size(75, 23);
            this.TestModel.TabIndex = 78;
            this.TestModel.Text = "验证模型";
            this.TestModel.UseVisualStyleBackColor = true;
            this.TestModel.Click += new System.EventHandler(this.TestModel_Click);
            // 
            // LoadModel
            // 
            this.LoadModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadModel.Location = new System.Drawing.Point(593, 438);
            this.LoadModel.Name = "LoadModel";
            this.LoadModel.Size = new System.Drawing.Size(75, 24);
            this.LoadModel.TabIndex = 76;
            this.LoadModel.Text = "载入模型";
            this.LoadModel.UseVisualStyleBackColor = true;
            this.LoadModel.Click += new System.EventHandler(this.LoadModel_Click);
            // 
            // NewModel
            // 
            this.NewModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.NewModel.Location = new System.Drawing.Point(696, 381);
            this.NewModel.Name = "NewModel";
            this.NewModel.Size = new System.Drawing.Size(75, 24);
            this.NewModel.TabIndex = 75;
            this.NewModel.Text = "新建模型";
            this.NewModel.UseVisualStyleBackColor = true;
            this.NewModel.Click += new System.EventHandler(this.NewModel_Click);
            // 
            // LoadDataSheet
            // 
            this.LoadDataSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadDataSheet.Location = new System.Drawing.Point(593, 381);
            this.LoadDataSheet.Name = "LoadDataSheet";
            this.LoadDataSheet.Size = new System.Drawing.Size(75, 23);
            this.LoadDataSheet.TabIndex = 74;
            this.LoadDataSheet.Text = "载入数据表";
            this.LoadDataSheet.UseVisualStyleBackColor = true;
            this.LoadDataSheet.Click += new System.EventHandler(this.LoadDataSheet_Click);
            // 
            // SaveModel
            // 
            this.SaveModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveModel.Location = new System.Drawing.Point(796, 381);
            this.SaveModel.Name = "SaveModel";
            this.SaveModel.Size = new System.Drawing.Size(75, 23);
            this.SaveModel.TabIndex = 73;
            this.SaveModel.Text = "保存模型";
            this.SaveModel.UseVisualStyleBackColor = true;
            this.SaveModel.Click += new System.EventHandler(this.SaveModel_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 566);
            this.Controls.Add(this.AnalyButton);
            this.Controls.Add(this.TrainButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TestModel);
            this.Controls.Add(this.LoadModel);
            this.Controls.Add(this.NewModel);
            this.Controls.Add(this.LoadDataSheet);
            this.Controls.Add(this.SaveModel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ModelLabel);
            this.Controls.Add(this.DataSheetLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label18);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advacon System One";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label DataSheetLabel;
        private System.Windows.Forms.Label ModelLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private Button AnalyButton;
        private Button TrainButton;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private Label label8;
        private Label label4;
        private Label label3;
        private ComboBox comboBox2;
        private TextBox TrainDataPre;
        private Label CvNameLabel;
        private Label MvNameLabel;
        private Button button1;
        private Label label9;
        private TextBox CText;
        private Label ParLabel;
        private TextBox OutputNumText;
        private Label label7;
        private TextBox InputNumText;
        private Label label6;
        private TextBox DetaText;
        private Label label5;
        private TextBox TestDataPre;
        private Label label19;
        private Label label21;
        private Button TestModel;
        private Button LoadModel;
        private Button NewModel;
        private Button LoadDataSheet;
        private Button SaveModel;
    }
}