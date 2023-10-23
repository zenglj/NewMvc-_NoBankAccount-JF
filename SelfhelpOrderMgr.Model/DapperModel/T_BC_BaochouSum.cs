using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 劳酬月全监汇总表
    /// </summary>
    public class T_BC_BaochouSum : BaseModel
    {
        public string YearMonth { get; set; }
        public decimal CompleteMoney { get; set; }
        public decimal WorkDay { get; set; }
        public decimal ExaminePersonNum { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal ExtAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal SalaryPersonNum { get; set; }
        public decimal AvgSalary { get; set; }
        public decimal SumWorkMark { get; set; }//总工作绩效(工分)
        public decimal AvgWorkMark { get; set; }//平均工作绩效(工分)
        public decimal SumWorkRatio { get; set; }//工作系数
        public decimal Points { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public int OrderStatus { get; set; }
        public string Remark { get; set; }
    }
}