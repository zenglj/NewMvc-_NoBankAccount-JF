using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Filters
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //注销请求不被处理
                if (filterContext.ActionDescriptor.ActionName.ToString() == "LoginOut")
                {
                }
                else
                {
                    //用户信息session保存的实体类
                    string loginName = filterContext.HttpContext.Session["loginUserName"] as string;
                    Hashtable singleOnline = (Hashtable)filterContext.HttpContext.Application["Online"];
                    // 判断当前SessionID是否存在
                    if (string.IsNullOrWhiteSpace(loginName))
                    {
                    }
                    else
                    {
                        if (singleOnline != null && singleOnline.ContainsKey(loginName))
                        {
                            if (!singleOnline[loginName].Equals(filterContext.HttpContext.Session.SessionID))
                            {
                                filterContext.Result = new ContentResult() { Content = "<script>alert('您的帐号已在别处登录 ，将被迫下线（请保管好自己的账号密码）！');window.location.href='/Admin/Index'</script>" };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            base.OnActionExecuting(filterContext);
        }
    }

}