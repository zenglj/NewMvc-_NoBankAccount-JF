using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class SuperMarketController : Controller
    {
        //
        // GET: /SuperMarket/
        public ActionResult Index()
        {
            //if (Session["loginUserCode"] == null)
            //{
            //    return View("/Login/Index");

            //}

            HttpCookie cookie = new HttpCookie("loginUserName", "Admin");
            cookie.Expires = DateTime.Now.AddHours(4);
            //写入Cookie
            Response.Cookies.Set(cookie);
            //strCookieLogin = "COOKIE";
            Session["loginUserAdmin"] = "1";
            Session["loginUserCode"] = "102";
            Session["loginUserName"] = "Admin";

            Session["loginUserCode"] = "102";
            ViewData["CtrName"] = "SuperMarket";

            T_CZY czy = new T_CZYBLL().GetModelList("FCode='" + Session["loginUserCode"].ToString() + "'")[0];
            string LoginUserName = czy.FName;

            List<t_TreeMeun> menus = new t_TreeMeunBLL().GetModelList(@"id>0 and id in(select distinct fid from t_treemeun where id in(select treeid from t_treerole_menu where roleid=" + czy.FRole + " and flag>=2 and treeid>0))");

            List<t_TreeMeun> treeMeuns = new List<t_TreeMeun>();

            foreach (t_TreeMeun menu in menus)
            {
                menu.Level = 1;
                treeMeuns.Add(menu);
                List<t_TreeMeun> subMenus = new t_TreeMeunBLL().GetModelList("id in(select treeid from t_treerole_menu where roleid=" + czy.FRole + " and flag>=2 and treeid>0) and Fid=" + menu.id);
                foreach (t_TreeMeun subMenu in subMenus)
                {
                    subMenu.Level = 2;
                    treeMeuns.Add(subMenu);
                }
            }

            ViewData["treeMeuns"] = treeMeuns;
            return View();
        }
        public ActionResult MainPage()
        {
            ViewData["CtrName"] = "SuperMarket";
            return View();
        }
	}
}