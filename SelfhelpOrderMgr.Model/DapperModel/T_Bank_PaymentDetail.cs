using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_PaymentDetail:BaseModel
    {
        /// <summary>
        /// 主单号
        /// </summary>
        public int MainId {get;set;}

        /// <summary>
        /// VcrdSeqno
        /// </summary>
        public int Vcrdseqno {get;set;}

        /// <summary>
        /// 是否成功标志
        /// </summary>
        public int SuccFlag { get; set; }
    }
}