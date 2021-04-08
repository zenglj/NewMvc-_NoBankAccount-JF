using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.CommonHeler;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [CustomActionFilterAttribute]

    public class BankRcvController : Controller
    {

        JavaScriptSerializer jss = new JavaScriptSerializer();
        string userLoginName = "";
        //
        // GET: /DepositList/
        public ActionResult Index()
        {
            return View();
        }


        private string GetUserLoginName()
        {
            if (string.IsNullOrWhiteSpace(userLoginName))
            {
                userLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            }
            return userLoginName;
        }

        //按姓名查找
        public JsonResult FindFCrimeCode(string fname)
        {
            ResultInfo rs = new ResultInfo() { 
                Flag=false,
                ReMsg="未处理",
                DataInfo=null
            };
            if (string.IsNullOrWhiteSpace(fname))
            {
                rs.ReMsg = "姓名不能为空";
                return Json(rs);
            }
            List<T_Criminal> ls = new BaseDapperBLL().GetModelList<T_Criminal, T_Criminal>( jss.Serialize(new { FName = fname,fflag=0 }), "FCode asc", 5);
            if (ls.Count == 1)
            {
                rs.Flag = true;
                rs.ReMsg = "成功";
                rs.DataInfo = ls[0].FCode;
            }
            else if(ls.Count > 1)
            {
                rs.Flag = false;
                rs.ReMsg = "有重名的记录";
            }
            else
            {
                rs.Flag = false;
                rs.ReMsg = "没有找到相应的记录";
            }
            return Json(rs);
        }

        //
        // GET: /DepositList/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DepositList/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /DepositList/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                string id = collection["Id"];
                string userName = collection["userName"];
                string password = collection["password"];

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /DepositList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /DepositList/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        [HttpPost]
        public JsonResult GetSearchList(string strJsonWhere, string orderField = " id asc ", int page = 1, int rows = 10)
        {
            PageResult<T_Bank_Rcv> rs = new T_Bank_DepositListBLL().GetPageList<T_Bank_Rcv, T_Bank_Rcv_Search>(orderField, strJsonWhere, page, rows);
            return Json(rs);
            //return Content(jss.Serialize(rs));
        }

        [HttpPost]
        public JsonResult GetListSumAmount(string strJsonWhere)
        {
            decimal _s = new T_Bank_DepositListBLL().GetListSumAmount<T_Bank_Rcv, T_Bank_Rcv_Search>("rcvamount", strJsonWhere, "");
            ResultInfo rs = new ResultInfo()
            {
                Flag = true,
                ReMsg = "成功",
                DataInfo = _s
            };
            return Json(rs);
            //return Content(jss.Serialize(rs));
        }

        [HttpPost]
        public JsonResult CheckRcvFName(string fcrimename, string vchnum)
        {
            ResultInfo rs = new ResultInfo()
            {
                Flag = false,
                ReMsg = "不包含",
                DataInfo = null
            };
            var search = new { VchNum = vchnum };
            T_Bank_Rcv rcv = new T_Bank_DepositListBLL().GetModelFirst<T_Bank_Rcv, T_Bank_Rcv_Search>(jss.Serialize(search));
            if(rcv.Remark.Contains(fcrimename))
            {
                rs.Flag = true;
                rs.ReMsg = "有包含";
            }
            
            return Json(rs);
            //return Content(jss.Serialize(rs));
        }

        /// <summary>
        /// 手动审核入账
        /// </summary>
        /// <param name="fcrimecode"></param>
        /// <param name="fcrimename"></param>
        /// <param name="remark"></param>
        /// <param name="vchnum"></param>
        /// <param name="checkFlag"></param>
        /// <returns></returns>
        public ActionResult SetBankArtificialAddNotImportRec(string fcrimecode,string fcrimename,string remark, string vchnum,int checkFlag=1)
        {
            ResultInfo rs = new ResultInfo()
            {
                Flag = false,
                ReMsg = "失败",
                DataInfo = null
            };

            T_Criminal criminal = new T_CriminalBLL().GetModel(fcrimecode);
            if (criminal == null)
            {
                rs.Flag = false;
                rs.ReMsg = "犯人编号不存在";
                return Content(jss.Serialize(rs));
            }

            if (criminal.FName != fcrimename)
            {
                rs.Flag = false;
                rs.ReMsg = "犯人编号与姓名不一致";
                return Content(jss.Serialize(rs));
            }
            if (criminal.fflag==1)
            {
                rs.Flag = false;
                rs.ReMsg = "犯人已经离监，无法补录";
                return Content(jss.Serialize(rs));
            }
            var search = new { VchNum = vchnum };
            T_Bank_Rcv rcv = new T_Bank_DepositListBLL().GetModelFirst<T_Bank_Rcv, T_Bank_Rcv_Search>(jss.Serialize(search));
            if (rcv.ImportFlag==1)
            {
                rs.Flag = false;
                rs.ReMsg = "Err|该记录已经入账成功了，不能重复入账";
                return Content(jss.Serialize(rs));
            }
            string strForceText = "";
            if (checkFlag==1)
            {
                if (rcv.RcvAmount >= 0)
                {
                    if (!(rcv.Remark.Contains(fcrimename) || rcv.Remark.Contains(fcrimecode)))
                    {
                        rs.Flag = false;
                        rs.ReMsg = "存款备注里没有这个犯人的姓名或编号";
                        return Content(jss.Serialize(rs));
                    }
                }
                else
                {
                }                
            }
            else
            {
                strForceText = "强制";
            }
            string crtby = Session["loginUserName"].ToString()+"_"+strForceText;
            string _s = new T_Bank_DepositListBLL().SetBankArtificialAddRecForProc(crtby, vchnum,fcrimecode, remark);

            if (_s.StartsWith("OK|"))
            {
                rs.Flag = true;
            }
            else
            {
                rs.Flag = false;
            }
            rs.ReMsg = _s;
            rs.DataInfo = new T_Bank_DepositListBLL().GetModelFirst<T_Bank_Rcv, T_Bank_Rcv_Search>(jss.Serialize(search)); ;

            return Content(jss.Serialize(rs));
            //return Content(jss.Serialize(rs));
        }


        public ActionResult PrintSumList(string strJsonWhere)
        {
            T_Bank_Rcv_Search search=null;
            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                search = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Bank_Rcv_Search>(strJsonWhere);
            }
            List<BankRcvDateSumModel> sumList = new T_Bank_DepositListBLL().GetSumOfFieldList<T_Bank_Rcv, T_Bank_Rcv_Search, BankRcvDateSumModel>("rcvamount", "tnxdate,ImportFlag ", strJsonWhere, "");
            ViewData["title"] = "中银结算卡存款汇总报表";
            if (search == null)
            {
                ViewData["startTime"] ="~";
                ViewData["endTime"] = "~";
            }
            else
            {
                ViewData["startTime"] = search.CreateDate_Start.ToString();
                ViewData["endTime"] = search.CreateDate_End.ToString();
            }
            
            ViewData["sumList"] = sumList;

            return View();
        }

        public ActionResult PrintDetailList(string strJsonWhere)
        {
            T_Bank_Rcv_Search search = null;
            if (!string.IsNullOrWhiteSpace(strJsonWhere))
            {
                search = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Bank_Rcv_Search>(strJsonWhere);
            }
            List<T_Bank_Rcv> sumList = new T_Bank_DepositListBLL().GetModelList<T_Bank_Rcv, T_Bank_Rcv_Search>(strJsonWhere,"Id",10000);
            ViewData["title"] = "中银结算卡存款明细报表";
            if (search == null)
            {
                ViewData["startTime"] = "~";
                ViewData["endTime"] = "~";
            }
            else
            {
                ViewData["startTime"] = search.CreateDate_Start.ToString();
                ViewData["endTime"] = search.CreateDate_End.ToString();
            }

            ViewData["sumList"] = sumList;

            return View();
        }

        /// <summary>
        /// 导出明细记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult ExcelDetailList(string strJsonWhere)
        {
            string strLoginName = this.GetUserLoginName();
            string strWhere = new T_Bank_DepositListBLL().GetParamString<T_Bank_Rcv, T_Bank_Rcv_Search>(strJsonWhere);
            

            StringBuilder strSql= new StringBuilder();
            bool mul_lan = false;
            string title="中银结算卡存款记录";
            strSql.Append(@"select [Id]
                  ,[CardNo] as 银行卡号
                  ,[VchNum] as 流水号
	              ,[fractName] as 汇款人
                  ,[RcvAmount] as 汇款金额
                  ,[tnxdate] as 流水日期
                  ,[Remark] as 备注
	              ,[CreateDate] as 导入日期
                  ,[FcrimeCode] as 犯人编号
                  ,[FName] as 犯人姓名
                  ,[Error] as 错误信息
                  ,[ImportFlag] 导入标志
	              from T_bank_Rcv 
             ");
            if (string.IsNullOrWhiteSpace(strWhere) == false)
            {
                strSql.Append(" where " + strWhere);
            }
            
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string strCountTime = string.Format("统计期间:{0}--{1}", startTime, endTime);

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankRcvList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;

            ExcelRender.RenderToExcel(dt, title, 4, strFileName, mul_lan, strCountTime);
            return Content("OK|" + strLoginName + "_BankRcvList.xls");
        }

        /// <summary>
        /// 月统计报表 期初 期间  期末
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ActionResult ExcelReportDetailList(DateTime startTime, DateTime endTime)
        {
            string strLoginName = this.GetUserLoginName();
            //string strWhere = new T_Bank_DepositListBLL().GetParamString<T_Bank_Rcv, T_Bank_Rcv_Search>(strJsonWhere);


            StringBuilder strSql = new StringBuilder();
            bool mul_lan = false;
            string title = "中银结算卡统计报表";
            strSql.Append(@"exec P_BankRecCountReport '" + startTime.ToString() + "','" + endTime.ToString() + "'");
            //if (string.IsNullOrWhiteSpace(strWhere) == false)
            //{
            //    strSql.Append(" where " + strWhere);
            //}

            //string startTime = Request["startTime"];
            //string endTime = Request["endTime"];
            string strCountTime = string.Format("统计期间:{0}--{1}", startTime, endTime);

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankTotalList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;

            ExcelRender.RenderToExcel(dt, title, 2, strFileName, mul_lan, strCountTime);
            return Content("OK|" + strLoginName + "_BankTotalList.xls");
        }
        public ActionResult HttpTest()
        {

            Encoding myEncoding = Encoding.GetEncoding("utf-8");  //选择编码字符集
            

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.1.87:8018/BankATM/AtmCall");
            //httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentType = "application/xml";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var data = new
                {
                    token = "346bb3b8514a5d16d9fcae72e27c73fc",
                    Action = "GetCardInfo",
                    Data = new
                    {
                        TranDate = "20201014",
                        TranTime = "101024",
                        AtmSerialNo = "2020010055",
                        MacCode = "ATM10",
                        F_AMOUNT = "0",
                        CardCode = "2020030384"
                    }
                };

                streamWriter.Write(jss.Serialize(data));
                streamWriter.Flush();
                streamWriter.Close();
            }
            string result;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            ViewData["result"] = result;
            return View();
        }

        #region  存款记录对账报表

        public ActionResult ExcelNoFindCashReport(DateTime startTime,DateTime endTime)
        {
            try
            {
                string strSTime = startTime.ToString("yyyyMMdd");
                string strETime = endTime.ToString("yyyyMMdd");

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select a.[fractnactacn] as 付款账户   ,a.[fractncardno] as 付款卡号   ,a.[fractnacntname]  as 付款人    ,a.[fractnibkname] as 付款行
              ,a.[toactntoibkn] as 收款行号 ,a.[toactnactacn] as 收款账户 ,a.[toactncardno] as 收款卡号  ,a.[toactntoname]  as 收款人
              ,a.[toactntobank]  as 收款银行  ,a.[vchnum] as 流水号 ,a.[txndate] as 流水日期
              ,a.[txntime] as 流水时间 ,a.[txnamt] as 转账金额 ,a.[acctbal] as 账户余额  ,a.[avlbal] as 可用余额 
              ,a.[useinfo] as 使用信息  ,a.[furinfo] as 附言 ,a.[transtype] as 传送类型
              ,a.[trncur]  as 货币类型  ,a.[direction] as 来往标识  ,a.[valdat] as 日期 ,a.[interinfo] as 中心备注
                from  ");
                strSql.Append(@"(select distinct 
	           [fractnactacn]      ,[fractncardno]      ,[fractnacntname]      ,[fractnibkname]
              ,[toactntoibkn]      ,[toactnactacn]      ,[toactncardno]      ,[toactntoname]
              ,[toactntobank]      ,[vchnum]      ,[transid]      ,[insid]      ,[txndate]
              ,[txntime]      ,[txnamt]      ,[acctbal]      ,[avlbal]      ,[frzamt]
              ,[overdramt]      ,[avloverdramt]      ,[useinfo]      ,[furinfo]      ,[transtype]
              ,[bustype]      ,[trncur]      ,[direction]      ,[valdat]
              ,[vouchtp]      ,[vouchnum]      ,[fxrate]      ,[interinfo]
                 from t_bank_transdetail where txndate between @startTime and @endTime and not vchnum is null --order by vchnum
                ) a left join t_bank_rcv b on a.vchnum=b.VchNum
                where b.VchNum is null and a.direction=1");
                strSql.Append(" and not ( isnull( a.fractnacntname,'') like '%监狱%' or isnull( a.fractnacntname,'') like '%看守所%' or isnull( a.fractnacntname,'') like '%公司%' ) ");
                string strLoginName = this.GetUserLoginName();
                string strCountTime = string.Format("统计期间:{0}--{1}", startTime, endTime);

                SqlParameter[] parameters = {
                    new SqlParameter("@startTime", SqlDbType.VarChar,8),
                    new SqlParameter("@endTime", SqlDbType.VarChar,8)};
                parameters[0].Value = strSTime;
                parameters[1].Value = strETime;

                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString(), parameters);
                string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankRcvList.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;


                ExcelRender.RenderToExcel(dt,strFileName);
                return Content("OK|" + strLoginName + "_BankRcvList.xls");
            }
            catch (Exception e)
            {

                return Content("Error|"+ e.Message.ToString());
            }
            

        }


        /// <summary>
        /// 手动识别流水号
        /// </summary>
        /// <param name="vchnum"></param>
        /// <returns></returns>
        public ActionResult btnCheckVchnum(string vchnum)
        {
            if (string.IsNullOrWhiteSpace(vchnum))
            {
                return Content("Error|流水号不能为空");
            }

            T_Bank_Rcv rcvRec = new BaseDapperBLL().GetModelFirst<T_Bank_Rcv, T_Bank_Rcv>(jss.Serialize(new { VchNum = vchnum }));
            if(rcvRec != null)
            {
                return Content("Error|该流水号的记录已经自动识别过了，无需再识别");
            }
            T_Bank_TransDetail dtlRec = new BaseDapperBLL().GetModelFirst<T_Bank_TransDetail, T_Bank_TransDetail>(jss.Serialize(new { vchnum = vchnum }));

            if (dtlRec== null)
            {
                return Content("Error|没有找到相应的银行记录，请确流水号，或到网银上查验，也有可能是汇款人姓名有特殊的符号无法下载");
            }
            if (dtlRec.furinfo != null)
            {
                if (dtlRec.furinfo.Contains("账号错") || dtlRec.furinfo.Contains("账号误") || dtlRec.furinfo.Contains("账号不存在") || dtlRec.furinfo.Contains("不符"))
                {
                    return Content("Error|包含账号错等信息一般是转账失败的退款记录，不能用于犯人个人入账用");
                }
            }
            

            if (dtlRec.transtype == "11")
            {
                dtlRec.txnamt = 0- dtlRec.txnamt;
            }

            rcvRec = new T_Bank_Rcv
            {
                CardNo = dtlRec.toactncardno,
                CreateDate = Convert.ToDateTime(dtlRec.txndate.Substring(0, 4) + "-" + dtlRec.txndate.Substring(4, 2) + "-" + dtlRec.txndate.Substring(6, 2)),
                Error = "手动识别",
                FCrimeCode = "",
                FName = "",
                ImportFlag = 0,
                OffsetVchNum = dtlRec.vouchnum,
                VchNum = dtlRec.vchnum,
                RcvAmount = dtlRec.txnamt,
                Remark = dtlRec.furinfo,
                tnxdate = dtlRec.txndate,
                transtype = dtlRec.transtype,
                fractName = dtlRec.fractnacntname
            };
            T_Bank_Rcv rstRec= new BaseDapperBLL().Insert<T_Bank_Rcv>(rcvRec);
            return Content("Ok|"+jss.Serialize(rstRec));
        }



        /// <summary>
        /// 导入银行报表，查找那条记录没有
        /// </summary>
        /// <returns></returns>
        public ActionResult ExcelInport()//Excel表格导入
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string strReSaveFlag = Request["reSaveFlag"];
            int id = 1;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LaoBaoModel");
            if (mset != null)
            {
                id = Convert.ToInt32(mset.MgrValue);
            }
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
                //DateTime edt;
                //TimeSpan tspan;
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
                    //int ErrNums = 0;
                    if (rows < 1)
                    {
                        return Content("Err|Excel表为空表,无数据!");
                    }
                    else
                    {

                        DataTable tbVchnums = new CommTableInfoBLL().GetDataTable("select vchnum from t_bank_transDetail where not vchnum is null group by vchnum");


                        #region 定义DataTable
                        DataTable dtUserAdd = new DataTable();
                        dtUserAdd.Columns.Add(new DataColumn("TransactionType", typeof(string)));//交易类型
                        dtUserAdd.Columns.Add(new DataColumn("BusinessType", typeof(string)));//业务类型
                        dtUserAdd.Columns.Add(new DataColumn("DebitAccountNo", typeof(string)));//付款人账号
                        dtUserAdd.Columns.Add(new DataColumn("PayersName", typeof(string)));//付款人名称
                        dtUserAdd.Columns.Add(new DataColumn("PayerAccountBank", typeof(string)));//付款人开户行名

                        dtUserAdd.Columns.Add(new DataColumn("PayeesAccountNumber", typeof(string)));//收款人账号
                        dtUserAdd.Columns.Add(new DataColumn("PayeesName", typeof(string)));//收款人名称
                        dtUserAdd.Columns.Add(new DataColumn("BeneficiaryAccountBank", typeof(string)));//收款人开户行名

                        dtUserAdd.Columns.Add(new DataColumn("VoucherNumber", typeof(string)));//收款人结算子卡号

                        
                        dtUserAdd.Columns.Add(new DataColumn("TransactionDate", typeof(string)));//交易日期
                        dtUserAdd.Columns.Add(new DataColumn("TransactionTime", typeof(string)));//交易时间
                        dtUserAdd.Columns.Add(new DataColumn("TradeCurrency", typeof(string)));//交易货币

                        dtUserAdd.Columns.Add(new DataColumn("TradeAmount", typeof(decimal)));//交易金额
                        dtUserAdd.Columns.Add(new DataColumn("After-transaction-balance", typeof(string)));//交易后的余额
                        dtUserAdd.Columns.Add(new DataColumn("TransactionReferenceNumber", typeof(string)));//交易流水号
                        dtUserAdd.Columns.Add(new DataColumn("Reference", typeof(string)));//摘要
                        dtUserAdd.Columns.Add(new DataColumn("Purpose", typeof(string)));//用途
                        dtUserAdd.Columns.Add(new DataColumn("Remark", typeof(string)));//交易附言


                        DataRow drTemp = null;
                        #endregion

                        NPOI.SS.UserModel.IRow titleRow = sheet.GetRow(0);
                        Dictionary<string, int> dis = new Dictionary<string, int>();
                        for (int s=0; s<titleRow.Cells.Count;s++)
                        {
                            if(titleRow.GetCell(s).ToString().StartsWith("交易类型"))
                            {
                                dis.Add("TransactionType", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("业务类型"))
                            {
                                dis.Add("BusinessType", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("付款人账号"))
                            {
                                dis.Add("DebitAccountNo", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("付款人名称"))
                            {
                                dis.Add("PayersName", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("付款人开户行名"))
                            {
                                dis.Add("PayerAccountBank", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("收款人账号"))
                            {
                                dis.Add("PayeesAccountNumber", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("收款人名称"))
                            {
                                dis.Add("PayeesName", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("收款人开户行名"))
                            {
                                dis.Add("BeneficiaryAccountBank", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("凭证号码"))
                            {
                                dis.Add("VoucherNumber", s);//收款人结算子卡号
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易日期"))
                            {
                                dis.Add("TransactionDate", s);
                            }

                            else if (titleRow.GetCell(s).ToString().StartsWith("交易时间"))
                            {
                                dis.Add("TransactionTime", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易货币"))
                            {
                                dis.Add("TradeCurrency", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易金额"))
                            {
                                dis.Add("TradeAmount", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易后余额"))
                            {
                                dis.Add("After-transaction-balance", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易流水号"))
                            {
                                dis.Add("TransactionReferenceNumber", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("摘要"))
                            {
                                dis.Add("Reference", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("用途"))
                            {
                                dis.Add("Purpose", s);
                            }
                            else if (titleRow.GetCell(s).ToString().StartsWith("交易附言"))
                            {
                                dis.Add("Remark", s);
                            }
                            
                        }

                        #region 标准劳酬Excel格式  ：编号、姓名、金额、备注

                        List<T_Bank_TransDetail> transList = new List<T_Bank_TransDetail>();
                        List<T_Bank_Rcv> bankrcvList = new List<T_Bank_Rcv>();
                        decimal TransMoney = 0;
                        int iDirection = 1;

                        for (int i = 1; i < rows; i++)
                        {
                            NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                            //int iType = row.GetCell(0).CellType;//文本是1，数字是0
                            //NPOI.SS.UserModel.CellType iType = 0;
                            if (string.IsNullOrWhiteSpace( row.GetCell(0).StringCellValue))
                            {
                                continue;
                            }

                            drTemp = dtUserAdd.NewRow();

                            foreach (var d in dis)
                            {
                                if(d.Key== "TradeAmount")
                                {
                                    drTemp[d.Key] = row.GetCell(d.Value).NumericCellValue;
                                }
                                else
                                {
                                    drTemp[d.Key] = row.GetCell(d.Value).StringCellValue;
                                }                                
                            }
                            DataRow[] testRow=tbVchnums.Select("vchnum='"+ drTemp["TransactionReferenceNumber"] + "'");
                            if (testRow.Length == 0)
                            {
                                dtUserAdd.Rows.Add(drTemp);
                                int mainId = Convert.ToInt32( ( DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00")));


                                #region 判断存款类型
                                string strTranstype = "";
                                switch (drTemp["TransactionType"].ToString())
                                {
                                    case "国内汇款":
                                        {
                                            strTranstype = "01";
                                        }break;
                                    case "国外汇款":
                                        {
                                            strTranstype = "02";
                                        }
                                        break;
                                    case "人行大额":
                                        {
                                            strTranstype = "03";
                                        }
                                        break;
                                    case "人行小额":
                                        {
                                            strTranstype = "04";
                                        }
                                        break;
                                    case "现金存款":
                                        {
                                            strTranstype = "05";
                                        }
                                        break;
                                    case "转帐收入":
                                        {
                                            strTranstype = "06";
                                        }
                                        break;
                                    case "汇票":
                                        {
                                            strTranstype = "07";
                                        }
                                        break;
                                    case "本票":
                                        {
                                            strTranstype = "08";
                                        }
                                        break;
                                    case "支票":
                                        {
                                            strTranstype = "09";
                                        }
                                        break;
                                    case "冲账":
                                        {
                                            strTranstype = "10";
                                        }
                                        break;
                                    case "冲正":
                                        {
                                            strTranstype = "11";
                                        }
                                        break;
                                    case "承兑汇票":
                                        {
                                            strTranstype = "12";
                                        }
                                        break;
                                    case "托收承付":
                                        {
                                            strTranstype = "13";
                                        }
                                        break;
                                    case "保证金":
                                        {
                                            strTranstype = "14";
                                        }
                                        break;
                                    case "现金取款":
                                        {
                                            strTranstype = "15";
                                        }
                                        break;
                                    case "转帐支出":
                                        {
                                            strTranstype = "16";
                                        }
                                        break;
                                    case "贷款放款":
                                        {
                                            strTranstype = "17";
                                        }
                                        break;
                                    case "贷款还款":
                                        {
                                            strTranstype = "18";
                                        }
                                        break;
                                    case "实时汇划":
                                        {
                                            strTranstype = "21";
                                        }
                                        break;
                                    case "退汇":
                                        {
                                            strTranstype = "22";
                                        }
                                        break;
                                    case "结息":
                                        {
                                            strTranstype = "31";
                                        }
                                        break;
                                    case "批量收费":
                                        {
                                            strTranstype = "32";
                                        }
                                        break;
                                    case "收费":
                                        {
                                            strTranstype = "41";
                                        }
                                        break;
                                    case "其他":
                                        {
                                            strTranstype = "99";
                                        }break;                                        
                                    default:
                                        { strTranstype = "99"; }
                                        break;
                                }







                                #endregion

                                if (drTemp["TransactionType"].ToString()=="来账")
                                {
                                    TransMoney = Convert.ToDecimal(drTemp["TradeAmount"].ToString());
                                    iDirection = 1;
                                }
                                else
                                {
                                    TransMoney = -Convert.ToDecimal(drTemp["TradeAmount"].ToString());
                                    iDirection = 2;
                                }
                                //获取用户备注摘要信息
                                string strfurinfo = "";
                                if (!string.IsNullOrWhiteSpace(drTemp["Remark"].ToString()))
                                {
                                    strfurinfo = drTemp["Remark"].ToString();
                                }
                                else if(!string.IsNullOrWhiteSpace(drTemp["Purpose"].ToString()))
                                {
                                    strfurinfo = drTemp["Purpose"].ToString();
                                }
                                else if (!string.IsNullOrWhiteSpace(drTemp["Reference"].ToString()))
                                {
                                    strfurinfo = drTemp["Reference"].ToString();
                                }
                                //增加记录
                                transList.Add(new T_Bank_TransDetail()
                                {
                                    MainID = mainId,
                                    valdat = TongYongHelper.StrToDate(drTemp["TransactionDate"].ToString()),
                                    avlbal = Convert.ToDecimal(drTemp["After-transaction-balance"].ToString()),
                                    fractnacntname = drTemp["PayersName"].ToString(),//付款人
                                    fractnactacn =drTemp["DebitAccountNo"].ToString(),//付款账号
                                    fractnibkname= drTemp["PayerAccountBank"].ToString(),//付款行
                                    toactntoname =drTemp["PayeesName"].ToString(),//收款人
                                    toactnactacn=drTemp["PayeesAccountNumber"].ToString(),//收款账号                                    
                                    toactntobank = drTemp["BeneficiaryAccountBank"].ToString(),//收款行
                                    toactncardno = drTemp["VoucherNumber"].ToString(),//收款结算子卡号
                                    txndate = drTemp["TransactionDate"].ToString(),
                                    txntime = drTemp["TransactionTime"].ToString().Replace(":",""),
                                    trncur = "CNY",
                                    transtype= strTranstype,
                                    txnamt = TransMoney,
                                    vchnum = drTemp["TransactionReferenceNumber"].ToString(),
                                    interinfo = drTemp["Reference"].ToString(),
                                    furinfo = "Excel补导入:"+strfurinfo,
                                    direction=iDirection,
                                });

                                //不插入T_Bank_Rcv 改为存储过程执行
                                //bankrcvList.Add(new T_Bank_Rcv() { 
                                //    ImportFlag=0,                                    
                                //    fractName = drTemp["PayersName"].ToString(),//付款人                                    
                                //    Error="Excel补导入",
                                //    CardNo = drTemp["VoucherNumber"].ToString(),//收款结算子卡号
                                //    tnxdate = drTemp["TransactionDate"].ToString(),
                                //    CreateDate = TongYongHelper.StrToDate( drTemp["TransactionDate"].ToString()),
                                //    //FCrimeCode="",
                                //    //FName="",
                                //    //OffsetVchNum="",
                                //    //Id=0,                                    
                                //    direction=iDirection,
                                //    transtype = strTranstype,
                                //    RcvAmount = Convert.ToDecimal(drTemp["TradeAmount"].ToString()),
                                //    VchNum = drTemp["TransactionReferenceNumber"].ToString(),
                                //    Remark = strfurinfo,//备注信息
                                //});
                            }
                            
                        }
                        #endregion

                        if (dtUserAdd.Rows.Count <= 0)
                        {
                            return Content("Err|恭喜，本次Excel记录以及全部导入到系统中了");
                        }
                        else
                        {
                            //插入不存在的记录
                            bool insertFlag=new BaseDapperBLL().Insert<T_Bank_TransDetail>(transList);
                            //不插入T_Bank_Rcv 改为存储过程执行
                            //bool insertRcvFlag = new BaseDapperBLL().Insert<T_Bank_Rcv>(bankrcvList);
                            int r=new BaseDapperBLL().ExecuteSql("exec [P_BankInsertNoCardRec]",null);
                        }
                        string title = "筛选后未导入的存款记录报表";
                        
                        //string startTime = Request["startTime"];
                        //string endTime = Request["endTime"];
                        string strCountTime = string.Format("统计期间:~--~");

                        string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankDetailList.xls");
                        strFileName = Server.MapPath("~/Upload/" + strFileName); ;

                        ExcelRender.RenderToExcel(dtUserAdd, title, 9, strFileName,false, strCountTime);
                        return Content("OK|" + strLoginName + "_BankDetailList.xls");
                    }
                }
            }
            return Content("Err|导入失败，服务器没有接收到Excel文件");
        }

        #endregion


    }
}
