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
    public struct DataSheet1
    {
        public string[] CVname;
        public string[] MVname;
        public int InputNum;
        public int OutputNum;
        public int TagNum;
        public int RowNum;
        public int TrainDataNum;
        public int TestDataNum;
        public double[,] TrainInputValue;
        public double[,] TrainOutputValue;
        public double[,] TestInputValue;
        public double[,] TestOutputValue;

        public void datasheet1(int traindatanum, int testdatanum, int inputNum, int outputNum)
        {

            TrainDataNum = traindatanum;
            TestDataNum = testdatanum;
            InputNum = inputNum;
            OutputNum = outputNum;
            TrainInputValue = new double[inputNum, TrainDataNum];
            TrainOutputValue = new double[outputNum, TrainDataNum];
            TestInputValue = new double[inputNum, TestDataNum];
            TestOutputValue = new double[outputNum, TestDataNum];
            MVname = new string[inputNum];
            CVname = new string[outputNum];
        }
    }


    public partial class Form4 : Form
    {

        public Form4()
        {
            InitializeComponent();
            // this.WindowState = FormWindowState.Maximized;
        }


        static string SSmodelPath = "E:";
        public static string DataSheetPath = "E:";
        private void LoadDataSheet_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = DataSheetPath;
            openFileDialog1.Filter = "数据表文件(*.datasheet)|*.datasheet|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                DataSheetLabel.Text = S;
                DataSheetPath = filename;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string str2 = Environment.CurrentDirectory;
            DataSheetPath = str2;
            SSmodelPath = str2;
        }

        public string[] CvName;
        public string[] MvName;
        public DataSheet1 LoadData(string filename, int trainNum, int testNum)
        {
            int tagnum;
            int rownum;
            //int trainNum;
            //int testNum;
            int inputNum;
            int ouputNum;
            string[] s1;

            StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
            String line;

            line = sr.ReadLine();
            s1 = line.Trim().Split(' ');
            tagnum = Convert.ToInt16(s1[3]);
            rownum = Convert.ToInt16(s1[5]);
            inputNum = Convert.ToInt16(s1[7]);
            ouputNum = Convert.ToInt16(s1[9]);

            int i = 0;
            int rownumsum = 0;

            double[,] inputVal = new double[inputNum, rownum];
            double[,] outputVal = new double[ouputNum, rownum];
            //string[] CvName = new string[ouputNum];
            //string[] MvName = new string[inputNum];

            CvName = new string[ouputNum];
            MvName = new string[inputNum];

            while ((line = sr.ReadLine()) != null)
            {
                s1 = line.Trim().Split(' ');

                //if (i == 0)
                //    datasheet.nameFlag[j] = s1[j];
                if (i == 1)
                    for (int j = 0; j < tagnum; j++)
                        if (j < inputNum)
                            MvName[j] = s1[j];
                        else
                            CvName[j - inputNum] = s1[j];

                if (i > 1)
                {
                    if (Convert.ToInt16(s1[1]) == 1)
                    {
                        rownumsum++;
                        for (int j = 0; j < tagnum; j++)
                        {

                            if (i > 1)
                            {
                                if (j < inputNum)
                                    inputVal[j, rownumsum - 1] = Convert.ToDouble(s1[2 * j]);
                                else
                                    outputVal[j - inputNum, rownumsum - 1] = Convert.ToDouble(s1[2 * j]);
                            }

                        }
                    }

                }
                i++;
            }
            sr.Close();

            rownum = rownumsum;
            trainNum = (int)(rownumsum * trainNum / 100 + 0.5);//+0.5 四舍五入取整
            testNum = (int)(rownumsum * testNum / 100 + 0.5);

            DataSheet1 datasheet1 = new DataSheet1();
            datasheet1.datasheet1(trainNum, testNum, inputNum, ouputNum);

            datasheet1.MVname = MvName;
            datasheet1.CVname = CvName;

            for (int k = 0; k < trainNum + testNum; k++)
            {
                if (k < trainNum)
                    for (int j = 0; j < tagnum; j++)
                    {
                        if (j < inputNum)
                            datasheet1.TrainInputValue[j, k] = inputVal[j, k];
                        else
                            datasheet1.TrainOutputValue[j - inputNum, k] = outputVal[j - inputNum, k];
                    }
                else
                    for (int j = 0; j < tagnum; j++)
                    {
                        if (j < inputNum)
                            datasheet1.TestInputValue[j, k - trainNum] = inputVal[j, k];
                        else
                            datasheet1.TestOutputValue[j - inputNum, k - trainNum] = outputVal[j - inputNum, k];
                    }
            }

            return datasheet1;
        }

        public LSSVM LsSvm = new LSSVM();

        private void NewModel_Click(object sender, EventArgs e)
        {
            if (DataSheetLabel.Text == " ")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            saveFileDialog1.InitialDirectory = SSmodelPath;
            saveFileDialog1.Filter = "数据表文件(*.LSSVMSSmodel)|*.LSSVMSSmodel|所有文件|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                SSmodelPath = filename;
                string S = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                ModelLabel.Text = S;
                SSmodelPath = filename;

                ModelInit();
                SaveModelFun();
            }
            else
                return;
        }

        public int ChangeCDetaFlag = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (ModelLabel.Text == " ")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }

            ChangeCDeta();

            int trainNum = LsSvm.sampleNum;
            int testNum = LsSvm.TestSampleNum;
            int sum = trainNum + testNum;
            double TrainPre = Math.Round(100 * Convert.ToDouble(trainNum) / Convert.ToDouble(sum), 0);
            double TestPre = Math.Round(100 * Convert.ToDouble(testNum) / Convert.ToDouble(sum), 0);
            double TrainPreText = Math.Round(Convert.ToDouble(TrainDataPre.Text), 0);
            double TestPreText = Math.Round(Convert.ToDouble(TestDataPre.Text), 0);
            if (TrainPreText + TestPreText != 100)
            {
                MessageBox.Show("训练数据和测试数据总和不是100%！");
                return;
            }
            ChangeCDetaFlag = 1;
            for (int i = 0; i < LsSvm.outNum; i++)
            {
                if (LsSvm.C[i] == 0 || LsSvm.Deta[i] == 0)
                    ChangeCDetaFlag = 0;
            }
            MessageBox.Show("参数修改成功！");
        }
        private void ChangeCDeta()
        {
            for (int i = 0; i < LsSvm.outNum; i++)
            {
                if (comboBox2.Text == CvName[i])
                {
                    LsSvm.C[i] = Convert.ToDouble(CText.Text);
                    LsSvm.Deta[i] = Convert.ToDouble(DetaText.Text);
                    CText.Text = Convert.ToString(LsSvm.C[i]);
                    DetaText.Text = Convert.ToString(LsSvm.Deta[i]);
                }
            }
        }
        private void ModelInit()
        {
            DataSheet1 datasheet1 = new DataSheet1();
            datasheet1 = LoadData(DataSheetPath, Convert.ToInt32(TrainDataPre.Text), Convert.ToInt32(TestDataPre.Text));
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Text = datasheet1.CVname[0];
            comboBox2.Text = datasheet1.CVname[0];
            for (int j = 0; j < datasheet1.OutputNum; j++)
            {
                comboBox1.Items.Add(datasheet1.CVname[j]);//增加comboBox1中的下拉菜单中的内容
                comboBox2.Items.Add(datasheet1.CVname[j]);
            }

            MvNameLabel.Text = "";
            for (int i = 0; i < datasheet1.InputNum; i++)
                MvNameLabel.Text += "MV" + Convert.ToString(i + 1) + ": " + datasheet1.MVname[i] + "  ";
            CvNameLabel.Text = "";
            for (int i = 0; i < datasheet1.OutputNum; i++)
                CvNameLabel.Text += "CV" + Convert.ToString(i + 1) + ": " + datasheet1.CVname[i] + "  ";


            LsSvm.InitiLSSVM(datasheet1.TrainInputValue, datasheet1.TrainOutputValue, datasheet1.TestInputValue, datasheet1.TestOutputValue);

            ChangeCDeta();
            InputNumText.Text = Convert.ToString(datasheet1.InputNum);
            OutputNumText.Text = Convert.ToString(datasheet1.OutputNum);
        }

        private void LoadModel_Click(object sender, EventArgs e)
        {
            string filename;
            string[] s1;
            openFileDialog1.InitialDirectory = SSmodelPath;
            openFileDialog1.Filter = "模型文件(*.LSSVMSSmodel)|*.LSSVMSSmodel|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                SSmodelPath = filename;

                StreamReader sr = new StreamReader(SSmodelPath, Encoding.GetEncoding("gb2312"));
                String line;
                line = sr.ReadToEnd();
                s1 = line.Trim().Split('\n');
                if (DataSheetLabel.Text != s1[0].Trim().Split(' ')[5])
                {
                    MessageBox.Show("数据表文件不存在,请先导入名称为“" + s1[0].Trim().Split(' ')[5] + "”的数据表！");
                    return;
                }
                LsSvm.inNum = Convert.ToInt16(s1[1].Trim().Split(' ')[1]);
                //bpnet.hideNum = Convert.ToInt16(s1[1].Trim().Split(' ')[3]);
                LsSvm.outNum = Convert.ToInt16(s1[1].Trim().Split(' ')[3]);
                LsSvm.sampleNum = Convert.ToInt16(s1[1].Trim().Split(' ')[5]);
                ModelInit();
                LsSvm.LoadPar();
                //bpnet.LoadPar();

                //读取sampleNum
                //LsSvm.sampleNum = Convert.ToInt16(s1[18 + LsSvm.outNum].Trim());

                //读取a
                for (int i = 0; i < LsSvm.outNum; i++)
                    for (int j = 0; j < LsSvm.sampleNum; j++)
                        LsSvm.a[i, j] = Convert.ToDouble(s1[3 + i].Trim().Split(' ')[j]);
                //读取b
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.b[i] = Convert.ToDouble(s1[4 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取训练参数C
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.C[i] = Convert.ToDouble(s1[6 + LsSvm.outNum].Trim().Split(' ')[i]);
                //读取训练参数Deta
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.Deta[i] = Convert.ToDouble(s1[8 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取All_in_rate不用loadmodel就有值
                for (int i = 0; i < LsSvm.inNum; i++)
                    LsSvm.All_in_rata[i] = Convert.ToDouble(s1[10 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取All_out_rate不用loadmodel就有值
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.All_out_rata[i] = Convert.ToDouble(s1[12 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取AllDataInputMin不用loadmodel就有值
                for (int i = 0; i < LsSvm.inNum; i++)
                    LsSvm.AllDataInputMin[i] = Convert.ToDouble(s1[14 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取AllDataOutputMin不用loadmodel就有值
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.AllDataOutputMin[i] = Convert.ToDouble(s1[16 + LsSvm.outNum].Trim().Split(' ')[i]);

                //读取x
                for (int i = 0; i < LsSvm.inNum; i++)
                    for (int j = 0; j < LsSvm.sampleNum; j++)
                        LsSvm.x[i, j] = Convert.ToDouble(s1[18 + LsSvm.outNum + i].Trim().Split(' ')[j]);

                sr.Close();

                ModelLabel.Text = S;
                ChangeCDetaFlag = 1;
            }
        }

        private void SaveModelFun()
        {
            FileStream fs = new FileStream(SSmodelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(SSmodelPath);

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);

            sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + DataSheetLabel.Text);
            sw.WriteLine();

            sw.Write("输入变量数: " + Convert.ToString(LsSvm.inNum) + " 输出变量数: " + Convert.ToString(LsSvm.outNum) + " 训练数据数: " + Convert.ToString(LsSvm.sampleNum));
            sw.WriteLine();


            //sw.Write("样本训练数据大小sampleNum:");
            //sw.WriteLine();
            //sw.Write(Convert.ToString(LsSvm.sampleNum) + " ");
            //sw.WriteLine();

            sw.Write("参数a:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
            {
                for (int j = 0; j < LsSvm.sampleNum; j++)
                    sw.Write(Convert.ToString(LsSvm.a[i, j]) + " ");
                sw.WriteLine();
            }

            sw.Write("参数b:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
                sw.Write(Convert.ToString(LsSvm.b[i]) + " ");
            sw.WriteLine();

            sw.Write("训练参数C:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
                sw.Write(Convert.ToString(LsSvm.C[i]) + " ");
            sw.WriteLine();

            sw.Write("训练参数deta:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
                sw.Write(Convert.ToString(LsSvm.Deta[i]) + " ");
            sw.WriteLine();

            sw.Write("输入数据归一化比例系数All_in_rate:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.inNum; i++)
                sw.Write(Convert.ToString(LsSvm.All_in_rata[i]) + " ");
            sw.WriteLine();

            sw.Write("输出数据归一化比例系数All_out_rate:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
                sw.Write(Convert.ToString(LsSvm.All_out_rata[i]) + " ");
            sw.WriteLine();

            sw.Write("样本输入数据最小值AllDataInputMin:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.inNum; i++)
                sw.Write(Convert.ToString(LsSvm.AllDataInputMin[i]) + " ");
            sw.WriteLine();

            sw.Write("样本输出数据最小值AllDataOutputMin:");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.outNum; i++)
                sw.Write(Convert.ToString(LsSvm.AllDataOutputMin[i]) + " ");
            sw.WriteLine();

            sw.Write("训练样本归一化后输入数据x：");
            sw.WriteLine();
            for (int i = 0; i < LsSvm.inNum; i++)
            {
                for (int j = 0; j < LsSvm.sampleNum; j++)
                {
                    sw.Write(Convert.ToString(LsSvm.x[i, j]) + " ");
                }
                sw.WriteLine();
            }

            sw.Flush();
            sw.Close();
            MessageBox.Show("稳态模型保存成功！");
        }

        public int Train_notFlag = 0;
        private void TrainButton_Click(object sender, EventArgs e)
        {
            if (DataSheetLabel.Text == " ")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (ModelLabel.Text == " ")
            {
                MessageBox.Show("模型不存在，请先导入模型！");
                return;
            }
            if (ChangeCDetaFlag == 0)
            {
                MessageBox.Show("请先修改各个输出变量训练参数！");
                return;
            }
            LsSvm.LSSVMTrain();
            Train_notFlag = 1;
            MessageBox.Show("模型训练完成！");

        }

        private void TestModel_Click(object sender, EventArgs e)
        {
            if (DataSheetLabel.Text == " ")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (ModelLabel.Text == " ")
            {
                MessageBox.Show("模型不存在，请先导入模型！");
                return;
            }
            if (Train_notFlag == 1)
            {
                //测试模型

                chart2.ChartAreas.Clear();
                chart2.Series.Clear();
                chart2.ChartAreas.Add("测试");
                chart2.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart2.Series.Add("测试");
                chart2.Series.Add("原始");
                chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择要测试的曲线！");
                    return;
                }

                int ChartFlag = 0;
                for (int i = 0; i < LsSvm.outNum; i++)
                    if (CvName[i] == comboBox1.Text)
                        ChartFlag = i;
                //训练数据显示
                for (int k = 0; k < LsSvm.sampleNum; k++)
                {
                    chart2.Series[0].Points.AddXY(k + 1, LsSvm.yc[ChartFlag, k]);
                    chart2.Series[1].Points.AddXY(k + 1, LsSvm.yd[ChartFlag, k]);
                }
                //测试数据显示
                for (int m = 0; m < LsSvm.TestSampleNum; m++)
                {
                    chart2.Series[0].Points.AddXY(m + 1 + LsSvm.sampleNum, LsSvm.Testyc[ChartFlag, m]);
                    chart2.Series[1].Points.AddXY(m + 1 + LsSvm.sampleNum, LsSvm.Testy[ChartFlag, m]);
                }

                DataSheet1 datasheet1 = new DataSheet1();
                datasheet1 = LoadData(DataSheetPath, 100, 0);

                chart1.ChartAreas.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Add("测试");
                chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.Series.Add("测试");
                chart1.Series.Add("原始");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                for (int i = 0; i < LsSvm.sampleNum + LsSvm.TestSampleNum; i++)
                {
                    chart1.Series[0].Points.AddXY(i + 1, LsSvm.fgyh_yc[ChartFlag, i]);
                    chart1.Series[1].Points.AddXY(i + 1, datasheet1.TrainOutputValue[ChartFlag, i]);
                }

                //double[] uu = new double[datasheet1.InputNum];
                //double[] aaa = new double[LsSvm.sampleNum];
                //double bbb;
                //double ycc;
                //double detar;
                //double[,] yy = new double[datasheet1.OutputNum, datasheet1.TrainDataNum];
                //for (int i = 0; i < datasheet1.OutputNum; i++)
                //{
                //    for (int j = 0; j < LsSvm.sampleNum; j++)
                //        aaa[j] = LsSvm.a[i, j];
                //    bbb = LsSvm.b[i];
                //    detar = LsSvm.Deta[i];
                //    for (int r = 0; r < datasheet1.TrainDataNum; r++)
                //    {
                //        for (int h = 0; h < datasheet1.InputNum; h++)
                //        {
                //            uu[h] = datasheet1.TrainInputValue[h, r];   
                //        }
                //        ycc = LsSvm.lssvm(uu, LsSvm.TrainInData, aaa, bbb, detar);
                //        yy[i, r] = ycc;

                //    }
                //}
                //for (int i = 0; i < datasheet1.TrainDataNum; i++)
                //{
                //    chart1.Series[0].Points.AddXY(i + 1, yy[ChartFlag,i]);
                //    chart1.Series[1].Points.AddXY(i + 1, datasheet1.TrainOutputValue[ChartFlag,i]);
                //}

                chart2.ResetAutoValues();
                chart2.Invalidate();

                chart1.ResetAutoValues();
                chart1.Invalidate();


            }
            else
            {
                MessageBox.Show("请先训练模型！");
                return;
            }
        }

        private void SaveModel_Click(object sender, EventArgs e)
        {
            if (DataSheetLabel.Text == " ")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (ModelLabel.Text == " ")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }

            if (File.Exists(SSmodelPath))
            {
                File.Delete(SSmodelPath);
                SaveModelFun();
            }
            else
            {
                saveFileDialog1.InitialDirectory = SSmodelPath;
                saveFileDialog1.Filter = "模型文件(*.LSSVMSSmodel)|*.LSSVMSSmodel|所有文件|*.*";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    SSmodelPath = filename;

                    if (File.Exists(SSmodelPath))
                        File.Delete(SSmodelPath);

                    SaveModelFun();
                }
            }
            //// 数据测试辨识效果
            // double[] uu = new double[2];
            // uu[0] = 10;
            // uu[1] = 650;
            // double[]uuu=new double[2];
            // uuu[0]=(uu[0]-LsSvm.AllDataInputMin[0])/LsSvm.All_in_rata[0];
            // uuu[1]=(uu[1]-LsSvm.AllDataInputMin[1])/LsSvm.All_in_rata[1];
            // double []aaa=new double[LsSvm.sampleNum];
            // double bbb;
            // double detar;
            // double[] yy = new double[LsSvm.outNum];
            // double[] fgyhyy = new double[LsSvm.outNum];
            // for(int j=0;j<LsSvm.outNum;j++)
            // {
            //     for(int i=0;i<LsSvm.sampleNum;i++)
            //          {
            //            aaa[i]=LsSvm.a[j,i];
            //     }
            //     bbb=LsSvm.b[j];
            //     detar=LsSvm.Deta[j];
            //     double yccc = LsSvm.lssvm(uuu, LsSvm.x, aaa, bbb, detar);
            //     yy[j] = yccc;
            //     fgyhyy[j] = yy[j] * LsSvm.All_out_rata[j] + LsSvm.AllDataOutputMin[j];
            // }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParLabel.Text = comboBox2.SelectedItem + " 参数";
            CText.Text = Convert.ToString(LsSvm.C[comboBox2.SelectedIndex]);
            DetaText.Text = Convert.ToString(LsSvm.Deta[comboBox2.SelectedIndex]);
        }

        private void AnalyButton_Click(object sender, EventArgs e)
        {
            if (DataSheetLabel.Text == " ")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (ModelLabel.Text == " ")
            {
                MessageBox.Show("模型不存在，请先导入模型！");
                return;
            }
            Form5 f5 = new Form5(this);
            f5.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
