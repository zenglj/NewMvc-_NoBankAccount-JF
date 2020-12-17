using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_CardPool_Search : T_Bank_CardPool
    {
        /// <summary>
        /// 开始日期
        /// </summary>
      public string CrtDate_Start { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
      public string CrtDate_End { get; set; }
    }
}