using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSmodel
{
    public partial class Form5 : Form
    {
        private Form4 Form4 { get; set; }

        public Form5()
        {
            InitializeComponent();
        }
         public Form5(Form4 frm)
        {
            InitializeComponent();
            Form4 = frm;
        }

         private void Form5_Load(object sender, EventArgs e)
         {
             DataSheet1 datasheet1 = new DataSheet1();
             datasheet1 = Form4.LoadData(Form4.DataSheetPath, 100, 0);
             TagcomboBox.Items.Clear();
             CVcomboBox.Items.Clear();
             MVcomboBox.Items.Clear();
             for (int i = 0; i < datasheet1.InputNum; i++)
                 MVcomboBox.Items.Add(datasheet1.MVname[i]);
             for (int i = 0; i < datasheet1.OutputNum; i++)
             {
                 TagcomboBox.Items.Add(datasheet1.CVname[i]);
                 CVcomboBox.Items.Add(datasheet1.CVname[i]);
             }

             DataSheetLabel.Text = Form4.Controls["DataSheetLabel"].Text;
             ModelLabel.Text = Form4.Controls["ModelLabel"].Text;
         }

         private void AnalyLSSVMModel_Click(object sender, EventArgs e)
         {
             if (Form4.Controls["DataSheetLabel"].Text == " ")
             {
                 MessageBox.Show("数据表文件不存在，请先导入数据表！");
                 return;
             }

             if (Form4.Controls["ModelLabel"].Text == " ")
             {
                 MessageBox.Show("模型不存在，请先导入模型！");
                 return;
             }

             DataSheet1 datasheet1 = new DataSheet1();
             datasheet1 = Form4.LoadData(Form4.DataSheetPath, 100, 0);

             int ChartFlag = 0;
             if (TagcomboBox.Text == "")
             {
                 MessageBox.Show("请选择要测试的曲线！");
                 return;
             }

             for (int i = 0; i < datasheet1.OutputNum; i++)
                 if (datasheet1.CVname[i] == TagcomboBox.Text)
                     ChartFlag = i;
             
             AnalyChart.ChartAreas.Clear();
             AnalyChart.Series.Clear();
             AnalyChart.ChartAreas.Add("测试");
             AnalyChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
             AnalyChart.ChartAreas[0].AxisX.Minimum = Form4.LsSvm.AllDataOutputMin[ChartFlag] * 0.9;
             //AnalyChart.ChartAreas[0].AxisX.Minimum = Form4.t.OutputMin[ChartFlag] * 0.9;
             AnalyChart.ChartAreas[0].AxisY.Minimum = Form4.LsSvm.AllDataOutputMin[ChartFlag] * 0.9;
             AnalyChart.ChartAreas[0].AxisX.Maximum = Form4.LsSvm.AllDataOutputMax[ChartFlag] * 1.1;
             //AnalyChart.ChartAreas[0].AxisX.Maximum = Form1.bpnet.OutputMax[ChartFlag] * 1.1;
             AnalyChart.ChartAreas[0].AxisY.Maximum = Form4.LsSvm.AllDataOutputMax[ChartFlag] * 1.1; ;
             AnalyChart.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
             AnalyChart.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
             AnalyChart.Series.Add("测试");
             AnalyChart.Series.Add("标准");
             AnalyChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
             AnalyChart.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
             AnalyChart.Series[1].Points.AddXY(AnalyChart.ChartAreas[0].AxisX.Minimum, AnalyChart.ChartAreas[0].AxisY.Minimum);
             AnalyChart.Series[1].Points.AddXY(AnalyChart.ChartAreas[0].AxisX.Maximum, AnalyChart.ChartAreas[0].AxisY.Maximum);

             for (int i = 0; i < Form4.LsSvm.sampleNum+Form4.LsSvm.TestSampleNum; i++)
             {
                 AnalyChart.Series[0].Points.AddXY(datasheet1.TrainOutputValue[ChartFlag, i], Form4.LsSvm.fgyh_yc[ChartFlag, i]);
 
             }
             AnalyChart.ResetAutoValues();
             AnalyChart.Invalidate();
         }

         private void AnalyGain_Click(object sender, EventArgs e)
         {
             if (Form4.Controls["DataSheetLabel"].Text == " ")
             {
                 MessageBox.Show("数据表文件不存在，请先导入数据表！");
                 return;
             }

             if (Form4.Controls["ModelLabel"].Text == " ")
             {
                 MessageBox.Show("模型不存在，请先导入模型！");
                 return;
             }

             DataSheet1 datasheet1 = new DataSheet1();

             datasheet1 = Form4.LoadData(Form4.DataSheetPath, 100, 0);

             int MVChartFlag = 0;
             int CVChartFlag = 0;

             if (MVcomboBox.Text == "")
             {
                 MessageBox.Show("请选择要测试的MV曲线！");
                 return;
             }

             if (CVcomboBox.Text == "")
             {
                 MessageBox.Show("请选择要测试的CV曲线！");
                 return;
             }
             for (int i = 0; i < datasheet1.InputNum; i++)
                 if (datasheet1.MVname[i] == MVcomboBox.Text)
                     MVChartFlag = i;
             for (int i = 0; i < datasheet1.OutputNum; i++)
                 if (datasheet1.CVname[i] == CVcomboBox.Text)
                     CVChartFlag = i;

             AnalyChart.ChartAreas.Clear();
             AnalyChart.Series.Clear();
             AnalyChart.ChartAreas.Add("测试");
             AnalyChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
             AnalyChart.ChartAreas[0].AxisX.Minimum = Form4.LsSvm.AllDataInputMin[MVChartFlag];
             AnalyChart.ChartAreas[0].AxisY.Minimum = Form4.LsSvm.AllDataOutputMin[CVChartFlag];
             AnalyChart.ChartAreas[0].AxisX.Maximum = Form4.LsSvm.AllDataInputMax[MVChartFlag];
             AnalyChart.ChartAreas[0].AxisY.Maximum = Form4.LsSvm.AllDataOutputMax[CVChartFlag];
             AnalyChart.ChartAreas[0].AxisX.Interval = (AnalyChart.ChartAreas[0].AxisX.Maximum - AnalyChart.ChartAreas[0].AxisX.Minimum) / 5;
             AnalyChart.ChartAreas[0].AxisX.LabelStyle.Format = "F2";
             AnalyChart.ChartAreas[0].AxisY.LabelStyle.Format = "F2";
             AnalyChart.Series.Add("测试");

             AnalyChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

             double[] uu = new double[datasheet1.InputNum];
             double[] uuu = new double[datasheet1.InputNum];
             //double[] aaa = new double[Form4.LsSvm.sampleNum];
             //double bbb;
             //double detar;
             double[] yy = new double[datasheet1.OutputNum];
             double[] fgyhyy = new double[datasheet1.OutputNum];
             for (int i = 0; i < 100; i++)
             {
                 for (int j = 0; j < datasheet1.InputNum; j++)
                 {
                     if (j == MVChartFlag)
                         uu[j] = Form4.LsSvm.AllDataInputMin[j] + i * (Form4.LsSvm.AllDataInputMax[j] - Form4.LsSvm.AllDataInputMin[j]) / 100;
                     else
                         uu[j] = Form4.LsSvm.AllDataInputMin[j];
                     uuu[j] = (uu[j] - Form4.LsSvm.AllDataInputMin[j]) / Form4.LsSvm.All_in_rata[j];
                 }
                 yy = Form4.LsSvm.lssvm(uuu);
                 for (int r = 0; r < datasheet1.OutputNum; r++)
                 {
                     //for (int h = 0; h < Form4.LsSvm.sampleNum; h++)
                         //aaa[h] = Form4.LsSvm.a[r, h];
                     //bbb = Form4.LsSvm.b[r];
                     //detar = Form4.LsSvm.Deta[r];
                     //double yccc = Form4.LsSvm.lssvm(uuu);
                     //yy[r] = yccc;
                     fgyhyy[r] = yy[r] * Form4.LsSvm.All_out_rata[r] + Form4.LsSvm.AllDataOutputMin[r];
                 }
                 AnalyChart.Series[0].Points.AddXY(uu[MVChartFlag], fgyhyy[CVChartFlag]);
             }
             AnalyChart.ResetAutoValues();
             AnalyChart.Invalidate();
         }



    }
}
