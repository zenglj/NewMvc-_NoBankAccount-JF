using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_TempAmount_CardBLL
    {
        public List<T_TempAmount_Card> GetSearchCardAmount(string strFCrimeCode, string strFCrimeName, string strAreaName, string cardStatus)
        {
            return new T_TempAmount_CardDAL().GetSearchCardAmount(strFCrimeCode, strFCrimeName, strAreaName, cardStatus);
            //return DataTableToList(ds.Tables[0]);
        }
    }
}