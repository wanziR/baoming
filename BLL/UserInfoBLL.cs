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
   public class UserInfoBLL
    {
        #region 添加用户
        public int Add(UserInfo obj)
        {
            return new UserInfoDAL().Add(obj);
        }
        #endregion

        #region 获取用户列表
        public List<UserInfo> GetList()
        {
            return new UserInfoDAL().GetList();
        }
        #endregion

        #region 通过id更新字段(收费状态)
        public int updateFeiIsOk(string id)
        {
            return new UserInfoDAL().updateFeiIsOk(id);
        }
        #endregion

        #region 统计报名总人数
        public string GetListNum()
        {
            return new UserInfoDAL().GetListNum();
        }
        #endregion

        #region 统计缴费总人数
        public string GetListFeiNum()
        {
            return new UserInfoDAL().GetListFeiNum();
        }
        #endregion

        #region 统计继续教育总人数
        public string GetListJx()
        {
            return new UserInfoDAL().GetListJx();
        }
        #endregion

        #region 通过ID获取对像详细
        public UserInfo GetObjById(string id)
        {
            return new UserInfoDAL().GetObjById(id);
        }
        #endregion

        #region 获取已缴费用户列表
        public List<UserInfo> GetFList()
        {
            return new UserInfoDAL().GetFList();
        }
        #endregion

        #region 获取未缴费用户列表
        public List<UserInfo> GetFNList()
        {
            return new UserInfoDAL().GetFNList();
        }
        #endregion

        #region 获取继续教育用户列表
        public List<UserInfo> GetJxList()
        {
            return new UserInfoDAL().GetJxList();
        }
        #endregion

        #region 获取公司列表
        public List<UserInfo> GetCoList()
        {
            return new UserInfoDAL().GetCoList();
        }
        #endregion

        #region 通过公司获取对像详细
        public List<UserInfo> GetCList(string Co)
        {
            return new UserInfoDAL().GetCList(Co);
        }
        #endregion

        #region 通过id删除对像
        public int Del(string id)
        {
            return new UserInfoDAL().Del(id);
        }
        #endregion

        #region @编辑对像 
        public int Edit(UserInfo obj)
        {
            return new UserInfoDAL().Edit(obj);
        }
        #endregion

        #region 验证码登录判断
        public UserInfo Code(UserInfo obj)
        {
            obj = new UserInfoDAL().Code(obj);
            if (obj != null)
            {
                HttpContext.Current.Session["Code"] = obj.Code;
            }
            return obj;
        }
        #endregion

        #region 通过ID获取对像详细
        public UserInfo GetObjByTel(string userPhone)
        {
            return new UserInfoDAL().GetObjByTel(userPhone);
        }
        #endregion

        #region 判断是否签到 
        public string GetNumUP(string uPhone)
        {
            return new UserInfoDAL().GetNumUP(uPhone);
        }
        #endregion

        #region 通过搜索关键字获取用户信息列表
        public List<UserInfo> GetUserByWord(string strWord)
        {
            return new UserInfoDAL().GetUserByWord(strWord);
        }
        #endregion

    }
}
