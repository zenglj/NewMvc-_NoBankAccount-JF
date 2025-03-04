using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class ReqBankInfo
    {
        public string FCrimeCode { get; set; }
        public string BankUserName { get; set; }
        public string OutBankRemark { get; set; }
        public string BankCNAPS { get; set; }
        public string OpeningBank { get; set; }
        public string OutBankCard { get; set; }
    }
}