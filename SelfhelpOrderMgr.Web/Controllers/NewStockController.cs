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
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class NewStockController : Controller
    {
        //
        // GET: /NewStock/
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult GetStocks()
        {
            List<T_Stock> stocks = new T_StockBLL().GetModelList("");
            return Content("");
        }
	}
}