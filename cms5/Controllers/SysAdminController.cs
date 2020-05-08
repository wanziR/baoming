using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Security;
using X.PagedList;


namespace cms5.Controllers
{
    public class SysAdminController : Controller
    {
        // GET: SysAdmin
        //[Authorize]
        //public ActionResult Index(SysAdmin objAdmin)
        //{
        //    if (this.User.Identity.IsAuthenticated)
        //    {
        //        string adminName = this.User.Identity.Name;  //获取写入cookie的adminName
        //        ViewBag.adminName = adminName;
        //        ViewBag.info = "欢迎你" + adminName;
        //        return View();
        //    }
        //    else
        //    {
        //        return View("Login");
        //    }

        //}
        [Authorize]
        public ActionResult Index(string info,int page = 1)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                string adminName = this.User.Identity.Name;  //获取写入cookie的adminName
                ViewBag.adminName = adminName;
                List<UserInfo> CList = new UserInfoBLL().GetCoList();
                ViewBag.clist = CList;
                List<UserInfo> uList = new UserInfoBLL().GetList();
                var pList = uList.ToPagedList(page,20);
                ViewBag.list = pList;
                ViewBag.info = info;
                ViewBag.num = new UserInfoBLL().GetListNum();
                ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
                ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
                ViewBag.Jx = new UserInfoBLL().GetListJx();
                return View(pList);
            }
            else
            {
                return View("Login");
            }

        }

        public ActionResult Login()
        {
            return View();
        }
       
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult Uedit(FormCollection fc)
        {
            ViewBag.content = fc["editor"];
            return View();
        }
      
        public ActionResult Jiaofei()
        {
            return View();
        }

        public ActionResult GetPwd(string info)
        {
            List<SysAdmin> List = new SysAdminBLL().GetList();
            ViewBag.info = info;
            ViewBag.list = List;
            return View("Pwd");
        }
        public ActionResult Pwd()
        {
            return View();
        }


        #region @编辑对像
        public ActionResult updatePwd(int PK_Sys_Admin,string admin_name,string admin_pwd)
        {
           SysAdmin obj = new SysAdmin()
            {
                PK_Sys_Admin = PK_Sys_Admin,
                admin_name = admin_name,
                admin_pwd =admin_pwd
           };
            int result = new SysAdminBLL().Edit(obj);
            string info = "已修改一条记录！";
            FormsAuthentication.SignOut();
            Session["CurrentAdmin"] = null;
            return View("Login", new { info });

        }
        #endregion

        #region 登录判断
        public ActionResult AdminLogin(SysAdmin obj)
        {
            obj = new SysAdmin
            {
                admin_name = obj.admin_name,
                admin_pwd = obj.admin_pwd
            };
            obj = new SysAdminBLL().Login(obj);
            if (obj != null)
            {
                string adminName = obj.admin_name;
                FormsAuthentication.SetAuthCookie(adminName, true);
                ViewBag.info = adminName;
            }

            else
            {
                ViewBag.info = "用户名或密码错误";
                return View("Login");
            }
            return RedirectToAction("Index", "SysAdmin");
        }
        #endregion

        #region 退出
        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            Session["CurrentAdmin"] = null;
            return View("Login");
        }
        #endregion

        #region 获取列表
        public ActionResult GetList(string info)
        {
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<SysAdmin> List = new SysAdminBLL().GetList();
            ViewBag.info = info;
            ViewBag.list = List;
            return View("List");
        }
        #endregion

        #region 获取已缴费用户列表
        public ActionResult FList(string info)
        {
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<UserInfo> List = new UserInfoBLL().GetFList();
            ViewBag.list = List;
            ViewBag.info = info;
            ViewBag.num = new UserInfoBLL().GetListNum();
            ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
            ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
            ViewBag.Jx = new UserInfoBLL().GetListJx();
            return View();
        }
        #endregion

        #region 获取未缴费用户列表
        public ActionResult FNList(string info)
        {
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<UserInfo> List = new UserInfoBLL().GetFNList();
            ViewBag.list = List;
            ViewBag.info = info;
            ViewBag.num = new UserInfoBLL().GetListNum();
            ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
            ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
            ViewBag.Jx = new UserInfoBLL().GetListJx();
            return View();
        }
        #endregion

        #region 获取未缴费用户列表
        public ActionResult JxList(string info)
        {
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<UserInfo> List = new UserInfoBLL().GetJxList();
            ViewBag.list = List;
            ViewBag.info = info;
            ViewBag.num = new UserInfoBLL().GetListNum();
            ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
            ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
            ViewBag.Jx = new UserInfoBLL().GetListJx();
            return View();
        }
        #endregion

        #region 通过公司获取对像详细
        public ActionResult CoList(string userCompany)
        {
            ViewBag.co = userCompany;
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<UserInfo> List = new UserInfoBLL().GetCList(userCompany);
            ViewBag.List = List;
            ViewBag.num = new UserInfoBLL().GetListNum();
            ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
            ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
            ViewBag.Jx = new UserInfoBLL().GetListJx();
            return View();
        }
        #endregion

        #region 获取详细
        public ActionResult GetDetail(string id)
        {
            SysAdmin obj = new SysAdminBLL().GetObjById(id);
            ViewBag.id = id;
            return View("Detail", obj);
        }
        #endregion


        #region 添加
        public ActionResult GetAdd(SysAdmin obj)
        {
            obj = new SysAdmin
            {
                admin_name = obj.admin_name,
                admin_pwd = obj.admin_pwd
            };
            int result = new SysAdminBLL().Add(obj);
            string info = "已添加一条记录！";
            return RedirectToAction("GetList", "SysAdmin", new { info });
            //return this.Content("<script>window.location='" + Url.Action("GetList", "SysAdmin") + "'</script>");
        }
        #endregion
        
        #region 删除对像
        public ActionResult Del(string id)
        {
            int result = new SysAdminBLL().Del(id);
            string info = "已删除一条记录！";
            return RedirectToAction("GetList", "SysAdmin", new { info });
        }
        #endregion

        #region 编辑页面
        public ActionResult Edit(string id)
        {
            SysAdmin obj = new SysAdminBLL().GetObjById(id);
            ViewBag.id = id;
            return View("Edit",obj);
        }
        #endregion

        #region @编辑对像
        public ActionResult GetEdit(SysAdmin obj)
        {
            obj = new SysAdmin()
            {
                PK_Sys_Admin = obj.PK_Sys_Admin,
                admin_name = obj.admin_name,
                admin_pwd= obj.admin_pwd
            };
            int result = new SysAdminBLL().Edit(obj);
             string info = "已修改一条记录！";
             return RedirectToAction("GetList", "SysAdmin", new { info });

        }
        #endregion

        #region 编辑框显示栏目内容
        [ValidateInput(false)]
        public ActionResult GetLanmu(FormCollection fc)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(id);
            ViewBag.neirong = obj.lanmuNeirong;
            ViewBag.content = fc["editor"];
            return View(obj);
        }
        #endregion

        #region @编辑栏目对像
        [ValidateInput(false)]
        public ActionResult UpdateLanmu(LanmuInfo obj)
        {
            obj = new LanmuInfo()
            {
                lanmuNeirong =obj.lanmuNeirong,
                lanmuId = obj.lanmuId
            };
            int result = new LanmuBLL().Edit(obj);
            string info = "修改成功 ！";
            return RedirectToAction("Index", "SysAdmin", new { info });

        }
        #endregion

        #region 搜索
        public ActionResult Search(string strWord)
        {
            //strWord = "甘肃第六建设集团股份有限公司";
            List<UserInfo> CList = new UserInfoBLL().GetCoList();
            ViewBag.clist = CList;
            List<UserInfo> List = new UserInfoBLL().GetUserByWord(strWord);
            ViewBag.list = List;
            ViewBag.num = new UserInfoBLL().GetListNum();
            ViewBag.feinum = new UserInfoBLL().GetListFeiNum();
            ViewBag.feinot = (Convert.ToInt32(ViewBag.num) - Convert.ToInt32(ViewBag.feinum));
            ViewBag.Jx = new UserInfoBLL().GetListJx();
            return View();
        }
        #endregion
    }
}