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
   public class UserInfoDAL
    {
        #region 添加用户
        public int Add(UserInfo obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into User_Info(userName, userCompany, userPost, userPhone, userAddress,jxJiaoyu, taxName, taxNumber, taxBank, userAddtime,taxIsPu,taxAddress,taxPhone,taxType,taxIsok,feiIsOk) ");
            sqlBuilder.Append("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')");
            string sql = string.Format(sqlBuilder.ToString(), obj.userName, obj.userCompany, obj.userPost, obj.userPhone, obj.userAddress,obj.jxJiaoyu, obj.taxName, obj.taxNumber, obj.taxBank, DateTime.Now.ToString(),obj.taxIsPu,obj.taxAddress,obj.taxPhone,0,0,0);
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

        #region 获取用户列表
        public List<UserInfo> GetList()
        {
            string sql = "select * from User_Info order by userAddtime desc";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"])

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 通过id更新字段(收费状态)
        public int updateFeiIsOk(string id)
        {
            string sql = "update User_Info set feiIsOk=1 where userId= " + id + "";
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new Exception("该号被其他实体引用，不能直接删除该学员对象！");
                else
                    throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 统计报名总人数
        public string GetListNum()
        {
            string sql = "select COUNT(*) as Num FROM User_Info";
            string str = SQLHelper.SingleResultStr(sql);
            return str;
        }
        #endregion

        #region 统计缴费总人数
        public string GetListFeiNum()
        {
            string sql = "select COUNT(*) as FeiNum FROM User_Info where feiIsok = 1";
            string str = SQLHelper.SingleResultStr(sql);

            return str;
        }
        #endregion

        #region 统计继续教育总人数
        public string GetListJx()
        {
            string sql = "select COUNT(*) as FeiNum FROM User_Info where jxJiaoyu != ''";
            string str = SQLHelper.SingleResultStr(sql);

            return str;
        }
        #endregion

        #region 通过ID获取对像详细
        public UserInfo GetObjById(string id)
        {
            string sql = "select * from User_Info where userId='{0}'";
            sql = string.Format(sql, id);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            UserInfo obj = null;
            if (dr.Read())
            {
                obj = new UserInfo
                {
                    //userName, userCompany, userPost, userPhone, userAddress, jxJiaoyu, 
                    //taxName, taxNumber, taxBank, taxType, feiIsOk, userAddtime
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    jxJiaoyu = dr["jxJiaoyu"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    feiIsOk =Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"]),
                    taxPhone = dr["taxPhone"].ToString(),
                    taxAddress = dr["taxAddress"].ToString(),
                    taxIsPu = Convert.ToInt32(dr["taxIsPu"]),
                };
            }

            return obj;
        }
        #endregion

        #region 获取已缴费用户列表
        public List<UserInfo> GetFList()
        {
            string sql = "select * from User_Info where feiIsOk like '%1%'";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"])

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 获取未缴费用户列表
        public List<UserInfo> GetFNList()
        {
            string sql = "select * from User_Info where feiIsOk like '%0%'";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"])

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 获取未缴费用户列表
        public List<UserInfo> GetJxList()
        {
            string sql = "select * from User_Info where jxJiaoyu != ''";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"])

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 获取公司列表
        public List<UserInfo> GetCoList()
        {
            string sql = "select distinct userCompany from User_Info order by userCompany desc ";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userCompany = dr["userCompany"].ToString(),
                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 通过公司获取用户列表
        public List<UserInfo> GetCList(string Co)
        {
            string sql = "select * from User_Info where userCompany like '%{0}%'";
            sql = string.Format(sql, Co);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
                list.Add(new UserInfo
                {
                    //userId, userName, userCompany, userPost, userPhone, userAddress, taxName,
                    //taxNumber, taxBank, taxType, taxIsok, userAddtime
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"]),

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        #region 通过id删除对像
        public int Del(string id)
        {
            string sql = "delete from User_Info where userId = " + id + "";
            try
            {
                return SQLHelper.Update(sql);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new Exception("该号被其他实体引用，不能直接删除该学员对象！");
                else
                    throw new Exception("数据库操作出现异常！具体信息：\r\n" + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region @编辑对像 
        public int Edit(UserInfo obj)
        {

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update User_Info set userName='{0}', userCompany='{1}', userPost='{2}', userPhone='{3}', userAddress='{4}',jxJiaoyu='{5}', taxName='{6}', taxNumber='{7}', taxBank='{8}',taxType='{9}'");
            sqlBuilder.Append(" where userId = '{10}'");
            string sql = string.Format(sqlBuilder.ToString(), obj.userName, obj.userCompany, obj.userPost, obj.userPhone, obj.userAddress, obj.jxJiaoyu, obj.taxName, obj.taxNumber, obj.taxBank, obj.taxType, obj.userId);
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

        #region 验证码登录判断
        public UserInfo Code(UserInfo obj)
        {
            string sql = "select * from Sms_Code where TelPhone= '{0}' and Code = '{1}' and Validtime >getdate() ";
            sql = string.Format(sql, obj.userPhone, obj.Code);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            try
            {
                if (dr.Read())
                {
                    obj.userPhone = dr["TelPhone"].ToString();
                    obj.Code = Convert.ToInt32(dr["Code"]);
                    dr.Close();
                }
                else
                {
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("应用程序数据库连接出现问题" + ex.Message);
            }
            return obj;


        }
        #endregion

        #region 通过手机号判断获取对像 
        public UserInfo GetObjByTel(string userPhone)
        {
            string sql = "select * from User_Info where userPhone='{0}'";
            sql = string.Format(sql, userPhone);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            UserInfo obj = null;
            if (dr.Read())
            {
                obj = new UserInfo
                {

                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"]),
                };
            }

            return obj;

        }
        #endregion

        #region 判断是否签到 
        public string GetNumUP(string uPhone)
        {
            string sql = "select COUNT(*) from qd_qiandao where userPhone='"+ uPhone+"'";
            string str = SQLHelper.SingleResultStr(sql);
            return str;
        }
        #endregion

        #region 通过搜索关键字获取用户信息列表
        public List<UserInfo> GetUserByWord(string strWord)
        {
            string sql = "select * from User_Info where  ";
            sql += "userName like '%" + strWord + "%' ";
            sql += "or userCompany like '%" + strWord + "%' ";
            sql += "or userPhone like '%" + strWord + "%' ";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<UserInfo> list = new List<UserInfo>();
            while (dr.Read())
            {
               
                list.Add(new UserInfo
                {
                    userId = Convert.ToInt32(dr["userId"]),
                    userName = dr["userName"].ToString(),
                    userCompany = dr["userCompany"].ToString(),
                    userPost = dr["userPost"].ToString(),
                    userPhone = dr["userPhone"].ToString(),
                    userAddress = dr["userAddress"].ToString(),
                    taxName = dr["taxName"].ToString(),
                    taxNumber = dr["taxNumber"].ToString(),
                    taxBank = dr["taxBank"].ToString(),
                    taxType = Convert.ToInt32(dr["taxType"]),
                    taxIsok = Convert.ToInt32(dr["taxIsok"]),
                    feiIsOk = Convert.ToInt32(dr["feiIsOk"]),
                    userAddtime = Convert.ToDateTime(dr["userAddtime"]),

                });

            }
            dr.Close();
            return list;
        }
        #endregion

        


    }
}
