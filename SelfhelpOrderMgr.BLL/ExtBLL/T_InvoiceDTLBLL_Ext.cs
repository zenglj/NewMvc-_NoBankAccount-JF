using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_InvoiceDTLBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_InvoiceDTL> GetModelList(string strWhere,int printSumOption)
        {
            DataSet ds = dal.GetList(strWhere, printSumOption);
            return DataTableToList(ds.Tables[0]);
        }

    }
}