using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string loginUserCode;
        protected string loginUserName ;
        protected string loginUserPrivate;
        
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        protected BaseDapperBLL _baseDapperBll = new BaseDapperBLL();
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
                    Session["loginUserAdmin"] = Request.Cookies["loginUserAdmin"].Value;
                    loginUserName = Session["loginUserName"].ToString();
                    loginUserCode = Session["loginUserCode"].ToString();
                    var user=_baseDapperBll.QueryModel<T_CZY>("FCode", Session["loginUserCode"].ToString());
                    
                    loginUserPrivate= user.FPRIVATE.ToString();
                }
                //filterContext.HttpContext.Response.Redirect("/Admin/Index");
                
            }
            else
            {
                loginUserName = Session["loginUserName"].ToString();
                loginUserCode = Session["loginUserCode"].ToString();
                //loginUserPrivate = Session["loginUserAdmin"].ToString();
                var user = _baseDapperBll.QueryModel<T_CZY>("FCode", Session["loginUserCode"].ToString());
                loginUserPrivate = user.FPRIVATE.ToString();
                base.OnActionExecuting(filterContext);
            }
        }


        //保存上传的Excel文件
        protected string SavePostExcelFile(HttpPostedFileBase f)
        {
            string fname = f.FileName;
            /* startIndex */
            int index = fname.LastIndexOf("\\") + 1;
            /* length */
            int len = fname.Length - index;
            fname = fname.Substring(index, len);
            /* save to server */
            string savePath = Server.MapPath("~/Upload/" + fname);

            f.SaveAs(savePath);
            return savePath;
        }
    }
}