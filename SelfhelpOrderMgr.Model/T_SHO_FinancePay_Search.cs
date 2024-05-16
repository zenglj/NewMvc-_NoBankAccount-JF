using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.Model
{
    //T_SHO_FinancePay
    public class T_SHO_FinancePay_Search: T_SHO_FinancePay
    {
        public DateTime CrtDt_Start { get; set; }
        public DateTime CrtDt_End { get; set; }

    }
}

