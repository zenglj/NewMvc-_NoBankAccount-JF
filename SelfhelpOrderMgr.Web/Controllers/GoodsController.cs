using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class GoodsController : BaseMenuController
    {
        //
        // GET: /Goods/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index()
        {
            List<T_GoodsType> types = (List<T_GoodsType>)new T_GoodsTypeBLL().GetListOfIEnumerable("");
            ViewData["types"] = types;
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y'");
            ViewData["goods"] = goods;
            return View();
        }

        public ActionResult Lists()
        {
            List<T_GoodsType> types = (List<T_GoodsType>)new T_GoodsTypeBLL().GetListOfIEnumerable("");
            ViewData["types"] = types;
            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y'");
            ViewData["goods"] = goods;
            return View();
        }

        public ActionResult GetGoodsInfo()
        {
            string data = Request["data"];
            string ID = Request["ID"].ToString();
            string lastName = Request["lastName"];
            string PTypeName = Request["PTypeName"];
            List<T_GoodsType> types = (List<T_GoodsType>)new T_GoodsTypeBLL().GetListOfIEnumerable("FName='" + PTypeName + "'");

            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetListOfIEnumerable("Active='Y' and GType='" + types[0].Fcode + "'");
            //ViewData["goods"] = goods;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content("OK|成功|"+jss.Serialize(goods));
        }




        #region 商品调价申请

        //商品调价申请
        public ActionResult PriceChangeIndex(int id = 1)
        {
            List<T_GoodsType> gtypes = new T_GoodsTypeBLL().GetModelList("");
            ViewData["gtypes"] = gtypes;

            List<T_Supplyer> supplyers = new T_SupplyerBLL().GetModelList("");
            ViewData["supplyers"] = supplyers;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from T_Goods_ChangeList Order by CrtBy");
            List<string> crtbys = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    crtbys.Add(row[0].ToString());
                }
            }
            ViewData["crtbys"] = crtbys;

            ViewData["id"] = id;

            return View();
        }

        public ActionResult GetChangeList()
        {
            string strWhere = GetChangeListWhere();


            List<T_Goods_ChangeList> lists;

            //if (string.IsNullOrEmpty(auditFlag))
            //{
            //    lists = new T_Criminal_ChangeListBLL().GetModelList("isnull(AuditFlag,0)<9");
            //}
            //else
            //{
            //    lists = new T_Criminal_ChangeListBLL().GetModelList("isnull(AuditFlag,0)=" + auditFlag);
            //}

            lists = new T_Goods_ChangeListBLL().GetModelList(strWhere);

            return Content(jss.Serialize(lists));
        }

        private string GetChangeListWhere()
        {
            string GCode = Request["GCode"];
            string GName = Request["GName"];
            string changeType = Request["changeType"];
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string GType = Request["GType"];
            string crtby = Request["crtby"];
            string GSupplyer = Request["GSupplyer"];
            string auditFlag = Request["auditFlag"];


            string strWhere = "1=1 ";
            if (string.IsNullOrEmpty(GCode) == false)
            {
                strWhere = strWhere + " and GCode='" + GCode + "'";
            }
            if (string.IsNullOrEmpty(GName) == false)
            {
                strWhere = strWhere + " and GName like '%" + GName + "%'";
            }
            if (string.IsNullOrEmpty(changeType) == false)
            {
                strWhere = strWhere + " and changeType= '" + changeType + "'";
            }
            if (string.IsNullOrEmpty(startTime) == false)
            {
                if (string.IsNullOrEmpty(endTime) == false)
                {
                    strWhere = strWhere + " and crtDate between '" + startTime + "'" + " and '" + endTime + "'";
                }
                else
                {
                    strWhere = strWhere + " and crtDate>='" + startTime + "'";
                }
            }

            if (string.IsNullOrEmpty(GType) == false)
            {
                strWhere = strWhere + " and GTXM in(select GTXM from t_goods where GType='" + GType + "')";
            }

            if (string.IsNullOrEmpty(crtby) == false)
            {
                strWhere = strWhere + " and crtby= '" + crtby + "'";
            }

            if (string.IsNullOrEmpty(GSupplyer) == false)
            {
                strWhere = strWhere + " and GTXM in(select GTXM from t_goods where GSupplyer='" + GSupplyer + "')";
            }

            if (string.IsNullOrEmpty(auditFlag) == false)
            {
                strWhere = strWhere + " and auditFlag= '" + auditFlag + "'";
            }
            return strWhere;
        }

        public ActionResult GetGoodsList()
        {
            string addSchGCode = Request["addSchGCode"];
            string addSchGName = Request["addSchGName"];
            string addSchGType = Request["addSchGType"];
            string addSchActive = Request["addSchActive"];
            string addSchGSupplyer = Request["addSchGSupplyer"];


            string strWhere = "1=1 ";

            //验证用户的队别,如果设定了Vcrd验证用户队别，则要查看是否有相应的队别权限下的犯人才可以查询到
            //T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");
            //if (mset != null)
            //{
            //    if (mset.MgrValue == "1")
            //    {
            //        strWhere = " FAreaCode in (  select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')";
            //    }
            //}
            if (!string.IsNullOrEmpty(addSchGCode))
            {
                //可用商品简或条码
                strWhere = strWhere + " and (SPShortCode='" + addSchGCode + "' or GTXM='"+ addSchGCode +"')";
            }
            if (!string.IsNullOrEmpty(addSchGName))
            {
                strWhere = strWhere + " and Gname like '%" + addSchGName + "%'";
            }
            if (!string.IsNullOrEmpty(addSchGType))
            {
                strWhere = strWhere + " and GType='" + addSchGType + "'";
            }
            if (!string.IsNullOrEmpty(addSchActive))
            {
                strWhere = strWhere + " and ACTIVE='" + addSchActive + "'";
            }
            if (!string.IsNullOrEmpty(addSchGSupplyer))
            {
                strWhere = strWhere + " and GSupplyer='" + addSchGSupplyer + "'";
            }


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

            listRows = new T_GoodsBLL().GetModelList(strWhere.ToString()).Count;

            List<T_Goods> goods = (List<T_Goods>)new T_GoodsBLL().GetPageListOfIEnumerable(page, row, strWhere);


            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(goods) + "}";
            return Content(sss);


        }
        [MyLogActionFilterAttribute]
        public ActionResult Save_RequestChangeList()//保存申请列表
        {
            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            string crtby = Session["loginUserName"].ToString();
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
                    List<T_Goods_ChangeList> models = SetGoodsChangeInfo(ja, crtby);

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
                    List<T_Goods_ChangeList> models = SetGoodsChangeInfo(ja, crtby);
                    return Content("OK|保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_Goods_ChangeList> SetGoodsChangeInfo(JArray ja, string crtby)
        {
            T_Goods_ChangeList model = new T_Goods_ChangeList();
            DateTime dt = DateTime.Now;

            foreach (JObject o in ja)
            {
                model = new T_Goods_ChangeList()
                {
                    GCode = o["GCode"].ToString(),
                    GName = o["GName"].ToString(),
                    ChangeType = o["ChangeType"].ToString(),
                    ChangeTypeName = o["ChangeTypeName"].ToString(),
                    OldPrice = Convert.ToDecimal( o["OldPrice"].ToString()),
                    NewPrice = Convert.ToDecimal(o["NewPrice"].ToString()),
                    ChangeInfo = o["ChangeInfo"].ToString(),
                    GTXM=o["ChangeInfo"].ToString(),
                    CrtBy = crtby,
                    CrtDate = DateTime.Now,
                    Remark = "",
                    AuditArea = "系统",
                    AuditBy = "自动审核",
                    AuditDate = DateTime.Now,
                    AuditFlag = 9,
                    AuditInfo = "变更不用审核"
                };
                if (model.ChangeType == "1")//处遇变更
                {
                    T_SHO_ManagerSet cyMset = new T_SHO_ManagerSetBLL().GetModel("GoodsChangeSetByPrice");
                    if (cyMset != null)
                    {
                        if (cyMset.MgrValue == "1")
                        {
                            model.AuditArea = "";
                            model.AuditBy = "";
                            model.AuditDate = DateTime.Now;
                            model.AuditFlag = 0;
                            model.AuditInfo = "";
                            new T_Goods_ChangeListBLL().Add(model, 1);
                        }
                        else
                        {
                            new T_Goods_ChangeListBLL().Add(model);
                            new CommTableInfoBLL().ExecSql("update t_Goods set GDJ='" + model.NewPrice.ToString() + "' where Gcode='" + model.GCode + "'");
                        }
                    }
                    else
                    {
                        new T_Goods_ChangeListBLL().Add(model);
                        new CommTableInfoBLL().ExecSql("update t_Goods set GDJ='" + model.NewPrice.ToString() + "' where Gcode='" + model.GCode + "'");
                    }

                }
                else if (model.ChangeType == "2")//队别变更
                {
                    
                }
                else
                {
                    new T_Goods_ChangeListBLL().Add(model);
                }

            }
            List<T_Goods_ChangeList> models = new T_Goods_ChangeListBLL().GetModelList("Crtby='" + crtby + "' and crtDate>='" + dt.ToString() + "'");
            return models;
        }

        //保存提交审核
        public ActionResult btnSubmitAduit()
        {
            string seqnos = Request["seqnos"];
            string remark = Request["remark"];
            string auditFlag = Request["auditFlag"];

            string crtby = Session["loginUserName"].ToString();
            string userCode = Session["loginUserCode"].ToString();

            T_CZY czy = new T_CZYBLL().GetModel(userCode);

            ////char ee = (char)124;
            ////string[] invNos = invoiceNos.Split(ee);
            try
            {
                List<T_Goods_ChangeList> models = new T_Goods_ChangeListBLL().GetModelList("Seqno in(" + seqnos + ")");

                foreach (T_Goods_ChangeList m in models)
                {
                    if (m.AuditFlag < 9)
                    {
                        m.AuditDate = DateTime.Now;
                        m.AuditFlag = Convert.ToInt32(auditFlag);
                        m.AuditInfo = remark;
                        m.AuditBy = crtby;
                        m.AuditArea = czy.FUserArea;
                        new T_Goods_ChangeListBLL().Update(m);
                        if (m.AuditFlag == 9)
                        {
                            if (m.ChangeType == "1")
                            {
                                new CommTableInfoBLL().ExecSql("update t_Goods set GDJ='" + m.NewPrice.ToString() + "' where GCode='" + m.GCode + "'");
                            }
                            else if (m.ChangeType == "2")
                            {
                                //new CommTableInfoBLL().ExecSql("update t_Criminal set FAreaCode='" + m.NewCode + "' where fcode='" + m.FCode + "'");
                            }
                        }
                    }
                }

                models = new T_Goods_ChangeListBLL().GetModelList("Seqno in(" + seqnos + ")");
                return Content("OK|" + jss.Serialize(models));
            }
            catch
            {
                return Content("Error|审核失败");
            }

        }

        //Excel导出变更记录
        public ActionResult ExcelChangeList()
        {

            string strWhere = GetChangeListWhere();

            string strSql = @"SELECT [Seqno] 序号
                  ,[GCode] 编号
                  ,[GTXM] 条码
                  ,[GName] 品名
                  ,[ChangeTypeName] 变更类型
                  ,[OldPrice] 变更前单价
                  ,[NewPrice] 变更后单价
                  ,[ChangeInfo] 变更原因
                  ,[CrtBy] 申请人
                  ,[CrtDate] 申请日期
                  ,[AuditBy] 审核人
                  ,[AuditArea] 审核部门
                  ,[AuditDate] 审核日期
                  ,[AuditInfo] 审核意见
                  ,case [AuditFlag] when 9 then '同意' when 10 then '拒绝' else '待审核' end 审核结果
                  ,[Remark] 备注
              FROM [T_Goods_ChangeList] " + " where "+ strWhere;


            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql);
            string strFileName = new CommonClass().GB2312ToUTF8("GoodsChangeList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|GoodsChangeList.xls");
        }

        #endregion

	}
}