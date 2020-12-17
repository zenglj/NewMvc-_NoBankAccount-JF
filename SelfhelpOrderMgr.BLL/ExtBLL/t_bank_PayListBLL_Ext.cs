using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class t_bank_PayListBLL
    {
        public List<t_bank_PayList> GetPageModelList(int page, int pageRow, string strWhere, string orderByField)
        {
            return new t_bank_PayListDAL().GetPageModelList(page, pageRow, strWhere, orderByField);
        }
        public List<t_bank_PayList> CustomerQuery(string strSql)
        {
            return new t_bank_PayListDAL().CustomerQuery(strSql);
        }
    }
}