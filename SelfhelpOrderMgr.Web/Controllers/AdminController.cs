using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filter;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            //List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("Flag=0 and Bankflag=1 and CAmount>0 and SendDate<'" + DateTime.Today.ToString() + "'");

            //ViewData["vcrdCount"] = vcrds.Count;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select isnull(count(1),0) fcount from t_Vcrd where Flag=0 and Bankflag=1 and CAmount>0 and SendDate<'" + DateTime.Today.ToString() + "'");
            if (dt == null)
            {
                ViewData["vcrdCount"] = 0;
            }
            if (dt.Rows.Count > 0)
            {
                ViewData["vcrdCount"] = dt.Rows[0][0];
            }
            return View();
        }
        
        //验证系统登录

        [CheckLicenseCodeAttribute]
        public ActionResult LoginCheck()
        {
            #region 注册码验证
            
            #endregion

            string fname = Request["FName"];
            string fpwd = Request["FPwd"];
            
            List<T_CZY> users = new T_CZYBLL().GetModelList("FName='" + fname + "' or FCode='" + fname + "'");
            string status = "Error|用户不存在或是姓名不正确";
            
            if (users.Count > 0)
            {
                T_CZY user = users[0];
                if (user.FPwd == fpwd)
                {                    
                    string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                    Log4NetHelper.logger.Info("后台系统登录,操作员：" + user.FName + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");
                    string strCookieLogin = "";
                    T_SHO_ManagerSet checkUserLoginModeMgr = new T_SHO_ManagerSetBLL().GetModel("CheckLoginSeccionOrCookie");

                    if (checkUserLoginModeMgr != null)
                    {
                        if (checkUserLoginModeMgr.MgrValue == "1")
                        {
                            //创建Cookie
                            HttpCookie cookie = new HttpCookie("loginUserName", user.FName);
                            cookie.Expires = DateTime.Now.AddHours(4);
                            //写入Cookie
                            Response.Cookies.Set(cookie);

                            HttpCookie cookieCode = new HttpCookie("loginUserCode", user.FCode);
                            cookieCode.Expires = DateTime.Now.AddHours(4);
                            //写入Cookie
                            Response.Cookies.Set(cookieCode);

                            strCookieLogin = "COOKIE";
                            Session["loginUserAdmin"] = user.FPRIVATE;
                            Session["loginUserCode"] = user.FCode;
                            Session["loginUserName"] = user.FName;
                        }                        
                    }
                    //如果不是Cookie就用Seccion
                    if (string.IsNullOrEmpty(strCookieLogin))
                    {
                        //Session["loginUserLevelId"] = user.FRole.LevelId;
                        Session["loginUserAdmin"] = user.FPRIVATE;
                        Session["loginUserCode"] = user.FCode;
                        Session["loginUserName"] = user.FName;
                    }
                    T_SysOperationLog log = new T_SysOperationLog()
                    {
                        ControlName = "Home",
                        ActionName = "UserSingCheck",
                        CrtDate = DateTime.Now,
                        Remark = "后台登录,IP:" + ip,
                        ReqJson = user.FName,
                        RtnJson = "OK|验证成功",
                        UserCode = user.FName
                    };
                    new BaseDapperBLL().Insert<T_SysOperationLog>(log);
                    status ="OK|验证成功";
                }
            }
            
            return Content(status);
        }
	}

    
}