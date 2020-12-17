using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["loginUserCode"] == null)
            {
                HttpCookie cookie = Request.Cookies["loginUserName"];
                if (cookie == null)//如果Cookie也为空
                {
                    //filterContext.HttpContext.Response.Redirect("/Admin/Index");
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Admin", action = "Index", area = string.Empty }));

                }
                else
                {
                    Session["loginUserCode"] = Request.Cookies["loginUserCode"].Value;
                    Session["loginUserName"] = Request.Cookies["loginUserName"].Value;
                }
                //filterContext.HttpContext.Response.Redirect("/Admin/Index");
                
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }


        
	}
}