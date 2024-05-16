using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_ATMCashBoxMain:BaseModel
    {
        public string ATMId { get; set; }
        public string AtmName { get; set; }
        public DateTime CrtDate { get; set; }
        public int CashBoxMoney { get; set; }
        public int CashBoxMoneyNotReturn { get; set; }
        public int CashBoxReturnMoney { get; set; }
        public string Remark { get; set; }
    }
}