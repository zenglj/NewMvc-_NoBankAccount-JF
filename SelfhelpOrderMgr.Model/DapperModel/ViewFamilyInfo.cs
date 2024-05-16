using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewFamilyInfo:T_Criminal_Family
    {
        [System.ComponentModel.Description("罪名姓名")]
        public string FName { get; set; }
        [System.ComponentModel.Description("队别编码")]
        public string FAreaCode { get; set; }
        [System.ComponentModel.Description("队别名称")]
        public string FAreaName { get; set; }


    }
}