using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class DamagesPayVcrdEntity
    {
        [System.ComponentModel.Description("seqno")]
        public int seqno { get; set; }
        [System.ComponentModel.Description("单号")]
        public string Vouno { get; set; }
        [System.ComponentModel.Description("编号")]
        public string FCrimeCode { get; set; }
        [System.ComponentModel.Description("姓名")]
        public string FCriminal { get; set; }
        [System.ComponentModel.Description("类型")]
        public string DType { get; set; }
        [System.ComponentModel.Description("姓名")]
        public DateTime CrtDate { get; set; }
        [System.ComponentModel.Description("扣款金额")]
        public decimal CAmount { get; set; }
        [System.ComponentModel.Description("经办")]
        public string Depositer { get; set; }
        [System.ComponentModel.Description("备注")]
        public string Remark { get; set; }
        [System.ComponentModel.Description("有效状态")]
        public int Flag { get; set; }
        [System.ComponentModel.Description("银行扣款标志")]
        public int BankFlag { get; set; }






    }
}