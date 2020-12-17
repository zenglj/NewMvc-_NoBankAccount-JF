using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
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
    public class BankRcvPayController : Controller
    {
        //
        // GET: /BankRcvPay/
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public ActionResult Index(int id=1)
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2 and fcode='" + Session["loginUserCode"].ToString() + "')");
            ViewData["areas"] = areas;

            List<T_CY_TYPE> cys = new T_CY_TYPEBLL().GetModelList("");
            ViewData["cys"] = cys;

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
            ViewData["paytypes"] = paytypes;

            ViewData["id"] = id;
            return View();
        }

        public ActionResult GetSearchLists(int id=1)
        {

            string strWhere = SetRequestSearchWhere(id,Session["loginUserCode"].ToString());

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
            if(id==1)
            {
                listRows = new t_bank_RcvListBLL().GetModelList(strWhere.ToString()).Count;
                List<t_bank_RcvList> vcrds = (List < t_bank_RcvList >) new t_bank_RcvListBLL().GetPageModelList(page, row, strWhere, "seqno");
                sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(vcrds) + "}";
                return Content(sss);
            }
            else
            {
                listRows = new t_bank_PayListBLL().GetModelList(strWhere.ToString()).Count;
                List<t_bank_PayList> vcrds = (List<t_bank_PayList>)new t_bank_PayListBLL().GetPageModelList(page, row, strWhere, "seqno");
                sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(vcrds) + "}";
                return Content(sss);
            }

            
        }

        //将Request参数传入GetSearchWhere函数，并生成查询条件
        private string SetRequestSearchWhere(int id,string LoginCode)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            //string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            //string cyName = Request["cyName"];
            //string CrtBy = Request["CrtBy"];
            //string CriminalFlag = Request["CriminalFlag"];//是否在押
            //string CashTypes = Request["CashTypes"];//存款类型
            //string PayTypes = Request["PayTypes"];//取款类型
            //string AccTypes = Request["AccTypes"];//账户类型
            string BankFlags = Request["BankFlags"];//银行状态类型

            string strWhere = GetSearchWhere(id,LoginCode, ref startTime, ref endTime,  FName, FCode, BankFlags);
            return strWhere;
        }

        //生成Vcrd查询条件
        private static string GetSearchWhere(int id,string LoginCode, ref string startTime, ref string endTime,  string FName, string FCode,  string BankFlags)
        {
            string strWhere = " 1=1 ";
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
                if(id==1)
                {
                    strWhere = strWhere + " and  RcvDate>='" + startTime + "'";
                }
                else
                {
                    strWhere = strWhere + " and  PayDate>='" + startTime + "'";
                }
            }
            if (string.IsNullOrEmpty(startTime) == false)
            {
                if (id == 1)
                {
                    strWhere = strWhere + " and  RcvDate<'" + endTime + "'";
                }else
                {
                    strWhere = strWhere + " and  PayDate<'" + endTime + "'";
                }
                
            }

            
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FName like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = strWhere + " and FCrimeCode='" + FCode + "'";
            }
            
 
            if (string.IsNullOrEmpty(BankFlags) == false)
            {
                strWhere = strWhere + " and isnull(Flag,0) in (" + BankFlags + ")";
            }
            
            strWhere = strWhere + " and FCrimeCode in ( Select FCode from T_Criminal where FAreaCode in ( select fareaCode from t_czy_area where fflag=2 and fcode='" + LoginCode + "'))";
            return strWhere;
        }


        //打印用户汇总总表
        public ActionResult PrintCriminalSumOrder(int id = 1)
        {
            string strWhere = SetRequestSearchWhere(id,Session["loginUserCode"].ToString());

            StringBuilder strSql;
            string title;
            string result;
            SetSumOrderWhereSql(0, id, strWhere, out strSql, out title, out result);

            if (result != "")//Err|对不起,您传入错误的参数了
            {
                return Content(result);
            }

            ViewData["id"] = id;
            ViewData["title"] = title;

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;

            if (id == 1)
            {
                List<t_bank_RcvList> vcrds = new t_bank_RcvListBLL().CustomerQuery(strSql.ToString());
                ViewData["vcrds"] = vcrds;
            }
            else
            {
                List<t_bank_PayList> vcrds = new t_bank_PayListBLL().CustomerQuery(strSql.ToString());
                ViewData["vcrds"] = vcrds;
            }

            return View();
        }

        //Excel导出用户汇总总表
        public ActionResult ExcelCriminalSumOrder(int id = 1)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strWhere = SetRequestSearchWhere(id,Session["loginUserCode"].ToString());
            StringBuilder strSql;
            string title;
            string result;
            SetSumOrderWhereSql(1, id, strWhere, out strSql, out title, out result);

            if (result != "")//Err|对不起,您传入错误的参数了
            {
                return Content(result);
            }

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_SumOrder.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|" + strLoginName + "_SumOrder.xls");

            //return View();
        }

        private static void SetSumOrderWhereSql(int intFlag, int id, string strWhere, out StringBuilder strSql, out string title, out string result)
        {

            strSql = new StringBuilder();
            title = "";
            result = "";
            switch (id)
            {
                case 1://银行付收记录清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select AccNo,FCrimeCode,FName,Amount,SuccFlag,RcvDate,Remark,BankAccNo from t_bank_RcvList  ");

                        }
                        else
                        {
                            strSql.Append("select AccNo 主单号,FCrimeCode 编号,FName 姓名,Amount 金额,SuccFlag 标志,RcvDate 日期,Remark 摘要,BankAccNo 银行卡号 from t_bank_RcvList  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        //strSql.Append(" group by RvcDate,FCrimeCode ");
                        strSql.Append(" Order by RcvDate,FCrimeCode ;");
                        title = "银行付收记录清单";
                    } break;
                case 2://银行代付记录清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select AccNo,FCrimeCode,FName,Amount,SuccFlag,PayDate,Remark,BankAccNo from t_bank_PayList  ");

                        }
                        else
                        {
                            strSql.Append("select AccNo 主单号,FCrimeCode 编号,FName 姓名,Amount 金额,SuccFlag 标志,PayDate 日期,Remark 摘要,BankAccNo 银行卡号 from t_bank_PayList  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        //strSql.Append(" group by FCrimeCode,FCriminal ");
                        strSql.Append(" Order by PayDate,FCrimeCode ;");
                        title = "银行代付记录清单";
                    } break; 
                default:
                    {
                        result = "Err|对不起,您传入错误的参数了";
                        //return Content("Err|对不起,您传入错误的参数了");
                    }
                    break;

            }
        }
	}
}