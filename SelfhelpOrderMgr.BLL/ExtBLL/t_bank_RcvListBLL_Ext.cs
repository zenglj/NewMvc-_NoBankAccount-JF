using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class t_bank_RcvListBLL
    {
        public List<t_bank_RcvList> GetPageModelList(int page, int pageRow, string strWhere, string orderByField)
        {
            return new t_bank_RcvListDAL().GetPageModelList( page,  pageRow,  strWhere,  orderByField);
        }
        public List<t_bank_RcvList> CustomerQuery(string strSql)
        {
            return new t_bank_RcvListDAL().CustomerQuery(strSql);
        }
    }
}