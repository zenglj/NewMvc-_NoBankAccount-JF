using NPOI.SS.Formula.Functions;
using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Common.CustomExtend;
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
    [CustomActionFilterAttribute]
    public class BankPaymentController : Controller
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        T_Bank_PaymentRecordBLL _bllPay=new T_Bank_PaymentRecordBLL();

        private string crtby = "";

        //
        // GET: /BankPayment/
        public ActionResult Index()
        {
            ViewData["areas"] = new T_AREABLL().GetModelList("");


            //DataTable dt = new CommTableInfoBLL().GetDataTable("select distinct CrtBy from t_Vcrd Order by CrtBy");
            //List<string> crtbys = new List<string>();
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        crtbys.Add(row[0].ToString());
            //    }
            //}

            var crtbys=_bllPay.GetPageList<T_CZY, T_CZY>("FCode", "{}", 1, 100, "")
                .rows.Select(p => p.FName).ToList();
            ViewData["crtbys"] = crtbys;


            //dt = new CommTableInfoBLL().GetDataTable("select distinct typename from T_Trans_FeeList");
            //List<string> paytypes = new List<string>();//存款类型
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        paytypes.Add(row[0].ToString());
            //    }
            //}

            var paytypes = _bllPay.GetModelList<T_Trans_FeeList, T_Trans_BankAccount>("{}", "Id", 100)
                .Select(p=>p.TypeName).ToList();
            ViewData["paytypes"] = paytypes;

            return View();
        }
        public ActionResult GetBankPaymentList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            PageResult<ViewPaymentRecordExtend> list = new T_Bank_PaymentRecordBLL().GetPageList<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>("Id", strJsonWhere, page, rows);

            EasyUiPageResult<ViewPaymentRecordExtend> rs = new EasyUiPageResult<ViewPaymentRecordExtend>()
            {
                total = list.total,
                rows = list.rows
            };
            return Content(jss.Serialize(rs));
        }


        public ActionResult GetVcrdPaymentList(string strJsonWhere, int page = 1, int rows = 10, string sort = "seqno",string order= "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { seqno = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            PageResult<T_Vcrd> list = new T_Bank_PaymentRecordBLL().GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere, page, rows, " CAmount>0 and Flag=0 and isnull(PayAuditFlag,0)=0 and isnull(BankFlag,0)<2");

            EasyUiPageResult<T_Vcrd> rs = new EasyUiPageResult<T_Vcrd>()
            {
                total = list.total,
                rows = list.rows,
                sumMoney = list.rows.Sum(p => p.CAmount)
            };
            return Content(jss.Serialize(rs));
        }

        public ActionResult GetVcrdPaymentSumMoney(string strJsonWhere)
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { seqno = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            PageResult<T_Vcrd> list = new T_Bank_PaymentRecordBLL().GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", strJsonWhere,1,9999999, " CAmount>0 and Flag=0 and isnull(BankFlag,0)<2");

            EasyUiPageResult<T_Vcrd> rs = new EasyUiPageResult<T_Vcrd>()
            {
                total = 0,
                rows = null,
                sumMoney = list.rows.Sum(p => p.CAmount)
            };
            return Content(jss.Serialize(rs));
        }

        public ActionResult GetVcrdByMain(int mainId , int page = 1, int rows = 10, string sort = "seqno", string order = "asc")
        {
            PageResult<T_Bank_PaymentDetail> dtls = new T_Bank_PaymentRecordBLL().GetPageList<T_Bank_PaymentDetail, T_Bank_PaymentDetail>("Id", "{}", page, rows, " MainId="+ mainId +" ");
            EasyUiPageResult<T_Vcrd> rs = new EasyUiPageResult<T_Vcrd>();
            if (dtls.rows.Count > 0)
            {
                string seqnos = string.Join(",", dtls.rows.Select(p => p.Vcrdseqno).ToArray());
                PageResult<T_Vcrd> list = new T_Bank_PaymentRecordBLL().GetPageList<T_Vcrd, T_Vcrd_Search>("seqno", "{}", page, rows, " seqno in(" + seqnos + ") ");
                rs.total = list.total;
                if (list.rows.Count == 0)
                {
                    list.rows.Add(new T_Vcrd());
                }
                rs.rows = list.rows;
            }
            else
            {
                rs.rows=new List<T_Vcrd>();
            }

            return Content(jss.Serialize(rs));
        }


        #region 转账记录的审核/撤销功能

        [MyLogActionFilterAttribute]
        
        /// <summary>
        /// 审核-转账的记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="selectMulIds"></param>
        /// <returns></returns>

        [RemarkAttribute("审核-转账的记录")]
        public ActionResult AuditPayList(string strJsonWhere, string selectMulIds)
        {
            crtby = Request.Cookies["loginUserName"].Value;
            //string crtby = "admin";
            Int16 auditAction = 1;
            string otherStrWhere = " isnull(AuditFlag,0)=0 ";
            string strMsg = "审核";
            string result = DelegateAuditPayList(crtby, strJsonWhere, selectMulIds, auditAction, otherStrWhere, strMsg);
            return Content(result);

        }

        [MyLogActionFilterAttribute]
        [RemarkAttribute("撤销审核-转账的记录")]
        /// <summary>
        /// 撤销审核-转账的记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="selectMulIds"></param>
        /// <returns></returns>
        public ActionResult UnAuditPayList(string strJsonWhere, string selectMulIds)
        {
            //string crtby = "admin";
            crtby = Request.Cookies["loginUserName"].Value;
            Int16 auditAction = 0;
            string strCheckWhere = " isnull(AuditFlag,0)=1 and isnull(TranStatus,0)>0 ";
            if (selectMulIds != "")
            {
                strCheckWhere = strCheckWhere + " and Id in (" + selectMulIds + ") ";
            }
            var list = new T_Bank_PaymentRecordBLL().GetPageList<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>("Id", strJsonWhere, 1, 10, strCheckWhere);
            if (list.total > 0)
            {
                return Content("Error|您选中的记录，有记录已经发送到银行，无法撤销审核！");
            }

            string otherStrWhere = " isnull(AuditFlag,0)=1 and isnull(TranStatus,0)=0 ";
            string strMsg = "撤销审核";
            string result = DelegateAuditPayList(crtby, strJsonWhere, selectMulIds, auditAction, otherStrWhere, strMsg);
            return Content(result);
        }

        /// <summary>
        /// 审核/撤销审核
        /// </summary>
        /// <param name="crtby">操作员</param>
        /// <param name="strJsonWhere">查询条件</param>
        /// <param name="selectMulIds">选中的Id号(可多个)</param>
        /// <param name="auditAction">审核动作：1审核/0撤销审核</param>
        /// <param name="otherStrWhere">附加条件</param>
        /// <param name="strMsg">提示信息</param>
        /// <returns></returns>
        private string DelegateAuditPayList(string crtby, string strJsonWhere, string selectMulIds, Int16 auditAction, string otherStrWhere, string strMsg)
        {
            string[] ids;
            var list = new List<T_Bank_PaymentRecord>();
            if (!string.IsNullOrWhiteSpace(selectMulIds))
            {
                ids = selectMulIds.Split((char)',');
            }
            else
            {
                List<ViewPaymentRecordExtend> modelList = new T_Bank_PaymentRecordBLL().GetModelList<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>(strJsonWhere, "Id", 50000);
                ids = modelList.Select(p => p.Id.ToString()).ToArray();
            }
            foreach (var id in ids)
            {
                list.Add(new T_Bank_PaymentRecord()
                {
                    Id = Convert.ToInt32(id),
                    AuditFlag = auditAction,
                    AuditBy = crtby,
                    AuditDate = DateTime.Now
                });
            }

            var strUpdateJson = jss.Serialize(new { Id = "1", AuditFlag = 1, AuditBy = crtby, AuditDate = DateTime.Now });

            if (new T_Bank_PaymentRecordBLL().Update<T_Bank_PaymentRecord>(list, strUpdateJson, otherStrWhere))
            {
                return "OK|" + strMsg + "成功";
            }
            else
            {
                return "Error|" + strMsg + "失败";
            }
        }
        
        #endregion


        #region 应付款生成

        [MyLogActionFilterAttribute]
        public JsonResult PayListCreate(string strJsonWhere, string selectMulIds,string purposeInfo,int tranType)
        {
            crtby = Request.Cookies["loginUserName"].Value;
            
            T_Bank_PayRecBase prb=new T_Bank_PayRecBase(){
                TranType = tranType,//转账方式,1公对公，2快捷代发
                PayMode=(int)MoneyPayMode.TranAccount,//结算模式：转账
                PurposeInfo=purposeInfo//转账摘要
            };

            var rs = new T_Bank_PaymentRecordBLL().VcrdPayListCreate(crtby, prb, strJsonWhere, selectMulIds);            
            return Json(rs,JsonRequestBehavior.AllowGet);
        }

        #endregion


        /// <summary>
        /// 复位重发转账记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public ActionResult ResetCheckSend(int id,string remark)
        {
            T_Bank_PaymentRecord rec = new T_Bank_PaymentRecordBLL().GetModel<T_Bank_PaymentRecord>(id);
            
            if (rec == null || rec.TranStatus!=3 )
            {
                return Content("Err|Id不存在或是付款不是失败转态，无法复位");
            }

            string rs = new T_Bank_PaymentRecordBLL().ResetCheckSend(id, remark);
            return Content(rs);
        }

        /// <summary>
        /// 转账退款后复位重发
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public ActionResult ResetRefund(int id, string remark)
        {
            T_Bank_PaymentRecord rec = new T_Bank_PaymentRecordBLL().GetModel<T_Bank_PaymentRecord>(id);

            if (rec == null || rec.TranStatus != 2)
            {
                return Content("Err|Id不存在或是付款不是失败转态，无法复位");
            }
            List<T_Bank_TransDetail> dtls = new T_Bank_PaymentRecordBLL().GetModelList<T_Bank_TransDetail, T_Bank_TransDetail>(jss.Serialize(new { vchnum = remark ,direction =1}), "Id", 10);
            if (dtls.Count == 0)
            {
                return Content("Err|找不到对应的退账流水号，无法复位");
            }
            if (dtls[0].txnamt != rec.TranMoney)
            {
                return Content("Err|流水单退款金额与转账金额不一致，无法复位");
            }
            string rs = new T_Bank_PaymentRecordBLL().ResetRefund(id, "转账成功后又退款，单号:"+remark);
            return Content(rs);

        }



        #region 放弃领款

        public ActionResult AbandonMoney(string strJsonWhere, string selectMulIds,string remark)
        {
            crtby = Request.Cookies["loginUserName"].Value;
            //string crtby = "admin";
            Int16 auditAction = 1;
            string otherStrWhere = " isnull(AuditFlag,0)=0 and isnull(TranStatus,0)<1";
            otherStrWhere += " and Id in(" + selectMulIds + ")";

            var rs = _bllPay.GetPageList<T_Bank_PaymentRecord, T_Bank_PaymentRecord>("Id asc", strJsonWhere, 1, 10, otherStrWhere);

            //string strMsg = "放弃领款";
            if (rs.rows.Count != 1)
            {
                return Content($"Err|记录{rs.rows.Count}条，不能存在两条及以上的记录");
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append($"update T_Bank_PaymentRecord set AuditFlag=1,AuditDate='{DateTime.Now.ToString()}',AuditBy='{crtby}', TranStatus=5,BankResultInfo='用户主动放弃领款:{remark }' where Id={rs.rows[0].Id} ;");
            strSql.Append($" delete T_Bank_PaymentDetail where MainId={rs.rows[0].Id} ;");
            strSql.Append(@" update t_balanceList set PayMode=5 where seqno	in(
                select top 1  seqno from t_balanceList where FCrimeCode = '" + rs.rows[0].FCrimeCode + @"' order by crtdate desc
                ); ");
            new CommTableInfoBLL().ExecSql(strSql.ToString());

            return Content($"OK|设定放弃领款成功");
        }

        #endregion


        /// <summary>
        /// 密码此次归零
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult PwdCountReset(int id)
        {
            T_Bank_PaymentRecord rec = new T_Bank_PaymentRecordBLL().GetModel<T_Bank_PaymentRecord>(id);

            if (rec == null || rec.PayMode !=1)
            {
                return Content("Err|Id不存在或是付款方式不是ATM机取款模式");
            }
            rec.PwdErrCount = 0;
            try
            {
                new T_Bank_PaymentRecordBLL().Update<T_Bank_PaymentRecord>(rec);
                return Content("OK|归零成功");
            }
            catch (Exception e)
            {
                return Content("Err|" +e.Message.ToString());
            }
            

        }

        /// <summary>
        /// 查询转记录单
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult ExcelDetailList(string strJsonWhere)
        {
            string strWhere = new T_Bank_DepositListBLL().GetParamString<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>(strJsonWhere);
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            StringBuilder strSql = new StringBuilder();
            bool mul_lan = false;
            string title = "转账付款记录";
            strSql.Append(@"
SELECT  [Id]
      ,case [TranStatus] when 0 then '未处理' when 1 then '已发送' when 2 then '成功' when 3 then '失败' when 4 then '失败已复位重发' else '其他' end as 状态
	  ,[FCrimeName] as 姓名
      ,[FCrimeCode] as 编号
      ,case [TranType] when 0 then '公转私' when 1 then '公转私' else '其他' end 转账方式
      ,case [PayMode] when 0 then '现金/网点' when 1 then 'ATM取款' else '转账' end 结算方式
      ,[Amount] as 结算金额
      ,[ToBankId]
      ,[AuditFlag] as 审核
      ,[AuditBy] as 审核人
      ,[AuditDate] as 审核日期
      ,[TranMoney] as 取款金额
      ,[PurposeInfo] as 备注
      ,[TranDate] as 转账日期
      ,[ReturnTime] as 成功日期
      ,[BankObssid] as 银行流水
      ,[BankResultInfo]  as 银行结果
      ,[OutBankCard] as 收款卡号
      ,[BankUserName] as 收款户名
      ,[OutBankRemark] as 收款行
	  ,[Crtdate] as 创建日期
  FROM [dbo].[ViewPaymentRecordExtend] 
             ");
            if (string.IsNullOrWhiteSpace(strWhere) == false)
            {
                strSql.Append(" where " + strWhere);
            }

            string startTime = Request["startTime"];
            string endTime = Request["endTime"];
            string strCountTime = string.Format("统计期间:{0}--{1}", startTime, endTime);

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankPayList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;

            ExcelRender.RenderToExcel(dt, title, 11, strFileName, mul_lan, strCountTime);
            return Content("OK|" + strLoginName + "_BankPayList.xls");
        }


        /// <summary>
        /// 转账主单的VCR的明细记录
        /// </summary>
        /// <param name="mainId"></param>
        /// <returns></returns>
        public ActionResult ExcelVcrdDetailList(int mainId)
        {
            string strLoginName = new T_CZYBLL().GetModel(Session["loginUserCode"].ToString()).FName;

            StringBuilder strSql = new StringBuilder();
            bool mul_lan = false;
            string title = $"主单{mainId} 的VCRD明细记录";
            strSql.Append(@"
                select seqno ,fcrimecode as 狱号,fcriminal as 姓名,CRTDATE as 创建日期,DTYPE as 科目类型,DAMOUNT as 收入
                ,CAMOUNT as 支出,REMARK as 备注,fareaName as 队别,Bankflag as 银行标志,senddate as 发送日期 from t_Vcrd 
             ");
            strSql.Append(@" where seqno in(select VcrdSeqno from t_bank_PayMentDetail where mainId="+ mainId +" )");


            //string startTime = Request["startTime"];
            //string endTime = Request["endTime"];
            string strCountTime = string.Format("统计期间:~--~");

            DataTable dt = new CommTableInfoBLL().GetDataTable(strSql.ToString());
            string strFileName = new CommonClass().GB2312ToUTF8(strLoginName + "_BankVcrdPayList.xls");
            strFileName = Server.MapPath("~/Upload/" + strFileName); ;

            ExcelRender.RenderToExcel(dt, title, 6, strFileName, mul_lan, strCountTime);
            return Content("OK|" + strLoginName + "_BankVcrdPayList.xls");
        }
    }
}