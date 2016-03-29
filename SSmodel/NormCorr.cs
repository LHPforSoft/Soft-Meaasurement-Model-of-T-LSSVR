using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSmodel
{
    public class NormCorr
    {
        public NormCorr()
        { }



        public double NormCORR(double[] X, double[] Y, int Start, int LENGTH, int DelayMax)
        {
            double returnDelay = 0;

            double[] ImportentVal = new double[DelayMax + 3];

            Matrix MATRIX = new Matrix();
            //归一化操作
            double averageX = MATRIX.AverageVal(X);//得到所有数据的平均值 使其具有统一的标准
            double averageY = MATRIX.AverageVal(Y);
            double[] normX = new double[X.Length];// 归一化后的数据
            double[] normY = new double[Y.Length];
            for (int i = 0; i < LENGTH; i++)
            {
                normX[i] = X[i] - averageX;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据                
            }
            for (int i = 0; i < Y.Length; i++)
            {
                normY[i] = Y[i] - averageY;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据                
            }
            double[] A = new double[LENGTH];//利用归一化数据进行互相关操作
            double[] B = new double[LENGTH];
            double C;
            double[] Cc = new double[DelayMax];//相关系数
            A = normX;
            for (int i = 0; i < DelayMax; i++)
            {
                for (int j = 0; j < LENGTH; j++)//求得Y中第i+Start个数作为起始的 长度为LENGTH的归一化数据
                {
                    B[j] = normY[(i + Start) + j];//  Y[(i + K) + j] - averageY;
                }
                C = (MATRIX.SumDianCheng(A, A)) * (MATRIX.SumDianCheng(B, B));
                Cc[i] = (MATRIX.SumDianCheng(A, B)) / (Math.Sqrt(C));
                ImportentVal[i] = Cc[i];
            }
            double[] absVal = MATRIX.AbsRow(Cc);
            int Xc = MATRIX.Max_Xc(absVal);
            double coxx = Cc[Xc];
            int delay = Xc;
            ImportentVal[DelayMax] = Xc;
            ImportentVal[DelayMax + 1] = coxx;
            ImportentVal[DelayMax + 2] = delay;

            return returnDelay = delay;
        }

        //针对生料细度的  TaoR
        public double TaoRCORR(double[] cX, double[] cY, int Start, int LENGTH, int DelayMax)
        {
            double returnDelay = 0;
            Matrix MATRIX = new Matrix();
            //归一化操作
            double averageY = MATRIX.AverageVal(cY);
            double[] normY = new double[cY.Length];
            for (int i = 0; i < cY.Length; i++)
            {
                normY[i] = cY[i] - averageY;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据
            }
            double[] A = new double[LENGTH];//利用归一化数据进行互相关操作
            double[] B = new double[LENGTH];
            double C;
            double[] Cc = new double[DelayMax];//相关系数
            //归一化操作
            double averageX;//得到所有数据的平均值 使其具有统一的标准
            double[] normX;// 归一化后的数据
            for (int i = 0; i < DelayMax; i++)
            {
                int LG = cY.GetLength(0);
                //得到某一个与细度对应时刻的所有X值
                double[] us = new double[LG];
                for (int m = 0; m < LG; m++)
                {
                    us[m] = cX[24 * (m + 1) - 1 - i];
                }
                averageX = MATRIX.AverageVal(us);
                normX = new double[us.Length];
                for (int n = 0; n < us.Length; n++)
                {
                    normX[n] = us[n] - averageX;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据
                }
                for (int j = 0; j < LENGTH; j++)//求得Y中第i+Start个数作为起始的 长度为LENGTH的归一化数据
                {
                    A[j] = normX[Start + j];
                    B[j] = normY[Start + j];//  Y[(i + K) + j] - averageY;
                }
                C = (MATRIX.SumDianCheng(A, A)) * (MATRIX.SumDianCheng(B, B));
                Cc[i] = (MATRIX.SumDianCheng(A, B)) / (Math.Sqrt(C));

            }

            double[] absVal = MATRIX.AbsRow(Cc);
            int Xc = MATRIX.Max_Xc(absVal);
            double coxx = Cc[Xc];
            returnDelay = Xc;
            return returnDelay;
        }


        //针对生料细度的  TaoD
        public double TaoDCORR(double[] cX, double[] cY, int Start, int LENGTH, int DelayMax)
        {
            double returnDelay = 0;
            Matrix MATRIX = new Matrix();
            //归一化操作
            double averageY = MATRIX.AverageVal(cY);
            double[] normY = new double[cY.Length];
            for (int i = 0; i < cY.Length; i++)
            {
                normY[i] = cY[i] - averageY;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据
            }
            double[] A = new double[LENGTH];//利用归一化数据进行互相关操作
            double[] B = new double[LENGTH];
            double C;
            double[] Cc = new double[DelayMax];//相关系数

            //归一化操作
            double averageX;//得到所有数据的平均值 使其具有统一的标准
            double[] normX;// 归一化后的数据
            for (int i = 0; i < DelayMax; i++)
            {
                int LG = cY.GetLength(0) + 1;
                //得到某一个与细度对应时刻的所有X值
                double[] us = new double[LG];
                for (int m = 0; m < LG; m++)
                {
                    us[m] = cX[24 * (m + 1) - 1 - i];
                }
                averageX = MATRIX.AverageVal(us);
                normX = new double[us.Length];
                for (int n = 0; n < us.Length; n++)
                {
                    normX[n] = us[n] - averageX;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据
                }
                for (int j = 0; j < LENGTH; j++)//求得Y中第i+Start个数作为起始的 长度为LENGTH的归一化数据
                {
                    A[j] = normX[Start + j];
                    B[j] = normY[Start + j];//  Y[(i + K) + j] - averageY;
                }
                C = (MATRIX.SumDianCheng(A, A)) * (MATRIX.SumDianCheng(B, B));
                Cc[i] = (MATRIX.SumDianCheng(A, B)) / (Math.Sqrt(C));

            }

            double[] absVal = MATRIX.AbsRow(Cc);
            int Xc = MATRIX.Max_Xc(absVal);
            double coxx = Cc[Xc];
            returnDelay = Xc;
            return returnDelay;


            //double returnDelay = 0;

            //double[] ImportentVal = new double[DelayMax + 3];

            //Matrix MATRIX = new Matrix();
            ////归一化操作
            //double averageX = MATRIX.AverageVal(cX);//得到所有数据的平均值 使其具有统一的标准
            //double averageY = MATRIX.AverageVal(cY);
            //double[] normX = new double[cX.Length];// 归一化后的数据
            //double[] normY = new double[cY.Length];
            //for (int i = 0; i < LENGTH; i++)
            //{
            //    normX[i] = cX[i] - averageX;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据                
            //}
            //for (int i = 0; i < cY.Length; i++)
            //{
            //    normY[i] = cY[i] - averageY;//求得X中第K个数作为起始的 长度为LENGTH的归一化数据                
            //}
            //double[] A = new double[LENGTH];//利用归一化数据进行互相关操作
            //double[] B = new double[LENGTH];
            //double C;
            //double[] Cc = new double[DelayMax];//相关系数
            //A = normX;
            //for (int i = 0; i < DelayMax; i++)
            //{
            //    for (int j = 0; j < LENGTH; j++)//求得Y中第i+Start个数作为起始的 长度为LENGTH的归一化数据
            //    {
            //        B[j] = normY[(i + Start) + j];//  Y[(i + K) + j] - averageY;
            //    }
            //    C = (MATRIX.SumDianCheng(A, A)) * (MATRIX.SumDianCheng(B, B));
            //    Cc[i] = (MATRIX.SumDianCheng(A, B)) / (Math.Sqrt(C));
            //    ImportentVal[i] = Cc[i];
            //}
            //double[] absVal = MATRIX.AbsRow(Cc);
            //int Xc = MATRIX.Max_Xc(absVal);
            //double coxx = Cc[Xc];
            //int delay = Xc;
            //ImportentVal[DelayMax] = Xc;
            //ImportentVal[DelayMax + 1] = coxx;
            //ImportentVal[DelayMax + 2] = delay;

            //return returnDelay = delay;
        }

    }
}
