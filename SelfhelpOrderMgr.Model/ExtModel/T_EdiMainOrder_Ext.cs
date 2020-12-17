using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class T_EdiMainOrder
    {
        public string MainSeqno { get; set; }
        public string Remark { get; set; }
        public string UploadDate { get; set; }
        public string SuccFlag { get; set; }
        public string ResetFlag { get; set; }
    }
}