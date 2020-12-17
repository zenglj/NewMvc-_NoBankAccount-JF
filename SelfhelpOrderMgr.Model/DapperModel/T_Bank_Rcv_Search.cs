using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_Rcv_Search : T_Bank_Rcv
    {
        public DateTime CreateDate_Start { get; set; }//创建时间__开始
        public DateTime CreateDate_End { get; set; }//创建时间__开始

    }
}