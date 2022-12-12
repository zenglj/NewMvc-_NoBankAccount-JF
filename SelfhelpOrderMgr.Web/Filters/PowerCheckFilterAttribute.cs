using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Filters
{

	public class PowerCheckFilterAttribute : FilterAttribute, IActionFilter
	{
		private JavaScriptSerializer jss = new JavaScriptSerializer();
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			string actionName = context.ActionDescriptor.ActionName;
			if (context.HttpContext.Session["loginUserCode"] != null)
			{
				HttpCookie cookie = context.HttpContext.Request.Cookies["loginUserCode"];
				if (cookie != null)
				{
					var crm = new T_CZYBLL().GetModel(cookie.Value);
					if (crm.FPRIVATE != 1)
					{
						Log4NetHelper.logger.Warn($"{controllerName}|{actionName}|操作员:{crm.FName},结果:当前用户,没有操作权限");
						context.Result = new JsonResult()
						{
							//JsonRequestBehavior = JsonRequestBehavior.AllowGet,
							//Data = new ResultInfo
							//{
							//	Flag = false,
							//	ReMsg = "当前用户,没有操作权限",
							//	DataInfo = null
							//}
							Data= "Err|当前用户,没有操作权限"
						};
						return;
					}
                    else
                    {
						return;
					}
				}
				
			}

		}

	}


}