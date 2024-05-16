using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewFamilyInfo_Search: ViewFamilyInfo
    {
        public DateTime CrtDate_Start { get; set; }
        public DateTime CrtDate_End { get; set; }
        public DateTime ModDate_Start { get; set; }
        public DateTime ModDate_End { get; set; }
    }
}