using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_BC_BaochouMain:BaseModel
    {
      [Key]
      public string OrderId { get; set; }
      public int TypeFlag { get; set; }
      public string YearMonth { get; set; }
      public string FAreaCode { get; set; }
      public string FAreaName { get; set; }
      public decimal CompleteMoney { get; set; }
      public decimal WorkDay { get; set; }
      public decimal ExaminePersonNum { get; set; }
      public decimal TichengBilv { get; set; }//提成比例
      public int Ranking { get; set; }
      public decimal BaseAmount { get; set; }
      public decimal ExtAmount { get; set; }
      public decimal Amount { get; set; }
      public decimal Points { get; set; }
      public int OrderStatus { get; set; }
      public decimal WorkChangeRatio { get; set; }
      public DateTime CreateDate { get; set; }
      public string CreateBy { get; set; }
      public DateTime? AuditDate { get; set; }
      public string AuditBy { get; set; }
      public string Remark { get; set; }
    }
}