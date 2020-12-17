using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class BankRcvDateSumModel:BaseSumFieldModel
    {
        public string tnxdate { get; set; }
        public int ImportFlag { get; set; }
    }
}