using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    public class JfConsumePrintController : Controller
    {

        JifenMgrService _jifenMgrService = new JifenMgrService();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        // GET: JfConsumePrint

        public ActionResult Index(int id = 1)
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
            PageResult<T_JF_Invoice> _pageResult = _jifenMgrService.GetPageList<T_JF_Invoice>("Id asc","",1,10," Flag=1 and OrderDate>='" + startTime + "' and OrderDate<'" + endTime + "'");
            var invoices = _pageResult.rows;
            ViewData["invoices"] = invoices;

            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in ( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            //打印消费记录单时，需要存取两类信息
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType like '%积分%'");
            ViewData["saleTypes"] = saleTypes;

            List<T_GoodsType> goodsTypes = _jifenMgrService.GetModelList<T_GoodsType>( jss.Serialize( new { UseType=1}), "Id asc", 200);
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

            PageResult<T_JF_Invoice> _pageResult = _jifenMgrService.GetPageList<T_JF_Invoice>("Id asc", "", page, row, strWhere);

            sss = "{\"total\":" + _pageResult.total.ToString() + ",\"rows\":" + jss.Serialize(_pageResult.rows) + "}";
            return Content(sss);
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


                    }
                    break;
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

        //打印小票单A4纸格式
        public ActionResult PrintXiaofeiDan()
        {
            string strInvoices = Request["invoices"];
            string printType = Request["printType"];

            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            List<PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>> rtnInvs = new List<PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>>();
            T_JF_Invoice inv;
            #region 获取消费小票单信息
            if (invoices.Length > 0)
            {
                for (int i = 0; i < invoices.Length; i++)
                {
                    PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL> rtnInv = new PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>();
                    inv =_jifenMgrService.QueryModel<T_JF_Invoice>("InvoiceNo",invoices[i]);
                    if (inv != null)
                    {
                        rtnInv.invoice = inv;
                        T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");

                        rtnInv.details = _jifenMgrService.GetInvoiceDtlList("InvoiceNo='" + invoices[i] + "'");

                        rtnInv.criminal = _jifenMgrService.GetCriminalXE_info(rtnInv.invoice.FCrimeCode, 1);

                        //账户清单中统计余额
                        rtnInv.invoice.FrealAreaName = $"消费积分:{inv.Amount}(剩余积分:{inv.ServAmount})";


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
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by b.FAreaName,a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by b.FAreaName,a.SPShortCode ");

                    }
                    break;
                case 1:
                    {
                        strSql.Append("select b.FAreaName FAreaName,isnull(RoomNo,'')  RoomNo,a.SPShortCode SPShortCode,a.gname GName");
                        strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty * b.fifoflag)) FCount");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append("group by b.FAreaName,isnull(RoomNo,''),a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by b.FAreaName,a.SPShortCode,isnull(RoomNo,'') ");
                    }
                    break;
                case 2:
                    {
                        strSql.Append("select '' FAreaName,a.SPShortCode SPShortCode,a.gname GName");
                        strSql.Append(" ,isnull(a.Remark,'') Remark,a.gtxm GTXM,abs(sum(a.qty * b.fifoflag)) FCount");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                        strSql.Append(" order by a.SPShortCode,a.gname,isnull(a.Remark,''),a.gtxm");
                    }
                    break;
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

                    }
                    break;
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
                    }
                    break;
                case 5:
                    {
                        strSql.Append("select a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,c.bankflag,a.gdj ");
                        strSql.Append(",abs(sum(a.qty * b.fifoflag)) FCount,abs(sum(a.amount * b.fifoflag)) FMoney ");
                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag, 1);

                        strSql.Append(" group by a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,c.bankflag,a.gdj ");
                        strSql.Append(" order by c.bankflag,a.invoiceno,a.fcrimecode,b.fcriminal,a.gname,a.gtxm,a.spshortcode,c.senddate,a.gdj ");
                    }
                    break;
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
                        T_GoodsType gt = _jifenMgrService.GetModelFirst<T_GoodsType>(jss.Serialize(new { Fcode = GoodsType }));

                        //获取商品相关信息的子条件
                        GetGoodSubWhere(GoodsType, GoodName, GoodGTXM, SpShortCode, strWhere, strSql, startTime, endTime, Flag);
                        strSql.Append(" group by b.fareaCode,b.fareaName");
                        strSql.Append(" order by b.fareaCode");

                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());

                        DataRow rowGdj = dt.NewRow();
                        rowGdj[0] = "";
                        rowGdj[1] = "单价元/份";
                        for (int i = 0; i < dtGoodNames.Rows.Count; i++)
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

                        dt.ImportRow(dtCount.Rows[0]);

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


        private static void GetGoodSubWhere(string GoodsType, string GoodName, string GoodGTXM, string SpShortCode, string strWhere, StringBuilder strSql, string startDate, string endDate, string Flag, int invListFlag = 0)
        {
            strSql.Append(" from t_JF_invoicedtl a inner join (select * from t_JF_invoice where " + strWhere + ") b  ");
            strSql.Append(" on a.invoiceno=b.invoiceno ");
            if (invListFlag == 1)
            {
                strSql.Append(" left join (select distinct origid,senddate,bankflag from t_JF_vcrd where isnull(origid,'')<>'' ");
                //strSql.Append(" and typeflag in (select typeflagid from t_sho_saletype) ");
                strSql.Append(" and crtDate between '" + startDate + "' and '" + endDate + "' ");
                strSql.Append(" ) c ");
                strSql.Append(" on a.invoiceno=c.origid");
            }

            strSql.Append(" left join t_goods d ");
            strSql.Append(" on a.gtxm=d.gtxm ");
            strSql.Append(" left outer join t_goodstype e ");
            strSql.Append(" on d.gtype=e.fcode ");
            strSql.Append(" where b.flag='" + Flag + "' ");

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
                strWhere = strWhere + " Flag='" + Flag + "'";
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
                            strFareaCode = strFareaCode + ",'" + area.FCode + "'";
                        }
                    }

                    strWhere = strWhere + @" and fareaCode in(" + strFareaCode + ")  ";

                }
                //2019-04-15由于运行太慢会超时改为等于
                //strWhere = strWhere + @" and fareaCode in(" + strFareaCode + ")  ";
            }

            return strWhere;

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
                for (int i = 2; i < argDataTable.Columns.Count; i++)
                {
                    if (Convert.ToDecimal(row[i]) == Convert.ToDecimal(Convert.ToInt32(row[i])))
                    {
                        rowNew[i] = Convert.ToInt32(row[i]).ToString();
                    }
                    else
                    {
                        rowNew[i] = (row[i]).ToString();
                    }


                }

                dtResult.Rows.Add(rowNew);
            }
            return dtResult;
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


                        strSql.Append("select b.FAreaName 队别,a.SPShortCode 货号,a.gname 品名");
                        strSql.Append(" ,isnull(a.Remark,'') 规格,a.gtxm 条码,d.GUnit 单位,a.gdj 单价,convert(numeric(18," + strPointNum + @"),abs(isnull(sum(a.qty * b.fifoflag),0))) 数量");
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 积分 ");
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
                            ,convert(numeric(18,2),sum([1号房]+[2号房]+[3号房]+[4号房]+[5号房]+[6号房]+[7号房]+[8号房]+[9号房]+[10号房]+[11号房]+[12号房]+[13号房]+[14号房]+[15号房]+[16号房]+[未知号房])*[单价]) as 积分
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
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 积分 ");
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
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 积分 ");
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
                        strSql.Append(",x.FCount 消费数量,x.FMoney 消费积分");
                        strSql.Append(",isnull(y.FCount,0) 退货数量,isnull(y.FMoney,0) 退货积分 ");
                        strSql.Append(",x.FMoney-isnull(y.FMoney,0) 总积分 ");
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
                        if (string.IsNullOrEmpty(GoodsType))
                        {
                            return Content("Error|您选择的商品类型不能为空");
                        }
                        DataTable dtGoodNames = new CommTableInfoBLL().GetDataTable("select GName from t_goods where GType='" + GoodsType + "'");
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
                        strSql.Append(" ,abs(sum(a.amount * b.fifoflag)) 积分 ");
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
            if (string.IsNullOrWhiteSpace(Flag))
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

        public ActionResult GetInvoices()
        {
            string strInvoices = Request["invoices"];
            char ee = (char)124;
            string[] invoices = strInvoices.Split(ee);
            List<PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>> rtnInvs = new List<PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>>();
            if (invoices.Length > 0)
            {
                for (int i = 0; i < invoices.Length; i++)
                {
                    PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL> rtnInv = new PrintInvoices<T_JF_Invoice, T_JF_InvoiceDTL>();
                    rtnInv.invoice = _jifenMgrService.QueryModel<T_JF_Invoice>("InvoiceNo",invoices[i]);
                    T_SHO_ManagerSet mgr = new T_SHO_ManagerSetBLL().GetModel("PrintSumOpion");

                    //小票汇总统计
                    rtnInv.details = _jifenMgrService.GetInvoiceDtlList("InvoiceNo='" + invoices[i] + "'");

                    rtnInv.criminal = _jifenMgrService.GetCriminalXE_info(rtnInv.invoice.FCrimeCode, 1);

                    rtnInv.criminal.AccPoints = rtnInv.invoice.ServAmount;
                    
                    rtnInvs.Add(rtnInv);
                }
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
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
            List<T_JF_InvoiceDTL> dtls = _jifenMgrService.QueryList<T_JF_InvoiceDTL>("InvoiceNo='" + invoiceNo + "'");
            return Content("OK|" + jss.Serialize(dtls));
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
            List<T_JF_Vcrd> vcrds = _jifenMgrService.QueryList<T_JF_Vcrd>("flag=0 and isnull(bankflag,0)>=1 and Origid in(" + strInvoices + ")");
            if (vcrds.Count > 0)
            {
                return Content("Err|您选的消费单号已经有发送到银行的记录，不能撤消");
            }

            //是否已经到第二个月，并且大于30天了
            List<T_JF_Invoice> dayInvs = _jifenMgrService.QueryList<T_JF_Invoice>("Invoiceno in(" + strInvoices + ") and DATEADD(day,30, OrderDate)<getdate()");
            if (dayInvs.Count > 0)
            {
                return Content("Err|您选的消费单超过30天不能撤单，不能撤消");
            }

            ////已经配货不能撤
            //List<T_Invoice_outdtl> outdtls = new T_Invoice_outdtlBLL().GetModelList("Invoiceno in(" + strInvoices + ")");
            //if (outdtls.Count > 0)
            //{
            //    return Content("Err|您选的消费单号已经有配货的记录，不能撤消");
            //}


            //离监人员不能撤单
            List<T_JF_Invoice> invs = _jifenMgrService.QueryList<T_JF_Invoice>("Invoiceno in(" + strInvoices + ") and fcrimecode in(select fcode from t_criminal where isnull(fflag,0)=1)");
            if (invs.Count > 0)
            {
                return Content($"Err|您选的消费单有{invs[0].FCriminal}等{invs.Count}条已离监，不能撤消");
            }




            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            //开始撤单

            if (_jifenMgrService.CancelInvoiceOrder(strInvoices, strLoginName))
            {
                return Content("OK.撤单成功！");
            }
            else
            {
                return Content("Err.对不起，撤单失败！");
            }
        }

        /// <summary>
        /// 整单批量退货
        /// </summary>
        /// <param name="Invoices"></param>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult SalesOrderAllReturn(string Invoices)
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
            //List<T_JF_Vcrd> vcrds = _jifenMgrService.QueryList<T_JF_Vcrd>("flag=0 and isnull(bankflag,0)>=1 and Origid in(" + strInvoices + ")");
            //if (vcrds.Count > 0)
            //{
            //    return Content("Err|您选的消费单号已经有发送到银行的记录，不能撤消");
            //}

            //是否已经到第二个月，并且大于30天了
            List<T_JF_Invoice> dayInvs = _jifenMgrService.QueryList<T_JF_Invoice>("Invoiceno in(" + strInvoices + ") and DATEADD(day,30, OrderDate)<getdate()");
            if (dayInvs.Count > 0)
            {
                return Content("Err|您选的消费单超过30天不能撤单，不能撤消");
            }

            ////已经配货不能撤
            //List<T_Invoice_outdtl> outdtls = new T_Invoice_outdtlBLL().GetModelList("Invoiceno in(" + strInvoices + ")");
            //if (outdtls.Count > 0)
            //{
            //    return Content("Err|您选的消费单号已经有配货的记录，不能撤消");
            //}


            //离监人员不能撤单
            List<T_JF_Invoice> invs = _jifenMgrService.QueryList<T_JF_Invoice>("Invoiceno in(" + strInvoices + ") and fcrimecode in(select fcode from t_criminal where isnull(fflag,0)=1)");
            if (invs.Count > 0)
            {
                return Content($"Err|您选的消费单有{invs[0].FCriminal}等{invs.Count}条已离监，不能撤消");
            }

            //判断是否已经退单过
            List<T_JF_Invoice> rtns = _jifenMgrService.QueryList<T_JF_Invoice>("Invoiceno in(" + strInvoices + ") and remark like '%退货%'");
            if (rtns.Count > 0)
            {
                return Content($"Err|您选的消费单有{rtns[0].FCriminal}等{rtns.Count}条已退货，不能重复退货");
            }



            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            //开始撤单

            if (_jifenMgrService.ReturnInvoiceOrder(strInvoices, strLoginName))
            {
                return Content("OK.退货成功！");
            }
            else
            {
                return Content("Err.对不起，退货失败！");
            }
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
                    List<T_JF_InvoiceDTL> models = SetTuihuoDetail(ja, "Add");
                    //判断退货数量 对应的消费主单：INV210400007120
                    List<T_JF_InvoiceDTL> dtls = _jifenMgrService.QueryList<T_JF_InvoiceDTL>("InvoiceNo='" + models[0].INVOICENO + "'");
                    string sql = "select gtxm,sum(qty) as qty from t_JF_invoiceDtl where Remark like '%" + models[0].INVOICENO + "%' group by GTXM";
                    var _rs = new CommTableInfoBLL().GetList<T_JF_InvoiceDTL>(sql, null);

                    var _ms = models.GroupBy(o => o.GTXM).Select(g => new { key = g.Key, value = g.Sum(p=>p.QTY) }).ToList();

                    var _ds = dtls.GroupBy(o => o.GTXM).Select(g => new { key = g.Key, value = g.Sum(p => p.QTY) }).ToList();

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
                                if (_r.QTY + item.value > orderCount)
                                {
                                    return Content($"Err|有历史退货，条码：{item.key}超过原购物数量{orderCount}，不能退货");
                                }
                            }
                        }
                    }
                    string resultInfo = _jifenMgrService.AddTuiHuoOrder(models, crtby, ipLastCode);

                    return Content(resultInfo);
                }
            }
            return Content("Err|eer");
        }

        //设定退货明细
        private static List<T_JF_InvoiceDTL> SetTuihuoDetail(JArray ja, string action)
        {
            List<T_JF_InvoiceDTL> models = new List<T_JF_InvoiceDTL>();
            T_JF_InvoiceDTL model = new T_JF_InvoiceDTL();
            foreach (JObject o in ja)
            {
                model = new T_JF_InvoiceDTL();
                model.FCrimecode = o["FCrimecode"].ToString();
                model.SPShortCode = o["SPShortCode"].ToString();
                model.GTXM = o["GTXM"].ToString();
                model.GCODE = o["GCODE"].ToString();
                model.GDJ = Convert.ToDecimal(o["GDJ"].ToString());
                model.GNAME = o["GNAME"].ToString();
                model.AMOUNT = Convert.ToDecimal(o["AMOUNT"].ToString());
                model.QTY = Convert.ToDecimal(o["QTY"].ToString());
                model.INVOICENO = o["INVOICENO"].ToString();
                DataTable dt = new CommTableInfoBLL().GetDataTable(string.Format("select top 1 ftzsp_typeflag from t_JF_invoiceDtl where invoiceno='{0}' and gtxm='{1}'", o["INVOICENO"].ToString(), o["GTXM"].ToString()));
                model.FTZSP_TypeFlag = Convert.ToInt16(dt.Rows[0][0].ToString());
                models.Add(model);
            }

            return models;
        }




    }
}