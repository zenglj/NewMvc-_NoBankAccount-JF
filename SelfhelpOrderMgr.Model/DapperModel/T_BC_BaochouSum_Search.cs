using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 劳酬月全监汇总表
    /// </summary>
    public class T_BC_BaochouSum_Search: T_BC_BaochouSum
    {
      public DateTime CreateDate_Start { get; set; }
      public DateTime CreateDate_End { get; set; }
    }
}