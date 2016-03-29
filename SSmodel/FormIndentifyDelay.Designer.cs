namespace SSmodel
{
    partial class FormIndentifyDelay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.btnIdentifyTaoDPara = new System.Windows.Forms.Button();
            this.btnLoadTParModel = new System.Windows.Forms.Button();
            this.labelTParpath = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnNewTParModel = new System.Windows.Forms.Button();
            this.labelOutputVar = new System.Windows.Forms.Label();
            this.textBoxTaoDCorrLength = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.btnLoadOriginalDt = new System.Windows.Forms.Button();
            this.labelInputVar = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelOriginalDt = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnChose = new System.Windows.Forms.Button();
            this.combChosePar = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelTaoR = new System.Windows.Forms.Label();
            this.labelTaoD = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSavePra = new System.Windows.Forms.Button();
            this.btnCreatNewSheet = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.labeldt = new System.Windows.Forms.Label();
            this.labeltitle = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label11 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIdentifyTaoDPara
            // 
            this.btnIdentifyTaoDPara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnIdentifyTaoDPara.Location = new System.Drawing.Point(12, 182);
            this.btnIdentifyTaoDPara.Name = "btnIdentifyTaoDPara";
            this.btnIdentifyTaoDPara.Size = new System.Drawing.Size(116, 26);
            this.btnIdentifyTaoDPara.TabIndex = 86;
            this.btnIdentifyTaoDPara.Text = "辨识静态延时参数";
            this.btnIdentifyTaoDPara.UseVisualStyleBackColor = false;
            this.btnIdentifyTaoDPara.Click += new System.EventHandler(this.btnIdentifyTaoDPara_Click);
            // 
            // btnLoadTParModel
            // 
            this.btnLoadTParModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadTParModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadTParModel.Location = new System.Drawing.Point(799, 154);
            this.btnLoadTParModel.Name = "btnLoadTParModel";
            this.btnLoadTParModel.Size = new System.Drawing.Size(123, 26);
            this.btnLoadTParModel.TabIndex = 85;
            this.btnLoadTParModel.Text = "载入延时参数表";
            this.btnLoadTParModel.UseVisualStyleBackColor = false;
            this.btnLoadTParModel.Click += new System.EventHandler(this.btnLoadTParModel_Click);
            // 
            // labelTParpath
            // 
            this.labelTParpath.AutoSize = true;
            this.labelTParpath.Location = new System.Drawing.Point(113, 94);
            this.labelTParpath.Name = "labelTParpath";
            this.labelTParpath.Size = new System.Drawing.Size(11, 12);
            this.labelTParpath.TabIndex = 84;
            this.labelTParpath.Text = "_";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "延时参数表：";
            // 
            // btnNewTParModel
            // 
            this.btnNewTParModel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewTParModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNewTParModel.Location = new System.Drawing.Point(651, 154);
            this.btnNewTParModel.Name = "btnNewTParModel";
            this.btnNewTParModel.Size = new System.Drawing.Size(123, 25);
            this.btnNewTParModel.TabIndex = 82;
            this.btnNewTParModel.Text = "新建延时参数表";
            this.btnNewTParModel.UseVisualStyleBackColor = false;
            this.btnNewTParModel.Click += new System.EventHandler(this.btnNewTParModel_Click);
            // 
            // labelOutputVar
            // 
            this.labelOutputVar.AutoSize = true;
            this.labelOutputVar.Location = new System.Drawing.Point(83, 134);
            this.labelOutputVar.Name = "labelOutputVar";
            this.labelOutputVar.Size = new System.Drawing.Size(11, 12);
            this.labelOutputVar.TabIndex = 78;
            this.labelOutputVar.Text = "_";
            this.labelOutputVar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxTaoDCorrLength
            // 
            this.textBoxTaoDCorrLength.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxTaoDCorrLength.ForeColor = System.Drawing.Color.Black;
            this.textBoxTaoDCorrLength.Location = new System.Drawing.Point(170, 117);
            this.textBoxTaoDCorrLength.Name = "textBoxTaoDCorrLength";
            this.textBoxTaoDCorrLength.Size = new System.Drawing.Size(89, 21);
            this.textBoxTaoDCorrLength.TabIndex = 84;
            this.textBoxTaoDCorrLength.Text = "12";
            this.textBoxTaoDCorrLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 77;
            this.label6.Text = "输出变量：";
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
            this.groupBox1.Location = new System.Drawing.Point(651, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 214);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "延时参数辨识组态";
            // 
            // combOutputVar
            // 
            this.combOutputVar.FormattingEnabled = true;
            this.combOutputVar.Location = new System.Drawing.Point(89, 54);
            this.combOutputVar.Name = "combOutputVar";
            this.combOutputVar.Size = new System.Drawing.Size(172, 20);
            this.combOutputVar.TabIndex = 83;
            this.combOutputVar.SelectedIndexChanged += new System.EventHandler(this.combOutputVar_SelectedIndexChanged);
            // 
            // combInptuVar
            // 
            this.combInptuVar.FormattingEnabled = true;
            this.combInptuVar.Location = new System.Drawing.Point(89, 20);
            this.combInptuVar.Name = "combInptuVar";
            this.combInptuVar.Size = new System.Drawing.Size(172, 20);
            this.combInptuVar.TabIndex = 82;
            this.combInptuVar.SelectedIndexChanged += new System.EventHandler(this.combInptuVar_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 120);
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
            this.textBoxTaoRCorrLength.Text = "8";
            this.textBoxTaoRCorrLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.textBoxVarLength.Text = "15";
            this.textBoxVarLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnIdentifyTaoRPara
            // 
            this.btnIdentifyTaoRPara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnIdentifyTaoRPara.Location = new System.Drawing.Point(148, 182);
            this.btnIdentifyTaoRPara.Name = "btnIdentifyTaoRPara";
            this.btnIdentifyTaoRPara.Size = new System.Drawing.Size(123, 26);
            this.btnIdentifyTaoRPara.TabIndex = 13;
            this.btnIdentifyTaoRPara.Text = "辨识动态延时参数";
            this.btnIdentifyTaoRPara.UseVisualStyleBackColor = false;
            this.btnIdentifyTaoRPara.Click += new System.EventHandler(this.btnIdentifyTaoRPara_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 151);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "动态延时最大相关步数(n2)：";
            // 
            // btnLoadOriginalDt
            // 
            this.btnLoadOriginalDt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadOriginalDt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnLoadOriginalDt.Location = new System.Drawing.Point(816, 78);
            this.btnLoadOriginalDt.Name = "btnLoadOriginalDt";
            this.btnLoadOriginalDt.Size = new System.Drawing.Size(106, 49);
            this.btnLoadOriginalDt.TabIndex = 71;
            this.btnLoadOriginalDt.Text = "载入原始数据表";
            this.btnLoadOriginalDt.UseVisualStyleBackColor = false;
            this.btnLoadOriginalDt.Click += new System.EventHandler(this.btnLoadOriginalDt_Click);
            // 
            // labelInputVar
            // 
            this.labelInputVar.AutoSize = true;
            this.labelInputVar.Location = new System.Drawing.Point(83, 115);
            this.labelInputVar.Name = "labelInputVar";
            this.labelInputVar.Size = new System.Drawing.Size(11, 12);
            this.labelInputVar.TabIndex = 76;
            this.labelInputVar.Text = "_";
            this.labelInputVar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 75;
            this.label5.Text = "输入变量：";
            // 
            // labelOriginalDt
            // 
            this.labelOriginalDt.AutoSize = true;
            this.labelOriginalDt.Location = new System.Drawing.Point(113, 62);
            this.labelOriginalDt.Name = "labelOriginalDt";
            this.labelOriginalDt.Size = new System.Drawing.Size(11, 12);
            this.labelOriginalDt.TabIndex = 74;
            this.labelOriginalDt.Text = "_";
            this.labelOriginalDt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnChose);
            this.groupBox4.Controls.Add(this.combChosePar);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.labelTaoR);
            this.groupBox4.Controls.Add(this.labelTaoD);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(651, 416);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(281, 122);
            this.groupBox4.TabIndex = 72;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "延时参数辨识结果";
            // 
            // btnChose
            // 
            this.btnChose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnChose.Location = new System.Drawing.Point(191, 79);
            this.btnChose.Name = "btnChose";
            this.btnChose.Size = new System.Drawing.Size(68, 23);
            this.btnChose.TabIndex = 40;
            this.btnChose.Text = "确认选择";
            this.btnChose.UseVisualStyleBackColor = false;
            this.btnChose.Click += new System.EventHandler(this.btnChose_Click);
            // 
            // combChosePar
            // 
            this.combChosePar.FormattingEnabled = true;
            this.combChosePar.Location = new System.Drawing.Point(110, 78);
            this.combChosePar.Name = "combChosePar";
            this.combChosePar.Size = new System.Drawing.Size(53, 20);
            this.combChosePar.TabIndex = 39;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 81);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 12);
            this.label12.TabIndex = 38;
            this.label12.Text = "选择辨识结果:";
            // 
            // labelTaoR
            // 
            this.labelTaoR.AutoSize = true;
            this.labelTaoR.Location = new System.Drawing.Point(108, 54);
            this.labelTaoR.Name = "labelTaoR";
            this.labelTaoR.Size = new System.Drawing.Size(11, 12);
            this.labelTaoR.TabIndex = 36;
            this.labelTaoR.Text = "_";
            // 
            // labelTaoD
            // 
            this.labelTaoD.AutoSize = true;
            this.labelTaoD.Location = new System.Drawing.Point(108, 26);
            this.labelTaoD.Name = "labelTaoD";
            this.labelTaoD.Size = new System.Drawing.Size(11, 12);
            this.labelTaoD.TabIndex = 35;
            this.labelTaoD.Text = "_";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 33;
            this.label9.Text = "静态响应延时:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "动态响应延时:";
            // 
            // btnSavePra
            // 
            this.btnSavePra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSavePra.Location = new System.Drawing.Point(822, 552);
            this.btnSavePra.Name = "btnSavePra";
            this.btnSavePra.Size = new System.Drawing.Size(100, 33);
            this.btnSavePra.TabIndex = 37;
            this.btnSavePra.Text = "保存辨识结果";
            this.btnSavePra.UseVisualStyleBackColor = false;
            this.btnSavePra.Click += new System.EventHandler(this.btnSavePra_Click);
            // 
            // btnCreatNewSheet
            // 
            this.btnCreatNewSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreatNewSheet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCreatNewSheet.Location = new System.Drawing.Point(714, 552);
            this.btnCreatNewSheet.Name = "btnCreatNewSheet";
            this.btnCreatNewSheet.Size = new System.Drawing.Size(100, 33);
            this.btnCreatNewSheet.TabIndex = 28;
            this.btnCreatNewSheet.Text = "生成新数据表";
            this.btnCreatNewSheet.UseVisualStyleBackColor = false;
            this.btnCreatNewSheet.Visible = false;
            this.btnCreatNewSheet.Click += new System.EventHandler(this.btnCreatNewSheet_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.chart1);
            this.groupBox2.Location = new System.Drawing.Point(14, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(631, 447);
            this.groupBox2.TabIndex = 80;
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
            this.chart1.Location = new System.Drawing.Point(14, 17);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(597, 413);
            this.chart1.TabIndex = 80;
            this.chart1.Text = "chart1";
            // 
            // labeldt
            // 
            this.labeldt.AutoSize = true;
            this.labeldt.Location = new System.Drawing.Point(18, 62);
            this.labeldt.Name = "labeldt";
            this.labeldt.Size = new System.Drawing.Size(77, 12);
            this.labeldt.TabIndex = 73;
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
            this.labeltitle.Location = new System.Drawing.Point(174, 9);
            this.labeltitle.Name = "labeltitle";
            this.labeltitle.Size = new System.Drawing.Size(619, 46);
            this.labeltitle.TabIndex = 79;
            this.labeltitle.Text = "生料细度延时参数辨识系统（T）";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(778, 161);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 12);
            this.label11.TabIndex = 86;
            this.label11.Text = "或";
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
            // FormIndentifyDelay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 612);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLoadTParModel);
            this.Controls.Add(this.labelTParpath);
            this.Controls.Add(this.btnSavePra);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnNewTParModel);
            this.Controls.Add(this.labelOutputVar);
            this.Controls.Add(this.btnCreatNewSheet);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadOriginalDt);
            this.Controls.Add(this.labelInputVar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelOriginalDt);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.labeldt);
            this.Controls.Add(this.labeltitle);
            this.Controls.Add(this.label11);
            this.Name = "FormIndentifyDelay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormIndentifyDelay";
            this.Load += new System.EventHandler(this.FormIndentifyDelay_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIdentifyTaoDPara;
        private System.Windows.Forms.Button btnLoadTParModel;
        private System.Windows.Forms.Label labelTParpath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnNewTParModel;
        private System.Windows.Forms.Label labelOutputVar;
        private System.Windows.Forms.TextBox textBoxTaoDCorrLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combOutputVar;
        private System.Windows.Forms.ComboBox combInptuVar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTaoRCorrLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxVarLength;
        private System.Windows.Forms.Button btnIdentifyTaoRPara;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLoadOriginalDt;
        private System.Windows.Forms.Label labelInputVar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelOriginalDt;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCreatNewSheet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label labeldt;
        private System.Windows.Forms.Label labeltitle;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnSavePra;
        private System.Windows.Forms.Label labelTaoR;
        private System.Windows.Forms.Label labelTaoD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox combChosePar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnChose;
        private System.Windows.Forms.Button btnExit;
    }
}