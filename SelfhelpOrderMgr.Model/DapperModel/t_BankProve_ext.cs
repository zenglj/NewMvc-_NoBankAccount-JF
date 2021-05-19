using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class t_BankProve
    {
        /// <summary>
        /// 结算模式
        /// </summary>
        public int PayMode { get; set; }
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string OutBankCard { get; set; }
        /// <summary>
        /// 收款人名称
        /// </summary>
        public string BankUserName { get; set; }
        /// <summary>
        /// 开户行名称
        /// </summary>
        public string OpeningBank { get; set; }

        /// <summary>
        /// 关系
        /// </summary>
        public string OutBankRemark { get; set; }

        /// <summary>
        /// 取款密码
        /// </summary>
        public string WithdrawalPassword { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string CrtBy { get; set; }
        public decimal AmountA { get; set; }
        public decimal AmountB { get; set; }
        public decimal AmountC { get; set; }
        public int PrintCount { get; set; }//打印次数
    }
}