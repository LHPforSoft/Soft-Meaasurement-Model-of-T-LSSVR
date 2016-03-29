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
    public partial class FormTrain : Form
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
        public FormTrain()
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

        private LSSVM LsSvm = new LSSVM();

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Advacon System One";
            this.WindowState = FormWindowState.Normal;//.Maximized;            
            string str = Environment.CurrentDirectory;
            DataSheetPath = str;
            TLSSVRModelPath = str;
            //combModelType.Text = combModelType.Items[1].ToString();
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
                    chart1.Series[i.ToString()].Points.AddXY(j, varDataX[j - 1, i - 1]);
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
            //combOutputVar.Items.Clear();
            for (int i = 0; i < outputNum; i++)
            {
                labelOutputVar.Text += "CV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i + inputNum] + " ";
                comboBox2.Items.Add(s1[2].Trim().Split(' ')[i + inputNum]);
                if (i == 0)
                    comboBox2.Text = s1[2].Trim().Split(' ')[i + inputNum];
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
        }

        //{
        //    string filename;
        //    openFileDialog1.InitialDirectory = DataSheetPath;
        //    if (combModelType.Text == "LSSVR")
        //    {//newdata
        //        openFileDialog1.Filter = "数据表文件(*.LssvrTrainData)|*.LssvrTrainData|所有文件|*.*";
        //    }
        //    else if (combModelType.Text == "T-LSSVR")
        //    {
        //        openFileDialog1.Filter = "数据表文件(*.TlssvrTrainData)|*.TlssvrTrainData|所有文件|*.*";
        //    }
        //    else
        //    {
        //        MessageBox.Show("请先选择训练模型的类型！");
        //        return;
        //    }
        //    openFileDialog1.RestoreDirectory = true;
        //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        filename = openFileDialog1.FileName;
        //        string S = System.IO.Path.GetFileName(openFileDialog1.FileName);
        //        labelnewDt.Text = S;
        //        DataSheetPath = filename;

        //        string fs = DataSheetPath;
        //        StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
        //        String line;
        //        line = sr.ReadToEnd();
        //        string[] s1 = line.Trim().Split('\n');
        //        dataLength = s1.Length;             //dataLength指总的数据长度，dataLength-3为变量的数据长度

        //        inputnum = Convert.ToInt16(s1[0].Trim().Split(' ')[7]);
        //        outputnum = Convert.ToInt16(s1[0].Trim().Split(' ')[9]);
        //        labelInputVar.Text = "";
        //        //combInptuVar.Items.Clear();
        //        for (int i = 0; i < inputnum; i++)
        //        {
        //            labelInputVar.Text += "MV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i] + " ";
        //            // combInptuVar.Items.Add(s1[2].Trim().Split(' ')[i]);
        //        }
        //        labelOutputVar.Text = "";
        //        //combOutputVar.Items.Clear();
        //        for (int i = 0; i < outputnum; i++)
        //        {
        //            labelOutputVar.Text += "CV" + Convert.ToString(i + 1) + ": " + s1[2].Trim().Split(' ')[i + inputnum] + " ";
        //            //combOutputVar.Items.Add(s1[2].Trim().Split(' ')[i + inputnum]);
        //        }

        //        varData = new double[dataLength - 3, inputnum + outputnum];
        //        for (int j = 0; j < dataLength - 3; j++)
        //        {
        //            for (int k = 0; k < inputnum + outputnum; k++)
        //            {
        //                varData[j, k] = Convert.ToDouble(s1[j + 3].Trim().Split(' ')[k]);
        //            }
        //        }
        //        chart1.Titles.Clear();
        //        chart1.ChartAreas.Clear();
        //        chart1.Series.Clear();
        //        chart1.Titles.Add("基于T-LSSVR的生料细度软测量数据");
        //        ChartArea chartarea1 = new ChartArea();
        //        chartarea1.Name = "area1";
        //        chart1.ChartAreas.Add(chartarea1);
        //        //chart1.ChartAreas.Add("area1");
        //        chart1.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
        //        //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
        //        var font = new Font("Trebuchet MS", 8);
        //        chart1.ChartAreas[0].AxisX.LabelStyle.Font = font;
        //        chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        //        chart1.ChartAreas[0].InnerPlotPosition.Height = 90;
        //        chart1.ChartAreas[0].InnerPlotPosition.Width = 90;
        //        chart1.ChartAreas[0].InnerPlotPosition.Y = 2;
        //        chart1.ChartAreas[0].InnerPlotPosition.X = 10;
        //        chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;

        //        for (int i = 1; i < outputnum + 1; i++)
        //        {
        //            Series _series = new Series();
        //            _series.ChartType = SeriesChartType.Spline;
        //            _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
        //            _series.ChartArea = chart1.ChartAreas[0].Name;
        //            chart1.Series.Add(_series); //加入Series

        //            chart1.Series[i.ToString()].LegendText = "CV" + i + ":" + s1[2].Trim().Split(' ')[inputnum - 1 + i];
        //            chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
        //            for (int j = 1; j <= dataLength - 3; j++)
        //            {
        //                chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, inputnum - 1 + i]);
        //            }

        //        }
        //        ChartArea chartarea2 = new ChartArea();
        //        chartarea1.Name = "area2";
        //        chart1.ChartAreas.Add(chartarea2);
        //        //chart1.ChartAreas.Add("area2");
        //        chart1.ChartAreas[1].AxisY.LabelStyle.Format = "F2";
        //        //chart1.ChartAreas[1].AxisX.LabelStyle.Format = "yy-MM-dd HH:mm:ss ";
        //        chart1.ChartAreas[1].AxisX.LabelStyle.Font = font;
        //        chart1.ChartAreas[1].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
        //        chart1.ChartAreas[1].InnerPlotPosition.Height = 90;
        //        chart1.ChartAreas[1].InnerPlotPosition.Width = 90;
        //        chart1.ChartAreas[1].InnerPlotPosition.Y = 2;
        //        chart1.ChartAreas[1].InnerPlotPosition.X = 10;
        //        chart1.ChartAreas[1].AxisX.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[1].AxisY.MajorGrid.Enabled = false;
        //        chart1.ChartAreas[1].AxisX.IsStartedFromZero = false;

        //        for (int i = outputnum + 1; i < inputnum + outputnum + 1; i++)
        //        {
        //            Series _series = new Series();
        //            _series.ChartType = SeriesChartType.Line;
        //            _series.Name = i.ToString();//chart1.Series.Add(i.ToString());//输出曲线定义从“1”开始
        //            _series.ChartArea = chart1.ChartAreas[1].Name;
        //            chart1.Series.Add(_series); //加入Series
        //            chart1.Series[i.ToString()].LegendText = "MV" + (i - outputnum) + ":" + s1[2].Trim().Split(' ')[i - 1 - outputnum];
        //            chart1.Series[i.ToString()].XValueType = ChartValueType.Int32;
        //            for (int j = 1; j <= dataLength - 3; j++)
        //            {
        //                chart1.Series[i.ToString()].Points.AddXY(j, varData[j - 1, i - 1 - outputnum]);
        //            }
        //        }
        //        chart1.DataBind();


        //        // Zoom into the X axis
        //        chart1.ChartAreas[0].AxisX.ScaleView.Zoom(1, dataLength / 2);

        //        // Enable range selection and zooming end user interface
        //        chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
        //        chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
        //        chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;

        //        //将滚动内嵌到坐标轴中
        //        chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

        //        // 设置滚动条的大小
        //        chart1.ChartAreas[0].AxisX.ScrollBar.Size = 10;

        //        // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
        //        //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

        //        // 设置自动放大与缩小的最小量
        //        chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = 500;
        //        chart1.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 2;


        //        // Zoom into the X axis
        //        chart1.ChartAreas[1].AxisX.ScaleView.Zoom(1, dataLength / 2);
        //        //chart1.ChartAreas[1].AxisY.ScaleView.Zoom(1, 20);

        //        // Enable range selection and zooming end user interface
        //        chart1.ChartAreas[1].CursorX.IsUserEnabled = true;
        //        chart1.ChartAreas[1].CursorX.IsUserSelectionEnabled = true;
        //        chart1.ChartAreas[1].AxisX.ScaleView.Zoomable = true;

        //        //将滚动内嵌到坐标轴中
        //        chart1.ChartAreas[1].AxisX.ScrollBar.IsPositionedInside = true;

        //        // 设置滚动条的大小
        //        chart1.ChartAreas[1].AxisX.ScrollBar.Size = 10;

        //        // 设置滚动条的按钮的风格，下面代码是将所有滚动条上的按钮都显示出来
        //        //chart1.ChartAreas["Default"].AxisX.ScrollBar.ButtonStyle =ScrollBarButtonStyle.All;

        //        // 设置自动放大与缩小的最小量
        //        chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollSize = 500;
        //        chart1.ChartAreas[1].AxisX.ScaleView.SmallScrollMinSize = 2;

        //        MessageBox.Show("原始数据加载成功！数据长度：" + (dataLength - 3));

        //    }

        //}

        private void btnNewModel_Click(object sender, EventArgs e)
        {
            if (labelnewDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }
            saveFileDialog1.InitialDirectory = TLSSVRModelPath;// SSmodelPath;
            //if (combModelType.Text == "LSSVR")
            //{
            // saveFileDialog1.Filter = "数据表文件(*.LssvrModel)|*.LssvrModel|所有文件|*.*";
            //}
            //else if (combModelType.Text == "T-LSSVR")
            //{
            saveFileDialog1.Filter = "数据表文件(*.TlssvrModel)|*.TlssvrModel|所有文件|*.*";
            //}
            //else
            //{
            //    MessageBox.Show("请先选择训练模型的类型！");
            //    return;
            //}
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                TLSSVRModelPath = filename;
                string S = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                labelModel.Text = S;

                TrainDataInit();

                ModelInit();
                SaveModelFun();
            }
            else
                return;
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



        private string[] CvName;
        private string[] MvName;
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
            //MvNameLabel.Text = "";
            //for (int i = 0; i < datasheet1.InputNum; i++)
            //    MvNameLabel.Text += "MV" + Convert.ToString(i + 1) + ": " + datasheet1.MVname[i] + "  ";
            //CvNameLabel.Text = "";
            //for (int i = 0; i < datasheet1.OutputNum; i++)
            //    CvNameLabel.Text += "CV" + Convert.ToString(i + 1) + ": " + datasheet1.CVname[i] + "  ";

            LsSvm.InitiLSSVM(datasheet1.TrainInputValue, datasheet1.TrainOutputValue, datasheet1.TestInputValue, datasheet1.TestOutputValue);

            ChangeCDeta();
            //InputNumText.Text = Convert.ToString(datasheet1.InputNum);
            //OutputNumText.Text = Convert.ToString(datasheet1.OutputNum);
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

        private void SaveModelFun()
        {
            FileStream fs = new FileStream(TLSSVRModelPath, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
            string ModelName = System.IO.Path.GetFileName(TLSSVRModelPath);

            sw.Flush();
            sw.BaseStream.Seek(0, SeekOrigin.End);

            string wenjianX = labelnewDt.Text.Trim().Split('\n')[0];
            string wenjianY = labelnewDt.Text.Trim().Split('\n')[1];
            sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 辅助变量数据表名称: " + wenjianX + " 生料细度数据表名称： " + wenjianY);

            // sw.Write(DateTime.Now.ToString() + " 模型名称: " + ModelName + " 数据表名称: " + labelnewDt.Text);
            sw.WriteLine();

            sw.Write("输入变量数: " + Convert.ToString(LsSvm.inNum) + " 输出变量数: " + Convert.ToString(LsSvm.outNum) + " 训练数据数: " + Convert.ToString(LsSvm.sampleNum) + " 训练数据比例: " + TrainDataPre.Text + " 预测数据比例: " + TestDataPre.Text);
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

        public int ChangeCDetaFlag = 0;
        public int Train_notFlag = 0;

        private void btnTrainModel_Click(object sender, EventArgs e)
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
            if (ChangeCDetaFlag == 0)
            {
                MessageBox.Show("请先修改各个输出变量训练参数！");
                return;
            }
            LsSvm.LSSVMTrain();
            Train_notFlag = 1;
            MessageBox.Show("模型训练完成！");
        }

        private void btnChangePar_Click(object sender, EventArgs e)
        {
            if (labelModel.Text == "_")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }
            ChangeCDeta();
            //int trainNum = LsSvm.sampleNum;
            //int testNum = LsSvm.TestSampleNum;
            //int sum = trainNum + testNum;

            //double TrainPre = Math.Round(100 * Convert.ToDouble(trainNum) / Convert.ToDouble(sum), 0);
            //double TestPre = Math.Round(100 * Convert.ToDouble(testNum) / Convert.ToDouble(sum), 0);
            double TrainPreText = Math.Round(Convert.ToDouble(TrainDataPre.Text), 0);
            double TestPreText = Math.Round(Convert.ToDouble(TestDataPre.Text), 0);
            if (TrainPreText + TestPreText != 100)
            {
                MessageBox.Show("训练数据和测试数据总和不是100%！");
                return;
            }
            else
            {
                ModelInit();
            }
            ChangeCDetaFlag = 1;
            for (int i = 0; i < LsSvm.outNum; i++)
            {
                if (LsSvm.C[i] == 0 || LsSvm.Deta[i] == 0)
                    ChangeCDetaFlag = 0;
            }
            MessageBox.Show("参数修改成功！");
        }

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            if (labelnewDt.Text == "_")
            {
                MessageBox.Show("数据表文件不存在，请先导入数据表！");
                return;
            }

            if (labelModel.Text == "_")
            {
                MessageBox.Show("模型不存在，请新建模型或导入模型！");
                return;
            }

            if (File.Exists(TLSSVRModelPath))
            {
                File.Delete(TLSSVRModelPath);
                SaveModelFun();
            }
            else
            {
                saveFileDialog1.InitialDirectory = TLSSVRModelPath;
                saveFileDialog1.Filter = "模型文件(*.LSSVMSSmodel)|*.LSSVMSSmodel|所有文件|*.*";
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string filename = saveFileDialog1.FileName;
                    TLSSVRModelPath = filename;

                    if (File.Exists(TLSSVRModelPath))
                        File.Delete(TLSSVRModelPath);

                    SaveModelFun();
                }
            }
        }

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
            if (Train_notFlag == 1)
            {
                //测试模型

                chart1.ChartAreas.Clear();
                chart1.Series.Clear();
                chart1.ChartAreas.Add("测试");
                chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                chart1.Series.Add("测试");
                chart1.Series.Add("原始");
                chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                if (comboBox1.Text == "")
                {
                    MessageBox.Show("请选择要测试的曲线！");
                    return;
                }

                int ChartFlag = 0;
                for (int i = 0; i < LsSvm.outNum; i++)
                    if (CvName[i] == comboBox1.Text)
                        ChartFlag = i;

                ////训练数据显示  归一化后的数据
                //for (int k = 0; k < LsSvm.sampleNum; k++)
                //{
                //    chart1.Series[0].Points.AddXY(k + 1, LsSvm.yc[ChartFlag, k]);
                //    chart1.Series[1].Points.AddXY(k + 1, LsSvm.yd[ChartFlag, k]);
                //}

                ////测试数据显示 归一化后的数据
                //for (int m = 0; m < LsSvm.TestSampleNum; m++)
                //{
                //    chart1.Series[0].Points.AddXY(m + 1 + LsSvm.sampleNum, LsSvm.Testyc[ChartFlag, m]);
                //    chart1.Series[1].Points.AddXY(m + 1 + LsSvm.sampleNum, LsSvm.Testy[ChartFlag, m]);
                //}


                //DataSheet1 datasheet1 = new DataSheet1();
                //datasheet1 = LoadData(DataSheetPath, 100, 0);// 100%的数据用于训练 0%的用于测试

                ////chart1.ChartAreas.Clear();
                ////chart1.Series.Clear();
                ////chart1.ChartAreas.Add("测试");
                ////chart1.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                ////chart1.Series.Add("测试");
                ////chart1.Series.Add("原始");
                //chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                for (int i = 0; i < LsSvm.sampleNum + LsSvm.TestSampleNum; i++)
                {
                    chart1.Series[0].Points.AddXY(i + 1, LsSvm.fgyh_yc[ChartFlag, i]);
                    //chart1.Series[1].Points.AddXY(i + 1, datasheet1.TrainOutputValue[ChartFlag, i]);
                    chart1.Series[1].Points.AddXY(i + 1, LsSvm.AllOutData[ChartFlag, i]);
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

                chart1.ResetAutoValues();
                chart1.Invalidate();

                //chart1.ResetAutoValues();
                //chart1.Invalidate();
            }
            else
            {
                MessageBox.Show("请先训练模型！");
                return;
            }
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
                CText.Text = LsSvm.C[0].ToString();
                //读取训练参数Deta
                for (int i = 0; i < LsSvm.outNum; i++)
                    LsSvm.Deta[i] = Convert.ToDouble(s1[8 + LsSvm.outNum].Trim().Split(' ')[i]);
                DetaText.Text = LsSvm.Deta[0].ToString();
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
                ChangeCDetaFlag = 1;
                MessageBox.Show("已加载模型参数文件！");
            }
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
        static DataSheetTao DST = new DataSheetTao();

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
            //}
            //else
            //{
            //    if (labelOriginalDt.Text != s1[0].Trim().Split(' ')[5])
            //    {
            //        MessageBox.Show("数据表文件不存在,请先导入名称为“" + s1[0].Trim().Split(' ')[5] + "”的数据表！");
            //        return;
            //    }
            //    else
            //    {
            //        for (int j = 0; j < DST.OutputNum; j++)
            //        {
            //            for (int i = 0; i < DST.InputNum; i++)
            //            {
            //                string[] oneLine = s1[2 + j * (DST.InputNum + 2) + 2 + i].Trim().Split(' ');
            //                DST.TaoD[DST.InputNum * j + i] = Convert.ToInt16(oneLine[0]);
            //                DST.TaoR[DST.InputNum * j + i] = Convert.ToInt16(oneLine[1]);
            //            }
            //        }
            //    }
            //}
            sr.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           
            this.Dispose();

            this.Close();
        }


    }
}
