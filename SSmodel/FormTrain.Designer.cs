namespace SSmodel
{
    partial class FormTrain
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.labelModel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnLoadNewDt = new System.Windows.Forms.Button();
            this.labelnewDt = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.TrainDataPre = new System.Windows.Forms.TextBox();
            this.btnChangePar = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.CText = new System.Windows.Forms.TextBox();
            this.ParLabel = new System.Windows.Forms.Label();
            this.DetaText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TestDataPre = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.btnTrainModel = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnTestModel = new System.Windows.Forms.Button();
            this.btnLoadModel = new System.Windows.Forms.Button();
            this.btnNewModel = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labelOutputVar = new System.Windows.Forms.Label();
            this.labelInputVar = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLoadTParModel = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.labelTParpath = new System.Windows.Forms.Label();
            this.labelTaoR = new System.Windows.Forms.Label();
            this.labelTaoD = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelModel.Location = new System.Drawing.Point(88, 120);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(11, 12);
            this.labelModel.TabIndex = 66;
            this.labelModel.Text = "_";
            this.labelModel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 65;
            this.label2.Text = "模    型：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.chart1);
            this.groupBox2.Location = new System.Drawing.Point(17, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(615, 408);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据显示区域";
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "ChartArea2";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(6, 20);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(603, 382);
            this.chart1.TabIndex = 80;
            this.chart1.Text = "chart1";
            // 
            // btnLoadNewDt
            // 
            this.btnLoadNewDt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadNewDt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadNewDt.Location = new System.Drawing.Point(787, 103);
            this.btnLoadNewDt.Name = "btnLoadNewDt";
            this.btnLoadNewDt.Size = new System.Drawing.Size(135, 36);
            this.btnLoadNewDt.TabIndex = 62;
            this.btnLoadNewDt.Text = "载入数据表";
            this.btnLoadNewDt.UseVisualStyleBackColor = false;
            this.btnLoadNewDt.Click += new System.EventHandler(this.btnLoadNewDt_Click);
            // 
            // labelnewDt
            // 
            this.labelnewDt.AutoSize = true;
            this.labelnewDt.Location = new System.Drawing.Point(88, 62);
            this.labelnewDt.Name = "labelnewDt";
            this.labelnewDt.Size = new System.Drawing.Size(11, 12);
            this.labelnewDt.TabIndex = 64;
            this.labelnewDt.Text = "_";
            this.labelnewDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 63;
            this.label1.Tag = "";
            this.label1.Text = "数  据  表：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelTitle.Location = new System.Drawing.Point(148, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(647, 46);
            this.labelTitle.TabIndex = 69;
            this.labelTitle.Text = "T-LSSVR生料细度软测量训练系统";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.TrainDataPre);
            this.groupBox1.Controls.Add(this.btnChangePar);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.CText);
            this.groupBox1.Controls.Add(this.ParLabel);
            this.groupBox1.Controls.Add(this.DetaText);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.TestDataPre);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Location = new System.Drawing.Point(638, 271);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 154);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模型参数组态";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F);
            this.label8.Location = new System.Drawing.Point(276, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 14);
            this.label8.TabIndex = 92;
            this.label8.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(135, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 14);
            this.label4.TabIndex = 91;
            this.label4.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 90;
            this.label5.Text = "修改主导变量：";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(127, 28);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(157, 20);
            this.comboBox2.TabIndex = 89;
            // 
            // TrainDataPre
            // 
            this.TrainDataPre.Location = new System.Drawing.Point(85, 60);
            this.TrainDataPre.Name = "TrainDataPre";
            this.TrainDataPre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TrainDataPre.Size = new System.Drawing.Size(44, 21);
            this.TrainDataPre.TabIndex = 73;
            this.TrainDataPre.Text = "85";
            this.TrainDataPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnChangePar
            // 
            this.btnChangePar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnChangePar.Location = new System.Drawing.Point(157, 123);
            this.btnChangePar.Name = "btnChangePar";
            this.btnChangePar.Size = new System.Drawing.Size(133, 27);
            this.btnChangePar.TabIndex = 86;
            this.btnChangePar.Text = "修改训练模型参数";
            this.btnChangePar.UseVisualStyleBackColor = false;
            this.btnChangePar.Click += new System.EventHandler(this.btnChangePar_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(98, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 85;
            this.label9.Text = "C：";
            // 
            // CText
            // 
            this.CText.Location = new System.Drawing.Point(127, 93);
            this.CText.Name = "CText";
            this.CText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CText.Size = new System.Drawing.Size(49, 21);
            this.CText.TabIndex = 84;
            this.CText.Text = "1000";
            this.CText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ParLabel
            // 
            this.ParLabel.AutoSize = true;
            this.ParLabel.Location = new System.Drawing.Point(14, 96);
            this.ParLabel.Name = "ParLabel";
            this.ParLabel.Size = new System.Drawing.Size(65, 12);
            this.ParLabel.TabIndex = 83;
            this.ParLabel.Text = "参    数：";
            // 
            // DetaText
            // 
            this.DetaText.Location = new System.Drawing.Point(240, 93);
            this.DetaText.Name = "DetaText";
            this.DetaText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DetaText.Size = new System.Drawing.Size(50, 21);
            this.DetaText.TabIndex = 78;
            this.DetaText.Text = "0.04";
            this.DetaText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(193, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 77;
            this.label10.Text = "Deta：";
            // 
            // TestDataPre
            // 
            this.TestDataPre.Location = new System.Drawing.Point(226, 60);
            this.TestDataPre.Name = "TestDataPre";
            this.TestDataPre.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.TestDataPre.Size = new System.Drawing.Size(44, 21);
            this.TestDataPre.TabIndex = 76;
            this.TestDataPre.Text = "15";
            this.TestDataPre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(155, 64);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 75;
            this.label19.Text = "测试数据：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 63);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 74;
            this.label21.Text = "训练数据：";
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSaveModel.Location = new System.Drawing.Point(830, 564);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(100, 36);
            this.btnSaveModel.TabIndex = 95;
            this.btnSaveModel.Text = "保 存 模 型";
            this.btnSaveModel.UseVisualStyleBackColor = false;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // btnTrainModel
            // 
            this.btnTrainModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrainModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTrainModel.Location = new System.Drawing.Point(828, 431);
            this.btnTrainModel.Name = "btnTrainModel";
            this.btnTrainModel.Size = new System.Drawing.Size(100, 36);
            this.btnTrainModel.TabIndex = 94;
            this.btnTrainModel.Text = "训 练 模 型";
            this.btnTrainModel.UseVisualStyleBackColor = false;
            this.btnTrainModel.Click += new System.EventHandler(this.btnTrainModel_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.btnTestModel);
            this.groupBox3.Location = new System.Drawing.Point(642, 469);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(292, 94);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "泛化能力验证";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 99;
            this.label7.Text = "选择主导变量：";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(123, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 20);
            this.comboBox1.TabIndex = 98;
            // 
            // btnTestModel
            // 
            this.btnTestModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnTestModel.Location = new System.Drawing.Point(188, 60);
            this.btnTestModel.Name = "btnTestModel";
            this.btnTestModel.Size = new System.Drawing.Size(100, 27);
            this.btnTestModel.TabIndex = 97;
            this.btnTestModel.Text = "辨识结果验证";
            this.btnTestModel.UseVisualStyleBackColor = false;
            this.btnTestModel.Click += new System.EventHandler(this.btnTestModel_Click);
            // 
            // btnLoadModel
            // 
            this.btnLoadModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadModel.Location = new System.Drawing.Point(836, 192);
            this.btnLoadModel.Name = "btnLoadModel";
            this.btnLoadModel.Size = new System.Drawing.Size(86, 27);
            this.btnLoadModel.TabIndex = 96;
            this.btnLoadModel.Text = "载入模型";
            this.btnLoadModel.UseVisualStyleBackColor = false;
            this.btnLoadModel.Click += new System.EventHandler(this.btnLoadModel_Click);
            // 
            // btnNewModel
            // 
            this.btnNewModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNewModel.Location = new System.Drawing.Point(721, 192);
            this.btnNewModel.Name = "btnNewModel";
            this.btnNewModel.Size = new System.Drawing.Size(86, 27);
            this.btnNewModel.TabIndex = 93;
            this.btnNewModel.Text = "新建模型";
            this.btnNewModel.UseVisualStyleBackColor = false;
            this.btnNewModel.Click += new System.EventHandler(this.btnNewModel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 94;
            this.label3.Text = "输入变量：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 95;
            this.label11.Text = "输出变量：";
            // 
            // labelOutputVar
            // 
            this.labelOutputVar.AutoSize = true;
            this.labelOutputVar.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOutputVar.Location = new System.Drawing.Point(88, 173);
            this.labelOutputVar.Name = "labelOutputVar";
            this.labelOutputVar.Size = new System.Drawing.Size(11, 12);
            this.labelOutputVar.TabIndex = 97;
            this.labelOutputVar.Text = "_";
            this.labelOutputVar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelInputVar
            // 
            this.labelInputVar.AutoSize = true;
            this.labelInputVar.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelInputVar.Location = new System.Drawing.Point(88, 145);
            this.labelInputVar.MaximumSize = new System.Drawing.Size(700, 0);
            this.labelInputVar.Name = "labelInputVar";
            this.labelInputVar.Size = new System.Drawing.Size(11, 12);
            this.labelInputVar.TabIndex = 96;
            this.labelInputVar.Text = "_";
            this.labelInputVar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(813, 199);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 98;
            this.label6.Text = "或";
            // 
            // btnLoadTParModel
            // 
            this.btnLoadTParModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTParModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadTParModel.Location = new System.Drawing.Point(787, 145);
            this.btnLoadTParModel.Name = "btnLoadTParModel";
            this.btnLoadTParModel.Size = new System.Drawing.Size(135, 34);
            this.btnLoadTParModel.TabIndex = 101;
            this.btnLoadTParModel.Text = "载入延时参数表";
            this.btnLoadTParModel.UseVisualStyleBackColor = false;
            this.btnLoadTParModel.Click += new System.EventHandler(this.btnLoadTParModel_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 96);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 12);
            this.label13.TabIndex = 102;
            this.label13.Text = "延时参数表：";
            // 
            // labelTParpath
            // 
            this.labelTParpath.AutoSize = true;
            this.labelTParpath.Location = new System.Drawing.Point(88, 96);
            this.labelTParpath.Name = "labelTParpath";
            this.labelTParpath.Size = new System.Drawing.Size(11, 12);
            this.labelTParpath.TabIndex = 103;
            this.labelTParpath.Text = "_";
            this.labelTParpath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelTaoR
            // 
            this.labelTaoR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTaoR.AutoSize = true;
            this.labelTaoR.Location = new System.Drawing.Point(730, 249);
            this.labelTaoR.Name = "labelTaoR";
            this.labelTaoR.Size = new System.Drawing.Size(11, 12);
            this.labelTaoR.TabIndex = 107;
            this.labelTaoR.Text = "_";
            // 
            // labelTaoD
            // 
            this.labelTaoD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTaoD.AutoSize = true;
            this.labelTaoD.Location = new System.Drawing.Point(730, 228);
            this.labelTaoD.Name = "labelTaoD";
            this.labelTaoD.Size = new System.Drawing.Size(11, 12);
            this.labelTaoD.TabIndex = 106;
            this.labelTaoD.Text = "_";
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(640, 228);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 12);
            this.label14.TabIndex = 104;
            this.label14.Text = "静态响应延时:";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(640, 249);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 12);
            this.label15.TabIndex = 105;
            this.label15.Text = "动态响应延时:";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(894, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(41, 31);
            this.btnExit.TabIndex = 142;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 612);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelTaoR);
            this.Controls.Add(this.labelTaoD);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.labelTParpath);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnLoadTParModel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSaveModel);
            this.Controls.Add(this.labelOutputVar);
            this.Controls.Add(this.labelInputVar);
            this.Controls.Add(this.btnTrainModel);
            this.Controls.Add(this.btnLoadModel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNewModel);
            this.Controls.Add(this.labelModel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLoadNewDt);
            this.Controls.Add(this.labelnewDt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelTitle);
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnLoadNewDt;
        private System.Windows.Forms.Label labelnewDt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TextBox TrainDataPre;
        private System.Windows.Forms.Button btnChangePar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox CText;
        private System.Windows.Forms.Label ParLabel;
        private System.Windows.Forms.TextBox DetaText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TestDataPre;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Button btnTrainModel;
        private System.Windows.Forms.Button btnNewModel;
        private System.Windows.Forms.Button btnTestModel;
        private System.Windows.Forms.Button btnLoadModel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelOutputVar;
        private System.Windows.Forms.Label labelInputVar;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLoadTParModel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelTParpath;
        private System.Windows.Forms.Label labelTaoR;
        private System.Windows.Forms.Label labelTaoD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnExit;
    }
}