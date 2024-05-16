using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class T_TempLeavePrison
    {
        public string strOutDate { get; set; }
        public string BankCardNo { get; set; }
        public decimal AmountA { get; set; }
        public decimal AmountB { get; set; }
        public decimal AmountC { get; set; }
        public decimal AccPoints { get; set; }
        //以下是出监人员银行卡信息
        public string OutBankCard { get; set; }//收款银行卡号
        public string BankUserName { get; set; }//收款人姓名
        public string OpeningBank { get; set; }//开户行
        public string PayMode { get; set; }//结算模式
        public int CollectMoneyFlag { get; set; }//现金已领标识
        public int seqno { get; set; }

    }
}