using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_DateBalance:BaseModel
    {
        public string rspcod { get; set; }
        public string rspmsg { get; set; }
        public string ibknum { get; set; }
        public string actacn { get; set; }
        public string curcde { get; set; }
        public decimal bokbal { get; set; }
        public decimal avabal { get; set; }
        public DateTime baldat { get; set; }
    }
}