using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
namespace SSmodel
{
    public partial class FormFrecast : Form
    {
        private struct DataSheetTao
        {

            public string[] CVname;
            public string[] MVname;
            public int RowNum;
            public int InputNum;
            public int OutputNum;
            //public int TagNum;
            //public int TrainDataNum;
            //public int TestDataNum;
            public double[,] InputData;
            public double[,] OutputData;

            public int[,] HangShu;

            public double[] TaoD;//任何输入变量都会与输出变量之间存在一个静态响应延时TaoD
            public double[] TaoR;//任何输入变量都会与输出变量之间存在一个动态响应延时TaoR



            public void datasheet(int inputNum, int outputNum, int dataLength, string[] cvname, string[] mvname)
            {
                //TrainDataNum = traindatanum;
                //TestDataNum = testdatanum;
                InputNum = inputNum;
                OutputNum = outputNum;
                RowNum = dataLength;
                InputData = new double[inputNum, RowNum];
                // OutputData = new double[outputNum, RowNum];
                MVname = new string[inputNum];
                CVname = new string[outputNum];
                CVname = cvname;
                MVname = mvname;
                TaoD = new double[inputNum * outputNum];//先第一个输出对应的所有的TaoD
                TaoR = new double[inputNum * outputNum];//先第一个输出对应的所有的TaoR
            }
            public void ShiliOutputDataAndHangShu(int outputnum, int datalength)
            {
                OutputData = new double[outputnum, datalength];
                HangShu = new int[outputnum, datalength];
            }
        }

        public FormFrecast()
        {
            InitializeComponent();
        }

        private static double[,] US;//T-LSSVR的训练模型的 训练数据
        private static double[,] YS;

        static string TLSSVRModelPath = "E:";
        private static string DataSheetPath = "E:";
        static string TParModelPath = "E:";
        // public double[,] varData;
        //public int inputnum;
        //public int outputnum;//与DT datasheet表格里边一致，先入 后出
        private int dataLength;



        //从FormIndentifyDelay中粘贴过来的
        private int inputNum;
        private int outputNum;//与DST datasheet表格里边一致，先入 后出
        private double[,] varDataX;//所有的变量中的数据 样本个数*变量个数  用于chart曲线中原始曲线绘制
        private double[,] varDataY;//所有的变量中的数据 样本个数*变量个数  用于chart曲线中原始曲线绘制

        private int inputBili = 0, outputBili = 0;
        private LSSVM LsSvm = new LSSVM();


        private void FormFrecast_Load(object sender, EventArgs e)
        {
            this.Text = "Advacon System One";
            this.WindowState = FormWindowState.Normal;//.Maximized;            
            string str = Environment.CurrentDirectory;
            DataSheetPath = str;
            TLSSVRModelPath = str;
        }

        private void btnLoadNewDt_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = DataSheetPath;

            openFileDialog1.Title = "打开辅助变量的原始数据表";
            openFileDialog1.Filter = "数据表文件(*.OriginalDataX)|*.OriginalDataX|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                labelnewDt.Text = S;
                DataSheetPath = filename;
                //当前路径下的数据对应的界面初始化 输入变量初始化  同时 模型初始化
                InterfaceInitX(filename);//*************************************待修改
                MessageBox.Show("辅助变量的原始数据加载成功！数据长度：" + (dataLength - 3));
            }
            else
            {
                return;
            }
            openFileDialog1.Title = "打开生料细度的原始数据表";
            openFileDialog1.Filter = "数据表文件(*.OriginalDataY)|*.OriginalDataY|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                labelnewDt.Text += "\n" + S;

