using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.IO;

using System.Data;
using System.Configuration;
using System.Text;


namespace SelfhelpOrderMgr.Web.Controllers
{
    public class StockController : BaseController
    {
        //
        // GET: /Stock/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index()
        {
            //商品类别
            List<T_GoodsType> goodtypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodtypes"] = goodtypes;

            //商品状态
            List<T_CommonTypeTab> gStatuses = new T_CommonTypeTabBLL().GetModelList("FType='GoodsZT'");
            ViewData["gStatuses"] = gStatuses;
            //限额类型
            List<T_CommonTypeTab> gFreeTypes = new T_CommonTypeTabBLL().GetModelList("FType='XianE'");
            ViewData["gFreeTypes"] = gFreeTypes;

            //队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("");
            ViewData["areas"] = areas;
            return View();
        }

        public ActionResult GetGoodsByType()
        {
            string typeCode = Request["TypeCode"];
            string active = Request["ACTIVE"];
            if (!string.IsNullOrEmpty(typeCode))
            {
                List<T_Goods> lists = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("GType='" + typeCode + "' and ACTIVE='" + active + "'");
                return Content(jss.Serialize(lists));
            }
            else
            {
                return Content("");
            }
        }


        public ActionResult SaveGoodsStockNumList()//保存商品库存数量
        {
            string strLoginCode = Session["loginUserCode"].ToString();
            string strLoginName = new T_CZYBLL().GetModel(strLoginCode).FName;

            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            string deleted = Request["deleted"];

            string inserted = Request["inserted"];

            string updated = Request["updated"];


            if (inserted != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(inserted);
                if (ja.Count > 0)
                {
                    DoSaveStockNumInfoAdd(ja, strLoginName);
                    //return Content("OK|插入成功" + jss.Serialize(models));
                    return Content("OK|插入成功");
                }
            }

            if (updated != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(updated);
                if (ja.Count > 0)
                {
                    DoSaveStockNumInfoAdd(ja, strLoginName);
                    return Content("OK保存成功！");
                }
            }

            return Content("");
        }

        //执行限量商品写入操作
        private bool DoSaveStockNumInfoAdd(JArray ja, string strLoginName)
        {
            T_GOODSSTOCKMAIN model = new T_GOODSSTOCKMAIN();
            T_Stock stockMainOut = null;//盘点出库主单
            T_Stock stockMainIn = null;//盘点入库主单
            foreach (JObject o in ja)
            {
                model = new T_GOODSSTOCKMAIN();
                model.GCODE = o["GCODE"].ToString();
                model.BALANCE = Convert.ToDecimal(o["Balance"].ToString());
                model.TMPBALANCE = 0;

                //如果商品的数量表是已经存在，则更新
                //如果商品的数量减少，则是盘点出库，如果是数量增加是是盘点入库

                T_StockDTL stockdtl = new T_StockDTL();
                T_Goods good;
                T_GOODSSTOCKMAIN goodstockMain;//商品库存量记录
                List<T_GOODSSTOCKMAIN> stockmains = new T_GOODSSTOCKMAINBLL().GetModelList("GCODE='" + model.GCODE + "'");

                if (stockmains.Count > 0)
                {
                    //盘点入库 StockFlag 1  inoutflag 1
                    //盘点出库 StockFlag 2  inoutflag -1
                    //超市消费 StockFlag 5  inoutflag -1
                    //消费退货 StockFlag 6  inoutflag 1   

                    goodstockMain = stockmains[0];
                    if (model.BALANCE > goodstockMain.BALANCE)
                    {//库存量增加                        
                        if (stockMainIn == null)
                        {
                            stockMainIn = new T_StockBLL().NewStock(strLoginName, "盘点入库", 1, 1);
                        }

                        good = new T_GoodsBLL().GetModel(o["GTXM"].ToString());
                        stockdtl = new T_StockDTL()
                        {
                            StockId = stockMainIn.StockId,
                            GCode = o["GCODE"].ToString(),
                            GTXM = o["GTXM"].ToString(),
                            Flag = 1,
                            GCount = model.BALANCE - goodstockMain.BALANCE,
                            GDJ = good.GDJ,
                            InOutFlag = stockMainIn.InOutFlag,
                            Remark = "修改库存量产生变化",
                            StockFlag = stockMainIn.Stockflag
                        };
                        new T_StockDTLBLL().Add(stockdtl);
                    }
                    else if (model.BALANCE < goodstockMain.BALANCE)
                    {//库存量减少
                        if (stockMainIn == null)
                        {
                            stockMainOut = new T_StockBLL().NewStock(strLoginName, "盘点出库", 2, -1);
                        }

                        good = new T_GoodsBLL().GetModel(o["GTXM"].ToString());
                        stockdtl = new T_StockDTL()
                        {
                            StockId = stockMainOut.StockId,
                            GCode = o["GCODE"].ToString(),
                            GTXM = o["GTXM"].ToString(),
                            Flag = 1,
                            GCount = goodstockMain.BALANCE - model.BALANCE,//反过来减
                            GDJ = good.GDJ,
                            InOutFlag = stockMainOut.InOutFlag,
                            Remark = "修改库存量产生变化",
                            StockFlag = stockMainOut.Stockflag
                        };
                        new T_StockDTLBLL().Add(stockdtl);
                    }
                    if (goodstockMain.LastDate == null)
                    {
                        goodstockMain.LastDate = DateTime.Today;
                    }
                    else if (goodstockMain.LastDate < Convert.ToDateTime("2000-01-01"))
                    {
                        goodstockMain.LastDate = DateTime.Today;
                    }
                    goodstockMain.BALANCE = model.BALANCE;//更新最新库存量
                    new T_GOODSSTOCKMAINBLL().Update(goodstockMain);
                }
                else //如果没有库存量单，则增加一条记录
                {
                    goodstockMain = model;
                    model.LastDate = DateTime.Today;
                    new T_GOODSSTOCKMAINBLL().Add(model);
                }
            }
            return true;
        }




    }
}