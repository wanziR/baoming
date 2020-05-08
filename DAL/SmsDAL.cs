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
    public class SmsDAL
    {
        #region 添加手验证码到数据库
        public int AddCoreInfo(Sms obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Sms_Code(Code, TelPhone, Sendtime, Validtime) ");
            sqlBuilder.Append("values('{0}','{1}','{2}','{3}')");
            string sql = string.Format(sqlBuilder.ToString(), obj.Code, obj.TelPhone, DateTime.Now, DateTime.Now.AddMinutes(5));
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

