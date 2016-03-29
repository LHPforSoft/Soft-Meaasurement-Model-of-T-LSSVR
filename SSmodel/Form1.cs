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
    public partial class Form1 : Form
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
                OutputData = new double[outputNum, RowNum];
                MVname = new string[inputNum];
                CVname = new string[outputNum];
                CVname = cvname;
                MVname = mvname;
                TaoD = new double[inputNum * outputNum];//先第一个输出对应的所有的TaoD
                TaoR = new double[inputNum * outputNum];//先第一个输出对应的所有的TaoR
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        public static string DataSheetPath = "E:";
        static string TParModelPath = "E:";

        public double[,] varData;//所有的变量中的数据
        public int inputNum;
        public int outputNum;//与datasheet表格里边一致，先入 后出
        public int dataLength;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Advacon System One";
            //this.WindowState = FormWindowState.Maximized;
            string str = Environment.CurrentDirectory;
            DataSheetPath = str;
            TParModelPath = str;
            //combSelectPara.Items.Clear();
            //combSelectPara.Items.Add("静态响应延时");
            //combSelectPara.Items.Add("动态响应延时");

        }

        private void btnLoadOriginalDt_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = DataSheetPath;
            openFileDialog1.Filter = "数据表文件(*.yuanshidata)|*.yuanshidata|所有文件|*.*";// 将yuanshidata改为datasheet
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                labelOriginalDt.Text = S;
                DataSheetPath = filename;

                //当前路径下的数据对应的界面初始化  同时 模型初始化
                InterfaceInit(filename);

                MessageBox.Show("原始数据加载成功！数据长度：" + (dataLength - 3));
            }
        }

        static DataSheetTao DST = new DataSheetTao();
        //界面初始化  同时 模型初始化
        private void InterfaceInit(string file)
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
            combInptuVar.Items.Clear();
            for (int i = 0; i < inputNum; i++)
            {
                labelInputVar.Text += "MV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i] + " ";
                combInptuVar.Items.Add(s1[2].Trim().Split(' ')[i]);
                if (i == 0)
                    combInptuVar.Text = s1[2].Trim().Split(' ')[i];
                mvname[i] = s1[2].Trim().Split(' ')[i];
            }
            labelOutputVar.Text = "";
            combOutputVar.Items.Clear();
            for (int i = 0; i < outputNum; i++)
            {
                labelOutputVar.Text += "CV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i + inputNum] + " ";
                combOutputVar.Items.Add(s1[2].Trim().Split(' ')[i + inputNum]);
                if (i == 0)
                    combOutputVar.Text = s1[2].Trim().Split(' ')[i + inputNum];
                cvname[i] = s1[2].Trim().Split(' ')[i + inputNum];
            }

            //初始化DST中的 延时参数的数据
            DST.datasheet(inputNum, outputNum, dataLength - 3, cvname, mvname);


            varData = new double[dataLength - 3, inputNum + outputNum];
            for (int j = 0; j < dataLength - 3; j++)
            {
                for (int k = 0; k < inputNum + outputNum; k++)
                {
                    varData[j, k] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k * 2]);
                    if (k < inputNum)
                    {
                        DST.InputData[k, j] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k * 2]);
                    }
                    else
                    {
                        DST.OutputData[k - inputNum, j] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k * 2]);
                    }
                }
            }

            chart1.Titles.Clear();
            chart1.ChartAreas.Clear();
            chart1.Series.Clear();
            chart1.Titles.Add("原始数据曲线分布图");
            ChartArea chartarea1 = new ChartArea();
            chartarea1.Name = "area1";
            chart1.ChartAreas.Add(chartarea1);
            //chart1.ChartAreas.Add("area1");
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
            var font = new Font("Trebuchet MS", 8);
            chart1.ChartAreas[0].AxisX.LabelStyle.Font = font;
            chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.ChartAreas[0].InnerPlotPosition.Height = 90;
            chart1.ChartAreas[0].InnerPlotPosition.Width = 90;
            chart1.ChartAreas[0].InnerPlotPosition.Y = 2;
            chart1.ChartAreas[0].InnerPlotPosition.X = 10;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;

            for (int i = 1; i < outputNum + 1; i++)
            {
                Series _series = new Series();
                _series.ChartType = SeriesChartType.Spline;
                _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
                _series.ChartArea = chart1.ChartAreas[0].Name;
                chart1.Series.Add(_series); //加入Series

                chart1.Series[i.ToString()].LegendText = "CV" + i + ":" + s1[2].Trim().Split(' ')[inputNum - 1 + i];
                chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
                for (int j = 1; j <= dataLength - 3; j++)
                {
                    chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, inputNum - 1 + i]);
                }

            }
            ChartArea chartarea2 = new ChartArea();
            chartarea1.Name = "area2";
            chart1.ChartAreas.Add(chartarea2);
            //chart1.ChartAreas.Add("area2");
            chart1.ChartAreas[1].AxisY.LabelStyle.Format = "F2";
            //chart1.ChartAreas[1].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
            chart1.ChartAreas[1].AxisX.LabelStyle.Font = font;
            chart1.ChartAreas[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chart1.ChartAreas[1].InnerPlotPosition.Height = 90;
            chart1.ChartAreas[1].InnerPlotPosition.Width = 90;
            chart1.ChartAreas[1].InnerPlotPosition.Y = 2;
            chart1.ChartAreas[1].InnerPlotPosition.X = 10;
            chart1.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[1].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[1].AxisX.IsStartedFromZero = false;

            for (int i = outputNum + 1; i < inputNum + outputNum + 1; i++)
            {
                Series _series = new Series();
                _series.ChartType = SeriesChartType.Line;
                _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
                _series.ChartArea = chart1.ChartAreas[1].Name;
                chart1.Series.Add(_series); //加入Series
                chart1.Series[i.ToString()].LegendText = "MV" + (i - outputNum) + ":" + s1[2].Trim().Split(' ')[i - 1 - outputNum];
                chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
                for (int j = 1; j <= dataLength - 3; j++)
                {
                    chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, i - 1 - outputNum]);
                }
            }
            chart1.DataBind();

            // Zoom into the X axis
            chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, dataLength / 2);

            // Enable range selection and zooming end user interface
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            //将滚动内嵌到坐标轴中
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            // 设置滚动条的大小
            chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;

            // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

            // 设置自动放大与缩小的最小量
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 500;
            chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2;


            // Zoom into the X axis
            chart1.ChartAreas[1].AxisX.ScaleView.Zoom(1, dataLength / 2);
            //chart1.ChartAreas[1].AxisY.ScaleView.Zoom(1, 20);

            // Enable range selection and zooming end user interface
            chart1.ChartAreas[1].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[1].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[1].AxisX.ScaleView.Zoomable = true;

            //将滚动内嵌到坐标轴中
            chart1.ChartAreas[1].AxisX.ScrollBar.IsPositionedInside = true;

            // 设置滚动条的大小
            chart1.ChartAreas[1].AxisX.ScrollBar.Size = 10;

            // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
            //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

            // 设置自动放大与缩小的最小量
            chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollSize = 500;
            chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollMinSize = 2;

        }

        private void btnNewTParModel_Click(object sender, EventArgs e)
        {
            if (labelOriginalDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            saveFileDialog1.InitialDirectory = TParModelPath;
            saveFileDialog1.Filter = "数据表文件(*.TParModel)|*.TParModel|所有文件|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;//E:\\ 论文中回转窑的实验数据延时参数模型
                TParModelPath = filename;
                string S = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                labelTParpath.Text = S;
                if (File.Exists(TParModelPath))
                {
                    File.Delete(TParModelPath);
                }
                NewModelFun();
            }
            else
                return;
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
            MessageBox.Show("时延参数表已建立！");
        }

        private void btnLoadTParModel_Click(object sender, EventArgs e)
        {
            if (labelOriginalDt.Text == "_")
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
                LoadModelFun();
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

        private void NewModelFun()
        {
            FileStream fs = new FileStream(TParModelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(TParModelPath);

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);

            sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelOriginalDt.Text);
            sw.WriteLine();

            sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.RowNum));
            sw.WriteLine();
            for (int i = 0; i < DST.OutputNum; i++)
            {
                sw.Write("各输入变量与" + DST.CVname[i] + "的延时参数：");
                sw.WriteLine();
                sw.Write("TaoD" + " TaoR");
                sw.WriteLine();
                for (int j = 0; j < DST.InputNum; j++)
                {
                    sw.Write("0 " + "0 ");//第一个0代表TaoD 第二个0代表TaoR
                    sw.WriteLine();
                }
            }
            sw.Flush();
            sw.Close();
            MessageBox.Show("新建模型成功！");
        }

        private void LoadModelFun()
        {
            string filename;
            string[] s1;
            openFileDialog1.InitialDirectory = TParModelPath;
            openFileDialog1.Filter = "模型文件(*.TParModel)|*.TParModel|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                TParModelPath = filename;

                labelTParpath.Text = S;
                StreamReader sr = new StreamReader(TParModelPath, Encoding.GetEncoding("gb2312"));
                String line;
                line = sr.ReadToEnd();
                s1 = line.Trim().Split('\n');
                if (labelOriginalDt.Text != s1[0].Trim().Split(' ')[5])
                {
                    MessageBox.Show("数据表文件不存在,请先导入名称为“" + s1[0].Trim().Split(' ')[5] + "”的数据表！");
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
        }

        private void btnSavePra_Click(object sender, EventArgs e)
        {
            if (labelOriginalDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }

            if (File.Exists(TParModelPath))
            {
                File.Delete(TParModelPath);
                SaveModelFun();
                MessageBox.Show("延时参数保存成功！");
            }
            else
            {
                saveFileDialog1.InitialDirectory = TParModelPath;
                saveFileDialog1.Filter = "模型文件(*.TParModel)|*.TParModel|所有文件|*.*";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    TParModelPath = filename;

                    if (File.Exists(TParModelPath))
                        File.Delete(TParModelPath);

                    SaveModelFun();
                    MessageBox.Show("延时参数保存成功！");
                }
            }
        }

        private void SaveModelFun()
        {
            FileStream fs = new FileStream(TParModelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(TParModelPath);
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelOriginalDt.Text);
            sw.WriteLine();
            sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.RowNum));
            sw.WriteLine();

            for (int j = 0; j < DST.OutputNum; j++)
            {
                sw.Write("各输入变量与" + DST.CVname[j] + "的延时参数：");
                sw.WriteLine();
                sw.Write("TaoD" + " TaoR");
                sw.WriteLine();
                for (int i = 0; i < DST.InputNum; i++)
                {
                    sw.Write(DST.TaoD[DST.InputNum * j + i] + " " + DST.TaoR[DST.InputNum * j + i] + " ");
                    sw.WriteLine();
                }
            }
            sw.Flush();
            sw.Close();
        }


        //private void btnXaxis_Click(object sender, EventArgs e)
        //{
        //    int xAxiswidthleft = Convert.ToInt16(this.textboxXasismin.Text);
        //    int xAxiswidthright = Convert.ToInt16(this.textboxXasismax.Text);
        //    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(xAxiswidthleft, xAxiswidthright);
        //    chart1.ChartAreas[1].AxisX.ScaleView.Zoom(xAxiswidthleft, xAxiswidthright);

        //}
        //private void btnYaxisCV_Click(object sender, EventArgs e)
        //{

        //    int yAxisCVwidthleft = Convert.ToInt16(this.textboxYasisCVmin.Text);
        //    int yAxisCVwidthright = Convert.ToInt16(this.textboxYasisCVmax.Text);
        //    chart1.ChartAreas[0].AxisY.ScaleView.Zoom(yAxisCVwidthleft, yAxisCVwidthright);
        //}

        //private void btnYaxisMV_Click(object sender, EventArgs e)
        //{
        //    int yAxisMVwidthleft = Convert.ToInt16(this.textboxYasisMVmin.Text);
        //    int yAxisMVwidthright = Convert.ToInt16(this.textboxYasisMVmax.Text);
        //    chart1.ChartAreas[1].AxisY.ScaleView.Zoom(yAxisMVwidthleft, yAxisMVwidthright);
        //}

        // static double[] delay;
        private void btnIdentifyPara_Click(object sender, EventArgs e)
        {
            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("未载入延时参数表，请先新建或载入延时参数表！");
                return;
            }
            NormCorr normCorr = new NormCorr();
            Matrix Matrix = new Matrix();
            int corrL = Convert.ToInt16(this.textBoxVarLength.Text);  // matlab中的 L
            int delay_Max = Convert.ToInt16(this.textBoxTaoRCorrLength.Text); // matlab中的DM
            int corrPinlv = 10; // matlab中的 i=1:10：Xend 中的10,以10为间隔
            int Xend, N;// = dataLength - 3 - delay_Max;
            N = (DST.RowNum - corrL - delay_Max) / corrPinlv;//若是699.9  可舍为699
            Xend = N * corrPinlv;
            double[,] X = new double[DST.InputNum, DST.RowNum];
            X = DST.InputData;
            double[,] Y = new double[DST.OutputNum, DST.RowNum];
            Y = DST.OutputData;
            double[] x = new double[DST.RowNum];
            double[] y = new double[DST.RowNum];
            double[] delay = new double[N];//任意个输入变量与输出变量之间的延时分布图为delay

            labelTaoR.Text = "";
            int n = 0, m = 0;//第n个输出 第m个输入
            for (int i = 0; i < DST.InputNum; i++)
            {
                if (combInptuVar.Text == "")
                {
                    MessageBox.Show("请选择输入变量！");
                    return;
                }
                if (DST.MVname[i] == combInptuVar.Text)
                {
                    m = i;
                }
            }
            for (int i = 0; i < DST.OutputNum; i++)
            {
                if (combOutputVar.Text == "")
                {
                    MessageBox.Show("请选择输出变量！");
                    return;
                }
                if (DST.CVname[i] == combOutputVar.Text)
                    n = i;
            }
            //辨识 动态响应延时
            for (int k = 0; k < DST.RowNum; k++)
            {
                y[k] = Y[n, k];          //得到的第一个y
            }
            //for (int m = 0; m < inputNum; m++)//选择第m个输入变量
            //{
            for (int k = 0; k < DST.RowNum; k++)
            {
                x[k] = X[m, k];//得到第一个x                        
            }
            for (int i = 1; i <= N; i++)
            {
                double[] corrX = new double[corrL];
                for (int j = 0; j < corrL; j++)
                {
                    corrX[j] = x[(i - 1) * corrPinlv + j];
                }
                int start = (i - 1) * 10;
                delay[i - 1] = normCorr.NormCORR(corrX, y, start, corrL, delay_Max);//k代表X和Y进行互相关的起始位置
            }

            DST.TaoR[m] = Convert.ToInt16(Matrix.AverageVal(delay));
            labelTaoR.Text += " " + Convert.ToInt16(DST.TaoR[m]).ToString();

            // ChartView(delay);
            // MessageBox.Show("延时参数辨识结果辨识完成!");

        }
        private void btnIdentifyTaoDPara_Click(object sender, EventArgs e)
        {
            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("未载入延时参数表，请先新建或载入延时参数表！");
                return;
            }
            NormCorr normCorr = new NormCorr();
            Matrix Matrix = new Matrix();
            int corrL = Convert.ToInt16(this.textBoxVarLength.Text);  // matlab中的 L
            int delay_Max = Convert.ToInt16(this.textBoxTaoDCorrLength.Text); // matlab中的DM
            int corrPinlv = 10; // matlab中的 i=1:10：Xend 中的10,以10为间隔
            int Xend, N;// = dataLength - 3 - delay_Max;
            N = (DST.RowNum - 1 - corrL - delay_Max) / corrPinlv;//若是699.9  可舍为699
            Xend = N * corrPinlv;
            double[,] X = new double[DST.InputNum, DST.RowNum];
            X = DST.InputData;
            double[,] Y = new double[DST.OutputNum, DST.RowNum];
            Y = DST.OutputData;

            //对输入变量与输出变量分别进行差分
            double[,] Xct = new double[DST.InputNum, DST.RowNum - 1];
            double[,] Yct = new double[DST.OutputNum, DST.RowNum - 1];
            Xct = ChaFen(X);
            Yct = ChaFen(Y);

            double[] x = new double[DST.RowNum - 1];
            double[] y = new double[DST.RowNum - 1];
            double[] delay = new double[N];//任意个输入变量与输出变量之间的延时分布图为delay            
            labelTaoD.Text = "";
            int n = 0, m = 0;//第n个输出 第m个输入
            for (int i = 0; i < DST.InputNum; i++)
            {
                if (combInptuVar.Text == "")
                {
                    MessageBox.Show("请选择输入变量！");
                    return;
                }
                if (DST.MVname[i] == combInptuVar.Text)
                {
                    m = i;
                }
            }
            for (int i = 0; i < DST.OutputNum; i++)
            {
                if (combOutputVar.Text == "")
                {
                    MessageBox.Show("请选择输出变量！");
                    return;
                }
                if (DST.CVname[i] == combOutputVar.Text)
                    n = i;
            }
            for (int k = 0; k < DST.RowNum - 1; k++)
            {
                y[k] = Yct[n, k];          //得到的第一个y
            }
            for (int k = 0; k < DST.RowNum - 1; k++)
            {
                x[k] = Xct[m, k];//得到第一个x                        
            }
            for (int i = 1; i <= N; i++)
            {
                double[] corrX = new double[corrL];
                for (int j = 0; j < corrL; j++)
                {
                    corrX[j] = x[(i - 1) * corrPinlv + j];
                }
                int start = (i - 1) * 10;
                delay[i - 1] = normCorr.NormCORR(corrX, y, start, corrL, delay_Max);//k代表X和Y进行互相关的起始位置
            }
            Dictionary<int, int> uq = Unique(delay);//返回一个泛型
            Dictionary<int, int>.ValueCollection value = uq.Values;//遍历所有的值

            int max = uq.Values.Max();
            foreach (KeyValuePair<int, int> kvp in uq)
            {
                if (kvp.Value.Equals(max))
                {
                    DST.TaoD[m] = Convert.ToInt16(kvp.Key);//寻找权值最大的key的值
                }
            }
            labelTaoD.Text += " " + Convert.ToInt16(DST.TaoD[m]).ToString();
        }

        private Dictionary<int, int> Unique(double[] d)
        {
            Dictionary<int, int> statDic = new Dictionary<int, int>();
            foreach (int i in d)
            {
                //还不存在于statDic中
                if (!statDic.ContainsKey(i))
                {
                    statDic.Add(i, 1);
                }
                else    //已经存在了，就在value中加一
                {
                    statDic[i] += 1;
                }
            }
            return statDic;
        }

        private double[,] ChaFen(double[,] OriMar)
        {
            double[,] ResultChaFen;
            int rowNum = OriMar.GetLength(0);//行数
            int colNum = OriMar.GetLength(1);//列数
            ResultChaFen = new double[rowNum, colNum - 1];
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum - 1; j++)
                {
                    ResultChaFen[i, j] = OriMar[i, j + 1] - OriMar[i, j];
                }
            }
            return ResultChaFen;

        }

        //private void ChartView(double[] d)
        //{
        //    chart1.Titles.Clear();
        //    chart1.ChartAreas.Clear();
        //    chart1.Series.Clear();
        //    chart1.Titles.Add("延时参数辨识结果分布图");
        //    ChartArea chartarea1 = new ChartArea();
        //    chartarea1.Name = "area1";
        //    chart1.ChartAreas.Add(chartarea1);
        //    //chart1.ChartAreas.Add("area1");
        //    chart1.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
        //    //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
        //    var font = new Font("Trebuchet MS", 8);
        //    chart1.ChartAreas[0].AxisX.LabelStyle.Font = font;
        //    chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        //    chart1.ChartAreas[0].InnerPlotPosition.Height = 90;
        //    chart1.ChartAreas[0].InnerPlotPosition.Width = 90;
        //    chart1.ChartAreas[0].InnerPlotPosition.Y = 2;
        //    chart1.ChartAreas[0].InnerPlotPosition.X = 10;
        //    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        //    chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;

        //    for (int i = 1; i < outputNum + 1; i++)
        //    {
        //        Series _series = new Series();
        //        _series.ChartType = SeriesChartType.Spline;
        //        _series.Name = i.ToString();//输出曲线定义从“1”开始
        //        _series.ChartArea = chart1.ChartAreas[0].Name;
        //        chart1.Series.Add(_series); //加入Series

        //        chart1.Series[i.ToString()].LegendText = "CV" + i + ":" + s1[2].Trim().Split(' ')[inputNum - 1 + i];
        //        chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
        //        for (int j = 1; j <= dataLength - 3; j++)
        //        {
        //            chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, inputNum - 1 + i]);
        //        }

        //    }
        //    ChartArea chartarea2 = new ChartArea();
        //    chartarea1.Name = "area2";
        //    chart1.ChartAreas.Add(chartarea2);
        //    //chart1.ChartAreas.Add("area2");
        //    chart1.ChartAreas[1].AxisY.LabelStyle.Format = "F2";
        //    //chart1.ChartAreas[1].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
        //    chart1.ChartAreas[1].AxisX.LabelStyle.Font = font;
        //    chart1.ChartAreas[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        //    chart1.ChartAreas[1].InnerPlotPosition.Height = 90;
        //    chart1.ChartAreas[1].InnerPlotPosition.Width = 90;
        //    chart1.ChartAreas[1].InnerPlotPosition.Y = 2;
        //    chart1.ChartAreas[1].InnerPlotPosition.X = 10;
        //    chart1.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
        //    chart1.ChartAreas[1].AxisY.MajorGrid.Enabled = false;
        //    chart1.ChartAreas[1].AxisX.IsStartedFromZero = false;

        //    for (int i = outputNum + 1; i < inputNum + outputNum + 1; i++)
        //    {
        //        Series _series = new Series();
        //        _series.ChartType = SeriesChartType.Line;
        //        _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
        //        _series.ChartArea = chart1.ChartAreas[1].Name;
        //        chart1.Series.Add(_series); //加入Series
        //        chart1.Series[i.ToString()].LegendText = "MV" + (i - outputNum) + ":" + s1[2].Trim().Split(' ')[i - 1 - outputNum];
        //        chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
        //        for (int j = 1; j <= dataLength - 3; j++)
        //        {
        //            chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, i - 1 - outputNum]);
        //        }
        //    }
        //    chart1.DataBind();

        //    // Zoom into the X axis
        //    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, dataLength / 2);

        //    // Enable range selection and zooming end user interface
        //    chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
        //    chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        //    chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

        //    //将滚动内嵌到坐标轴中
        //    chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

        //    // 设置滚动条的大小
        //    chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;

        //    // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
        //    //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

        //    // 设置自动放大与缩小的最小量
        //    chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 500;
        //    chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2;


        //    // Zoom into the X axis
        //    chart1.ChartAreas[1].AxisX.ScaleView.Zoom(1, dataLength / 2);
        //    //chart1.ChartAreas[1].AxisY.ScaleView.Zoom(1, 20);

        //    // Enable range selection and zooming end user interface
        //    chart1.ChartAreas[1].CursorX.IsUserEnabled = true;
        //    chart1.ChartAreas[1].CursorX.IsUserSelectionEnabled = true;
        //    chart1.ChartAreas[1].AxisX.ScaleView.Zoomable = true;

        //    //将滚动内嵌到坐标轴中
        //    chart1.ChartAreas[1].AxisX.ScrollBar.IsPositionedInside = true;

        //    // 设置滚动条的大小
        //    chart1.ChartAreas[1].AxisX.ScrollBar.Size = 10;

        //    // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
        //    //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

        //    // 设置自动放大与缩小的最小量
        //    chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollSize = 500;
        //    chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollMinSize = 2;
        //}

        private void textBoxVarLength_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.textBoxVarLength, "请输入整数");
        }

        private void textBoxCorrLength_MouseEnter(object sender, EventArgs e)
        {
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(this.textBoxTaoRCorrLength, "请输入整数");
        }


        private void btnCreatNewSheet_Click(object sender, EventArgs e)
        {
            if (labelOriginalDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }

            string newDataPath = "";

            saveFileDialog1.InitialDirectory = TParModelPath;
            saveFileDialog1.Filter = "T-LSSVR数据文件(*.newdata)|*.newdata|所有文件|*.*";
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;//E:\\  论文中回转窑的实验数据重组文件
                newDataPath = filename;

                if (File.Exists(newDataPath))
                {
                    File.Delete(newDataPath);
                }
                else
                {
                    //应该保存多个文件 每个输出变量对应一个文件


                    //FileStream fs = new FileStream(newDataPath, FileMode.OpenOrCreate, FileAccess.Write);
                    //StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
                    //string ModelName = System.IO.Path.GetFileName(newDataPath);

                    //sw.Flush();
                    //sw.BaseStream.Seek(0, SeekOrigin.End);

                    //sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelOriginalDt.Text);
                    //sw.WriteLine();

                    //sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.RowNum));
                    //sw.WriteLine();
                    //for (int i = 0; i < DST.OutputNum; i++)
                    //{
                    //    sw.Write("各输入变量与" + DST.CVname[i] + "的延时参数：");
                    //    sw.WriteLine();
                    //    sw.Write("TaoD" + " TaoR");
                    //    sw.WriteLine();
                    //    for (int j = 0; j < DST.InputNum; j++)
                    //    {
                    //        sw.Write("0 " + "0 ");//第一个0代表TaoD 第二个0代表TaoR
                    //        sw.WriteLine();
                    //    }
                    //}
                    //sw.Flush();
                    //sw.Close();
                }

                //SaveModelFun();
                MessageBox.Show("已建立T-LSSVR软测量模型所需的数据表文件！");
            }
        }
    }
}
