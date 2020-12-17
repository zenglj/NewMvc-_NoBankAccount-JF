using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_DepositList_Search : T_Bank_DepositList
    {
        public DateTime CrtDate_Start { get; set; }//创建时间__开始
        public DateTime CrtDate_End { get; set; }//创建时间__开始
        public DateTime TranSuccDate_Start { get; set; }//传送时间_开始
        public DateTime TranSuccDate_End { get; set; }//传送时间_结束
        public DateTime AccountingTime_Start { get; set; }//到账时间_开始
        public DateTime AccountingTime_End { get; set; }//到账时间_结束

    }
}