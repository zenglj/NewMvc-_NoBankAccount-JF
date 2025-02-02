﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class PeihuoDanPrintList
    {
        public string FAreaName { get; set; }
        public string RoomNo { get; set; }
        public string SPShortCode { get; set; }
        public string GName { get; set; }
        public string Remark { get; set; }
        public string GTXM { get; set; }
        public decimal FCount { get; set; }
        public decimal FMoney { get; set; }


        public decimal xfCount { get; set; }//消费数量
        public decimal xfMoney { get; set; }//消费金额
        public decimal thCount { get; set; }//退货数量
        public decimal thMoney { get; set; }//退货金额
        public string FCrimeCode { get; set; }
        public string FCriminal { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Gdj { get; set; }
        public int? BankFlag { get; set; }
        public DateTime SendDate { get; set; }
    }
}