using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [CustomActionFilterAttribute]
    public class MainpanelController : Controller
    {
        //
        // GET: /Mainpanel/
        public ActionResult Index()
        {
            //new T_TreeMenuBLL().GetTableByFId()
            string loginUserName = "";
            HttpCookie cookie = Request.Cookies["loginUserName"];
            if (cookie != null)
            {
                loginUserName = cookie.Value;
            }
            string usercode = Session["loginUserCode"] == null ? string.Empty : Session["loginUserCode"].ToString();
            if (usercode == "")
            {
                usercode = new T_CZYBLL().GetModelList("FName='" + loginUserName + "'")[0].FCode;
            }
            if (usercode != "")
            {
                T_CZY czy = new T_CZYBLL().GetModel(usercode);
                List<t_TreeRole_Menu> rms = (List<t_TreeRole_Menu>)new t_TreeRole_MenuBLL().GetModelList("RoleID='" + czy.FRole.ToString() + "' and Flag>0");
                Dictionary<string, object> menus = new Dictionary<string, object>();
                if (rms.Count > 0)
                {
                    int czyRoleId=Convert.ToInt16( czy.FRole);
                    foreach (t_TreeRole_Menu rm in rms.Where(p=>p.FId.Equals(0)&& p.TreeId>0).ToList())
                    {
                        if (rm.FId == 0 && rm.TreeId > 0)
                        {
                            //List<t_TreeRole_Menu> subs = (List<t_TreeRole_Menu>)new t_TreeRole_MenuBLL().GetModelList("RoleID='" + czy.FRole.ToString() + "' and Flag>0");
                            List<t_TreeRole_Menu> subs = rms.Where(p => p.FId.Equals(rm.TreeId) && p.flag > 0).ToList();
                            List<subMenu> submenus = new List<subMenu>();
                            foreach (t_TreeRole_Menu sub in subs)
                            {
                                if (sub.FId == rm.TreeId)
                                {
                                    subMenu menuop = new subMenu();
                                    menuop.text = sub.Text;
                                    menuop.url = sub.URL;
                                    submenus.Add(menuop);
                                }
                            }
                            menus.Add(rm.Text, submenus);
                        }
                    }

                }
                ViewData["menus"] = menus;
                ViewData["titleMset"] = null;

                ViewData["LoginUserName"] = loginUserName;
                ViewData["chaoshi"] = "";
                var mset = new T_SHO_ManagerSetBLL().GetModel("PwdErrLoginCount");
                if (mset!=null)
                {
                    try
                    {
                        var month = mset.MgrValue.Split((char)124)[2];
                        if (czy.PwdUpdateTime.AddDays(Convert.ToInt32(month)) < DateTime.Now)
                        {
                            ViewData["chaoshi"] = "[密码已过期，请您及时修改]";
                        }
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
                T_SHO_ManagerSet titleMset = new T_SHO_ManagerSetBLL().GetModel("back-stageMgrTitle");
                if (titleMset != null)
                {
                    ViewData["titleMset"] = titleMset.MgrValue;
                }

            }
            return View();
        }
        public ActionResult ExitSystem()
        {

            #region 单点登录
            try
            {
                Hashtable singleOnline = (Hashtable)System.Web.HttpContext.Current.Application["Online"];
                singleOnline.Remove(Session.SessionID);
                System.Web.HttpContext.Current.Application.Lock();
                System.Web.HttpContext.Current.Application["Online"] = singleOnline;
                System.Web.HttpContext.Current.Application.UnLock();
                Session.Abandon();
            }
            catch (Exception)
            {

            }
            #endregion


            FormsAuthentication.SignOut();
            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();
            return Redirect("/Admin/Index");



            //Session.Abandon();//退出系统，让Session失效
            //return Redirect("/Admin/Index");
        }        
	}
    public class subMenu
    {
        public string text { get; set; }
        public string url { get; set; }
    }
}