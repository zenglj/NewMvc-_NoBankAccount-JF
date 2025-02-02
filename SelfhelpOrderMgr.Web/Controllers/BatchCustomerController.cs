﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class BatchCustomerController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        // GET: /BatchCustomer/
        public ActionResult Index(int id=1)
        {
            //扣款大类型
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("FifoFlag=-1");
            ViewData["saleTypes"] = saleTypes;

            //扣款小类型
            List<T_Savetype> custTypes = new T_SavetypeBLL().GetModelList("typeflag=1");
            ViewData["custTypes"] = custTypes;

            Session["loginUserCode"] = "102";
            return View();
        }

        public ActionResult AddMainOrder()//增加主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            //string strLoginName = "admin";
            string saleTypeNameMain = Request["saleTypeNameMain"];//扣款大类名称
            string saleTypeFlagId = Request["saleTypeFlagId"];//扣款大类Id
            string custTypeNameMain = Request["custTypeNameMain"];//扣款小类名称
            string custTypeCodeMain = Request["custTypeCodeMain"];//扣款小类Id
            string sremark = Request["sremark"];
            string sapplyby = Request["sapplyby"];
            int FMoneyInOutFlag = -1;//为扣款模式
            string strFBid = new T_BONUSBLL().CreateOrderId("PLK");//批量扣款主单号
            //首先创建一个主单
            T_BatchMoneyTrade trade = new T_BatchMoneyTrade();
            trade.Bid = strFBid;
            trade.FCourseCode = Convert.ToInt32(custTypeCodeMain);
            trade.FCourseType = custTypeNameMain;
            trade.FMoneyInOutFlag = FMoneyInOutFlag;
            trade.FrealAreaCode = "";
            trade.FAmount = 0;
            trade.Remark = sremark;
            trade.ApplyBy = sapplyby;
            trade.Applydt = DateTime.Today;
            trade.Crtby = strLoginName;
            trade.crtdt = DateTime.Today;
            trade.CheckBy = "";
            trade.CheckDate = DateTime.Today;
            trade.Flag = 0;
            trade.FAreaCode = "";
            trade.FAreaName = "";
            trade.UDate = DateTime.Today;
            trade.PType = saleTypeNameMain;
            trade.cnt = 0;
            trade.AuditBy = "";
            trade.AuditFlag = 0;
            trade.AuditDate = DateTime.Today;
            trade.FDbCheckBY = "";
            trade.FdbCheckDate = DateTime.Today;
            trade.FdbCheckFlag = 0;
            trade.FPostBy = "";
            trade.FPostDate = DateTime.Today;
            trade.FPoestFlag = 0;
            trade.FrealAreaCode = "";
            trade.FrealAreaName = "";
            try
            { 
                new T_BatchMoneyTradeBLL().Add(trade);
                T_BatchMoneyTrade newModel = new T_BatchMoneyTradeBLL().GetModel(trade.Bid);
                return Content("OK|" + jss.Serialize(newModel));
            }
            catch
            {
                return Content("Error|创建主单失败");
            }
            


        }

        public ActionResult getBonus(int id = 1)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"];
            List<T_BatchMoneyTrade> bonuies;
            StringBuilder strSql = new StringBuilder();
            if (action == "LoginIn")
            {
                switch (id)
                {
                    case 1:
                        {
                            strSql.Append("isnull(flag,0)=0 and CrtBy='" + strLoginName + "'");
                        } break;
                    case 2:
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(FCheckFlag,0)=1 and isnull(AuditFlag,0)=0 ");
                        } break;
                    case 3:
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(auditFlag,0)=1 and isnull(FDbCheckFlag,0)=0 ");
                        } break;
                    case 4:
                        {
                            strSql.Append("isnull(flag,0)=0 and isnull(FDbCheckFlag,0)=1 ");
                        } break;
                    default:
                        {
                            strSql.Append("isnull(flag,0)=0 ");
                        }
                        break;
                }
                bonuies = new T_BatchMoneyTradeBLL().GetModelList(strSql.ToString());
            }
            else if (action == "GetSearchMainOrder")
            {
                string PType = Request["PType"];//消费类别名
                if (PType == "请选消费类型")
                {
                    PType = "";
                }
                string FCourseType = Request["FCourseType"];//扣款科目名
                if (FCourseType == "请选科目名称")
                {
                    FCourseType = "";
                }
                string strStartDate = Request["startDate"];//开始日期
                string strEndDate = Request["endDate"];//结束日期
                string strRemark = Request["fRemark"];//结束日期
                string strFCrimeCode = Request["fCrimeCode"];//编号
                string strFCrimeName = Request["fCrimeName"];//姓名    
                //获取查询条件的SQL
                strSql = GetSearchSql(PType, FCourseType, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName);
                switch (id)
                {
                    case 1:
                        {
                            //strSql.Append(" and FAreaCode in(select fareacode from t_Czy_Area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "') ");
                        } break;
                    case 2:
                        {
                            strSql.Append(" and FCheckFlag=1 ");
                        } break;
                    case 3:
                        {
                            strSql.Append(" and auditFlag=1 ");
                        } break;
                    case 4:
                        {
                            strSql.Append(" and FDbCheckFlag=1 ");
                        } break;
                    default:
                        break;
                }
                List<T_BatchMoneyTrade> list;
                list = (List<T_BatchMoneyTrade>)new T_BatchMoneyTradeBLL().GetModelList(strSql.ToString());
                if (list == null)
                {
                    list = new List<T_BatchMoneyTrade>();
                    T_BatchMoneyTrade m1 = new T_BatchMoneyTrade();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(jss.Serialize(list));
            }
            else
            {
                bonuies = new List<T_BatchMoneyTrade>();
            }

            return Content(jss.Serialize(bonuies));
        }

        private static StringBuilder GetSearchSql(string PType, string FCourseType, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();

            //消费类型
            if (string.IsNullOrEmpty(PType) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" PType ='" + PType + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and PType ='" + PType + "' ");
                }
            }

            //消费科目名称
            if (string.IsNullOrEmpty(FCourseType) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" FCourseType ='" + FCourseType + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and FCourseType ='" + FCourseType + "' ");
                }
            }

            //开始日期
            if (string.IsNullOrEmpty(strStartDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtdt>='" + strStartDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtdt>='" + strStartDate + "' ");
                }
            }
            //结束日期
            if (string.IsNullOrEmpty(strEndDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtdt<'" + strEndDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtdt<'" + strEndDate + "' ");
                }
            }
            //备注
            if (string.IsNullOrEmpty(strRemark) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" Remark like '%" + strRemark + "%' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and Remark like '%" + strRemark + "%' ");
                }
            }

            if (string.IsNullOrEmpty(strFCrimeCode) == false || string.IsNullOrEmpty(strFCrimeName) == false)
            {
                int inWhereFlag = 0;
                if (whereFlag == 0)
                {
                    strSql.Append(@"  bid in (
                                select bid from t_Bonusdtl ");
                }
                else
                {
                    strSql.Append(@"and bid in (
                                select bid from t_Bonusdtl ");
                }
                if (string.IsNullOrEmpty(strFCrimeCode) == false)
                {
                    if (inWhereFlag == 0)
                    {
                        strSql.Append(@" where FCrimeCode='" + strFCrimeCode + "'");
                        inWhereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and FCrimeCode='" + strFCrimeCode + "'");
                    }
                }

                if (string.IsNullOrEmpty(strFCrimeName) == false)
                {
                    if (inWhereFlag == 0)
                    {
                        strSql.Append(@" where fcriminal like '%" + strFCrimeName + "%'");
                        inWhereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(@" and fcriminal like '%" + strFCrimeName + "%'");
                    }
                }
                strSql.Append(@" )");

            }
            return strSql;
        }

        public ActionResult ExcelOut(int id = 1)//Excel导出成功记录
        {

            string strFBid = Request["FBidExcel"];
            if (id == 6) //莆田劳动报酬导出格式      
            {
                //                DataTable dt = new CommTableInfoBLL().GetDataTable(@"select ROW_NUMBER() OVER (ORDER BY a.seqno) AS 序号,a.fcrimecode ID,a.fcriminal 姓名,b.bankaccno 银行卡号,a.famount 录卡 from t_bonusdtl a,t_criminal_card b where bid='"+ strFBid +@"' and a.fcrimecode=b.fcrimecode
                //                    order by a.seqno");
                DataTable dtMain = new CommTableInfoBLL().GetDataTable(@"select  * from t_bonus b where bid='" + strFBid + @"' ");
                if (dtMain.Rows[0]["FCheckFlag"].ToString() != "1")
                {
                    return Content("Err|主单未提交确认不能导出");
                }
                DataTable dt = new CommTableInfoBLL().GetDataTable(@"select  ROW_NUMBER() OVER (ORDER BY ID) AS 序号,* from 
                    (SELECT a.FCRIMECODE AS ID, a.fcriminal AS 姓名, b.BankAccNo AS 银行卡号, a.FAMOUNT AS 录卡, a.BID
                    FROM dbo.T_BatchMoneyTrade_DTL AS a INNER JOIN
                      dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode) b where bid='" + strFBid + @"' ");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_PKList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, "劳动报酬录入金额", 4, strFileName);
            }
            else
            {
                //DataTable dt = new T_BONUSBLL().GetDtlDataTableByBid(strFBid);
                DataTable dt = new CommTableInfoBLL().GetDataTable(@"select  ROW_NUMBER() OVER (ORDER BY ID) AS 序号,* from 
                (SELECT a.FCRIMECODE AS ID, a.fcriminal AS 姓名, b.BankAccNo AS 银行卡号, a.FAMOUNT AS 录卡, a.BID
                FROM dbo.T_BatchMoneyTrade_DTL AS a INNER JOIN
                      dbo.T_Criminal_card AS b ON a.FCRIMECODE = b.fcrimecode) b where bid='" + strFBid + @"' ");
                string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_PKList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, strFileName);
            }


            return Content("OK|" + strFBid + "_PKList.xls");
        }

        public ActionResult ErrorListOutport()//Excel导出成功记录
        {
            string strFBid = Request["excelBid"];
            DataTable dt = new T_BatchMoneyTradeBLL().GetErrListDataTable(strFBid);
            string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_PKErrList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|" + strFBid + "_PKErrList.xls");
        }

        public ActionResult ExcelInport()//Excel表格导入
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (Request.Files.Count > 0)
            {
                string strFBid = Request["excelBid"];
                HttpPostedFileBase f = Request.Files[0];
                string fname = f.FileName;
                /* startIndex */
                int index = fname.LastIndexOf("\\") + 1;
                /* length */
                int len = fname.Length - index;
                fname = fname.Substring(index, len);
                /* save to server */
                string savePath = Server.MapPath("~/Upload/" + fname);
                f.SaveAs(savePath);
                //context.Response.Write("Success!");
                DateTime sdt;
                DateTime edt;
                TimeSpan tspan;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;
                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);
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
                    int ErrNums = 0;
                    if (rows < 1)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        #region 定义DataTable
                        DataTable dtUserAdd = new DataTable();
                        dtUserAdd.Columns.Add(new DataColumn("BID", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FCrimeCode", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FCriminal", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FMoney", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("FRemark", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("Notes", typeof(string)));
                        DataRow drTemp = null;
                        #endregion

                        for (int i = 1; i <= rows; i++)
                        {
                            NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                            //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                            NPOI.SS.UserModel.CellType iType = 0;
                            try
                            {
                                iType = row.GetCell(0).CellType;
                            }
                            catch
                            {
                                break;
                            }
                            string FCode = "";
                            if (iType == 0)
                            {
                                FCode = Convert.ToString(row.GetCell(0).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                            }
                            else
                            {
                                FCode = row.GetCell(0).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                            }
                            string FName = row.GetCell(1).StringCellValue;//编号 列名 以下类似
                            string FMoney = "0";  //金额
                            try
                            {
                                FMoney = Convert.ToString(row.GetCell(2).NumericCellValue);
                            }
                            catch { }

                            string FRemark = "";  //开始日期
                            try
                            {
                                FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                            }
                            catch { }


                            string okInfo;
                            int strFlag;
                            //检测并增加一条记录（劳动报酬）
                            string strDoResult = CheckAndAddRecord(strLoginName, Session["loginUserCode"].ToString(), FCode, FName, FMoney, strFBid, out okInfo, out strFlag);

                            if (strDoResult != "")
                            {
                                T_BatchMoneyTrade_ErrList importList = new T_BatchMoneyTrade_ErrList();
                                importList.ImportType = 4;
                                importList.fcrimecode = FCode;
                                importList.fname = FName;
                                importList.Amount = Convert.ToDecimal(FMoney);
                                importList.Crtdt = DateTime.Now;
                                importList.CrtBy = strLoginName;
                                importList.Remark =FRemark+":"+ strDoResult;
                                importList.pc = strFBid;
                                importList.notes = "";
                                //插入失败记录
                                new T_BatchMoneyTrade_ErrListBLL().Add(importList);
                                ErrNums++;
                            }
                            else
                            {
                                #region Excel行写入到DataTabel中
                                decimal rstMoney = 0;
                                try
                                {//如果金额有
                                    rstMoney = Convert.ToDecimal(FMoney);
                                    if (FCode != "")
                                    {
                                        drTemp = dtUserAdd.NewRow();
                                        drTemp["BID"] = strFBid;
                                        drTemp["FCrimeCode"] = FCode;
                                        drTemp["FCriminal"] = FName;
                                        drTemp["FMoney"] = FMoney;
                                        drTemp["FRemark"] = FRemark;
                                        drTemp["Notes"] = "";
                                        dtUserAdd.Rows.Add(drTemp);
                                    }

                                }
                                catch
                                { }
                                #endregion
                            }
                            
                        }


                        #region 写入到数据库中
                        //将DataTabe dtUserAdd 写入到数据库中
                        new CommTableInfoBLL().ExecSql("Delete from t_Bonus_Temp where Bid='" + strFBid + "'");
                        string strAddResult = new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "dbo.[T_Bonus_Temp]");
                        if (strAddResult == "1")
                        {
                            if (new T_BatchMoneyTradeBLL().PLWriteTradeDtl(strFBid, strLoginName))
                            {
                                rtnTradeExcel rtn = new rtnTradeExcel();
                                rtn.trade = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                                rtn.dtls = new T_BatchMoneyTradeBLL().GetDtlPageList(1, 10, "Bid='" + strFBid + "'", "seqno");
                                //T_BONUS tbouns = new T_BONUSBLL().GetModel(strFBid);
                                List<T_BatchMoneyTrade_ErrList> imports = new T_BatchMoneyTrade_ErrListBLL().GetModelList("pc='" + strFBid + "'");
                                edt = DateTime.Now;
                                tspan = edt - sdt;
                                if (imports.Count > 0)
                                {
                                    return Content("OK|导入完成,失败记录" + imports.Count.ToString() + "条,用时" + tspan.TotalSeconds.ToString() + "秒|" + jss.Serialize(rtn));
                                }
                                else
                                {
                                    return Content("OK|导入完成,用时" + tspan.TotalSeconds.ToString() + "秒|" + jss.Serialize(rtn));
                                }
                            }
                            else
                            {
                                return Content("Err|" + strAddResult);
                            }
                        }
                        else
                        {
                            return Content("Err|写入银行Excel文件时失败");
                        }
                        #endregion

                        //rtnLaobaoExcel rtn = new rtnLaobaoExcel();
                        //rtn.bonus = new T_BONUSBLL().GetModel(strFBid);
                        //rtn.dtls = new T_BONUSDTLBLL().GetModelList("Bid='" + strFBid + "'");

                        //if (ErrNums > 0)
                        //{
                        //    return Content("OK|导入完成，失败记录：" + ErrNums.ToString() + "条|" + jss.Serialize(rtn));
                        //}
                        //else
                        //{
                        //    return Content("OK|导入完成|" + jss.Serialize(rtn));
                        //}
                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        //获取明细记录表
        public ActionResult getLaobaoDetailByBid()
        {
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
            string FBid = Request["FBid"];

            listRows = new T_BatchMoneyTradeBLL().GetDtlListCount("BID='" + FBid + "'")[0];


            List<T_BatchMoneyTrade_DTL> bonusDtls = new T_BatchMoneyTradeBLL().GetDtlPageList(page, row, "BID='" + FBid + "'", "seqno");
            //return Content(jss.Serialize(bonusDtls));
            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(bonusDtls) + "}";
            return Content(sss);
        }

        //删除明细记录
        public ActionResult DelOrderDetail()
        {
            string strFBid = Request["sbid"];//主单编号
            string strSeqno = Request["seqno"];//流水号
            T_BatchMoneyTrade bonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
            if (bonus.FCheckFlag == 1)
            {
                return Content("Error.主单已经确认明细不能删除");
            }
            if (new T_BatchMoneyTrade_DTLBLL().Delete(Convert.ToInt32(strSeqno)))
            {
                new T_BatchMoneyTradeBLL().UpdateByCountMoney(strFBid);
                T_BatchMoneyTrade model = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                return Content("OK." + model.Bid + "|" + model.cnt + "|" + model.FAmount);
            }
            else
            {
                return Content("Error.删除失败");
            }
        }

        public ActionResult QueryCrimeCode()//查询账号
        {
            #region 查询犯人编号信息
            string strFCrimeCode = Request["FCode"];//编号
            T_Criminal model = new T_CriminalBLL().GetModel(strFCrimeCode);
            if (model != null)
            {
                T_AREA area = new T_AREABLL().GetModel(model.FAreaCode);
                List<T_Criminal> list = new T_CriminalBLL().GetCrimeBySearch(strFCrimeCode, "", area.FName, Session["loginUserCode"].ToString());
                if (list == null)
                {
                    return Content("OK." + model.FCode + "|" + model.FName + "|" + model.fflag + "|" + "-1" + "|" + area.FName + "的犯人，您无权管理");
                }
                else
                {
                    T_CY_TYPE cyType = new T_CY_TYPEBLL().GetModel(model.FCYCode);
                    return Content("OK." + model.FCode + "|" + model.FName + "|" + model.fflag + "|" + "1" + "|" + cyType.cpct.ToString());
                }
            }
            else
            {
                return Content("Error.编号不存在");
            }
            #endregion
        }

        public ActionResult AddOrderDetail()//增加明细记录
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strFCode = Request["FCode"];//编号
            string strFName = Request["FName"];//姓名
            string strFMoney = Request["FMoney"];//金额
            string strFBid = Request["FBid"];//主单号
            Decimal myMoney = 0;
            try
            {
                myMoney = Convert.ToDecimal(strFMoney);
            }
            catch
            {
                return Content("Error.金额必须是数值的");
            }
            string okInfo;
            int strFlag;

            
            //检测并增加一条记录（劳动报酬）
            string strResult = CheckAndAddRecord(strLoginName, Session["loginUserCode"].ToString(), strFCode, strFName, strFMoney, strFBid, out okInfo, out strFlag);
            if (strResult == "")
            {
                return Content("OK." + okInfo);
            }
            else
            {
                return Content("Error." + strResult);
            }
        }

        private static string CheckAndAddRecord(string LoginUserName, string LoginUserCode, string strFCode, string strFName, string strFMoney, string strFBid, out string okInfo, out int strFlag)
        {
            string strRusult = "";//返回处理结果信息
            decimal cpctMoney = 0;//留存金额
            okInfo = "";//成功的信息



            T_BatchMoneyTrade trade = new T_BatchMoneyTradeBLL().GetModel(strFBid);

            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + trade.PType + "'");
            if (saleTypes.Count <= 0)
            {
                strFlag = 6;//扣款消费大类不存在
                return "Error.您选择的扣款消费大类不存在";
            }

            T_Criminal user = new T_CriminalBLL().GetCriminalXE_info(strFCode, saleTypes[0].TypeFlagId);
            if (user.ErrInfo != "")
            {
                strFlag = 7;//可消费金额不足                
                return "Error." + user.ErrInfo + "，请与管理人员联系";
            }
            if (user.NoXiaofeimoney < Convert.ToDecimal(strFMoney))
            {
                strFlag = 7;//可消费金额不足
                return "Error.用户当前可消费金额不足";
            }


            T_BatchMoneyTrade_DTL model = new T_BatchMoneyTrade_DTL();

            //犯人信息不正确或是无权管理
            //List<T_CrimeList> criminals = (List<T_CrimeList>)new T_CriminalBLL().GetCrimeBySearch(dbname, strFCode, strFName, "", LoginUserCode);
            List<T_Criminal> criminals = new T_CriminalBLL().GetModelList("fcode='" + strFCode + "' and fareacode in(select fareacode from t_czy_area where fcode='" + LoginUserCode + "' and fflag=2)");
            strFlag = 0;
            if (criminals.Count <= 0)
            {
                strFlag = 1;//犯人信息不正确或是无权管理
                strRusult = "编号不存在或是无权管理";
            }
            else
            {
                T_Criminal criminal = criminals[0];
                if (criminal.fflag == 1)
                {
                    strFlag = 2;//犯人已经离监结算了
                    strRusult = "犯人已经离监结算了";
                }
                else if (criminal.FName != strFName)
                {
                    strFlag = 6;//姓名与编号不对一致
                    strRusult = "姓名出错：" + strFCode + "对应的姓名是" + criminal.FName + ",而不是" + strFName;
                }
                else
                {
                    T_BatchMoneyTrade bonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                    if (bonus == null)
                    {
                        strFlag = 3;//主单号不存在
                        strRusult = "主单号不存在";
                    }
                    else
                    {
                        //获取处遇信息
                        T_CY_TYPE cy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                        cpctMoney = Convert.ToDecimal(strFMoney) * (decimal)cy.cpct / 100;

                        //获取队别信息
                        T_AREA area = new T_AREABLL().GetModel(criminal.FAreaCode);

                        //获取卡号信息
                        T_Criminal_card card = new T_Criminal_cardBLL().GetModel(strFCode);
                        if (card == null)
                        {
                            strFlag = 5;//未办理IC卡
                            strRusult = "该犯人未办理IC卡";
                            return strRusult;
                        }
                        T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LyjBankCardCheckFlag");
                        if (mset != null)
                        {
                            if (mset.MgrValue == "1")
                            {
                                if (card.RegFlag != 1)
                                {
                                    strFlag = 5;//未办理IC卡
                                    return "该犯未办理银行卡签约，不能入账";
                                }
                            }
                        }

                        
                        //设定Model信息
                        SetBonusDetailModel(LoginUserName, strFCode, strFMoney, strFBid, cpctMoney, model, criminal, bonus, area, card);

                        if (new T_BatchMoneyTrade_DTLBLL().Add(model) > 0)
                        {
                            //更新主单金额及数量
                            if (new T_BatchMoneyTradeBLL().UpdateByCountMoney(strFBid))
                            {
                                T_BatchMoneyTrade nbonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                                okInfo = model.FCrimeCode + "|" + model.FCriminal + "|" + model.FareaName + "|" + model.FAmount.ToString() + "|" + model.AmountC.ToString() + "|" + bonus.Remark + "|" + nbonus.cnt.ToString() + "|" + nbonus.FAmount.ToString();
                            }
                        }
                        else
                        {
                            strFlag = 4;//明细单保存失败
                            strRusult = "明细单保存失败";
                        }
                        
                    }
                }
            }
            return strRusult;
        }

        private static void SetBonusDetailModel(string LoginUserName, string strFCode, string strFMoney, string strFBid, decimal cpctMoney, T_BatchMoneyTrade_DTL model, T_Criminal criminal, T_BatchMoneyTrade bonus, T_AREA area, T_Criminal_card card)
        {
            model.Bid = strFBid;
            model.FCrimeCode = strFCode;
            model.FCriminal = criminal.FName;
            model.CardCode = card.cardcodea;
            model.FrealareaCode = "";
            model.FrealAreaName = "";
            model.FareaCode = area.FCode;
            model.FareaName = area.FName;
            model.FAmount = Convert.ToDecimal(strFMoney);
            model.AmountC = cpctMoney;
            model.CardType = 0;
            model.AccType = 1;
            model.CrtBy = LoginUserName;
            model.CrtDt = Convert.ToDateTime(bonus.crtdt);
            model.UDate = Convert.ToDateTime(bonus.UDate);
            model.PType = bonus.Remark;
            model.Remark = bonus.Remark;
            model.cqbt = 0;
            model.grkj = 0;
            model.gwjt = 0;
            model.ldjx = 0;
            model.tbbz = 0;
            model.Flag = 0;
            model.Vouno = "";
            model.ApplyBy = "";
        }

        //批量保存Dtl记录
        public ActionResult BatchSaveDtlList()
        {
            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            string deleted = Request["deleted"];

            string inserted = Request["inserted"];

            string updated = Request["updated"];


            //if (inserted != null)
            //{
            //    //把json字符串转换成对象
            //    // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
            //    //TODO 下面就可以根据转换后的对象进行相应的操作了

            //    JavaScriptSerializer jss = new JavaScriptSerializer();
            //    //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
            //    JArray ja = (JArray)JsonConvert.DeserializeObject(inserted);
            //    if (ja.Count > 0)
            //    {
            //        List<T_BONUSDTL> models = DoBatchDtlInfoAdd(ja);
            //        return Content("OK|" + jss.Serialize(models));
            //    }
            //}

            if (updated != null)
            {
                //把json字符串转换成对象
                // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
                //TODO 下面就可以根据转换后的对象进行相应的操作了

                JavaScriptSerializer jss = new JavaScriptSerializer();
                //List<T_SHO_AreaGoodMaxCount> listDeleted=jss.Deserialize<T_SHO_AreaGoodMaxCount>(inserted);
                JArray ja = (JArray)JsonConvert.DeserializeObject(updated);
                string bid = ja[0]["Bid"].ToString();
                T_BatchMoneyTrade bon = new T_BatchMoneyTradeBLL().GetModel(bid);
                if (bon != null)
                {
                    if (bon.FCheckFlag != 1)
                    {
                        if (ja.Count > 0)
                        {
                            T_BatchMoneyTrade model = DoBatchDtlInfoAdd(ja);

                            return Content("OK|" + jss.Serialize(model));
                        }
                    }
                    else
                    {
                        return Content("Err|已确认，不能再保存成功！");
                    }
                }

            }


            return Content("");
        }

        private static T_BatchMoneyTrade DoBatchDtlInfoAdd(JArray ja)
        {
            string bid = "";
            foreach (JObject o in ja)
            {
                T_BatchMoneyTrade_DTL m = new T_BatchMoneyTrade_DTLBLL().GetModel(Convert.ToInt32(o["seqno"].ToString()));
                if (m != null)
                {
                    m.FAmount = Convert.ToDecimal(o["FAmount"].ToString());
                    //获取处遇信息
                    T_Criminal criminal = new T_CriminalBLL().GetModel(o["FCrimeCode"].ToString());
                    T_CY_TYPE cy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                    m.AmountC = m.FAmount * (decimal)cy.cpct / 100;
                    bool b = new T_BatchMoneyTrade_DTLBLL().Update(m);
                    bid = m.Bid;
                }
            }
            new T_BatchMoneyTradeBLL().UpdateByCountMoney(bid);
            //List<T_BONUSDTL> models = new T_BONUSDTLBLL().GetModelList("Bid='"+ bid + "'");

            return new T_BatchMoneyTradeBLL().GetModel(bid); ;
        }

        public ActionResult InDbMainOrder()//财务入账主单
        {
            string strFBid = Request["sbid"];//主单编号
            if (string.IsNullOrEmpty(strFBid) == false)
            {
                T_BatchMoneyTrade bonus = new T_BatchMoneyTradeBLL().GetModel(strFBid);
                if (bonus != null)
                {
                    if (bonus.FdbCheckFlag < 1)
                    {
                        return Content("Err|财务未复核，不能入账");
                    }
                    if (bonus.Flag >= 1)
                    {
                        return Content("Err|该主单已经入账过了，不能重复入账");
                    }
                }
                string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
                if (new T_BatchMoneyTradeBLL().UpdateInDbFlag(strFBid, strLoginName))
                {
                    List<T_BatchMoneyTrade_ErrList> imports = new T_BatchMoneyTrade_ErrListBLL().GetModelList("pc='" + strFBid + "' and Remark='该记录财务入账时，已离监销户了'");
                    if (imports.Count > 0)
                    {
                        return Content("OK|提交成功,但有" + imports.Count.ToString() + "条已销IC卡，不能入账");
                    }
                    else
                    {
                        return Content("OK|全部提交成功");
                    }
                }
                else
                {
                    return Content("Err|提交失败");
                }

            }
            else
            {
                return Content("Err.Bid主单号不能为空");
            }

        }
	}

    public class rtnTradeExcel
    {
        public T_BatchMoneyTrade trade { get; set; }
        public List<T_BatchMoneyTrade_DTL> dtls { get; set; }
    }
}