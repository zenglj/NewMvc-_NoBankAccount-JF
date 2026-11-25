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

            //新方式：前端发送两边MD5(MD5(MD5(明文)))，与数据库FPwd存储格式一致，直接比较
            ////兼容旧方式：前端发送base64(明文)，解码后按明文/双重MD5比较
            //string decPwd = null;
            //try
            //{
            //    decPwd = Encoding.UTF8.GetString(Convert.FromBase64String(fpwd));
            //}
            //catch
            //{
            //    //非Base64格式，为新方式双重MD5哈希
            //    decPwd = null;
            //}

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
                //新方式优先：直接比较两边MD5；旧方式兜底：base64解码后比较明文或双重MD5
                bool pwdMatch = (user.FPwd == fpwd && user.FPwd.Length>=25)
                    //明码部分的验证方式
                    || (user.FPwd.Length< 25 && fpwd == MD5ProcessHelper.GetMd5Password(user.FPwd));
                if (pwdMatch)//明码和密码都支持
                {                    
                    Log4NetHelper.logger.Info("后台系统登录,操作员：" + user.FName + ",登录时间=" + DateTime.Now.ToString() + ",登录IP为：" + ip + "");
                    string strCookieLogin = "";
                    T_SHO_ManagerSet checkUserLoginModeMgr = new T_SHO_ManagerSetBLL().GetModel("CheckLoginSeccionOrCookie");

                    if (user.FPwd.Length < 8)
                    {
                        //弱密码强制修改：先设置Session以便访问/Admin/ChangePassword页面
                        Session["loginUserAdmin"] = user.FPRIVATE;
                        Session["loginUserCode"] = user.FCode;
                        Session["loginUserName"] = user.FName;
                        return Content("ChangePwd|当前密码强度不足（少于8位），请修改密码后再使用系统");
                    }

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

        // GET: /Admin/ChangePassword - 弱密码强制修改密码页面
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Admin/SavePassword - 保存修改的密码
        [MyLogActionFilterAttribute]
        public ActionResult SavePassword()
        {
            if (Session["loginUserCode"] == null)
            {
                return Content("登录已过期，请重新登录");
            }

            string userCode = Session["loginUserCode"].ToString();
            //前端发送双重MD5，与数据库FPwd存储格式一致
            string oldPwd = Request["oldPassword"];
            string newPwd = Request["newPassword"];
            string chkPwd = Request["checkPassword"];

            if (string.IsNullOrEmpty(oldPwd))
            {
                return Content("原密码不能为空");
            }
            if (string.IsNullOrEmpty(newPwd))
            {
                return Content("新密码不能为空");
            }
            if (newPwd != chkPwd)
            {
                return Content("两次新密码不一致");
            }

            //参数化查询，防止SQL注入
            List<T_CZY> czys = new BaseDapperBLL().QueryList<T_CZY>(
                "select * from t_czy where FCode=@FCode",
                new { FCode = userCode });

            if (czys.Count == 0)
            {
                return Content("用户信息不存在");
            }

            T_CZY czy = czys[0];
            //与登录逻辑一致：兼容双重MD5密码(长度32)和明文密码(长度<32)
            bool pwdMatch = (czy.FPwd == oldPwd && czy.FPwd.Length == 32)
                || (czy.FPwd.Length < 32 && oldPwd == MD5ProcessHelper.GetMd5Password(czy.FPwd));

            if (!pwdMatch)
            {
                return Content("原密码不正确");
            }

            //前端已双重MD5，直接保存
            czy.FPwd = newPwd;
            czy.PwdUpdateTime = DateTime.Now;
            new T_CZYBLL().Update(czy);

            Log4NetHelper.logger.Info("用户修改密码，操作员：" + czy.FName + ",修改时间=" + DateTime.Now.ToString() + ",登录IP为：" + System.Web.HttpContext.Current.Request.UserHostAddress + "");

            //清除Session，强制用户使用新密码重新登录
            Session["loginUserAdmin"] = null;
            Session["loginUserCode"] = null;
            Session["loginUserName"] = null;

            return Content("密码修改成功");
        }
    }


}