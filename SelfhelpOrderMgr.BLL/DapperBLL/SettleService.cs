using SelfhelpOrderMgr.Common.CustomExtend;
using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.BLL
{
    public class SettleService:BaseDapperBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        SettleDao _settleDao = new SettleDao();
        public ResultInfo RestoryInPrison(string fcrimecode, MoneyPayMode payMode)
        {//恢复在押
            ResultInfo rs = new ResultInfo() { 
                Flag=true,
                ReMsg="未处理",
                DataInfo=""
            };
            t_balanceList bal = new t_balanceListBLL().GetModelList("FCrimecode='" + fcrimecode + "'").OrderByDescending(p=>p.seqno ).FirstOrDefault();
            if (bal == null)
            {
                rs.ReMsg = "没有离监结算的记录，无需恢复";
                return rs;
            }
            if (bal.PayMode != 0)
            {
                PageResult<T_Vcrd> list = this.GetPageList<T_Vcrd, T_Vcrd>(" Seqno Desc ", jss.Serialize(new { FCrimeCode = fcrimecode }), 1, 10, " TypeFlag in(5,6)");
                if (list.rows.Count > 0)
                {
                    if (list.rows[0].BankFlag == 2)
                    {
                        rs.ReMsg = "离监结算款已经支付成功，不能恢复";
                        return rs;
                    }
                    else if (list.rows[0].BankFlag == 1)
                    {
                        rs.ReMsg = "离监结算款已经发送给银行，不能恢复";
                        return rs;
                    }
                }
                
            }
            if (bal.PayMode != 0)
            {
                PageResult<T_Bank_PaymentRecord> list = this.GetPageList<T_Bank_PaymentRecord, T_Bank_PaymentRecord>(" Id Desc ", jss.Serialize(new { FCrimeCode = fcrimecode }), 1, 10, "");
                if (list.rows.Count > 0)
                {
                    if (list.rows[0].TranStatus == 2)
                    {
                        rs.ReMsg = "离监结算款已经支付成功，不能恢复";
                        return rs;
                    }
                    else if (list.rows[0].TranStatus == 1)
                    {
                        rs.ReMsg = "离监结算款已经发送给银行，不能恢复";
                        return rs;
                    }
                }
                
            }
            return _settleDao.RestoryInPrisonBase(fcrimecode, payMode);
        }
    }
}