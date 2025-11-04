using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_JF_KouFen : BaseModel
    {
        [System.ComponentModel.Description("狱政编号")]
        public string FCode { get; set; }
        [System.ComponentModel.Description("姓名")]
        public string FName { get; set; }
        [System.ComponentModel.Description("队别编号")]
        public string FAreaCode { get; set; }
        [System.ComponentModel.Description("队别名称")]
        public string FAreaName { get; set; }
        [System.ComponentModel.Description("扣分类型")]
        public string Memo { get; set; }
        [System.ComponentModel.Description("扣分值")]
        public decimal ScoreValue { get; set; }
        [System.ComponentModel.Description("备注")]
        public string Remark { get; set; }
        public string PcNo { get; set; }
        public string CrtBy { get; set; }
        [System.ComponentModel.Description("录入日期")]
        public DateTime CreateDate { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsDelete { get; set; }
    }
}