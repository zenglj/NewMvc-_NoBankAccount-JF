using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_RetentionAmount_Search: T_Bank_RetentionAmount
    {
        public DateTime CrtDate_Start { get; set; }
        public DateTime CrtDate_End { get; set; }
        public DateTime ModifyDate_Start { get; set; }
        public DateTime ModifyDate_End { get; set; }
    }
}