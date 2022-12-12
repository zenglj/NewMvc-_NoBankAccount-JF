using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Configuration;
using System.Text;
using SelfhelpOrderMgr.Common;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using SelfhelpOrderMgr.Web.Filters;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class SuperController : BaseController
    {
        //
        // GET: /Super/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index(int id = 1)
        {
            //商品类别
            List<T_GoodsType> goodtypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodtypes"] = goodtypes;

            //商家
            List<T_Supplyer> supplyers = new T_SupplyerBLL().GetModelList("");
            ViewData["supplyers"] = supplyers;
            //商品的属性
            List<T_SHO_GoodsAttribute> goodsAttribute = new T_SHO_GoodsAttributeBLL().GetModelList("");
            ViewData["goodsAttribute"] = goodsAttribute;
            //商品信息

            //int pageNumber = new T_GoodsBLL().GetPageNumber(pageSize, "ACTIVE='Y'");
            //ViewData["pageNumber"] = pageNumber.ToString();

            //商品状态
            List<T_CommonTypeTab> gStatuses = new T_CommonTypeTabBLL().GetModelList("FType='GoodsZT'");
            ViewData["gStatuses"] = gStatuses;
            //限额类型
            List<T_CommonTypeTab> gFreeTypes = new T_CommonTypeTabBLL().GetModelList("FType='XianE'");
            ViewData["gFreeTypes"] = gFreeTypes;

            string strPage = Request["page"];
            string strPageSize = Request["rows"];
            int page = 1;
            int pageSize = 10;
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }
            if (page == 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(strPageSize) != true)
            {
                pageSize = Convert.ToInt32(strPageSize);
            }
            //List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetPageListOfIEnumerable(page, pageSize, "GType in (select fcode from t_goodsType where SaleTypeId=" + id.ToString() + ")");
            //ViewData["goods"] = goods;

            ViewData["typeId"] = id;

            List<T_SHO_GoodsAttribute> attrs = new T_SHO_GoodsAttributeBLL().GetModelList("");
            ViewData["attrs"] = attrs;

            T_SHO_ManagerSet goodPriceMset = new T_SHO_ManagerSetBLL().GetModel("GoodsChangeSetByPrice");

            ViewData["goodPriceMset"] = "0";
            if (goodPriceMset != null)
            {
                ViewData["goodPriceMset"] = goodPriceMset.MgrValue;
            }

            return View();
        }
        public ActionResult GetGoods(int id = 1)
        {
            string strPage = Request["page"];
            string strPageSize = Request["rows"];
            string strActive = Request["Active"];
            string strFFreeFlag = Request["FFreeFlag"];
            string strGType = Request["GType"];
            string strGTXM = Request["GTXM"];
            string strGName = Request["GName"];
            string selSupplyer = Request["selSupplyer"];
            string FGoodsShortCode = Request["FGoodsShortCode"];
            if (string.IsNullOrEmpty(strActive))
            {
                strActive = "Y";
            }
            int page = 1;
            int pageSize = 10;
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }
            if (page == 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(strPageSize) != true)
            {
                pageSize = Convert.ToInt32(strPageSize);
            }
            string strWhere = "GType in (select fcode from t_goodsType where SaleTypeId=" + id.ToString() + ")";
            if (strActive != "-2")
            {
                strWhere = strWhere + " and Active='" + strActive + "'";
            }

            if (strFFreeFlag != "2")
            {
                strWhere = strWhere + " and FFreeFlag='" + strFFreeFlag + "'";
            }

            if (strGType != "")
            {
                strWhere = strWhere + " and GType='" + strGType + "'";
            }

            if (strGTXM != "")
            {
                strWhere = strWhere + " and GTXM='" + strGTXM + "'";
            }

            if (strGName != "")
            {
                strWhere = strWhere + " and GName like '%" + strGName + "%'";
            }

            if (string.IsNullOrEmpty(selSupplyer) == false)
            {
                strWhere = strWhere + " and GSupplyer = '" + selSupplyer + "'";
            }

            if (string.IsNullOrEmpty(FGoodsShortCode) == false)
            {
                strWhere = strWhere + " and SPShortCode = '" + FGoodsShortCode + "'";
            }
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetPageListOfIEnumerable(page, pageSize, strWhere);
            ViewData["goods"] = goods;


            List<T_Goods> rowList = new T_GoodsBLL().GetModelList(strWhere);

            string sss = "{\"total\":" + rowList.Count.ToString() + ",\"rows\":" + jss.Serialize(goods) + "}";
            return Content(sss);
        }

        public ActionResult GetGoodsType()
        {
            List<T_GoodsType> gtypes = new T_GoodsTypeBLL().GetModelList("");
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string sss = jss.Serialize(gtypes);
            return Content(sss);
        }

        public ActionResult GetSupplyer()
        {
            List<T_Supplyer> supplyers = new T_SupplyerBLL().GetModelList("");
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            string sss = jss.Serialize(supplyers);
            return Content(sss);
        }

        [MyLogActionFilterAttribute]
        public ActionResult SaveGoods()
        {
            string src = "";
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                if (fname != "")
                {
                    string savePath = Server.MapPath("~/Content/GoodsImages/" + fname);
                    f.SaveAs(savePath);
                    src = "/Content/GoodsImages/" + fname;
                }                
            }
            T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");//商品导入模式

            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string doType = Request["Dotype"];
            string gCode = Request["GCode"];
            string gName = Request["GName"];
            string gType = Request["GType"];
            string gUnit = Request["GUnit"];
            string gStandard = Request["GStandard"];
            string gDJ = Request["GDJ"];
            string gSupplyer = Request["GSupplyer"];
            string gTXM = Request["GTXM"];
            string gMadein = Request["GMadein"];
            string gFreeflag = Request["GFreeflag"];
            string gXgsl = Request["Gxgsl"];
            string gXgMode = Request["XgMode"];
            string SPShortCode = Request["SPShortCode"];
            if (string.IsNullOrEmpty(SPShortCode))
            {
                if (mgrSet.KeyMode == 0)
                {
                    return Content("Err|商品简码不能为空");
                }
            }
            string goodsJM = "";
            T_Goods good;
            T_Goods oldGood;
            bool brs = false;
            if (doType == "add")
            {
                List<T_Goods> gds = new T_GoodsBLL().GetModelList("Gtxm='" + gTXM + "' or SPShortCode='" + SPShortCode + "'");
                if (gds.Count > 0)
                {
                    return Content("Error|对不起，该商品编码已经存在了");
                }
                good = new T_Goods();
                good.CrtBy = strLoginName;
                good.Crtdt = DateTime.Now.ToShortDateString();
                good.ModBy = "";
                good.Moddt = DateTime.Now.ToShortDateString();
                good.GCODE = new T_SEQNOBLL().GetSeqTypeNo("G");
                good.XgMode = Convert.ToInt32( gXgMode );
                good.ACTIVE = "Y";
                good.gjm = "";


            }
            else
            {
                List<T_Goods> gGoods = new T_GoodsBLL().GetModelList("SPShortCode='" + SPShortCode + "'");
                if (gGoods.Count > 0)
                {
                    good = gGoods[0];
                    goodsJM = good.SPShortCode;
                }
                else
                {
                    good = new T_GoodsBLL().GetModel(gTXM);
                }
                good.ModBy = strLoginName;
                good.Moddt = DateTime.Now.ToShortDateString();

            }

            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");
            if (mset.KeyMode == 0)
            {
                if (src != "")
                {
                    good.src = src;
                }else
                {
                    good.src = "/Content/GoodsImages/" + SPShortCode + "." + mset.MgrValue;
                }
                good.data = "/Content/GoodsImages/null.png";
            }
            else
            {
                if (src != "")
                {
                    good.src = src;
                }
                else
                {
                    good.src = "/Content/GoodsImages/" + gName + "." + mset.MgrValue;
                }
                good.data = "/Content/GoodsImages/null.png";
            }
            good.GNAME = gName;
            good.GUnit = gUnit;
            good.GStandard = gStandard;


            //good.GDJ = Convert.ToDecimal(gDJ);
            good.GTYPE = gType;
            good.GSupplyer = gSupplyer;
            good.GTXM = gTXM;
            good.madein = gMadein;
            good.Ffreeflag = Convert.ToInt32(gFreeflag);
            good.Xgsl = Convert.ToInt32(gXgsl);
            good.XgMode = Convert.ToInt32(gXgMode);
            good.SPShortCode = SPShortCode;
            if (doType == "add")
            {
                try
                {
                    good.GDJ = Convert.ToDecimal(gDJ);

                    new T_GoodsBLL().Add(good);
                    brs = true;
                    Log4NetHelper.logger.Info("新增商品信息,操作员：" + strLoginName + ",商品编码:" + good.GCODE + ",商品名称=" + good.GNAME + ",商品条码:" + good.GTXM + ",商品店内码:" + good.SPShortCode + ",商品价格:" + good.GDJ.ToString() + ",商品类别:" + good.GTYPE + ",非限额标志:" + good.Ffreeflag.ToString() + "");
                }
                catch
                {
                    brs = false;
                }
            }
            else
            {
                //brs=new T_GoodsBLL().Update(good);


                //如果单价不用申请
                T_SHO_ManagerSet goodPriceMset = new T_SHO_ManagerSetBLL().GetModel("GoodsChangeSetByPrice");
                if (goodPriceMset != null)
                {
                    if (goodPriceMset.MgrValue != "1")
                    {
                        good.GDJ = Convert.ToDecimal(gDJ);
                    }                    
                }


                if (string.IsNullOrEmpty(goodsJM))
                {
                    oldGood = new T_GoodsBLL().GetModel(good.GTXM);//保存更新前商品信息
                    brs = new T_GoodsBLL().Update(good);
                }
                else
                {
                    oldGood = new T_GoodsBLL().GetModelList("SPShortCode='" + good.SPShortCode + "'")[0];//保存更新前商品信息
                    brs = new T_GoodsBLL().UpdateByShortCode(good);
                }
                if (brs == true)
                {

                    if (good.GDJ != oldGood.GDJ)
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(@"INSERT INTO [T_Goods_ChangeList]
                        ([GCode],[GName],[GTXM],[ChangeType],[ChangeTypeName],[OldPrice],[NewPrice]
                        ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                        ,[Remark])
                             VALUES
                        ('" + good.GCODE + "','" + good.GNAME + "','" + good.GTXM + "','1','价格','" + oldGood.GDJ.ToString() + "','" + good.GDJ.ToString() + @"'
                        ,'商品价格调整','" + Session["loginUserName"].ToString() + "','" + DateTime.Now.ToString() + "','自动审核','系统','" + DateTime.Now.ToString() + "','变更不用审核',9,'')");
                        new CommTableInfoBLL().ExecSql(strSql.ToString());
                    }

                    Log4NetHelper.logger.Info("保存更新商品信息,操作员：" + strLoginName + ",更新前，商品编码:" + oldGood.GCODE + ",商品名称=" + oldGood.GNAME + ",商品条码:" + oldGood.GTXM + ",商品店内码:" + oldGood.SPShortCode + ",商品价格:" + oldGood.GDJ.ToString() + ",商品类别:" + oldGood.GTYPE + ",非限额标志:" + oldGood.Ffreeflag.ToString());
                    Log4NetHelper.logger.Info("保存更新商品信息,操作员：" + strLoginName + ",更新后，商品编码:" + good.GCODE + ",商品名称=" + good.GNAME + ",商品条码:" + good.GTXM + ",商品店内码:" + good.SPShortCode + ",商品价格:" + good.GDJ.ToString() + ",商品类别:" + good.GTYPE + ",非限额标志:" + good.Ffreeflag.ToString());

                    
                }

            }


            //JavaScriptSerializer jss = new JavaScriptSerializer();
            if (brs == true)
            {
                return Content("OK|保存成功");
            }
            else
            {
                return Content("Error|保存失败");
            }
        }

        public ActionResult GetGoodAttrs()//获取商品Attr属性
        {
            string GCode = Request["GCode"];
            List<T_SHO_GoodsForAttr> goodattrs;
            if (string.IsNullOrEmpty(GCode) == false)
            {
                goodattrs = new T_SHO_GoodsForAttrBLL().GetModelList("GCode='" + GCode + "'", "AttrInfo");
            }
            else
            {
                goodattrs = new List<T_SHO_GoodsForAttr>();
            }
            return Content(jss.Serialize(goodattrs));
        }

        public ActionResult AddGoodAttrs()//增加商品Attr属性
        {
            string GCode = Request["GCode"];
            string GoodsAttrId = Request["GoodsAttrId"];
            string AttrInfo = Request["AttrInfo"];
            string ID = Request["ID"];
            string attrDoType = Request["attrDoType"];
            if (string.IsNullOrEmpty(GCode) == true)
            {
                return Content("Err|GCode商品编码不能为空");
            }
            if (string.IsNullOrEmpty(GoodsAttrId) == true)
            {
                return Content("Err|商品属性类型不能为空");
            }
            if (string.IsNullOrEmpty(AttrInfo) == true)
            {
                return Content("Err|商品规格不能为空");
            }

            if (string.IsNullOrEmpty(attrDoType) == true)
            {
                return Content("Err|操作类型不能为空");
            }
            if (attrDoType == "add")
            {
                T_SHO_GoodsForAttr goodAttr = new T_SHO_GoodsForAttr();
                goodAttr.GCode = GCode;
                goodAttr.GoodsAttrId = Convert.ToInt32(GoodsAttrId);
                goodAttr.AttrInfo = AttrInfo;
                int i = new T_SHO_GoodsForAttrBLL().Add(goodAttr);
                if (i > 0)
                {
                    goodAttr = new T_SHO_GoodsForAttrBLL().GetModel(i);
                    return Content("OK|" + jss.Serialize(goodAttr));
                }
                else
                {
                    return Content("Err|插入记录失败");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(ID) == false)
                {
                    T_SHO_GoodsForAttr goodAttr = new T_SHO_GoodsForAttrBLL().GetModel(Convert.ToInt32(ID));
                    goodAttr.GoodsAttrId = Convert.ToInt32(GoodsAttrId);
                    goodAttr.AttrInfo = AttrInfo;
                    if (new T_SHO_GoodsForAttrBLL().Update(goodAttr))
                    {
                        return Content("OK|" + jss.Serialize(goodAttr));
                    }
                    else
                    {
                        return Content("Err|更新记录失败");
                    }
                }
            }
            return Content("Err|未知道处理的错误");
        }


        [MyLogActionFilterAttribute]
        public ActionResult DelGoodAttrs()//删除商品Attr属性
        {
            string ID = Request["ID"];
            if (string.IsNullOrEmpty(ID) == false)
            {
                if (new T_SHO_GoodsForAttrBLL().Delete(Convert.ToInt32(ID)))
                {
                    return Content("OK|删除成功");
                }
            }
            return Content("Err|删除失败");
        }


        public ActionResult MulitImageUpload()//多文件上传
        {
            return View("MulitImageUpload");
        }
        public ActionResult BatchUploadImages()//批量上传图片
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Content/GoodsImages/" + fname);
                f.SaveAs(savePath);
                string extName = System.IO.Path.GetExtension(fname).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
                string strImgName = fname.Substring(0, (fname.Length - extName.Length));
                T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");//商品导入模式

                if (!string.IsNullOrEmpty(strImgName))
                {
                    if (mgrSet.KeyMode == 1)
                    {
                        List<T_Goods> goods = new T_GoodsBLL().GetModelList("GName='" + strImgName + "'");
                        if (goods.Count > 0)
                        {
                            T_Goods model = goods[0];
                            model.src = "/Content/GoodsImages/" + fname;
                            model.data = "/Content/GoodsImages/null.png";
                            new T_GoodsBLL().UpdateByShortCode(model);
                        }
                    }
                    else
                    {
                        List<T_Goods> goods = new T_GoodsBLL().GetModelList("SPShortCode='" + strImgName + "'");
                        if (goods.Count > 0)
                        {
                            T_Goods model = goods[0];
                            model.data = "/Content/GoodsImages/" + fname;
                            model.data = "/Content/GoodsImages/null.png";
                            new T_GoodsBLL().UpdateByShortCode(model);
                        }
                    }

                }



            }
            return Content("");
        }
        public ActionResult ChangeGoodsStatus()
        {
            //"GCode": row.GCODE,
            //"GActive": e
            string gtxm = Request["GTXM"];
            string gActive = Request["GActive"];
            T_Goods good = new T_GoodsBLL().GetModel(gtxm);
            good.ACTIVE = gActive;
            good.ModBy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            good.Moddt = DateTime.Now.ToString();

            bool rs = new T_GoodsBLL().Update(good);
            if (rs == true)
            {
                return Content("OK|保存成功");
            }
            else
            {
                return Content("Error|保存失败");
            }

        }


        //更新打印小票次数
        public ActionResult UpdatePrintCount()
        {
            string strInvoices = Request["invoices"];
            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            string myStrInvoices = "'" + strInvoices.Replace("|", "','") + "'";
            if (new T_InvoiceBLL().UpdatePrintCount(myStrInvoices))
            {
                return Content("OK|更新打印次数成功");
            }
            else
            {
                return Content("OK|更新打印次数成功");
            }

        }

        /// <summary>
        /// 删除商品信息  2018年10月30日增加功能 ——zeng
        /// </summary>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult DeleteGood()
        {
            //"GCode": row.GCODE,
            //"GActive": e
            string gtxm = Request["GTXM"];
            T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if (czy == null)
            {
                return Content("Error|操作员不能为空");
            }
            if (czy.FPRIVATE !=1)
            {
                return Content("Error|要删除商品，请与管理员联系");
            }
            T_Goods good = new T_GoodsBLL().GetModel(gtxm);

            bool rs = new T_GoodsBLL().Delete(gtxm);
            if (rs == true)
            {
                return Content("OK|删除成功");
            }
            else
            {
                return Content("Error|删除失败");
            }

        }

        public ActionResult customerPrint(int id=1)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            if (string.IsNullOrEmpty(startTime) == true)
            {
                startTime = DateTime.Today.ToShortDateString();
            }
            if (string.IsNullOrEmpty(endTime) == true)
            {
                endTime = DateTime.Now.ToShortDateString();
                endTime = endTime + " " + DateTime.Now.ToShortTimeString();
            }
            List<T_Invoice> invoices = new T_InvoiceBLL().GetModelList(10, "FCrimeCode='12221212' and Flag=1 and OrderDate>='" + startTime + "' and OrderDate<'" + endTime + "'", "InvoiceNO");
            ViewData["invoices"] = invoices;

            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in ( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            //打印消费记录单时，需要存取两类信息
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("");
            ViewData["saleTypes"] = saleTypes;

            List<T_GoodsType> goodsTypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodsTypes"] = goodsTypes;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from t_vcrd where dtype in(select PType from t_sho_SaleType)");
            List<string> crtbys = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    crtbys.Add(row[0].ToString());
                }
            }
            ViewData["crtbys"] = crtbys;


            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XiaoPiaoStyle");
            ViewData["mset"] = mset;

            ViewData["operatorId"] = id;
            return View();
        }

        public ActionResult GetSearchInvoices()
        {
            string strWhere = GetInvoicesSearchWhere(Session["loginUserCode"].ToString());

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            //分页信息
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
            int page = 1;
            int row = 10;
            decimal listRows = 0;
            string sss = "";
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }

            if (string.IsNullOrEmpty(strRow) == false)
            {
                row = Convert.ToInt32(strRow);
            }

            listRows = new T_InvoiceBLL().GetListCount(strWhere.ToString())[0];

            List<T_Invoice> invoices = new T_InvoiceBLL().GetPageList(page, row, strWhere,startTime,endTime, "FAreaCode,RoomNo,FCriminal");

            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(invoices) + "}";
            return Content(sss);
        }

        //获取打印消费单模块的查询条件
        private string GetInvoicesSearchWhere(string LoginCode)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string FCrtBy = Request["FCrtBy"];
            string TypeFlag = Request["TypeFlag"];
            string Flag = Request["Flag"];
            string strWhere = "";

            if (string.IsNullOrWhiteSpace(Flag))
            {
                Flag = "1";
            }
            if (string.IsNullOrEmpty(strWhere) == false)
            {
                strWhere = strWhere + " and Flag='" + Flag + "'";
            }
            else
            {
                strWhere = strWhere + " Flag='"+ Flag + "'";
            }

            if (string.IsNullOrEmpty(startTime) == true)
            {
                startTime = DateTime.Today.ToShortDateString();
            }
            if (string.IsNullOrEmpty(endTime) == true)
            {
                endTime = DateTime.Now.ToShortDateString();
                endTime = endTime + " " + DateTime.Now.ToShortTimeString();
            }

            if (string.IsNullOrEmpty(strWhere) == false)
            {
                strWhere = strWhere + " and OrderDate between '" + startTime + "' and '" + endTime + "'";
            }
            else
            {
                strWhere = strWhere + " OrderDate between '" + startTime + "' and '" + endTime + "'";
            }
            

            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = strWhere + " and FCrimeCode='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(FCrtBy) == false)
            {
                strWhere = strWhere + " and Crtby='" + FCrtBy + "'";
            }
            if (string.IsNullOrEmpty(TypeFlag) == false)
            {
                strWhere = strWhere + " and TypeFlag in (" + TypeFlag + ") ";
            }

            if (string.IsNullOrEmpty(FName) == false)
            {
                //优化SQL20200311
                //strWhere = strWhere + " and FCriminal like '%" + FName + "%'";
                strWhere = strWhere + " and FCriminal like '" + FName + "%'";
            }

            if ("请选择队别" == areaName)
            {
                areaName = "";
            }
            
            //2019-04-15由于运行太慢会超时去掉
            //            List<T_AREA> areas = new T_AREABLL().GetModelList("fname in( select fname from t_area where fname='" + areaName + @"' or fid in(
            //                                    select id from t_area where fname='" + areaName + "'))");
            //            string strFareaCode = "";
            //            foreach (T_AREA area in areas)
            //            {
            //                if (strFareaCode == "")
            //                {
            //                    strFareaCode = "'" + area.FCode + "'";
            //                }
            //                else
            //                {
            //                    strFareaCode =strFareaCode+ ",'" + area.FCode + "'";
            //                }
            //            }
            //2019-07-09停用，应永安的要求增加 子监区查询功能
            //if (string.IsNullOrEmpty(areaName) == false)
            //{
            //    //2019-04-15由于运行太慢会超时改为等于
            //    //strWhere = strWhere + @" and fareaCode in(" + strFareaCode + ")  ";
            //    strWhere = strWhere + @" and FAreaName ='" + areaName + "' ";
            //}

            //2019-07-09应永安监狱的要求恢复子监区查询
            //增加一个设置判断 AreaSelectMulFlag 是否开启
            if (string.IsNullOrEmpty(areaName) == false)
            {
                T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("AreaSelectMulFlag");
                if (areaMset == null || areaMset.MgrValue == "0")
                {
                    strWhere = strWhere + @" and FAreaName ='" + areaName + "' ";
                }
                else
                {
                    //2019-04-15由于运行太慢会超时去掉
                    //2019-07-09应永安监狱的要求复子监区查询
                    List<T_AREA> areas = new T_AREABLL().GetModelList("fname in( select fname from t_area where fname='" + areaName + @"' or fid in(
                                            select id from t_area where fname='" + areaName + "'))");
                    string strFareaCode = "";
                    foreach (T_AREA area in areas)
                    {
                        if (strFareaCode == "")
                        {
                            strFareaCode = "'" + area.FCode + "'";
                        }
                        else
                        {
                            strFareaCode =strFareaCode+ ",'" + area.FCode + "'";
                        }
                    }

                    strWhere = strWhere + @" and fareaCode in(" + strFareaCode + ")  ";

                }
                //2019-04-15由于运行太慢会超时改为等于
                //strWhere = strWhere + @" and fareaCode in(" + strFareaCode + ")  ";
            }
            
            
            
            


            //验证用户的队别,如果设定了Vcrd验证用户队别，则要查看是否有相应的队别权限下的犯人才可以查询到
            //T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");
            //if (mset != null)
            //{
            //    if (mset.MgrValue == "1")
            //    {
            //        strWhere = strWhere + " and FAreaCode in  ( select fareaCode from t_czy_area where fflag=2 and fcode='" + LoginCode + "')";
            //    }
            //}
            return strWhere;
            
        }

        public ActionResult GetInvoices()
        {
            string strInvoices = Request["invoices"];
            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            List<PrintInvoices> rtnInvs = new List<PrintInvoices>();
            if (invoices.Length > 0)
            {
                for (int i = 0; i < invoices.Length; i++)
                {
                    PrintInvoices rtnInv = new PrintInvoices();
                    rtnInv.invoice = new T_InvoiceBLL().GetModel(invoices[i]);
                    T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");
                    if (mgr.MgrValue == "1")
                    {
                        rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'", 1);
                    }
                    else
                    {
                        rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'");
                    }
                    rtnInv.criminal = new T_CriminalBLL().GetCriminalXE_info(rtnInv.invoice.FCrimeCode, 1);
                    rtnInvs.Add(rtnInv);
                }
            }
            return Content(jss.Serialize(rtnInvs));
        }

        //获取消费单的明细记录
        public ActionResult GetInvoiceDtlByNo()
        {
            string invoiceNo = Request["invoiceNo"];
            if (string.IsNullOrEmpty(invoiceNo) == true)
            {
                return Content("Err|消费主单号不能为空");
            }
            List<T_InvoiceDTL> dtls = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='"+ invoiceNo +"'");
            return Content("OK|" + jss.Serialize(dtls));
        }


        //提交退货单
        [MyLogActionFilterAttribute]
        public ActionResult AddTuiHuoOrder(string details)
        {
            string ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            string crtby = Session["loginUserName"].ToString();
            string ipLastCode = CommonClass.GetIpLastCode(ip);
            //string details = Request["details"];
            if (details != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(details);
                if (ja.Count > 0)
                {
                    List<T_InvoiceDTL> models = SetTuihuoDetail(ja, "Add");
                    //判断退货数量 对应的消费主单：INV210400007120
                    List<T_InvoiceDTL> dtls = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='"+ models[0].INVOICENO +"'");
                    string sql = "select gtxm,count(qty) as qty from t_invoiceDtl where Remark like '%"+ models[0].INVOICENO + "%' group by GTXM";
                    var _rs=new CommTableInfoBLL().GetList<T_InvoiceDTL>(sql, null);

                    var _ms = models.GroupBy(o => o.GTXM).Select(g => new { key = g.Key, value = g.Count() }).ToList();

                    var _ds = dtls.GroupBy(o => o.GTXM).Select(g => new { key = g.Key, value = g.Count() }).ToList();

                    foreach (var item in _ms)
                    {
                        var orderCount = _ds.Where(o => o.key == item.key).FirstOrDefault().value;
                        if (item.value > orderCount)
                        {
                            return Content($"Err|条码：{item.key}超过原购物数量{orderCount}，不能退货");

                        }
                        else
                        {
                            var _r = _rs.Where(o => o.GTXM == item.key).FirstOrDefault();
                            if (_r != null)
                            {
                                if(_r.QTY+ item.value> orderCount)
                                {
                                    return Content($"Err|有历史退货，条码：{item.key}超过原购物数量{orderCount}，不能退货");
                                }
                            }
                        }
                    }
                    string resultInfo = new T_InvoiceBLL().AddTuiHuoOrder(models, crtby, ipLastCode);

                    return Content(resultInfo);
                }
            }
            return Content("Err|eer");
        }

        //设定退货明细
        private static List<T_InvoiceDTL> SetTuihuoDetail(JArray ja, string action)
        {
            List<T_InvoiceDTL> models = new List<T_InvoiceDTL>();
            T_InvoiceDTL model = new T_InvoiceDTL();
            foreach (JObject o in ja)
            {
                model = new T_InvoiceDTL();
                model.FCrimecode = o["FCrimecode"].ToString();
                model.SPShortCode = o["SPShortCode"].ToString();
                model.GTXM = o["GTXM"].ToString();
                model.GCODE = o["GCODE"].ToString();
                model.GDJ = Convert.ToDecimal( o["GDJ"].ToString());
                model.GNAME = o["GNAME"].ToString();
                model.AMOUNT = Convert.ToDecimal( o["AMOUNT"].ToString());
                model.QTY = Convert.ToDecimal(o["QTY"].ToString());
                model.INVOICENO = o["INVOICENO"].ToString();
                DataTable dt = new CommTableInfoBLL().GetDataTable(string.Format("select top 1 ftzsp_typeflag from t_invoiceDtl where invoiceno='{0}' and gtxm='{1}'", o["INVOICENO"].ToString(), o["GTXM"].ToString()));
                model.FTZSP_TypeFlag = Convert.ToInt16( dt.Rows[0][0].ToString());
                models.Add(model);                
            }

            return models;
        }

        [MyLogActionFilterAttribute]
        public ActionResult CancelInvoiceOrder(string Invoices)
        {
            string strInvoices = Invoices;// Request["Invoices"];

            if (string.IsNullOrEmpty(strInvoices))
            {
                return Content("Err|消费单号不能为空");
            }
            strInvoices = strInvoices.Replace("|", "','");
            strInvoices = "'" + strInvoices + "'";

            T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if (czy == null)
            {
                return Content("Err|操作员账户不能为空,请用管理员登录");
            }
            if (czy.FPRIVATE != 1)
            {
                return Content("Err|不是管理员不能删除,请用管理员登录");
            }

            //已经发送到银行不能撤
            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("flag=0 and isnull(bankflag,0)>=1 and Origid in(" + strInvoices + ")");
            if (vcrds.Count > 0)
            {
                return Content("Err|您选的消费单号已经有发送到银行的记录，不能撤消");
            }

            //是否已经到第二个月，并且大于30天了
            List<T_Invoice> dayInvs = new T_InvoiceBLL().GetModelList("Invoiceno in(" + strInvoices + ") and DATEADD(day,30, OrderDate)<getdate()");
            if (dayInvs.Count > 0)
            {
                return Content("Err|您选的消费单超过30天不能撤单，不能撤消");
            }

            //已经配货不能撤
            List<T_Invoice_outdtl> outdtls = new T_Invoice_outdtlBLL().GetModelList("Invoiceno in(" + strInvoices + ")");
            if (outdtls.Count > 0)
            {
                return Content("Err|您选的消费单号已经有配货的记录，不能撤消");
            }


            //离监人员不能撤单
            List<T_Invoice> invs = new T_InvoiceBLL().GetModelList("Invoiceno in(" + strInvoices + ") and fcrimecode in(select fcode from t_criminal where isnull(fflag,0)=1)");
            if (invs.Count > 0)
            {
                return Content($"Err|您选的消费单有{invs[0].FCriminal}等{invs.Count}条已离监，不能撤消");
            }


            

            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            //开始撤单

            if (new T_InvoiceBLL().CancelInvoiceOrder(strInvoices, strLoginName))
            {
                return Content("OK.撤单成功！");
            }
            else
            {
                return Content("Err.对不起，撤单失败！");
            }
        }

        public ActionResult testPrint()
        {
            return View();
        }

        public ActionResult customerDateMgr()//获取特殊时段的相关信息
        {
            T_SHO_ManagerSet xfDateRect = new T_SHO_ManagerSetBLL().GetModel("XFDateRect");
            ViewData["xfDateRect"] = xfDateRect;
            T_SHO_ManagerSet xfDateEnd = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd");
            //ViewData["xfDateEnd"] = xfDateEnd;
            ViewData["xfDateEnd"] = xfDateEnd.StartTime.Month.ToString() + "/" + xfDateEnd.StartTime.Day.ToString() + "/" + xfDateEnd.StartTime.Year.ToString() + " 00:00";
            T_SHO_ManagerSet xfDateStart = new T_SHO_ManagerSetBLL().GetModel("XFDateStart");
            ViewData["xfDateStart"] = xfDateStart.StartTime.Month.ToString() + "/" + xfDateStart.StartTime.Day.ToString() + "/" + xfDateStart.StartTime.Year.ToString() + " 00:00";

            T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetBLL().GetModel("XFBeiShu");
            ViewData["xfBeiShu"] = xfBeiShu;
            return View();
        }

        [MyLogActionFilterAttribute]
        public ActionResult SaveXFDateRect()//保存特殊消费时段设定
        {
            string xfDateRect = Request["XFDateRect"];
            string xfDateStart = Request["XFDateStart"];
            string xfDateEnd = Request["XFDateEnd"];
            string xfBeiShu = Request["XFBeiShu"];
            //是否启用特殊消费时段
            T_SHO_ManagerSet mgs = new T_SHO_ManagerSetBLL().GetModel("XFDateRect");
            mgs.MgrValue = xfDateRect;
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);
            //开始时段
            mgs = new T_SHO_ManagerSetBLL().GetModel("XFDateStart");
            mgs.StartTime = Convert.ToDateTime(xfDateStart);
            new T_SHO_ManagerSetBLL().Update(mgs);
            //结束时段
            mgs = new T_SHO_ManagerSetBLL().GetModel("XFDateEnd");
            mgs.StartTime = Convert.ToDateTime(xfDateEnd);
            new T_SHO_ManagerSetBLL().Update(mgs);
            //可消费的倍数
            mgs = new T_SHO_ManagerSetBLL().GetModel("XFBeiShu");
            mgs.MgrValue = xfBeiShu;
            new T_SHO_ManagerSetBLL().Update(mgs);

            return Content("OK|保存成功");

        }

        public ActionResult XianEGoodsMgr()//限额商品管理
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
            return View("XianEGoodsMgr");
        }

        public ActionResult GetXEGoodTypeList()//获取指定商品类别的列表
        {
            string typeName = Request["TypeName"];
            string fareaInfo = Request["FAreaInfo"];
            string FGoodsName = Request["FGoodsName"];
            string FGoodsGTXM = Request["FGoodsGTXM"];

            string strPage = Request["page"];
            string strPageSize = Request["rows"];
            int page = 1;
            int pageSize = 10;
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }
            if (page == 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(strPageSize) != true)
            {
                pageSize = Convert.ToInt32(strPageSize);
            }


            string strWhere = "1=1 ";
            if (!string.IsNullOrEmpty(typeName))
            {
                strWhere = strWhere + " and FGoodType='" + typeName + "'";
            }
            if (!string.IsNullOrEmpty(fareaInfo))
            {
                strWhere = strWhere + " and FAreaCode='" + fareaInfo + "'";
            }
            if (!string.IsNullOrEmpty(FGoodsName))
            {
                strWhere = strWhere + " and FGoodName='" + FGoodsName + "'";
            }
            if (!string.IsNullOrEmpty(FGoodsGTXM))
            {
                strWhere = strWhere + " and FGtxm='" + FGoodsGTXM + "'";
            }
            List<T_SHO_AreaGoodMaxCount> lists = new T_SHO_AreaGoodMaxCountBLL().GetModelList(page, pageSize, strWhere);
            List<T_SHO_AreaGoodMaxCount> allLists = new T_SHO_AreaGoodMaxCountBLL().GetModelList(strWhere);

            JavaScriptSerializer jss = new JavaScriptSerializer();

            string sss = "{\"total\":" + allLists.Count.ToString() + ",\"rows\":" + jss.Serialize(lists) + "}";

            return Content(sss);

        }

        public ActionResult GetXianEGoodsList()//获取限额商品列表
        {
            return Content("");
        }

        public ActionResult GetGoodsByType()
        {
            string typeName = Request["TypeName"];
            if (!string.IsNullOrEmpty(typeName))
            {
                List<T_SHO_AreaGoodMaxCount> lists = new T_SHO_AreaGoodMaxCountBLL().GetGoodsByTypeModel(typeName);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                return Content(jss.Serialize(lists));
            }
            else
            {
                return Content("");
            }
        }

        [MyLogActionFilterAttribute]
        public ActionResult SaveXianEGoodsList()//保存限额商品列表
        {
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
                    List<T_SHO_AreaGoodMaxCount> models = DoXianLiangInfoAdd(ja);
                    return Content("OK|" + jss.Serialize(models));
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
                    List<T_SHO_AreaGoodMaxCount> models = DoXianLiangInfoAdd(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        //执行限量商品写入操作
        private static List<T_SHO_AreaGoodMaxCount> DoXianLiangInfoAdd(JArray ja)
        {
            T_SHO_AreaGoodMaxCount model = new T_SHO_AreaGoodMaxCount();
            foreach (JObject o in ja)
            {
                model = new T_SHO_AreaGoodMaxCount();
                model.Id = Convert.ToInt32(o["Id"].ToString());
                model.FAreaCode = o["FAreaCode"].ToString();
                model.FAreaName = o["FAreaName"].ToString();
                model.FGtxm = o["FGtxm"].ToString();
                model.FGoodName = o["FGoodName"].ToString();
                model.FGoodType = o["FGoodType"].ToString();
                model.FGoodMaxCount = Convert.ToInt32(o["FGoodMaxCount"].ToString());
                T_SHO_AreaGoodMaxCount m = new T_SHO_AreaGoodMaxCountBLL().GetModel(model.FAreaCode, model.FGtxm);
                if (m != null)
                {
                    m.FGoodMaxCount = model.FGoodMaxCount;
                    if (model.FGoodMaxCount < 0)
                    {
                        bool xx = new T_SHO_AreaGoodMaxCountBLL().Delete(m.Id);
                    }
                    else
                    {
                        bool b = new T_SHO_AreaGoodMaxCountBLL().Update(m);
                    }
                }
                else
                {
                    if (model.FGoodMaxCount >= 0)
                    {
                        int r = new T_SHO_AreaGoodMaxCountBLL().Add(model);
                    }
                }
            }
            List<T_SHO_AreaGoodMaxCount> models = new T_SHO_AreaGoodMaxCountBLL().GetModelList("FAreaCode='" + model.FAreaCode + "' and FGoodType='" + model.FGoodType + "'");
            return models;
        }

        public ActionResult DeleleGoodXLInfo()//删除限量列表，BY Id
        {
            string strRes = "Err|删除失败";
            string strId = Request["Id"];
            if (new T_SHO_AreaGoodMaxCountBLL().Delete(Convert.ToInt32(strId)))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }


        //清空所有限购信息
        public ActionResult DeleleAllGoodXLInfo()
        {
            string strRes = "Err|清空失败";
            string strId = Request["Id"];
            if (new CommTableInfoBLL().ExecSql("delete from T_SHO_AreaGoodMaxCount") > 0)
            {
                strRes = "OK|清空成功";
            }
            return Content(strRes);
        }
        public ActionResult ExcelGoodsInport(int id = 1)//Excel商品信息导入
        {
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                #region 保存文件的方法
                //string fname = f.FileName;
                ///* startIndex */
                //int index = fname.LastIndexOf("\\") + 1;
                ///* length */
                //int len = fname.Length - index;
                //fname = fname.Substring(index, len);
                ///* save to server */
                //string savePath = Server.MapPath("~/Upload/" + fname);
                //f.SaveAs(savePath);
                #endregion

                string savePath = CommonQueryService.SavePostExcelFile(f);

                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");//商品导入模式
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);
                    IWorkbook workbook = null;
                    try
                    {
                        workbook = new XSSFWorkbook(stream); // 2007版本  
                    }
                    catch
                    {
                        workbook = new HSSFWorkbook(stream); // 2003版本  
                    }
                    //HSSFSheet sheet = workbook.GetSheetAt(0);
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(0);
                    //NPOI.SS.UserModel.Sheet
                    int rows = sheet.LastRowNum;
                    int ErrNums = 0, SuccNums = 0;
                    string ErrInfo = "";
                    if (rows < 1)
                    {
                        return Content("Excel表为空表,无数据!");
                    }
                    else
                    {

                        #region 自动下架所有商品
                        T_SHO_ManagerSet mySet = new T_SHO_ManagerSetBLL().GetModel("ShangpinZidongXiajia");
                        if (mySet != null)
                        {
                            if (mySet.MgrValue == "1")
                            {
                                new CommTableInfoBLL().ExecSql("update t_goods set active='N'");
                            }
                        } 
                        #endregion

                        for (int i = 1; i <= rows; i++)
                        {
                            //商品类别,商品名称,商品条码,店内简码,单位,规格,金额，是否限额，是否上架
                            NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                            //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                            string GType = "";//商品类别
                            try
                            {
                                GType = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }
                            catch { }
                            string GName = row.GetCell(1).StringCellValue;//商品名称 列名 以下类似

                            string Gtxm = "";//商品条码
                            NPOI.SS.UserModel.CellType iGtxm = row.GetCell(2).CellType;
                            if (iGtxm == 0)
                            {
                                Gtxm = Convert.ToString(row.GetCell(2).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                Gtxm = row.GetCell(2).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }

                            string SPShortCode = "";//店内简码
                            NPOI.SS.UserModel.CellType iShortCode = row.GetCell(3).CellType;
                            if (iShortCode == 0)
                            {
                                SPShortCode = Convert.ToString(row.GetCell(3).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                SPShortCode = row.GetCell(3).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }

                            string GUnit = "";  //单位
                            try
                            {
                                GUnit = Convert.ToString(row.GetCell(4).StringCellValue);
                            }
                            catch { }

                            string GStandard = "";  //规格
                            try
                            {
                                GStandard = Convert.ToString(row.GetCell(5).StringCellValue);
                            }
                            catch { }

                            string GPrice = "";  //单价
                            try
                            {
                                GPrice = Convert.ToString(row.GetCell(6).NumericCellValue);
                            }
                            catch {
                                GPrice = Convert.ToString(row.GetCell(6).StringCellValue);
                            }
                            
                            string FFreeFlag = "";  //是否限额
                            try
                            {
                                FFreeFlag = Convert.ToString(row.GetCell(7).StringCellValue);
                                if (FFreeFlag == "不限额")
                                {
                                    FFreeFlag = "1";
                                }
                            }
                            catch { }


                            string Active = "";  //是否上架
                            try
                            {
                                Active = Convert.ToString(row.GetCell(8).StringCellValue);
                                if (Active == "下架")
                                {
                                    Active = "N";
                                }
                            }
                            catch { }

                            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
                            string imgExtName = ConfigurationManager.ConnectionStrings["imgExtName"].ConnectionString;

                            bool strFlag = false;
                            //检测并增加一条记录（商品信息）
                            #region 检测并增加一条记录（商品信息）
                            T_Goods model;
                            List<T_Goods> scModels = new T_GoodsBLL().GetModelList("SPShortCode='" + SPShortCode + "'");
                            T_Goods chkModel = null;
                            string tmpSpShortCode = "";
                            if (scModels.Count > 0)
                            {
                                chkModel = scModels[0];

                            }
                            else
                            {
                                scModels = new T_GoodsBLL().GetModelList("GTXM='" + Gtxm + "'");
                                if (scModels.Count > 0)
                                {
                                    chkModel = scModels[0];
                                }
                            }
                            //T_Goods chkModel = new T_GoodsBLL().GetModel(Gtxm);//用条码来验证更新
                            if (chkModel == null)
                            {
                                model = new T_Goods();
                            }
                            else
                            {
                                model = chkModel;
                                Log4NetHelper.logger.Info("Excel导入,保存更新商品信息,操作员：" + strLoginName + ",更新前，商品编码:" + chkModel.GCODE + ",商品名称=" + chkModel.GNAME + ",商品条码:" + chkModel.GTXM + ",商品店内码:" + chkModel.SPShortCode + ",商品价格:" + chkModel.GDJ.ToString() + ",商品类别:" + chkModel.GTYPE + ",非限额标志:" + chkModel.Ffreeflag.ToString());
                            }
                            model.GTXM = Gtxm;
                            model.GNAME = GName;
                            model.GStandard = GStandard;
                            string strGtype = "";
                            #region 测试并添加新商品类型
                            if (!string.IsNullOrEmpty(GType))
                            {
                                try
                                {
                                    strGtype = new T_GoodsTypeBLL().GetModelList("FName='" + GType + "'")[0].Fcode;
                                }
                                catch
                                {
                                    T_GoodsType gt = new T_GoodsType();
                                    T_SEQNO seqModel = new T_SEQNO();
                                    if(new T_SEQNOBLL().GetModelList("SeqType='GT'").Count>0){
                                        seqModel = new T_SEQNOBLL().GetModelList("SeqType='GT'")[0];
                                    }
                                    else{
                                        seqModel.SEQNO=0;
                                    }
                                    

                                    seqModel.SEQNO++;
                                    new T_SEQNOBLL().Update(seqModel);
                                    gt.Fcode = "GT" + seqModel.SEQNO.ToString();
                                    gt.Fname = GType;
                                    gt.Remark = "";
                                    gt.SaleTypeId = id;
                                    gt.FTypeCode = "";
                                    gt.LevelNo = 2;
                                    new T_GoodsTypeBLL().Add(gt);
                                    strGtype = gt.Fcode;
                                }
                            }
                            #endregion
                            model.GTYPE = strGtype;
                            model.GUnit = GUnit;
                            model.GDJ = Convert.ToDecimal(GPrice);
                            if (Active != "N")
                            {
                                model.ACTIVE = "Y";
                            }else
                            {
                                model.ACTIVE = "N";
                            }

                            if (FFreeFlag == "1")
                            {
                                model.Ffreeflag =1;
                            }
                            else
                            {
                                model.Ffreeflag = 0;
                            }
                            
                            if (mgrSet.KeyMode == 1)
                            {
                                model.src = "/Content/GoodsImages/" + GName + "." + mgrSet.MgrValue;
                                model.data = "/Content/GoodsImages/null.png";
                            }
                            else
                            {
                                model.src = "/Content/GoodsImages/" + SPShortCode + "." + mgrSet.MgrValue;
                                model.data = "/Content/GoodsImages/null.png";
                            }
                            tmpSpShortCode = model.SPShortCode;//先保留备份
                            model.SPShortCode = SPShortCode;//商品简码
                            

                            if (chkModel == null)
                            {//增加一条记录
                                //model = new T_Goods();
                                T_SEQNO seqModel = new T_SEQNOBLL().GetModelList("SeqType='G'")[0];
                                seqModel.SEQNO++;
                                new T_SEQNOBLL().Update(seqModel);
                                model.GCODE = "G" + seqModel.SEQNO.ToString();
                                model.CrtBy = strLoginName;
                                model.Ffreeflag = 0;
                                model.Crtdt = DateTime.Now.ToString();
                                model.Moddt = DateTime.Now.ToString();
                                model.ModBy = "";
                                model.madein = "";
                                model.gjm = "";
                                model.gindj = Convert.ToDecimal(GPrice);
                                model.GSupplyer = "";
                                model.Serviceflag = 0;
                                model.subflag = 0;
                                model.Xgsl = 9999;
                                model.COMBFLAG = 0;
                                model.balflag = 0;

                                if (Active != "N")
                                {
                                    model.ACTIVE = "Y";
                                }
                                else
                                {
                                    model.ACTIVE = "N";
                                }

                                if (FFreeFlag == "1")
                                {
                                    model.Ffreeflag = 1;
                                }
                                else
                                {
                                    model.Ffreeflag = 0;
                                }

                                try
                                {
                                    strFlag = new T_GoodsBLL().Add(model);
                                    Log4NetHelper.logger.Info("Excel导入，新增商品信息,操作员：" + strLoginName + ",商品编码:" + model.GCODE + ",商品名称=" + model.GNAME + ",商品条码:" + model.GTXM + ",商品店内码:" + model.SPShortCode + ",商品价格:" + model.GDJ.ToString() + ",商品类别:" + model.GTYPE + ",非限额标志:" + model.Ffreeflag.ToString());
                                }
                                catch
                                {
                                    strFlag = false;
                                    ErrInfo = ErrInfo + "|" + model.SPShortCode;
                                }
                            }
                            else
                            {
                                model.Moddt = DateTime.Now.ToString();
                                model.ModBy = "";
                                //try
                                //{
                                //    strFlag = new T_GoodsBLL().UpdateByShortCode(model);
                                //}
                                //catch
                                //{
                                //    strFlag = false;
                                //    ErrInfo = ErrInfo + "|" + model.SPShortCode;
                                //}
                                if (string.IsNullOrEmpty(tmpSpShortCode))
                                {
                                    strFlag = new T_GoodsBLL().Update(model);
                                }
                                else
                                {
                                    strFlag = new T_GoodsBLL().UpdateByShortCode(model);
                                }
                                if (strFlag)
                                {
                                    //Log4NetHelper.logger.Info("Excel导入,保存更新商品信息,操作员：" + strLoginName + ",更新前，商品编码:" + chkModel.GCODE + ",商品名称=" + chkModel.GNAME + ",商品条码:" + chkModel.GTXM + ",商品店内码:" + chkModel.SPShortCode + ",商品价格:" + chkModel.GDJ.ToString() + ",商品类别:" + chkModel.GTYPE + ",非限额标志:" + chkModel.Ffreeflag.ToString());
                                    Log4NetHelper.logger.Info("Excel导入,保存更新商品信息,操作员：" + strLoginName + ",更新后，商品编码:" + model.GCODE + ",商品名称=" + model.GNAME + ",商品条码:" + model.GTXM + ",商品店内码:" + model.SPShortCode + ",商品价格:" + model.GDJ.ToString() + ",商品类别:" + model.GTYPE + ",非限额标志:" + model.Ffreeflag.ToString());
                                }

                            }
                            #endregion

                            //计算成功失败记录数
                            if (strFlag)
                            {
                                SuccNums++;
                            }
                            else
                            {
                                ErrNums++;
                            }
                        }
                        //获得主单的信息
                        return Content("导入完成，失败记录：" + ErrNums.ToString() + "条，成功：" + SuccNums.ToString() + " 条,失败情况:" + ErrInfo);
                    }
                }
            }
            return Content("");
        }

        public ActionResult ExcelGoodsOut(int id = 1)//导出Excel商品信息
        {

            string strPage = Request["page"];
            string strPageSize = Request["rows"];
            string strActive = Request["Active"];
            string strFFreeFlag = Request["FFreeFlag"];
            string strGType = Request["GType"];
            string strGTXM = Request["GTXM"];
            string strGName = Request["GName"];
            string selSupplyer = Request["selSupplyer"];
            string FGoodsShortCode = Request["FGoodsShortCode"];
            if (string.IsNullOrEmpty(strActive))
            {
                strActive = "Y";
            }

            string strWhere = "GType in (select fcode from t_goodsType where SaleTypeId=" + id.ToString() + ")";
            if (strActive != "-2")
            {
                strWhere = strWhere + " and Active='" + strActive + "'";
            }

            if (strFFreeFlag != "2")
            {
                strWhere = strWhere + " and FFreeFlag='" + strFFreeFlag + "'";
            }

            if (strGType != "")
            {
                strWhere = strWhere + " and GType='" + strGType + "'";
            }

            if (strGTXM != "")
            {
                strWhere = strWhere + " and GTXM='" + strGTXM + "'";
            }

            if (strGName != "")
            {
                strWhere = strWhere + " and a.GName like '%" + strGName + "%'";
            }

            if (string.IsNullOrEmpty(selSupplyer) == false)
            {
                strWhere = strWhere + " and GSupplyer = '" + selSupplyer + "'";
            }
            if (string.IsNullOrEmpty(FGoodsShortCode) == false)
            {
                strWhere = strWhere + " and SPShortCode = '" + FGoodsShortCode + "'";
            }

            string sql = @"SELECT b.[FNAME] 类别,a.[GNAME] 品名,[GTXM] 条码,[SPShortCode] 店内码,[GUnit] 单位,[GStandard] 规格,[GDJ] 单价,Case Active when 'Y' then '正常' else '下架' end 状态
                FROM [T_Goods] a,[T_GoodsType] b where b.FCode=a.GType and " + strWhere + " order by SPShortCode";
            DataTable dt = new CommTableInfoBLL().GetDataTable(sql);
            string strFileName = new CommonClass().GB2312ToUTF8("ExcelOut_GoodsInfo.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|ExcelOut_GoodsInfo.xls");

        }

        #region 商品类型管理
        public ActionResult GoodsTypeMgr()//商品类别管理
        {
            //商品类别
            List<T_GoodsType> goodtypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["goodtypes"] = goodtypes;

            //消费类型
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");
            ViewData["saleTypes"] = saleTypes;

            return View("GoodsTypeMgr");
        }

        public ActionResult GetGoodsTypeMgr()//获取商品类别管理
        {
            //商品类别
            List<T_GoodsType> goodtypes = new T_GoodsTypeBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(goodtypes));
        }

        public ActionResult SaveGoodsTypeList()//保存商品类型列表
        {
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
                    List<T_GoodsType> models = SetGoodsTypeInfo(ja);

                    return Content("OK|" + jss.Serialize(models));
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
                    List<T_GoodsType> models = SetGoodsTypeInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_GoodsType> SetGoodsTypeInfo(JArray ja)
        {
            T_GoodsType model = new T_GoodsType();
            foreach (JObject o in ja)
            {
                model = new T_GoodsType();
                model.Fcode = o["Fcode"].ToString();
                model.Fname = o["Fname"].ToString();
                model.flag = Convert.ToInt32(o["flag"].ToString());
                model.SaleTypeId = Convert.ToInt32(o["SaleTypeId"].ToString());
                model.Remark = o["Remark"].ToString();
                if (o["FTZSP_TypeFlag"].ToString() == "")//该类商品是否属于劳酬专用商品
                {
                    model.FTZSP_TypeFlag = 0;
                }
                else
                {
                    model.FTZSP_TypeFlag = Convert.ToInt32(o["FTZSP_TypeFlag"].ToString());
                }
                //最大购买数量
                if (o["MaxBuyCount"].ToString() == "")
                {
                    model.MaxBuyCount = 0;
                }
                else
                {
                    model.MaxBuyCount = Convert.ToInt32(o["MaxBuyCount"].ToString());
                }

                //限购模式
                if (o["CtrlMode"].ToString() == "")
                {
                    model.CtrlMode = 0;
                }
                else
                {
                    model.CtrlMode = Convert.ToInt32(o["CtrlMode"].ToString());
                }

                model.LevelNo = 2;
                model.FTypeCode = "";
                T_GoodsType m = new T_GoodsTypeBLL().GetModel(model.Fcode);
                if (m != null)
                {
                    bool b = new T_GoodsTypeBLL().Update(model);
                }
                else
                {
                    List<T_SEQNO> seqnos = new T_SEQNOBLL().GetModelList("SeqType='GT'");
                    T_SEQNO seqno = seqnos[0];
                    seqno.SEQNO++;
                    new T_SEQNOBLL().Update(seqno);

                    model.Fcode = "GT" + seqno.SEQNO.ToString();
                    new T_GoodsTypeBLL().Add(model);
                }
            }
            List<T_GoodsType> models = new T_GoodsTypeBLL().GetModelList("");
            return models;
        }

        public ActionResult DeleleGoodsType()//删除商品类型
        {
            string strRes = "Err|删除失败";
            string strFcode = Request["Fcode"];
            if (new T_GoodsTypeBLL().Delete(strFcode))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }

        #endregion

        #region 消费类型管理
        public ActionResult SaleTypeMgr()//消费类型管理
        {
            return View("SaleTypeMgr");
        }
        public ActionResult GetSaleTypesMgr()//获取消费类别管理
        {
            //商品类别
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(saleTypes));
        }

        public ActionResult SaveSaleTypeList()//保存消费类型列表
        {
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
                    List<T_SHO_SaleType> models = SetSaleTypeInfo(ja);

                    return Content("OK|" + jss.Serialize(models));
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
                    List<T_SHO_SaleType> models = SetSaleTypeInfo(ja);
                    return Content("OK保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_SHO_SaleType> SetSaleTypeInfo(JArray ja)
        {
            T_SHO_SaleType model = new T_SHO_SaleType();
            foreach (JObject o in ja)
            {
                model = new T_SHO_SaleType();
                if (o["ID"].ToString() != "")
                {
                    model.ID = Convert.ToInt32(o["ID"].ToString());
                }

                model.PType = o["PType"].ToString();
                model.TypeFlagId = Convert.ToInt32(o["TypeFlagId"].ToString());
                model.CanconsumeAccount = Convert.ToInt32(o["CanconsumeAccount"].ToString());
                model.FirstPaymentAccount = Convert.ToInt32(o["FirstPaymentAccount"].ToString());
                model.ShoppingFlag = Convert.ToInt32(o["ShoppingFlag"].ToString());
                model.Remark = o["Remark"].ToString();
                model.Fifoflag = Convert.ToInt32(o["Fifoflag"].ToString());
                T_SHO_SaleType m = new T_SHO_SaleTypeBLL().GetModel(model.ID);
                if (m != null)
                {
                    bool b = new T_SHO_SaleTypeBLL().Update(model);
                }
                else
                {
                    new T_SHO_SaleTypeBLL().Add(model);
                }
            }
            List<T_SHO_SaleType> models = new T_SHO_SaleTypeBLL().GetModelList("");
            return models;
        }

        [MyLogActionFilterAttribute]
        public ActionResult DeleleSaleType()//删除商品类型
        {
            string strRes = "Err|删除失败";
            string strId = Request["ID"];
            if (new T_SHO_SaleTypeBLL().Delete(Convert.ToInt32(strId)))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }
        #endregion

        public ActionResult OpenComputerMode()//电脑开机模式
        {
            T_SHO_ManagerSet OpenMode = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
            ViewData["OpenMode"] = OpenMode;

            //结算时Vcrd表CheckFlag值
            T_SHO_ManagerSet JsVcrdCheckFlag = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCheckFlag");
            ViewData["JsVcrdCheckFlag"] = JsVcrdCheckFlag;

            //关闭系统密码
            T_SHO_ManagerSet ExitSystemPwd = new T_SHO_ManagerSetBLL().GetModel("ExitSystemPwd");
            ViewData["ExitSystemPwd"] = ExitSystemPwd;

            //Excel导入时图片的格式及名称标准
            T_SHO_ManagerSet ImgDRGS = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");
            ViewData["ImgDRGS"] = ImgDRGS;

            //是否按商品汇总打印小票
            T_SHO_ManagerSet PrintSumOpion = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");
            ViewData["PrintSumOpion"] = PrintSumOpion;

            //结算时是否按自动打印小票
            T_SHO_ManagerSet PrintXiaoPiao = new T_SHO_ManagerSetBLL().GetModel("PrintXiaoPiao");
            ViewData["PrintXiaoPiao"] = PrintXiaoPiao;

            //购买时是否需要验证库存量是否够扣
            T_SHO_ManagerSet YanZhenKCL = new T_SHO_ManagerSetBLL().GetModel("YanZhenKCL");
            ViewData["YanZhenKCL"] = YanZhenKCL;

            return View("OpenComputerMode");
        }
        public ActionResult SaveOpenComputerMode()//保存电脑开机模式
        {
            string OpenMode = Request["OpenMode"];
            string OpenModeName = Request["OpenModeName"];
            string OpenModeHour = Request["OpenModeHour"];
            string PrintSumOpion = Request["PrintSumOpion"];
            string PrintXiaoPiao = Request["PrintXiaoPiao"];
            string YanZhenKCL = Request["YanZhenKCL"];
            string ImgDRGS_Mode = Request["ImgDRGS_Mode"];
            string ImgDRGS_Format = Request["ImgDRGS_Format"];
            string JsVcrdCheckFlag = Request["JsVcrdCheckFlag"];
            string ExitSystemPwd = Request["ExitSystemPwd"];


            //是否启用特殊消费时段
            T_SHO_ManagerSet mgs = new T_SHO_ManagerSetBLL().GetModel("OpenMode");
            mgs.KeyMode = Convert.ToInt32(OpenMode);
            mgs.MgrName = OpenModeName;
            mgs.MgrValue = OpenModeHour;
            new T_SHO_ManagerSetBLL().Update(mgs);

            //打印小票
            mgs = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");//小票自动汇总
            mgs.MgrValue = PrintSumOpion;
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);
            mgs = new T_SHO_ManagerSetBLL().GetModel("PrintXiaoPiao");//自动打印小票
            mgs.MgrValue = PrintXiaoPiao;
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);

            //验证库存量
            mgs = new T_SHO_ManagerSetBLL().GetModel("YanZhenKCL");
            mgs.MgrValue = YanZhenKCL;
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);


            //Excel导入商品时图片命名方式及图片格式
            mgs = new T_SHO_ManagerSetBLL().GetModel("ImgDRGS");
            mgs.KeyMode = Convert.ToInt32(ImgDRGS_Mode);//0是按简码命名，1是按品名命名
            mgs.MgrValue = ImgDRGS_Format;//有jpg 和  png 两种格式
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);


            //结算时Vcrd 的 CheckFlag 状态
            mgs = new T_SHO_ManagerSetBLL().GetModel("JsVcrdCheckFlag");
            mgs.MgrValue = JsVcrdCheckFlag;//有 -1 和 0 两种，
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);

            //前台关机退出密码
            mgs = new T_SHO_ManagerSetBLL().GetModel("ExitSystemPwd");
            mgs.MgrValue = ExitSystemPwd;
            mgs.StartTime = DateTime.Now;
            new T_SHO_ManagerSetBLL().Update(mgs);

            return Content("OK|保存成功");
        }

        public ActionResult CopyToAreas()//将相同的限量设定复制到其他监区去
        {
            string rtnInfo = "Err|复制失败";
            string farea = Request["FArea"];
            if (string.IsNullOrEmpty(farea) == true)
            {
                return Content("Err|请选择一个有效的队别，谢谢");
            }
            if (new T_SHO_AreaGoodMaxCountBLL().CopyInfoToAears(farea))
            {
                rtnInfo = "OK|复制成功";
            }
            return Content(rtnInfo);
        }

        //打印小票单A4纸格式
        public ActionResult PrintXiaofeiDan()
        {
            string strInvoices = Request["invoices"];
            string printType = Request["printType"];

            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            List<PrintInvoices> rtnInvs = new List<PrintInvoices>();
            T_Invoice inv;
            #region 获取消费小票单信息
            if (invoices.Length > 0)
            {
                for (int i = 0; i < invoices.Length; i++)
                {
                    PrintInvoices rtnInv = new PrintInvoices();
                    inv = new T_InvoiceBLL().GetModel(invoices[i]);
                    if (inv != null)
                    {
                        rtnInv.invoice = inv;
                        T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");
                        if (mgr.MgrValue == "1")
                        {
                            rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'", 1);
                        }
                        else
                        {
                            rtnInv.details = new T_InvoiceDTLBLL().GetModelList("InvoiceNo='" + invoices[i] + "'");
                        }
                        rtnInv.criminal = new T_CriminalBLL().GetCriminalXE_info(rtnInv.invoice.FCrimeCode, 1);

                        //2022年改为到账户清单中统计余额
                        T_Criminal_card crimeBalance = new T_CriminalBLL().GetInvoiceBalance(rtnInv.invoice.FCrimeCode, rtnInv.invoice.InvoiceNo);
                        rtnInv.invoice.FrealAreaName = $"消费余额:{crimeBalance.BankAmount}(接济款:{crimeBalance.AmountA},报酬:{crimeBalance.AmountB},留存:{crimeBalance.AmountC})";
                        

                        rtnInvs.Add(rtnInv);
                    }

                }
            }
            #endregion



            if (string.IsNullOrEmpty(printType))
            {
                printType = "1";
            }
            ViewData["printType"] = printType;
            ViewData["rtnInvs"] = rtnInvs;
            return View();
        }

        //id=1是按队别汇总，id=2是按号房汇总
        public ActionResult PrintSumOrder(int id = 1)
        {
            string strWhere = GetInvoicesSearchWhere(Session["loginUserCode"].ToString());


            string GoodsType = Request["GoodsType"];
            string GoodName = Request["GoodName"];
            string GoodGTXM = Request["GoodGTXM"];
            string SpShortCode = Request["SpShortCode"];

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string Flag = Request["Flag"];

            if (string.IsNullOrWhiteSpace(Flag))
            {
                Flag = "1";
            }

            StringBuilder strSql = new StringBuilder();
            switch (id)
            {
                case 0:
                    {
                        strSql.Append("select b.FAreaName FAreaName,a.SPShortCode SPShortCode,a.gname GName");
                        strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty* b.fifoflag)) FCount");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime,Flag);
                        strSql.Append(" group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by b.FAreaName,a.SPShortCode ");

                    } break;
                case 1:
                    {
                        strSql.Append("select b.FAreaName FAreaName,isnull(RoomNo,'')  RoomNo,a.SPShortCode SPShortCode,a.gname GName");
                        strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty * b.fifoflag)) FCount");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append("group by b.FAreaName,isnull(RoomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by b.FAreaName,a.SPShortCode,isnull(RoomNo,'') ");
                    } break;
                case 2:
                    {
                        strSql.Append("select '' FAreaName,a.SPShortCode SPShortCode,a.gname GName");
                        strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty * b.fifoflag)) FCount");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                    } break;
                case 3:
                    {
                        strSql.Append(@" select '' FAreaName,d.gtype SPShortCode,e.fname GName ,'' Remark,'' GTXM
                            ,sum(case when b.fifoflag=-1 then -a.qty * b.fifoflag else 0 end) FCount
                            ,sum(case when b.fifoflag=-1 then -a.amount * b.fifoflag else 0 end) FMoney
                            ,sum(case when b.fifoflag=1 then -a.qty * b.fifoflag else 0 end) thCount
                            ,sum(case when b.fifoflag=1 then -a.amount * b.fifoflag else 0 end) thMoney ");
                        
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by d.gtype,e.fname");

                    } break;
                case 4:
                    {
                        strSql.Append("select a.gtxm,a.spshortcode,a.gname +'('+ isnull(d.GStandard,'') +')' as gname,a.gdj ");
                        strSql.Append(@", convert(numeric(18,0),abs(sum( case when b.fifoflag=-1 then  a.qty else 0 end))) xfCount,abs(sum(case when b.fifoflag=-1 then a.amount else 0 end)) xfMoney,
                                convert(numeric(18,0),abs(sum( case when b.fifoflag=1 then  a.qty else 0 end))) thCount,abs(sum(case when b.fifoflag=1 then a.amount else 0 end)) thMoney ");
                        strSql.Append(",abs(sum(a.qty * b.fifoflag)) FCount,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 0);
                        strSql.Append(" group by a.gname +'('+ isnull(d.GStandard,'') +')',a.gtxm,a.spshortcode,a.gdj ");
                        strSql.Append(" order by a.spshortcode,a.gname +'('+ isnull(d.GStandard,'') +')',a.gtxm,a.gdj ");
                    } break;
                case 5:
                    {
                        strSql.Append("select a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,c.bankflag,a.gdj ");
                        strSql.Append(",abs(sum(a.qty * b.fifoflag)) FCount,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 1);

                        strSql.Append(" group by a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,c.bankflag,a.gdj ");
                        strSql.Append(" order by c.bankflag,a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,a.gdj ");
                    } break;
                case 6:
                    {
                        if (string.IsNullOrEmpty(GoodsType))
                        {
                            return Content("Error|您选择的商品类型不能为空");
                        }
                        DataTable dtGoodNames = new CommTableInfoBLL().GetDataTable("select GName,GStandard,GDJ from t_goods where GType='" + GoodsType + "'");
                        strSql.Append(@"select b.fareaCode,b.fareaName as 队别 ");
                        foreach (DataRow row in dtGoodNames.Rows)
                        {
                            strSql.Append(@",SUM(CASE WHEN a.GName = N'" + row["GName"].ToString() + "' THEN a.qty ELSE 0 END) AS '" + row["GName"].ToString() + "' ");
                        }
                        T_GoodsType gt = new T_GoodsTypeBLL().GetModel(GoodsType);

                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by b.fareaCode,b.fareaName");
                        strSql.Append(" order by b.fareaCode");

                        DataTable dt =new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        
                        DataRow rowGdj = dt.NewRow();
                        rowGdj[0] = "";
                        rowGdj[1] = "单价元/份";
                        for (int i = 0; i < dtGoodNames.Rows.Count;i++ )
                        {
                            //rowGStandard[i + 1] = dtGoodNames.Rows[i]["GStandard"];
                            rowGdj[i + 2] = dtGoodNames.Rows[i]["GDJ"];
                        }

                        dt.Rows.InsertAt(rowGdj, 0);
                        


                        strSql = new StringBuilder();
                        strSql.Append(@"select '' as fareaName,'合计(份数)' as 队别 ");
                        foreach (DataRow row in dtGoodNames.Rows)
                        {
                            strSql.Append(@",SUM(CASE WHEN a.GName = N'" + row["GName"].ToString() + "' THEN a.qty ELSE 0 END) AS '" + row["GName"].ToString() + "' ");
                        }
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        

                        DataTable dtCount = new CommTableInfoBLL().GetDataTable(strSql.ToString());

                        dt.ImportRow (dtCount.Rows[0]);

                        strSql = new StringBuilder();
                        strSql.Append(@"select '' as fareaName,'合计(金额)' as 队别 ");
                        foreach (DataRow row in dtGoodNames.Rows)
                        {
                            strSql.Append(@",SUM(CASE WHEN a.GName = N'" + row["GName"].ToString() + "' THEN a.amount ELSE 0 END) AS '" + row["GName"].ToString() + "' ");
                        }
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        
                        DataTable dtAmount = new CommTableInfoBLL().GetDataTable(strSql.ToString());

                        dt.ImportRow(dtAmount.Rows[0]);


                        
                        //把合计数改为文本
                        DataTable rstDt = UpdateDataTable(dt);

                        DataRow rowGStandard = rstDt.NewRow();
                        rowGStandard[0] = "";
                        rowGStandard[1] = "规格";
                        for (int i = 0; i < dtGoodNames.Rows.Count; i++)
                        {
                            rowGStandard[i + 2] = dtGoodNames.Rows[i]["GStandard"];
                        }
                        rstDt.Rows.InsertAt(rowGStandard, 0);

                        ViewData["dtGoodNames"] = dtGoodNames;
                        ViewData["dt"] = rstDt;
                        ViewData["GoodsType"] = gt.Fname;
                        ViewData["startTime"] = startTime;
                        ViewData["endTime"] = endTime;
                        return View("PrintRowSumOrder");
                    }
                default:
                    return Content("Err|您传入错误的参数");
            }

            List<PeihuoDanPrintList> phds = new CommTableInfoBLL().GetListData(strSql.ToString());
            ViewData["phds"] = phds;
            ViewData["roomNoFlag"] = id.ToString();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            return View();

        }


        /// <summary>
        /// 修改数据表DataTable某一列的类型和记录值(正确步骤：1.克隆表结构，2.修改列类型，3.修改记录值，4.返回希望的结果)
        /// </summary>
        /// <param name="argDataTable">数据表DataTable</param>
        /// <returns>数据表DataTable</returns>  

        private DataTable UpdateDataTable(DataTable argDataTable)
        {
            DataTable dtResult = new DataTable();
            //克隆表结构
            dtResult = argDataTable.Clone();
            foreach (DataColumn col in dtResult.Columns)
            {
                if (col.DataType != typeof(String))
                {
                    //修改列类型
                    col.DataType = typeof(String);
                }
            }
            foreach (DataRow row in argDataTable.Rows)
            {
                DataRow rowNew = dtResult.NewRow();
                rowNew[0] = row[0];
                rowNew[1] = row[1];
                //修改记录值
                for (int i = 2; i < argDataTable.Columns.Count;i++ )
                {
                    if (Convert.ToDecimal(row[i]) == Convert.ToDecimal(Convert.ToInt32(row[i])))
                    {
                        rowNew[i] = Convert.ToInt32(row[i]).ToString();
                    }
                    else
                    {
                        rowNew[i] =(row[i]).ToString();
                    }
                    

                }

                dtResult.Rows.Add(rowNew);
            }
            return dtResult;
        }

        public ActionResult PrintAllXiaofeiDan(int id = 1)
        {
            string strWhere = GetInvoicesSearchWhere(Session["loginUserCode"].ToString());


            string GoodsType = Request["GoodsType"];
            string GoodName = Request["GoodName"];
            string GoodGTXM = Request["GoodGTXM"];
            string SpShortCode = Request["SpShortCode"];

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            string Flag = Request["Flag"];
            if (string.IsNullOrWhiteSpace(Flag))
            {
                Flag = "1";
            }

            StringBuilder strSql = new StringBuilder();
            switch (id)
            {
                case 1:
                    {
                        strSql.Append("select distinct b.invoiceno");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);


                    } break;
                default:
                    return Content("Err|您传入错误的参数");
            }

            List<T_Invoice> phds = new T_InvoiceBLL().GetModelList("Invoiceno in(" + strSql.ToString() + ")");

            
            
            
            ViewData["phds"] = phds;
            ViewData["roomNoFlag"] = id.ToString();
            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;
            return View();

        }

        //获取商品相关信息的子条件
        private static void GetGoodSubWhere(string GoodsType, string GoodName, string GoodGTXM, string SpShortCode, string strWhere, StringBuilder strSql, string startDate, string endDate,string Flag, int invListFlag=0)
        {
            strSql.Append(" from t_invoicedtl a inner join (select * from t_invoice where " + strWhere + ") b  ");
            strSql.Append(" on a.invoiceno=b.invoiceno ");
            if (invListFlag == 1)
            {
                strSql.Append(" left join (select distinct origid,senddate,bankflag from t_vcrd where isnull(origid,'')<>'' ");
                //strSql.Append(" and typeflag in (select typeflagid from t_sho_saletype) ");
                strSql.Append(" and crtDate between '" + startDate + "' and '" + endDate + "' ");
                strSql.Append(" ) c ");
                strSql.Append(" on a.invoiceno=c.origid");
            }
            
            strSql.Append(" left join t_goods d ");
            strSql.Append(" on a.gtxm=d.gtxm ");
            strSql.Append(" left outer join t_goodstype e ");
            strSql.Append(" on d.gtype=e.fcode ");
            strSql.Append(" where b.flag='"+ Flag +"' ");            

            if (string.IsNullOrEmpty(GoodGTXM) == false)
            {
                strSql.Append(" and a.gtxm='" + GoodGTXM + "' ");
            }
            if (string.IsNullOrEmpty(SpShortCode) == false)
            {
                strSql.Append(" and a.spshortCode='" + SpShortCode + "' ");
            }
            if (string.IsNullOrEmpty(GoodsType) == false)
            {
                strSql.Append(" and d.gtype='" + GoodsType + "' ");
            }
            if (string.IsNullOrEmpty(GoodName) == false)
            {
                strSql.Append(" and a.gname like '%" + GoodName + "%' ");
            }
        }

        //id=0是按队别汇总，id=1是按号房汇总
        public ActionResult ExcelSumOrder(int id)//按分监区生成Excel订货单
        {
            string GoodsType = Request["GoodsType"];
            string GoodName = Request["GoodName"];
            string GoodGTXM = Request["GoodGTXM"];
            string SpShortCode = Request["SpShortCode"];

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            string Flag = Request["Flag"];
            if (string.IsNullOrWhiteSpace(Flag))
            {
                Flag = "1";
            }

            string strWhere = GetInvoicesSearchWhere(Session["loginUserCode"].ToString());
            StringBuilder strSql = new StringBuilder();


            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("ExcekDecimalPointNum");
            string strPointNum = "2";
            if (mset != null)
            {
                strPointNum = mset.MgrValue;
            }

            switch (id)
            {
                case 0:
                    {
                        //strSql.Append("select b.FAreaName 队别,a.SPShortCode 货号,a.gname 品名");
                        //strSql.Append(" ,isnull(a.Remark,'') 规格,a.gtxm 条码,abs(isnull(sum(a.qty * b.fifoflag),0)) 数量");
                        //strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 金额 ");
                        //strSql.Append(" from t_invoicedtl a inner join (select * from t_invoice where " + strWhere + ") b  ");
                        //strSql.Append(" on a.invoiceno=b.invoiceno ");
                        //strSql.Append(" where a.flag=1 and not b.FAreaName is null ");
                        //strSql.Append(" where a.flag=1 and not b.FAreaName is null and a.OrderDate between '" + startTime + "' and '"+ endTime +"' ");
                        //strSql.Append(" group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");


                        //strSql.Append("select b.FAreaName FAreaName,a.SPShortCode SPShortCode,a.gname GName");
                        //strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty* b.fifoflag)) FCount");
                        //strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");


                        strSql.Append("select b.FAreaName 队别,a.SPShortCode 货号,a.gname 品名");
                        strSql.Append(" ,isnull(a.Remark,'') 规格,a.gtxm 条码,d.GUnit 单位,a.gdj 单价,convert(numeric(18," + strPointNum + @"),abs(isnull(sum(a.qty * b.fifoflag),0))) 数量");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 金额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 0);
                        strSql.Append(" group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm,d.GUnit,a.gdj ");


                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("AreaGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品订货信息", 8, strFileName);
                        return Content("OK|AreaGoodInfo.xls");
                    }
                case 1:
                    {
                        strSql.Append(@"select 队别
                            , 货号,品名 , 规格,单位, 单价 
                            ,case sum( [1号房] ) when 0 then '' else convert(varchar(10), sum( [1号房] )) end as [1号房]
                            ,case sum( [2号房] ) when 0 then '' else convert(varchar(10), sum( [2号房] )) end as [2号房]
                            ,case sum( [3号房] ) when 0 then '' else convert(varchar(10), sum( [3号房] )) end as [3号房]
                            ,case sum( [4号房] ) when 0 then '' else convert(varchar(10), sum( [4号房] )) end as [4号房]
                            ,case sum( [5号房] ) when 0 then '' else convert(varchar(10), sum( [5号房] )) end as [5号房]
                            ,case sum( [6号房] ) when 0 then '' else convert(varchar(10), sum( [6号房] )) end as [6号房]
                            ,case sum( [7号房] ) when 0 then '' else convert(varchar(10), sum( [7号房] )) end as [7号房]
                            ,case sum( [8号房] ) when 0 then '' else convert(varchar(10), sum( [8号房] )) end as [8号房]
                            ,case sum( [9号房] ) when 0 then '' else convert(varchar(10), sum( [9号房] )) end as [9号房]
                            ,case sum( [10号房] ) when 0 then '' else convert(varchar(10), sum( [10号房] )) end as [10号房]
                            ,case sum( [11号房] ) when 0 then '' else convert(varchar(10), sum( [11号房] )) end as [11号房]
                            ,case sum( [12号房] ) when 0 then '' else convert(varchar(10), sum( [12号房] )) end as [12号房]
                            ,case sum( [13号房] ) when 0 then '' else convert(varchar(10), sum( [13号房] )) end as [13号房]
                            ,case sum( [14号房] ) when 0 then '' else convert(varchar(10), sum( [14号房] )) end as [14号房]
                            ,case sum( [15号房] ) when 0 then '' else convert(varchar(10), sum( [15号房] )) end as [15号房]
                            ,case sum( [16号房] ) when 0 then '' else convert(varchar(10), sum( [16号房] )) end as [16号房]
                            ,case sum( [未知号房] ) when 0 then '' else convert(varchar(10), sum( [未知号房] )) end as [未知号房]
                            ,sum([1号房]+[2号房]+[3号房]+[4号房]+[5号房]+[6号房]+[7号房]+[8号房]+[9号房]+[10号房]+[11号房]+[12号房]+[13号房]+[14号房]+[15号房]+[16号房]+[未知号房]) as 数量
                            ,convert(numeric(18,2),sum([1号房]+[2号房]+[3号房]+[4号房]+[5号房]+[6号房]+[7号房]+[8号房]+[9号房]+[10号房]+[11号房]+[12号房]+[13号房]+[14号房]+[15号房]+[16号房]+[未知号房])*[单价]) as 金额
                            from (");
                        strSql.Append("select b.FAreaName 队别,a.SPShortCode 货号,a.gname 品名");
                        strSql.Append(" ,isnull(a.Remark,'') 规格,a.SPShortCode 简码,a.gtxm 条码,d.GUnit 单位,a.gdj 单价");
                        strSql.Append(" ,case isnull(roomno,'') when 1 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '1号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 2 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '2号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 3 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '3号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 4 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '4号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 5 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '5号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 6 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '6号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 7 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '7号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 8 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '8号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 9 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '9号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 10 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '10号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 11 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '11号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 12 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '12号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 13 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '13号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 14 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '14号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 15 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '15号房'");
                        strSql.Append(" ,case isnull(roomno,'') when 16 then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '16号房'");
                        strSql.Append(" ,case isnull(roomno,'') when '' then convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  else 0 end as '未知号房'");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 金额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append("group by b.FAreaName,isnull(roomno,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm,d.GUnit,a.gdj ");
                        strSql.Append(") as t group by 队别, 货号,品名 , 规格, 单位, 单价  ");
                        strSql.Append(" order by 队别, 货号 ");
                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("RoomGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        //ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品订货信息", 10, strFileName, 1);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品订货信息", 24, strFileName, 0);
                        return Content("OK|RoomGoodInfo.xls");
                    }
                case 2:
                    {
                        strSql.Append("select '' 队别,a.SPShortCode 货号,a.gname 品名");
                        strSql.Append(" ,isnull(a.Remark,'') 规格,a.gtxm 条码,d.GUnit 单位,a.gdj 单价,convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag))) 数量");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 金额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm,d.GUnit,a.gdj");
                        strSql.Append(" order by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm,d.GUnit,a.gdj");
                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("GCodeGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品订货信息", 8, strFileName);
                        return Content("OK|GCodeGoodInfo.xls");
                    }
                case 3:
                    {

                        strSql.Append("select x.SPShortCode 类别号,x.GName 类别名称");
                        strSql.Append(",x.FCount 销售数量,x.FMoney 销售金额");
                        strSql.Append(",isnull(y.FCount,0) 退货数量,isnull(y.FMoney,0) 退货金额 ");
                        strSql.Append(",x.FMoney-isnull(y.FMoney,0) 总金额 ");
                        strSql.Append("from ( ");

                        strSql.Append("select '' FAreaName,d.gtype SPShortCode,e.fname GName");
                        strSql.Append(" ,'' Remark,'' GTXM,abs(sum(a.qty * b.fifoflag)) FCount");
                        strSql.Append(",abs(sum(a.amount * b.fifoflag)) FMoney  ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" and b.fifoflag=-1");
                        strSql.Append(" group by d.gtype,e.fname");
                        //strSql.Append(" order by d.gtype,sum(a.amount)desc,sum(a.qty) desc");
                        strSql.Append(") x  ");
                        strSql.Append(" left outer join ( ");

                        strSql.Append("select '' FAreaName,d.gtype SPShortCode,e.fname GName");
                        strSql.Append(" ,'' Remark,'' GTXM,sum(a.qty * b.fifoflag) FCount");
                        strSql.Append(",sum(a.amount * b.fifoflag) FMoney  ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" and b.fifoflag=1");
                        strSql.Append(" group by d.gtype,e.fname");
                        //strSql.Append(" order by d.gtype,sum(a.amount)desc,sum(a.qty) desc");

                        strSql.Append(") y on x.spshortCode=y.spshortCode  ");
                        strSql.Append(" order by x.spshortCode");


                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("TypeSumGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品大类汇总", 6, strFileName);
                        return Content("OK|TypeSumGoodInfo.xls");
                    }
                case 4:
                    {
                        strSql.Append("select a.gtxm 条码,a.spshortcode 店内码,a.gname+'('+ isnull(d.GStandard,'') +')' [品名/规格],a.gdj 单价 ");
                        strSql.Append(@", convert(numeric(18,0),abs(sum( case when b.fifoflag=-1 then  a.qty else 0 end))) 消费量,abs(sum(case when b.fifoflag=-1 then a.amount else 0 end)) 消费额,
                                convert(numeric(18,0),abs(sum( case when b.fifoflag=1 then  a.qty else 0 end))) 退货量,abs(sum(case when b.fifoflag=1 then a.amount else 0 end)) 退货额 ");
                        strSql.Append(",convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  实售量,abs(sum(a.amount * b.fifoflag))  实售额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 0);
                        strSql.Append(" group by a.gname +'('+ isnull(d.GStandard,'') +')',a.gtxm,a.spshortcode,a.gdj ");
                        strSql.Append(" order by a.spshortcode,a.gname +'('+ isnull(d.GStandard,'') +')',a.gtxm,a.gdj ");
                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("PriceGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, "商品销售分类统计报表", 9, strFileName);
                        return Content("OK|PriceGoodInfo.xls");
                    }
                case 5:
                    {
                        strSql.Append("select a.invoiceno 流水号,a.fcrimecode 编号,b.fcriminal 姓名,a.gname 货名,a.gtxm 条码,a.spshortcode 店内码,c.senddate 回款日期,c.bankflag 回款状态,a.gdj 单价 ");
                        strSql.Append(",convert(numeric(18," + strPointNum + @"),abs(sum(a.qty * b.fifoflag)))  数量,abs(sum(a.amount * b.fifoflag))  金额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 1);

                        strSql.Append(" group by a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,c.bankflag,a.gdj ");
                        //strSql.Append(" order by c.bankflag,a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,a.gdj ");

                        //增加显示银行卡号

                        string newstrSql = "select x.*,n.sName as 供货商,y.BankAccNo 银行卡号 from (" + strSql.ToString() + ") x left outer join t_criminal_Card y on x.编号=y.FCrimeCode left outer join t_goods m on x.条码=m.gtxm left outer join t_supplyer n on m.GSupplyer=n.SCode";


                        DataTable dt = new CommTableInfoBLL().GetDataTable(newstrSql);
                        string strFileName = new CommonClass().GB2312ToUTF8("DetailGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品销售明细清单", 10, strFileName);
                        return Content("OK|DetailGoodInfo.xls");
                    }
                case 6:
                    {
                        if(string.IsNullOrEmpty( GoodsType))
                        {
                            return Content("Error|您选择的商品类型不能为空");
                        }
                        DataTable dtGoodNames= new CommTableInfoBLL().GetDataTable("select GName from t_goods where GType='"+ GoodsType +"'");
                        strSql.Append(@"select b.fareaName as 队别 ");
                        foreach (DataRow row in dtGoodNames.Rows)
                        {
                            strSql.Append(@",SUM(CASE WHEN a.GName = N'" + row["GName"].ToString() + "' THEN a.qty ELSE 0 END) AS '" + row["GName"].ToString() + "' ");
                        }

                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by b.fareaName");

                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("rowAreaGoodGroupBy.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "横向队别商品报表", 10, strFileName);
                        return Content("OK|rowAreaGoodGroupBy.xls");
                    }
                case 7:
                    {
                        strSql.Append("select b.FAreaName 队别,isnull(RoomNo,'')  房号,a.SPShortCode 简码,a.gname 品名");
                        strSql.Append(" ,isnull(a.Remark,'') 规格,a.gtxm 条码,abs(sum(a.qty * b.fifoflag)) 数量");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 金额 ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by b.FAreaName,isnull(RoomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm ");
                        strSql.Append(" order by b.FAreaName,isnull(RoomNo,''),a.SPShortCode ");

                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8("RoomGoodInfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "商品订货信息", 7, strFileName);
                        return Content("OK|RoomGoodInfo.xls");
                    }
                default:
                    {
                        return Content("Err|你传入无效的参数");
                    }
            }

        }

        /// <summary>
        /// 生成Excel用户签字确认单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExcelAllXiaofeiDan(int id)
        {
            string GoodsType = Request["GoodsType"];
            string GoodName = Request["GoodName"];
            string GoodGTXM = Request["GoodGTXM"];
            string SpShortCode = Request["SpShortCode"];

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            string Flag = Request["Flag"];
            if (string.IsNullOrWhiteSpace("Flag"))
            {
                Flag = "1";
            }
            string strWhere = GetInvoicesSearchWhere(Session["loginUserCode"].ToString());
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select b.Invoiceno as 单号,b.FCriminal as 姓名,b.FCrimeCode as 编号,b.FAreaName 队别,b.Ptype as 消费类型,b.Amount 金额,b.OrderDate 日期,'' as 签名");
            //获取商品相关信息的子条件
            GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
            strSql.Append(" group by b.Invoiceno,b.FCriminal,b.FCrimeCode,b.FAreaName,b.Ptype,b.Amount,b.OrderDate");


            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8("InvocieListInfo.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, dt.Rows[0][0].ToString() + "订单用户签字确认表", 5, strFileName);
            return Content("OK|InvocieListInfo.xls");

        }
        //销售管理参数设定
        public ActionResult SaleManagerParameterSet()
        {
            List<T_SHO_ManagerSet> mgrsets = new T_SHO_ManagerSetBLL().GetModelList("KeyName not like '%.%'");
            ViewData["mgrsets"] = mgrsets;
            return View();
        }
    }

    public class PrintInvoices
    {
        public T_Invoice invoice { get; set; }
        public List<T_InvoiceDTL> details { get; set; }

        public T_Criminal criminal { get; set; }
    }
}