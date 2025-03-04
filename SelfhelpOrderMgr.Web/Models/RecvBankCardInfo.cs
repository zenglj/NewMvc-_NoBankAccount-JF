using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.Models
{
    public class RecvBankCardInfo
    {
        public T_Criminal criminal { get; set; }
        public T_Criminal_OutBankAccount recvBankAccount { get; set; }
        public T_Bank_PaymentRecord paymentRecord { get; set; }
        public T_Criminal_card card { get; set; }

        public bool jieqingFlag { get; set; }//扣款结清标志

    }
    
}