using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_Recharge:BaseModel
    {
      public string Payls{get;set;}
      public string FCode{get;set;}
      public string FName{get;set;}
      public string Source{get;set;}
      public Decimal Amount{get;set;}
      public DateTime RechargeDate{get;set;}
      public int Status{get;set;}
      public string Memo{get;set;}
      public int VerifyInportFlag{get;set;}
      public DateTime VerifyDate{get;set;}
      public string Remark{get;set;}
      public string PhoneNum{get;set;}
      public string FamilyName{get;set;}
      public string Relation{get;set;}
      public string UserAuthCode{get;set;}
    }
}