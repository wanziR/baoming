using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.UI.HtmlControls;
using System.IO;

namespace cms5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ulogin(string info)
        {
            ViewBag.info = info;
            return View();
        }

        #region 内容页面
        public ActionResult gaikuang()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(1);
            return View(obj);
        }
        public ActionResult anpai()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(2);
            return View(obj);
        }
        public ActionResult jiabin()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(3);
            return View(obj);
        }

        public ActionResult jiaotong()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(4);
            return View(obj);
        }
        public ActionResult Xuzhi()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(5);
            return View(obj);
        }
        public ActionResult tongzhi()
        {
            return View();
        }
        public ActionResult tongzhi_1()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(6);
            return View(obj);
        }

        public ActionResult tongzhi_2()
        {
            return View();
        }
        public ActionResult tongzhi_3()
        {
            return View();
        }
        public ActionResult tongzhi_4()
        {
            return View();
        }
        public ActionResult tongzhi_5()
        {
            return View();
        }
        public ActionResult tongzhi_6()
        {
            return View();
        }
        public ActionResult huichang()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(7);
            return View(obj);
        }
        public ActionResult tishi()
        {
            LanmuInfo obj = new LanmuBLL().GetLanmuObjById(8);
            return View(obj);
        }
        #endregion
        #region 个人中心
        public ActionResult center(UserInfo obju)
        {
           obju = new UserInfoBLL().GetObjByTel("13909426879");
            ViewBag.huiyi = new QiandaoBLL().GetObjByTime();
            return View(obju);
        }
        #endregion
        //public ActionResult qiandaoHiyi()
        //{
        //    Qiandao obj = new QiandaoBLL().GetObjByTime();
        //    return PartialView("_Qiandao",obj);
        //}

        #region 无刷新添加签到 
        public ActionResult AddQd(QiandaoN obj)
        {
            obj = new QiandaoN {
                //qdName,userName,userPhone,qdAddress,qdTime
                qdName = obj.qdName,
                userName = obj.userName,
                userPhone = obj.userPhone,
                qdAddress = obj.qdAddress,
                qdTime = DateTime.Now,
            };
            int result = new QiandaoBLL().Add(obj);
            if (Request.IsAjaxRequest())
            {
                return this.Content("签到成功！");
            }

            else
            {
                return this.Content("不是Ajax提交");
            }
        }
        #endregion

        public ActionResult baomingc()
        {
            return View();
        }

        #region 文件上传
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns>上传文件结果信息</returns>
        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files["myFile"];
            if (file != null)
            {
                try
                {
                    var filename = Path.Combine(Request.MapPath("~/Upload"), file.FileName);
                    file.SaveAs(filename);
                    ViewBag.msg= "上传成功";
                    return View("baomingc");
                }
                catch (Exception ex)
                {
                   // return Content(string.Format("上传文件出现异常：{0}", ex.Message));
                    ViewBag.msg = "上传错误，请重上传文件！";
                    return View("baomingc");
                }

            }
            else
            {
               // return Content("没有文件需要上传！");
                ViewBag.msg = "没有文件需要上传！";
                return View("baomingc");
            }
        }
        #endregion



    }
}