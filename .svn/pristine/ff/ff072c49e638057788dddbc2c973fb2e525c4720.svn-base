using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class t_XFQueryListBLL
    {
        public List<t_XFQueryList> GetFCrimeXFModelList(string action, string fcrimecode, DateTime startDate, DateTime endDate)
        {
            DataSet ds = new t_XFQueryListDAL().GetFCrimeXFInfo(action,fcrimecode,startDate,endDate);
            if (ds.Tables.Count>0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            return null;
        }
    }
}