using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
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
    public class FinaPayController : BaseController
    {
        // GET: /Laobao/

        JavaScriptSerializer jss = new JavaScriptSerializer();
        BaseDapperBLL _baseDapperBLL = new BaseDapperBLL();

        string strWherePay = " and isnull(FinancePayFlag,0)=0 and flag=0 and CAmount<>0 and Dtype in(select FName from T_CommonTypeTab where FTYpe='CWKM' and FRemark='支')";
        public ActionResult Index(int id = 1)
        {
            //监区队别
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            ViewData["areas"] = areas;
            List<T_CommonTypeTab> payTypes = new T_VcrdBLL().GetFinaType(0);
            ViewData["payTypes"] = payTypes;
            return View();
        }

        public ActionResult GetPayType()
        {
            
            List<T_CommonTypeTab> payTypes = new T_VcrdBLL().GetFinaType(0);
            return Content(jss.Serialize(payTypes));
        }
        public ActionResult GetAreaName()
        {
            List<T_AREA> areas = new T_AREABLL().GetModelList("fcode in(select fareacode From t_czy_area where fcode='" + Session["loginUserCode"].ToString() + "' and fflag=2)");
            return Content(jss.Serialize(areas));
        }
        public ActionResult getVcrds(int id = 1)
        {
            string action = Request["action"];
            string strPage = Request["page"];
            string strRow = Request["rows"];
            int page = 1;
            int row = 50;
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
            
            List<T_Vcrd> bonuies;
            if (action == "GetSearchMainOrder")
            {                
                //获取查询条件的SQL
                //StringBuilder strSql = GetSearchSql(strFPayKemu, strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName, strRtnMoneyFlag);
                StringBuilder strSql = GetSearchSql(Request);

                bonuies = new T_VcrdBLL().GetPageList(page, row, strSql.ToString() + strWherePay," CrtDate");
                listRows = new T_VcrdBLL().GetListCount(strSql.ToString() + strWherePay)[0];
            }
            else if (action == "GetAllMoney")
            {
                //获取查询条件的SQL
                //StringBuilder strSql = GetSearchSql(strFPayKemu, strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName, strRtnMoneyFlag);
                StringBuilder strSql = GetSearchSql(Request);
                bonuies = new T_VcrdBLL().GetPageList(page, row, strSql.ToString() + strWherePay, " CrtDate");
                decimal allmoney = new T_VcrdBLL().GetListCount(strSql.ToString() + strWherePay)[1];
                return Content(allmoney.ToString());
            }
            else
            {
                bonuies = new List<T_Vcrd>();
            }
            sss = "{\"total\":" + listRows.ToString() + ",\"rows\":" + jss.Serialize(bonuies) + "}";
            return Content(sss);
        }

        public JsonResult GetPayList(string strJsonWhere, string orderField = " Id asc ", int page = 1, int rows = 10)
        {
            var paramWhere = Newtonsoft.Json.JsonConvert.DeserializeObject<T_Bank_Rcv_Search>(strJsonWhere);

            PageResult<T_SHO_FinancePay> _r =_baseDapperBll.GetPageList<T_SHO_FinancePay, T_SHO_FinancePay_Search>(orderField, strJsonWhere, page, rows);

            return Json(_r);
        
        }

    public ActionResult getPaies(int id = 1)
        {
            string action = Request["action"];
            List<T_SHO_FinancePay> bonuies;
            if (action == "LoginIn")
            {
                bonuies = new T_SHO_FinancePayBLL().GetModelList("");
            }
            else if (action == "GetSearchMainOrder")
            {
                string strStartDate = Request["startDate"];//开始日期
                string strEndDate = Request["endDate"];//结束日期
                string strRemark = Request["FPayRemark"];//备注
                string strId = Request["FPayId"];//单号
                string strTitle = Request["FPayTitle"];//摘要    
                //获取查询条件的SQL

                int whereFlag = 0;
                StringBuilder strSql = new StringBuilder();
                //开始日期
                if (string.IsNullOrEmpty(strStartDate) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(" crtDt>='" + strStartDate + "' ");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(" and crtDt>='" + strStartDate + "' ");
                    }
                }
                //结束日期
                if (string.IsNullOrEmpty(strEndDate) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(" crtDt<'" + strEndDate + "' ");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(" and crtDt<'" + strEndDate + "' ");
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
                //编号
                if (string.IsNullOrEmpty(strId) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(" Id=" + strId + " ");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(" and Id=" + strId + " ");
                    }
                }
                //摘要
                if (string.IsNullOrEmpty(strTitle) == false)
                {
                    if (whereFlag == 0)
                    {
                        strSql.Append(" FTitle like '%" + strTitle + "%' ");
                        whereFlag = 1;
                    }
                    else
                    {
                        strSql.Append(" and FTitle like '%" + strTitle + "%' ");
                    }
                }
                List<T_SHO_FinancePay> list;
                list = (List<T_SHO_FinancePay>)new T_SHO_FinancePayBLL().GetModelList(strSql.ToString());
                if (list == null)
                {
                    list = new List<T_SHO_FinancePay>();
                    T_SHO_FinancePay m1 = new T_SHO_FinancePay();
                    list.Add(m1);
                }
                //string sss = "{\"total\":" + 0 + ",\"rows\":" + jss.Serialize(list) + "}";
                //context.Response.Write(sss);
                return Content(jss.Serialize(list));
            }
            else
            {
                bonuies = new List<T_SHO_FinancePay>();
            }

            return Content(jss.Serialize(bonuies));
        }

        private static StringBuilder GetSearchSql(string strFPayKemu, string strAreaName, string strStartDate, string strEndDate, string strRemark, string strFCrimeCode, string strFCrimeName, string strRtnMoneyFlag)
        {
            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();
            char ee = (char)44;
            //科目
            if (string.IsNullOrEmpty(strFPayKemu) == false)
            {
                string strpays = "";
                string[] fpaies = strFPayKemu.Split(ee);
                foreach (string fpay in fpaies)
                {
                    if (strpays=="")
                    {
                        strpays = "'" + fpay + "'";
                    }
                    else
                    {
                        strpays = strpays+",'" + fpay + "'";
                    }
                }
                if (whereFlag == 0)
                {
                    strSql.Append(" DType in(" + strpays + ") ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and DType in(" + strpays + ") ");
                }
            }
            //队别
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                string strareas = "";
                string[] areas = strAreaName.Split(ee);
                foreach (string area in areas)
                {
                    if (strareas == "")
                    {
                        strareas = "'" + area + "'";
                    }
                    else
                    {
                        strareas = strareas + ",'" + area + "'";
                    }
                }
                if (whereFlag == 0)
                {
                    strSql.Append(" FAreaName in(" + strareas + ") ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and FAreaName in(" + strareas + ") ");
                }
            }
            //开始日期
            if (string.IsNullOrEmpty(strStartDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtDate>='" + strStartDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtDate>='" + strStartDate + "' ");
                }
            }
            //结束日期
            if (string.IsNullOrEmpty(strEndDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtDate<'" + strEndDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtDate<'" + strEndDate + "' ");
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

            if (string.IsNullOrEmpty(strFCrimeCode) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" where FCrimeCode='" + strFCrimeCode + "'");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and FCrimeCode='" + strFCrimeCode + "'");
                }
            }

            if (string.IsNullOrEmpty(strFCrimeName) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" where fcriminal like '%" + strFCrimeName + "%'");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and fcriminal like '%" + strFCrimeName + "%'");
                }
            }
            if (string.IsNullOrEmpty(strRtnMoneyFlag) == false)
            {
                if (whereFlag == 0)
                {
                    if (strRtnMoneyFlag=="2")
                    {
                        strSql.Append(@" where BankFlag >="+strRtnMoneyFlag+"");
                    }
                    else
                    {
                        strSql.Append(@" where BankFlag <=" + strRtnMoneyFlag + "");
                    }
                    
                    whereFlag = 1;
                }
                else
                {
                    if (strRtnMoneyFlag == "2")
                    {
                        strSql.Append(@" and BankFlag >=" + strRtnMoneyFlag + "");
                    }
                    else
                    {
                        strSql.Append(@" and BankFlag <=" + strRtnMoneyFlag + "");
                    }
                }
            }
            
                        
            return strSql;
        }

        private static StringBuilder GetSearchSql(HttpRequestBase Request)
        {
            string strAreaName = Request["FAreaName"];//队别名称
            if (strAreaName == "请选择队别")
            {
                strAreaName = "";
            }//LingyongJin
            string strFPayKemu = Request["FPayKemu"];//科目类型
            if (strFPayKemu == "请选付款科目")
            {
                strFPayKemu = "";
            }//LingyongJin
            string strStartDate = Request["startDate"];//开始日期
            string strEndDate = Request["endDate"];//结束日期
            string strFCrimeCode = Request["FCrimeCode"];//编号
            string strFCrimeName = Request["FCrimeName"];//姓名
            string strRemark = Request["FRemark"];//备注
            string strRtnMoneyFlag = Request["rtnMoneyFlag"];//备注

            int whereFlag = 0;
            StringBuilder strSql = new StringBuilder();
            char ee = (char)44;
            //科目
            if (string.IsNullOrEmpty(strFPayKemu) == false)
            {
                string strpays = "";
                string[] fpaies = strFPayKemu.Split(ee);
                foreach (string fpay in fpaies)
                {
                    if (strpays == "")
                    {
                        strpays = "'" + fpay + "'";
                    }
                    else
                    {
                        strpays = strpays + ",'" + fpay + "'";
                    }
                }
                if (whereFlag == 0)
                {
                    strSql.Append(" DType in(" + strpays + ") ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and DType in(" + strpays + ") ");
                }
            }
            //队别
            if (string.IsNullOrEmpty(strAreaName) == false)
            {
                string strareas = "";
                string[] areas = strAreaName.Split(ee);
                foreach (string area in areas)
                {
                    if (strareas == "")
                    {
                        strareas = "'" + area + "'";
                    }
                    else
                    {
                        strareas = strareas + ",'" + area + "'";
                    }
                }
                if (whereFlag == 0)
                {
                    strSql.Append(" FAreaName in(" + strareas + ") ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and FAreaName in(" + strareas + ") ");
                }
            }
            //开始日期
            if (string.IsNullOrEmpty(strStartDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtDate>='" + strStartDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtDate>='" + strStartDate + "' ");
                }
            }
            //结束日期
            if (string.IsNullOrEmpty(strEndDate) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(" crtDate<'" + strEndDate + "' ");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(" and crtDate<'" + strEndDate + "' ");
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

            if (string.IsNullOrEmpty(strFCrimeCode) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" where FCrimeCode='" + strFCrimeCode + "'");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and FCrimeCode='" + strFCrimeCode + "'");
                }
            }

            if (string.IsNullOrEmpty(strFCrimeName) == false)
            {
                if (whereFlag == 0)
                {
                    strSql.Append(@" where fcriminal like '%" + strFCrimeName + "%'");
                    whereFlag = 1;
                }
                else
                {
                    strSql.Append(@" and fcriminal like '%" + strFCrimeName + "%'");
                }
            }
            if (string.IsNullOrEmpty(strRtnMoneyFlag) == false)
            {
                if (whereFlag == 0)
                {
                    if (strRtnMoneyFlag == "2")
                    {
                        strSql.Append(@" where BankFlag >=" + strRtnMoneyFlag + "");
                    }
                    else
                    {
                        strSql.Append(@" where BankFlag <=" + strRtnMoneyFlag + "");
                    }

                    whereFlag = 1;
                }
                else
                {
                    if (strRtnMoneyFlag == "2")
                    {
                        strSql.Append(@" and BankFlag >=" + strRtnMoneyFlag + "");
                    }
                    else
                    {
                        strSql.Append(@" and BankFlag <=" + strRtnMoneyFlag + "");
                    }
                }
            }


            return strSql;
        }

        public ActionResult SavePayOrder()//保存增加付款主单
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            string action = Request["action"];
            if(action=="Add")
            {
                string payType = Request["payType"];
                string payTitle = Request["payTitle"];
                string payMoney = Request["payMoney"];
                string payPosName = Request["payPosName"];
                string payBankCard = Request["payBankCard"];
                string payRemark = Request["payRemark"];

                string strAreaName = Request["fAreaName"];//队别名称
                if (strAreaName == "请选择队别")
                {
                    strAreaName = "";
                }
                string strFPayKemu = Request["FPayKemu"];//科目类型
                if (strFPayKemu == "请选付款科目")
                {
                    strFPayKemu = "";
                }
                string strStartDate = Request["startDate"];//开始日期
                string strEndDate = Request["endDate"];//结束日期
                string strRemark = Request["fRemark"];//结束日期
                string strFCrimeCode = Request["fCrimeCode"];//编号
                string strFCrimeName = Request["fCrimeName"];//姓名    
                string strRtnMoneyFlag = Request["rtnMoneyFlag"];//备注

                T_SHO_FinancePay payModel = new T_SHO_FinancePay();
                payModel.FType = payType;
                payModel.FTitle = payTitle;
                payModel.BankCard = payBankCard;
                payModel.PosName = payPosName;
                payModel.Remark = payRemark;
                payModel.FCount = 0;
                payModel.FMoney = Convert.ToDecimal( payMoney);
                payModel.CrtBy = strLoginName;
                payModel.CrtDt = DateTime.Today;
                payModel.PayBy = strLoginName;
                payModel.PayDate = DateTime.Today;
                payModel.Flag = 1;



                //获取查询条件的SQL
                StringBuilder strSql = GetSearchSql(strFPayKemu, strAreaName, strStartDate, strEndDate, strRemark, strFCrimeCode, strFCrimeName, strRtnMoneyFlag);
                T_SHO_FinancePay newPay = new T_SHO_FinancePayBLL().AddPayOrder(payModel, strSql.ToString() + strWherePay);
                return Content("OK|"+jss.Serialize(newPay));

            }



            return Content("Err|操作方法不正确");
            
        }

        public ActionResult DelMainOrder(string bid)
        {
            ResultInfo rs = new ResultInfo();
            try
            {
                var czy = _baseDapperBll.QueryModel<T_CZY>("FCode", base.loginUserCode);
                if (czy == null || czy.FPRIVATE != 1)
                {
                    rs.ReMsg = "Err|对不起，您不用删除付款主单，需要与管理员联系。";
                    return Json(rs);
                }

                _baseDapperBLL.ExecuteSql(@"update t_vcrd set FinancePayId=null,FinancePayFlag=0 where FinancePayId=@FinancePayId;
                                        delete from T_SHO_FinancePay where Id=@FinancePayId;"
                            , new { FinancePayId = bid });
                rs.Flag = true;
                rs.ReMsg = "OK|删除成功";
                return Json(rs);
            }
            catch (Exception ex )
            {
                rs.ReMsg = $"Err|{ex.Message}";
                throw;
            }
            
        }

        //删除明细记录
        public ActionResult DelOrderDetail()
        {
            string strFBid = Request["sbid"];//主单编号
            string strSeqno = Request["seqno"];//流水号
            if (new T_ProvideDTLBLL().Delete(Convert.ToInt32(strSeqno)))
            {
                new T_ProvideBLL().UpdateByCountMoney(strFBid);
                T_Provide model = new T_ProvideBLL().GetModel(strFBid);
                return Content("OK." + model.PId + "|" + model.FManNb + "|" + model.FWomNb + "|" + model.FAmount);
            }
            else
            {
                return Content("Error.删除失败");
            }
        }

 
        public ActionResult VcrdExcelOut()//Excel导出成功记录
        {
            //string strFBid = Request["FBidExcel"];
            StringBuilder strSql = GetSearchSql(Request);

            DataTable dt = new CommTableInfoBLL().GetDataTable("select seqno 序号,Dtype 类型,(Damount-camount) 金额,Crtdate 销售日期,FCrimeCode 编号,FCriminal 姓名,FAreaName 队别,case Bankflag when 2 then '已回款' when 3 then '手动回款' else '未回款' end 回款状态,senddate 回款日期,crtby 操作员 from t_vcrd where " + strSql.ToString() + strWherePay);
            string strFileName = new CommonClass().GB2312ToUTF8("Vcrd_List.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|" + "Vcrd_List.xls");
        }

        public ActionResult PayListOutport()//Excel导出成功记录
        {
            string strFBid = Request["payId"];
            
            DataTable dt = new CommTableInfoBLL().GetDataTable("select seqno 序号,Dtype 类型,(Damount-camount) 金额,Crtdate 销售日期,FCrimeCode 编号,FCriminal 姓名,FAreaName 队别,case Bankflag when 2 then '已回款' when 3 then '手动回款' else '未回款' end 回款状态,senddate 回款日期,crtby 操作员,isnull(FinancePayId,0) 付款单号 from t_vcrd where FinancePayId=" + strFBid);
            string strFileName = new CommonClass().GB2312ToUTF8("Vcrd_PayList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;
            //ExcelRender.RenderToExcel(dt, context, strFileName);
            ExcelRender.RenderToExcel(dt, strFileName);
            return Content("OK|" + "Vcrd_PayList.xls");
        }

 

        public ActionResult PrintReportList()//打印报表
        {
            string strbid = Request["bid"];
            T_SHO_FinancePay pay = new T_SHO_FinancePayBLL().GetModel(Convert.ToInt32(strbid));
            ViewData["pay"] = pay;
            return View();
        }

        public ActionResult PrintPayList()//打印报表清单
        {
            string strbid = Request["bid"];
            T_SHO_FinancePay pay = new T_SHO_FinancePayBLL().GetModel(Convert.ToInt32(strbid));
            ViewData["pay"] = pay;
            List<T_Vcrd> vcrds = new T_VcrdBLL().GetModelList("FinancePayId="+strbid);
            ViewData["vcrds"] = vcrds;
            return View();
        }


        //账户显示平台
        public ActionResult AccForm()
        {
            //获取银行最新的余额
            ResultInfo rs = new BankEnterpriseSerice().GetBankCardDateBalance(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1));

            T_Bank_DateBalance bal = _baseDapperBll.QueryList<T_Bank_DateBalance>("select Top 1 * from T_Bank_DateBalance order by baldat desc", null).FirstOrDefault();

            if (bal != null)
            {
                T_Bank_SubAccBalance subBal = _baseDapperBll.QueryModel<T_Bank_SubAccBalance>("AccountName", "BankAccount");
                subBal.AccountBalance = bal.avabal;
                subBal.AccountDate = bal.baldat;
                _baseDapperBll.Update(subBal);
            }

            //罪犯个人总额
            var sumMoney = new CommTableInfoBLL().ExtSqlGetModel<decimal>("select sum(Damount-CAmount) from T_Vcrd where flag=0", null);

            T_Bank_SubAccBalance prisonerBal = _baseDapperBll.QueryModel<T_Bank_SubAccBalance>("AccountName", "PersonerAccount");
            prisonerBal.AccountBalance = sumMoney;
            prisonerBal.AccountDate = DateTime.Now;
            _baseDapperBll.Update(prisonerBal);



            //超市消费
            decimal superBal = new CommTableInfoBLL().ExtSqlGetModel<decimal>("select isnull(sum(camount-damount),0) from T_vcrd where flag=0 and DTYPE in('超市消费','消费退货') and isnull(FinancePayFlag,0)=0", null);
                        
            _baseDapperBll.UpdatePartInfo<T_Bank_SubAccBalance>(
                new { AccountBalance = superBal, AccountDate = DateTime.Now }
                    , " AccountName=@AccountName "
                    , new { AccountName = "SuperCenterAccount" });

            //医院消费
            decimal hospitalBal = new CommTableInfoBLL().ExtSqlGetModel<decimal>("select isnull(sum(camount-damount),0) from T_vcrd where flag=0 and DTYPE in('医院消费','医院退货') and isnull(FinancePayFlag,0)=0", null);

            _baseDapperBll.UpdatePartInfo<T_Bank_SubAccBalance>(
                new { AccountBalance = hospitalBal, AccountDate = DateTime.Now }
                    , " AccountName=@AccountName "
                    , new { AccountName = "HospitalAccount" });


            List<T_Bank_SubAccBalance> ls = _baseDapperBLL.GetModelList<T_Bank_SubAccBalance>("");
            ViewData["balanceInfo"] = ls;
            return View();
        }

        /// <summary>
        /// 子账户设定管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SubAccMgr()
        {
            IEnumerable<T_Bank_SubAccBalance> model = _baseDapperBll.GetModelList<T_Bank_SubAccBalance>(""); // 获取你的数据
            return View(model);
        }
	}
}