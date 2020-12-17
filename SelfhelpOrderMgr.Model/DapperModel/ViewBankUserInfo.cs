using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewBankUserInfo
    {
		public int Id { get; set; }
		public string FCrimeCode { get; set; }
		public string FCrimeName { get; set; }
		public decimal TranMoney { get; set; }
		public int TranStatus { get; set; }
		public string PurposeInfo { get; set; }

	}
}