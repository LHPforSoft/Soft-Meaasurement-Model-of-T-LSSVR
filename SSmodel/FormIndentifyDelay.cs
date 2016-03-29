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
    public partial class FormIndentifyDelay : Form
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


        public FormIndentifyDelay()
        {
            InitializeComponent();
        }

        private void FormIndentifyDelay_Load(object sender, EventArgs e)
        {

            this.Text = "Advacon System One";
            //this.WindowState = FormWindowState.Maximized;
            string str = Environment.CurrentDirectory;
            DataSheetPath = str;
            TParModelPath = str;
        }
        private static double[,] US;
        public static string DataSheetPath = "E:";
        static string TParModelPath = "E:";
        public double[,] varData;//所有的变量中的数据 样本个数*变量个数  用于chart曲线中原始曲线绘制
        public double[,] varDataY;//所有的变量中的数据 样本个数*变量个数  用于chart曲线中原始曲线绘制
        public int inputNum;
        public int outputNum;//与datasheet表格里边一致，先入 后出
        public int dataLength;
        public int FlagSavePar = 0;
        private void btnLoadOriginalDt_Click(object sender, EventArgs e)
        {
            string filename;
            openFileDialog1.InitialDirectory = DataSheetPath;
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                openFileDialog1.Title = "打开辅助变量的原始数据表";
                openFileDialog1.Filter = "数据表文件(*.OriginalDataX)|*.OriginalDataX|所有文件|*.*";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    labelOriginalDt.Text = S;
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
                    labelOriginalDt.Text += "\n" + S;

                    DataSheetPath = filename;
                    //当前路径下的数据对应的界面初始化  输出变量初始化 同时 模型初始化
                    InterfaceInitY(filename);//*************************************待修改
                    MessageBox.Show("生料细度的原始数据加载成功！数据长度：" + (dataLength - 4));
                }
            }
            else
            {
                openFileDialog1.Filter = "数据表文件(*.datasheet)|*.datasheet|所有文件|*.*";
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filename = openFileDialog1.FileName;
                    string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
                    labelOriginalDt.Text = S;
                    DataSheetPath = filename;
                    //当前路径下的数据对应的界面初始化  输入变量初始化 同时 模型初始化
                    InterfaceInit(filename);
                    MessageBox.Show("原始数据加载成功！数据长度：" + (dataLength - 3));
                }
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
            combInptuVar.Items.Clear();
            for (int i = 0; i < inputNum; i++)
            {
                labelInputVar.Text += "MV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i] + " ";
                combInptuVar.Items.Add(s1[2].Trim().Split(' ')[i]);
                if (i == 0)
                    combInptuVar.Text = s1[2].Trim().Split(' ')[i];
                mvname[i] = s1[2].Trim().Split(' ')[i];
            }

            for (int i = 0; i < outputNum; i++)
            {
                cvname[i] = s1[2].Trim().Split(' ')[i + inputNum];
            }

            //初始化DST中的 延时参数的数据
            DST.datasheet(inputNum, outputNum, dataLength - 3, cvname, mvname);


            varData = new double[dataLength - 3, inputNum + outputNum];
            for (int j = 0; j < dataLength - 3; j++)
            {
                for (int k = 0; k < inputNum; k++)
                {

                    varData[j, k] = Convert.ToDouble(s1[j + 3].Trim().Split('\t')[k]);

                    DST.InputData[k, j] = Convert.ToDouble(s1[j + 3].Trim().Split('\t')[k]);
                }
            }
            sr.Close();
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
            ChartArea chartarea2 = new ChartArea();
            chartarea2.Name = "area2";
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

            for (int i = 1; i < inputNum + 1; i++)
            {
                Series _series = new Series();
                _series.ChartType = SeriesChartType.Line;
                _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输入曲线定义从“1”开始
                _series.ChartArea = chart1.ChartAreas[1].Name;
                chart1.Series.Add(_series); //加入Series
                chart1.Series[i.ToString()].LegendText = "MV" + (i) + ":" + s1[2].Trim().Split(' ')[i - 1];
                chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
                for (int j = 1; j <= dataLength - 3; j++)
                {
                    chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, i - 1]);
                }
            }
            chart1.DataBind();

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

            for (int i = 1; i < outputNum + 1; i++)
            {
                Series _series = new Series();
                _series.ChartType = SeriesChartType.Spline;
                _series.Name = (i + inputNum).ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
                _series.ChartArea = chart1.ChartAreas[0].Name;
                chart1.Series.Add(_series); //加入Series

                chart1.Series[(i + inputNum).ToString()].LegendText = "CV" + i + ":" + s1[2].Trim().Split(' ')[inputNum - 1 + i];
                chart1.Series[(i + inputNum).ToString()].XValueType = ChartValueType.Int32;
                for (int j = 1; j <= dataLength - 4; j++)
                {
                    chart1.Series[(i + inputNum).ToString()].Points.AddXY(j, varDataY[j - 1, i - 1]);
                }

            }

            chart1.DataBind();

            //// Zoom into the X axis
            //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, dataLength / 2);

            //// Enable range selection and zooming end user interface
            //chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

            ////将滚动内嵌到坐标轴中
            //chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            //// 设置滚动条的大小
            //chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;

            //// 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
            ////chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

            //// 设置自动放大与缩小的最小量
            //chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 500;
            //chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2;


        }

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
                    if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
                    {
                        varData[j, k] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k]);
                        if (k < inputNum)
                        {
                            DST.InputData[k, j] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k]);
                        }
                        else
                        {
                            DST.OutputData[k - inputNum, j] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k]);
                        }
                    }
                    else
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
            }
            sr.Close();
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

        private void NewModelFun()
        {
            FileStream fs = new FileStream(TParModelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(TParModelPath);

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);
            //测试辨识历史与细度之间的延时参数文件

            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                string wenjianX = labelOriginalDt.Text.Trim().Split('\n')[0];
                string wenjianY = labelOriginalDt.Text.Trim().Split('\n')[1];
                sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 辅助变量数据表名称: " + wenjianX + " 生料细度数据表名称： " + wenjianY);
            }
            else
            {
                sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelOriginalDt.Text);
            }

            sw.WriteLine();
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.OutputData.GetLength(1)));
            }
            else
            {
                sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.RowNum));
            }
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
            // MessageBox.Show("新建模型成功！");
        }

        private void LoadTParModelFun()
        {
            string[] s1;
            StreamReader sr = new StreamReader(TParModelPath, Encoding.GetEncoding("gb2312"));
            String line;
            line = sr.ReadToEnd();
            s1 = line.Trim().Split('\n');
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                if (labelOriginalDt.Text != s1[0].Trim().Split(' ')[5] + "\n" + s1[0].Trim().Split(' ')[7])
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
            }
            else
            {
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
            }
            sr.Close();
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

            }
            SaveModelFun();
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
            MessageBox.Show("延时参数保存成功！");

            //else
            //{
            //    saveFileDialog1.InitialDirectory = TParModelPath;
            //    saveFileDialog1.Filter = "模型文件(*.TParModel)|*.TParModel|所有文件|*.*";
            //    saveFileDialog1.RestoreDirectory = true;
            //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //    {
            //        string filename = saveFileDialog1.FileName;
            //        TParModelPath = filename;

            //        if (File.Exists(TParModelPath))
            //            File.Delete(TParModelPath);
            //        SaveModelFun();
            //        labelTaoD.Text = "";
            //        labelTaoR.Text = "";
            //        for (int j = 0; j < DST.OutputNum; j++)
            //        {
            //            for (int i = 0; i < DST.InputNum; i++)
            //            {
            //                labelTaoD.Text += " " + (Int16)DST.TaoD[i];
            //            }
            //            if (j > 0)
            //            {
            //                labelTaoD.Text += " |";
            //            }
            //        }
            //        for (int j = 0; j < DST.OutputNum; j++)
            //        {
            //            for (int i = 0; i < DST.InputNum; i++)
            //            {
            //                labelTaoR.Text += " " + (Int16)DST.TaoR[i];
            //            }
            //            if (j > 0)
            //            {
            //                labelTaoR.Text += " |";
            //            }
            //        }
            //        MessageBox.Show("延时参数保存成功！");
            //    }
            //}

            FlagSavePar = 1;
        }

        private void SaveModelFun()
        {
            FileStream fs = new FileStream(TParModelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(TParModelPath);
            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);

            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                string wenjianX = labelOriginalDt.Text.Trim().Split('\n')[0];
                string wenjianY = labelOriginalDt.Text.Trim().Split('\n')[1];
                sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 辅助变量数据表名称: " + wenjianX + " 生料细度数据表名称： " + wenjianY);
            }
            else
            {
                sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelOriginalDt.Text);
            }
            sw.WriteLine();
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.OutputData.GetLength(1)));
            }
            else
            {
                sw.Write("输入变量数: " + Convert.ToString(DST.InputNum) + " 输出变量数: " + Convert.ToString(DST.OutputNum) + " 样本个数: " + Convert.ToString(DST.RowNum));
            }
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

        static int n = 0, m = 0;//第n个输出 第m个输入
        public static int FlagHuoGenggai = 0;

        // static double[] delay;
        private void btnIdentifyTaoRPara_Click(object sender, EventArgs e)
        {
            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("未载入延时参数表，请先新建或载入延时参数表！");
                return;
            }
            if (labelTaoD.Text.Contains("或"))  // FlagHuoGenggai == 1)
            {
                MessageBox.Show("请选择辨识的参数结果，并按\"确认选择\"按钮！");
                return;
            }
            FlagSavePar = 0;
            NormCorr NormCorr = new NormCorr();
            Matrix Matrix = new Matrix();
            int corrL = Convert.ToInt16(this.textBoxVarLength.Text);  // matlab中的 L
            int delay_Max = Convert.ToInt16(this.textBoxTaoRCorrLength.Text); // matlab中的DM
            // matlab中的 i=1:10：Xend 中的10,以10为间隔
            // N= dataLength - 3 - delay_Max;
            int corrPinlv, Xend, N;
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                corrPinlv = 1; // matlab中的 i=1:1：Xend 中的10,以10为间隔
                N = (DST.OutputData.GetLength(1) + 1 - corrL) / corrPinlv;//若是699.9  可舍为699
            }
            else
            {
                corrPinlv = 10; // matlab中的 i=1:10：Xend 中的10,以10为间隔
                N = (DST.RowNum - 1 - corrL - delay_Max) / corrPinlv;//若是699.9  可舍为699
            }
            Xend = N * corrPinlv;
            int[] HangShu24;
            double[,] X, Y;
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                int outputdataLength = DST.OutputData.GetLength(1);// 细度列数 123
                HangShu24 = new int[outputdataLength * 24];
                X = new double[DST.InputNum, HangShu24.GetLength(0)];
                Y = new double[DST.OutputNum, outputdataLength];

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
                        X[i, j] = DST.InputData[i, xuhao - 1];//实际的序号应该从0开始  
                    }
                }
                Y = DST.OutputData;
                labelTaoR.Text = "";

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
                double[] corrX = new double[HangShu24.GetLength(0)];
                double[] corrY = new double[outputdataLength];
                for (int i = 0; i < HangShu24.GetLength(0); i++)
                {
                    corrX[i] = X[m, i];//得到进行归一化互相关的 corrX 第m行的X
                }
                for (int i = 0; i < outputdataLength; i++)
                {
                    corrY[i] = Y[n, i];
                }
                double[] delay = new double[N];//任意个输入变量与输出变量之间的延时分布图为delay
                for (int pl = 0; pl < Xend; pl++)
                {
                    delay[pl] = NormCorr.TaoRCORR(corrX, corrY, pl, corrL, delay_Max);
                }
                Dictionary<int, int> uq = Unique(delay);//返回一个泛型
                Dictionary<int, int>.ValueCollection value = uq.Values;//遍历所有的值

                int max = uq.Values.Max();
                foreach (KeyValuePair<int, int> kvp in uq)
                {
                    if (kvp.Value.Equals(max))
                    {
                        DST.TaoR[m] = Convert.ToInt16(kvp.Key);//寻找权值最大的key的值
                        if (labelTaoR.Text == "")
                        {
                            labelTaoR.Text += " " + Convert.ToInt16(DST.TaoR[m]).ToString();
                            combChosePar.Text = "";
                            combChosePar.Items.Clear();
                            combChosePar.Items.Add(Convert.ToInt16(DST.TaoR[m]).ToString());
                            combChosePar.Text = combChosePar.Items[0].ToString();
                        }
                        else
                        {
                            labelTaoR.Text += " 或 " + Convert.ToInt16(DST.TaoR[m]).ToString();
                            combChosePar.Items.Add(Convert.ToInt16(DST.TaoR[m]).ToString());
                            FlagHuoGenggai = 1;
                        }
                    }
                }
            }
            else
            {
                X = new double[DST.InputNum, DST.RowNum];
                X = DST.InputData;
                Y = new double[DST.OutputNum, DST.RowNum];
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
                    delay[i - 1] = NormCorr.NormCORR(corrX, y, start, corrL, delay_Max);//k代表X和Y进行互相关的起始位置
                }
                DST.TaoR[m] = Convert.ToInt16(Matrix.AverageVal(delay));
                labelTaoR.Text += " " + Convert.ToInt16(DST.TaoR[m]).ToString();
            }

        }


        private void btnIdentifyTaoDPara_Click(object sender, EventArgs e)
        {
            if (labelTParpath.Text == "_")
            {
                MessageBox.Show("未载入延时参数表，请先新建或载入延时参数表！");
                return;
            }
            if (labelTaoR.Text.Contains("或"))  // FlagHuoGenggai == 1)
            {
                MessageBox.Show("请选择辨识的参数结果，并按\"确认选择\"按钮！");
                return;
            }
            FlagSavePar = 0;
            NormCorr NormCorr = new NormCorr();
            Matrix Matrix = new Matrix();
            int corrL = Convert.ToInt16(this.textBoxVarLength.Text);  // matlab中的 L
            int delay_Max = Convert.ToInt16(this.textBoxTaoDCorrLength.Text); // matlab中的DM
            int corrPinlv, Xend, N;
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                corrPinlv = 1; // matlab中的 i=1:1：Xend 中的10,以10为间隔
                N = (DST.OutputData.GetLength(1) - corrL) / corrPinlv;//若是699.9  可舍为699
            }
            else
            {
                corrPinlv = 10; // matlab中的 i=1:10：Xend 中的10,以10为间隔
                N = (DST.RowNum - 1 - corrL - delay_Max) / corrPinlv;//若是699.9  可舍为699
            }
            Xend = N * corrPinlv;
            int[] HangShu24;
            double[,] X, Y;
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                int outputdataLength = DST.OutputData.GetLength(1);// 细度列数 123
                int inputdataLength = DST.InputData.GetLength(1);
                //对原始输入变量与原始输出变量分别进行差分
                double[,] Xss = new double[DST.InputNum, inputdataLength];
                double[,] Yss = new double[DST.OutputNum, outputdataLength];
                double[,] Xct = new double[DST.InputNum, inputdataLength - 1];
                double[,] Yct = new double[DST.OutputNum, outputdataLength - 1];
                for (int j = 0; j < DST.InputData.GetLength(0); j++)
                {
                    for (int i = 0; i < inputdataLength; i++)
                    {
                        Xss[j, i] = DST.InputData[j, i];
                    }
                }
                for (int j = 0; j < DST.OutputData.GetLength(0); j++)
                {
                    for (int i = 0; i < outputdataLength; i++)
                    {
                        Yss[j, i] = DST.OutputData[j, i];
                    }
                }
                Xct = ChaFen(Xss);
                Yct = ChaFen(Yss);

                HangShu24 = new int[DST.OutputData.GetLength(1) * 24];//2912
                X = new double[DST.InputNum, HangShu24.GetLength(0)];
                Y = new double[DST.OutputNum, outputdataLength - 1];
                for (int i = 0; i < outputdataLength; i++)
                {
                    for (int j = 0; j < 24; j++)
                    {
                        HangShu24[i * 24 + j] = DST.HangShu[0, i] - (24 - 1 - j);
                    }
                }

                for (int i = 0; i < DST.InputNum; i++)
                {
                    for (int j = 0; j < HangShu24.GetLength(0); j++)
                    {
                        int xuhao = 0;
                        xuhao = HangShu24[j];
                        X[i, j] = Xct[i, xuhao - 1];//实际的序号应该从0开始  
                    }
                }
                Y = Yct;

                labelTaoD.Text = "";
                // int n = 0, m = 0;//第n个输出 第m个输入
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
                double[] corrX = new double[HangShu24.GetLength(0)];
                double[] corrY = new double[outputdataLength - 1];
                for (int i = 0; i < HangShu24.GetLength(0); i++)
                {
                    corrX[i] = X[m, i];//得到进行归一化互相关的 corrX 第m行的X
                }
                for (int i = 0; i < outputdataLength - 1; i++)
                {
                    corrY[i] = Y[n, i];
                }
                double[] delay = new double[N];//任意个输入变量与输出变量之间的延时分布图为delay
                for (int pl = 0; pl < Xend; pl++)
                {
                    delay[pl] = NormCorr.TaoDCORR(corrX, corrY, pl, corrL, delay_Max);
                }
                Dictionary<int, int> uq = Unique(delay);//返回一个泛型
                Dictionary<int, int>.ValueCollection value = uq.Values;//遍历所有的值
                int max = uq.Values.Max();
                foreach (KeyValuePair<int, int> kvp in uq)
                {
                    if (kvp.Value.Equals(max))
                    {
                        DST.TaoD[m] = Convert.ToInt16(kvp.Key);//寻找权值最大的key的值
                        if (labelTaoD.Text == "")
                        {
                            labelTaoD.Text += " " + Convert.ToInt16(DST.TaoD[m]).ToString();
                            combChosePar.Text = "";
                            combChosePar.Items.Clear();
                            combChosePar.Items.Add(Convert.ToInt16(DST.TaoD[m]).ToString());
                            combChosePar.Text = combChosePar.Items[0].ToString();
                        }
                        else
                        {
                            labelTaoD.Text += " 或 " + Convert.ToInt16(DST.TaoD[m]).ToString();
                            combChosePar.Items.Add(Convert.ToInt16(DST.TaoD[m]).ToString());
                            FlagHuoGenggai = 1;

                        }

                    }
                }
            }
            else
            {
                X = new double[DST.InputNum, DST.RowNum];
                X = DST.InputData;
                Y = new double[DST.OutputNum, DST.RowNum];
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
                    delay[i - 1] = NormCorr.NormCORR(corrX, y, start, corrL, delay_Max);//k代表X和Y进行互相关的起始位置
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
            if (FlagSavePar == 0)
            {
                MessageBox.Show("请先保存当前辨识结果！");
                return;
            }
            if (labeltitle.Text == "生料细度延时参数辨识系统（T）")
            {
                //分两步进行保存
                string TLSSVRModelPath = "";
                saveFileDialog1.InitialDirectory = TParModelPath;
                saveFileDialog1.Filter = "T-LSSVR训练模型的数据文件(*.TlssvrTrainData)|*.TlssvrTrainData|所有文件|*.*";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;//E:\\  论文中回转窑的实验数据重组文件
                    TLSSVRModelPath = filename;
                    if (File.Exists(TLSSVRModelPath))
                    {
                        File.Delete(TLSSVRModelPath);
                    }
                    //应该保存多个文件 每个输出变量对应一个文件
                    int[] HangShu24;
                    double[,] X, Y;
                    int outputdataLength = DST.OutputData.GetLength(1);    // 细度列数 123
                    HangShu24 = new int[outputdataLength * 24];           //2952
                    X = new double[DST.InputNum, HangShu24.GetLength(0)];  //8*2952
                    Y = new double[DST.OutputNum, outputdataLength];       //1*123
                    
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
                    Y = DST.OutputData; // 1*123

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

                    //新建第一个文件
                    FileStream fs = new FileStream(TLSSVRModelPath, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
                    string ModelName = System.IO.Path.GetFileName(TLSSVRModelPath);

                    sw.Flush();
                    sw.BaseStream.Seek(0, SeekOrigin.End);

                    string wenjianX = labelOriginalDt.Text.Trim().Split('\n')[0];
                    string wenjianY = labelOriginalDt.Text.Trim().Split('\n')[1];
                    sw.Write(DateTime.Now.ToString() + " TagNum: " + Convert.ToString(lengthTlssTnDa) + " RowNum: " + outputdataLength + " MV: " + DST.InputNum + " CV: " + DST.OutputNum + " 模型名称: " + ModelName + " 辅助变量数据表名称: " + wenjianX + " 生料细度数据表名称： " + wenjianY);
                    // TagNum: 29 RowNum: 123 MV: 8 CV: 1
                    sw.WriteLine();

                    sw.Flush();
                    sw.Close();

                    MessageBox.Show("已建立T-LSSVR软测量模型所需的数据表文件！");
                }

            }

            else
            {
                //建立普通的  如回转窑 的 T-LSSVR  LSSVR的软测量模型所需文件

            }



        }

        private void btnChose_Click(object sender, EventArgs e)
        {

            if (labelTaoD.Text.Contains("或"))
            {
                DST.TaoD[m] = Convert.ToDouble(combChosePar.Text);
                labelTaoD.Text = " " + combChosePar.Text;
                FlagHuoGenggai = 0;
                FlagSavePar = 0;//未进行保存参数
            }
            else if (labelTaoR.Text.Contains("或"))
            {
                DST.TaoR[m] = Convert.ToDouble(combChosePar.Text);
                labelTaoR.Text = " " + combChosePar.Text;
                FlagHuoGenggai = 0;
                FlagSavePar = 0;
            }
            else
            {
                MessageBox.Show("当前参数只有一个，无需选择！");
            }

        }

        private void combInptuVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FlagHuoGenggai == 1)
            {
                MessageBox.Show("请选择辨识的参数结果，并按\"确认选择\"按钮！");
            }
        }

        private void combOutputVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FlagHuoGenggai == 1)
            {
                MessageBox.Show("请选择辨识的参数结果，并按\"确认选择\"按钮！");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }


    }
}
