using Rotativa.MVC;
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
    public class ReportController : BaseController
    {
        //
        // GET: /Report/
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
            ViewData["SaveType"] = new T_SavetypeBLL().GetModelList("typeflag='1'");

            return View();
        }

        //获取消费明细记录
        public ActionResult getInvList()
        {
            string invNo = Request["invNo"];
            if(string.IsNullOrEmpty(invNo))
            {
                return Content("Err|传入的消费单号不能为空");
            }
            T_Invoice invoice = new T_InvoiceBLL().GetModel(invNo);
            if (invoice!=null)
            {
                return Content("OK|"+ jss.Serialize(invoice));
            }
            else
            {
                return Content("Err|查找不到相应的记录");
            }
            
        }

        //获取消费类型并可判断该类型ID是否存在
        public ActionResult GetSaleType()
        {
            string saleId = Request["saleId"];
            if (string.IsNullOrEmpty(saleId))
            {
                return Content("Err|消费类型不能为空");
            }
            T_SHO_SaleType saletype = new T_SHO_SaleTypeBLL().GetModelList("TypeFlagId="+saleId)[0];
            if (saletype != null)
            {
                return Content("OK|" + jss.Serialize(saletype));
            }
            else
            {
                return Content("Err|查找不到相应的消费类型");
            }
            
        }


        public ActionResult SaveChangeType()
        {
            string invNo = Request["invNo"];
            string saveType = Request["saveType"];
            if (string.IsNullOrEmpty(invNo))
            {
                return Content("Err|消费单号不能为空");
            }
            List<T_Savetype> saveTypes = new T_SavetypeBLL().GetModelList("fcode='"+ saveType +"' and typeflag=1");
            if (saveTypes.Count <= 0)
            {
                return Content("Err|您所选的调账类型不正确");
            }

            T_Invoice invoice = new T_InvoiceBLL().GetModel(invNo);
            
            if (invoice != null)
            {
                if (new T_VcrdBLL().ChangeVcrdListType(invNo, saveTypes[0].fname, saveTypes[0].fcode))
                {
                    return Content("OK|保存修改成功");
                }
                else
                {
                    return Content("Err|保存修改失败");
                }
            }
            else
            {
                return Content("Err|查找不到相应的记录");
            }
            
        }
        public ActionResult GetSearchVcrds()
        {

            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

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
            
            listRows = new T_VcrdBLL().GetListCount(strWhere.ToString())[0];

            List<T_Vcrd> vcrds = new T_VcrdBLL().GetPageList(page, row, strWhere, "CrtDate,FCrimeCode");

            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(vcrds) + "}";
            return Content(sss);
        }

        //将Request参数传入GetSearchWhere函数，并生成查询条件
        private string SetRequestSearchWhere(string LoginCode,int id=1)
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string cyName = Request["cyName"];
            string CrtBy = Request["CrtBy"];
            string CriminalFlag = Request["CriminalFlag"];//是否在押
            string CashTypes = Request["CashTypes"];//存款类型
            string PayTypes = Request["PayTypes"];//取款类型
            string AccTypes = Request["AccTypes"];//账户类型
            string BankFlags = Request["BankFlags"];//银行状态类型
            string FRemark = Request["FRemark"];//备注
            string FFlags = Request["FFlags"];//记录的有效状态值
            string CheckFlag = Request["CheckFlag"]; //审核标志
            string CardTypeFlag = Request["CardTypeFlag"];//烛光卡表示
            if (string.IsNullOrEmpty(FFlags)==true)
            {
                FFlags = "0";
            }

            string strWhere = GetSearchWhere(LoginCode, ref startTime, ref endTime, areaName, FName, FCode, CrtBy, CriminalFlag, CashTypes, PayTypes, AccTypes, BankFlags, FRemark, FFlags, CheckFlag, CardTypeFlag, id);
            return strWhere;
        }

        //生成Vcrd查询条件
        private static string GetSearchWhere(string LoginCode, ref string startTime, ref string endTime, string areaName, string FName, string FCode, string CrtBy, string CriminalFlag, string CashTypes, string PayTypes, string AccTypes, string BankFlags, string FRemark, string FFlags,string CheckFlag,string CardTypeFlag, int id=1)
        {
            //string strWhere = "Flag=0 ";
            string strWhere = "Flag in("+ FFlags+") ";
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
                if (id != 26)
                {
                    strWhere = strWhere + " and  CrtDate>='" + startTime + "'";
                }                
            }
            if (string.IsNullOrEmpty(startTime) == false)
            {
                strWhere = strWhere + " and  CrtDate<'" + endTime + "'";
            }

            if (string.IsNullOrEmpty(areaName) == false)
            {
                if (areaName != "请选择队别")
                {
                    //strWhere = strWhere + " and FAreaName='" + areaName + "'";
                    strWhere = strWhere + @" and fcrimecode in( select fcode from t_Criminal 
                        where FAreaCode in(select fcode from t_area where fcode='"+ areaName +@"' or fid in(
                        select id from t_area where fcode='" + areaName + "')))";
                }
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strWhere = strWhere + " and FCriminal like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strWhere = strWhere + " and FCrimeCode='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(CrtBy) == false)
            {
                strWhere = strWhere + " and CrtBy='" + CrtBy + "'";
            }
            string savePays = "";
            if (string.IsNullOrEmpty(CashTypes) == false)
            {
                savePays = CashTypes;
            }
            if (string.IsNullOrEmpty(PayTypes) == false)
            {
                if (savePays == "")
                {
                    savePays = PayTypes;
                }
                else
                {
                    savePays = savePays + "," + PayTypes;
                }

            }
            if (!string.IsNullOrWhiteSpace(CheckFlag))
            {
                strWhere = strWhere + " and isnull(checkflag,0)= " + CheckFlag + "";
            }
            if (string.IsNullOrEmpty(savePays) == false)
            {
                strWhere = strWhere + " and Dtype in (" + savePays + ")";
            }

            if (string.IsNullOrEmpty(AccTypes) == false)
            {
                strWhere = strWhere + " and AccType in (" + AccTypes + ")";
            }

            if (string.IsNullOrEmpty(BankFlags) == false)
            {
                strWhere = strWhere + " and isnull(BankFlag,0) in (" + BankFlags + ")";
            }
            if (string.IsNullOrEmpty(CriminalFlag) == false)
            {
                strWhere = strWhere + " and FCrimeCode in ( Select FCode from T_Criminal where isnull(FFlag,0)=" + CriminalFlag + ")";
            }
            if (string.IsNullOrEmpty(FRemark) == false)
            {
                strWhere = strWhere + " and Remark like '%" + FRemark + "%' ";
            }
            //烛光卡状态
            if (!string.IsNullOrWhiteSpace(CardTypeFlag))
            {
                strWhere = strWhere + @" and fcrimecode in( select fcrimecode from t_Criminal_Card 
                        where (case when isnull(Bankaccno,'')<>'' then 1 else 0 end )='"+CardTypeFlag+"')";
            }

            //验证用户的队别,如果设定了Vcrd验证用户队别，则要查看是否有相应的队别权限下的犯人才可以查询到
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");
            if (mset != null)
            {
                if (mset.MgrValue == "1")
                {
                    strWhere = strWhere + " and FCrimeCode in ( Select FCode from T_Criminal where FAreaCode in ( select fareaCode from t_czy_area where fflag=2 and fcode='" + LoginCode + "'))";
                }
            }
            return strWhere;
        }

        //打印用户汇总总表
        public ActionResult PrintCriminalSumOrder(int id=1)
        {
            string CashTypes = Request["CashTypes"];//存款类型
            string PayTypes = Request["PayTypes"];//取款类型

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString());

            StringBuilder strSql;
            string title;
            string result;
            bool mul_lan = false;
            SetSumOrderWhereSql(0, id, strWhere, out strSql, out title, out result, out mul_lan);

            if (result != "")//Err|对不起,您传入错误的参数了
            {
                return Content(result);
            }

            ViewData["id"] = id;
            ViewData["title"] = title;
            
            object param = null;
            if (id == 26)
            {
                param = new { endTime = endTime };
                title = title;
            }
            //ViewData["id"] = id;
            //ViewData["title"] = title;
            //List<T_Vcrd> vcrds = new T_VcrdBLL().CustomerQuery(strSql.ToString());
            //ViewData["vcrds"] = vcrds;
            List<T_Vcrd> vcrds;
            if (param != null)
            {
                vcrds = new CommTableInfoBLL().GetList<T_Vcrd>(strSql.ToString(), param).ToList();
            }
            else
            {
                vcrds = new T_VcrdBLL().CustomerQuery(strSql.ToString());
            }
            

            

           

            //上期结存金额

            T_Vcrd shangqiJinE = new T_Vcrd();
            if (string.IsNullOrEmpty(startTime)==false)
            {
                shangqiJinE = new T_VcrdBLL().CustomerQuery("Select '上期结存' DType,sum(DAmount-CAmount) DAmount from t_vcrd where flag=0 and not (fcrimecode is null) and crtdate<'"+ startTime +"'")[0];
            }
            else
            {
                shangqiJinE.DType = "上期结存";
                shangqiJinE.DAmount = 0;
            }
            if (!string.IsNullOrEmpty(CashTypes))
            {

            }
            
            ViewData["vcrds"] = vcrds;
            ViewData["shangqiJinE"] = shangqiJinE;

            ViewData["startTime"]=startTime;
            ViewData["endTime"] = endTime;
            string crtby="";
            T_CZY czy = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString());
            if(czy!=null){
                crtby=czy.FName;
            }
            ViewData["crtby"] = crtby;

            ViewData["Dtype"] = CashTypes + "," + PayTypes;
            //return View();
            //var model = new TestViewModel { DocTitle = id, DocContent = "This is a test with a partial view" };
            return new PartialViewAsPdf();
        }

        //Excel导出用户汇总总表
        public ActionResult ExcelCriminalSumOrder(int id = 1)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string strWhere = SetRequestSearchWhere(Session["loginUserCode"].ToString(),id);
            StringBuilder strSql;
            bool mul_lan = false;
            string title;
            string result;


            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string strCountTime = string.Format("统计期间:{0}--{1}",startTime,endTime);

            SetSumOrderWhereSql(1,id, strWhere, out strSql, out title, out result,out mul_lan);

            object param = null;
            if (result != "")//Err|对不起,您传入错误的参数了
            {
                return Content(result);
            }
            if (id == 26)
            {
                param = new { endTime = endTime };
                title = title;
            }
            //ViewData["id"] = id;
            //ViewData["title"] = title;
            //List<T_Vcrd> vcrds = new T_VcrdBLL().CustomerQuery(strSql.ToString());
            //ViewData["vcrds"] = vcrds;
            DataTable dt ;
            if (param != null)
            {
                dt = new CommTableInfoBLL().GetDataTable(strSql.ToString(),param);
            }
            else
            {
                dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            }
            
            
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_SumOrder.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            switch (id)
            {
                case 1://个人余额汇总
                    {
                        //string CashTypes = Request["CashTypes"];//存款类型
                        //string PayTypes = Request["PayTypes"];//取款类型
                        //if (string.IsNullOrEmpty(CashTypes) == false)
                        //{
                        //    title = title + "(" + CashTypes + ")";
                        //}
                        //if (string.IsNullOrEmpty(PayTypes) == false)
                        //{
                        //    title = title + "(" + CashTypes + ")";
                        //}
                        ExcelRender.RenderToExcel(dt, title, 5, strFileName, mul_lan, strCountTime);
                    }
                    break;
                case 22://双行模式（无明细日期）
                    {
                        string CashTypes = Request["CashTypes"];//存款类型
                        string PayTypes = Request["PayTypes"];//取款类型
                        if (string.IsNullOrEmpty(CashTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        if (string.IsNullOrEmpty(PayTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        ExcelRender.RenderToExcel(dt, title, 3, strFileName, mul_lan, strCountTime);
                    }break;
                case 23://双行模式 +有明细日期
                    {
                        string CashTypes = Request["CashTypes"];//存款类型
                        string PayTypes = Request["PayTypes"];//取款类型
                        if (string.IsNullOrEmpty(CashTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        if (string.IsNullOrEmpty(PayTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        ExcelRender.RenderToExcel(dt, title, 4, strFileName, mul_lan, strCountTime);
                    }break;
                case 24://单行模式
                    {
                        string CashTypes = Request["CashTypes"];//存款类型
                        string PayTypes = Request["PayTypes"];//取款类型
                        if (string.IsNullOrEmpty(CashTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        if (string.IsNullOrEmpty(PayTypes) == false)
                        {
                            title = title + "(" + CashTypes + ")";
                        }
                        ExcelRender.RenderToExcel(dt, title, 5, strFileName, mul_lan, strCountTime);
                    } break;
                case 26://单行模式
                    {
                        
                        ExcelRender.RenderToExcel(dt, title, 4, strFileName, mul_lan, strCountTime);
                    }
                    break;
                default:
                    {
                        ExcelRender.RenderToExcel(dt, title, strFileName, mul_lan);
                    }
                    break;

            }
            
            
            return Content("OK|" + strLoginName + "_SumOrder.xls");


            //return View();
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
        private static void SetSumOrderWhereSql(int intFlag, int id, string strWhere, out StringBuilder strSql, out string title, out string result, out bool mul_lan)
        {

            strSql = new StringBuilder();
            title = "";
            result = "";
            mul_lan = false;
            switch (id)
            {
                case 1://按个用户个人汇总表
                    {
                        if(intFlag==0)
                        {
                            strSql.Append("select FCrimeCode,FCriminal,b.FAreaName,Sum(Damount) Damount,Sum(Camount) Camount,b.Remark from T_VCrd a" +
                                ",(Select FCode ,FAreaName=(select fname from t_area where fcode=e.FAreaCode),case when isnull(e.fflag,0)=1 then '离监' when isnull(e.fflag,0)=2 then '保外' else '在押' end as Remark from T_Criminal e ) b   ");

                        }else
                        {
                            strSql.Append("select FCrimeCode 编号,FCriminal 姓名,b.FAreaName 队别,Sum(Damount) 收入,Sum(Camount) 支出,Sum(Damount-Camount) 余额,b.Remark 备注 from T_VCrd a" +
                                ",(Select FCode ,FAreaName=(select fname from t_area where fcode=e.FAreaCode),case when isnull(e.fflag,0)=1 then '离监' when isnull(e.fflag,0)=2 then '保外' else '在押' end as Remark from T_Criminal e ) b  ");
                        }
                        strSql.Append(" Where a.fcrimecode=b.FCode and " + strWhere);
                        strSql.Append(" group by a.FCrimeCode,FCriminal,b.FAreaName,b.Remark ");
                        strSql.Append(" Order by b.FAreaName,FCriminal,FCrimeCode ;");
                        title = "用户个人汇总报表";
                    } break;
                case 2://按个用户个人清表
                    {
                        if(intFlag==0)
                        {
                            strSql.Append("select Top 5000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,BankFlag from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志,Origid 消费流水单号 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by FCrimeCode,FCriminal,CrtDate ;");
                        title = "用户个人记录清单";
                    } break;
                case 3://按队别汇总总表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select FAreaName,DType,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select FAreaName 队别,DType 类型,Sum(Damount) 收入,Sum(Camount) 支出 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" group by FAreaName,DType ");
                        strSql.Append(" Order by FAreaName,DType ;");
                        title = "用户个人记录清单";
                    } break;
                case 4://按队别分类清表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,BankFlag,Remark from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志,Origid 消费流水单号,Remark 备注 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by FAreaCode,FAreaName,FCriminal,CrtDate ;");
                        title = "用户个人记录清单";
                    } break;
                case 5://按存取类型汇总总表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select TypeFlag,DType,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select TypeFlag 类型ID,DType 类型,Sum(Damount) 收入,Sum(Camount) 支出 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" group by TypeFlag,DType ");
                        strSql.Append(" Order by TypeFlag,DType ;");
                        title = "存取类型汇总报表";
                    } break;
                case 6://按存取类型分类清表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,BankFlag,Remark from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志,Origid 消费流水单号,Remark 备注 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by TypeFlag,DType,FAreaCode,CrtDate ;");
                        title = "存取类型分类清单报表";
                    } break;
                case 7://超市消费汇总表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select a.FAreaCode,a.FAreaName,a.Dtype,a.Damount+isnull( b.Damount,0) Damount,a.Camount+isnull( b.Camount,0) Camount from (");
                        }
                        else
                        {
                            strSql.Append("select a.FAreaCode 队别号,a.FAreaName 队别,a.Dtype 类型,a.Damount+isnull( b.Damount,0) 退货金额,a.Camount+isnull( b.Camount,0) 消费金额 from (");
                        }
                        strSql.Append("");
                        strSql.Append("select FAreaCode,FAreaName,Dtype,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        strSql.Append(" Where " + strWhere + " and TypeFlag=7 ");
                        strSql.Append(" group by FAreaCode,FAreaName,Dtype ");
                        strSql.Append(" ) a Left Outer join (");
                        strSql.Append("");
                        strSql.Append("select FAreaCode,FAreaName,Dtype,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        strSql.Append(" Where " + strWhere + " and TypeFlag=11 ");
                        strSql.Append(" group by FAreaCode,FAreaName,Dtype ");
                        strSql.Append(" ) b On a.FAreaCode=b.FAreaCode ");
                        strSql.Append(" order by a.FAreaCode,a.FAreaName,a.Dtype");
                        title = "超市消费汇总报表";
                    } break;
                case 8://明细记录清单
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,BankFlag,Remark from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志,Remark 备注 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by CrtDate,Vouno;");
                        title = "明细记录清单";
                    } break;
                case 9://按消费日期银行交易汇总报表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end Depositer,TypeFlag,DType,Damount,Camount,Remark from ( ");
                        }
                        else
                        {
                            strSql.Append("select case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end 状态,TypeFlag 类型ID,DType 类型,Damount 收入,Camount 支出,Remark 回款月份 from ( ");
                        }
                        strSql.Append("select Bankflag ,substring( Convert(varchar(20),SendDate,102),1,7) Remark,TypeFlag,DType,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");

                        //strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere +" and TypeFlag not in(5,6,14)");
                        strSql.Append(" group by Bankflag,substring( Convert(varchar(20),SendDate,102),1,7) ,TypeFlag,DType ");
                        strSql.Append(" ) x ");
                        strSql.Append(" Order by TypeFlag,DType ;");
                        title = "按消费日期银行交易汇总报表";
                    }break;
                case 10://银行交易明细清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 8000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,SendDate,case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end Depositer  from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 下单日期,SendDate 回款日期,BankFlag 银行标志 from T_VCrd  ");
                        }
                        //strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " and TypeFlag not in(5,6,14) ");
                        strSql.Append(" Order by BankFlag,TypeFlag,DType,FAreaCode,FCriminal,CrtDate ;");
                        title = "按消费日期银行交易明细清单";
                    } break;
                case 11://银行每日交易报表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select convert(datetime, convert(varchar(20), CrtDate,102)) CrtDate,SendDate,TypeFlag,DType,BankFlag,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select convert(datetime, convert(varchar(20), CrtDate,102)) 消费日期,SendDate 交易日期,TypeFlag 类型ID,DType 类型,BankFlag 回款标志,Sum(Damount) 收入,Sum(Camount) 支出 from T_VCrd  ");
                        }
                        //strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " and TypeFlag not in(5,6,14) ");
                        strSql.Append(" group by convert(datetime, convert(varchar(20), CrtDate,102)),SendDate,TypeFlag,DType,BankFlag ");
                        strSql.Append(" Order by convert(datetime, convert(varchar(20), CrtDate,102)),SendDate,TypeFlag,DType,BankFlag ;");
                        title = "按消费日期银行日交易分类报表";
                    } break;
                case 12:
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(@"select * from (
                                select a.fcrimecode,c.fname fcriminal,b.BankAccNo CardCode,a.Crtby CrtBy,0 Flag
                                ,a.CrtDate CrtDate,0 DAmount,(a.AmountA+a.AmountB+a.AmountC) CAmount,d.FName FAreaName,'离监取款' DType
                                from t_balanceList a inner join t_Criminal_Card b on a.fcrimecode=b.fcrimecode 
                                inner join t_criminal c on a.fcrimecode=c.fcode
                                inner join t_area d on c.FAreaCode=d.FCode
                                ) e  ");
                        }
                        else
                        {
                            strSql.Append(@"select fcrimecode 编号,fcriminal 姓名,CardCode 银行卡
                                ,FAreaName 队别,CAmount 余额,CrtDate 日期,DType 类型,CrtBy 操作员 from (
                                select a.fcrimecode,c.fname fcriminal,b.BankAccNo CardCode,a.Crtby CrtBy,0 Flag
                                ,a.CrtDate CrtDate,0 DAmount,(a.AmountA+a.AmountB+a.AmountC) CAmount,d.FName FAreaName,'离监取款' DType
                                from t_balanceList a inner join t_Criminal_Card b on a.fcrimecode=b.fcrimecode 
                                inner join t_criminal c on a.fcrimecode=c.fcode
                                inner join t_area d on c.FAreaCode=d.FCode
                                ) e");
                        }
                        //strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " ");
                        //strSql.Append(" group by convert(datetime, convert(varchar(20), CrtDate,102)),TypeFlag,DType ");
                        strSql.Append(" Order by convert(datetime, convert(varchar(20), CrtDate,102)),FAreaName,fcriminal ;");
                        title = DateTime.Now.Month.ToString()+"月份监狱需冻结出狱犯人卡清单";
                    }break;
                case 13://按回款日期银行交易汇总报表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end Depositer,TypeFlag,DType,Damount,Camount,Remark from ( ");
                        }
                        else
                        {
                            strSql.Append("select case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end 状态,TypeFlag 类型ID,DType 类型,Damount 收入,Camount 支出,Remark 消费月份 from ( ");
                        }
                        strSql.Append("select Bankflag ,substring( Convert(varchar(20),CrtDate,102),1,7) Remark,TypeFlag,DType,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");

                        strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " and BankFlag=2 and TypeFlag <>14");
                        strSql.Append(" group by Bankflag,substring( Convert(varchar(20),CrtDate,102),1,7) ,TypeFlag,DType ");
                        strSql.Append(" ) x ");
                        strSql.Append(" Order by TypeFlag,DType ;");
                        title = "按回款日期银行交易汇总报表";
                    } break;
                case 14://按回款日期银行交易明细清单
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 8000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,SendDate,case Bankflag  when 2 then '成功' when 3 then '手动成功' when 4 then '手动成功' else  '失败' end Depositer  from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 下单日期,SendDate 回款日期,BankFlag 银行标志 from T_VCrd  ");
                        }
                        strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " and BankFlag=2 and TypeFlag <>14 ");
                        strSql.Append(" Order by BankFlag,TypeFlag,DType,FAreaCode,FCriminal,CrtDate ;");
                        title = "按回款日期银行交易明细清单";
                    } break;
                case 15://按回款日期银行每日交易报表
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append("select convert(datetime, convert(varchar(20), CrtDate,102)) crtdate,convert(datetime, convert(varchar(20), SendDate,102)) SendDate,TypeFlag,DType,BankFlag,Sum(Damount) Damount,Sum(Camount) Camount from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select convert(datetime, convert(varchar(20), CrtDate,102)) 消费月份,convert(datetime, convert(varchar(20), SendDate,102)) 交易日期,TypeFlag 类型ID,DType 类型,BankFlag 回款标志,Sum(Damount) 收入,Sum(Camount) 支出 from T_VCrd  ");
                        }
                        strWhere = strWhere.Replace("CrtDate", "SendDate");
                        strSql.Append(" Where " + strWhere + " and TypeFlag <>14 ");
                        strSql.Append(" group by convert(datetime, convert(varchar(20), CrtDate,102)),convert(datetime, convert(varchar(20), SendDate,102)),TypeFlag,DType,BankFlag ");
                        strSql.Append(" Order by convert(datetime, convert(varchar(20), CrtDate,102)),convert(datetime, convert(varchar(20), SendDate,102)),TypeFlag,DType,BankFlag ;");
                        title = "按回款日期银行日交易分类报表";
                    } break;
                case 20://个人消费明细清单
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000 Vouno,FCrimeCode,FCriminal,Dtype,DAmount,CAmount,FAreaName,FAreaCode,CrtDate,BankFlag,Remark from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 200000 Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志,Remark 备注 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by CrtDate,Vouno;");
                        title = "消费记录清单";
                    } break;
                case 21://罚金清单打印
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000 FAreaName,FCrimeCode,FCriminal,DAmount,CAmount,Dtype,Remark,FAreaCode=(select frealareaCode from t_criminal where fcode=t_vcrd.fcrimecode),Vouno=(select f.fname from t_criminal e,t_crime f where e.fcrimecode=f.fcode and e.fcode=t_vcrd.fcrimecode),CrtDate,BankFlag from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select Top 5000 FAreaName 队别,FCrimeCode 狱号,FCriminal 姓名,DAmount 收,CAmount 支,Dtype 类型,Remark 用途,案件号=(select frealareaCode from t_criminal where fcode=t_vcrd.fcrimecode),罪名=(select f.fname from t_criminal e,t_crime f where e.fcrimecode=f.fcode and e.fcode=t_vcrd.fcrimecode),CrtDate 日期,BankFlag 扣款状态 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" Order by FAreaName,FCriminal;");
                        title = "扣款记录清单";
                    } break;
                case 22://存取类型个人汇总打印
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000  fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10) crtdate,dtype,fareaName,remark,sum(damount) DAmount,sum(camount) CAmount from t_vcrd ");
                        }
                        else
                        {
                            //strSql.Append("select Top 5000 fcrimecode 编号,fcriminal 姓名,fareaName 队别,sum(damount-camount) 金额,remark 备注,dtype 类型 from t_vcrd  ");
                            strSql.Append("select Top 5000 fcrimecode 编号,fcriminal 姓名,fareaName 队别,sum(damount-camount) 金额 from t_vcrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" group by fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10),dtype,fareaName,fareaCode,remark ");
                        strSql.Append(" order by left(convert(varchar(20),crtdate,120),10) ,fareaCode,fcriminal");
                        title = "存取类型个人汇总单";
                        mul_lan = true;//设定为多栏打印
                    } break;
                case 23://存取类型个人汇总打印(+有日期)
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000  fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10) crtdate,dtype,fareaName,remark,sum(damount) DAmount,sum(camount) CAmount from t_vcrd ");
                        }
                        else
                        {
                            //strSql.Append("select Top 5000 fcrimecode 编号,fcriminal 姓名,fareaName 队别,sum(damount-camount) 金额,remark 备注,dtype 类型 from t_vcrd  ");
                            strSql.Append("select Top 5000 left(convert(varchar(20),crtdate,120),10) 日期,fcrimecode 编号,fcriminal 姓名,fareaName 队别,sum(damount-camount) 金额 from t_vcrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" group by fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10),dtype,fareaName,fareacode,remark ");
                        strSql.Append(" order by left(convert(varchar(20),crtdate,120),10) ,fareacode,fcriminal");
                        title = "存取类型个人汇总单";
                        mul_lan = true;//设定为多栏打印
                    } break;
                case 24://存取类型个人汇总打印
                    {
                        //Vouno 流水号,FCrimeCode 编号,FCriminal 姓名,Dtype 类型,DAmount 收入,CAmount 支出,FAreaName 队别,FAreaCode 队别号,CrtDate 日期,BankFlag 银行标志
                        if (intFlag == 0)
                        {
                            strSql.Append("select Top 5000  fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10) crtdate,dtype,fareaName,remark,sum(damount) DAmount,sum(camount) CAmount from t_vcrd ");
                        }
                        else
                        {
                            strSql.Append("select Top 5000 left(convert(varchar(20),crtdate,120),10) 日期,fareaName 队别,fcrimecode 编号,fcriminal 姓名,dtype 类型,sum(damount-camount) 金额,remark 备注 from t_vcrd  ");
                            //strSql.Append("select Top 5000 fcrimecode 编号,fcriminal 姓名,fareaName 队别,sum(damount-camount) 金额 from t_vcrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" group by fcrimecode,fcriminal,left(convert(varchar(20),crtdate,120),10),dtype,fareaName,fareaCode,remark ");
                        strSql.Append(" order by left(convert(varchar(20),crtdate,120),10) ,fareaCode,fcriminal");
                        title = "存取类型个人汇总单";
                        //mul_lan = true;//设定为多栏打印
                    } break;
                case 25:
                    {
                        
                        if (intFlag == 0)
                        {
                            strSql.Append("select b.*,a.AmountA,a.AmountB,a.AmountC,a.PayMode from t_balanceList a,(");
                            strSql.Append("select Top 5000 FCrimeCode,FCriminal,Dtype,FAreaName,FAreaCode,convert(varchar(10), CrtDate,120) as CrtDate ,BankFlag,Remark from T_VCrd  ");
                        }
                        else
                        {
                            strSql.Append("select b.*,a.AmountA,a.AmountB,a.AmountC,a.PayMode from t_balanceList a,(");
                            strSql.Append("select Top 200000 FCrimeCode 编号,FCriminal 姓名,Dtype 类型,FAreaName 队别,FAreaCode 队别号,convert(varchar(10), CrtDate,120) 日期,BankFlag 银行标志,Remark 备注 from T_VCrd  ");
                        }
                        strSql.Append(" Where " + strWhere);
                        strSql.Append(" and typeflag in(5,6) ");
                        strSql.Append(" group by FCrimeCode,FCriminal,Dtype,FAreaName,FAreaCode,convert(varchar(10), CrtDate,120),BankFlag,Remark ) b ");
                        strSql.Append(" where a.fcrimecode =b.fcrimecode and CONVERT(varchar(10), a.crtdate,120)=b.CrtDate");

                        strSql.Append(" Order by PayMode,FAreaCode,b.Crtdate ;");



                        title = "出监结算统计报表";
                        return;
                    }
                case 26:
                    {
                        if (intFlag == 0)
                        {
                            strSql.Append(@"select a.fcrimecode,fcriminal,c.FName As FAreaName,case b.fflag when 1 then '离监' when 2 then '保外' else '在押' end as Frealareacode,sum(DAMOUNT-CAMOUNT) as DAmount
                                    ,(case when isnull(e.BankAccNo,'')<>'' then '有' else '无' end) as Remark
                                from (select * from t_vcrd where " + strWhere + @") a left outer join T_CRIMINAL b on a.fcrimecode=b.FCode
                                left outer join T_AREA c on b.FAreaCode=c.FCode 
                                left outer join (select fcrimecode,CONVERT(varchar(10), dateadd(day,1,CRTDATE),120) OutDate from t_vcrd where flag=0 and typeflag=5
                                    group by fcrimecode,CONVERT(varchar(10), dateadd(day,1,CRTDATE),120)) d on a.fcrimecode=d.fcrimecode
                                left outer join T_Criminal_Card e on a.fcrimecode=e.fcrimecode
                                where a.fcrimecode=b.FCode and  a.flag=0 and a.CRTDATE<=@endTime  
                                GROUP by a.fcrimecode,fcriminal,c.FName,b.fflag,OutDate
                                    ,(case when isnull(e.BankAccNo,'')<>'' then '有' else '无' end)
                                having sum(DAMOUNT-CAMOUNT)<>0 or isnull(d.OutDate,getdate())>@endTime or isnull(b.fflag,0)=0
                                ");
                        }
                        else
                        {
                            strSql.Append(@"select a.fcrimecode as 编号, fcriminal as 姓名, c.FName As 队别,case b.fflag when 1 then '离监' when 2 then '保外' else '在押' end as 状态,sum(DAMOUNT - CAMOUNT) 金额
                                    ,(case when isnull(e.BankAccNo, '') <> '' then '有' else '无' end) as 卡标志
                                from(select * from t_vcrd where " + strWhere + @") a left outer join T_CRIMINAL b on a.fcrimecode = b.FCode
                                left outer join T_AREA c on b.FAreaCode = c.FCode
                                left outer join(select fcrimecode, CONVERT(varchar(10), dateadd(day, 1, CRTDATE), 120) OutDate from t_vcrd where flag = 0 and typeflag = 5
                                    group by fcrimecode, CONVERT(varchar(10), dateadd(day, 1, CRTDATE), 120)) d on a.fcrimecode = d.fcrimecode
                                left outer join T_Criminal_Card e on a.fcrimecode = e.fcrimecode
                                where a.fcrimecode = b.FCode and a.flag = 0 and a.CRTDATE <= @endTime
                                GROUP by a.fcrimecode,fcriminal,c.FName,b.fflag,OutDate
                                    ,(case when isnull(e.BankAccNo, '') <> '' then '有' else '无' end)
                                having sum(DAMOUNT-CAMOUNT)<> 0 or isnull(d.OutDate, getdate())> @endTime or isnull(b.fflag,0)=0
                            ");
                        }
                        title = "个人账户实点余额统计表";

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



        public ActionResult DeleteBankflagFailVcrd(int id=1)
        {
            string seqno = Request["Seqno"];
            string rs = "Err|未处理";
            if(string.IsNullOrEmpty(seqno)==false)
            {
                T_Vcrd vcrd = new T_VcrdBLL().GetModel( Convert.ToInt32(seqno));
                if (vcrd.BankFlag == -1)
                {
                    if(new T_VcrdBLL().DeleteDtlByVcrdSeqno(vcrd))
                    {
                        rs = "OK|删除成功";
                    }
                    else
                    {
                        rs = "Err|删除失败";
                    }
                }
                else
                {
                    rs = "Err|该记录不处于扣款失败的状态，不能删除";
                }
            }
            else
            {
                rs = "Err|您传入无效的记录，请确是否选中一条相应的记录";
            }

            return Content(rs);
        }


        //消费IC卡制卡清单
        public ActionResult CreateIcCardList()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("FCode in( select fareaCode from t_czy_area where fflag=2)");
            ViewData["areas"] = areas;


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

            return View();
        }

        public ActionResult GetIcCardList()
        {
            string strSql = GetSqlForIcCardList();


            //分页信息
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
            int page = 1;
            int row = 10;
            string sss = "";
            if (string.IsNullOrEmpty(strPage) == false)
            {
                page = Convert.ToInt32(strPage);
            }

            if (string.IsNullOrEmpty(strRow) == false)
            {
                row = Convert.ToInt32(strRow);
            }

            int startRow, endRow;
            startRow = (page - 1) * row;
            endRow = page*row -1;
            List<T_ICCARD_LIST> list = new CommTableInfoBLL().GetICCardListData(strSql);

            List<T_ICCARD_LIST> cards = new List<T_ICCARD_LIST>();
            if (list.Count > 0)
            {
                if (list.Count<=endRow)
                {
                    endRow = list.Count - 1;
                }
                for (int i = startRow; i <= endRow; i++)
                {
                    cards.Add(list[i]);
                    if (i >= endRow)
                    {
                        break;
                    }
                }
            }
                sss = "{\"total\":" + list.Count.ToString() + ",\"rows\":" + jss.Serialize(cards) + "}";
            return Content(sss);
        }

        private string GetSqlForIcCardList()
        {
            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string areaName = Request["areaName"];
            string FName = Request["FName"];
            string FCode = Request["FCode"];
            string CrtBy = Request["CrtBy"];
            string CriminalFlag = Request["CriminalFlag"];//是否在押
            string CashTypes = Request["CashTypes"];//存款类型

            string strSql = @"SELECT CardCode,FCrimeCode,fcriminal,fareaname,FRCZY,FRDate  FROM T_ICCArd_List T
                    WHERE 1=1";
            if (string.IsNullOrEmpty(startTime) == false && string.IsNullOrEmpty(endTime) == false)
            {
                strSql = strSql + " and FRDate>='" + startTime + "' and FRDate<'" + endTime + "'";
            }
            if (string.IsNullOrEmpty(areaName) == false)
            {
                strSql = strSql + " and FAreaName='" + areaName + "'";
            }
            if (string.IsNullOrEmpty(FName) == false)
            {
                strSql = strSql + " and FCriminal like '%" + FName + "%'";
            }
            if (string.IsNullOrEmpty(FCode) == false)
            {
                strSql = strSql + " and FCrimeCode='" + FCode + "'";
            }
            if (string.IsNullOrEmpty(CrtBy) == false)
            {
                strSql = strSql + " and FRCZY='" + CrtBy + "'";
            }
            switch (CashTypes)
            {
                case "'0'":
                    {
                        strSql = strSql + @" and CardCode in (
                        SELECT top 1 CardCode  FROM T_ICCArd_List 
                        WHERE FCrimeCode=T.FCrimeCode order by FCrimeCode asc )";
                    } break;
                case "'1'":
                    {
                        strSql = strSql + @" and CardCode not in (
                        SELECT top 1 CardCode  FROM T_ICCArd_List 
                        WHERE FCrimeCode=T.FCrimeCode order by FCrimeCode asc )";
                    } break;
                default:
                    break;

            }
            strSql = strSql + " order by FRDate asc";
            return strSql;
        }

        public ActionResult PrintIcCardList()
        {
            string strSql = GetSqlForIcCardList();
            List<T_ICCARD_LIST> list = new CommTableInfoBLL().GetICCardListData(strSql);



            string title = GetICCardTypeForTitle();

            ViewData["title"] = title;

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];

            ViewData["list"] = list;

            ViewData["startTime"] = startTime;
            ViewData["endTime"] = endTime;

            return View();
        }

        private string GetICCardTypeForTitle()
        {
            string title = "";
            string CashTypes = Request["CashTypes"];//存款类型
            switch (CashTypes)
            {
                case "'0'":
                    {
                        title = "首次制IC卡清单";
                    } break;
                case "'1'":
                    {
                        title = "IC卡制卡补卡清单";
                    } break;
                default:
                    {
                        title = "IC卡制卡所有清单";
                    }
                    break;

            }
            return title;
        }


        //Excel导出IcCard制卡清单
        public ActionResult ExcelIcCardList(int id = 1)
        {
            string strSql = GetSqlForIcCardList();
            string title = GetICCardTypeForTitle();
            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql);
            string strFileName = new CommonClass().GB2312ToUTF8("CreateICard_List.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, title, strFileName);
            return Content("OK|" + "CreateICard_List.xls");


            //return View();
        }



	}
}