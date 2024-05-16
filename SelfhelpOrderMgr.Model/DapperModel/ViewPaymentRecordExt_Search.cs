using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewPaymentRecordExt_Search : ViewPaymentRecordExtend
    {
        public DateTime CrtDate_Start { get; set; }//创建时间__开始
        public DateTime CrtDate_End { get; set; }//创建时间__开始
        public DateTime TranDate_Start { get; set; }//传送时间_开始
        public DateTime TranDate_End { get; set; }//传送时间_结束
        public DateTime AuditDate_Start { get; set; }//审核时间_开始
        public DateTime AuditDate_End { get; set; }//审核时间_结束
        public DateTime ReturnTime_Start { get; set; }//到账时间_开始
        public DateTime ReturnTime_End { get; set; }//到账时间_结束

    }
}