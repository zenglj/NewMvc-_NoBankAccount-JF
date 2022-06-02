using SelfhelpOrderMgr.Web.CommonHeler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Filter
{
    public class CheckLicenseCodeAttribute : ActionFilterAttribute
    {
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        private Dictionary<string, IDictionary<string, object>> sessionDicts = new Dictionary<string, IDictionary<string, object>>();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //string sessionId = context.HttpContext.Session.SessionID;
            //IDictionary<string, object> curActionParam = context.ActionParameters;
            //if (this.sessionDicts.ContainsKey(sessionId))
            //{
            //    this.sessionDicts[sessionId] = curActionParam;
            //    return;
            //}
            //this.sessionDicts.Add(sessionId, curActionParam);

            var _r = LicenseHelper.CheckLicenseCode();
            if (_r.StartsWith("OK|") != true)
            {
                var cs = new ContentResult();
                cs.Content = _r;
                context.Result = cs;
                base.OnActionExecuting(context);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //业务逻辑


        }

    }
}