using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class JF_Koufen_ErrModel
    {
        [System.ComponentModel.Description("批次号")]
        public string PcNo { get; set; }
        
        [System.ComponentModel.Description("编号")]
        public string FCode { get; set; }
        [System.ComponentModel.Description("姓名")]
        public string FName { get; set; }
        [System.ComponentModel.Description("队别")]
        public string FAreaName { get; set; }

        [System.ComponentModel.Description("扣分事由")]
        public string Memo { get; set; }

        [System.ComponentModel.Description("扣分")]
        public decimal ScoreValue { get; set; }

        [System.ComponentModel.Description("备注")]
        public string Remark { get; set; }
        [System.ComponentModel.Description("申请结果说明")]
        public string ErrInfo { get; set; }

    }
}