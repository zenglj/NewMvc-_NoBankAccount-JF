﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using SelfhelpOrderMgr.Web.Filters;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class LingyongJinController : BaseController
    {
        //
        // GET: /Laobao/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index(int id=1)
        {
            //监区队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;
            
            return View();
        }
        public ActionResult getBonus(int id = 1)
        {
            string action = Request["action"];
            List<T_Provide> bonuies;
            if (action == "LoginIn")
            {
                bonuies = new T_ProvideBLL().GetModelList("isnull(flag,0)=0");
            }
            else if (action == "GetSearchMainOrder")
            {
                    string strAreaName = Request["fAreaName"];//队别名称
                    if (strAreaName == "请选择队别")
                    {
                        strAreaName = "";
                    }//LingyongJin
                        string strStartDate = Request["startDate"];//开始日期
                        string strEndDate = Request["endDate"];//结束日期
                        string strRemark = Request["fRemark"];//结束日期
                        string strFCrimeCode = Request["fCrimeCode"];//编号
                        string strFCrimeName = Request["fCrimeName"];//姓名    
                        //获取查询条件的SQL
                        StringBuilder strSql = GetSearchSql(strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName);
                        List<T_Provide> list;
                        list = (List<T_Provide>)new T_ProvideBLL().GetModelList(strSql.ToString());
                        if ( list==null)
                        {
                            list = new List<T_Provide>();
                            T_Provide m1 = new T_Provide();
                            list.Add(m1);
                        }
                        //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                        //context.Response.Write(sss);
                        return Content(jss.Serialize(list));
                }
            else
            {
                bonuies = new List<T_Provide>();
            }

            return Content(jss.Serialize(bonuies));
        }

        private static StringBuilder GetSearchSql(string strAreaName, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();
            //队别
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and FAreaName in( select fname from t_area where fname='" + strAreaName + @"' or fid in(
                                    select id from t_area where fname='" + strAreaName + "') ) ");
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
                    strSql.Append(@"  pid in (
                                select pid from T_ProvideDtl ");
                }
                else
                {
                    strSql.Append(@"and pid in (
                                select pid from T_ProvideDtl ");
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

        public ActionResult AddMainOrder()//增加主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strFAreaName = Request["sAreaName"];//队别名称
            //string strFAreaCode = context.Request["sAreaCode"];//队别编号
            string strFYear = Request["syear"];//年
            string strFMonth = Request["smonth"];//月 
            //获得月最大发放次数
            T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];
            int sendCount = new T_ProvideBLL().GetSendCountByArea(strFAreaName, strFYear + "-" + strFMonth + "-1");
            if (Convert.ToInt32(sModel.VALUE) <= sendCount)
            {
                return Content("Error|各队别每月发放次数，不能超过" + sModel.VALUE + "次");
            }
            else
            {
                //生成设定一个主单信息
                bonusInfo bonusinfo = new bonusInfo();
                bonusinfo.strApplyBy = Request["sapplyby"];//申请人
                bonusinfo.strFAreaName = Request["sAreaName"];//队别名称
                bonusinfo.strFAreaCode = Request["sAreaCode"];//队别编号
                bonusinfo.strFYear = Request["syear"];//年
                bonusinfo.strFMonth = Request["smonth"];//月 
                bonusinfo.strRemark = Request["sremark"];//备注
                bonusinfo.strCrtDate = strFYear + "-" + strFMonth + "-1";
                T_Provide model = SetMainOrderModelInfo(bonusinfo, strLoginName);
                if (new T_ProvideBLL().Add(model, "part"))
                {
                    T_Provide newModel = new T_ProvideBLL().GetModel(model.PId);
                    return Content("OK|" +jss.Serialize(newModel));
                }
                else
                {
                    return Content("Error|创建主单失败");
                }
            }
        }

        private static T_Provide SetMainOrderModelInfo(bonusInfo bonusinfo, string LoginUserName)
        {
            T_Provide model = new T_Provide();
            model.PId = new T_ProvideBLL().CreateOrderId("PID");
            model.PType = "零用金";
            model.PTag = 3;
            model.PTagName = "零用金";
            model.ApplyBy = bonusinfo.strApplyBy;
            model.CrtDt = DateTime.Today;
            model.PFlag= 0;
            model.Flag = 0;
            model.PDate =Convert.ToDateTime( bonusinfo.strCrtDate);
            model.Remark = bonusinfo.strRemark;
            model.FAreaName = bonusinfo.strFAreaName;
            model.FAreaCode = bonusinfo.strFAreaCode;
            model.CrtBy = LoginUserName;
            model.FAmount = 0;
            model.FManNb = 0;
            model.FManAmount = 0;
            model.FWomNb = 0;
            model.FWomAmount = 0;
            model.ApplyDt = DateTime.Today;
            model.PType = bonusinfo.strRemark;
            return model;
        }

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
            //List<T_ProvideDTL> bonusDtls = new T_ProvideDTLBLL().GetModelList("PId='" + FBid + "'");
            //return Content(jss.Serialize(bonusDtls));

            listRows = new T_ProvideBLL().GetDtlListCount("PId='" + FBid + "'")[0];


            List<T_ProvideDTL> bonusDtls = new T_ProvideBLL().GetDtlPageList(page, row, "PId='" + FBid + "'", "seqno");
            //return Content(jss.Serialize(bonusDtls));
            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(bonusDtls) + "}";
            return Content(sss);
        }

        //删除明细记录
        [MyLogActionFilterAttribute]
        public ActionResult DelOrderDetail(string sbid,string seqno)
        {
            string strFBid = sbid;// Request["sbid"];//主单编号
            string strSeqno = seqno;// Request["seqno"];//流水号
            T_Provide bonus = new T_ProvideBLL().GetModel(strFBid);

            if (bonus.PFlag==1)
            {
                return Content("Error.主单已经确认明细不能删除");              
            }
            if (new T_ProvideDTLBLL().Delete(Convert.ToInt32(strSeqno)))
            {
                new T_ProvideBLL().UpdateByCountMoney(strFBid);
                T_Provide model = new T_ProvideBLL().GetModel(strFBid);
                return Content("OK." + model.PId + "|" + model.FManNb+ "|"+ model.FWomNb + "|" + model.FAmount);
            }
            else
            {
                return Content("Error.删除失败");
            }
        }

        public ActionResult PostMainOrder()//提交主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strFBid = Request["sbid"];//主单编号
            T_Provide model = new T_Provide();
            model.CheckBy = "";
            model.CheckDt = DateTime.Today;
            model.PFlag = 1;
            model.PId = strFBid;
            if (new T_ProvideBLL().UpdateByCheckFlag(model))
            {
                return Content("OK.提交成功");
            }
            else
            {
                return Content("OK.提交失败");
            }
        }

        public ActionResult DelMainOrder(string sbid)//删除主单
        {
            string strFBid = sbid;// Request["sbid"];//主单编号
            T_Provide model = new T_ProvideBLL().GetModel(strFBid);
            if (model.Flag != 1)//判断是否已经审核
            {
                if (new T_ProvideBLL().Delete(strFBid))
                {
                    new T_ProvideBLL().DeleteDtlByPid(strFBid);
                    return Content("OK.主单删除成功");
                }
                else
                {
                    return Content("Error.删除主单失败");
                }
            }
            else
            {
                return Content("Error.已经审核不能删除");
            }
        }

        [MyLogActionFilterAttribute]
        public ActionResult CancalOrderById(string sbid)//账务退账
        {
            string PId = sbid;// Request["PId"];
            if (string.IsNullOrEmpty(PId))
            {
                return Content("Err.主单号不能为空");
            }

            T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if (czy == null)
            {
                return Content("Err|操作员账户不能为空,请用管理员登录");
            }
            if (czy.FINVCHK != 1)
            {
                return Content("Err|不是管理员不能删除,请用管理员登录");
            }
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            List<T_Vcrd> checkVcrds = new T_VcrdBLL().GetModelList(" flag in(0,-2) and typeflag=3 and Origid='" + PId + "'");
            if (checkVcrds.Count <= 0)
            {
                return Content("Err.该主单号没有对应的Vcrd记录，无法批量删除！");
            }

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList(" flag in(0,-2) and typeflag=3 and Origid='" + PId + "' and isnull(bankflag,0)>=1");
            if (vcrds.Count > 0)
            {
                return Content("Err.该主单号的数据已经发送到银行了，不能删除！");
            }

            //开始删除数据
            
            if (new T_ProvideBLL().plDelLingYongJinByPId(PId, strLoginName))
            {
                return Content("OK.退账成功！");
            }
            else
            {
                return Content("Err.对不起，退账失败！");
            }

        }

        public ActionResult ExcelOut()//Excel导出成功记录
        {
            string strFBid = Request["FBidExcel"];
            DataTable dt = new T_ProvideBLL().GetDtlDataTableByPid(strFBid);
            string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LingYJList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            //ExcelRender.RenderToExcel(dt, strFileName);
            ExcelRender.RenderToExcel(dt, "成功记录清单", 2, strFileName);
            return Content("OK|"+strFBid + "_LingYJList.xls");
        }

        public ActionResult ErrorListOutport()//Excel导出成功记录
        {
            string strFBid = Request["excelBid"];
            DataTable dt = new T_ProvideBLL().GetErrListDataTable(strFBid);
            string strFileName = new CommonClass().GB2312ToUTF8(strFBid + "_LingYJErrList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            //ExcelRender.RenderToExcel(dt, strFileName);
            ExcelRender.RenderToExcel(dt, "失败记录清单", 3, strFileName);
            return Content("OK|" + strFBid + "_LingYJErrList.xls");
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
                    int ErrNums = 0;
                    if (rows < 2)
                    {
                        return Content("Excel表为空表,无数据!");
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

                            //string okInfo;
                            //int strFlag;
                            ////检测并增加一条记录（劳动报酬）
                            //string strDoResult = CheckAndAddRecord(strLoginName, Session["loginUserCode"].ToString(), FCode, FName, FMoney, strFBid, out okInfo, out strFlag);
                            ////string strDoResult="";
                            
                            //if (strDoResult != "")
                            //{
                            //    T_ImportList importList = new T_ImportList();
                            //    importList.ImportType = 4;
                            //    importList.fcrimecode = FCode;
                            //    importList.fname = FName;
                            //    importList.Amount = Convert.ToDecimal(FMoney);
                            //    importList.Crtdt = DateTime.Now;
                            //    importList.CrtBy = strLoginName;
                            //    importList.Remark = strDoResult;
                            //    importList.pc = strFBid;
                            //    importList.notes = "";
                            //    //插入失败记录
                            //    new T_ImportListBLL().Add(importList);
                            //    ErrNums++;
                            //}

                        }
                        #region 写入到数据库中
                        //将DataTabe dtUserAdd 写入到数据库中
                        new CommTableInfoBLL().ExecSql("Delete from t_Bonus_Temp where Bid='" + strFBid + "'");
                        string strAddResult=new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "dbo.[T_Bonus_Temp]");
                        if (strAddResult=="1")
                        {
                            if (new T_ProvideBLL().PLWriteProvideDtl(strFBid, strLoginName))
                            {
                                rtnLYJExcel rtn = new rtnLYJExcel();
                                rtn.provide = new T_ProvideBLL().GetModel(strFBid);
                                rtn.dtls = new T_ProvideBLL().GetDtlPageList(1, 10, "PId='" + strFBid + "'", "seqno");
                                //T_BONUS tbouns = new T_BONUSBLL().GetModel(strFBid);
                                List<T_ImportList> imports = new T_ImportListBLL().GetModelList("pc='" + strFBid + "'");
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
                                return Content("Err|劳酬更新余额数据时失败." + strAddResult);
                            }
                        }
                        else
                        {
                            return Content("Err|写入银行Excel文件时失败");
                        }
                        #endregion
                    }
                }
            }
            return Content("导入失败，服务器没有接收到Excel文件");
        }

        private static string CheckAndAddRecord(string LoginUserName, string LoginUserCode, string strFCode, string strFName, string strFMoney, string strFBid, out string okInfo, out int strFlag)
        {
            string strRusult = "";//返回处理结果信息
            decimal cpctMoney = 0;//留存金额
            okInfo = "";//成功的信息
            T_ProvideDTL model = new T_ProvideDTL();

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
                    T_Provide bonus = new T_ProvideBLL().GetModel(strFBid);
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
                            return "该犯人未办理IC卡";
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
                        
                        //获得系统最大奖金发放次数
                        T_SETTINGS sModel = new T_SETTINGSBLL().GetModelList("TYPE=606")[0];

                        string strUdate = Convert.ToString(bonus.PDate);
                        string strWhere = " UDate='" + strUdate + "' and FCrimeCode='" + strFCode + "'";
                        int userCount = new T_ProvideBLL().GetSendCountByPid(strFCode, strUdate);
                        if (Convert.ToInt32(sModel.VALUE) <= userCount)
                        {
                            strFlag = 7;//超过每月最大发放次数
                            strRusult = strFCode + "," + strFName + ",超过每月最大发放次数:" + sModel.VALUE;
                        }
                        else
                        {
                            //设定Model信息
                            SetBonusDetailModel(LoginUserName, strFCode, strFMoney, strFBid, cpctMoney, model, criminal, bonus, area, card);

                            if (new T_ProvideDTLBLL().Add(model) > 0)
                            {
                                //更新主单金额及数量
                                if (new T_ProvideBLL().UpdateByCountMoney(strFBid))
                                {
                                    T_Provide nbonus = new T_ProvideBLL().GetModel(strFBid);
                                    okInfo = model.FCrimeCode + "|" + model.FCriminal + "|" + model.FAreaName + "|" + model.FAmount.ToString() + "|" + model.FSex.ToString() + "|" + bonus.Remark + "|" + (nbonus.FManNb+nbonus.FWomNb).ToString() + "|" + nbonus.FAmount.ToString();
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
            }
            return strRusult;
        }

        private static void SetBonusDetailModel(string LoginUserName, string strFCode, string strFMoney, string strFBid, decimal cpctMoney, T_ProvideDTL model, T_Criminal criminal, T_Provide bonus, T_AREA area, T_Criminal_card card)
        {
            model.PId = strFBid;
            model.FCrimeCode = strFCode;
            model.FCriminal = criminal.FName;
            model.CardCode = card.cardcodea;
            model.FSex = criminal.FSex;
            model.FrealareaName = "";
            model.FrealareaCode = "";
            model.FAreaCode = area.FCode;
            model.FAreaName = area.FName;
            model.FAmount = Convert.ToDecimal(strFMoney);
            //model.AmountC = cpctMoney;
            model.CardType = 0;
            model.AccType = 0;
            //model.crtby = LoginUserName;
            //model.crtdt = Convert.ToDateTime(bonus.crtdt);
            model.PDate = Convert.ToDateTime(bonus.PDate);
            //model.re = bonus.Remark;
            //model.remark = bonus.Remark;
            //model.cqbt = 0;
            //model.grkj = 0;
            //model.gwjt = 0;
            //model.ldjx = 0;
            //model.tbbz = 0;
            //model.FLAG = 0;
            model.VouNo = "";
            //model.app = "";
        }

        public ActionResult QueryCrimeCode()//查询账号
        {
            #region 查询犯人编号信息
            string strFCrimeCode = Request["FCode"];//编号
            T_Criminal model = new T_CriminalBLL().GetModel( strFCrimeCode);
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
            try 
            {
                Decimal myMoney=Convert.ToDecimal(strFMoney);
            }
            catch
            {
                return Content("Error.金额必须是数值的");
            }
            string okInfo;
            int strFlag;
            //检测并增加一条记录（劳动报酬）
            string strResult = CheckAndAddRecord(strLoginName, Session["loginUserCode"].ToString(),  strFCode, strFName, strFMoney, strFBid, out okInfo, out strFlag);
            if (strResult == "")
            {
                return Content("OK." + okInfo);
            }
            else
            {
                return Content("Error." + strResult);
            }
        }

        public ActionResult PrintReportList()//打印报表
        {
            string strbid = Request["bid"];
            List<T_ProvideDTL> lists = new T_ProvideDTLBLL().GetModelList("pid='" + strbid + "'");
            ViewData["lists"] = lists;

            T_Provide bonus = new T_ProvideBLL().GetModel(strbid);
            ViewData["bonus"] = bonus;
            return View();
        }


        //按队别批量生成零用金记录
        public ActionResult CreateAreaList()
        {
            string strbid = Request["sbid"];
            T_Provide bon = new T_ProvideBLL().GetModel(strbid);
            if(bon!=null)
            {
                if(bon.PFlag==1)
                {
                    return Content("Err|主单已确认，不能再增加");
                }
            }
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (new T_ProvideBLL().BatchCreateAreaList(strbid, strLoginName))
            {
                List<T_ProvideDTL> lists = new T_ProvideBLL().GetDtlPageList(1,10,"pid='" + strbid + "'","seqno");
                new T_ProvideBLL().UpdateByCountMoney(strbid);
                T_Provide provide=new T_ProvideBLL().GetModel(strbid);
                return Content("OK|"+jss.Serialize(lists)+"|" +jss.Serialize(provide));
            }
            return Content("Err|创建失败");

        }

        //全监批量生成零用金记录
        public ActionResult CreateAllList()
        {
            string strbid = Request["sbid"];
            T_Provide bon = new T_ProvideBLL().GetModel(strbid);
            if (bon != null)
            {
                if (bon.PFlag == 1)
                {
                    return Content("Err|主单已确认，不能再增加");
                }
            }
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (new T_ProvideBLL().BatchCreateAllList(strbid, strLoginName))
            {
                List<T_ProvideDTL> lists = new T_ProvideBLL().GetDtlPageList(1,10,"pid='" + strbid + "'","seqno");
                new T_ProvideBLL().UpdateByCountMoney(strbid);
                T_Provide provide = new T_ProvideBLL().GetModel(strbid);
                return Content("OK|" + jss.Serialize(lists) + "|" + jss.Serialize(provide));
            }
            return Content("Err|创建失败");

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
                string bid = ja[0]["BID"].ToString();
                T_Provide bon = new T_ProvideBLL().GetModel(bid);
                if(bon!=null)
                {
                    if(bon.PFlag!=1)
                    {
                        if (ja.Count > 0)
                        {
                            T_Provide model = DoBatchDtlInfoAdd(ja);
                            
                            return Content("OK|"+jss.Serialize(model));
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


        //执行限量DTL写入操作
        private static T_Provide DoBatchDtlInfoAdd(JArray ja)
        {
            string bid = "";
            foreach (JObject o in ja)
            {
                T_ProvideDTL m = new T_ProvideDTLBLL().GetModel(Convert.ToInt32(o["seqno"].ToString()));
                if (m != null)
                {
                    m.FAmount = Convert.ToDecimal( o["FAMOUNT"].ToString());
                    //获取处遇信息
                    T_Criminal criminal = new T_CriminalBLL().GetModel(o["FCRIMECODE"].ToString());
                    T_CY_TYPE cy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                    //m.AmountC = m.FAmount * (decimal)cy.cpct / 100;
                    bool b = new T_ProvideDTLBLL().Update(m);
                    bid = m.PId;
                }
            }
            new T_ProvideBLL().UpdateByCountMoney(bid);
            //List<T_BONUSDTL> models = new T_BONUSDTLBLL().GetModelList("Bid='"+ bid + "'");

            return new T_ProvideBLL().GetModel(bid); ;
        }

        //过账
        public ActionResult DbInPostMainOrder()
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strFBid = Request["sbid"];//主单编号
            T_Provide provide = new T_ProvideBLL().GetModel(strFBid);
            if (provide.PFlag != 1)
            {
                return Content("Err|主单未确认，不能过账");
            }
            if (provide.Flag == 1)
            {
                return Content("Err|主单已过账，不能重复过账");
            }

            int intAcctype = 0;
            T_SHO_ManagerSet mset=new T_SHO_ManagerSetBLL().GetModel("LingYongJin_Acctype");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    intAcctype = 1;
                }
            }

            if (new T_ProvideBLL().UpdateInDbData(strFBid, strLoginName, intAcctype))
            {
                return Content("OK.提交成功");
            }
            else
            {
                return Content("Err.提交失败");
            }
        }

	}

    public class lingyjInfo
    {
        public string strApplyBy { get; set; }//申请人
        public string strFAreaName { get; set; }//队别名称
        public string strFAreaCode { get; set; }//队别编号
        public string strFYear { get; set; }//年
        public string strFMonth { get; set; }//月 
        public string strRemark { get; set; }//备注
        public string strCrtDate { get; set; }
    }

    public class rtnLYJExcel
    {
        public T_Provide provide { get; set; }
        public List<T_ProvideDTL> dtls { get; set; }
    }
}