using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_UserInfoExt
    {
        //select a.fcode FCode,a.fname FName,b.fName FAreaCode,isnull(a.fflag,0) FFlag,c.CardCode CardCode,c.AmountA AmountA,c.AmountB AmountB,c.AmountC AmountC,(c.AmountA+c.AmountB+c.AmountC) AllMoney,c.BankAccNo BankAccNo 
        public string FCode { get; set; }
        public string FName { get; set; }
        public string FAreaName { get; set; }
        public string FAreaCode { get; set; }
        public string FFlag { get; set; }
        public string CardCode { get; set; }
        public string AmountA { get; set; }
        public string AmountB { get; set; }
        public string AmountC { get; set; }
        public string AllMoney { get; set; }
        public string BankAccNo { get; set; }
        

    }
}