using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_ATMCashBoxDetail:BaseModel
    {
        public int MainId { get; set; }
        public string CashBoxName { get; set; }
        public string CashType { get; set; }
        public string Currency { get; set; }
        public string MianE { get; set; }
        public int JiaChao { get; set; }
        public int ShengYu { get; set; }
        public string BoxStatus { get; set; }
    }
}