using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
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
            //Session["loginUserCode"] = null;
            Session.Abandon();//退出系统，让Session失效
            return Redirect("/Admin/Index");
        }        
	}
    public class subMenu
    {
        public string text { get; set; }
        public string url { get; set; }
    }
}