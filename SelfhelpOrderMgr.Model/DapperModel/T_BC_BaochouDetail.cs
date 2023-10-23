using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_BC_BaochouDetail : BaseModel
    {
        public string OrderId { get; set; }
        public string FCrimeCode { get; set; }
        public string FCrimeName { get; set; }
        public string WorkPostCode { get; set; }
        public string WorkLevel { get; set; }
        public decimal CompleteRatio { get; set; }
        public string WorkCommentCode { get; set; }
        public int Ranking { get; set; }
        public decimal WorkDays { get; set; }
        public decimal WorkRatio { get; set; }
        /// <summary>
        /// 出勤工分
        /// </summary>
        public decimal WorkMark { get; set; }
        /// <summary>
        /// 绩效工分
        /// </summary>
        public decimal PerformanceMark { get; set; }
        public decimal DutyDays { get; set; }
        public string DutyCommentsCode { get; set; }
        public decimal DutyRatio { get; set; }
        /// <summary>
        /// 夜值星工分
        /// </summary>
        public decimal DutyMark { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal ExtAmount { get; set; }
        public decimal DeductAmount { get; set; }
        public decimal SumAmount { get; set; }
        public decimal OverrunAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal MerberPoints { get; set; }
        public decimal DeductPoints { get; set; }
        public decimal SumPoints { get; set; }
        public decimal Points { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public string Remark { get; set; }
    }
}