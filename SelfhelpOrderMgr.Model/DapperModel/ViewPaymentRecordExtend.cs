using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewPaymentRecordExtend:T_Bank_PaymentRecord
    {
        public string FCrimeName { get; set; }
        public string FAreaName { get; set; }
        //public string OutBankCard { get; set; }
        //public string BankUserName { get; set; }
        //public string BankOrgName { get; set; }
        //public string BankCNAPS { get; set; }
        //public string OpeningBank { get; set; }
        //public string OutBankRemark { get; set; }
    }
}