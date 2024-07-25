using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filter;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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


            DataTable paytable = new CommTableInfoBLL().GetDataTable("select isnull(count(1),0) fcount from T_Bank_PaymentRecord where TranStatus=3 and AuditDate<'" + DateTime.Now.ToString() + "'");

            if (dt == null)
            {
                ViewData["payErrCount"] = 0;
            }
            if (dt.Rows.Count > 0)
            {
                ViewData["payErrCount"] = paytable.Rows[0][0];
            }
            return View();
        }
        
        //验证系统登录

        [CheckLicenseCodeAttribute]
        [LoginPwdErrorFilterAttribute]
        public ActionResult LoginCheck()
        {
            #region 注册码验证

            #endregion

            #region ip地址验证
            string ipCheckResult=RegexHelper.RegexIpAddressCheck(IpAddressHelper.GetHostAddress());
            if (!string.IsNullOrWhiteSpace(ipCheckResult))
                return Content($"Error|{ipCheckResult}");

            //string testIp = IpAddressHelper.GetHostAddress();
            //string mac = IpAddressHelper.getHostMac(testIp);

            #endregion

            string fname = Request["FName"];
            string fpwd = Request["FPwd"];

            string decPwd = Encoding.UTF8.GetString(Convert.FromBase64String(fpwd));

            //List<T_CZY> users = new T_CZYBLL().GetModelList("FName='" + fname + "' or FCode='" + fname + "'");
            List<T_CZY> users = new BaseDapperBLL().QueryList<T_CZY>("select * from t_czy where FName=@FName  or FCode=@FCode", new { FName = fname, FCode = fname });

            string status = "Error|用户不存在或是密码不正确";
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            if (users.Count > 0)
            {
                T_CZY user = users[0];
                if (user.FFlag == 1)
                {
                    return Content("Error|该用户状态已禁用，无法登录");
                }
                if (user.FPwd == decPwd || user.FPwd== MD5ProcessHelper.GetMd5Password(decPwd))//明码和密码都支持
                {                    
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

                            HttpCookie cookiePrivate = new HttpCookie("loginUserAdmin", user.FPRIVATE.ToString());
                            cookiePrivate.Expires = DateTime.Now.AddHours(4);
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

                    //单点登录登记
                    LogInSingle(user.FName);

                    //记录日志
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


        /// <summary>
        /// 单点登录 - 登录在线记录
        /// </summary>
        /// <param name="UserName">用户名</param>
        private void LogInSingle(string UserName)
        {
            #region 单点登录 - 登录在线记录
            try
            {
                Hashtable SingleOnline = (Hashtable)System.Web.HttpContext.Current.Application["Online"];
                if (SingleOnline == null)
                    SingleOnline = new Hashtable();
                if (SingleOnline.ContainsKey(UserName))
                {
                    SingleOnline[UserName] = Session.SessionID;//记录唯一sessionid
                }
                else
                    SingleOnline.Add(UserName, Session.SessionID);

                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Online"] = SingleOnline;
                System.Web.HttpContext.Current.Application.UnLock();
            }
            catch (Exception)
            {

            }
            #endregion
        }
    }

    
}