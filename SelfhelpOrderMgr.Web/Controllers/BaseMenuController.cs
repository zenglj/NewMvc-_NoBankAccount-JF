using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class BaseMenuController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");
            ViewData["saleTypes"] = saleTypes;
        }
	}
}