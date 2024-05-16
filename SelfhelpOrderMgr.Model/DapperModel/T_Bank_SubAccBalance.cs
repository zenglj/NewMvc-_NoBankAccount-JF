using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_SubAccBalance:BaseModel
    {
        public string AccountName { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime AccountDate { get; set; }
        public string Remark { get; set; }
    }
}