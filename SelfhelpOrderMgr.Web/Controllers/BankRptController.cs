using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;
using System.Data;
using SelfhelpOrderMgr.BLL.ExtBLL;
using System.Text;
using SelfhelpOrderMgr.Web.Filters;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [LoginActionFilter]
    [MyLogActionFilterAttribute]
    public class BankRptController : BaseController
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        //
        // GET: /BankRpt/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BankDataReset()
        {
            return View();
        }

        public ActionResult BankSumReport()
        {
            return View();
        }

        public ActionResult GetBankDataInfo()
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"].ToString();
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            string succflag=Request["succflag"];
            string remark = Request["remark"];
            string rcvpay = Request["rcvpay"];
            DateTime startDate = Convert.ToDateTime(startDt);
            DateTime endDate = Convert.ToDateTime(endDt);

            if (action == "GetList" && !(string.IsNullOrEmpty(startDt)))
            {
                //DateTime startDate = Convert.ToDateTime(startDt);
                //DateTime endDate = Convert.ToDateTime(endDt);

                List<T_EdiMainOrder> list = (List<T_EdiMainOrder>)new T_EdiListBLL().GetListByDate(startDate, endDate, rcvpay,succflag,remark);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                if (list == null)
                {
                    list = new List<T_EdiMainOrder>();
                }
                if (list.Count > 0)
                {
                    Response.Write(jss.Serialize(list));
                }
                else
                {
                    Response.Write("");
                }
                
            }
            else if (action == "ExcelOutport" && !(string.IsNullOrEmpty(startDt)))
            {
                //DateTime startDate = Convert.ToDateTime(startDt);
                //DateTime endDate = Convert.ToDateTime(endDt);
                string rcvFlag = Request["rcvFlag"];
                string payFlag = Request["payFlag"];

                string strSql = @"select isnull(MainSeqno,'') 主单号,isnull(remark,'') 摘要,UploadDate 上传日期,case DetailDownloadflag* succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end 状态
                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end 复位情况
                         from t_EdiList
                        where code<>'yhyewj' and UploadDate>='" + startDt.ToString() + "' and UploadDate<'" + endDt.ToString() + "'";



                if (rcvpay == "pay")
                {
                    strSql = strSql + " and code='pldfwj'";
                }
                else if (rcvpay == "rcv")
                {
                    strSql = strSql + " and code='pldswj'";
                }

                if (string.IsNullOrEmpty(succflag) == false)
                {
                    strSql = strSql + " and succflag='" + succflag + "'";
                }

                if (string.IsNullOrEmpty(remark) == false)
                {
                    strSql = strSql + " and remark like '%" + remark + "%'";
                }

                strSql = strSql + " order by MainSeqno desc";

                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql);

                string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankDataEdiOrder.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, strFileName);
                return Content("OK|" + strLoginName + "_BankDataEdiOrder.xls");

            }

            
            else if (action == "resetFlag" && !(string.IsNullOrEmpty(startDt)))
            {
                //DateTime startDate = Convert.ToDateTime(startDt);
                //DateTime endDate = Convert.ToDateTime(endDt);
                string mainSeqno = Request["MainSeqno"];
                string uploadDate = Request["UploadDate"];
                string resultStr = "Error.上传日期无效";
                int bjDt = DateTime.Compare(Convert.ToDateTime(uploadDate), DateTime.Today);

                if (bjDt < 0)
                {
                    List<T_EdiMainOrder> MainList = (List<T_EdiMainOrder>)new T_EdiListBLL().GetListByMainSeqno( Convert.ToInt32(mainSeqno));
                    if (MainList[0].ResetFlag == "已复位")
                    {
                        resultStr = "Error.该记录已经复位了，无需再复位!";
                    }
                    else
                    {
                        if (MainList[0].SuccFlag == "已发送")
                        {
                            new T_EdiListBLL().UpdateByMainSeqno( Convert.ToInt32(mainSeqno), -2, 1);

                            resultStr = "OK.复位成功";
                        }
                        else
                        {
                            resultStr = "Error.只有处于【已发送】状态才可以复位！";
                        }
                    }
                }
                else
                {
                    resultStr = "Error.当天上传的数据不能复位";
                }
                return Content(resultStr);
            }
            else
            {
                List<T_VeryDay_RcvPay> list = new List<T_VeryDay_RcvPay>();
                JavaScriptSerializer jss = new JavaScriptSerializer();

                T_VeryDay_RcvPay m1 = new T_VeryDay_RcvPay();
                list.Add(m1);
                return Content(jss.Serialize(list));
            }
            return Content("");
        }


        public ActionResult EdiMainRcvPay()
        {
            return View();
        }

        public ActionResult GetEdiMainRcvPayInfo()
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"].ToString();
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            string rcvpay = Request["rcvpay"];
            if (action == "GetTmpRcvPay" && !(string.IsNullOrEmpty(startDt)))
            {
                DateTime startDate = Convert.ToDateTime(startDt);
                DateTime endDate = Convert.ToDateTime(endDt);

                List<T_TmpRcvPay> list = (List<T_TmpRcvPay>)new T_TmpRcvPayBLL().GetTmpRcvPay( startDate, endDate, rcvpay);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                if (list.Count <= 0)
                {
                    T_TmpRcvPay m1 = new T_TmpRcvPay();
                    list.Add(m1);
                }

                return Content(jss.Serialize(list));
            }
            else if (action == "ExcelOutport" && !(string.IsNullOrEmpty(startDt)))
            {
                DateTime startDate = Convert.ToDateTime(startDt);
                DateTime endDate = Convert.ToDateTime(endDt);
                //string rcvFlag = Request["rcvFlag"];
                //string payFlag = Request["payFlag"];

                //if (rcvFlag != null && payFlag == null)
                //{
                //    rcvpay = "rcv";
                //}
                //else if (rcvFlag == null && payFlag != null)
                //{
                //    rcvpay = "pay";
                //}
                //else
                //{
                //    rcvpay = "all";
                //}
                
                string strSql = @"exec tmprcvPay '" + Convert.ToString(startDate) + "','" + Convert.ToString(endDate) + "','" + rcvpay + "'";
                new CommTableInfoBLL().ExecSql( strSql);

                //列名:AccNo	Dtype	paydate	Fmoney	SucMoney	ErrMoney
                strSql = @"select BankName 银行账户,AccNo 主单号,Dtype 类型,paydate 日期,Fmoney 总金额,SucMoney 成功金额,ErrMoney 失败金额,Remark 摘要 from t_tmpRcvpay order by paydate,accno,dtype";
                DataTable dt = new CommTableInfoBLL().GetDataTable( strSql);
                

                string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankRcvPaySum.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);
                ExcelRender.RenderToExcel(dt, strFileName);
                return Content("OK|" + strLoginName + "_BankRcvPaySum.xls");
            }
            else
            {
                List<T_TmpRcvPay> list = new List<T_TmpRcvPay>();
                JavaScriptSerializer jss = new JavaScriptSerializer();

                T_TmpRcvPay m1 = new T_TmpRcvPay();
                list.Add(m1);

                return Content(jss.Serialize(list));
            }
            return Content("");
        }

        /// <summary>
        /// 执行发送数据到银行扣款
        /// </summary>
        /// <returns></returns>

        public ActionResult DoSendBankData()
        {
            string action = Request["action"];
            if (action == "DoSendBankData")
            {
                try
                {
                    new CommTableInfoBLL().ExecSql("exec CreateBankDataAll ''");
                    return Content("OK|执行成功！");
                }
                catch
                {
                    return Content("Error|执行失败！");
                }
            }
            else
            {
                return Content("Error|参数不对！");
            }            
        }


        public ActionResult GetListByMainSeqno()
        {
            string mainSeqno=Request["mainSeqno"];
            if(string.IsNullOrEmpty( mainSeqno)){
                return Content("Err|主单号不能为空");
            }
            try{
                int i=Convert.ToInt32(mainSeqno);
            }
            catch{
                return Content("Err|主单号必须是数值类型的");
            }
            IEnumerable<T_EdiDetail> lists=new T_EdiDetailBLL().GetListByMainseqno(Convert.ToInt32(mainSeqno));
            return Content(jss.Serialize(lists));
        }


        public ActionResult GetMainList ()
        {
            string mainSeqno=Request["mainSeqno"];

            if (string.IsNullOrEmpty(mainSeqno))
            {
                return Content("Err|主单号不能为空");
            }
            try
            {
                int i = Convert.ToInt32(mainSeqno);
            }
            catch
            {
                return Content("Err|主单号必须是数值类型的");
            }
            //列名:AccNo	Dtype	paydate	Fmoney	SucMoney	ErrMoney
            string strSql = @"select a.MainSeqno,a.FCrimeCode 编号,b.fcriminal 姓名,b.dtype 类型,b.crtdate 消费日期,a.DAmount 收,a.Camount 支,case SuccFlag when 1 then '成功' when -1 then '失败' else '未返回' end 状态,a.remark 备注 from t_edidetail a left join t_vcrd b on a.vcrdseqno=b.seqno where mainseqno='" + mainSeqno + "'";
            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql);


            string strFileName = new CommonClass().GB2312ToUTF8("_BankRcvPaySum.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|" + "_BankRcvPaySum.xls");
        }


        #region 银行接口报表文件
        public ActionResult GetEdiBankSumList()
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;
            string action = Request["action"].ToString();
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            string succflag = Request["succflag"];
            string remark = Request["remark"];
            string rcvpay = Request["rcvpay"];
            DateTime startDate = Convert.ToDateTime(startDt);
            DateTime endDate = Convert.ToDateTime(endDt);

            if (action == "GetList" && !(string.IsNullOrEmpty(startDt)))
            {
                
                string strCode = "'pldfwj','pldswj'";
                if (rcvpay == "pay")
                {
                    strCode="'pldfwj'";
                }
                else if (rcvpay == "rcv")
                {
                    strCode = "'pldswj'";
                }

                List<T_EdiBankSumList> lists = (List<T_EdiBankSumList>)new T_EdiBankSumListBLL().GetModelListByPage(1, 500, startDate, endDate, strCode, remark, succflag);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                if (lists == null)
                {
                    lists = new List<T_EdiBankSumList>();
                }
                if (lists.Count > 0)
                {
                    Response.Write(jss.Serialize(lists));
                }
                else
                {
                    Response.Write("");
                }

            }
            else if (action == "ExcelOutport" && !(string.IsNullOrEmpty(startDt)))
            {
                //DateTime startDate = Convert.ToDateTime(startDt);
                //DateTime endDate = Convert.ToDateTime(endDt);
                string rcvFlag = Request["rcvFlag"];
                string payFlag = Request["payFlag"];

                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append(@"select ROW_NUMBER() OVER (ORDER BY a.mainseqno desc) AS 序号,a.mainseqno as 主单号,a.remark 摘要,substring(convert(varchar(20),a.crtdate,120),1,10) 日期
                ,[代发合计]=sum( b.damount )                
                ,[代发成功金额]=sum(case when b.succflag=1 then b.damount else 0 end)
                ,[代发失败金额]=sum(case when b.succflag=-1 then b.damount else 0 end)
                ,[代收成功金额]=sum(case when b.succflag=1 then b.camount else 0 end)
                ,[未处理金额]=abs(sum(case when b.succflag=0 then b.damount-camount else 0 end))
                ,[回款合计]='' 
                ,case DetailDownloadflag* a.succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end as [状态]
                                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end as [复位状态]
                ,a.UpLoadDate 上传时间,a.DetailDownLoadDate 下载时间
                 From t_edilist a left join t_edidetail b on a.mainseqno=b.mainseqno");

                strSql.Append(@" where a.crtdate>='" + startDt + "' and a.crtdate<'" + endDt + "'  and a.remark LIKE ('%" + remark + "%') ");
                if (rcvpay == "pay")
                {
                    strSql.Append(@" and code='pldfwj'");
                }
                else if (rcvpay == "rcv")
                {
                    strSql.Append(@" and code='pldswj'");
                }

                if (succflag != "")
                {
                    strSql.Append(" and DetailDownloadflag* a.succflag=" + succflag);
                }
                strSql.Append(@" group by a.mainseqno,a.remark,substring(convert(varchar(20),a.crtdate,120),1,10),a.UpLoadDate,a.DetailDownLoadDate
                ,case DetailDownloadflag* a.succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end 
                ,case isnull(resetflag,0) when 1 then '已复位' else '' end ");
                strSql.Append(") b");
                //strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");

                DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());

                string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankDataEdiOrder.xls");
                strFileName = Server.MapPath("~/Upload/" + strFileName); ;
                //ExcelRender.RenderToExcel(dt, context, strFileName);//[8,9,10]
                int[] sumfields= {6,7,8};
                ExcelRender.RenderGroupbyToExcel(strFileName,dt, "中行对账汇总报表", 3, sumfields,9);
                return Content("OK|" + strLoginName + "_BankDataEdiOrder.xls");

            }
            else if (action == "resetFlag" && !(string.IsNullOrEmpty(startDt)))
            {
                //DateTime startDate = Convert.ToDateTime(startDt);
                //DateTime endDate = Convert.ToDateTime(endDt);
                string mainSeqno = Request["MainSeqno"];
                string uploadDate = Request["UploadDate"];
                string resultStr = "Error.上传日期无效";
                int bjDt = DateTime.Compare(Convert.ToDateTime(uploadDate), DateTime.Today);

                if (bjDt < 0)
                {
                    List<T_EdiMainOrder> MainList = (List<T_EdiMainOrder>)new T_EdiListBLL().GetListByMainSeqno(Convert.ToInt32(mainSeqno));
                    if (MainList[0].ResetFlag == "已复位")
                    {
                        resultStr = "Error.该记录已经复位了，无需再复位!";
                    }
                    else
                    {
                        if (MainList[0].SuccFlag == "已发送")
                        {
                            new T_EdiListBLL().UpdateByMainSeqno(Convert.ToInt32(mainSeqno), -2, 1);

                            resultStr = "OK.复位成功";
                        }
                        else
                        {
                            resultStr = "Error.只有处于【已发送】状态才可以复位！";
                        }
                    }
                }
                else
                {
                    resultStr = "Error.当天上传的数据不能复位";
                }
                return Content(resultStr);
            }
            else
            {
                List<T_VeryDay_RcvPay> list = new List<T_VeryDay_RcvPay>();
                JavaScriptSerializer jss = new JavaScriptSerializer();

                T_VeryDay_RcvPay m1 = new T_VeryDay_RcvPay();
                list.Add(m1);
                return Content(jss.Serialize(list));
            }
            return Content("");
        }

        public ActionResult PrintBankDataReport()
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"].ToString();
            string startDt = Request["startDate"];
            string endDt = Request["endDate"];
            string succflag = Request["succflag"];
            string remark = Request["remark"];
            string rcvpay = Request["rcvpay"];
            DateTime startDate = Convert.ToDateTime(startDt);
            DateTime endDate = Convert.ToDateTime(endDt);
            string strCode = "'pldfwj','pldswj'";
            if (rcvpay == "pay")
            {
                strCode = "'pldfwj'";
            }
            else if (rcvpay == "rcv")
            {
                strCode = "'pldswj'";
            }
            List<T_EdiBankSumList> lists = (List<T_EdiBankSumList>)new T_EdiBankSumListBLL().GetModelListByPage(1, 500, startDate, endDate, strCode, remark, succflag);
            int dayRows = 0;
            decimal daySum = 0;
            int dayFirst = 0;
            string dt = "";
            for (int j = 0; j < lists.Count; j++)
            {
                lists[j].dayRows = "";
                if (dt != lists[j].CrtDate.ToString())
                {
                    lists[dayFirst].dayRows = dayRows.ToString(); 
                    lists[dayFirst].daySum = daySum;
                    dayFirst = j;
                    dayRows = 0;
                    daySum = 0;
                }
                daySum = daySum + lists[j].DfFailMoney + lists[j].DsSuccMoney + lists[j].NodoMoney;
                dayRows++;
                dt = lists[j].CrtDate.ToString();

            }
            //最后两行的处理
            lists[dayFirst].dayRows = dayRows.ToString();
            lists[dayFirst].daySum = daySum;

            ViewData["lists"] = lists;
            return View();
        }

        #endregion

	}
}