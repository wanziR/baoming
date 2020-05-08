using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DAL
{
   public class QiandaoDAL
    {
        #region 通过时间获取对像 
        public Qiandao GetObjByTime()
        {
            string sql = "select * from qd_huiyi where  qdStarttime<=GETDATE() and qdEndttime >GETDATE()";
           // string sql = "select * from qd_huiyi where 1=2";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            Qiandao obj = null;
            if (dr.Read())
            {
                obj = new Qiandao
                {
                    // Id,qdName, qdStarttime, qdEndttime
                    Id = Convert.ToInt32(dr["Id"]),
                    qdName = dr["qdName"].ToString(),
                    qdStarttime = Convert.ToDateTime(dr["qdStarttime"]),
                    qdEndttime = Convert.ToDateTime(dr["qdEndttime"]),
                };
            }

            return obj;

        }
        #endregion

        #region 添加
        public int Add(QiandaoN obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into qd_qiandao(qdName,userName,userPhone,qdAddress,qdTime) ");
            sqlBuilder.Append("values('{0}','{1}','{2}','{3}','{4}')");
            string sql = string.Format(sqlBuilder.ToString(), obj.qdName, obj.userName, obj.userPhone, obj.qdAddress, DateTime.Now);
            try
            {
                return Convert.ToInt32(SQLHelper.Update(sql));
            }
            catch (SqlException ex)
            {
                throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
        }
        #endregion

        #region 获取今日签到对像 
        public Qiandao GetObjByNum()
        {
            string sql = "select * from qd_huiyi where  qdStarttime<=GETDATE() and qdEndttime >GETDATE()";
            // string sql = "select * from qd_huiyi where 1=2";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            Qiandao obj = null;
            if (dr.Read())
            {
                obj = new Qiandao
                {
                    // Id,qdName, qdStarttime, qdEndttime
                    Id = Convert.ToInt32(dr["Id"]),
                    qdName = dr["qdName"].ToString(),
                    qdStarttime = Convert.ToDateTime(dr["qdStarttime"]),
                    qdEndttime = Convert.ToDateTime(dr["qdEndttime"]),
                };
            }

            return obj;

        }
        #endregion
    }
}
