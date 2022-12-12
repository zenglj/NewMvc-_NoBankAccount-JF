using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class BankMonthReportModel
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string baldat { get; set; }
        //收入部分
        public decimal grDamount { get; set; }
        public decimal jtDamount { get; set; }
        public decimal dgDamount { get; set; }
        public decimal thDamount { get; set; }
        public decimal wfpDamount { get; set; }
        //支出部分
        public decimal grCamount { get; set; }
        public decimal jtCamount { get; set; }
        public decimal dgCamount { get; set; }
        public decimal thCamount { get; set; }
        public decimal wfpCamount { get; set; }

        //余额部分

        public decimal bokbal { get; set; }//期初余额
        public decimal avabal { get; set; }//期末余额
    }
}