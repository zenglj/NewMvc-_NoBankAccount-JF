using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewCriminalInfo:T_Criminal
    {
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string SecondaryBankCard { get; set; }
        /// <summary>
        /// IC卡开卡标志
        /// </summary>
        public string CardOpenFlag { get; set; }
        /// <summary>
        /// 银行卡开卡标志
        /// </summary>
        public string BankOpenFlag { get; set; }
        /// <summary>
        /// 人脸采集标志
        /// </summary>
        public int faceFlag { get; set; }

    }
}