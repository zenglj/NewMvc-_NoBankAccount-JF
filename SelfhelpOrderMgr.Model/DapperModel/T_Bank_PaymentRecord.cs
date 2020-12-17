using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_PaymentRecord : T_Bank_PayRecBase
    {
        public string FCrimeCode {get;set;}
        //public int TranType {get;set;}
        //public int PayMode {get;set;}
        public decimal Amount {get;set;}
        public int ToBankId {get;set;}
        public int AuditFlag {get;set;}
        public string AuditBy {get;set;}
        public DateTime? AuditDate { get; set; }
        public decimal TranMoney {get;set;}
        //public string PurposeInfo {get;set;}
        public DateTime? TranDate { get; set; }
        public int TranStatus {get;set;}
        public DateTime Crtdate { get; set; }
        public DateTime? ReturnTime {get;set;}
        public string BankObssid { get; set; }
        public string BankResultInfo {get;set;}
        public string WithdrawalPassword { get; set; }
        public int PwdErrCount { get; set; }//密码输错次数
    }
}