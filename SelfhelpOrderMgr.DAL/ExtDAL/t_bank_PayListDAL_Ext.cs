﻿using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace SelfhelpOrderMgr.DAL
{
    public partial class t_bank_PayListDAL
    {
        public List<t_bank_PayList> GetPageModelList(int page, int pageRow, string strWhere, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY seqno) AS RowNumber,* from t_bank_PayList");
                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                if (string.IsNullOrEmpty(orderByField) == false)
                {
                    strSql.Append(" Order by " + orderByField);
                }

                return (List<t_bank_PayList>)conn.Query<t_bank_PayList>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

        public List<t_bank_PayList> CustomerQuery(string strSql)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                List<t_bank_PayList> vcrds = (List<t_bank_PayList>)conn.Query<t_bank_PayList>(strSql);
                return vcrds;
            }
        }
    }
}