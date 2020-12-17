using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_EdiDetailDAL
    {
        public IEnumerable<T_EdiDetail> GetListByMainseqno(int mainseqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.*,b.dtype,b.fcriminal,b.crtdate from t_edidetail a left join t_vcrd b on a.vcrdseqno=b.seqno where mainseqno=@mainseqno
                            ");

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return conn.Query<T_EdiDetail>(strSql.ToString(), new { mainseqno = mainseqno });
            }
        }
    }
}