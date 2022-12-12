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
    public class GoodManagerController : Controller
    {
        //
        // GET: /GoodManager/
        public ActionResult Index(int id=1)
        {
            //商品类别
            List<T_GoodsType> goodtypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodtypes"] = goodtypes;

            //商品的属性
            List<T_SHO_GoodsAttribute> goodsAttribute = new T_SHO_GoodsAttributeBLL().GetModelList("");
            ViewData["goodsAttribute"] = goodsAttribute;
            //商品信息
            int page = id;
            int pageRow = 10;
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetPageListOfIEnumerable(page, pageRow, "ACTIVE='Y'");
            ViewData["goods"] = goods;

            int pageNumber = new T_GoodsBLL().GetPageNumber(pageRow, "ACTIVE='Y'");
            ViewData["pageNumber"] = pageNumber.ToString();
            return View();
        }
	}
}