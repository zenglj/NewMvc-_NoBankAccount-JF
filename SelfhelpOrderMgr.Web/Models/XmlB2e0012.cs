using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models.B2e0012
{
    public class bocb2e
    {
        public Head head { get; set; }
        public Trans trans { get; set; }
    }


    public class Trans
    {
        public Trn_b2e0012_rs trn_b2e0012_rs { get; set; }
    }

    public class Head
    {
        /// <summary>
        /// 终端ID
        /// </summary>
        public string termid { get; set; }
        /// <summary>
        /// 传送ID
        /// </summary>
        public string trnid { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string custid { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string cusopr { get; set; }
        /// <summary>
        /// 传送编码如：b2e0012
        /// </summary>
        public string trncod { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string token { get; set; }

    }

    public class Status
    {
        public string rspcod { get; set; }
        public string rspmsg { get; set; }
    }

    public class Account
    {
        public string ibknum { get; set; }
        public string actacn { get; set; }
        public string curcde { get; set; }

    }

    public class Balance
    {
        public decimal bokbal { get; set; }
        public decimal avabal { get; set; }
    }
    [Serializable]
    public class B2e0012_rs
    {
        public Status status { get; set; }
        public Account account { get; set; }
        public Balance balance { get; set; }
        public string baldat { get; set; }
    }

    public class Trn_b2e0012_rs
    {
        public Status status { get; set; }
        public string b2e0012_rs { get; set; }
    }
}
