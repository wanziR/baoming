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
   public class LanmuDAL
    {
        #region 通过ID获取栏目对像详细
        public LanmuInfo GetObjById(int id)
        {
            string sql = "select * from Lanmu_Info where lanmuId={0}";
            sql = string.Format(sql, id);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            LanmuInfo obj = null;
            if (dr.Read())
            {
                obj = new LanmuInfo
                {
                    lanmuId = Convert.ToInt32(dr["lanmuId"]),
                    lanmuName = dr["lanmuName"].ToString(),
                    lanmuNeirong = dr["lanmuNeirong"].ToString()
                };
            }

            return obj;
        }
        #endregion

        #region @编辑栏目对像 
        public int Edit(LanmuInfo obj)
        {

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update Lanmu_Info set lanmuNeirong='{0}' ");
            sqlBuilder.Append(" where lanmuId = {1}");
            string sql = string.Format(sqlBuilder.ToString(), obj.lanmuNeirong, obj.lanmuId);
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
    }
}
