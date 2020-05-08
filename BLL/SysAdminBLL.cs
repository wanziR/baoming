using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
using System.Web;

namespace BLL
{
   public  class SysAdminBLL
    {
        #region 登录判断
        public SysAdmin Login(SysAdmin obj)
        {
            obj = new SysAdminDAL().Login(obj);
            if (obj !=null)
            {
                HttpContext.Current.Session["CurrentAdmin"] = obj;
            }
            return obj;
        }
        #endregion

        #region 获取用户列表
        public List<SysAdmin> GetList()
        {
            return new SysAdminDAL().GetList();
        }
        #endregion

        #region 通过ID获取对像详细
        public SysAdmin GetObjById(string id)
        {
            return new SysAdminDAL().GetObjById(id);
        }
        #endregion

        #region 添加用户
        public int Add(SysAdmin obj)
        {
            return new SysAdminDAL().Add(obj);
        }
        #endregion

        #region 通过id删除对像
        public int Del(string id)
        {
            return new SysAdminDAL().Del(id);
        }
        #endregion

        #region 编辑对像 
        public int Edit(SysAdmin obj)
        {
            return new SysAdminDAL().Edit(obj);

        }
        #endregion

      
    }
}
