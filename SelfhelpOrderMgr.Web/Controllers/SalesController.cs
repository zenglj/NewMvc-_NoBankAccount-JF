using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class SalesController : LoginController
    {
        //
        // GET: /Sales/
        public ActionResult Index(int id = 1)//默认1是超市消费
        {
            //取消原来有在管理表设定的模式，改用在T_Sho_SaleType表 ShoppingFlag设定
            //if (id == 1)
            //{
            //    T_SHO_ManagerSet mgrset = new T_SHO_ManagerSetBLL().GetModel("ShoppingFlag");
            //    if (mgrset != null)
            //    {
            //        if (mgrset.MgrValue == "0")
            //        {
            //            return Redirect("/Shopping/StopShoppingNotice");
            //        }
            //    }
            //}

            int saleTypeId = id;
            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModel(saleTypeId);

            if (saletype.ShoppingFlag == 0)
            {
                return Redirect("/Shopping/StopShoppingNotice");
            }


            //是否启用销售日期，如果启用销售日期，则要判断购买日是否在配置列表中
            T_SHO_ManagerSet mgrset = new T_SHO_ManagerSetBLL().GetModel("SaleDayEnableFlag");
            if (mgrset != null)
            {
                if (mgrset.MgrValue == "1")
                {
                    //如果今天没有在列表里，就说明不能消费，则转到停止消费页面
                    int saledayFlag = new T_SHO_SaleDayListBLL().SaleDayExists(saleTypeId, DateTime.Today);
                    switch (saledayFlag)
                    {
                        case 0:
                            return Redirect("/Shopping/StopShoppingNotice/"+id);
                        case 1:
                            break;
                        case -1:
                            return Redirect("/Shopping/NoAtShoppingTime/"+id);
                    }
                    //if (!saledayFlag)
                    //{
                    //    return Redirect("/Shopping/StopShoppingNotice");
                    //}

                }
            }


            T_SHO_ManagerSet saleTimeSet = new T_SHO_ManagerSetBLL().GetModel("SaleTimeAreaFlag");
            if (saleTimeSet != null)
            {
                if (saleTimeSet.KeyMode == 1)
                {
                    string startTime = saleTimeSet.MgrValue.Substring(0, 5);
                    string endTime = saleTimeSet.MgrValue.Substring(6, 5);
                    if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + startTime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + endTime))
                    {
                        return Redirect("/Shopping/NoAtShoppingTime");
                    }                    
                }
            }


            ViewData["id"] = id;
            ViewData["ptype"] = saletype.PType;
            ViewData["saleTypeId"] = saleTypeId;

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XiaoPiaoStyle");
            ViewData["mset"] = mset;

            T_SHO_ManagerSet loginMode = new T_SHO_ManagerSetBLL().GetModel("LoginMode");
            ViewData["loginMode"] = loginMode.MgrValue;

            T_SHO_ManagerSet softNumerKeyBoard = new T_SHO_ManagerSetBLL().GetModel("SoftNumerKeyBoard");
            if (softNumerKeyBoard == null)
            {
                ViewData["softNumerKeyBoard"] = "0";
            }
            else
            {
                ViewData["softNumerKeyBoard"] = softNumerKeyBoard.MgrValue;
            }
            return View();
        }

        public ActionResult Medicine()//医药
        {
            return View();
        }
        public ActionResult GetGoodsInfo()
        {
            string gtxm = Request["Gtxm"];
            string SaleTypeId = Request["SaleTypeId"];
            List<T_Goods> good = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("gtxm='" + gtxm + "' and gtype in(select fcode from T_GoodsType where saletypeid='" + SaleTypeId + "')");
            
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (good.Count ==1)
            {
                //获取商品的属性
                List<T_SHO_GoodsForAttr> attrs = new T_SHO_GoodsForAttrBLL().GetModelList("GCODE='" + good[0].GCODE + "'","AttrInfo");

                if (good[0].ACTIVE == "N")
                {
                    return Content( "Error|抱谦，该商品已经下架了");
                }
                else
                {
                    return Content("OK|" + jss.Serialize(good[0]) + "|" + jss.Serialize(attrs));
                }


            }
            else if (good.Count > 1)
            {
                return Content("Error|商品条码有重复的记录" );
            }
            else
            {
                string status = "Error|商品信息不存在";
                if (!string.IsNullOrEmpty(gtxm))
                {
                    good = new T_GoodsBLL().GetModelList("SPShortCode='" + gtxm + "' and gtype in(select fcode from T_GoodsType where saletypeid='" + SaleTypeId + "')");
                    if (good.Count == 1)
                    {
                        //获取商品的属性
                        List<T_SHO_GoodsForAttr> attrs = new T_SHO_GoodsForAttrBLL().GetModelList("GCODE='" + good[0].GCODE + "'","AttrInfo");

                        if (good[0].ACTIVE == "N")
                        {
                            return Content("Error|抱谦，该商品已经下架了");
                        }
                        else
                        {
                            status = "OK|" + jss.Serialize(good[0]) + "|" + jss.Serialize(attrs);
                        }

                    }
                }
                return Content(status);
            }
        }

        
	}
}