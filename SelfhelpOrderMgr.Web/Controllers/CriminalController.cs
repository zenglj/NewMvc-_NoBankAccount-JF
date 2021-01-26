using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [CustomActionFilterAttribute]
    public class CriminalController : Controller
    {
        //
        // GET: /Criminal/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        
        public ActionResult Index()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            List<T_CY_TYPE> cys = new T_CY_TYPEBLL().GetModelList("");
            ViewData["cys"] = cys;

            List<T_CRIME> crimes = new T_CRIMEBLL().GetModelList("");
            ViewData["crimes"] = crimes;

            //DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from t_Vcrd Order by CrtBy");
            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct fname from t_Czy");
            List<string> crtbys = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    crtbys.Add(row[0].ToString());
                }
            }
            ViewData["crtbys"] = crtbys;


            //dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where damount<>0 Order by Dtype");
            dt = new CommTableInfoBLL().GetDataTable("select distinct typename from [t_bankFeeList] where typeid in (0,1,3,4,6,14)");
            
            List<string> cashtypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cashtypes.Add(row[0].ToString());
                }
            }
            ViewData["cashtypes"] = cashtypes;

            //dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where camount<>0 Order by Dtype");
            dt = new CommTableInfoBLL().GetDataTable("select distinct typename from [t_bankFeeList] where typeid not in (0,1,3,4,6,14)");

            List<string> paytypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    paytypes.Add(row[0].ToString());
                }
            }
            ViewData["paytypes"] = paytypes;
            T_SHO_ManagerSet cyMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCy");
            
            ViewData["cyMset"] = "0";
            if (cyMset != null)
            {
                ViewData["cyMset"] = cyMset.MgrValue;
            }
            T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByArea");
            ViewData["areaMset"] = "0";
            if (cyMset != null)
            {
                ViewData["areaMset"] = areaMset.MgrValue;
            }

            //测试一下
            //List<T_Criminal> lists =  DapperHelperBLL<T_Criminal>.GetAll("select * from t_criminal where fcycode=@fcycode", new { fcycode = "3" });
            return View();
        }


        public ActionResult GetSearchUsers(string strJsonWhere, string orderField = " id asc ", int page = 1, int rows = 10)
        {

            PageResult<ViewCriminalInfo> rs = new T_Bank_DepositListBLL().GetPageList<ViewCriminalInfo, ViewCriminalInfoExt_Search>(orderField, strJsonWhere, page, rows);
            return Json(rs);

            #region 老旧方式--停用了
            //if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            //{
            //    ViewCriminalInfo s = Newtonsoft.Json.JsonConvert.DeserializeObject<ViewCriminalInfo>(strJsonWhere);
            //    Type stype = typeof(ViewCriminalInfo);
            //    var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
            //        .ToList();
            //}

            //string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            //分页信息

            //string action = Request["action"];
            //string strPage = Request["page"];
            //string strRow = Request["rows"];
            //int page = 1;
            //int row = 10;
            //decimal listRows = 0;
            //string sss = "";
            //if (string.IsNullOrEmpty(strPage) == false)
            //{
            //    page = Convert.ToInt32(strPage);
            //}

            //if (string.IsNullOrEmpty(strRow) == false)
            //{
            //    row = Convert.ToInt32(strRow);
            //}

            //listRows = new T_CriminalBLL().GetCriminalsCount(strWhere.ToString());

            //List<T_Criminal> criminals = new T_CriminalBLL().GetPageCriminalExtInfo(page, row, strWhere, "FCode");

            //sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(criminals) + "}";
            //return Content(sss);
            #endregion
        }

        #region 老旧方式-停用了--直接拼接字符串的方式
        //将Request参数传入GetSearchWhere函数，并生成查询条件
        private string SetRequestSearchWhere(string LoginCode)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string rjStartTime = Request["rjStartTime"];
            string rjEndTime = Request["rjEndTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string endFCode = Request["endFCode"];
            string cyName = Request["cyName"];
            string CrtBy = Request["CrtBy"];
            string CriminalFlag = Request["CriminalFlag"];//是否在押
            string CashTypes = Request["CashTypes"];//存款类型
            string PayTypes = Request["PayTypes"];//取款类型
            string AccTypes = Request["AccTypes"];//账户类型
            string BankFlags = Request["BankFlags"];//银行状态类型

            string strWhere = GetSearchWhere(LoginCode, ref startTime, ref endTime, ref rjStartTime, ref rjEndTime, areaName, FName, FCode, endFCode, CrtBy, CriminalFlag, CashTypes, PayTypes, AccTypes, BankFlags, cyName);
            return strWhere;
        }

        //生成Vcrd查询条件
        private static string GetSearchWhere(string LoginCode, ref string startTime, ref string endTime, ref string rjStartTime, ref string rjEndTime, string areaName, string FName, string FCode, string endFCode, string CrtBy, string CriminalFlag, string CashTypes, string PayTypes, string AccTypes, string BankFlags, string cyName)
        {
            string strWhere = " 1=1 ";

            //验证用户的队别,如果设定了Vcrd验证用户队别，则要查看是否有相应的队别权限下的犯人才可以查询到
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");


            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    strWhere = " FAreaCode in (  select fareaCode from t_czy_area where fflag=2 and fcode='" + LoginCode + "')";
                }
            }


            //刑满日期
            if (string.IsNullOrEmpty(startTime) == false)
            {
                DateTime sdt = Convert.ToDateTime(startTime);
                startTime = sdt.Year.ToString() + "-" + sdt.Month.ToString() + "-" + sdt.Day.ToString() + " 00:00:00";
            }
            if (string.IsNullOrEmpty(endTime) == false)
            {
                DateTime edt = Convert.ToDateTime(endTime);
                endTime = edt.Year.ToString() + "-" + edt.Month.ToString() + "-" + edt.Day.ToString() + " 23:59:00";
            }

            if (string.IsNullOrEmpty(startTime) == false)
            {
                strWhere = strWhere + " and  FOuDate>='" + startTime + "'";
            }
            if (string.IsNullOrEmpty(startTime) == false)
            {
                strWhere = strWhere + " and  FOuDate<'" + endTime + "'";
            }

            //入监日期
            if (string.IsNullOrEmpty(rjStartTime) == false)
            {
                DateTime rjsdt = Convert.ToDateTime(rjStartTime);
                rjStartTime = rjsdt.Year.ToString() + "-" + rjsdt.Month.ToString() + "-" + rjsdt.Day.ToString() + " 00:00:00";
            }
            if (string.IsNullOrEmpty(rjEndTime) == false)
            {
                DateTime rjedt = Convert.ToDateTime(rjEndTime);
                rjEndTime = rjedt.Year.ToString() + "-" + rjedt.Month.ToString() + "-" + rjedt.Day.ToString() + " 23:59:00";
            }

            if (string.IsNullOrEmpty(rjStartTime) == false)
            {
                strWhere = strWhere + " and  FInDate>='" + rjStartTime + "'";
            }
            if (string.IsNullOrEmpty(rjEndTime) == false)
            {
                strWhere = strWhere + " and  FInDate<'" + rjEndTime + "'";
            }


            if (string.IsNullOrEmpty(areaName) == false)
            {
                if (areaName != "请选择队别")
                {
                    //strWhere = strWhere + " and FAreaName='" + areaName + "'";
                    strWhere = strWhere + @" and fAreacode in (select fcode from t_area where fcode='" + areaName + @"' or fid in(
                        select id from t_area where fcode='" + areaName + "')) ";
                }
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FName like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                if (string.IsNullOrEmpty(endFCode) == false)
                {
                    strWhere = strWhere + " and FCode between '" + FCode + "'" + " and '" + endFCode + "'";
                }
                else
                {
                    strWhere = strWhere + " and FCode='" + FCode + "'";
                }

            }
            if (string.IsNullOrEmpty(CrtBy) == false)
            {
                strWhere = strWhere + " and FCZY='" + CrtBy + "'";
            }

            if (string.IsNullOrEmpty(AccTypes) == false)
            {
                if (AccTypes == "0")
                {
                    strWhere = strWhere + " and CardCode='' ";
                }
                else
                {
                    strWhere = strWhere + " and CardCode<>''";
                }
            }

            if (string.IsNullOrEmpty(BankFlags) == false)
            {
                if (BankFlags == "0")
                {
                    strWhere = strWhere + " and BankCardNo='' ";
                }
                else
                {
                    strWhere = strWhere + " and BankCardNo<>''";
                }
            }

            if (string.IsNullOrEmpty(CriminalFlag) == false)
            {
                strWhere = strWhere + " and isnull(FFlag,0) in ( " + CriminalFlag + ")";
            }
            if (string.IsNullOrEmpty(cyName) == false)
            {
                strWhere = strWhere + " and FCYCode ='" + cyName + "'";
            }
            return strWhere;
        }
        #endregion

        /// <summary>
        /// 保存犯人信息
        /// </summary>
        /// <param name="txtFCode"></param>
        /// <param name="txtFName"></param>
        /// <param name="txtFSex"></param>
        /// <param name="txtFAreaCode"></param>
        /// <param name="txtFCyCode"></param>
        /// <param name="txtFAddr"></param>
        /// <param name="txtFIdenNo"></param>
        /// <param name="txtFCrimeCode"></param>
        /// <param name="txtFTerm"></param>
        /// <param name="txtFInDate"></param>
        /// <param name="txtFOuDate"></param>
        /// <param name="txtFlimitFlag"></param>
        /// <param name="txtFlimitAmt"></param>
        /// <param name="txtFDesc"></param>
        /// <param name="doType"></param>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult SaveCriminal(string txtFCode, string txtFName, string txtFSex
            , string txtFAreaCode, string txtFCyCode, string txtFAddr, string txtFIdenNo, string txtFCrimeCode,
            string txtFTerm, string txtFInDate, string txtFOuDate, string txtFlimitFlag, string txtFlimitAmt,
            string txtFDesc, string doType)
        {
            //string txtFCode = Request["txtFCode"];
            //string txtFName = Request["txtFName"];
            //string txtFSex = Request["txtFSex"];
            //string txtFAreaCode = Request["txtFAreaCode"];
            //string txtFCyCode = Request["txtFCyCode"];
            //string txtFAddr = Request["txtFAddr"];
            //string txtFIdenNo = Request["txtFIdenNo"];
            //string txtFCrimeCode = Request["txtFCrimeCode"];
            //string txtFTerm = Request["txtFTerm"];
            //string txtFInDate = Request["txtFInDate"];
            //string txtFOuDate = Request["txtFOuDate"];
            //string txtFlimitFlag = Request["txtFlimitFlag"];
            //string txtFlimitAmt = Request["txtFlimitAmt"];
            //string txtFDesc = Request["txtFDesc"];
            //string doType = Request["doType"];

            //验证编号是否存在
            if (string.IsNullOrEmpty(txtFCode))
            {
                return Content("Err|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(txtFName))
            {
                return Content("Err|用户姓名不能为空");
            }
            if (string.IsNullOrEmpty(txtFAreaCode))
            {
                return Content("Err|请选择一个所在队别");
            }
            if (string.IsNullOrEmpty(txtFCyCode))
            {
                return Content("Err|请选择一个所处遇级别");
            }
            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(txtFCode, 1);
            T_Criminal oldCriminal = criminal;//保存更改前的记录
            if (criminal != null)
            {
                if (criminal.fflag == 1)
                {
                    return Content("Err|该犯人已经离监了，不能再编辑");
                }
                if (string.IsNullOrEmpty(doType) == false)
                {
                    if (doType == "add")
                    {
                        return Content("Err|该编号已经存在，不能输入重复的编号");
                    }
                }
            }
            else
            {
                criminal = new T_Criminal();
            }
            criminal.FCode = txtFCode;
            criminal.FName = txtFName;
            criminal.FSex = txtFSex;

            try
            {
                if (doType != "add")
                {
                    T_SHO_ManagerSet cyMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCy");
                    if (cyMset != null)
                    {
                        if (cyMset.MgrValue == "0")
                        {
                            criminal.FCYCode = txtFCyCode;
                        }
                        else
                        {
                            criminal.FCYCode = oldCriminal.FCYCode;
                        }
                    }
                    T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByArea");
                    if (areaMset != null)
                    {
                        if (areaMset.MgrValue == "0")
                        {
                            criminal.FAreaCode = txtFAreaCode;
                        }
                        else
                        {
                            criminal.FAreaCode = oldCriminal.FAreaCode;
                        }
                    }
                }
                else
                {
                    criminal.FAreaCode = txtFAreaCode;
                    criminal.FCYCode = txtFCyCode;
                }

                criminal.FAddr = txtFAddr;
                criminal.FIdenNo = txtFIdenNo;
                criminal.FCrimeCode = txtFCrimeCode;
                criminal.FTerm = txtFTerm;
                criminal.FDesc = txtFDesc;
                criminal.FInDate = Convert.ToDateTime("1900-01-01");
                criminal.FOuDate = Convert.ToDateTime("1900-01-01");
                if (string.IsNullOrEmpty(txtFInDate) == false)
                {
                    criminal.FInDate = Convert.ToDateTime(txtFInDate);
                }
                if (string.IsNullOrEmpty(txtFOuDate) == false)
                {
                    criminal.FOuDate = Convert.ToDateTime(txtFOuDate);
                }
                criminal.flimitflag = 0;
                criminal.flimitamt = 0;
                if (string.IsNullOrEmpty(txtFlimitFlag) == false)
                {
                    if (txtFlimitFlag == "on")
                    {
                        criminal.flimitflag = 1;
                    }
                }
                if (string.IsNullOrEmpty(txtFlimitAmt) == false)
                {
                    criminal.flimitamt = Convert.ToDecimal(txtFlimitAmt);
                }

                string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
                criminal.FCZY = strLoginName;
                if (doType == "add")
                {
                    criminal.Frealareacode = "";
                    criminal.FSubArea = "";
                    criminal.FAddr_tmp = "";
                    criminal.FAge = 0;
                    criminal.FStatus = 0;
                    criminal.FStatus2 = 0;

                    new T_CriminalBLL().Add(criminal, "");
                    return Content("OK|" + jss.Serialize(criminal));
                }
                else
                {
                    new T_CriminalBLL().Update(criminal, "");

                    if (criminal.FAreaCode != oldCriminal.FAreaCode)
                    {
                        T_AREA newArea = new T_AREABLL().GetModel(criminal.FAreaCode);
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                            ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                            ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                            ,[Remark])
                                 VALUES
                            ('" + criminal.FCode + "','" + criminal.FName + "','2','队别','" + oldCriminal.FAreaCode + "','" + oldCriminal.FAreaName + "','" + criminal.FAreaCode + "','" + newArea.FName + @"'
                            ,'人员管理调队','" + Session["loginUserName"].ToString() + "','" + DateTime.Now.ToString() + "','自动审核','系统','" + DateTime.Now.ToString() + "','变更不用审核',9,'')");
                        new CommTableInfoBLL().ExecSql(strSql.ToString());
                    }

                    if (criminal.FCYCode != oldCriminal.FCYCode)
                    {
                        T_CY_TYPE newCy = new T_CY_TYPEBLL().GetModel(criminal.FCYCode);
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append(@"INSERT INTO [T_Criminal_ChangeList]
                            ([FCode],[FName],[ChangeType],[ChangeTypeName],[OldCode],[OldName],[NewCode],[NewName]
                            ,[ChangeInfo],[CrtBy],[CrtDate],[AuditBy],[AuditArea],[AuditDate],[AuditInfo],[AuditFlag]
                            ,[Remark])
                                 VALUES
                            ('" + criminal.FCode + "','" + criminal.FName + "','1','处遇','" + oldCriminal.FCYCode + "','" + oldCriminal.CyName + "','" + criminal.FCYCode + "','" + newCy.FName + @"'
                            ,'人员管理调队','" + Session["loginUserName"].ToString() + "','" + DateTime.Now.ToString() + "','自动审核','系统','" + DateTime.Now.ToString() + "','变更不用审核',9,'')");
                        new CommTableInfoBLL().ExecSql(strSql.ToString());
                    }


                    Log4NetHelper.logger.Info("保存更新犯人信息,操作员：" + Session["loginUserName"].ToString() + ",更新前：ID=" + oldCriminal.FCode + ",用户名为：" + oldCriminal.FName + ",处遇为：" + oldCriminal.FCYCode + ",队别名为：" + oldCriminal.FAreaCode + ",身份证号为：" + oldCriminal.FIdenNo + ",冻结标志为：" + oldCriminal.flimitflag + ",冻结金额为：" + oldCriminal.flimitamt + "");
                    Log4NetHelper.logger.Info("保存更新犯人信息,操作员：" + Session["loginUserName"].ToString() + ",更新后：ID=" + criminal.FCode + ",用户名为：" + criminal.FName + ",处遇为：" + criminal.FCYCode + ",队别名为：" + criminal.FAreaCode + ",身份证号为：" + criminal.FIdenNo + ",冻结标志为：" + criminal.flimitflag + ",冻结金额为：" + criminal.flimitamt + "");


                    return Content("OK|更新保存成功");
                }

            }
            catch (Exception e)
            {
                return Content("Err|" + e.Message);
            }
        }

        [MyLogActionFilterAttribute]
        public ActionResult DelCriminal(string txtFCode)
        {
            //string txtFCode = Request["txtFCode"];
            if (string.IsNullOrEmpty(txtFCode))
            {
                return Content("Err|用户编号不能为空");
            }
            T_Criminal criminal = new T_CriminalBLL().GetModel(txtFCode);
            if (criminal != null)
            {
                //不验证了
                //if (criminal.fflag == 1)
                //{
                //    return Content("Err|该犯人已经离监了，不能再编辑");
                //}
                //T_Criminal_card card = new T_Criminal_cardBLL().GetModel(txtFCode);
                //if (card != null)
                //{
                //    return Content("Err|该犯人已经IC卡开户了，不能删除");
                //}
                if (new T_CriminalBLL().Delete(txtFCode))
                {
                    Log4NetHelper.logger.Info("删除犯人信息,操作员：" + Session["loginUserName"].ToString() + ",ID=" + criminal.FCode + ",用户名为：" + criminal.FName + ",处遇为：" + criminal.FCYCode + ",队别名为：" + criminal.FAreaCode + "");

                    return Content("OK|删除成功");
                }
                else
                {
                    return Content("Err|删除失败");
                }
            }
            else
            {
                return Content("Err|用户编号不存在");
            }
        }


        /// <summary>
        /// 恢复在押
        /// </summary>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult RecCriminal()
        {
            string txtFCode = Request["txtFCode"];
            if (string.IsNullOrEmpty(txtFCode))
            {
                return Content("Err|用户编号不能为空");
            }

            T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if(czy.FPRIVATE==null || czy.FPRIVATE == 0)
            {
                return Content("Err|您不是管理员，无权恢复，请与管理部门联系");
            }

            T_Criminal criminal = new T_CriminalBLL().GetModel(txtFCode);
            if (criminal != null)
            {
                if (criminal.fflag != 1)
                {
                    return Content("Err|该犯人是在押状态，不需恢复");
                }
                criminal.fflag = 0;
                if (new T_CriminalBLL().Update(criminal))
                {
                    Log4NetHelper.logger.Info("恢复离监人员为在押,操作员：" + Session["loginUserName"].ToString() + ",ID=" + criminal.FCode + ",用户名为：" + criminal.FName + "");
                    return Content("OK|恢复成功，请刷新记录列表");
                }
                else
                {
                    return Content("Err|恢复失败");
                }
            }
            else
            {
                return Content("Err|用户编号不存在");
            }
        }


        //Excel导出用户汇总总表
        public ActionResult ExcelCriminalSumOrder(string strJsonWhere, int id = 1)
        {
            StringBuilder strSql;
            string title;
            string result;
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            //如果要判断用户的监区管理权限的附加条件            
            string otherQuery = CommonQueryService.GetUserAreaPowerString(Session["loginUserCode"].ToString());

            //设置查询参数信息
            SetSumOrderWhereSqlByDapper(1, id,  out strSql, out title, out result);
            if (result != "")//Err|对不起,您传入错误的参数了
            {
                return Content(result);
            }
            //获取监狱单位名称
            string unitName = CommonQueryService.GetPrisonUnitName();
            //获取查询的数据表DataTable
            DataTable dt = new T_Bank_DepositListBLL().GetPageDataTable<ViewCriminalInfo, ViewCriminalInfoExt_Search>(" FCode asc", strJsonWhere, 1, 5000, otherQuery, strSql.ToString());
            
            //设置导出Excel文件的名称
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_Criminalinfo.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            switch (id)
            {
                case 1:
                    {
                        ExcelRender.RenderToExcel(dt, strFileName);
                    }
                    break;
                case 2:
                    {
                        //int[] cols={{10,1},6,10,12,6,40};
                        int[,] cols = { { 10, 1 }, { 6, 1 }, { 10, 1 }, { 12, 1 }, { 6, 1 }, { 42, 0 } };
                        //List<ExcelColumnsWides> cols = new List<ExcelColumnsWides>();

                        ExcelRender.RenderToExcelByBankList(dt, strFileName, title, unitName, cols);
                    }
                    break;
                case 3:
                    {
                        int[,] cols = { { 25, 1 }, { 25, 1 }, { 25, 1 } };
                        ExcelRender.RenderToExcelByShuishouList(dt, strFileName, title, unitName, cols);
                    }
                    break;
                case 4:
                    {
                        ExcelRender.RenderToExcel_NoSeqno(dt, strFileName);
                    }
                    break;

            }

            return Content("OK|" + strLoginName + "_Criminalinfo.xls");


            //return View();
        }

        //获取银行卡信息
        public ActionResult GetUserBankInfo(int id = 1)
        {
            string txtFCode = Request["FCode"];

            if (string.IsNullOrEmpty(txtFCode))
            {
                return Content("Err|犯人编号不能为空");
            }
            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(txtFCode);
            if (card == null)
            {
                return Content("Err|没有相关信息");
            }
            else
            {
                string sss = jss.Serialize(card);
                return Content("OK|" + sss);
            }
        }

        //保存银行卡信息
        public ActionResult BankCardSave(int id = 1)
        {
            string txtFCode = Request["FUserCode"];
            string txtFName = Request["FUserName"];
            string txtFCardNo = Request["FBankCardNo"];
            int regflag = 1;
            if (string.IsNullOrEmpty(txtFCardNo))
            {
                txtFCardNo = "";
                regflag = 0;
            }
            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(txtFCode);
            if (new T_Criminal_cardBLL().UpdateBankInfo(txtFCode, txtFCardNo, regflag))
            {
                Log4NetHelper.logger.Info("保存更新犯人银行卡号,操作员：" + Session["loginUserName"].ToString() + ",ID=" + card.fcrimecode + ",原银行卡号为：" + card.BankAccNo + ",更新后银行卡号为：" + txtFCardNo + "");

                return Content("OK|保存成功");
            }
            else
            {
                return Content("Err|操作失败");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="intFlag">查询SQL类型，0是报表，1是Excel 把字段转成中文件名</param>
        /// <param name="id"></param>
        /// <param name="strWhere"></param>
        /// <param name="strSql"></param>
        /// <param name="title"></param>
        /// <param name="result"></param>
        private static void SetSumOrderWhereSql(int intFlag, int id, string strWhere, out StringBuilder strSql, out string title, out string result)
        {

            strSql = new StringBuilder();
            title = "";
            result = "";
            switch (id)
            {
                case 1://按个用户个人汇总表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select fcode,FName,FIdenNo,FDesc,FAreaName,CyName ,CardCode ,SecondaryBankCard,BankCardNo  ");

                        }
                        else
                        {
                            strSql.Append("select fcode 编号,FName 姓名,FIdenNo 身份证号,FDesc 描述,FAreaName 队别,CyName 处遇级别,CardCode IC卡号,SecondaryBankCard 新银行卡,BankCardNo 停用旧烛光卡   ");
                        }
                        strSql.Append(@" from  (select a.*,b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.SecondaryBankCard,'') SecondaryBankCard,isnull(f.BankAccNo,'') BankCardNo
                            from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                            left outer join t_cy_type c on a.fcycode=c.fcode 
                            left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                            left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode) d ");
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by fcode ;");
                        title = "用户个人信息表";
                    }
                    break;
                case 2://银行开户清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select FName ,FSex ,FAge ,fcode ,'中国' nationality,FAddr ");

                        }
                        else
                        {
                            strSql.Append("select FName 中文姓名,FSex 性别,FAge 出生日期,fcode 证件号码,'中国' 国籍,FAddr 通讯地址  ");
                        }
                        strSql.Append(@" from  (select a.*,b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.BankAccNo,'') BankCardNo
                            from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                            left outer join t_cy_type c on a.fcycode=c.fcode 
                            left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                            left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode) d ");
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by fcode ;");
                        title = "在押服刑人员批量开卡清单";
                    }
                    break;
                case 3://银行税收清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select FName,fcode,'仅为中国税收居民' FDesc ");

                        }
                        else
                        {
                            strSql.Append("select FName 中文姓名 ,fcode 证件号码,'仅为中国税收居民' 声明 ");
                        }
                        strSql.Append(@" from  (select a.*,b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.SecondaryBankCard,'') BankCardNo
                            from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                            left outer join t_cy_type c on a.fcycode=c.fcode 
                            left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                            left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode) d ");
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by fcode ;");
                        title = "个人税收居民身份情况说明";
                    } break;
                case 4://银行税收清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select fcode 编号,FName 中文姓名 ,'' 金额,'' 备注 ");
                        }
                        else
                        {
                            strSql.Append("select fcode 编号,FName 中文姓名 ,'' 金额,'' 备注 ");
                        }
                        strSql.Append(@" from  (select a.*,b.FName FAreaName,c.FName CyName,d.FName CrimeName,isnull(f.CardCodea,'') CardCode,isnull(f.SecondaryBankCard,'') BankCardNo
                            from t_criminal a left outer join t_area b on a.fareacode=b.fcode 
                            left outer join t_cy_type c on a.fcycode=c.fcode 
                            left outer join T_CRIME d on a.FCrimeCode=d.FCode 
                            left outer join T_Criminal_Card f on a.FCode=f.FCrimeCode) d ");
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by fcode ;");
                        title = "Excel存款清单模板";
                    } break; 
                default:
                    {
                        result = "Err|对不起,您传入错误的参数了";
                        //return Content("Err|对不起,您传入错误的参数了");
                    }
                    break;

            }
        }

        private static void SetSumOrderWhereSqlByDapper(int intFlag, int id,  out StringBuilder strSql, out string title, out string result)
        {

            strSql = new StringBuilder();
            title = "";
            result = "";
            switch (id)
            {
                case 1://按个用户个人汇总表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(" fcode,FName,FIdenNo,FDesc,FAreaName,CyName ,CardCode ,SecondaryBankCard,BankCardNo  ");

                        }
                        else
                        {
                            strSql.Append(" fcode 编号,FName 姓名,FIdenNo 身份证号,FDesc 描述,FAreaName 队别,CyName 处遇级别,CardCode IC卡号,SecondaryBankCard 新银行卡,BankCardNo 停用旧烛光卡   ");
                        }
                        
                        title = "用户个人信息表";
                    }
                    break;
                case 2://银行开户清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(" FName ,FSex ,FAge ,fcode ,'中国' nationality,FAddr ");

                        }
                        else
                        {
                            strSql.Append(" FName 中文姓名,FSex 性别,FAge 出生日期,fcode 证件号码,'中国' 国籍,FAddr 通讯地址  ");
                        }
                        
                        title = "在押服刑人员批量开卡清单";
                    }
                    break;
                case 3://银行税收清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(" FName,fcode,'仅为中国税收居民' FDesc ");

                        }
                        else
                        {
                            strSql.Append(" FName 中文姓名 ,fcode 证件号码,'仅为中国税收居民' 声明 ");
                        }
                        
                        title = "个人税收居民身份情况说明";
                    }
                    break;
                case 4://银行税收清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(" fcode 编号,FName 中文姓名 ,'' 金额,'' 备注 ");
                        }
                        else
                        {
                            strSql.Append(" fcode 编号,FName 中文姓名 ,'' 金额,'' 备注 ");
                        }
                        
                        title = "Excel存款清单模板";
                    }
                    break;
                default:
                    {
                        result = "Err|对不起,您传入错误的参数了";
                        //return Content("Err|对不起,您传入错误的参数了");
                    }
                    break;

            }
        }

        public ActionResult ExcelInport()//Excel表格导入
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase f = Request.Files[0];
                string savePath = SavePostExcelFile(f);
                //string savePath = CommonQueryService.SavePostExcelFile(f);
                //导入耗时计算
                DateTime sdt;
                DateTime edt;
                TimeSpan tspan;
                //IWorkbook workbook = null;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;

                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);// 2003版本 
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);// 2007版本
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
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        DataTable dtUserAdd;
                        DataRow drTemp = null;
                        int modeId = 0;
                        T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("personInfoExcelImportMode");
                        if (mset != null)
                        {
                            if (mset.MgrValue == "1")
                            {
                                //编号，姓名，性别，地址，罪行，处遇级别，刑期，入监时间，出监时间，队别，身份证号，备注
                                dtUserAdd = SetPersonInfoByFZJY();//福州监狱模板
                                drTemp = ReadExcelPersonInfoByFZJY(sheet, rows, dtUserAdd, drTemp);//福州监狱模板
                                modeId = 1;
                            }
                            if (mset.MgrValue == "2")
                            {
                                //编号,姓名,性别,队别,处遇,身份证号,罪名,案号,备注
                                dtUserAdd = SetPersonInfoByZZJY();//漳州监狱模板
                                drTemp = ReadExcelPersonInfoByZZJY(sheet, rows, dtUserAdd, drTemp);//福州监狱模板
                                modeId = 2;
                            }
                            else
                            {
                                //编号,姓名,性别,队别,处遇,身份证号,备注
                                #region 定义DataTable
                                dtUserAdd = SetPersonInfoByBZ();//标准模板
                                drTemp = ReadExcelPersonInfoByBZ(sheet, rows, dtUserAdd, drTemp);
                                modeId = 0;
                                #endregion
                            }
                        }
                        else
                        {
                            #region 定义DataTable
                            dtUserAdd = SetPersonInfoByBZ();//标准模板
                            drTemp = ReadExcelPersonInfoByBZ(sheet, rows, dtUserAdd, drTemp);
                            #endregion
                        }

                        #region 写入到数据库中


                        try
                        {
                            string strAddResult = new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "dbo.[T_Criminal_MyTmp]");
                            if (strAddResult == "1")
                            {
                                tspan = DateTime.Now - sdt;
                                return Content(new T_CriminalBLL().PLExcelImport(strLoginName, modeId) + "。用时:" + tspan.TotalSeconds.ToString());

                            }
                            else
                            {
                                return Content("Err|" + strAddResult);
                            }
                        }
                        catch (Exception e)
                        {
                            return Content("Err|" + e.ToString());
                        }

                        #endregion


                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        //保存上传的Excel文件
        private string SavePostExcelFile(HttpPostedFileBase f)
        {
            string fname = f.FileName;
            /* startIndex */
            int index = fname.LastIndexOf("\\") + 1;
            /* length */
            int len = fname.Length - index;
            fname = fname.Substring(index, len);
            /* save to server */
            string savePath = Server.MapPath("~/Upload/" + fname);
            
            f.SaveAs(savePath);
            return savePath;
        }

        public ActionResult ExcelChangeEdu()//Excel调整存款金额消费购买食品额度
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string loginUserCode=Session["loginUserCode"].ToString();
            if (Request.Files.Count > 0)
            {
                //string strFBid = Request["excelBid"];
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
                //IWorkbook workbook = null;
                using (FileStream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read))
                {
                    sdt = DateTime.Now;

                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);// 2003版本 
                    //XSSFWorkbook workbook = new XSSFWorkbook(stream);// 2007版本
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
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        DataTable dtUserAdd;
                        DataRow drTemp = null;
                        int modeId=0;
                        dtUserAdd = new DataTable();
                        dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FName", typeof(string)));
                        dtUserAdd.Columns.Add(new DataColumn("FMoney", typeof(decimal)));
                        dtUserAdd.Columns.Add(new DataColumn("FRemark", typeof(string)));
                        //标准金额模板

                        drTemp = ReadExcelStandardTemplate(sheet, rows, dtUserAdd, drTemp);//标准金额模板

                        #region 写入到数据库中


                        try
                        {
                            string strAddResult = new CommTableInfoBLL().AddDataTableToDB(dtUserAdd, "dbo.[T_Criminal_TmpMoney]");

                            #region 检测有没有哪些人没有管理权限
                            string strCheckResult = "";
                            string strCheckSql = @"select * from T_Criminal_TmpMoney where fcode not in(
                                select fcode from t_criminal where fareaCode  in(
                                select fareaCode from t_czy_area where fcode='" + loginUserCode + "' and fflag=2 ))";
                            DataTable dtChk = new CommTableInfoBLL().GetDataTable(strCheckSql);
                            if (dtChk.Rows.Count > 0)
                            {
                                foreach (DataRow row in dtChk.Rows)
                                {
                                    strCheckResult = strCheckResult +"[" + row[1].ToString() + "],";
                                }
                                return Content("Err|下面这些人您没有管理权限:" + strCheckResult);
                            } 
                            #endregion

                            if (strAddResult=="1")
                            {
                                tspan = DateTime.Now - sdt;
                                return Content(new T_CriminalBLL().PLExcelChangeEduImport(strLoginName) + "。用时:" + tspan.TotalSeconds.ToString());

                            }
                            else
                            {
                                return Content("Err|" + strAddResult);
                            }
                        }
                        catch (Exception e)
                        {
                            return Content("Err|" + e.ToString());
                        }
                        
                        #endregion

                        
                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        //标准金额模板
        private DataRow ReadExcelStandardTemplate(NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
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
                decimal FMoney = 0;  //队别
                try
                {
                    FMoney = Convert.ToDecimal(row.GetCell(2).NumericCellValue);
                }
                catch { }        

                string FRemark = "";  //备注
                try
                {
                    FRemark = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }

                try
                {//如果金额有
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["FCode"] = FCode;
                        drTemp["FName"] = FName;
                        drTemp["FMoney"] = FMoney;
                        drTemp["FRemark"] = FRemark;
                        dtUserAdd.Rows.Add(drTemp);
                        Log4NetHelper.logger.Info("Excel导入,操作员：" + Session["loginUserName"].ToString() + ",导入存款消费调整额度，ID=" + FCode + ",用户名为：" + FName + ",变更金额为：" + FMoney + ",事由为：" + FRemark + "");
                    }
                }
                catch
                { }
            }


            //将DataTabe dtUserAdd 写入到数据库中
            string strSql ;
            strSql=(@" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'T_Criminal_TmpMoney') AND type in (N'U'))
                DROP TABLE T_Criminal_TmpMoney; ");

            new CommTableInfoBLL().ExecSql(strSql);
            strSql=(@"CREATE TABLE [dbo].[T_Criminal_TmpMoney](
	                            [FCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FMoney] [numeric](18, 2) NULL DEFAULT (0),	                            
	                            [FRemark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL
                            ) ON [PRIMARY]; ");
            new CommTableInfoBLL().ExecSql(strSql);
            return drTemp;
        }

        
        private DataRow ReadExcelPersonInfoByBZ(NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
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
                string FSex = "男";  //队别
                try
                {
                    FSex = Convert.ToString(row.GetCell(2).StringCellValue);
                }
                catch { }

                string FAreaName = "";  //队别
                try
                {
                    FAreaName = Convert.ToString(row.GetCell(3).StringCellValue);
                }
                catch { }
                string FCyName = "";  //处遇
                try
                {
                    FCyName = Convert.ToString(row.GetCell(4).StringCellValue);
                }
                catch { }
                string FIdenNo = "";  //身份证号码
                try
                {
                    FIdenNo = Convert.ToString(row.GetCell(5).StringCellValue);
                }
                catch { }

                string FRemark = "";  //开始日期
                try
                {
                    FRemark = Convert.ToString(row.GetCell(6).StringCellValue);
                }
                catch { }


                try
                {//如果金额有
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["FCode"] = FCode;
                        drTemp["FName"] = FName;
                        drTemp["FSex"] = FSex;
                        drTemp["FAreaName"] = FAreaName;
                        drTemp["FCyName"] = FCyName;
                        drTemp["FIdenNo"] = FIdenNo;
                        drTemp["Remark"] = FRemark;
                        dtUserAdd.Rows.Add(drTemp);

                        Log4NetHelper.logger.Info("Excel导入,操作员：" + Session["loginUserName"].ToString() + ",修改一个用户信息，ID=" + FCode + ",用户名为：" + FName + ",处遇为：" + FCyName + ",队别名为：" + FAreaName + "");

                    }
                }
                catch
                { }


            }


            //将DataTabe dtUserAdd 写入到数据库中
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Drop Table T_Criminal_MyTmp;");
            strSql.Append(@"CREATE TABLE [dbo].[T_Criminal_MyTmp](
	                            [FCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FSex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	                            [FAreaName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	                            [FCyName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	                            [FIdenNo] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	                            [Remark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
                             CONSTRAINT [PK_T_Criminal_MyTmp] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY];");
            new CommTableInfoBLL().ExecSql(strSql.ToString());
            return drTemp;
        }

        private DataRow ReadExcelPersonInfoByFZJY(NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
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

                string FSex = "男";  //队别
                try
                {
                    FSex = Convert.ToString(row.GetCell(2).StringCellValue);
                }
                catch { }

                string FAddr = Convert.ToString(checkValue(row.GetCell(3))); //地址
                


                string FCrimeCode = "";//犯罪代码
                try
                {
                    iType = row.GetCell(4).CellType;
                }
                catch
                {
                    break;
                }
                
                if (iType == 0)
                {
                    FCrimeCode = Convert.ToString(row.GetCell(4).NumericCellValue);//数字型 excel列名【名称不能变,否则就会出错】
                }
                else
                {
                    FCrimeCode = row.GetCell(4).StringCellValue;//文本型 excel列名【名称不能变,否则就会出错】
                }


                string FCyName = Convert.ToString(checkValue(row.GetCell(5))); //处遇级别

                string FTerm = "";  //刑期
                try
                {
                    FTerm = Convert.ToString(row.GetCell(6).StringCellValue);
                }
                catch { }

                string FInDate = Convert.ToString(checkValue(row.GetCell(7))); //入监日期

                string FOuDate = Convert.ToString(checkValue(row.GetCell(8))); //出监日期

                string FAreaName = Convert.ToString(checkValue(row.GetCell(9))); ////队别

                string FIdenNo = Convert.ToString(checkValue(row.GetCell(10))); //身份证号码

                string FRemark = Convert.ToString(checkValue(row.GetCell(11))); //备注
                


                try
                {//如果金额有
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["FCode"] = FCode;
                        drTemp["FName"] = FName;
                        drTemp["FSex"] = FSex;
                        drTemp["FAddr"] = FAddr;
                        drTemp["FCrimeCode"] = FCrimeCode;
                        drTemp["FCyName"] = FCyName;
                        drTemp["FTerm"] = FTerm;
                        drTemp["FInDate"] = FInDate;
                        drTemp["FOuDate"] = FOuDate;
                        drTemp["FAreaName"] = FAreaName;
                        drTemp["FIdenNo"] = FIdenNo;
                        drTemp["Remark"] = FRemark;
                        dtUserAdd.Rows.Add(drTemp);

                        Log4NetHelper.logger.Info("Excel导入,操作员：" + Session["loginUserName"].ToString() + ",修改一个用户信息，ID=" + FCode + ",用户名为：" + FName + ",处遇为：" + FCyName + ",队别名为：" + FAreaName + "");

                    }
                }
                catch
                { }


            }


            //将DataTabe dtUserAdd 写入到数据库中
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Drop Table T_Criminal_MyTmp;");
            strSql.Append(@"CREATE TABLE [dbo].[T_Criminal_MyTmp](
	                            [FCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FSex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
                                [FAddr] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
                                [FCrimeCode] [varchar](200)  NULL,
                                [FCyName] [varchar](200) NULL,
                                [FTerm] [varchar](200)  NULL,
                                [FInDate] [varchar](200)  NULL,
                                [FOuDate] [varchar](200)  NULL,
	                            [FAreaName] [varchar](50)  NULL,
	                            [FIdenNo] [varchar](50)  NULL,
	                            [Remark] [varchar](100)  NULL,
                             CONSTRAINT [PK_T_Criminal_MyTmp] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY];");
            new CommTableInfoBLL().ExecSql(strSql.ToString());
            return drTemp;
        }

        private DataRow ReadExcelPersonInfoByZZJY(NPOI.SS.UserModel.ISheet sheet, int rows, DataTable dtUserAdd, DataRow drTemp)
        {
            for (int i = 1; i <= rows; i++)
            {
                //编号,姓名,性别,队别,处遇,身份证号,罪名,案号,备注
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

                string FSex = "男";  //队别
                try
                {
                    FSex = Convert.ToString(row.GetCell(2).StringCellValue);
                }
                catch { }

                string FAreaName = Convert.ToString(checkValue(row.GetCell(3))); //队别

                string FCyName = Convert.ToString(checkValue(row.GetCell(4))); //处遇级别   
                string FIdenNo = Convert.ToString(checkValue(row.GetCell(5))); //身份证号码
                string FCrimeName = Convert.ToString(checkValue(row.GetCell(6))); //犯罪名称

                string FAnHao = Convert.ToString(checkValue(row.GetCell(7))); //案号
                string FRemark = Convert.ToString(checkValue(row.GetCell(8))); //备注
                //编号,姓名,性别,队别,处遇,身份证号,罪名,案号,备注
                try
                {//如果金额有
                    if (FCode != "")
                    {
                        drTemp = dtUserAdd.NewRow();
                        drTemp["FCode"] = FCode;
                        drTemp["FName"] = FName;
                        drTemp["FSex"] = FSex;
                        drTemp["FAreaName"] = FAreaName;
                        drTemp["FCyName"] = FCyName;
                        drTemp["FIdenNo"] = FIdenNo;
                        drTemp["FCrimeName"] = FCrimeName;
                        drTemp["FAnHao"] = FAnHao;              
                        drTemp["Remark"] = FRemark;
                        dtUserAdd.Rows.Add(drTemp);

                        Log4NetHelper.logger.Info("Excel导入,操作员：" + Session["loginUserName"].ToString() + ",修改一个用户信息，ID=" + FCode + ",用户名为：" + FName + ",处遇为：" + FCyName + ",队别名为：" + FAreaName + "");

                    }
                }
                catch
                { }


            }


            //将DataTabe dtUserAdd 写入到数据库中
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" Drop Table T_Criminal_MyTmp;");
            strSql.Append(@"CREATE TABLE [dbo].[T_Criminal_MyTmp](
	                            [FCode] [varchar](20) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FName] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	                            [FSex] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
                                [FAreaName] [varchar](50)  NULL,
                                [FCyName] [varchar](200) NULL,
                                [FIdenNo] [varchar](50)  NULL,
                                [FCrimeName] [varchar](200)  NULL,
                                [FAnHao] [varchar](200)  NULL,
	                            [Remark] [varchar](100)  NULL,
                             CONSTRAINT [PK_T_Criminal_MyTmp] PRIMARY KEY CLUSTERED 
                            (
	                            [FCode] ASC
                            )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
                            ) ON [PRIMARY];");
            new CommTableInfoBLL().ExecSql(strSql.ToString());
            return drTemp;
        }

        private static DataTable SetPersonInfoByBZ()
        {
            DataTable dtUserAdd;
            dtUserAdd = new DataTable();
            dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FSex", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FAreaName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FCyName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FIdenNo", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("Remark", typeof(string)));
            return dtUserAdd;
        }


        private static DataTable SetPersonInfoByFZJY()
        {
            DataTable dtUserAdd;
            dtUserAdd = new DataTable();
            dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FSex", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FAddr", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FCrimeCode", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FCyName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FTerm", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FInDate", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FOuDate", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FAreaName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FIdenNo", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("Remark", typeof(string)));
            return dtUserAdd;
        }

        private static DataTable SetPersonInfoByZZJY()
        {
            //编号,姓名,性别,队别,处遇,身份证号,罪名,案号,备注
            DataTable dtUserAdd;
            dtUserAdd = new DataTable();
            dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FSex", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FAreaName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FCyName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FIdenNo", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FCrimeName", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("FAnHao", typeof(string)));
            dtUserAdd.Columns.Add(new DataColumn("Remark", typeof(string)));
            return dtUserAdd;
        }


        //检查单元格的输出值
        private static object checkValue(NPOI.SS.UserModel.ICell cell)
        {
            object returnValue = "";
            if (cell == null)
            {
                returnValue = "";
            }
            else
            {
                //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)***
                switch (cell.CellType)
                {
                    case NPOI.SS.UserModel.CellType.Blank:
                        returnValue = "";
                        break;
                    case NPOI.SS.UserModel.CellType.Numeric:
                        short format = cell.CellStyle.DataFormat;
                        //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理***
                        if (format == 14 || format == 31 || format == 57 || format == 58 || format == 20)
                            returnValue = cell.DateCellValue.ToString();
                        else
                            returnValue = cell.NumericCellValue;
                        break;
                    case NPOI.SS.UserModel.CellType.String:
                        returnValue = cell.StringCellValue;
                        break;
                }
            }
            return returnValue;
        }

        public ActionResult ExcelSearch()//Excel批量查询名单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            if (Request.Files.Count > 0)
            {
                //string strFBid = Request["excelBid"];
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
                    //HSSFWorkbook workbook = new HSSFWorkbook(stream);//2003版

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
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {
                        #region 定义DataTable
                        DataTable dtUserAdd = new DataTable();
                        dtUserAdd.Columns.Add(new DataColumn("FCode", typeof(string)));
                        DataRow drTemp = null;
                        #endregion

                        string strFCodes = "";
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
                            #region Excel行写入到DataTabel中
                            try
                            {//如果金额有
                                if (FCode != "")
                                {
                                    if (strFCodes == "")
                                    {
                                        strFCodes = "'" + FCode + "'";
                                    }
                                    else
                                    {
                                        strFCodes = strFCodes + ",'" + FCode + "'";
                                    }

                                }

                            }
                            catch
                            { }
                            #endregion
                        }

                        string strWhere = " FCode in(" + strFCodes + ")";
                        StringBuilder strSql;
                        string title;
                        string result;
                        SetSumOrderWhereSql(1, 1, strWhere, out strSql, out title, out result);

                        if (result != "")//Err|对不起,您传入错误的参数了
                        {
                            return Content(result);
                        }
                        DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
                        string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_Criminalinfo.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                        //ExcelRender.RenderToExcel(dt, context, strFileName);
                        ExcelRender.RenderToExcel(dt, strFileName);
                        return Content("OK|" + strLoginName + "_Criminalinfo.xls");
                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }



        #region 营养餐功能管理
        //获取用户营养餐特批记录
        public ActionResult GetUsersTP_YYCList()
        {
            string fcode = Request["fcode"];
            //fifoFlag =0 表示有效的记录
            List<T_Criminal_TPList> lists = new T_Criminal_TPListBLL().GetModelList("FCode='" + fcode + "' and isnull(FifoFlag,0)=0");
            string sss = "{\"total\":" + lists.Count.ToString() + ",\"rows\":" + jss.Serialize(lists) + "}";
            return Content(sss);
        }

        //增加用户营养餐特批记录
        public ActionResult AddTPList()
        {
            string id = Request["id"];
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            string TPMoney = Request["TPMoney"];
            string EffectiveDate = Request["EffectiveDate"];
            string Remark = Request["Remark"];
            string MoneyUseFlag = Request["MoneyUseFlag"];
            string SrcFileName = "";
            if (Request.Files.Count > 0)
            {
                //string strFBid = Request["excelBid"];
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
                    string savePath = Server.MapPath("~/Upload/Yyc_TPFile/" + fname);
                    f.SaveAs(savePath);
                    SrcFileName = "/Upload/Yyc_TPFile/" + fname;
                }

            }

            if (string.IsNullOrEmpty(FCode) == true)
            {
                return Content("Error|用户编号不能为空");
            }
            if (string.IsNullOrEmpty(FName) == true)
            {
                return Content("Error|用户姓名不能为空");
            }
            if (string.IsNullOrEmpty(TPMoney) == true)
            {
                return Content("Error|特批金额不能为空");
            }
            if (string.IsNullOrEmpty(Remark) == true)
            {
                return Content("Error|特批原因不能为空");
            }
            T_Criminal criminal = new T_CriminalBLL().GetModel(FCode);
            if (criminal.FName != FName)
            {
                return Content("Error|提交的用户名[" + FName + "]与[" + criminal.FName + "]不一致");
            }
            if (criminal.fflag == 1)
            {
                return Content("Error|该犯已经离监了，不必调整");
            }
            T_Criminal_TPList tplist = new T_Criminal_TPList();
            tplist.FCode = FCode;
            tplist.FName = FName;
            tplist.TPMoney = Convert.ToDecimal(TPMoney);
            tplist.Remark = Remark;
            tplist.EffectiveDate = Convert.ToDateTime(EffectiveDate);
            tplist.CrtBy = Session["loginUserName"].ToString();
            tplist.CrtDate = DateTime.Now;
            tplist.FifoFlag = 0;
            tplist.MoneyUseFlag = 1;
            if (string.IsNullOrEmpty(MoneyUseFlag) == true)
            {
                tplist.MoneyUseFlag = 0;
            }

            tplist.SrcFileName = SrcFileName;
            tplist.DelBy = "";
            tplist.DelDate = "";
            try
            {
                new T_Criminal_TPListBLL().Add(tplist);
                new CommTableInfoBLL().ExecSql("update t_Criminal set TP_YingYangCan_Money=" + TPMoney + " where fcode='" + FCode + "'");
                Log4NetHelper.logger.Info("增加营养餐特批记录,操作员：" + Session["loginUserName"].ToString() + ",ID=" + tplist.id + ",编号为：" + tplist.FCode + ",用户名为：" + tplist.FName + ",特批金额：" + tplist.TPMoney + ",特批原因为：" + tplist.Remark + "");

                return Content("OK|添加成功");
            }
            catch (Exception e)
            {
                return Content("Err|添加失败" + e.ToString());
            }

        }
        //删除用户营养餐特批记录
        public ActionResult DelTPList()
        {
            string id = Request["id"];
            string fcode = Request["fcode"];
            if (string.IsNullOrEmpty(fcode))
            {
                return Content("Err|用户编号不能为空");
            }
            T_Criminal_TPList tplist = new T_Criminal_TPListBLL().GetModel(Convert.ToInt32(id));

            if (tplist.FCode != fcode)
            {
                return Content("Err|用户编号不正确");
            }

            tplist.FifoFlag = 1;
            tplist.DelBy = Session["loginUserName"].ToString();
            tplist.DelDate = DateTime.Now.ToString();

            if (new T_Criminal_TPListBLL().Update(tplist))
            {
                if (tplist.EffectiveDate >= DateTime.Today)
                {
                    new CommTableInfoBLL().ExecSql("update t_Criminal set TP_YingYangCan_Money=0  where fcode='" + tplist.FCode + "'");
                }

                Log4NetHelper.logger.Info("删除营养餐特批记录,操作员：" + Session["loginUserName"].ToString() + ",ID=" + tplist.id + ",编号为：" + tplist.FCode + ",用户名为：" + tplist.FName + ",特批金额：" + tplist.TPMoney + ",特批原因为：" + tplist.Remark + "");

                return Content("OK|删除成功");
            }
            else
            {
                return Content("Err|删除失败");
            }
        }
        #endregion



        //扣入监所包
        public ActionResult btnRSB_Koukuan()
        {
            string fcrimecode = Request["FCrimeCode"];
            if (string.IsNullOrEmpty(fcrimecode))
            {
                return Content("Err|学员编号不能为空");
            }

            string crtby = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;



            string rst = new T_CriminalBLL().Rsb_KouKuan(fcrimecode, crtby);
            return Content(rst);

        }




        #region 人员信息变更管理

        //人员信息变更界面
        public ActionResult ChangeIndex(int id = 1)
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            List<T_CY_TYPE> cys = new T_CY_TYPEBLL().GetModelList("");
            ViewData["cys"] = cys;

            List<T_CRIME> crimes = new T_CRIMEBLL().GetModelList("");
            ViewData["crimes"] = crimes;

            DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from t_Vcrd Order by CrtBy");
            List<string> crtbys = new List<string>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    crtbys.Add(row[0].ToString());
                }
            }
            ViewData["crtbys"] = crtbys;


            dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where damount<>0 Order by Dtype");
            List<string> cashtypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cashtypes.Add(row[0].ToString());
                }
            }
            ViewData["cashtypes"] = cashtypes;

            dt = new CommTableInfoBLL().GetDataTable("select distinct Dtype from t_Vcrd where camount<>0 Order by Dtype");
            List<string> paytypes = new List<string>();//存款类型
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    paytypes.Add(row[0].ToString());
                }
            }
            ViewData["id"] = id;
            ViewData["paytypes"] = paytypes;
            return View();
        }

        public ActionResult GetChangeList()
        {
            string FCode = Request["FCode"];
            string endFCode = Request["endFCode"];
            string FName = Request["FName"];
            string changeType = Request["changeType"];
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaCode = Request["areaCode"];
            string crtby = Request["crtby"];
            string CriminalFlag = Request["CriminalFlag"];
            string auditFlag = Request["auditFlag"];


            string strWhere = "1=1 ";
            if (string.IsNullOrEmpty(FCode) == false)
            {
                if (string.IsNullOrEmpty(endFCode) == false)
                {
                    strWhere = strWhere + " and fcode between '" + FCode + "'" + " and '" + endFCode + "'";
                }
                else
                {
                    strWhere = strWhere + " and fcode='" + FCode + "'";
                }
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FName like '%" + FName + "%'";
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

            if (string.IsNullOrEmpty(areaCode) == false)
            {
                strWhere = strWhere + " and FCode in(select fcode from t_Criminal where FAreaCode='" + areaCode + "')";
            }

            if (string.IsNullOrEmpty(crtby) == false)
            {
                strWhere = strWhere + " and crtby= '" + crtby + "'";
            }

            if (string.IsNullOrEmpty(CriminalFlag) == false)
            {
                strWhere = strWhere + " and FCode in(select fcode from t_Criminal where isnull(fflag,0)=" + CriminalFlag + ")";
            }

            if (string.IsNullOrEmpty(auditFlag) == false)
            {
                strWhere = strWhere + " and auditFlag= '" + auditFlag + "'";
            }
            List<T_Criminal_ChangeList> lists;

            //if (string.IsNullOrEmpty(auditFlag))
            //{
            //    lists = new T_Criminal_ChangeListBLL().GetModelList("isnull(AuditFlag,0)<9");
            //}
            //else
            //{
            //    lists = new T_Criminal_ChangeListBLL().GetModelList("isnull(AuditFlag,0)=" + auditFlag);
            //}

            lists = new T_Criminal_ChangeListBLL().GetModelList(strWhere);

            return Content(jss.Serialize(lists));
        }

        public ActionResult GetUserList()
        {
            string addSchFCode = Request["addSchFCode"];
            string addSchFName = Request["addSchFName"];
            string addSchFArea = Request["addSchFArea"];
            string addSchFCy = Request["addSchFCy"];
            string addSchFFlag = Request["addSchFFlag"];


            string strWhere = "1=1 ";

            //验证用户的队别,如果设定了Vcrd验证用户队别，则要查看是否有相应的队别权限下的犯人才可以查询到
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    strWhere = " FAreaCode in (  select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')";
                }
            }
            if (!string.IsNullOrEmpty(addSchFCode))
            {
                strWhere = strWhere + " and fcode='" + addSchFCode + "'";
            }
            if (!string.IsNullOrEmpty(addSchFName))
            {
                strWhere = strWhere + " and fname like '%" + addSchFName + "%'";
            }
            if (!string.IsNullOrEmpty(addSchFArea))
            {
                strWhere = strWhere + " and FAreaCode='" + addSchFArea + "'";
            }
            if (!string.IsNullOrEmpty(addSchFCy))
            {
                strWhere = strWhere + " and FCyCode='" + addSchFCy + "'";
            }
            if (!string.IsNullOrEmpty(addSchFFlag))
            {
                strWhere = strWhere + " and isnull(fflag,0)='" + addSchFFlag + "'";
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

            listRows = new T_CriminalBLL().GetCriminalsCount(strWhere.ToString());

            List<T_Criminal> criminals = new T_CriminalBLL().GetPageCriminalExtInfo(page, row, strWhere, "FCode");

            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(criminals) + "}";
            return Content(sss);


        }

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
                    List<T_Criminal_ChangeList> models = SetCriminalChangeInfo(ja, crtby);

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
                    List<T_Criminal_ChangeList> models = SetCriminalChangeInfo(ja, crtby);
                    return Content("OK|保存成功！");
                }
            }


            return Content("");
        }

        private static List<T_Criminal_ChangeList> SetCriminalChangeInfo(JArray ja, string crtby)
        {
            T_Criminal_ChangeList model = new T_Criminal_ChangeList();
            DateTime dt = DateTime.Now;

            foreach (JObject o in ja)
            {
                model = new T_Criminal_ChangeList()
                {
                    FCode = o["FCode"].ToString(),
                    FName = o["FName"].ToString(),
                    ChangeType = o["ChangeType"].ToString(),
                    ChangeTypeName = o["ChangeTypeName"].ToString(),
                    OldCode = o["OldCode"].ToString(),
                    OldName = o["OldName"].ToString(),
                    NewCode = o["NewCode"].ToString(),
                    NewName = o["NewName"].ToString(),
                    ChangeInfo = o["ChangeInfo"].ToString(),
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
                    T_SHO_ManagerSet cyMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByCy");
                    if (cyMset != null)
                    {
                        if (cyMset.MgrValue == "1")
                        {
                            model.AuditArea = "";
                            model.AuditBy = "";
                            model.AuditDate = DateTime.Now;
                            model.AuditFlag = 0;
                            model.AuditInfo = "";
                            new T_Criminal_ChangeListBLL().Add(model, 1);
                        }
                        else
                        {
                            new T_Criminal_ChangeListBLL().Add(model);
                            new CommTableInfoBLL().ExecSql("update t_Criminal set FCyCode='" + model.NewCode + "' where fcode='" + model.FCode + "'");
                        }
                    }
                    else
                    {
                        new T_Criminal_ChangeListBLL().Add(model);
                        new CommTableInfoBLL().ExecSql("update t_Criminal set FCyCode='" + model.NewCode + "' where fcode='" + model.FCode + "'");
                    }

                }
                else if (model.ChangeType == "2")//队别变更
                {
                    T_SHO_ManagerSet areaMset = new T_SHO_ManagerSetBLL().GetModel("criminalChangeSetByArea");
                    if (areaMset != null)
                    {
                        if (areaMset.MgrValue == "1")
                        {
                            model.AuditArea = "";
                            model.AuditBy = "";
                            model.AuditDate = DateTime.Now;
                            model.AuditFlag = 0;
                            model.AuditInfo = "";
                            new T_Criminal_ChangeListBLL().Add(model, 1);
                        }
                        else
                        {
                            new T_Criminal_ChangeListBLL().Add(model);
                            new CommTableInfoBLL().ExecSql("update t_Criminal set FAreaCode='" + model.NewCode + "' where fcode='" + model.FCode + "'");
                        }
                    }
                    else
                    {
                        new T_Criminal_ChangeListBLL().Add(model);
                        new CommTableInfoBLL().ExecSql("update t_Criminal set FAreaCode='" + model.NewCode + "' where fcode='" + model.FCode + "'");
                    }
                }
                else
                {
                    new T_Criminal_ChangeListBLL().Add(model);
                }

            }
            List<T_Criminal_ChangeList> models = new T_Criminal_ChangeListBLL().GetModelList("Crtby='" + crtby + "' and crtDate>='" + dt.ToString() + "'");
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
                List<T_Criminal_ChangeList> models = new T_Criminal_ChangeListBLL().GetModelList("Seqno in(" + seqnos + ")");

                foreach (T_Criminal_ChangeList m in models)
                {
                    if (m.AuditFlag < 9)
                    {
                        m.AuditDate = DateTime.Now;
                        m.AuditFlag = Convert.ToInt32(auditFlag);
                        m.AuditInfo = remark;
                        m.AuditBy = crtby;
                        m.AuditArea = czy.FUserArea;
                        new T_Criminal_ChangeListBLL().Update(m);
                        T_Criminal crm = new T_CriminalBLL().GetModel(m.FCode);
                        if (m.AuditFlag == 9)
                        {
                            if (m.ChangeType == "1")
                            {
                                new CommTableInfoBLL().ExecSql("update t_Criminal set FCyCode='" + m.NewCode + "' where fcode='" + m.FCode + "'");
                            }
                            else if (m.ChangeType == "2")
                            {
                                new CommTableInfoBLL().ExecSql("update t_Criminal set FAreaCode='" + m.NewCode + "' where fcode='" + m.FCode + "'");
                            }
                        }
                    }
                }

                models = new T_Criminal_ChangeListBLL().GetModelList("Seqno in(" + seqnos + ")");
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
            string FCode = Request["FCode"];
            string endFCode = Request["endFCode"];
            string FName = Request["FName"];
            string changeType = Request["changeType"];
            string startTime = Request["StartDate"];
            string endTime = Request["EndDate"];
            string areaCode = Request["FAreaName"];
            string crtby = Request["FCrtBy"];
            string CriminalFlag = Request["FCriminalFlag"];
            string auditFlag = Request["schAuditFlag"];


            string strWhere = "where 1=1 ";
            if (string.IsNullOrEmpty(FCode) == false)
            {
                if (string.IsNullOrEmpty(endFCode) == false)
                {
                    strWhere = strWhere + " and fcode between '" + FCode + "'" + " and '" + endFCode + "'";
                }
                else
                {
                    strWhere = strWhere + " and fcode='" + FCode + "'";
                }
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and like '%" + FName + "%'";
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

            if (string.IsNullOrEmpty(areaCode) == false)
            {
                strWhere = strWhere + " and FCode in(select fcode from t_Criminal where FAreaCode='" + areaCode + "')";
            }

            if (string.IsNullOrEmpty(crtby) == false)
            {
                strWhere = strWhere + " and crtby= '" + crtby + "'";
            }

            if (string.IsNullOrEmpty(CriminalFlag) == false)
            {
                strWhere = strWhere + " and FCode in(select fcode from t_Criminal where isnull(fflag,0)=" + CriminalFlag + ")";
            }

            if (string.IsNullOrEmpty(auditFlag) == false)
            {
                strWhere = strWhere + " and auditFlag= '" + auditFlag + "'";
            }


            string strSql = @"SELECT [Seqno] 序号
                  ,[FCode] 编号
                  ,[FName] 姓名
                  ,[ChangeTypeName] 变更类型
                  ,[OldName] 变更前
                  ,[NewName] 变更后
                  ,[ChangeInfo] 变更原因
                  ,[CrtBy] 申请人
                  ,[CrtDate] 申请日期
                  ,[AuditBy] 审核人
                  ,[AuditArea] 审核部门
                  ,[AuditDate] 审核日期
                  ,[AuditInfo] 审核意见
                  ,case [AuditFlag] when 9 then '同意' when 10 then '拒绝' else '待审核' end 审核结果
                  ,[Remark] 备注
              FROM [T_Criminal_ChangeList] " + strWhere;


            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql);
            string strFileName = new CommonClass().GB2312ToUTF8("CriminalChangeList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|CriminalChangeList.xls");
        }

        #endregion


        public ActionResult AddOutBankInfo()
        {
            string fOutUserCode = base.Request["FOutUserCode"];
            string outBankCard = base.Request["OutBankCard"];
            string bankOrgName = base.Request["BankOrgName"];
            string bankCNAPS = base.Request["BankCNAPS"];
            string bankUserName = base.Request["BankUserName"];
            string openingBank = base.Request["OpeningBank"];
            string outBankRemark = base.Request["OutBankRemark"];
            string crtby = (base.Request.Cookies["loginUserCode"] == null) ? "" : base.Request.Cookies["loginUserCode"].Value;
            T_Criminal_OutBankAccount model = new PayeeAccountService().GetModelFirst<T_Criminal_OutBankAccount, T_Criminal_OutBankAccount>("{\"FCrimecode\":\"" + fOutUserCode + "\"}");
            if (model != null)
            {
                model.OutBankCard = outBankCard;
                model.BankUserName = bankUserName;
                model.OpeningBank = openingBank;
                model.BankCNAPS = bankCNAPS;
                model.OutBankRemark = outBankRemark;
                model.Flag = 0;
                model.BankOrgName = bankOrgName;
                model.ModifyBy = crtby;
                model.ModifyTime = new DateTime?(DateTime.Now);
                new PayeeAccountService().Update<T_Criminal_OutBankAccount>(model);
                return base.Content("OK|保存成功");
            }
            model = new T_Criminal_OutBankAccount
            {
                FCrimecode = fOutUserCode,
                OutBankCard = outBankCard,
                BankUserName = bankUserName,
                OpeningBank = openingBank,
                BankCNAPS = bankCNAPS,
                OutBankRemark = outBankRemark,
                CrtBy = crtby,
                CrtDate = DateTime.Now,
                Flag = 0,
                BankOrgName = bankOrgName,
                ModifyBy = ""
            };
            if (new PayeeAccountService().Insert<T_Criminal_OutBankAccount>(model) != null)
            {
                return base.Content("OK|插入成功");
            }
            return base.Content("Err|插入失败");
        }
        public ActionResult GetOutBankInfo()
        {
            string fOutUserCode = base.Request["FOutUserCode"];
            T_Criminal_OutBankAccount model = new PayeeAccountService().GetModelFirst<T_Criminal_OutBankAccount, T_Criminal_OutBankAccount>("{\"FCrimecode\":\"" + fOutUserCode + "\"}");
            if (model != null)
            {
                return base.Content("OK|" + this.jss.Serialize(model));
            }
            return base.Content("Err|插入失败");
        }
        public ActionResult GetPagelistByBankOpenName(string q)
        {
            string[] queryArr = q.Split(new char[]
            {
                ' '
            });
            List<T_Bank_CNAPS> ss = new T_Bank_CNAPSBLL().GetPagelistByBankOpenName(queryArr).rows;
            return base.Content(this.jss.Serialize(ss));
        }
        [MyLogActionFilter]
        public JsonResult DistributeNewCard(string fcrimecode)
        {
            ResultInfo rs = new T_Bank_CardPoolBLL().DistributeNewCard(fcrimecode);
            return base.Json(rs, JsonRequestBehavior.AllowGet);
        }
        [MyLogActionFilter]
        public JsonResult BatchDistributeNewCard()
        {
            string arg_1A_0 = base.Request.Cookies["loginUserName"].Value;
            string czyCode = base.Request.Cookies["loginUserCode"].Value;
            if (new T_CZYBLL().GetModel(czyCode).FPRIVATE != 1)
            {
                ResultInfo rs = new ResultInfo
                {
                    Flag = false,
                    ReMsg = "您没有权限批量分配，请与管理员联系!",
                    DataInfo = null
                };
                return base.Json(rs, JsonRequestBehavior.AllowGet);
            }
            return base.Json(new T_Bank_CardPoolBLL().BatchDistributeNewCard(), JsonRequestBehavior.AllowGet);
        }
    }
}