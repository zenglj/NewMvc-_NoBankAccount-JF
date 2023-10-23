using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// ATM机服务付款记录实体
    /// </summary>
    public class T_Bank_AtmServerPay : BaseModel
    {
        public DateTime CrtDate { get; set; }
        public Decimal Amount { get; set; }
        public string CrtBy { get; set; }
        public string TotalDesc { get; set; }
        public string AuditBy { get; set; }
        public DateTime? AuditDate { get; set; }
        public string PayBy { get; set; }
        public DateTime? PayDate { get; set; }
        public int OrderStatus { get; set; }
        public string Remark { get; set; }
    }
}