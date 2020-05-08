using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Security;

namespace cms5.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo

        #region 返回页面

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult baoming( string info)
        {
            ViewBag.meg = info;
            return View();
        }
        public ActionResult ok(string info)
        {
            ViewBag.info = info;
            return View();
        }

        #endregion


        #region 添加
        public ActionResult GetAdd(UserInfo obj)
        {
            if (ModelState.IsValid)
            {
                obj = new UserInfo
                {
                    userName = obj.userName,
                    userCompany = obj.userCompany,
                    userPost = obj.userPost,
                    userPhone = obj.userPhone,
                    userAddress = obj.userAddress,
                    jxJiaoyu = obj.jxJiaoyu,
                    taxName = obj.taxName,
                    taxNumber = obj.taxNumber,
                    taxBank = obj.taxBank,
                    taxPhone = obj.taxPhone,
                    taxAddress = obj.taxAddress,
                    taxIsPu = obj.taxIsPu,
                    //taxType = obj.taxType,
                    //taxIsok= obj.taxIsok,
                    //feiIsOk= obj.feiIsOk


                };
                int result = new UserInfoBLL().Add(obj);
                string info = "您的报名信息已经提交！";
                return RedirectToAction("ok", "UserInfo", new { info });
            }
            else return View("baoming");
            //return this.Content("<script>window.location='" + Url.Action("GetList", "SysAdmin") + "'</script>");
        }
        #endregion

        #region 获取列表
        public ActionResult GetList(string info)
        {
            List<UserInfo> List = new UserInfoBLL().GetList();
            ViewBag.info = info;
            ViewBag.list = List;
            return View("Main");
        }
        #endregion

        #region 通过id更新字段(收费状态)
        public ActionResult updateFeiIsOk(string uid)
        {
            int result = new UserInfoBLL().updateFeiIsOk(uid);
            string info = "已更改一条缴费状态！";
            return RedirectToAction("Index", "SysAdmin", new { info });
        }
        #endregion

        #region 获取用户详细
        public ActionResult GetUDetail(string id)
        {
            UserInfo obj = new UserInfoBLL().GetObjById(id);
            ViewBag.id = id;
            return View("Udetail", obj);
        }
        #endregion

        #region 删除对像
        public ActionResult Del(string id)
        {
            int result = new UserInfoBLL().Del(id);
            if (result == 1)
            {
                return this.Content("删除成功！");
            }
            else
            {
                return this.Content("删除失败！");
            }
            //string info = "已删除一条记录！";
            //return RedirectToAction("Index", "SysAdmin", new { info });
        }
        #endregion

        #region 编辑页面

        public ActionResult UEdit(string id)
        {
            UserInfo obj = new UserInfoBLL().GetObjById(id);
            ViewBag.id = id;
            return View("UEdit", obj);
        }
        #endregion

        #region @编辑对像
        public ActionResult UUpdate(UserInfo obj)
        {
            obj = new UserInfo()
            {
                //userName='', userCompany='', userPost='', userPhone='', 
                //userAddress ='',jxJiaoyu='', taxName='', taxNumber='', taxBank='',taxType='1'
                userId = obj.userId,
                userName = obj.userName,
                userCompany = obj.userCompany,
                userPost = obj.userPost,
                userPhone = obj.userPhone,
                userAddress = obj.userAddress,
                jxJiaoyu = obj.jxJiaoyu,
                taxName = obj.taxName,
                taxNumber = obj.taxNumber,
                taxBank = obj.taxBank,
                taxType = obj.taxType,
            };
            int result = new UserInfoBLL().Edit(obj);
            string info = "已修改一条记录！";
            return RedirectToAction("Index", "SysAdmin", new { info });

        }
        #endregion

        #region 验证码登录
        public ActionResult GetLogin(UserInfo obj)
        {
            obj = new UserInfo
            {
                userPhone = obj.userPhone,
                Code = obj.Code
            };
            obj = new UserInfoBLL().Code(obj);
            if (obj != null)
            {
                UserInfo obju = new UserInfoBLL().GetObjByTel(obj.userPhone);
                if (obju != null)
                {
                    //ViewBag.tel = "*该手机号已被注册";
                    string userName = obju.userName;
                    FormsAuthentication.SetAuthCookie(userName, true);
                    Session["CurrentUser"] = obju;
                    return RedirectToAction("center", "Home", obju);

                }
                else
                {
                   
                    string info = "你还没有报名，请先报名！";

                    return RedirectToAction("baoming", "UserInfo", new { info });


                }


            }
            else
            {
                string info = "*验证码错误！";
                //return this.Content("<script>alert('输入值不正确');</script>");
                return RedirectToAction("ulogin", "Home", new { info });
            }


        }
        #endregion

    }


}