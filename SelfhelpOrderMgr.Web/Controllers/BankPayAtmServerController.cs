using SelfhelpOrderMgr.BLL;
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.Web.Filters;
using SelfhelpOrderMgr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Web.Controllers
{
    [MyLogActionFilterAttribute]
    public class BankPayAtmServerController : BaseController
    {
        //BaseDapperBLL _baseDapperBll = new BaseDapperBLL();
        //JavaScriptSerializer jss = new JavaScriptSerializer();
        // GET: BankPayAtmServer

        int payMode = 1;//ATM取款的类型，值是1，这里测试先改为2=============
        public ActionResult Index(int id=0)
        {
            ViewData["id"] = id;
            return View();
        }
                            
        public ActionResult GetBankPayAtmServerList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrEmpty(strJsonWhere))
            {
                var sWhere = new { Id = 0 };
                strJsonWhere = jss.Serialize(sWhere);
            }

            PageResult<T_Bank_AtmServerPay> list = _baseDapperBll.GetPageList<T_Bank_AtmServerPay, T_Bank_AtmServerPay_Search>("Id", strJsonWhere, page, rows);

            EasyUiPageResult<T_Bank_AtmServerPay> rs = new EasyUiPageResult<T_Bank_AtmServerPay>()
            {
                total = list.total,
                rows = list.rows,
                sum = list.rows.Sum(o => o.Amount)
            };
            return Content(jss.Serialize(rs));
        }

        public ActionResult GetPaymentRecordList(int atmSrvId, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            string strJsonWhere = "";
            if (atmSrvId<1)
            {
                var sWhere = new { AtmSrvId = -4,AtmSrvPayFlag=1 };
                strJsonWhere = jss.Serialize(sWhere);
            }
            else
            {
                var sWhere = new { AtmSrvId = atmSrvId };
                strJsonWhere = jss.Serialize(sWhere);
            }

            return GetPaymentRecord(page, rows, strJsonWhere);
        }
        
        /// <summary>
        /// 获取ATM服务为对账的记录
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public ActionResult GetPaymentAtmList(string strJsonWhere, int page = 1, int rows = 10, string sort = "Id", string order = "asc")
        {
            if (string.IsNullOrWhiteSpace(strJsonWhere))
            {
                strJsonWhere ="{ \"Id\":\"0\"}";
            }

            string otherQuery = $"PayMode={payMode} and TranType=0 and TranStatus in(1,2) and isnull(AtmSrvPayFlag,0)=0";

            return GetPaymentRecord(page, rows, strJsonWhere, otherQuery);
        }

        private ActionResult GetPaymentRecord(int page, int rows, string strJsonWhere,string otherQuery="")
        {
            PageResult<ViewPaymentRecordExtend> list = new T_Bank_PaymentRecordBLL().GetPageList<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>("Id", strJsonWhere, page, rows,otherQuery);
            //if (list.rows.Count == 0)
            //{
            //    list.rows.Add(new ViewPaymentRecordExtend());
            //}
            EasyUiPageResult<ViewPaymentRecordExtend> rs = new EasyUiPageResult<ViewPaymentRecordExtend>()
            {
                total = list.total,
                rows = list.rows,
                sum = list.rows.Sum(o => o.TranMoney)
            };
            return Content(jss.Serialize(rs));
        }

        /// <summary>
        /// 创建付款主单
        /// </summary>
        /// <param name="TotalDesc"></param>
        /// <param name="SelectIds"></param>
        /// <returns></returns>
        public ActionResult DoCreatePayMain(string TotalDesc,string SelectIds)
        {
            //int payMode = 1;//ATM取款的类型，值是1，这里测试先改为2=============
            ResultInfo rs = new ResultInfo();
            if (string.IsNullOrWhiteSpace(SelectIds))
            {
                rs.ReMsg = "选择的记录数不能为空";
                return Json(rs);
            }
            List<T_Bank_PaymentRecord> payRecs = _baseDapperBll.QueryList<T_Bank_PaymentRecord>($"Id in({SelectIds})");
            if(payRecs.Count<=0)
            {
                rs.ReMsg = "选择的记录为0";
                return Json(rs);
            }
            if (payRecs.Count > 0)
            {
                var selRecs = payRecs.Where(o => o.AtmSrvPayFlag == 0
                  && o.PayMode == payMode && o.TranType==0
                  && (o.TranStatus == 1 || o.TranStatus == 2)).ToList();
                if (payRecs.Count != selRecs.Count)
                {
                    rs.ReMsg = "有部分记录不是有效的未对账的ATM取款记录";
                    return Json(rs);
                }

                T_Bank_AtmServerPay model = new T_Bank_AtmServerPay {
                    CrtDate = DateTime.Now,
                    CrtBy = Session["loginUserName"].ToString(),
                    Amount=selRecs.Sum(o=>o.Amount),
                    OrderStatus=0,
                    AuditBy="",
                    PayBy="",
                    Remark="",
                    TotalDesc=TotalDesc
                };

                using (TransactionScope ts = new TransactionScope())
                {
                    try
                    {
                        var _m = _baseDapperBll.Insert<T_Bank_AtmServerPay>(model);
                        for (int i = 0; i < payRecs.Count; i++)
                        {
                            payRecs[i].AtmSrvId = _m.Id;
                            payRecs[i].AtmSrvPayFlag = 1;
                        }
                        _baseDapperBll.Update<T_Bank_PaymentRecord>(payRecs);

                        ts.Complete();
                        rs.Flag = true;
                        rs.DataInfo = _m;
                        rs.ReMsg = "OK|创建成功";
                    }
                    catch (Exception e)
                    {
                        rs.ReMsg = "Err|" + e.Message;
                        return Json(rs);
                    }
                    finally
                    {
                        //释放资源
                        ts.Dispose();
                    }
                   
                }
            }
            return Json(rs);
        }

        /// <summary>
        /// 审核提交主单
        /// </summary>
        /// <param name="Id">主单号</param>
        /// <returns></returns>
        
        public ActionResult AuditSubmitMain(int Id)
        {
            ResultInfo rs = new ResultInfo();
            if (Id == 0)
            {
                rs.ReMsg = "Err|主单号不能为0";
                return Json(rs);
            }
            T_Bank_AtmServerPay _m = _baseDapperBll.GetModel<T_Bank_AtmServerPay>(Id);

            if (_m == null)
            {
                rs.ReMsg = "Err|主单号不存在";
                return Json(rs);
            }

            if (_m.OrderStatus>=1)
            {
                rs.ReMsg = "Err|主单号已审核过，无需重新审核";
                return Json(rs);
            }

            _m.OrderStatus = 1;
            _m.AuditBy = base.loginUserName;
            if( _baseDapperBll.Update<T_Bank_AtmServerPay>(_m))
            {
                rs.Flag = true;
                rs.DataInfo = _m;
                rs.ReMsg = "OK|审核成功";
                return Json(rs);
            }
            rs.ReMsg = "Err|审核更新失败";
            return Json(rs);
        }

        /// <summary>
        /// 删除服务主单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult DeletePayServerMain(int Id)
        {
            ResultInfo rs = new ResultInfo();
            if (Id == 0)
            {
                rs.ReMsg = "Err|主单号不能为0";
                return Json(rs);
            }
            T_Bank_AtmServerPay _m = _baseDapperBll.GetModel<T_Bank_AtmServerPay>(Id);

            if (_m == null)
            {
                rs.ReMsg = "Err|主单号不存在";
                return Json(rs);
            }

            if (_m.OrderStatus >= 1)
            {
                rs.ReMsg = "Err|主单号已审核过，不能删除";
                return Json(rs);
            }


            if (_baseDapperBll.Delete<T_Bank_AtmServerPay>(Id))
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                dict.Add("AtmSrvId", "0");
                dict.Add("AtmSrvPayFlag", "0");
                var  b = _baseDapperBll.UpdatePartInfo<T_Bank_PaymentRecord>(dict,$"AtmSrvId={Id}");

                rs.Flag = true;
                rs.DataInfo = _m;
                rs.ReMsg = "OK|删除成功";
                return Json(rs);
            }
            rs.ReMsg = "Err|删除处理失败";
            return Json(rs);
        }



        /// <summary>
        /// ATM机对账报表
        /// </summary>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public ActionResult PrintPayMentAtmReport(int atmSrvId)
        {

            List<ViewPaymentRecordExtend> res = _baseDapperBll.GetModelList<ViewPaymentRecordExtend, ViewPaymentRecordExt_Search>(jss.Serialize(new { AtmSrvId=atmSrvId}), "Id asc", 10000);
            T_Bank_AtmServerPay mainOrder = _baseDapperBll.GetModel<T_Bank_AtmServerPay>( Convert.ToInt32( atmSrvId));

            ViewData["mainOrder"] = mainOrder;
            ViewData["res"] = res.Where(o => (o.TranStatus == 2 || o.TranStatus == 1)).ToList();
            return View();
        }
    }
}