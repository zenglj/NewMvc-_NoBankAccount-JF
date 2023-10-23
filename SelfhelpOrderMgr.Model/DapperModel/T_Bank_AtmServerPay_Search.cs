using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{

    public class T_Bank_AtmServerPay_Search : T_Bank_AtmServerPay
    {
        /// <summary>
        /// 创建日期开始
        /// </summary>
        public DateTime CrtDate_Start { get; set; }
        /// <summary>
        /// 创建日期结束
        /// </summary>
        public DateTime CrtDate_End { get; set; }

        /// <summary>
        /// 审核日期开始
        /// </summary>
        public DateTime AuditDate_Start { get; set; }
        /// <summary>
        /// 审核日期结束
        /// </summary>
        public DateTime AuditDate_End { get; set; }

        /// <summary>
        /// 支付日期开始
        /// </summary>
        public DateTime PayDate_Start { get; set; }
        /// <summary>
        /// 支付日期结束
        /// </summary>
        public DateTime PayDate_End { get; set; }
    }
}