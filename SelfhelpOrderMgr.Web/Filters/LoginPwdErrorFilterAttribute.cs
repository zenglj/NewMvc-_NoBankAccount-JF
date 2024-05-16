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

	public class LoginPwdErrorFilterAttribute : FilterAttribute, IActionFilter
	{
		T_CZYBLL _bll=new T_CZYBLL();
		string ipAddress = IpAddressHelper.GetHostAddress();
		private JavaScriptSerializer jss = new JavaScriptSerializer();
		public void OnActionExecuted(ActionExecutedContext context)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			string actionName = context.ActionDescriptor.ActionName;
			if (context.HttpContext.Request["FName"] != null)
			{
				var fname = context.HttpContext.Request["FName"].ToString();

				ContentResult _cs = (ContentResult)context.Result;
				if(_cs.Content== "Error|用户不存在或是密码不正确")
                {
					SetLoginInfo(1, fname, 1, _cs.Content);
				}
                else if(_cs.Content == "OK|验证成功" )
                {
					SetLoginInfo(0, fname, 0, _cs.Content);
				}


			}
		}

		private void SetLoginInfo(int errMode,string fname,int changeCount,string statusInfo)
        {
			string strErrMode = "失败";
			//T_CZY _user = _bll.GetModelList("FName='" + fname + "' or FCode='" + fname + "'").FirstOrDefault();
			T_CZY _user = new BaseDapperBLL().QueryList<T_CZY>("select * from t_czy where FName=@FName  or FCode=@FCode", new { FName = fname, FCode = fname }).FirstOrDefault();
			if (_user != null)
			{
                if (errMode == 1)
                {
					_user.ErrCount = _user.ErrCount + 1;
                    
                }
                else
                {
					_user.ErrCount = 0;
					strErrMode = "成功";
                }
                _user.LastLoginTime = DateTime.Now;
                if (_user.PwdUpdateTime < Convert.ToDateTime("2000-01-01"))
                {
                    _user.PwdUpdateTime = Convert.ToDateTime("2000-01-01");
                }
                Log4NetHelper.logger.Warn($"用户登录|IP：{ipAddress}，操作员:{_user.FCode},结果:{statusInfo}");
                T_SysOperationLog log = new T_SysOperationLog()
                {
                    ControlName = "Home",
                    ActionName = "UserSingCheck",
                    CrtDate = DateTime.Now,
                    Remark = $"后台登录{strErrMode},IP:" + ipAddress,
                    ReqJson = _user.FName,
                    RtnJson = statusInfo,
                    UserCode = _user.FName
                };
                new BaseDapperBLL().Insert<T_SysOperationLog>(log);
                _bll.Update(_user);
			}
		}
        public void OnActionExecuting(ActionExecutingContext context)
		{
			JavaScriptSerializer jss = new JavaScriptSerializer();
			string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;
			string actionName = context.ActionDescriptor.ActionName;
			if (context.HttpContext.Request["FName"] != null)
			{
				var fname = context.HttpContext.Request["FName"].ToString();
				//T_CZY _user = _bll.GetModelList("FName='" + fname + "' or FCode='" + fname + "'").FirstOrDefault();
				T_CZY _user = new BaseDapperBLL().QueryList<T_CZY>("select * from t_czy where FName=@FName  or FCode=@FCode", new { FName = fname, FCode = fname }).FirstOrDefault();

				if (_user != null)
				{
					var mset = new T_SHO_ManagerSetBLL().GetModel("PwdErrLoginCount");
                    if (mset != null)
                    {
						string[] setvalues = mset.MgrValue.Split((char)124);
						if (_user.ErrCount >= Convert.ToInt32( setvalues[0]) 
							&& _user.LastLoginTime.AddMinutes(Convert.ToInt32(setvalues[1]))>DateTime.Now )//错误登录次数,没有达到锁定时间
						{
							Log4NetHelper.logger.Warn($"{controllerName}|{actionName}|操作员:{_user.FName},结果:错误登录次数,没有达到锁定时间");

                            T_SysOperationLog log = new T_SysOperationLog()
                            {
                                ControlName = "Home",
                                ActionName = "UserSingCheck",
                                CrtDate = DateTime.Now,
                                Remark = "后台登录失败,IP:" + ipAddress,
                                ReqJson = _user.FName,
                                RtnJson = "Err|错误登录次数,没有达到锁定时间",
                                UserCode = _user.FName
                            };
                            new BaseDapperBLL().Insert<T_SysOperationLog>(log);

                            context.Result = new ContentResult()
                            {
                                Content = $"Err|密码错误超过{setvalues[0]}次,请{setvalues[1]}分钟再试"
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

}