using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_PCJ_RequestRecord_Search : T_PCJ_RequestRecord
	{
        public DateTime EndTime_Start { get; set; }
        public DateTime EndTime_End { get; set; }

        public DateTime CreateDate_Start { get; set; }
        public DateTime CreateDate_End { get; set; }
    }
}