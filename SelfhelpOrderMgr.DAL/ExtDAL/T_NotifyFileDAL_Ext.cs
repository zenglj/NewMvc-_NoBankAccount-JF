using SelfhelpOrderMgr.Model;
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
    public partial class T_NotifyFileDAL
    {
        public IEnumerable<T_NotifyFile> GetPageListOfIEnumerable(int stratId, int page, int pageRow, string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1 + stratId;
                int endNumber = page * pageRow + stratId;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY ID desc) AS RowNumber,a.*  from T_NotifyFile a");
                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                return conn.Query<T_NotifyFile>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

    }
}