using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Trans_BankAccount:BaseModel
    {
        /// <summary>
        /// 账户编号
        /// </summary>
      public  string AccCode {get;set;}
        /// <summary>
      /// 银行账户名称
        /// </summary>
      public string AccountName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
      public string AccountNo { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
      public string BankName { get; set; }
        /// <summary>
        /// 银行地址
        /// </summary>
      public string BankAddr { get; set; }
        /// <summary>
        /// 存取标志 （备用）
        /// </summary>
      public  int? InOutFlag {get;set;}
        /// <summary>
        /// 备注说明
        /// </summary>
      public string Remark { get; set; }
        /// <summary>
        /// 联行号
        /// </summary>
      public string BankCNAPS { get; set; }
        /// <summary>
        /// 是否本行 1是本行，0是他行
        /// </summary>
      public int BocFlag { get; set; }
    }
}