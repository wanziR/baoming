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
   public class SysAdminDAL
    {
        #region 用户登录判断
        public SysAdmin Login(SysAdmin obj)
        {
            string sql = "select admin_name from Sys_Admin where admin_name= '{0}' and admin_pwd = '{1}'";
            sql = string.Format(sql,obj.admin_name,obj.admin_pwd);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            try
            {
                if (dr.Read())
                {
                    obj.admin_name = dr["admin_name"].ToString();
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

        #region 获取用户列表
        public List<SysAdmin> GetList()
        {
            string sql = "select * from Sys_Admin";
            SqlDataReader dr = SQLHelper.GetReader(sql);
            List<SysAdmin> list = new List<SysAdmin>();
                while (dr.Read())
                {
                    list.Add(new SysAdmin {
                        PK_Sys_Admin = Convert.ToInt32(dr["PK_Sys_Admin"]),
                        admin_name = dr["admin_name"].ToString(),
                        admin_addtime = Convert.ToDateTime(dr["admin_addtime"])

                    });
                    
                }
                dr.Close();
                return list;
        }
        #endregion

        #region 通过ID获取对像详细
        public SysAdmin GetObjById(string id)
        {
            string sql = "select * from Sys_Admin where PK_Sys_Admin='{0}'";
            sql = string.Format(sql,id);
            SqlDataReader dr = SQLHelper.GetReader(sql);
            SysAdmin obj = null;
            if (dr.Read())
            {
                obj = new SysAdmin {
                    admin_name=dr["admin_name"].ToString(),
                    admin_pwd = dr["admin_pwd"].ToString(),
                    admin_addtime = Convert.ToDateTime(dr["admin_addtime"])
                };
            }

            return obj;
        }
        #endregion

        #region 添加用户
        public int Add(SysAdmin obj)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("insert into Sys_Admin(admin_name,admin_pwd,admin_addtime) ");
            sqlBuilder.Append("values('{0}','{1}','{2}')");
            string sql = string.Format(sqlBuilder.ToString(),obj.admin_name,obj.admin_pwd,DateTime.Now);
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

        #region 通过id删除对像
        public int Del(string id)
        {
            string sql = "delete from Sys_Admin where PK_Sys_Admin = "+id+"";
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
        public int Edit(SysAdmin obj)
        {

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update Sys_Admin set admin_name='{0}',admin_pwd='{1}' ");
            sqlBuilder.Append(" where PK_Sys_Admin = '{2}'");
            string sql = string.Format(sqlBuilder.ToString(),obj.admin_name,obj.admin_pwd,obj.PK_Sys_Admin);
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
