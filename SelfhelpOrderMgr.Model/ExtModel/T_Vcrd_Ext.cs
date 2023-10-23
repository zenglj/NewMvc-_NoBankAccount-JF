using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class T_Vcrd
    {
        //付款模式，0是现金，1是ATM机取款，2是转账
        public int PayMode { get; set; }
        public decimal AmountA { get; set; }
        public decimal AmountB { get; set; }
        public decimal AmountC { get; set; }
    }
}