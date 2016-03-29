using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace SSmodel
{
    /// <summary>
    /// Matrix 的摘要说明。
    /// 实现矩阵的基本运算
    /// </summary>

    public class Matrix
    {

        public Matrix()
        { }

        public Matrix(int row)
        {
            m_data = new double[row, row];
        }
        //
        // 摘要:
        // 构造方阵
        //

        public Matrix(int row, int col)
        {
            m_data = new double[row, col];
        }
        //复制构造函数
        public Matrix(Matrix m)
        {
            int row = m.Row;
            int col = m.Col;
            m_data = new double[row, col];

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    m_data[i, j] = m[i, j];

        }

        /*
        //分配方阵的大小
        //对于已含有内存的矩阵，将清空数据
        public void SetSize(int row)
        {
            m_data = new double[row,row];
        }

        
        //分配矩阵的大小
        //对于已含有内存的矩阵，将清空数据
        public void SetSize(int row,int col)
        {
            m_data = new double[row,col];
        }
        */

        //求转置
        public double[,] Tan(double[,] a)
        {
            int aa = a.GetLength(0);
            int bb = a.GetLength(1);
            double[,] tan = new double[bb, aa];
            for (int i = 0; i < aa; i++)
                for (int j = 0; j < bb; j++)
                    tan[j, i] = a[i, j];
            return tan;
        }


        public double AverageVal(double[] Xrow)//对单行矩阵求平均值
        {
            int colNum = Xrow.GetLength(0);
            double SUM = 0;
            for (int i = 0; i < colNum; i++)
            {
                SUM += Xrow[i];
            }
            double AVERAGE = SUM / colNum;
            return AVERAGE;
        }
        public double AverageVal(double[] Xrow, int Start, int Length)//对单行矩阵的一部分求平均值
        {
            double SUM = 0;
            for (int i = Start; i < Start + Length; i++)
            {
                SUM += Xrow[i];
            }
            double AVERAGE = SUM / Length;
            return AVERAGE;
        }
        //矩阵点乘再求和
        public double SumDianCheng(double[] Arow, double[] Brow)
        {
            double result = 0;
            if (Arow.GetLength(0) != Brow.GetLength(0))
            {
                MessageBox.Show("进行点乘的两个矩阵大小不同！");
            }
            for (int i = 0; i < Arow.GetLength(0); i++)
            {
                result += Arow[i] * Brow[i];
            }
            return result;
        }
        //对矩阵一行求绝对值，并返回一行
        public double[] AbsRow(double[] Arow)
        {
            double[] NewA = new double[Arow.GetLength(0)];
            for (int i = 0; i < Arow.GetLength(0); i++)
            {
                NewA[i] = Math.Abs(Arow[i]);
            }
            return NewA;
        }
        public int Max_Xc(double[] Arow)
        {
            int Xc = 0;
            double max = Arow[0];
            for (int q = 0; q < Arow.Length; q++)
            {
                if (max < Arow[q])
                {
                    max = Arow[q];
                    Xc = q;
                }
                else
                { }
            }
            return Xc;
        }




        //unit matrix:设为单位阵
        public void SetUnit()
        {
            for (int i = 0; i < m_data.GetLength(0); i++)
                for (int j = 0; j < m_data.GetLength(1); j++)
                    m_data[i, j] = ((i == j) ? 1 : 0);
        }

        //设置元素值
        public void SetValue(double d)
        {
            for (int i = 0; i < m_data.GetLength(0); i++)
                for (int j = 0; j < m_data.GetLength(1); j++)
                    m_data[i, j] = d;
        }

        // Value extraction：返中行数
        public int Row
        {
            get
            {
                return m_data.GetLength(0);
            }
        }

        //返回列数
        public int Col
        {
            get
            {
                return m_data.GetLength(1);
            }
        }

        //重载索引
        //存取数据成员
        public double this[int row, int col]
        {


            get
            {
                return m_data[row, col];
            }
            set
            {
                m_data[row, col] = value;
            }
        }
        //primary change
        //　初等变换　对调两行：ri<-->rj
        public Matrix Exchange(int i, int j)
        {
            double temp;

            for (int k = 0; k < Col; k++)
            {
                temp = m_data[i, k];
                m_data[i, k] = m_data[j, k];
                m_data[j, k] = temp;
            }
            return this;
        }


        //初等变换　第index 行乘以mul
        Matrix Multiple(int index, double mul)
        {
            for (int j = 0; j < Col; j++)
            {
                m_data[index, j] *= mul;
            }
            return this;
        }


        //初等变换 第src行乘以mul加到第index行
        Matrix MultipleAdd(int index, int src, double mul)
        {
            for (int j = 0; j < Col; j++)
            {
                m_data[index, j] += m_data[src, j] * mul;
            }

            return this;
        }

        //transpose 转置
        public Matrix Transpose()
        {
            Matrix ret = new Matrix(Col, Row);

            for (int i = 0; i < Row; i++)
                for (int j = 0; j < Col; j++)
                {
                    ret[j, i] = m_data[i, j];
                }
            return ret;
        }

        //binary addition 矩阵加
        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            if (lhs.Row != rhs.Row)    //异常
            {
                System.Exception e = new Exception("相加的两个矩阵的行数不等");
                throw e;
            }
            if (lhs.Col != rhs.Col)     //异常
            {
                System.Exception e = new Exception("相加的两个矩阵的列数不等");
                throw e;
            }

            int row = lhs.Row;
            int col = lhs.Col;
            Matrix ret = new Matrix(row, col);

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    double d = lhs[i, j] + rhs[i, j];
                    ret[i, j] = d;
                }
            return ret;

        }

        //binary subtraction 矩阵减
        public static Matrix operator -(Matrix lhs, Matrix rhs)
        {
            if (lhs.Row != rhs.Row)    //异常
            {
                System.Exception e = new Exception("相减的两个矩阵的行数不等");
                throw e;
            }
            if (lhs.Col != rhs.Col)     //异常
            {
                System.Exception e = new Exception("相减的两个矩阵的列数不等");
                throw e;
            }

            int row = lhs.Row;
            int col = lhs.Col;
            Matrix ret = new Matrix(row, col);

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    double d = lhs[i, j] - rhs[i, j];
                    ret[i, j] = d;
                }
            return ret;
        }


        //binary multiple 矩阵乘
        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            if (lhs.Col != rhs.Row)    //异常
            {
                System.Exception e = new Exception("相乘的两个矩阵的行列数不匹配");
                throw e;
            }

            Matrix ret = new Matrix(lhs.Row, rhs.Col);
            double temp;
            for (int i = 0; i < lhs.Row; i++)
            {
                for (int j = 0; j < rhs.Col; j++)
                {
                    temp = 0;
                    for (int k = 0; k < lhs.Col; k++)
                    {
                        temp += lhs[i, k] * rhs[k, j];
                    }
                    ret[i, j] = temp;
                }
            }

            return ret;
        }


        //binary division 矩阵除
        public static Matrix operator /(Matrix lhs, Matrix rhs)
        {
            return lhs * rhs.Inverse();
        }

        //unary addition单目加
        public static Matrix operator +(Matrix m)
        {
            Matrix ret = new Matrix(m);
            return ret;
        }

        //unary subtraction 单目减
        public static Matrix operator -(Matrix m)
        {
            Matrix ret = new Matrix(m);
            for (int i = 0; i < ret.Row; i++)
                for (int j = 0; j < ret.Col; j++)
                {
                    ret[i, j] = -ret[i, j];
                }

            return ret;
        }

        //number multiple 数乘
        public static Matrix operator *(double d, Matrix m)
        {
            Matrix ret = new Matrix(m);
            for (int i = 0; i < ret.Row; i++)
                for (int j = 0; j < ret.Col; j++)
                    ret[i, j] *= d;

            return ret;
        }

        //number division 数除
        public static Matrix operator /(double d, Matrix m)
        {
            return d * m.Inverse();
        }

        //功能：返回列主元素的行号
        //参数：row为开始查找的行号
        //说明：在行号[row,Col)范围内查找第row列中绝对值最大的元素，返回所在行号
        int Pivot(int row)
        {
            int index = row;
            for (int i = row + 1; i < Row; i++)
            {
                if (m_data[i, row] > m_data[index, row])
                    index = i;
            }

            return index;
        }

        //inversion 逆阵：使用矩阵的初等变换，列主元素消去法
        public Matrix Inverse()
        {
            if (Row != Col)    //异常,非方阵
            {
                System.Exception e = new Exception("求逆的矩阵不是方阵");
                throw e;
            }
            StreamWriter sw = new StreamWriter("..\\annex\\close_matrix.txt");
            Matrix tmp = new Matrix(this);
            Matrix ret = new Matrix(Row);    //单位阵
            ret.SetUnit();

            int maxIndex;
            double dMul;

            for (int i = 0; i < Row; i++)
            {
                maxIndex = tmp.Pivot(i);

                if (tmp.m_data[maxIndex, i] == 0)
                {
                    System.Exception e = new Exception("求逆的矩阵的行列式的值等于0,");
                    throw e;
                }

                if (maxIndex != i)    //下三角阵中此列的最大值不在当前行，交换
                {
                    tmp.Exchange(i, maxIndex);
                    ret.Exchange(i, maxIndex);

                }

                ret.Multiple(i, 1 / tmp[i, i]);

                tmp.Multiple(i, 1 / tmp[i, i]);

                for (int j = i + 1; j < Row; j++)
                {
                    dMul = -tmp[j, i] / tmp[i, i];
                    tmp.MultipleAdd(j, i, dMul);
                    ret.MultipleAdd(j, i, dMul);

                }
                sw.WriteLine("tmp=\r\n" + tmp);
                sw.WriteLine("ret=\r\n" + ret);
            }//end for


            sw.WriteLine("**=\r\n" + this * ret);

            for (int i = Row - 1; i > 0; i--)
            {
                for (int j = i - 1; j >= 0; j--)
                {
                    dMul = -tmp[j, i] / tmp[i, i];
                    tmp.MultipleAdd(j, i, dMul);
                    ret.MultipleAdd(j, i, dMul);
                }
            }//end for


            sw.WriteLine("tmp=\r\n" + tmp);
            sw.WriteLine("ret=\r\n" + ret);
            sw.WriteLine("***=\r\n" + this * ret);
            sw.Close();

            return ret;

        }//end Inverse

        #region
        /*
        //inversion 逆阵：使用矩阵的初等变换，列主元素消去法
        public Matrix Inverse()
        {
            if(Row != Col)    //异常,非方阵
            {
                System.Exception e = new Exception("求逆的矩阵不是方阵");
               throw e;
            }
            ///////////////
            StreamWriter sw = new StreamWriter("..\\annex\\matrix_mul.txt");
            ////////////////////
            ///    
            Matrix tmp = new Matrix(this);
            Matrix ret =new Matrix(Row);    //单位阵
            ret.SetUnit();

            int maxIndex;
            double dMul;

            for(int i=0;i<Row;i++)
            {

                maxIndex = tmp.Pivot(i);
    
                if(tmp.m_data[maxIndex,i]==0)
                {
                    System.Exception e = new Exception("求逆的矩阵的行列式的值等于0,");
                    throw e;
                }
                if(maxIndex != i)    //下三角阵中此列的最大值不在当前行，交换
              {
                    tmp.Exchange(i,maxIndex);
                    ret.Exchange(i,maxIndex);

                }

                ret.Multiple(i,1/tmp[i,i]);

                /////////////////////////
                //sw.WriteLine("nul \t"+tmp[i,i]+"\t"+ret[i,i]);
                ////////////////
                tmp.Multiple(i,1/tmp[i,i]);
               //sw.WriteLine("mmm \t"+tmp[i,i]+"\t"+ret[i,i]);
                sw.WriteLine("111111111 tmp=\r\n"+tmp);
               for(int j=i+1;j<Row;j++)
               {
                    dMul = -tmp[j,i];
                   tmp.MultipleAdd(j,i,dMul);
                    ret.MultipleAdd(j,i,dMul);
   
                }
             sw.WriteLine("222222222222=\r\n"+tmp);

            }//end for



            for(int i=Row-1;i>0;i--)
            {
                for(int j=i-1;j>=0;j--)
                {
                    dMul = -tmp[j,i];
                    tmp.MultipleAdd(j,i,dMul);
                    ret.MultipleAdd(j,i,dMul);
                }
            }//end for
        
            //////////////////////////


            sw.WriteLine("tmp = \r\n" + tmp.ToString());

            sw.Close();
            ///////////////////////////////////////
           ///
            return ret;
        }//end Inverse

*/

        #endregion

        //determine if the matrix is square:方阵
        public bool IsSquare()
        {
            return Row == Col;
        }

        //determine if the matrix is symmetric对称阵
        public bool IsSymmetric()
        {

            if (Row != Col)
                return false;

            for (int i = 0; i < Row; i++)
                for (int j = i + 1; j < Col; j++)
                    if (m_data[i, j] != m_data[j, i])
                        return false;
            return true;
        }

        //一阶矩阵->实数
        public double ToDouble()
        {
            Trace.Assert(Row == 1 && Col == 1);

            return m_data[0, 0];
        }

        //conert to string
        public override string ToString()
        {

            string s = "";
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                    s += string.Format("{0} ", m_data[i, j]);

                s += "\r\n";
            }
            return s;

        }


        //私有数据成员
        private double[,] m_data;

    }//end class Matrix
}