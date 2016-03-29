using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Collections;
//using System.Web.UI;
using System.Web;
namespace SSmodel
{
    class SQLConnect
    {
        /// <summary>
        /// DataAccess类的摘要说明。
        /// </summary>
        SqlConnection conn;
        static string Str = @"Data Source=";
        
        //(local)\SQLEXPRESS;Initial Catalog=控制器数据库;Persist Security Info=True;User ID=sa;Password=123456
        public SQLConnect(string s)
        {
           // Str = Str + s + ";MAX Pool Size=512;Min Pool Size=1;Connection Lifetime=30";
            Str = s;
            conn = new SqlConnection(Str);
            this.conn.Open();
            Str = @"Data Source=";
        }

        //static string Str = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=控制器数据库;Persist Security Info=True;User ID=sa;Password=123456;MAX Pool Size=512;Min Pool Size=1;Connection Lifetime=30";

        ////static string Str = @"Data Source=LHP\SQLEXPRESS;Initial Catalog=C:\DOCUMENTS AND SETTINGS\ADMINISTRATOR\桌面\OPCCONFIGURE改\OPCCONFIGURE\DATABASE.MDF;Persist Security Info=True;User ID=sa;Password=LIU151;";

        //public SQLConnect()
        //{
        //    conn = new SqlConnection(Str);
        //    this.conn.Open();
        //}
        public bool CheckExistsTable(string tablename, string s)
        {
            //string Strr = Str + s + ";MAX Pool Size=512;Min Pool Size=1;Connection Lifetime=30";
            string Strr = s;
            String tableNameStr = "select count(1) from sysobjects where name = '" + tablename + "'";
            using (SqlConnection con = new SqlConnection(Strr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(tableNameStr, con);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                if (result == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public int ExeSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, this.conn);
            if (this.conn.State == ConnectionState.Closed)
            {
                this.conn.Open();
            }
            try
            {
                cmd.ExecuteNonQuery();
                return 0;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
               // ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), "success", "<script>alert('" + ex.Message.ToString() + "');</script>", false);

                //  Page.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", "<script>alert('操作成功！')</script>") 
                MessageBox.Show(ex.Message.ToString());
                return -1;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public int SQLread(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, this.conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                dr.Read();
                return 0;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return -1;
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
            }
        }
        public bool IsRead(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, this.conn);
            SqlDataReader dr = cmd.ExecuteReader();
            try
            {
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            finally
            {
                dr.Close();
                cmd.Dispose();
            }
        }
        public void ExeSQLs(string[] sql)
        {
            SqlCommand cmd = new SqlCommand();
            int j = sql.Length;
            SqlTransaction transaction = this.conn.BeginTransaction();
            try
            {
                cmd.Connection = this.conn;
                cmd.Transaction = transaction;
                foreach (string str in sql)
                {
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                transaction.Rollback();
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public ArrayList ExQSqlValGroup(string mysql, string exceptionSource)
        {
            SqlCommand ocomm = new SqlCommand();
            ArrayList val = new ArrayList();
            try
            {
                ocomm.Connection = conn;
                ocomm.CommandText = mysql;
                SqlDataReader odr = ocomm.ExecuteReader();
                while (odr.Read())
                {
                    int count = odr.FieldCount;
                    int columnindex = 1;
                    while (columnindex < count)
                    {
                        val.Add(odr.GetSqlString(columnindex).ToString());
                        columnindex++;
                    }
                }
                odr.Close();
                return val;
            }
            catch (Exception ee)
            {
                string ExceptionName = DateTime.Now.ToShortDateString();
                StreamWriter sw = File.AppendText(ExceptionName + ".txt");
                sw.WriteLine(DateTime.Now.ToString() + ":" + "OracleDataAccess.cs_ExQSqlValGroup(string mysql)_Source_" + exceptionSource + "错误信息:" + ee.Message);
                sw.Close();
                return val;
            }
            finally
            {
                ocomm.Dispose();
            }
        }
        public void CreateTable(string tablename, int AllOPCNum, string[] tagnums)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            string sql;
            sql = "CREATE   TABLE   " + tablename + "";
            sql += "(";
            sql += "DateTime" + " datetime,"; ;
            int i = 0;
            for (; i < AllOPCNum - 1; i++)
            {
                sql += "\"" + tagnums[i] + "\" nchar(10),";
            }
            sql += "\"" + tagnums[i] + "\" nchar(10)";
            sql += ")";
            comm.CommandText = sql;
            SqlParameter parm1 = new SqlParameter("@" + tablename, SqlDbType.NChar, 20);
            parm1.Value = (tablename);
            comm.Parameters.Add(parm1);
            try
            {
                comm.ExecuteNonQuery();
            }
            finally
            {
                comm.Dispose();
            }
        }
        public string ExQSqlVal(string mysql, string exceptionSource)
        {

            SqlCommand ocomm = new SqlCommand();
            string val = "";
            try
            {
                ocomm.Connection = conn;
                ocomm.CommandText = mysql;

                SqlDataReader odr = ocomm.ExecuteReader();
                while (odr.Read())
                {
                    try
                    {
                        val = odr.GetSqlString(0).ToString();
                    }
                    catch
                    {
                        val = odr.GetSqlString(0).ToString();
                    }
                }
                odr.Close();
                return val;
            }
            catch (Exception ee)
            {
                string ExceptionName = DateTime.Now.ToShortDateString();
                StreamWriter sw = File.AppendText(ExceptionName + ".txt");
                sw.WriteLine(DateTime.Now.ToString() + ":" + "OracleDataAccess.cs_ExQSqlVal(string mysql)_Source_" + exceptionSource + "错误信息:" + ee.Message);
                sw.Close();
                return val;
            }
            finally
            {
                ocomm.Dispose();
            }
        }
        public DataTable ExeSQLdt(string sql)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
            finally
            {
                this.conn.Close();
            }
        }
        public void CloseCon()
        {
            this.conn.Close();
        }
    }
}