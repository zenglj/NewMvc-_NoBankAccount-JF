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
	public class CustomActionFilterAttribute : FilterAttribute, IActionFilter
	{
		private JavaScriptSerializer jss = new JavaScriptSerializer();
		private Dictionary<string, IDictionary<string, object>> sessionDicts = new Dictionary<string, IDictionary<string, object>>();
		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			CustomActionFilterAttribute.CheckLoginInfo(context);
			string sessionId = context.HttpContext.Session.SessionID;
			IDictionary<string, object> curActionParam = context.ActionParameters;
			if (this.sessionDicts.ContainsKey(sessionId))
			{
				this.sessionDicts[sessionId] = curActionParam;
				return;
			}
			this.sessionDicts.Add(sessionId, curActionParam);
		}
		private void WriteLogInfo(ActionExecutedContext context)
		{
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			string actionName = context.ActionDescriptor.ActionName;
			context.ActionDescriptor.GetParameters();
			NameValueCollection arg_38_0 = HttpContext.Current.Request.Params;
			string sessionId = context.HttpContext.Session.SessionID;
			IDictionary<string, object> ss = new Dictionary<string, object>();
			if (this.sessionDicts.ContainsKey(sessionId))
			{
				ss = this.sessionDicts[sessionId];
			}
			string rtnText = "";
			string reqText = string.Join(",", (
				from p in ss
				select this.jss.Serialize(p)).ToArray<string>());
			JsonResult rtnJson = context.Result as JsonResult;
			if (rtnJson != null)
			{
				rtnText = this.jss.Serialize(rtnJson.Data);
			}
			string userName = context.HttpContext.Session["loginUserName"].ToString();
			T_SysOperationLog i = new T_SysOperationLog
			{
				Id = 0,
				ControlName = controllerName,
				ActionName = actionName,
				ReqJson = reqText,
				RtnJson = rtnText,
				UserCode = (userName == null) ? "" : userName,
				CrtDate = DateTime.Now,
				Remark = "测试01"
			};
			new BaseDapperBLL().Insert<T_SysOperationLog>(i);
		}
		private static void CheckLoginInfo(ActionExecutingContext context)
		{
			Console.WriteLine("ActionFilter Executing!");
			if (context.HttpContext.Session["loginUserCode"] == null)
			{
				HttpCookie cookie = context.HttpContext.Request.Cookies["loginUserCode"];
				if (cookie == null)
				{
					context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
					{
						controller = "Admin",
						action = "Index",
						area = string.Empty
					}));
					return;
				}
				context.HttpContext.Session["loginUserCode"] = cookie.Value;
				context.HttpContext.Session["loginUserName"] = context.HttpContext.Request.Cookies["loginUserName"].Value;
			}
		}
	}

	public class MyLogActionFilterAttribute : FilterAttribute, IActionFilter
	{
		private JavaScriptSerializer jss = new JavaScriptSerializer();
		private Dictionary<string, IDictionary<string, object>> sessionDicts = new Dictionary<string, IDictionary<string, object>>();
		public void OnActionExecuted(ActionExecutedContext context)
		{
			this.WriteLogInfo(context);
		}
		private void WriteLogInfo(ActionExecutedContext context)
		{
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			string actionName = context.ActionDescriptor.ActionName;
			string actionRemarName = "";
			actionRemarName = "登录IP:"+ IpAddressHelper.GetHostAddress(); ;
			MethodInfo[] methods = context.Controller.GetType().GetMethods();
			for (int l = 0; l < methods.Length; l++)
			{
				MethodInfo item = methods[l];
				if (item.Name == actionName)
				{
					foreach (CustomAttributeData atr in item.CustomAttributes)
					{
						if (atr.ToString().Contains("RemarkAttribute"))
						{
							string expr_86 = atr.ToString();
							int i = expr_86.IndexOf("(\"") + 2;
							int j = expr_86.IndexOf("\")");
							actionRemarName = expr_86.Substring(i, j - i);
						}
					}
				}
			}
			context.ActionDescriptor.GetParameters();
			string[] arg_F7_0 = HttpContext.Current.Request.QueryString.AllKeys;
			string sessionId = context.HttpContext.Session.SessionID;
			IDictionary<string, object> ss = new Dictionary<string, object>();
			if (this.sessionDicts.ContainsKey(sessionId))
			{
				ss = this.sessionDicts[sessionId];
			}
			string rtnText = "";
			string reqText = string.Join(",", (
				from p in ss
				select this.jss.Serialize(p)).ToArray<string>());
			JsonResult rtnJson = context.Result as JsonResult;
			if (rtnJson != null)
			{
				rtnText = this.jss.Serialize(rtnJson.Data);
			}
			else
			{
				ContentResult cntRs = context.Result as ContentResult;
				rtnText = ((cntRs == null) ? "" : cntRs.Content);
			}
			string userName = "";
			if (context.HttpContext.Session["loginUserName"] != null)
			{
				userName = context.HttpContext.Session["loginUserName"].ToString();
			}
			if ("BankATM" == controllerName)
			{
				userName = "ATM";
			}
			T_SysOperationLog k = new T_SysOperationLog
			{
				Id = 0,
				ControlName = controllerName,
				ActionName = actionName,
				ReqJson = reqText,
				RtnJson = rtnText,
				UserCode = (userName == null) ? "" : userName,
				CrtDate = DateTime.Now,
				Remark = actionRemarName
			};
			new BaseDapperBLL().Insert<T_SysOperationLog>(k);
		}
		public void OnActionExecuting(ActionExecutingContext context)
		{
			MyLogActionFilterAttribute.CheckLoginInfo(context);
            string sessionId = context.HttpContext.Session.SessionID;
            IDictionary<string, object> curActionParam = context.ActionParameters;
            if (this.sessionDicts.ContainsKey(sessionId))
            {
                this.sessionDicts[sessionId] = curActionParam;
                return;
            }
            this.sessionDicts.Add(sessionId, curActionParam);

		}
		private static void CheckLoginInfo(ActionExecutingContext context)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			if ("BankATM" != controllerName)
			{
				Console.WriteLine("ActionFilter Executing!");
				if (context.HttpContext.Session["loginUserCode"] == null)
				{
					HttpCookie cookie = context.HttpContext.Request.Cookies["loginUserCode"];
					if (cookie == null)
					{
						context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
						{
							controller = "Admin",
							action = "Index",
							area = string.Empty
						}));
						return;
					}
					context.HttpContext.Session["loginUserCode"] = cookie.Value;
					context.HttpContext.Session["loginUserName"] = context.HttpContext.Request.Cookies["loginUserName"].Value;
					return;
				}
			}
			else
			{
				ResultInfo rs = new ResultInfo
				{
					Flag = false,
					ReMsg = "未授权的访问IP地址",
					DataInfo = null
				};
				string strIp = IpAddressHelper.GetHostAddress();
				string strJsonWhere = jss.Serialize(new
				{
					IpAddr = strIp
				});
				T_Bank_AtmMachine i = new BankAtmService().GetModelFirst<T_Bank_AtmMachine, T_Bank_AtmMachine>(strJsonWhere);
				if (i == null)
				{
					JsonResult jr = new JsonResult
					{
						Data = rs
					};
					context.Result = jr;
				}
				ReqDataAtm req = (ReqDataAtm)context.ActionParameters["reqJson"];
				string ipAddr = IpAddressHelper.GetHostAddress();
				if (!MD5ProcessHelper.CheckMd5Token(req.token, i.Pwd, ipAddr))
				{
					rs.ReMsg = "无效的授权Key";
					JsonResult jr2 = new JsonResult
					{
						Data = rs
					};
					T_SysOperationLog _model = new T_SysOperationLog
					{
						Id = 0,
						ControlName = controllerName,
						ActionName = "AtmCall",
						ReqJson = jss.Serialize(req),
						RtnJson = jss.Serialize(jr2),
						UserCode = "",
						CrtDate = DateTime.Now,
						Remark = "BankATM"
					};
					new BaseDapperBLL().Insert<T_SysOperationLog>(_model);
					context.Result = jr2;
				}
			}
		}
	}


}