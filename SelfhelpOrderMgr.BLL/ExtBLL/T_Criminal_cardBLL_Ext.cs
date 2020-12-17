using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_Criminal_cardBLL
    {

        public bool UpdateBankInfo(string fcode, string bankCard, int regflag)
        {
            return new T_Criminal_cardDAL().UpdateBankInfo(fcode, bankCard, regflag);  
        }
		
    }
}