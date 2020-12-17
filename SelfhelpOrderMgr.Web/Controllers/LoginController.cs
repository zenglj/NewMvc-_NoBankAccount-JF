﻿using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class LoginController : BaseMenuController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string loginCard = "";
            try
            {
                //先验证Cookie
                HttpCookie cookie = Request.Cookies["loginUserName"];
                string loginUserName = "";
                if (cookie != null)
                {
                    loginUserName = cookie.Value;
                }
                if (!string.IsNullOrEmpty(loginUserName))
                {
                    loginCard = loginUserName;
                }
                else
                {
                    //后验证Session
                    T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
                    if (mgr.KeyMode == 1)
                    {
                        //原用cookie模式
                        //loginCard = filterContext.HttpContext.Request.Cookies["person_Users"]["userLoginCard"];

                        //现改为后能验证模式,验证该IP是否有刷卡过
                        string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                        T_SHO_ManagerSet oldmgr = new T_SHO_ManagerSetBLL().GetModel(ip);
                        if (oldmgr != null)
                        {
                            if (oldmgr.StartTime.AddHours(Convert.ToDouble(mgr.MgrValue)) > DateTime.Now)
                            {
                                loginCard = oldmgr.MgrName;
                            }
                        }
                    }
                    else if (mgr.KeyMode == 2)
                    {
                        if (mgr.StartTime.AddHours(Convert.ToDouble(mgr.MgrValue)) > DateTime.Now)
                        {
                            loginCard = "NowOK";
                        }
                        else
                        {
                            loginCard = "";
                        }
                    }
                }
            }
            catch
            {
                loginCard = "";
            }
            if (string.IsNullOrEmpty(loginCard) == true)
            {
                filterContext.HttpContext.Response.Redirect("/Home/Login/");
                
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }

        public ActionResult LoadPartialPage()
        {
            string pageMdu = Request["pageMdu"];
            return PartialView(pageMdu);
        }
	}
}