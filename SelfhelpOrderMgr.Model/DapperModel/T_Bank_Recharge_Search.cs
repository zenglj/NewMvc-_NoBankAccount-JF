using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_Recharge_Search: T_Bank_Recharge
    {
        public DateTime RechargeDate_Start { get; set; }
        public DateTime RechargeDate_End { get; set; }
        public DateTime VerifyDate_Start { get; set; }
        public DateTime VerifyDate_End { get; set; }
    }
}