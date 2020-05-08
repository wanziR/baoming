using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace DAL
{
    public class SQLHelper
    {
        //public static readonly string connString = "server = 119.28.13.88;database=DB5amcn; uid = 5amcn; pwd = 5amcn@321";
        public static string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        #region 封装格式化SQL语句执行各方法 
        public static int Update(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            try
            {
                conn.Open();
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string errorInfo = " 调用public static int Update(string sql)方法时发生错误" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }
            finally {
                conn.Close();
            }
        }
        public static object SingleResult(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try {
                conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex) {
                ////将异常信息写入日志
                string errorinfo = "调用 public static object SingleResult(string sql )时出错" + ex.Message;
                WriteLog(errorinfo);
                throw new Exception(errorinfo);
            }
            finally {
                conn.Close();
            }
        }

        public static string SingleResultStr(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                ////将异常信息写入日志
                string errorinfo = "调用 public static object SingleResult(string sql )时出错" + ex.Message;
                WriteLog(errorinfo);
                throw new Exception(errorinfo);
            }
            finally
            {
                conn.Close();
            }
        }

        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try {
                conn.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                conn.Close();
                ////将异常信息写入日志
                string errorinfo = "调用 public static SqlDataReader GetReader(string sql)时出错" + ex.Message;
                WriteLog(errorinfo);
                throw new Exception(errorinfo);
            }


        }
        public static DataSet GetDataSet(string sql)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                //conn.Close();
                //将异常信息写入日志
                string errorInfo = "public static DataSet GetDataSet(string sql)方法时发生错：" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }
            finally
            {
                conn.Close();
            }
          
           
        }
        #endregion
        #region 封带参数的SQL语句执行的各种方法
        public static int Update(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);//封装参数
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //将异常信息写入日志
                string errorInfo = "public static int Update(string sql,SqlParameter[] param)方法时发生错：" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }
            finally
            {
                conn.Close();
            }
        }
        public static object GetSingleResult(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);//封装参数
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //将异常信息写入日志
                string errorInfo = "public static object GetSingleResult(string sql,SqlParameter[] param)方法时发生错：" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }
            finally
            {
                conn.Close();
            }
        }
        public static SqlDataReader GetReader(string sql, SqlParameter[] param)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddRange(param);//封装参数
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                conn.Close();
                //将异常信息写入日志
                string errorInfo = "public static SqlDataReader GetReader(string sql,SqlParameter[] param)方法时发生错：" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }

        }
        /// <summary>
        /// 启用事务提交多条带参数的SQL语句  "主表，明细表--多参List"
        /// </summary>
        /// <param name="mainSql">主表SQL语句</param>
        /// <param name="mainParam">主表SQL语句对应的参数</param>
        /// <param name="detailSql">明细表SQL语句</param>
        /// <param name="detailParam">明细表SQL语句对应的数组集</param>
        /// <returns></returns>
        public static bool UpdateByTran(string mainSql, SqlParameter[] mainParam,
             string detailSql, List<SqlParameter[]> detailParam)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            try
            {
                conn.Open();
                cmd.Transaction = conn.BeginTransaction();//开启事务
                if (mainSql != null && mainSql.Length != 0)
                {
                    cmd.CommandText = mainSql;
                    cmd.Parameters.AddRange(mainParam);
                    cmd.ExecuteNonQuery();
                }
                foreach (SqlParameter[] param in detailParam)
                {
                    cmd.CommandText = detailSql;
                    cmd.Parameters.Clear();//必须要清除以前的参数
                    cmd.Parameters.AddRange(param);
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();//提交事务
                return true;
            }
            catch (Exception ex)
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction.Rollback();//回滚事务
                }
                string errorInfo = "调用  public static bool UpdateByTran(string mainSql, SqlParameter[] mainParam, string detailSql, List<SqlParameter[]> detailParam)方法时发生错：" + ex.Message;
                WriteLog(errorInfo);
                throw new Exception(errorInfo);
            }
            finally
            {
                if (cmd.Transaction != null)
                {
                    cmd.Transaction = null;//清空事务
                }
                conn.Close();
            }
        }
        #endregion
        #region 封装调用存储过程执行的各种方法
        #endregion
        #region 其它方法
        private static void WriteLog(string log)
        {
            FileStream fs = new FileStream("sqlhelper.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + "   " + log);
            sw.Close();
            fs.Close();
        }
        #endregion
    }
}
