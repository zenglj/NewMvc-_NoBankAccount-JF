using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_CardPool:BaseModel
    {
      /// <summary>
      /// 中银结算卡号
      /// </summary>
      public string BankCardNoNew { get; set; }
        /// <summary>
        /// 犯人编号
        /// </summary>
      public string FCrimeCode {get;set;}
        /// <summary>
        /// 使用标志
        /// </summary>
      public int UseFlag {get;set;}
        /// <summary>
        /// 开始使用日期
        /// </summary>
      public DateTime? UseDate {get;set;}
        /// <summary>
        /// 导入日期
        /// </summary>
      public string CrtDate { get; set; }
    }
}