                DataSheetPath = filename;
                //当前路径下的数据对应的界面初始化  输出变量初始化 同时 模型初始化
                InterfaceInitY(filename);//*************************************待修改
                MessageBox.Show("生料细度的原始数据加载成功！数据长度：" + (dataLength - 4));
            }

        }
        static DataSheetTao DST = new DataSheetTao();
        private void InterfaceInitX(string file)
        {
            string fs = file;
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadToEnd();
            string[] s1 = line.Trim().Split('\n');
            dataLength = s1.Length;             //dataLength指总的数据长度，dataLength-3为变量的数据长度
            inputNum = Convert.ToInt16(s1[0].Trim().Split(' ')[7]);
            outputNum = Convert.ToInt16(s1[0].Trim().Split(' ')[9]);
            string[] mvname = new string[inputNum];
            string[] cvname = new string[outputNum];
            labelInputVar.Text = "";
            // combInptuVar.Items.Clear();
            for (int i = 0; i < inputNum; i++)
            {
                labelInputVar.Text += "MV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i] + " ";
                // combInptuVar.Items.Add(s1[2].Trim().Split(' ')[i]);
                //if (i == 0)
                //    combInptuVar.Text = s1[2].Trim().Split(' ')[i];
                mvname[i] = s1[2].Trim().Split(' ')[i];
            }

            for (int i = 0; i < outputNum; i++)
            {
                cvname[i] = s1[2].Trim().Split(' ')[i + inputNum];
            }

            //初始化DST中的 延时参数的数据
            DST.datasheet(inputNum, outputNum, dataLength - 3, cvname, mvname);
            varDataX = new double[dataLength - 3, inputNum + outputNum];
            for (int j = 0; j < dataLength - 3; j++)
            {
                for (int k = 0; k < inputNum; k++)
                {

                    varDataX[j, k] = Convert.ToDouble(s1[j + 3].Trim().Split('\t')[k]);

                    DST.InputData[k, j] = Convert.ToDouble(s1[j + 3].Trim().Split('\t')[k]);
                }
            }
            sr.Close();
            //chart1.Titles.Clear();
            //chart1.ChartAreas.Clear();
            //chart1.Series.Clear();
            //chart1.Titles.Add("原始数据曲线分布图");
            //ChartArea chartarea1 = new ChartArea();
            //chartarea1.Name = "area1";
            //chart1.ChartAreas.Add(chartarea1);
            ////chart1.ChartAreas.Add("area1");
            //chart1.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
            ////chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
            //var font = new Font("Trebuchet MS", 8);
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = font;
            //chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //chart1.ChartAreas[0].InnerPlotPosition.Height = 90;
            //chart1.ChartAreas[0].InnerPlotPosition.Width = 90;
            //chart1.ChartAreas[0].InnerPlotPosition.Y = 2;
            //chart1.ChartAreas[0].InnerPlotPosition.X = 10;
            //chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            //chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
            //ChartArea chartarea2 = new ChartArea();
            //chartarea2.Name = "area2";
            //chart1.ChartAreas.Add(chartarea2);
            ////chart1.ChartAreas.Add("area2");
            //chart1.ChartAreas[1].AxisY.LabelStyle.Format = "F2";
            ////chart1.ChartAreas[1].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
            //chart1.ChartAreas[1].AxisX.LabelStyle.Font = font;
            //chart1.ChartAreas[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            //chart1.ChartAreas[1].InnerPlotPosition.Height = 90;
            //chart1.ChartAreas[1].InnerPlotPosition.Width = 90;
            //chart1.ChartAreas[1].InnerPlotPosition.Y = 2;
            //chart1.ChartAreas[1].InnerPlotPosition.X = 10;
            //chart1.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
            //chart1.ChartAreas[1].AxisY.MajorGrid.Enabled = false;
            //chart1.ChartAreas[1].AxisX.IsStartedFromZero = false;

            //for (int i = 1; i < inputNum + 1; i++)
            //{
            //    Series _series = new Series();
            //    _series.ChartType = SeriesChartType.Line;
            //    _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输入曲线定义从“1”开始
            //    _series.ChartArea = chart1.ChartAreas[1].Name;
            //    chart1.Series.Add(_series); //加入Series
            //    chart1.Series[i.ToString()].LegendText = "MV" + (i) + ":" + s1[2].Trim().Split(' ')[i - 1];
            //    chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
            //    for (int j = 1; j <= dataLength - 3; j++)
            //    {
            //        chart1.Series[i.ToString()].Points.AddXY(j, varDataX[j - 1, i - 1]);
            //    }
            //}
            //chart1.DataBind();

        }

        private void InterfaceInitY(string file)
        {
            string fs = file;
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadToEnd();
            string[] s1 = line.Trim().Split('\n');
            dataLength = s1.Length;             //dataLength指总的数据长度，dataLength-3为变量的数据长度
            string[] cvname = new string[outputNum];
            labelOutputVar.Text = "";
            //combOutputVar.Items.Clear();
            for (int i = 0; i < outputNum; i++)
            {
                labelOutputVar.Text += "CV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i + inputNum] + " ";
                //  comboBox2.Items.Add(s1[2].Trim().Split(' ')[i + inputNum]);
                // if (i == 0)
                //      comboBox2.Text = s1[2].Trim().Split(' ')[i + inputNum];
                cvname[i] = s1[2].Trim().Split(' ')[i + inputNum];
            }
            //初始化DST中的 延时参数的数据
            //   DST.datasheet(inputNum, outputNum, dataLength - 3, cvname, mvname);
            DST.ShiliOutputDataAndHangShu(outputNum, dataLength - 4);

            varDataY = new double[dataLength - 4, outputNum];
            for (int j = 0; j < dataLength - 4; j++)
            {
                for (int k = 0; k < outputNum; k++)
                {
                    varDataY[j, k] = Convert.ToDouble(s1[j + 4].Trim().Split(' ')[2]);
                    DST.OutputData[k, j] = Convert.ToDouble(s1[j + 4].Trim().Split(' ')[2]);
                    DST.HangShu[k, j] = Convert.ToInt16(s1[j + 4].Trim().Split(' ')[3]);
                }
            }
            sr.Close();

            //for (int i = 1; i < outputNum + 1; i++)
            //{
            //    Series _series = new Series();
            //    _series.ChartType = SeriesChartType.Spline;
            //    _series.Name = (i + inputNum).ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
            //    _series.ChartArea = chart1.ChartAreas[0].Name;
            //    chart1.Series.Add(_series); //加入Series

            //    chart1.Series[(i + inputNum).ToString()].LegendText = "CV" + i + ":" + s1[2].Trim().Split(' ')[inputNum - 1 + i];
            //    chart1.Series[(i + inputNum).ToString()].XValueType = ChartValueType.Int32;
            //    for (int j = 1; j <= dataLength - 4; j++)
            //    {
            //        chart1.Series[(i + inputNum).ToString()].Points.AddXY(j, varDataY[j - 1, i - 1]);
            //    }

            //}
            //chart1.DataBind();
        }

        private void btnLoadTParModel_Click(object sender, EventArgs e)
        {
            if (labelnewDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            openFileDialog1.Filter = "数据表文件(*.TParModel)|*.TParModel|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                labelTParpath.Text = S;
                TParModelPath = filename;
                LoadTParModelFun();
            }
            else
            {
                return;
            }
            labelTaoD.Text = "";
            labelTaoR.Text = "";
            for (int j = 0; j < DST.OutputNum; j++)
            {
                for (int i = 0; i < DST.InputNum; i++)
                {
                    labelTaoD.Text += " " + (Int16)DST.TaoD[i];
                }
                if (j > 0)
                {
                    labelTaoD.Text += " |";
                }
            }
            for (int j = 0; j < DST.OutputNum; j++)
            {
                for (int i = 0; i < DST.InputNum; i++)
                {
                    labelTaoR.Text += " " + (Int16)DST.TaoR[i];
                }
                if (j > 0)
                {
                    labelTaoR.Text += " |";
                }
            }
            MessageBox.Show("时延参数加载完成！");
        }

        private void LoadTParModelFun()
        {
            string[] s1;
            StreamReader sr = new StreamReader(TParModelPath, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadToEnd();
            s1 = line.Trim().Split('\n');
            //if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            //{
            if (labelnewDt.Text != s1[0].Trim().Split(' ')[5] + "\n" + s1[0].Trim().Split(' ')[7])
            {
                MessageBox.Show("数据表文件不存在,请先导入名称为\n“" + s1[0].Trim().Split(' ')[5] + "\n" + s1[0].Trim().Split(' ')[7] + "”\n的数据表！");
                return;
            }
            else
            {
                for (int j = 0; j < DST.OutputNum; j++)
                {
                    for (int i = 0; i < DST.InputNum; i++)
                    {
                        string[] oneLine = s1[2 + j * (DST.InputNum + 2) + 2 + i].Trim().Split(' ');
                        DST.TaoD[DST.InputNum * j + i] = Convert.ToInt16(oneLine[0]);
                        DST.TaoR[DST.InputNum * j + i] = Convert.ToInt16(oneLine[1]);
                    }
                }
            }

            sr.Close();
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            if (labelnewDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("延时参数表表文件不存在，请先导入延时参数表！");
                return;
            }
            string filename;
            string[] s1;
            openFileDialog1.InitialDirectory = TLSSVRModelPath;
            //if (combModelType.Text == "LSSVR")
            //{//newdata
            //    openFileDialog1.Filter = "数据表文件(*.LssvrModel)|*.LssvrModel|所有文件|*.*";
            //}
            //else if (combModelType.Text == "T-LSSVR")
            //{
            openFileDialog1.Filter = "数据表文件(*.TlssvrModel)|*.TlssvrModel|所有文件|*.*";
            //}
            //else
            //{
            //    MessageBox.Show("请先选择训练模型的类型！");
            //    return;
            //}
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                TLSSVRModelPath = filename;

                StreamReader sr = new StreamReader(TLSSVRModelPath, Encoding.GetEncoding("gb2312"));
                String line;
                line = sr.ReadToEnd();
                s1 = line.Trim().Split('\n');
                //if (labelnewDt.Text != s1[0].Trim().Split(' ')[5])
                if (labelnewDt.Text != s1[0].Trim().Split(' ')[5] + "\n" + s1[0].Trim().Split(' ')[7])
                {
                    MessageBox.Show("数据表文件不存在,请先导入名称为\n“" + s1[0].Trim().Split(' ')[5] + "\n" + s1[0].Trim().Split(' ')[7] + "”\n的数据表！");
                    return;
                }
                LsSvm.inNum = Convert.ToInt16(s1[1].Trim().Split(' ')[1]);
                //bpnet.hideNum = Convert.ToInt16(s1[1].Trim().Split(' ')[3]);
                LsSvm.outNum = Convert.ToInt16(s1[1].Trim().Split(' ')[3]);
                LsSvm.sampleNum = Convert.ToInt16(s1[1].Trim().Split(' ')[5]);
                inputBili = Convert.ToInt16(s1[1].Trim().Split(' ')[7]);
                outputBili = Convert.ToInt16(s1[1].Trim().Split(' ')[9]);



                TrainDataInit();
                ModelInit();
                LsSvm.LoadPar();//定义各参数a b C Deta InputMin OutputMin in_rate out_rate All_in_rata All_out_rata AllDataInputMin AllDataOutputMin x
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
                // CText.Text = LsSvm.C[0].ToString();
                //读取训练参数Deta
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.Deta[i] = Convert.ToDouble(s1[8 + LsSvm.outNum].Trim().Split(' ')[i]);
                // DetaText.Text = LsSvm.Deta[0].ToString();
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

                labelModel.Text = S;
                //ChangeCDetaFlag = 1;
                MessageBox.Show("已加载模型参数文件！");
            }
        }

        private void TrainDataInit()
        {
            int[] HangShu24;
            double[,] X;
            int outputdataLength = DST.OutputData.GetLength(1);    // 细度列数 123
            HangShu24 = new int[outputdataLength * 24];           //2952
            X = new double[DST.InputNum, HangShu24.GetLength(0)];  //8*2952
            YS = new double[DST.OutputNum, outputdataLength];       //1*123

            int lengthTlssTnDa = 0;
            for (int n = 0; n < DST.TaoR.Length; n++)
            {
                lengthTlssTnDa += Convert.ToInt16(DST.TaoR[n]);
            }
            lengthTlssTnDa += 8;
            US = new double[outputdataLength, lengthTlssTnDa];//123行* 28（Taor和 +8）
            for (int i = 0; i < outputdataLength; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    HangShu24[i * 24 + j] = DST.HangShu[0, i] - (24 - 1 - j);
                }
            }
            for (int i = 0; i < DST.InputNum; i++)
            {
                for (int j = 0; j < HangShu24.GetLength(0); j++)//2952行
                {
                    int xuhao = 0;
                    xuhao = HangShu24[j];
                    X[i, j] = DST.InputData[i, xuhao - 1];//实际的序号应该从0开始  AllHisX  8*2952
                }
            }
            //double[,] ct;
            for (int j = 0; j < outputdataLength; j++)            //123行
            {
                int startUS = 0;
                for (int i = 0; i < DST.InputNum; i++)         //28列  8个变量的Taor得到28
                {
                    // int lg = Convert.ToInt16(DST.TaoR[i]);  //每个变量的taoR对应的长度
                    //ct = new double[1, lg + 1];//每个变量进行平移 再进行TaoR整合后的长度 1*Taor

                    if (i == 0)
                        startUS = 0;
                    else
                        startUS = startUS + 1 + Convert.ToInt16(DST.TaoR[i - 1]);

                    int oneOfTaoD = Convert.ToInt16(DST.TaoD[i]);//每个TaoD的长度
                    int oneOfTaoR = Convert.ToInt16(DST.TaoR[i]);//每个TaoD的长度
                    for (int k = 0; k < oneOfTaoR + 1; k++)
                    {
                        US[j, k + startUS] = X[i, 24 * (j + 1) - 1 - oneOfTaoD - oneOfTaoR + k];
                    }
                }
            }
            YS = DST.OutputData; // 1*123
        }

        public DataSheet1 LoadData(string filename, int trainNum, int testNum)
        {
            int tagnum;   //共有9个变量
            int rownum;   //共123行变量
            //int trainNum;
            //int testNum;
            int inputNum;
            int ouputNum;
            string[] s1;
            StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadLine();
            s1 = line.Trim().Split(' ');
            //tagnum = Convert.ToInt16(s1[3]);
            tagnum = US.GetLength(1) + YS.GetLength(0);
            rownum = Convert.ToInt16(s1[5]);
            //inputNum = Convert.ToInt16(s1[7]);
            //ouputNum = Convert.ToInt16(s1[9]);
            inputNum = US.GetLength(1);//28     US= 123*28
            ouputNum = YS.GetLength(0);//1
            int i = 0;
            int rownumsum = 0;
            double[,] inputVal = new double[inputNum, rownum];//28*123
            double[,] outputVal = new double[ouputNum, rownum];//1*123
            //string[] CvName = new string[ouputNum];
            //string[] MvName = new string[inputNum];
            CvName = new string[ouputNum];
            MvName = new string[inputNum];
            for (i = 0; i < inputNum; i++)
            {
                for (int j = 0; j < rownum; j++)
                    inputVal[i, j] = US[j, i];
            }
            outputVal = YS;

            CvName = DST.CVname;

            int start = 0;
            for (int j = 0; j < DST.InputNum; j++)
            {
                if (j == 0)
                {
                    start = 0;
                    for (i = 0; i <= Convert.ToInt16(DST.TaoR[j]); i++)
                    {
                        MvName[start + i] = DST.MVname[j] + (i + 1).ToString();
                    }
                }
                else
                {
                    start = start + 1 + Convert.ToInt16(DST.TaoR[j - 1]);
                    for (i = 0; i <= Convert.ToInt16(DST.TaoR[j]); i++)
                    {
                        MvName[start + i] = DST.MVname[j] + (i + 1).ToString();
                    }
                }
            }
            //while ((line = sr.ReadLine()) != null)
            //{
            //    s1 = line.Trim().Split(' ');

            //    //if (i == 0)
            //    //    datasheet.nameFlag[j] = s1[j];
            //    if (i == 1)
            //        for (int j = 0; j < tagnum; j++)
            //            if (j < inputNum)
            //                MvName[j] = s1[j];
            //            else
            //                CvName[j - inputNum] = s1[j];
            //    if (i > 1)
            //    {
            //        //if (Convert.ToInt16(s1[1]) == 1)
            //        //{
            //        rownumsum++;
            //        for (int j = 0; j < tagnum; j++)
            //        {
            //            if (j < inputNum)
            //                inputVal[j, rownumsum - 1] = Convert.ToDouble(s1[j]);
            //            else
            //                outputVal[j - inputNum, rownumsum - 1] = Convert.ToDouble(s1[j]);
            //        }
            //        //}
            //    }
            //    i++;
            //}
            sr.Close();

            //rownum = rownumsum;
            rownumsum = rownum;
            trainNum = (int)(rownumsum * trainNum / 100 + 0.5);//+0.5 四舍五入取整  实际是向下取整
            testNum = rownumsum - trainNum;//(int)(rownumsum * testNum / 100 + 0.5);
            DataSheet1 DS1 = new DataSheet1();
            DS1.datasheet1(trainNum, testNum, inputNum, ouputNum);

            DS1.MVname = MvName;
            DS1.CVname = CvName;

            for (int k = 0; k < trainNum + testNum; k++)
            {
                if (k < trainNum)
                    for (int j = 0; j < tagnum; j++)
                    {
                        if (j < inputNum)
                            DS1.TrainInputValue[j, k] = inputVal[j, k];
                        else
                            DS1.TrainOutputValue[j - inputNum, k] = outputVal[j - inputNum, k];
                    }
                else
                    for (int j = 0; j < tagnum; j++)
                    {
                        if (j < inputNum)
                            DS1.TestInputValue[j, k - trainNum] = inputVal[j, k];
                        else
                            DS1.TestOutputValue[j - inputNum, k - trainNum] = outputVal[j - inputNum, k];
                    }
            }

            return DS1;
        }

        private void ModelInit()
        {

            DataSheet1 datasheet1 = new DataSheet1();
            //datasheet1 = LoadData(DataSheetPath, Convert.ToInt32(TrainDataPre.Text), Convert.ToInt32(TestDataPre.Text));


            datasheet1 = LoadData(DataSheetPath, inputBili, outputBili);
            //  comboBox1.Items.Clear();
            // comboBox2.Items.Clear();
            // comboBox1.Text = datasheet1.CVname[0];
            // comboBox2.Text = datasheet1.CVname[0];
            //for (int j = 0; j < datasheet1.OutputNum; j++)
            //{
            //    comboBox1.Items.Add(datasheet1.CVname[j]);//增加comboBox1中的下拉菜单中的内容
            //    // comboBox2.Items.Add(datasheet1.CVname[j]);
            //}
            //MvNameLabel.Text = "";
            //for (int i = 0; i < datasheet1.InputNum; i++)
            //    MvNameLabel.Text += "MV" + Convert.ToString(i + 1) + ": " + datasheet1.MVname[i] + "  ";
            //CvNameLabel.Text = "";
            //for (int i = 0; i < datasheet1.OutputNum; i++)
            //    CvNameLabel.Text += "CV" + Convert.ToString(i + 1) + ": " + datasheet1.CVname[i] + "  ";

            LsSvm.InitiLSSVM(datasheet1.TrainInputValue, datasheet1.TrainOutputValue, datasheet1.TestInputValue, datasheet1.TestOutputValue);

            //ChangeCDeta();
            //InputNumText.Text = Convert.ToString(datasheet1.InputNum);
            //OutputNumText.Text = Convert.ToString(datasheet1.OutputNum);
        }


        private string[] CvName;
        private string[] MvName;


        private void btnTestModel_Click(object sender, EventArgs e)
        {
            if (labelnewDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            if (labelModel.Text == "_")
            {
                MessageBox.Show("模型不存在，请先导入模型！");
                return;
            }

            //  double[] abpar=   LsSvm.lssvmident()

            double[] ugtd = GetTestData();//28*1  u
            if (ugtd == null)
                return;

            double[] gyhu = LsSvm.gyh(ugtd, LsSvm.AllDataInputMax, LsSvm.AllDataInputMin);



            double[] yy = new double[1];//归一化后的yy

            yy = LsSvm.lssvmforecast(gyhu, LsSvm.x, LsSvm.a, LsSvm.b, LsSvm.Deta);

            double[] yForecast = LsSvm.fgyh(yy, LsSvm.AllDataOutputMax, LsSvm.AllDataOutputMin); //得到最新的历史的细度


            double num1 = Convert.ToDouble(yForecast[0]);
            string result1 = num1.ToString("#0.00"); //点后面几个0就保留几位
            TBCeShiXiDu.Text = result1;


        }
        private double[] GetTestData()
        {
            double[,] returnData, Uct;
            int lengthRowGdv = dataGridView1.Rows.Count;
            if (lengthRowGdv == 0)
            {
                MessageBox.Show("数据表没有数据！请连接数据库！");

                return null;
            }

            //计算 长度
            int lengthTlssTnDa = 0;
            for (int n = 0; n < DST.TaoR.Length; n++)
            {
                lengthTlssTnDa += Convert.ToInt16(DST.TaoR[n]);
            }
            lengthTlssTnDa += 8;
            Uct = new double[1, lengthTlssTnDa];//28*1

            //得到最后的24行数据 存放在X里
            double[,] X = new double[DST.InputNum, 24];//取前24个数据

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    string da = dataGridView1.Rows[lengthRowGdv - (24 - j)].Cells[i + 2].Value.ToString();
                    X[i, j] = Convert.ToDouble(da);
                }
            }
            int startUS = 0;
            for (int i = 0; i < DST.InputNum; i++)         //28列  8个变量的Taor得到28
            {
                if (i == 0)
                    startUS = 0;
                else
                    startUS = startUS + 1 + Convert.ToInt16(DST.TaoR[i - 1]);


                int oneOfTaoD = Convert.ToInt16(DST.TaoD[i]);//每个TaoD的长度
                int oneOfTaoR = Convert.ToInt16(DST.TaoR[i]);//每个TaoD的长度
                for (int k = 0; k < oneOfTaoR + 1; k++)
                {
                    Uct[0, k + startUS] = X[i, 24 * (0 + 1) - 1 - oneOfTaoD - oneOfTaoR + k];
                }
            }
            Matrix mt = new Matrix();
            //求转置
            returnData = mt.Tan(Uct);

            double[] rd = new double[lengthTlssTnDa];
            for (int i = 0; i < lengthTlssTnDa; i++)
            {
                rd[i] = returnData[i, 0];
            }

            return rd;
        }

        private static string lastRiqi, lastTime, nowRiqi, nowTime;

        private void btnConSql_Click(object sender, EventArgs e)
        {
            DataGridViewColumn colRiqi = new DataGridViewColumn();
            colRiqi.HeaderText = "主电机电流";
            colRiqi.Name = "Coln2";
            DataGridViewCell dgvCell = new DataGridViewTextBoxCell();//这里根据自己需要来定义不同模板。当前模板为“文本单元格”
            colRiqi.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(colRiqi);
            DataGridViewColumn colTine = new DataGridViewColumn();
            colTine.HeaderText = "主电机电流";
            colTine.Name = "Coln2";
            colTine.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(colTine);
            DataGridViewColumn col1 = new DataGridViewColumn();
            col1.HeaderText = "喂料量";
            col1.Name = "Coln1";
            col1.CellTemplate = dgvCell;//设置模板
            dataGridView1.Columns.Add(col1);
            DataGridViewColumn col2 = new DataGridViewColumn();
            col2.HeaderText = "主电机电流";
            col2.Name = "Coln2";
            col2.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col2);
            DataGridViewColumn col3 = new DataGridViewColumn();
            col3.HeaderText = "磨机差压";
            col3.Name = "Coln3";
            col3.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col3);
            DataGridViewColumn col4 = new DataGridViewColumn();
            col4.HeaderText = "入口温度";
            col4.Name = "Coln4";
            col4.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col4);
            DataGridViewColumn col5 = new DataGridViewColumn();
            col5.HeaderText = "出口温度";
            col5.Name = "Coln5";
            col5.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col5);
            DataGridViewColumn col6 = new DataGridViewColumn();
            col6.HeaderText = "斗提机电流";
            col6.Name = "Coln6";
            col6.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col6);
            DataGridViewColumn col7 = new DataGridViewColumn();
            col7.HeaderText = "选粉机转速";
            col7.Name = "Coln7";
            col7.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col7);
            DataGridViewColumn col8 = new DataGridViewColumn();
            col8.HeaderText = "循环风机电流";
            col8.Name = "Coln8";
            col8.CellTemplate = dgvCell;//设置模板            
            dataGridView1.Columns.Add(col8);

            //连接数据库
            string SqlPathFind = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=控制器数据库;Persist Security Info=True;User ID=sa;Password=123456;MAX Pool Size=512;Min Pool Size=1;Connection Lifetime=30";
            SQLConnect DA = new SQLConnect(SqlPathFind);
            if (DA.CheckExistsTable("生料细度辅助变量历史列表", SqlPathFind) == true)
            {
                TextBoxSqlYesNo.BackColor = System.Drawing.Color.LimeGreen;
                DataTable dtNow = new DataTable();
                dtNow.Rows.Clear();
                string nowDataExited = "select * from 生料细度辅助变量历史列表";
                dtNow = DA.ExeSQLdt(nowDataExited);
                lastRiqi = (string)dtNow.Rows[0][0];
                lastTime = (string)dtNow.Rows[0][1];
                timer1.Enabled = true;
                timer1.Interval = 5000;
            }
            else
            {
                TextBoxSqlYesNo.BackColor = System.Drawing.Color.Red;
                return;
            }
            ////先插入一段历史    0318原始数据.txt
            //string pathYuanShiData = Environment.CurrentDirectory;
            //pathYuanShiData += "\\0318原始数据.txt";
            //StreamReader sr = new StreamReader(pathYuanShiData, Encoding.GetEncoding("gb2312"));
            //String line;
            //line = sr.ReadToEnd();
            //string[] s1 = line.Trim().Split('\n');
            //dataLength = s1.Length;             //dataLength指总的数据长度，dataLength-3为变量的数据长度
            //string[] findData = new string[10];
            //for (int i = 0; i < dataLength - 1; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        findData[j] = s1[i + 1].Trim().Split('\t')[j];
            //    }
            //    string inr = "insert into 生料细度辅助变量历史列表(日期,时间,喂料量,主电机电流,磨机差压,入口温度,出口温度,斗提机电流,选粉机转速,循环风机电流) values('" + findData[0] + "','" + findData[1] + "','" + findData[2] + "','" + findData[3] + "','" + findData[4] + "','" + findData[5] + "','" + findData[6] + "','" + findData[7] + "','" + findData[8] + "','" + findData[9] + "')";
            //    DA.ExeSQL(inr);
            //}
            //读取这段历史            
            DataTable dt = new DataTable();
            dt.Rows.Clear();
            string DataExited = "select * from 生料细度辅助变量历史列表";
            dt = DA.ExeSQLdt(DataExited);
            DA.CloseCon();
            int n = dt.Rows.Count;
            string[] readData = new string[10];
            dataGridView1.Rows.Clear();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    readData[j] = (string)dt.Rows[i][j];
                    readData[j] = readData[j].Trim();
                }
                dataGridView1.Rows.Add(readData[0], readData[1], readData[2], readData[3], readData[4], readData[5], readData[6], readData[7], readData[8], readData[9]);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string SqlPathFind = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=控制器数据库;Persist Security Info=True;User ID=sa;Password=123456;MAX Pool Size=512;Min Pool Size=1;Connection Lifetime=30";
            SQLConnect DA = new SQLConnect(SqlPathFind);
            DataTable dtNow = new DataTable();
            dtNow.Rows.Clear();
            string nowDataExited = "select * from 生料细度辅助变量历史列表";
            dtNow = DA.ExeSQLdt(nowDataExited);
            nowRiqi = (string)dtNow.Rows[0][0];
            nowTime = (string)dtNow.Rows[0][1];
            DA.CloseCon();

            if (nowRiqi == lastRiqi && nowTime == lastTime)
            {
                textBoxSqlFreshOrNo.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                textBoxSqlFreshOrNo.BackColor = System.Drawing.Color.LimeGreen;
            }
            lastRiqi = nowRiqi;
            lastTime = nowTime;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Dispose();
            this.Close();
        }
    }
}
