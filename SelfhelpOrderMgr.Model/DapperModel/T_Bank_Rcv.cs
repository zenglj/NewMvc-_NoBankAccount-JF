using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_Rcv :BaseModel
    {
        public string CardNo { get; set; }
        public string VchNum { get; set; }
        public decimal RcvAmount { get; set; }
        public string transtype { get; set; }
        public string tnxdate { get; set; }
        public string OffsetVchNum { get; set; }
        public string Remark { get; set; }
        public string FCrimeCode { get; set; }
        public string FName { get; set; }
        public string Error { get; set; }
        public int? ImportFlag { get; set; }
        public DateTime CreateDate { get; set; }
        public string fractName { get; set; }
    }
}