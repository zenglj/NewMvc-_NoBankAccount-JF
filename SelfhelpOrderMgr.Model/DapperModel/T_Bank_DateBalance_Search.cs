using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_DateBalance_Search: T_Bank_DateBalance
    {
        public DateTime baldat_Start { get; set; }
        public DateTime baldat_End { get; set; }
    }
}