using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_RetentionAmount:BaseModel
    {
        public string FCrimeCode { get; set; }
        public string FName { get; set; }
        public string FAreaCode { get; set; }
        public string FAreaName { get; set; }
        public string TypeName { get; set; }
        public decimal Amount { get; set; }
        public string CauseDesc { get; set; }
        public string CrtBy { get; set; }
        public DateTime CrtDate { get; set; }
        public string HistoryOrderNO { get; set; }
        public string ResultDesc { get; set; }
        public int OrderStatus { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string Remark { get; set; }
    }
}