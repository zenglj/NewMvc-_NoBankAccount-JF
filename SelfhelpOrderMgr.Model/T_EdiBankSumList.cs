using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    //中行接口收付报表
    public class T_EdiBankSumList
    {
        public int MainSeqno { get; set; }

        public string Remark { get; set; }
        public DateTime CrtDate { get; set; }
        public DateTime UpLoadDate { get; set; }
        public DateTime DetailDownLoadDate { get; set; }
        //代付总金额
        public decimal DfMoney { get; set; }
        //代付成功金额
        public decimal DfSuccMoney { get; set; }
        //代付失败应退金额
        public decimal DfFailMoney { get; set; }
        //代收成功的金额
        public decimal DsSuccMoney { get; set; }
        //未处理的金额
        public decimal NodoMoney { get; set; }
        //状态
        public string SuccFlag { get; set; }
        //复位标志
        public string ResetFlag { get; set; }

        public string dayRows { get; set; }//日记录数
        public decimal daySum { get; set; }//日汇总
    }
}