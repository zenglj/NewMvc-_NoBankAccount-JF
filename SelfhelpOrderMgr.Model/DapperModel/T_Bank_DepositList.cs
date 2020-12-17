using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_DepositList:BaseModel
    {
        public string obssid {get;set;}
        public string FromActAccount {get;set;}
        public string FromActName	{get;set;}
        public string ToAccno	{get;set;}
        public string ToName {get;set;}
        public decimal TranAmount {get;set;}
        public string TranCurrency {get;set;}
        public DateTime TranSuccDate {get;set;}
        public string TranStatus {get;set;}
        public string PurposeInfo {get;set;}
        public DateTime AccountingTime { get; set; }
        public string Memo { get; set; }
        public int InVcrdFlag { get; set; }//是否进Vcrd的标志
        public DateTime CrtDate { get; set; }//创建时间
		public string AuditRemark {get;set;}//入Vcrd审核信息
        public string FCrimeCode { get; set; }//犯人编号
    }
}