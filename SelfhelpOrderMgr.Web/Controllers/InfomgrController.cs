using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common;
using SelfhelpOrderMgr.Common.CustomExtend;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [CustomActionFilterAttribute]
    public class InfomgrController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        //
        // GET: /Infomgr/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchInfo()
        {
            //string dbname = context.Request.Cookies["person_Users"]["dbname"];
            string strStartDate = Request["startDate"];
            string strEndDate = Request["endDate"];

            List<t_XFQueryList> list = GetXFList(strStartDate,strEndDate);
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(list));
        }

        /// <summary>
        /// 测试犯人是否存在或是正确与否
        /// </summary>
        /// <returns></returns>
        public ActionResult ExistsCriminalCheck()
        {
            string strFCrimeCode = Request["fCode"];
            string strFCrimeName = Request["fName"];

            if ("" == strFCrimeCode)
            {
                List<T_Criminal> czys = new T_CriminalBLL().GetModelList("fname='" + strFCrimeName + "'");
                if (czys.Count == 1)
                {
                    strFCrimeCode = czys[0].FCode;
                }
                else if (czys.Count==0)
                {
                    return Content("Err|抱歉，找不到该名字的犯人信息！");
                }
                else
                {
                    return Content("Err|找到"+ czys.Count.ToString() +"个与该名字相同的犯人信息，请您用狱号查询吧！");
                }
            }
            else
            {
                T_Criminal czy = new T_CriminalBLL().GetModel( strFCrimeCode);
                if(czy==null)
                {
                    return Content("Err|抱歉，找不到该狱号的犯人信息！");
                }
            }
            return Content("OK|成功" + strFCrimeCode);
            
        }

        private List<t_XFQueryList> GetXFList(string strStartDate, string strEndDate)
        {
            //string order = Request["order"];
            //int page = Convert.ToInt16(Request["page"]);
            //int rows = Convert.ToInt16(Request["rows"]);

            //string sort = Request["sort"];
            string action = Request["action"];
            string strFCrimeCode = Request["fCode"];
            string strFCrimeName = Request["fName"];
            //string strAreaName = context.Request["fAreaName"].ToString();
            

            if ("" == strFCrimeCode)
            {
                List<T_Criminal> czys = new T_CriminalBLL().GetModelList("fname='" + strFCrimeName + "'");
                if (czys.Count > 0)
                {
                    strFCrimeCode = czys[0].FCode;
                }                
            }

            List<t_XFQueryList> list = new t_XFQueryListBLL().GetFCrimeXFModelList(action, strFCrimeCode, Convert.ToDateTime(strStartDate), Convert.ToDateTime(strEndDate));

            return list;
        }

        public  ActionResult InfoListPrint()
        {
            string strStartDate = Request["startDate"];
            string strEndDate = Request["endDate"];
            List<t_XFQueryList> lists = GetXFList(strStartDate, strEndDate);
            ViewData["lists"] = lists;
            xfSummaryInfo xfsuminfo = new xfSummaryInfo();
            DateTime endDate = Convert.ToDateTime(strEndDate);
            DateTime startDate = Convert.ToDateTime(strStartDate);
            xfsuminfo.Tongjiyueshu = Fun(endDate, startDate);
            
            foreach (t_XFQueryList list in lists)
            { 
                switch(list.fcrimecode)
                {
                    case "累计消费":
                        {
                            xfsuminfo.Leijizongxf = list.Cmoney;
                        }
                        break;
                    case "刚需":
                        {
                            xfsuminfo.gxFood = list.Cmoney;
                        }
                        break;
                    case "月平均消费":
                        {
                            xfsuminfo.Yuejunxf = list.Cmoney;
                        }
                        break;
                    case "衣服被褥":
                        {
                            xfsuminfo.Yufubeizi = list.Cmoney;
                        }
                        break;
                    case "超市购物":
                        {
                            xfsuminfo.Chaoshigouwu = list.Cmoney;                            
                        }
                        break;
                    case "药品其他":
                        {
                            xfsuminfo.Yaopinqita = list.Cmoney;
                        }
                        break;
                    case "小炒消费":
                        {
                            xfsuminfo.Xiaochao = list.Cmoney;
                        }
                        break;
                    case "书刊报纸":
                        {
                            xfsuminfo.Shukanbaozi = list.Cmoney;
                        }
                        break;
                    case "交罚金":
                        {
                            xfsuminfo.Jiaofajin = list.Cmoney;
                        }
                        break;
                    
                    case "总收入":
                        {
                            xfsuminfo.Zongshouru = list.Cmoney;
                        }
                        break;
                    case "汇款":
                        {
                            xfsuminfo.Huikuanshouru = list.Cmoney;
                        }
                        break;
                    case "劳动报酬":
                        {
                            xfsuminfo.Laodongbaochou = list.Cmoney;
                            
                        }
                        break;
                    case "零用金":
                        {
                            xfsuminfo.Ningyongjin = list.Cmoney;
                            
                        }
                        break;
                    case "账户余额":
                        {
                            xfsuminfo.Zhanghuzongyue = list.Cmoney;
                            
                        }
                        break;
                    case "不可用金额":
                        {
                            xfsuminfo.BuKeYongMoney = list.Cmoney;
                        }break;
                    default:
                        break;
                }
            }
            ViewData["xfsuminfo"] = xfsuminfo;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            int days = 0;
            int Month = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
            if (endDate.Day - startDate.Day <0)
            {
                Month = Month - 1;
            }
            if (endDate.Day - startDate.Day>=0)
            {
                days = endDate.Day - startDate.Day;
            }
            else
            {
                days = 30+endDate.Day - startDate.Day;
            }
            ViewData["Month"] = Month;
            if (days == 0)
            {
                ViewData["days"] = "整";
            }else
            {
                ViewData["days"] = days.ToString()+"天";
            }
            
            return View();
        }

        public int Fun(DateTime endDate,DateTime datetime )
        {
            DateTime dt = endDate;
            DateTime dt2 = datetime;
            if (DateTime.Compare(dt, dt2) < 0)
            {
                dt2 = dt;
                dt = datetime;
            }
            int year = dt.Year - dt2.Year;
            int month = dt.Month - dt2.Month;
            month = year * 12 + month;
            if (dt.Day - dt2.Day < -15)
            {
                month--;
            }
            else if (dt.Day - dt2.Day > 14)
            {
                month++;
            }
            return month;
        }

        //监区银行账户查询e
        public ActionResult AmountQuery()
        {
            #region 加载队别信处
            List<T_AREA> areas = (List<T_AREA>)new T_AREABLL().GetModelList("");
            Dictionary<string, string> userAreas = new Dictionary<string, string>();
            foreach (T_AREA area in areas)
            {
                userAreas.Add(area.FName, area.FCode);
            }
            
            ViewData["userAreas"] = userAreas;
            #endregion
            return View();
        }

        public ActionResult AmountSearch()
        {
            List<T_TempAmount_Card> cards = GetCardAmountList(); 
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return Content(jss.Serialize(cards));
        }

        public ActionResult PrintCardAmountList()
        {
            List<T_TempAmount_Card> cards = GetCardAmountList();
            ViewData["cards"] = cards;
            return View();
        }


             
        public ActionResult PrintXFGBList()
        {
            string action = Request["action"];
            string strFCrimeCode = Request["fCode"];
            string strFCrimeName = Request["fName"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            //string dbname = Request.Cookies["person_Users"]["dbname"];

            string order = Request["order"];
            int page = Convert.ToInt16(Request["page"]);
            int rows = Convert.ToInt16(Request["rows"]);
            string sort = Request["sort"];
            string strAreaName = Request["fAreaName"];
            string cardStatus = Request["cardStatus"]; //IC卡的状态

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.FCode,a.FName,c.Fname FAreaName,isnull(d.FMoneyIn,0) FMoneyIn,isnull(d.FMoneyOut,0) FMoneyOut,(b.AmountA+b.AmountB+b.AmountC) FUserMoneyAll,b.AmountC 
                from t_Criminal a left join T_Criminal_Card b on a.fcode=b.fcrimecode 
				left join t_Area c on a.FAreaCode=c.fcode
				left outer join
                (select fcrimecode,isnull(sum(Damount),0) FMoneyIn,isnull(sum(Camount),0) FMoneyOut 
                from t_vcrd where flag=0 and Crtdate>=@StartDate and CrtDate<@EndDate
                group by fcrimecode) d on a.fcode=d.fcrimecode 
                where a.fcode=b.fcrimecode and a.fAreaCode=c.fcode and isnull(a.fflag,0)=0
                ");
            if (!string.IsNullOrEmpty(strFCrimeCode))
            {
                strSql.Append(" and a.fcode=@FCode");
            }
            if (!string.IsNullOrEmpty(strFCrimeName))
            {
                strSql.Append(" and a.FName =@FName");
            }
            if ("" != strAreaName && "000" != strAreaName)
            {
               
                strSql.Append(" and c.fcode=@FAreaName");
            }
            if ("" != cardStatus && "000" != cardStatus)
            {

                strSql.Append(" and b.cardflaga=@cardStatus");
            }
            object param = new { FCode = strFCrimeCode, FName = strFCrimeName, FAreaName = strAreaName, StartDate = StartDate, EndDate = EndDate, cardStatus = cardStatus };
            List<T_XFGSList> cards = new CommTableInfoBLL().GetXFGSList(strSql.ToString(), param);

            ViewData["StartDate"] = StartDate;
            ViewData["EndDate"] = EndDate;
            ViewData["cards"] = cards;
            return View();
        }
        //存款指南
        public ActionResult DepositGuide()
        {
            string bankAccNo = Request["bankAccNo"];
            string fName = Request["fName"];
            ViewData["fName"] = fName;
            string fCode = Request["fCode"];
            ViewData["fCode"] = fCode;
            string fareaName = Request["fareaName"];
            ViewData["fareaName"] = fareaName;
            
            ViewData["bankAccNo"] = bankAccNo;
            return View();
        }

        //余额公示表Excel导出
        public ActionResult ExcelGSBB()
        {
            string action = Request["action"];
            string strFCrimeCode = Request["fCode"];
            string strFCrimeName = Request["fName"];
            string StartDate = Request["StartDate"];
            string EndDate = Request["EndDate"];
            //string dbname = Request.Cookies["person_Users"]["dbname"];
            string LoginUserName = Session["loginUserCode"] == null ? string.Empty : Session["LoginUserName"].ToString();

            string order = Request["order"];
            int page = Convert.ToInt16(Request["page"]);
            int rows = Convert.ToInt16(Request["rows"]);
            string sort = Request["sort"];
            string strAreaName = Request["fAreaName"];
            string cardStatus = Request["cardStatus"]; //IC卡的状态

            StringBuilder strSql = new StringBuilder();
    //        strSql.Append(@"select a.FCode 编号,a.FName 姓名,c.Fname 队别,b.SecondaryBankCard 中银结算卡,isnull(d.FMoneyIn,0) 本期收入,isnull(d.FMoneyOut,0) 本期支出,(b.AmountA+b.AmountB+b.AmountC) 总余额,b.AmountC 留存不可用金额
    //            from t_Criminal a left join T_Criminal_Card b on a.fcode=b.fcrimecode 
				//left join t_Area c on a.FAreaCode=c.fcode
				//left outer join
    //            (select fcrimecode,isnull(sum(Damount),0) FMoneyIn,isnull(sum(Camount),0) FMoneyOut 
    //            from t_vcrd where flag=0 and Crtdate>='" + StartDate.Substring(0,10) + "' and CrtDate<'"+ EndDate.Substring(0,10) +@"'
    //            group by fcrimecode) d on a.fcode=d.fcrimecode 
    //            where a.fcode=b.fcrimecode and a.fAreaCode=c.fcode and isnull(a.fflag,0)=0
    //            ");

            strSql.Append(@"select a.FCode 编号,a.FName 姓名,c.Fname 队别,b.SecondaryBankCard 中银结算卡,isnull(d.FMoneyIn,0) 本期收入,isnull(d.FMoneyOut,0) 本期支出,(b.AmountA+b.AmountB+b.AmountC) 总余额,b.AmountC 留存不可用金额
                from t_Criminal a left join T_Criminal_Card b on a.fcode=b.fcrimecode 
				left join t_Area c on a.FAreaCode=c.fcode
				left outer join
                (select fcrimecode,isnull(sum(Damount),0) FMoneyIn,isnull(sum(Camount),0) FMoneyOut 
                from t_vcrd where flag=0 ");

            if (string.IsNullOrWhiteSpace(StartDate) == false)
            {
                strSql.Append(@" and Crtdate>='" + StartDate.Substring(0, 10) + "'");
            }

            if (string.IsNullOrWhiteSpace(EndDate) == false)
            {
                strSql.Append(@" and Crtdate<'" + EndDate.Substring(0, 10) + "'");
            }
            strSql.Append(@"  group by fcrimecode) d on a.fcode=d.fcrimecode 
                where a.fcode=b.fcrimecode  ");


            if (!string.IsNullOrEmpty(strFCrimeCode))
            {
                strSql.Append(" and a.fcode='" + strFCrimeCode + "'");
            }
            if (!string.IsNullOrEmpty(strFCrimeName))
            {
                strSql.Append(" and a.FName ='" + strFCrimeName + "'");
            }
            if ("" != strAreaName && "000" != strAreaName)
            {

                strSql.Append(" and c.fcode='" + strAreaName + "'");
            }
            if ("" != cardStatus && "000" != cardStatus)
            {

                strSql.Append(" and b.cardflaga=" + cardStatus );
            }
            object param = new { FCode = strFCrimeCode, FName = strFCrimeName, FAreaName = strAreaName, StartDate = StartDate, EndDate = EndDate, cardStatus = cardStatus };
            List<T_XFGSList> cards = new CommTableInfoBLL().GetXFGSList(strSql.ToString(), param);

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(LoginUserName + "_ExcelList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); 
            ExcelRender.RenderToExcel(dt, "余额公示报表("+StartDate + "~" + EndDate + ")", strFileName);
            return Content("OK|" + LoginUserName + "_ExcelList.xls");
        }

        private List<T_TempAmount_Card> GetCardAmountList()
        {
            string action = Request["action"];
            string strFCrimeCode = Request["fCode"];
            string strFCrimeName = Request["fName"];
            //string dbname = Request.Cookies["person_Users"]["dbname"];

            string order = Request["order"];
            int page = Convert.ToInt16(Request["page"]);
            int rows = Convert.ToInt16(Request["rows"]);
            string sort = Request["sort"];
            string strAreaName = Request["fAreaName"];
            string cardStatus = Request["cardStatus"];//IC卡的状态
            List<T_TempAmount_Card> cards = new T_TempAmount_CardBLL().GetSearchCardAmount(strFCrimeCode, strFCrimeName, strAreaName,cardStatus);

            return cards;
        }

        public ActionResult CYSetting()
        {

            return View();
        }
        public ActionResult GetCyInfo()
        {
            string action = Request["action"].ToString();
            //string LoginUserName = Request.Cookies["person_Users"]["sysLoginName"].ToString();
            //string dbname = Request.Cookies["person_Users"]["dbname"].ToString();
            if (action == "GetAllList")
            {
                List<T_CY_TYPE> list = (List<T_CY_TYPE>)new T_CY_TYPEBLL().GetModelList("");
                JavaScriptSerializer jss = new JavaScriptSerializer();
                if (list.Count <= 0)
                {
                    T_CY_TYPE m1 = new T_CY_TYPE();
                    list.Add(m1);
                }
                return Content(jss.Serialize(list));
            }
            else if (action == "SaveRoleTree")
            {
                T_CY_TYPE m1 = new T_CY_TYPE();
                m1.FName = Request["FName"];
                m1.FCode = Request["FCode"];
                m1.famtmonth = Convert.ToDecimal(Request["FAmtMonth"]);
                m1.FPower =Request["FPower"];
                m1.FDesc = "";

                T_CY_TYPEBLL bb = new T_CY_TYPEBLL();
                T_CY_TYPE m2 = bb.GetModel( m1.FCode);
                if (m2 == null)
                {
                    new T_CY_TYPEBLL().Add( m1);
                }
                else
                {
                    new T_CY_TYPEBLL().Update( m1);
                }
                return Content("保存成功!");
            }
            else if (action == "DelRoleTree")
            {
                T_CY_TYPE m1 = new T_CY_TYPE();
                m1.FName = Request["FName"];
                m1.FCode = Request["FCode"];
                new T_CY_TYPEBLL().Delete( m1.FCode);
                return Content("删除成功！");

            }
            else
            {
                return Content("未能识别的操作！");
            }
        }

        public ActionResult ICCardManager()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("");
            ViewData["areas"] = areas;
            return View();
        }

        public ActionResult GetICCardInfo()
        {
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
            string FName =  Request["FName"];
            string FCode =  Request["FCode"];
            string FAreaCode =  Request["FAreaCode"];
            string strWhere="";
            if(string.IsNullOrEmpty(FName)==false)
            {
                strWhere = " and FName Like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = " and FCode ='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(FAreaCode) == false)
            {
                strWhere = " and FAreaCode='" + FAreaCode + "'";
            }
            
            //string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string userCode = Session["loginUserCode"].ToString();
            List<T_Criminal> criminals = (List<T_Criminal>)new T_CriminalBLL().GetPageListOfIEnumerable(page, pageSize, "isnull(fflag,0)=0 and fareacode in(select fareacode from t_czy_area where fcode='" + userCode + "' and fflag=2)" + strWhere);
            ViewData["criminals"] = criminals;


            List<T_Criminal> rowList = new T_CriminalBLL().GetModelList("isnull(fflag,0)=0 and fareacode in(select fareacode from t_czy_area where fcode='" + userCode + "' and fflag=2)" + strWhere);

            string sss = "{\"total\":" + rowList.Count.ToString() + ",\"rows\":" + jss.Serialize(criminals) + "}";
            return Content(sss); 
        }

        public ActionResult GetCardList()//获取卡号信息
        {
            string FCrimeCode = Request["FCrimeCode"];
            List<T_ICCARD_LIST> cards = new T_ICCARD_LISTBLL().GetModelList("FCrimeCode='" + FCrimeCode + "'");
            return Content(jss.Serialize(cards));
        }

        public ActionResult SetICCardStatus()//改变IC卡的状态
        {
            string FCrimeCode = Request["FCrimeCode"];
            string CardCode = Request["CardCode"];
            string FFlag = Request["FFlag"];
            if(new T_ICCARD_LISTBLL().UpdateCardStatus(Convert.ToInt32(FFlag),CardCode,FCrimeCode))
            {
                return Content("OK|变更成功");
            }
            return Content("Err|变更失败");
        }
        //获取将离监人员名单
        public ActionResult GetLeavePrisonList()
        {

            //监区队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;

            return View();
        }

        public ActionResult GetCheckLeaveUserMoney()
        {
            string FCode = Request["FCode"];
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|用户编号不能为空");
            }

            return Content(new BaseInfoMgrBLL().LeavePrisonCheckUserMoney(FCode));
        }


        //中行版，吴个人银行卡版结算方式
        #region 离监结算与恢复在押


        //挂失或销户结算操作
        /// <summary>
        /// 挂失或销户结算操作
        /// </summary>
        /// <param name="payMode"></param>
        /// <param name="FCode"></param>
        /// <param name="FName"></param>
        /// <param name="FOuDate"></param>
        /// <returns></returns>
        public ActionResult NoBankCardLeavePrisonList(int payMode, string FCode, string FName, string FOuDate)
        {
            string LoginUserName = Session["loginUserCode"] == null ? string.Empty : Session["LoginUserName"].ToString();
            
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            
            if (string.IsNullOrEmpty(FCode))
            {
                return Content("Err|离监的犯人编号不能为空");
            }

            //验证是否具有管辖权
            if (!new CommTableInfoBLL().CheckUserManagerrPower(FCode, Session["loginUserCode"].ToString()))
            {
                return Content("Err|您没有该犯的管理权限，不能办理");
            }


            string rtnReustl = "OK|该犯之前就结算过了";
            T_Criminal fuser = new T_CriminalBLL().GetModel(FCode);
            string FAreaCode = Request["FAreaCode"];
            string rtnJson = "";

            //判断是否有在途的记录存取款记录
            #region 判断是否有在途的记录存取款记录
            //判断劳动报酬
            string sql = "";
            sql = "select b.BID,a.DType,b.FCRIMECODE,b.FAMOUNT,a.Crtdt from t_Bonus a,T_BONUSDTL b where a.BID=b.BID and a.FLAG=0 and b.FCRIMECODE=@fcrimecode";
            DataTable dt = new CommTableInfoBLL().GetDataTable(sql, new { fcrimecode = FCode });
            if (dt.Rows.Count > 0)
            {
                return Content($"Err|劳动报酬有未审批完记录，没有进账，单号：{dt.Rows[0]["BID"].ToString()},日期{dt.Rows[0]["Crtdt"].ToString()},记录{dt.Rows.Count}条，不能结算。");
            }

           

            //判断零用金在途

            sql = "select b.pid,'零用金' as DType,b.FCRIMECODE,b.FAMOUNT,a.Crtdt from T_PROVIDE a,T_PROVIDEDTL b where a.pid=b.pid and a.FLAG=0 and b.FCRIMECODE=@fcrimecode";
            dt = new CommTableInfoBLL().GetDataTable(sql, new { fcrimecode = FCode });
            if (dt.Rows.Count > 0)
            {
                return Content($"Err|零用金有未审批完记录，没有进账，单号：{dt.Rows[0]["pid"].ToString()},日期{dt.Rows[0]["Crtdt"].ToString()},记录：{dt.Rows.Count}条,不能结算");
            }

            //判断未审核的记录
            sql = "select DType,fcrimecode,CrtDate from t_Vcrd where  flag =-2 and FCRIMECODE=@fcrimecode";
            dt = new CommTableInfoBLL().GetDataTable(sql, new { fcrimecode = FCode });
            if (dt.Rows.Count > 0)
            {
                return Content($"Err|未审批完记录，没有进账，类型：{dt.Rows[0]["DType"].ToString()},日期{dt.Rows[0]["CrtDate"].ToString()},记录：{dt.Rows.Count}条,不能结算");
            }


            #endregion

            //否则采用正常结算模式
            if (fuser.fflag !=1)//如果未结算过，就进行结算
            {
                //判断是否存在付款记录
                List<T_Bank_PaymentRecord> recs = new T_Bank_PaymentRecordBLL().GetModelList<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(jss.Serialize(new { FCrimeCode = FCode }),"Id",10);
                if (recs.Count > 0)
                {
                    return Content("Err|已经存在【"+ recs.Count + "】条结算记录，不能重复结算");
                }
                switch (payMode)
                {
                    case 0://网点支取
                        {
                            rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProcedure(FCode, LoginUserName);

                            //网点支取
                        } break;
                    case 1://现金结算
                        {
                            rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProc_NoBankCard(FCode, LoginUserName, payMode);
                            //现金结算
                        } break;
                    case 2://转账支付
                        {
                            //否则采用正常结算模式
                            rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProc_NoBankCard(FCode, LoginUserName, payMode);
                            //转账支付
                        } break;
                    case 5://放弃领款
                        {
                            rtnReustl = new T_TempLeavePrisonBLL().ExcuteStoredProcedure(FCode, LoginUserName);
                            new CommTableInfoBLL().ExecSql($"update T_balancelist set PayMode=5 where fcrimecode='{FCode}'");
                        }
                        break;
                    case 3://只做挂失
                        {
                            rtnReustl = new T_TempLeavePrisonBLL().SetLossAndInsertBankProve(FCode);
                        }break;

                    default:
                        {
                            return Content("Err|未定义的动作方式【" + payMode + "】");
                        }
                }
            }
            
            string sss = new T_TempLeavePrisonBLL().InsertBankProve(FCode,payMode);
            rtnJson = rtnReustl;
            return Content(rtnJson);
        }

        /// <summary>
        /// 获取离监用户列表
        /// </summary>
        /// <param name="FCode"></param>
        /// <param name="FName"></param>
        /// <param name="startDt"></param>
        /// <param name="endDt"></param>
        /// <param name="FAreaCode"></param>
        /// <param name="rtnJson"></param>
        /// <returns></returns>
        public ActionResult GetTempLeavePrisonList(string FCode, string FName, string startDate, string endDate, string FAreaCode)
        {
            string loginUserCode = Session["loginUserCode"] == null ? string.Empty : Session["loginUserCode"].ToString();

            List<T_TempLeavePrison> list = (List<T_TempLeavePrison>)new T_TempLeavePrisonBLL().GetLeavePrison(loginUserCode, startDate, endDate, FAreaCode, FCode, FName);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            if (list.Count <= 0)
            {
                T_TempLeavePrison m1 = new T_TempLeavePrison();
                list.Add(m1);
            }
            string rtnJson = jss.Serialize(list);
            return  Content(rtnJson);
        }

        /// <summary>
        /// 结算后，恢复在押
        /// </summary>
        /// <param name="FCode"></param>
        /// <param name="FName"></param>
        /// <param name="payMode"></param>
        /// <returns></returns>

        [MyLogActionFilterAttribute]
        public ActionResult ResotreCriminalInPrison(string FCode, string FName, int payMode)
        {
            /*
             * 如果设定只有管理才可以恢复离监人员，则下面内容生效
             * 2017-09-20 曾林进，针对永安监狱需求而改
             * 判断操作用户是否是管理员，如果不是则不能恢复
             */
            ResultInfo rs = new ResultInfo()
            {
                Flag = false,
                ReMsg = "未处理",
                DataInfo = null
            };
            
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("LJHF_AdminFlag");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
                    if (czy.FPRIVATE != 1)
                    {
                        rs.ReMsg = "Err|对不起您没有权限，恢复离监的用户，请与管理员联系，谢谢";
                        return Json(rs);
                        //return Content("Err|对不起您没有权限，恢复离监的用户，请与管理员联系，谢谢");
                    }
                }
            }

            string strFCode = Request["FCode"];
            string strFName = Request["FName"];

            //验证是否具有管辖权
            if (!new CommTableInfoBLL().CheckUserManagerrPower(FCode, Session["loginUserCode"].ToString()))
            {
                return Content("Err|您没有该犯的管理权限，不能办理");
            }

            if (string.IsNullOrEmpty(strFCode) == true)
            {
                //return Content("Err|编号不能为空");
                rs.ReMsg = "Err|编号不能为空";
                return Json(rs);
            }
            T_Criminal criminal = new T_CriminalBLL().GetModel(strFCode);
            if (criminal == null)
            {
                rs.ReMsg = "Err|对不起，找不到该编号的犯人";
                return Json(rs);
                //return Content("Err|对不起，找不到该编号的犯人");
            }
            //验证是否存在多条结算的记录
            List<T_Bank_PaymentRecord> recs = new T_Bank_PaymentRecordBLL().GetModelList<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(jss.Serialize(new { FCrimeCode = strFCode }),"Id",10);
            if (recs.Count > 1)
            {
                rs.ReMsg = "Err|存在多条结算的记录，无法恢复";
                return Json(rs);
            }
            //==============================================================
            //日期:2020-08-04
            //修改人:曾林进
            //由于执行恢复时，可能出现人员 的FFLag 状态为0 ，IC卡的状态为4 所以取消判断
            //===============================================================

            t_balanceList bal = new t_balanceListBLL().GetModelList("FCrimeCode='" + FCode + "'").OrderByDescending(o => o.seqno).FirstOrDefault();
            if (bal != null)
            {
                if (bal.fcrimecode == FCode && bal.PayMode == 0 && bal.CollectMoneyFlag == 1)
                {
                    rs.ReMsg = "Err|结算的现金已领走，不能恢复";
                    return Json(rs);
                }
            }

            if (payMode == 0)
            {
                T_SHO_ManagerSet mySet = new T_SHO_ManagerSetBLL().GetModel("LijianHuifuXianJinStopFlag");
                if (mySet != null)
                {
                    if (mySet.MgrValue == "1")
                    {
                        rs.ReMsg = "Err|现金结算的记录不能回复，请与管理部门联系";
                        return Json(rs);
                    }
                }
            }



            using (TransactionScope ts = new TransactionScope())
            {
                rs = new SettleService().RestoryInPrison(strFCode, (MoneyPayMode)payMode);               
                ts.Complete();
                Log4NetHelper.logger.Info("恢复离监人员为在押(将离监人员),操作员：" + Session["loginUserName"].ToString() + ",ID=" + criminal.FCode + ",用户名为：" + criminal.FName + "");

            }
            return Json(rs);
        }

        /// <summary>
        /// 设置已领现金
        /// </summary>
        /// <param name="FCode"></param>
        /// <param name="FName"></param>
        /// <param name="seqno"></param>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult QuerenXianjinLingqu(string FCode, string FName,int seqno)
        {
            
            ResultInfo rs = new ResultInfo()
            {
                Flag = false,
                ReMsg = "未处理",
                DataInfo = null
            };
            try
            {
                if (!new CommTableInfoBLL().CheckUserManagerrPower(FCode, Session["loginUserCode"].ToString()))
                {
                    rs.ReMsg = "对不起，您没改犯的管理权限！";
                    return Json(rs);
                }
                t_balanceList bal = new t_balanceListBLL().GetModelList("FCrimeCode='"+ FCode +"' and seqno="+seqno).OrderByDescending(o=>o.seqno ).FirstOrDefault();
                if (bal != null)
                {
                    if (bal.fcrimecode == FCode && bal.PayMode == 0 && bal.CollectMoneyFlag == 0)
                    {
                        bal.CollectMoneyFlag = 1;
                        if (new t_balanceListBLL().Update(bal))
                        {
                            rs.DataInfo = bal;
                            rs.Flag = true;
                            rs.ReMsg = "标记成功";
                        }
                    }
                }
                else
                {
                    rs.ReMsg = "找不到相应的结算记录";
                }
            }
            catch (Exception e)
            {

                rs.ReMsg = e.Message;
            }
            
            return Json(rs);
        }

            #endregion

            //Excel导出消费清表
            public ActionResult ExcelOutList(int id = 1)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string fcrimecode = Request["fcrimecode"];
            string fcrimename = Request["fcrimename"];
            if(string.IsNullOrEmpty(fcrimecode))
            {
                if (string.IsNullOrEmpty(fcrimename))
                {
                    return Content("Err|您传入编号不正确");
                }
                List<T_Criminal> cs = new T_CriminalBLL().GetModelList("fname='" + fcrimename + "'");
                if(cs.Count>1)
                {
                    return Content("Err|您传入人员名字重复数为" + cs.Count.ToString());
                }
                else if(cs.Count==1)
                {
                    fcrimecode = cs[0].FCode;
                }
                else
                {
                    return Content("Err|您传入人员信息不正确");
                }
            }
            if (fcrimecode.Length > 15)
            {
                return Content("Err|您传入编号长度不正确");
            }
            string title = "消费记录清单";
            StringBuilder strSql=new StringBuilder();

            strSql.Append("select [fcrimecode] 编号,[fname] 姓名,[CDate] 日期,[Cmoney] 金额,[Dtype] 类型 from t_XFQueryList where fcrimecode='" + fcrimecode + "' order by CDate");
            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("select [fcrimecode] 编号,[fname] 姓名,[CDate] 日期,[Cmoney] 金额,[Dtype] 类型 from t_XFQueryList where fcrimecode<>'" + fcrimecode + "' order by CDate");
            DataTable dtSumRow = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_ExcelList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);


            

            string strStartDate = Request["StartDate"];
            string strEndDate = Request["EndDate"];

            xfSummaryInfo xfsuminfo = new xfSummaryInfo();
            DateTime endDate = Convert.ToDateTime(strEndDate);
            DateTime startDate = Convert.ToDateTime(strStartDate);
            xfsuminfo.Tongjiyueshu = Fun(endDate, startDate);

            foreach (DataRow r in dtSumRow.Rows)
            {
                switch (r["编号"].ToString())
                {
                    case "累计消费":
                        {
                            xfsuminfo.Leijizongxf = Convert.ToDecimal( r["金额"].ToString());
                        }
                        break;
                    case "刚需":
                        {
                            xfsuminfo.gxFood = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "月平均消费":
                        {
                            xfsuminfo.Yuejunxf = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "衣服被褥":
                        {
                            xfsuminfo.Yufubeizi = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "超市购物":
                        {
                            xfsuminfo.Chaoshigouwu = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "药品其他":
                        {
                            xfsuminfo.Yaopinqita = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "小炒消费":
                        {
                            xfsuminfo.Xiaochao = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "书刊报纸":
                        {
                            xfsuminfo.Shukanbaozi = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "交罚金":
                        {
                            xfsuminfo.Jiaofajin = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;

                    case "总收入":
                        {
                            xfsuminfo.Zongshouru = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "汇款":
                        {
                            xfsuminfo.Huikuanshouru = Convert.ToDecimal(r["金额"].ToString());
                        }
                        break;
                    case "劳动报酬":
                        {
                            xfsuminfo.Laodongbaochou = Convert.ToDecimal(r["金额"].ToString());

                        }
                        break;
                    case "零用金":
                        {
                            xfsuminfo.Ningyongjin = Convert.ToDecimal(r["金额"].ToString());

                        }
                        break;
                    case "账户余额":
                        {
                            xfsuminfo.Zhanghuzongyue = Convert.ToDecimal(r["金额"].ToString());

                        }
                        break;
                    case "不可用金额":
                        {
                            xfsuminfo.BuKeYongMoney = Convert.ToDecimal(r["金额"].ToString());
                        } break;
                    default:
                        break;
                }
            }


            strSql = new StringBuilder();
            strSql.Append(string.Format("select a.fcrimecode,a.bankaccno,c.fname from t_criminal_card a,t_criminal b,t_area c where a.fcrimecode=b.fcode and b.fareaCode=c.fcode and (b.fcode='{0}' or b.fname='{1}')",fcrimecode,fcrimename));
            DataTable dtUserInfo = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            xfsuminfo.BankAccCode = dtUserInfo.Rows[0][1].ToString();
            xfsuminfo.FAreaName = dtUserInfo.Rows[0][2].ToString();


            ViewData["xfsuminfo"] = xfsuminfo;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            int days = 0;
            int Month = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
            if (endDate.Day - startDate.Day < 0)
            {
                Month = Month - 1;
            }
            if (endDate.Day - startDate.Day >= 0)
            {
                days = endDate.Day - startDate.Day;
            }
            else
            {
                days = 30 + endDate.Day - startDate.Day;
            }
            ViewData["Month"] = Month;
            if (days == 0)
            {
                ViewData["days"] = "整";
            }
            else
            {
                ViewData["days"] = days.ToString() + "天";
            }

            //厦门样式
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("XFList_ExcelExport_Mode");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    ExcelRender.RenderToExcel(dt, title, strFileName, xfsuminfo, Convert.ToDateTime(strStartDate), Convert.ToDateTime(strEndDate));
                    return Content("OK|" + strLoginName + "_ExcelList.xls");
                }
            }

            //否则用传统样式=========================================

            strSql = new StringBuilder();
            strSql.Append("select '本期存款' 编号,[fname] 姓名,[CDate] 日期,[Cmoney] 金额,[Dtype] 类型 from t_XFQueryList where fcrimecode='总收入'");
            DataTable dtSave = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("select '本期取款' 编号,[fname] 姓名,[CDate] 日期,[Cmoney] 金额,[Dtype] 类型 from t_XFQueryList where fcrimecode='累计消费'");
            DataTable dtCust = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            strSql = new StringBuilder();
            strSql.Append("select '账户余额' 编号,[fname] 姓名,[CDate] 日期,[Cmoney] 金额,[Dtype] 类型 from t_XFQueryList where fcrimecode='账户余额'");
            DataTable dtSum = new CommTableInfoBLL().GetDataTable(strSql.ToString());

            DataRow row;
            if (dtSave.Rows.Count > 0)
            {
                row = dt.NewRow();
                row["编号"] = dtSave.Rows[0][0];
                row["姓名"] = dtSave.Rows[0][1];
                row["日期"] = dtSave.Rows[0][2];
                row["金额"] = dtSave.Rows[0][3];
                row["类型"] = dtSave.Rows[0][4];
                dt.Rows.Add(row);
            }
            if (dtCust.Rows.Count > 0)
            {
                row = dt.NewRow();
                row["编号"] = dtCust.Rows[0][0];
                row["姓名"] = dtCust.Rows[0][1];
                row["日期"] = dtCust.Rows[0][2];
                row["金额"] = dtCust.Rows[0][3];
                row["类型"] = dtCust.Rows[0][4];
                dt.Rows.Add(row);
            }
            if (dtSum.Rows.Count > 0)
            {
                row = dt.NewRow();
                row["编号"] = dtSum.Rows[0][0];
                row["姓名"] = dtSum.Rows[0][1];
                row["日期"] = dtSum.Rows[0][2];
                row["金额"] = dtSum.Rows[0][3];
                row["类型"] = dtSum.Rows[0][4];
                dt.Rows.Add(row);
            }

            
            ExcelRender.RenderToExcel(dt,title,strFileName);
            return Content("OK|" + strLoginName + "_ExcelList.xls");


            //return View();
        }


        public ActionResult ExcelCriminalList()
        {
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            string FAreaCode = Request["FAreaCode"];
            string FCode = Request["FCode"];
            string FName = Request["FName"];
            string LoginUserCode = Session["loginUserCode"] == null ? string.Empty : Session["loginUserCode"].ToString();


            StringBuilder strSql = GetLeavePrisonListSql(startDt, endDt, FAreaCode, FCode, FName, LoginUserCode);

            string title = "将离监人员名单";
            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(LoginUserCode + "_OutPrisonList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, title, strFileName);
            return Content("OK|" + LoginUserCode + "_OutPrisonList.xls");
        }

        private static StringBuilder GetLeavePrisonListSql(string startDt, string endDt, string FAreaCode, string FCode, string FName, string LoginUserCode)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select c.*,isnull((d.AmountA+d.AmountB+d.AmountC),0) JSMoney ");
            strSql.Append("select c.fcode 编号,c.fname 姓名,c.foudate 日期,c.fareaName 队别,c.strOutDate 出监日期,FStatus 办理状态,BankCardNo 银行卡号,isnull((d.AmountA+d.AmountB+d.AmountC),0) 结算金额 ");
            strSql.Append(@" from (
                    select a.fcode,a.fname,a.foudate,b.fcode FAreaCode,b.fname fareaName,a.foudate strOutDate
                    ,case isnull(FFlag,0) when 0 then '未结' else '已结算' end as FStatus,c.BankAccNo BankCardNo
                    from t_criminal a,t_area b,t_criminal_card c 
                    where a.fareacode=b.fcode and a.Fcode=c.fcrimecode ");
            if (string.IsNullOrEmpty(startDt) == false && string.IsNullOrEmpty(endDt) == false)
            {
                strSql.Append(" and foudate>='" + startDt + "' and foudate<'" + endDt + "' ");
            }

            strSql.Append(@" and fareacode in (
	                select fareaCode from t_czy_area where fflag=2 and fcode =" + LoginUserCode + ") ");
            if (string.IsNullOrEmpty(FAreaCode) == false)
            {
                strSql.Append(" and a.FAreaCode='" + FAreaCode + "' ");
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strSql.Append(" and a.FCode='" + FCode + "' ");
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strSql.Append(" and a.fname like '%" + FName + "%' ");
            }
            strSql.Append(") c");
            strSql.Append(" left outer join t_balanceList d on c.fcode=d.fcrimecode");
            strSql.Append(" order by c.FAreaCode,c.foudate;");
            return strSql;
        }

        public ActionResult SelectPrintOutBill()
        {
            string fcode = Request["FCode"];
            t_balanceList bal = new t_balanceListBLL().GetModelList("fcrimecode='" + fcode + "'").OrderByDescending(s => s.seqno).ToList()[0];
            if (bal.PayMode == 0)
            {
                return Redirect("PrintOutPrisonReport?FCode="+fcode);
            }
            else
            {
                return Redirect("NewPrintOutPrisonReport?FCode=" + fcode);
            }
        }
        public ActionResult PrintOutPrisonReport()
        {
            string fcode = Request["FCode"];
            ViewData["prove"] = "";
            t_balanceList bal = new t_balanceListBLL().GetModelList("fcrimecode='" + fcode + "'").OrderByDescending(s => s.seqno).ToList()[0];
            string sss = new T_TempLeavePrisonBLL().InsertBankProve(fcode, bal.PayMode);
            List<t_BankProve> proves = new t_BankProveBLL().GetModelList("fcode='" + fcode + "'");
            
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("SystemUnitName");
            if (mset != null)
            {
                proves[0].UnitName = mset.MgrName;
            }
            if (proves.Count > 0)
            {
                proves[0].CrtBy = bal.crtby;
                proves[0].AmountA = bal.AmountA;
                proves[0].AmountB = bal.AmountB;
                proves[0].AmountC = bal.AmountC;

                ViewData["prove"] = proves[0];
            }
            
            return View();
        }



        public ActionResult NewPrintOutPrisonReport()
        {
            string fcode = Request["FCode"];
            ViewData["prove"] = "";
            t_balanceList bal = new t_balanceListBLL().GetModelList("fcrimecode='" + fcode + "'").OrderByDescending(s => s.seqno).ToList()[0];
            string sss = new T_TempLeavePrisonBLL().InsertBankProve(fcode, bal.PayMode);
            List<t_BankProve> proves = new t_BankProveBLL().GetModelList("fcode='" + fcode + "'");
            var prove = proves[0];
            //List<t_balanceList> list = new t_balanceListBLL().GetModelList(" fcrimecode ='" + fcode + "'");
            //t_balanceList bal = list.OrderByDescending(p => p.crtdate).SingleOrDefault();
            T_Criminal_card card = new T_Criminal_cardBLL().GetModel(fcode);
            prove.PayMode = bal.PayMode;
            prove.BankCode = card.SecondaryBankCard;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("SystemUnitName");
            if (mset != null)
            {
                prove.UnitName = mset.MgrName;
            }
            if (bal.PayMode == 2)
            {
                T_Criminal_OutBankAccount acc = new BaseDapperBLL().GetModelFirst <T_Criminal_OutBankAccount, T_Criminal_OutBankAccount>(jss.Serialize( new { FCrimecode =fcode}));
                if (acc != null)
                {
                    prove.OutBankCard = acc.OutBankCard;
                    prove.OpeningBank = acc.OpeningBank;
                    prove.BankUserName = acc.BankUserName;
                    prove.OutBankRemark = acc.OutBankRemark;
                }
            }else if (bal.PayMode == 1)
            {
                T_Bank_PaymentRecord t = new BaseDapperBLL().GetModelFirst<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(jss.Serialize(new { FCrimeCode = fcode }));
                if (string.IsNullOrWhiteSpace(t.WithdrawalPassword))
                {
                    //生成取款密码
                    Random r1 = new Random();

                    int rowNo = r1.Next(100000, 999999);
                    t.WithdrawalPassword = rowNo.ToString();
                    new BaseDapperBLL().Update<T_Bank_PaymentRecord>(t);
                    
                }
                prove.WithdrawalPassword = t.WithdrawalPassword;

            }

            if (proves.Count > 0)
            {
                bal.PrintCount = bal.PrintCount + 1;
                prove.CrtBy = bal.crtby;
                prove.AmountA = bal.AmountA;
                prove.AmountB = bal.AmountB;
                prove.AmountC = bal.AmountC;
                prove.PrintCount = bal.PrintCount;
                ViewData["prove"] =prove;
            }

            new t_balanceListBLL().Update(bal);//更新打印数量
            return View();
        }




        //==========================处遇类型管理========================

        #region 处遇类型管理

        [MyLogActionFilterAttribute]
        public ActionResult DeleleCYType(string strId)//删除处遇类型
        {
            string strRes = "Err|删除失败";
            //string strId = Request["FCode"];
            if (new T_CY_TYPEBLL().Delete(strId))
            {
                strRes = "OK|删除成功";
            }
            return Content(strRes);
        }

        /// <summary>
        /// 保存处遇类型
        /// </summary>
        /// <param name="deleted"></param>
        /// <param name="inserted"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        [MyLogActionFilterAttribute]
        public ActionResult SaveCYTypeList(string deleted, string inserted, string updated)//保存处遇类型列表
        {
            //Request.("UTF-8");
            //取编辑数据 这里获取到的是json字符串

            //string deleted = Request["deleted"];

            //string inserted = Request["inserted"];

            //string updated = Request["updated"];


            //把json字符串转换成对象
            // List<T_SHO_AreaGoodMaxCount> listDeleted = JSON.parseArray(deleted, T_SHO_AreaGoodMaxCount);
            //TODO 下面就可以根据转换后的对象进行相应的操作了
            JavaScriptSerializer jss = new JavaScriptSerializer();
            JArray ja = new JArray();

            if (inserted != null)
            {
                ja = (JArray)JsonConvert.DeserializeObject(inserted);
            }

            if (updated != null)
            {
                ja = (JArray)JsonConvert.DeserializeObject(updated);
            }

            // 验证限额参是否正确，开启总限额后，总限额不能小于分账户限额
            string resCheck = "";
            resCheck = CYMoneyCheck(ja, resCheck);
            if (resCheck != "")
            {
                return Content("Err|" + resCheck);
            }

            if (ja.Count > 0)
            {
                List<T_CY_TYPE> models = SetCYTypeInfo(ja);
                return Content("OK保存成功！");
            }
            else
            {
                return Content("");
            }


        }

        /// <summary>
        /// 验证限额参是否正确，开启总限额后，总限额不能小于分账户限额
        /// </summary>
        /// <param name="ja"></param>
        /// <param name="resCheck"></param>
        /// <returns></returns>
        private static string CYMoneyCheck(JArray ja, string resCheck)
        {
            foreach (JObject o in ja)
            {
                decimal tot = Convert.ToDecimal(o["ftotamtmonth"].ToString());
                decimal fa = Convert.ToDecimal(o["famtmonth"].ToString());
                decimal fb = Convert.ToDecimal(o["FBamtMonth"].ToString());
                if (tot > 0)
                {
                    if (tot < fa || tot < fb)
                    {
                        resCheck = ("限额参不正确，开启总限额后，总限额不能小于分账户限额！");
                        break;
                    }
                }
            }
            return resCheck;
        }

        private static List<T_CY_TYPE> SetCYTypeInfo(JArray ja)
        {
            T_CY_TYPE model = new T_CY_TYPE();
            foreach (JObject o in ja)
            {
                model = new T_CY_TYPE();
                if (o["FCode"].ToString() != "")
                {
                    model.FCode = o["FCode"].ToString();
                }

                model.FName = o["FName"].ToString();
                model.ftotamtmonth = Convert.ToDecimal(o["ftotamtmonth"].ToString());
                model.famtmonth = Convert.ToDecimal(o["famtmonth"].ToString());
                model.FBamtMonth = Convert.ToDecimal(o["FBamtMonth"].ToString());
                model.Fbonusflag = Convert.ToInt32(o["Fbonusflag"].ToString());//劳动报酬留存标志，1是启用，2是不启用
                model.cpct = Convert.ToInt32(o["cpct"].ToString());//劳动报酬留存比率，如30%
                model.flag = Convert.ToInt32(o["flag"].ToString());//是否生效
                model.FPower = o["FPower"].ToString();//是否下放权限
                model.FamtLimit = Convert.ToDecimal(o["FamtLimit"].ToString());//存款最大限额
                model.fcamtlimit = Convert.ToDecimal(o["fcamtlimit"].ToString());//取款最大限额
                model.payaccount = Convert.ToInt32(o["payaccount"].ToString());//扣款优先账户
                model.FDesc = o["FDesc"].ToString();
                model.FDinnerAFlag = Convert.ToInt32(o["FDinnerAFlag"].ToString());//A账户加餐可用标志
                model.FDinnerBFlag = Convert.ToInt32(o["FDinnerBFlag"].ToString());//B账户加餐可用标志
                model.pct = 0;//A账户百份比
                model.bpct = 0;//B账户百份比
                //model.totpct = 0;//总账户百份比
                model.totpct = Convert.ToInt32(o["totpct"].ToString()); ;//加餐消费的最高额度
                model.FLimittype = 0;//限额类型
                model.FdaylimitAmt = 0;//每日最大限额标志
                model.FdayLimitflag = 0;//每日最大限额标志
                model.FTZSP_Money = Convert.ToInt32(o["FTZSP_Money"].ToString());//特种物品的购物可用金额
                model.FTZSP_Zero_Flag = Convert.ToInt32(o["FTZSP_Zero_Flag"].ToString());//特种物品归零_标志的购物可用金额
                model.JaRi_Cy_Money = Convert.ToInt32(o["JaRi_Cy_Money"].ToString());//中国传统节日的增加金额
                T_CY_TYPE m = new T_CY_TYPEBLL().GetModel(model.FCode);
                if (m != null)
                {
                    bool b = new T_CY_TYPEBLL().Update(model);
                }
                else
                {
                    new T_CY_TYPEBLL().Add(model);
                }
            }
            List<T_CY_TYPE> models = new T_CY_TYPEBLL().GetModelList("");
            return models;
        }

        public ActionResult GetCYTypesMgr()//获取消费类别管理
        {
            //商品类别
            List<T_CY_TYPE> saleTypes = new T_CY_TYPEBLL().GetModelList("");
            //ViewData["goodtypes"] = goodtypes;
            JavaScriptSerializer jss = new JavaScriptSerializer();

            return Content(jss.Serialize(saleTypes));
        }
        
        #endregion

	}

    public class xfSummaryInfo
    {
        public decimal Leijizongxf { get; set; }//累计总消费
        public int Tongjiyueshu { get; set; }//统计月数
        public decimal gxFood { get; set; }//刚需物品
        public decimal Yuejunxf { get; set; }//食品\日用品\小炒,月均消费
        public decimal Yufubeizi { get; set; }//衣服被褥
        public decimal Chaoshigouwu { get; set; }//超市购物
        public decimal Yaopinqita { get; set; }//药品、劳酬消费及其他
        public decimal Xiaochao { get; set; }//小炒
        public decimal Shukanbaozi { get; set; }//书刊报纸
        public decimal Jiaofajin { get; set; }//交罚金(总共)
        public decimal Zongshouru { get; set; }//总收入
        public decimal Huikuanshouru { get; set; }//其中汇款
        public decimal Laodongbaochou { get; set; }//劳动报酬
        public decimal Ningyongjin { get; set; }//零用金
        public decimal Zhanghuzongyue { get; set; }//账户总余额
        public decimal BuKeYongMoney { get; set; }//不可用金额
        public string BankAccCode { get; set; }//银行卡号
        public string FAreaName { get; set; }//队别名称
    }

     
    
}