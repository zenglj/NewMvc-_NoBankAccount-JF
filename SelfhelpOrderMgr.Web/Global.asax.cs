using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SelfhelpOrderMgr.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //路由调试设置代码
            //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }

        #region 单点登录 - 在Session过期或者退出系统时释放资源
        protected void Session_End()
        {
            try
            {
                string loginName = Session["loginUserName"] as string;
                Hashtable SingleOnline = (Hashtable)System.Web.HttpContext.Current.Application["Online"];
                if (!string.IsNullOrWhiteSpace(loginName))
                {
                    if (SingleOnline != null && SingleOnline[loginName] != null)
                    {
                        SingleOnline.Remove(Session.SessionID);
                        System.Web.HttpContext.Current.Application.Lock();
                        System.Web.HttpContext.Current.Application["Online"] = SingleOnline;
                        System.Web.HttpContext.Current.Application.UnLock();
                    }
                    Session.Abandon();
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

    }
}
