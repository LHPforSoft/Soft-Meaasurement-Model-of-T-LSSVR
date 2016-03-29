using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSmodel
{
    public class LSSVM
    {
        public int inNum;//输入变量的 变量个数
        public int outNum;//输出变量的 变量个数
        public int sampleNum;//训练 样本 个数
        public int TestSampleNum;//测试 样本 个数
        public double[,] a;
        public double[] b;

        public double[,] x;   //x训练归一化后输入数据
        public double[,] yd;  //训练归一化后输出数据
        public double[,] yc;  //训练辨识输出数据

        public double[,] fgyh_yc;

        public double[,] K;   //径向基核函数
        public double[,] A22;
        public double[,] A;
        public double[] B;

        public double[] C;//正常数C，表示模型泛化能力和精度之间的一个折中参数
        public double[] Deta;//核函数deta

        public double[,] Testx;//测试归一化后输入数据
        public double[,] Testy;//测试归一化后输出数据
        public double[,] Testyc;//测试辨识输出数据

        public double[] AllDataInputMax;
        public double[] AllDataOutputMax;
        public double[] AllDataInputMin;
        public double[] AllDataOutputMin;

        public double[] All_in_rata;
        public double[] All_out_rata;

        public double[] InputMax;//训练样本输入数据最大值
        public double[] OutputMax;//训练样本输出数据最大值
        public double[] InputMin;//训练样本输入数据最小值
        public double[] OutputMin;//训练样本输出数据最小值

        public double[] TestInputMax;//测试样本输入数据最大值
        public double[] TestOutputMax;//测试样本输出数据最大值
        public double[] TestInputMin;//测试样本输入数据最小值
        public double[] TestOutputMin;//测试样本输出数据最小值

        public double[] in_rate;//训练样本输入数据归一化比例系数
        public double[] out_rate;//训练样本输出数据归一化比例系数

        public double[] Test_in_rate;//测试样本输入数据归一化比例系数
        public double[] Test_out_rate;//测试样本输出数据归一化比例系数

        public double[,] TrainInData;
        public double[,] TrainOutData;
        double[,] TestInData;
        double[,] TestOutData;
        public double[,] AllInData;
        public double[,] AllOutData;

        public LSSVM()
        { }

        public void LoadPar()
        {
            a = new double[outNum, sampleNum];
            b = new double[outNum];
            C = new double[outNum];
            Deta = new double[outNum];
            InputMin = new double[inNum];
            OutputMin = new double[outNum];
            in_rate = new double[inNum];
            out_rate = new double[outNum];
            All_in_rata = new double[inNum];
            All_out_rata = new double[outNum];
            AllDataInputMin = new double[inNum];
            AllDataOutputMin = new double[outNum];
            x = new double[inNum, sampleNum];
        }

        public void InitiLSSVM(double[,] trainInputData, double[,] trainOutputData, double[,] testInputData, double[,] testOutputData)
        {

            TrainInData = trainInputData;
            TrainOutData = trainOutputData;

            TestInData = testInputData;
            TestOutData = testOutputData;

            this.inNum = trainInputData.GetLength(0);
            this.outNum = trainOutputData.GetLength(0);

            this.sampleNum = trainInputData.GetLength(1);
            this.TestSampleNum = testInputData.GetLength(1);

            AllInData = new double[inNum, sampleNum + TestSampleNum];
            AllOutData = new double[outNum, sampleNum + TestSampleNum];
            for (int i = 0; i < sampleNum + TestSampleNum; i++)
            {
                if (i < sampleNum)
                {
                    for (int j = 0; j < inNum; j++)
                    {
                        AllInData[j, i] = TrainInData[j, i];
                    }
                    for (int j = 0; j < outNum; j++)
                    {
                        AllOutData[j, i] = TrainOutData[j, i];
                    }
                }
                else
                {
                    for (int j = 0; j < inNum; j++)
                    {
                        AllInData[j, i] = TestInData[j, i - sampleNum];
                    }
                    for (int j = 0; j < outNum; j++)
                    {
                        AllOutData[j, i] = TestOutData[j, i - sampleNum];
                    }
                }
            }

            K = new double[sampleNum, sampleNum];
            A22 = new double[sampleNum, sampleNum];
            A = new double[sampleNum + 1, sampleNum + 1];
            B = new double[sampleNum + 1];
            C = new double[outNum];
            Deta = new double[outNum];

            x = new double[inNum, sampleNum];

            Testx = new double[inNum, TestSampleNum];//测试输入
            Testy = new double[outNum, TestSampleNum];//测试输出  实际值
            Testyc = new double[outNum, TestSampleNum];//测试输出的  预测值？

            a = new double[outNum, sampleNum];
            b = new double[outNum];
            InputMin = new double[inNum];
            OutputMin = new double[outNum];
            in_rate = new double[inNum];
            out_rate = new double[outNum];

            yd = new double[outNum, sampleNum];
            yc = new double[outNum, sampleNum];
            fgyh_yc = new double[outNum, sampleNum + TestSampleNum];

            AllDataInputMax = new double[inNum];
            AllDataInputMin = new double[inNum];
            AllDataOutputMax = new double[outNum];
            AllDataOutputMin = new double[outNum];
            All_in_rata = new double[inNum];
            All_out_rata = new double[outNum];

            InputMax = new double[inNum];
            OutputMax = new double[outNum];
            InputMin = new double[inNum];
            OutputMin = new double[outNum];
            in_rate = new double[inNum];
            out_rate = new double[outNum];

            TestInputMax = new double[inNum];
            TestOutputMax = new double[outNum];
            TestInputMin = new double[inNum];
            TestOutputMin = new double[outNum];
            Test_in_rate = new double[inNum];
            Test_out_rate = new double[outNum];

            //求训练样本中的最大值;确定归一化系数。

            for (int i = 0; i < inNum; i++)
            {
                InputMax[i] = TrainInData[i, 0];
                InputMin[i] = TrainInData[i, 0];
            }
            for (int i = 0; i < outNum; i++)
            {

                OutputMax[i] = TrainOutData[i, 0];
                OutputMin[i] = TrainOutData[i, 0];
            }

            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                for (int i = 0; i < inNum; i++)
                {
                    if (TrainInData[i, isamp] > InputMax[i])
                        InputMax[i] = TrainInData[i, isamp];
                    if (TrainInData[i, isamp] < InputMin[i])
                        InputMin[i] = TrainInData[i, isamp];
                }

                for (int i = 0; i < outNum; i++)
                {
                    if (TrainOutData[i, isamp] > OutputMax[i])
                        OutputMax[i] = TrainOutData[i, isamp];
                    if (TrainOutData[i, isamp] < OutputMin[i])
                        OutputMin[i] = TrainOutData[i, isamp];
                }

            }
            //数据归一化比例系数
            for (int i = 0; i < inNum; i++)
            {
                in_rate[i] = InputMax[i] - InputMin[i];
                if (in_rate[i] == 0)
                    in_rate[i] = 1;
            }

            for (int i = 0; i < outNum; i++)
            {
                out_rate[i] = OutputMax[i] - OutputMin[i];
                if (out_rate[i] == 0)
                    out_rate[i] = 1;
            }

            //求测试样本中的最大值;确定归一化系数。

            if (TestInData.GetLength(1) == 0)
            {
            }
            else
            {
                for (int i = 0; i < inNum; i++)
                {
                    TestInputMax[i] = TestInData[i, 0];
                    TestInputMin[i] = TestInData[i, 0];
                }
                for (int i = 0; i < outNum; i++)
                {

                    TestOutputMax[i] = TestOutData[i, 0];
                    TestOutputMin[i] = TestOutData[i, 0];
                }

                for (int isamp = 0; isamp < TestSampleNum; isamp++)
                {
                    for (int i = 0; i < inNum; i++)
                    {
                        if (TestInData[i, isamp] > TestInputMax[i])
                            TestInputMax[i] = TestInData[i, isamp];
                        if (TestInData[i, isamp] < TestInputMin[i])
                            TestInputMin[i] = TestInData[i, isamp];
                    }

                    for (int i = 0; i < outNum; i++)
                    {
                        if (TestOutData[i, isamp] > TestOutputMax[i])
                            TestOutputMax[i] = TestOutData[i, isamp];
                        if (TestOutData[i, isamp] < TestOutputMin[i])
                            TestOutputMin[i] = TestOutData[i, isamp];
                    }

                }

                for (int i = 0; i < inNum; i++)
                {
                    Test_in_rate[i] = TestInputMax[i] - TestInputMin[i];
                    if (Test_in_rate[i] == 0)
                        Test_in_rate[i] = 1;
                }
                for (int i = 0; i < outNum; i++)
                {
                    Test_out_rate[i] = TestOutputMax[i] - TestOutputMin[i];
                    if (Test_out_rate[i] == 0)
                        Test_out_rate[i] = 1;
                }


            }
            //所有数据归一化
            for (int i = 0; i < inNum; i++)
            {
                if (InputMax[i] > TestInputMax[i])
                    AllDataInputMax[i] = InputMax[i];
                if (InputMax[i] < TestInputMax[i])
                    AllDataInputMax[i] = TestInputMax[i];
                if (InputMax[i] == TestInputMax[i])
                    AllDataInputMax[i] = InputMax[i];

                if (InputMin[i] > TestInputMin[i])
                    AllDataInputMin[i] = TestInputMin[i];
                if (InputMin[i] < TestInputMin[i])
                    AllDataInputMin[i] = InputMin[i];
                if (InputMin[i] == TestInputMin[i])
                    AllDataInputMin[i] = InputMin[i];

                All_in_rata[i] = AllDataInputMax[i] - AllDataInputMin[i];
                if (All_in_rata[i] == 0)
                    All_in_rata[i] = 1;
            }
            for (int i = 0; i < outNum; i++)
            {
                if (OutputMax[i] > TestOutputMax[i])
                    AllDataOutputMax[i] = OutputMax[i];
                if (OutputMax[i] < TestOutputMax[i])
                    AllDataOutputMax[i] = TestOutputMax[i];
                if (OutputMax[i] == TestOutputMax[i])
                    AllDataOutputMax[i] = OutputMax[i];

                if (OutputMin[i] > TestOutputMin[i])
                    AllDataOutputMin[i] = TestOutputMin[i];
                if (OutputMin[i] < TestOutputMin[i])
                    AllDataOutputMin[i] = OutputMin[i];
                if (OutputMin[i] == TestOutputMin[i])
                    AllDataOutputMin[i] = OutputMin[i];

                All_out_rata[i] = AllDataOutputMax[i] - AllDataOutputMin[i];
                if (All_out_rata[i] == 0)
                    All_out_rata[i] = 1;
            }
        }

        public void LSSVMTrain()
        {
            double[] ya = new double[sampleNum];
            double cc;
            double detar;

            //x;   //训练归一化后输入数据
            //yd;  //训练归一化后输出数据
            //yc;  //训练辨识输出数据

            //for (int isamp = 0; isamp < sampleNum; isamp++)
            //{
            //    //数据归一化
            //    for (int i = 0; i < inNum; i++)
            //        x[i,isamp] = (TrainInData[i, isamp] - InputMin[i]) / in_rate[i];

            //    for (int i = 0; i < outNum; i++)
            //        yd[i,isamp] = (TrainOutData[i, isamp] - OutputMin[i]) / out_rate[i];
            //}

            for (int isamp = 0; isamp < sampleNum; isamp++)
            {
                //数据归一化
                for (int i = 0; i < inNum; i++)
                    x[i, isamp] = (TrainInData[i, isamp] - AllDataInputMin[i]) / All_in_rata[i];

                for (int i = 0; i < outNum; i++)
                    yd[i, isamp] = (TrainOutData[i, isamp] - AllDataOutputMin[i]) / All_out_rata[i];
            }



            //求取参数a，b
            for (int tt = 0; tt < outNum; tt++)
            {
                for (int j = 0; j < sampleNum; j++)
                {
                    ya[j] = yd[tt, j];
                }
                cc = C[tt];
                detar = Deta[tt];
                double[] X;
                X = lssvmident(x, ya, cc, detar);
                b[tt] = X[0];
                for (int mm = 1; mm < sampleNum + 1; mm++)
                    a[tt, mm - 1] = X[mm];
            }



            //辨识求解输出yc
            double[] uu = new double[inNum];
            double[] ycc = new double[outNum];
            for (int r = 0; r < sampleNum; r++)
            {
                for (int h = 0; h < inNum; h++)
                    uu[h] = x[h, r];                //x训练归一化后输入数据
                ycc = lssvm(uu);                     //yd;训练归一化后输出数据
                for (int i = 0; i < outNum; i++)
                    yc[i, r] = ycc[i];              //yc; 训练辨识输出数据
            }

            //测试数据输出testyc

            //for (int isamp = 0; isamp <  TestSampleNum; isamp++)
            //{
            //    //测试数据归一化
            //    for (int i = 0; i < inNum; i++)
            //        Testx[i, isamp] = (TestInData[i, isamp] - TestInputMin[i]) / Test_in_rate[i];
            //       // x[i, isamp] = (TrainInData[i, isamp] - InputMin[i]) / in_rate[i];

            //    for (int i = 0; i < outNum; i++)
            //        Testy[i, isamp] = (TestOutData[i, isamp] - TestOutputMin[i]) / Test_out_rate[i];
            //        //yd[i, isamp] = (TrainOutData[i, isamp] - OutputMin[i]) / out_rate[i];
            //}

            for (int isamp = 0; isamp < TestSampleNum; isamp++)
            {
                //测试数据归一化
                for (int i = 0; i < inNum; i++)
                    Testx[i, isamp] = (TestInData[i, isamp] - AllDataInputMin[i]) / All_in_rata[i];
                for (int i = 0; i < outNum; i++)
                    Testy[i, isamp] = (TestOutData[i, isamp] - AllDataOutputMin[i]) / All_out_rata[i];
            }


            for (int kk = 0; kk < TestSampleNum; kk++)
            {
                for (int m = 0; m < inNum; m++)
                    uu[m] = Testx[m, kk];
                ycc = lssvm(uu);                            //泛化能力验证时候得到的ycc
                for (int k = 0; k < outNum; k++)
                    Testyc[k, kk] = ycc[k];
            }


            //数据反归一化
            for (int i = 0; i < outNum; i++)
            {
                for (int j = 0; j < sampleNum; j++)
                {
                    fgyh_yc[i, j] = yc[i, j] * All_out_rata[i] + AllDataOutputMin[i];
                }
                for (int jj = 0; jj < TestSampleNum; jj++)
                {
                    fgyh_yc[i, jj + sampleNum] = Testyc[i, jj] * All_out_rata[i] + AllDataOutputMin[i];
                }
            }
        }

        //求解参数a，b的函数
        public double[] lssvmident(double[,] xx, double[] y1, double c, double deta)
        {
            double[] x1 = new double[inNum];
            double[] x2 = new double[inNum];
            double[] x12 = new double[inNum];
            double[] X11 = new double[sampleNum + 1];
            double[,] AA = new double[sampleNum + 1, sampleNum + 1];

            //构造核函数K
            for (int i = 0; i < sampleNum; i++)
            {
                for (int ii = 0; ii < inNum; ii++)
                    x1[ii] = xx[ii, i];
                for (int j = 0; j < sampleNum; j++)
                {
                    double x21;
                    x21 = 0;
                    for (int jj = 0; jj < inNum; jj++)
                    {
                        x2[jj] = xx[jj, j];
                        x12[jj] = x1[jj] - x2[jj];
                        x21 += x12[jj] * x12[jj];//可能会出现循环问题
                    }
                    K[i, j] = Math.Exp(-x21 / deta);
                }
            }
            //构造核矩阵
            double[,] II = getLxx(sampleNum);  //构造单位矩阵 A22的后半部分的一部分
            double[,] III = new double[sampleNum, sampleNum];// A22中的后半部分
            for (int m = 0; m < sampleNum; m++)
                for (int n = 0; n < sampleNum; n++)
                    III[m, n] = II[m, n] * (1 / c);
            A22 = add(K, III);

            for (int i = 0; i < sampleNum + 1; i++)
            {
                for (int j = 0; j < sampleNum + 1; j++)
                {
                    if (i == 0 && j == 0)
                        A[i, j] = 0;
                    if (i == 0 && j != 0)
                        A[i, j] = 1;
                    if (i != 0 && j == 0)
                        A[i, j] = 1;
                    if (i != 0 && j != 0)
                        A[i, j] = A22[i - 1, j - 1];
                }
                if (i == 0)
                    B[i] = 0;
                else
                    B[i] = y1[i - 1];
            }
            //求解参数 a，b;X = mun(inv(A), B);
            AA = inv(A);
            for (int i = 0; i < sampleNum + 1; i++)
            {
                double XX;
                XX = 0;
                for (int j = 0; j < sampleNum + 1; j++)
                {
                    XX += AA[i, j] * B[j];
                }
                X11[i] = XX;
            }

            return X11;
        }

        //求解辨识输出yc；
        public double[] lssvm(double[] u)
        {
            double[] aK = new double[outNum];  // size 1
            //aK = 0;
            double[] yc1 = new double[outNum];  // size 1
            //yc1 = 0;
            double[] us1 = new double[inNum];  // size 28
            double[] us2 = new double[inNum];  // size 28
            double[] aa = new double[sampleNum];  // size 104
            double detar;
            detar = 0;
            double bb;
            bb = 0;
            for (int r = 0; r < outNum; r++)
            {
                for (int j = 0; j < sampleNum; j++)
                    aa[j] = a[r, j];
                detar = Deta[r];
                bb = b[r];
                for (int i = 0; i < sampleNum; i++)
                {
                    double us22;
                    us22 = 0;
                    for (int ii = 0; ii < inNum; ii++)
                    {
                        us1[ii] = x[ii, i];
                        us2[ii] = u[ii] - us1[ii];
                        us22 += us2[ii] * us2[ii];
                    }
                    aK[r] += aa[i] * Math.Exp(-us22 / detar);
                }
                yc1[r] = aK[r] + bb;

            }
            return yc1;
        }

        //求解辨识输出yc-----------自己更改的；
        public double[] lssvmforecast(double[] u, double[,] xTrainGyh, double[,] a, double[] b, double[] deta)
        {
            double[] aK = new double[outNum];
            //aK = 0;
            double[] yc1 = new double[outNum];
            //yc1 = 0;
            double[] us1 = new double[inNum];
            double[] us2 = new double[inNum];
            double[] aa = new double[sampleNum];
            double detar;
            detar = 0;
            double bb;
            bb = 0;
            for (int r = 0; r < outNum; r++)
            {
                for (int j = 0; j < sampleNum; j++)
                    aa[j] = a[r, j];
                detar = Deta[r];
                bb = b[r];
                for (int i = 0; i < sampleNum; i++)
                {
                    double us22;
                    us22 = 0;
                    for (int ii = 0; ii < inNum; ii++)
                    {
                        us1[ii] = x[ii, i];
                        us2[ii] = u[ii] - us1[ii];
                        us22 += us2[ii] * us2[ii];
                    }
                    aK[r] += aa[i] * Math.Exp(-us22 / detar);
                }
                yc1[r] = aK[r] + bb;

            }
            return yc1;
        }

        public double[] gyh(double[] u, double[] maxOfOneVar, double[] minOfOneVar)//将输入变量进行归一化 u 28*1
        {
            double[] gyhU;
            int lengthOfU = u.Length;
            gyhU = new double[lengthOfU];


            //数据归一化
            for (int i = 0; i < lengthOfU; i++)
            {
                gyhU[i] = (u[i] - minOfOneVar[i]) / (maxOfOneVar[i] - minOfOneVar[i]);

            }
            return gyhU;
        }


        public double[] fgyh(double[] u, double[] maxOfOneVar, double[] minOfOneVar)//将输入变量进行归一化 u 28*1
        {
            double[] fgyhU;
            int lengthOfU = u.Length;

            fgyhU = new double[lengthOfU];

            fgyhU[0] = u[0] * All_out_rata[0] + AllDataOutputMin[0];

            return fgyhU;
        }

        //********************************************************
        public int min;
        private int[,] Order(int[,] list) //降序排序
        {
            int[,] odt = new int[list.GetLength(0), list.GetLength(1)];
            for (int d = 0; d < list.GetLength(0); d++)
                for (int i = 0; i < list.GetLength(1); i++)
                    odt[d, i] = list[d, i];
            for (int d = 0; d < list.GetLength(0); d++)
            {

                for (int i = 0; i < list.GetLength(1) - 1; i++)
                {
                    min = i;
                    for (int j = i + 1; j < list.GetLength(1); j++)
                    {
                        if (odt[d, j] > odt[d, min])
                            min = j;
                    }
                    int t = odt[d, min];
                    odt[d, min] = odt[d, i];
                    odt[d, i] = t;
                }
            }
            return odt;
        }
        private int[,] ord(int[,] list)//排列序数
        {
            int[,] ord = new int[list.GetLength(0), list.GetLength(1)];
            for (int d = 0; d < list.GetLength(0); d++)
            {
                int[,] dt1 = new int[2, list.GetLength(1)];
                for (int j = 0; j < list.GetLength(1); j++)
                {
                    dt1[0, j] = list[d, j];
                    dt1[1, j] = j;
                }
                for (int i = 0; i < list.GetLength(1) - 1; i++)
                {
                    min = i;
                    for (int j = i + 1; j < list.GetLength(1); j++)
                    {
                        if (dt1[0, j] > dt1[0, min])
                            min = j;
                    }
                    //max[0, i] = min;
                    for (int a = 0; a < 2; a++)
                    {
                        int t = dt1[a, min];
                        dt1[a, min] = dt1[a, i];
                        dt1[a, i] = t;
                    }
                }
                for (int j = 0; j < list.GetLength(1); j++)
                    ord[d, j] = dt1[1, j];
            }
            return ord;
        }
        //单位矩阵
        private double[,] getLxx(int n)
        {
            double[,] I = new double[n, n];
            for (int e = 0; e < n; e++)
                for (int r = 0; r < n; r++)
                    I[e, r] = ((e == r) ? 1 : 0);
            return I;
        }
        private int checkdata(int n, int m)
        {
            if (n <= 0)
            {
                MessageBox.Show("number of variables<=0");
                return 1;
            }
            if (m < 0)
            {
                MessageBox.Show("number of equality constraints< 0");
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //矩阵加法
        private double[,] add(double[,] m, double[,] n)
        {
            int aa = m.GetLength(0);
            int bb = m.GetLength(1);
            int cc = n.GetLength(0);
            int dd = n.GetLength(1);
            double[,] add = new double[aa, bb];
            if (aa == cc & bb == dd)
            {
                for (int i = 0; i < aa; i++)
                    for (int j = 0; j < bb; j++)
                    {
                        add[i, j] = m[i, j] + n[i, j];
                    }
                return add;
            }
            else
            {
                MessageBox.Show("相减数组维数不匹配");
                return add;
            }
        }
        //矩阵减法
        private double[,] sub(double[,] m, double[,] n)
        {
            int aa = m.GetLength(0);
            int bb = m.GetLength(1);
            int cc = n.GetLength(0);
            int dd = n.GetLength(1);
            double[,] add = new double[aa, bb];
            if (aa == cc & bb == dd)
            {
                for (int i = 0; i < aa; i++)
                    for (int j = 0; j < bb; j++)
                    {
                        add[i, j] = m[i, j] - n[i, j];
                    }
                return add;
            }
            else
            {
                MessageBox.Show("相减数组维数不匹配");
                return add;
            }
        }
        //求平方
        private double[,] square(double[] x)
        {
            int aa = x.GetLength(0);
            double[,] sq = new double[aa, 1];
            for (int i = 0; i < aa; i++)
            {
                sq[i, 0] = x[i] * x[i];
            }
            return sq;
        }
        //一范数,返回第k列最大值
        private double norm1(double[,] aa, int k)
        {
            double norm1 = 0;
            int mm = aa.GetLength(0);
            int nn = aa.GetLength(1);
            for (int j = 0; j < nn; j++)
            {
                if (Math.Abs(aa[j, k]) > norm1)
                    norm1 = Math.Abs(aa[j, k]);
            }
            return norm1;
        }
        // 二范数
        private double norm(double[] aa)
        {
            double norm = 0;
            int mm = aa.GetLength(0);
            for (int k = 0; k < mm; k++)
                norm += aa[k] * aa[k];
            norm = Math.Sqrt(norm);
            return norm;

        }
        /// 矩阵的行列式的值
        private double det(double[,] a)
        {
            int i, j, k, p, r, m1, n1;
            m1 = a.GetLength(0);
            n1 = a.GetLength(1);
            double X, temp = 1, temp1 = 1, s = 0, s1 = 0;

            if (n1 == 2)
            {
                for (i = 0; i < m1; i++)
                    for (j = 0; j < n1; j++)
                        if ((i + j) % 2 > 0) temp1 *= a[i, j];
                        else temp *= a[i, j];
                X = temp - temp1;
            }
            else
            {
                for (k = 0; k < n1; k++)
                {
                    for (i = 0, j = k; i < m1 && j < n1; i++, j++)
                        temp *= a[i, j];
                    if (m1 - i > 0)
                    {
                        for (p = m1 - i, r = m1 - 1; p > 0; p--, r--)
                            temp *= a[r, p - 1];
                    }
                    s += temp;
                    temp = 1;
                }

                for (k = n1 - 1; k >= 0; k--)
                {
                    for (i = 0, j = k; i < m1 && j >= 0; i++, j--)
                        temp1 *= a[i, j];
                    if (m1 - i > 0)
                    {
                        for (p = m1 - 1, r = i; r < m1; p--, r++)
                            temp1 *= a[r, p];
                    }
                    s1 += temp1;
                    temp1 = 1;
                }

                X = s - s1;
            }
            return X;
        }
        //求转置
        private double[,] Tan(double[,] a)
        {
            int aa = a.GetLength(0);
            int bb = a.GetLength(1);
            double[,] tan = new double[bb, aa];
            for (int i = 0; i < aa; i++)
                for (int j = 0; j < bb; j++)
                    tan[j, i] = a[i, j];
            return tan;
        }
        //求逆
        private double[,] inv(double[,] a0)
        {
            int m = a0.GetLength(0);
            int n = a0.GetLength(1);
            if (m != n)
            {
                Exception myException = new Exception("数组维数不匹配");
                throw myException;
            }
            double[,] b = new double[m, n];
            double[,] a = (double[,])a0.Clone();
            int i, j, row, k;
            double max, temp;
            //单位矩阵
            for (i = 0; i < n; i++)
            {
                b[i, i] = 1;
            }
            for (k = 0; k < n; k++)
            {
                max = 0; row = k;
                //找最大元，其所在行为row
                for (i = k; i < n; i++)
                {
                    temp = Math.Abs(a[i, k]);
                    if (max < temp)
                    {
                        max = temp;
                        row = i;
                    }

                }
                if (max == 0)
                {
                    return a0;
                }
                else
                {
                    //交换k与row行
                    if (row != k)
                    {
                        for (j = 0; j < n; j++)
                        {
                            temp = a[row, j];
                            a[row, j] = a[k, j];
                            a[k, j] = temp;

                            temp = b[row, j];
                            b[row, j] = b[k, j];
                            b[k, j] = temp;
                        }

                    }

                    //首元化为1
                    for (j = k + 1; j < n; j++) a[k, j] /= a[k, k];
                    for (j = 0; j < n; j++) b[k, j] /= a[k, k];

                    a[k, k] = 1;

                    //k列化为0
                    //对a
                    for (j = k + 1; j < n; j++)
                    {
                        for (i = 0; i < k; i++) a[i, j] -= a[i, k] * a[k, j];
                        for (i = k + 1; i < n; i++) a[i, j] -= a[i, k] * a[k, j];
                    }
                    //对b
                    for (j = 0; j < n; j++)
                    {
                        for (i = 0; i < k; i++) b[i, j] -= a[i, k] * b[k, j];
                        for (i = k + 1; i < n; i++) b[i, j] -= a[i, k] * b[k, j];
                    }
                    for (i = 0; i < n; i++) a[i, k] = 0;
                    a[k, k] = 1;
                }

            }
            return b;
        }
        //普通二维矩阵相乘
        private double[,] mun(double[,] m, double[,] n)
        {
            int aa = m.GetLength(0);
            int bb = m.GetLength(1);
            int cc = n.GetLength(0);
            int dd = n.GetLength(1);
            double[,] mun = new double[aa, dd];
            if (bb == cc)
            {
                for (int i = 0; i < aa; i++)
                    for (int j = 0; j < dd; j++)
                    {
                        mun[i, j] = 0;
                        for (int k = 0; k < bb; k++)
                            mun[i, j] += m[i, k] * n[k, j];
                    }
                return mun;
            }
            else
            {
                MessageBox.Show("相乘数组维数不匹配");
                return mun;
            }
        }
        //带k的矩阵相乘
        private double[] munwithk(int k, double[,] m, double[,] n)
        {
            int aa = m.GetLength(0);
            int bb = m.GetLength(1);
            int cc = n.GetLength(0);
            int dd = n.GetLength(1);
            double[] mun = new double[aa];
            //{
            for (int i = 0; i < aa; i++)
            {
                mun[i] = 0;
                for (int j = 0; j < bb; j++)
                    mun[i] += m[i, j] * n[k, j];
            }
            return mun;
            //}
        }

    }
}
