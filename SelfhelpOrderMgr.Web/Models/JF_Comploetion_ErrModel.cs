using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class JF_Comploetion_ErrModel
    {
        [System.ComponentModel.Description("批次号")]
        public string PcNo { get; set; }
        
        [System.ComponentModel.Description("编号")]
        public string FCode { get; set; }
        [System.ComponentModel.Description("姓名")]
        public string FName { get; set; }
        [System.ComponentModel.Description("队别")]
        public string FAreaName { get; set; }

        [System.ComponentModel.Description("工作类型")]
        public string WorkTypeName { get; set; }
        [System.ComponentModel.Description("年月")]
        public string YearMonth { get; set; }
        [System.ComponentModel.Description("产值")]
        public decimal OutputValue { get; set; }
        [System.ComponentModel.Description("完成率")]
        public decimal CompletionRate { get; set; }
        [System.ComponentModel.Description("评议结果")]
        public string WorkResult { get; set; }
        [System.ComponentModel.Description("备注")]
        public string Remark { get; set; }
        [System.ComponentModel.Description("申请结果说明")]
        public string ErrInfo { get; set; }

    }
}