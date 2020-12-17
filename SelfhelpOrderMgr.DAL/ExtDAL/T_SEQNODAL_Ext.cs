using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_SEQNODAL
    {
        public string GetSeqTypeNo(string seqnoType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @vouno varchar(30);");
            strSql.Append("update t_seqno set seqno=seqno+1 where seqtype='" + seqnoType + "';");
            strSql.Append("select @vouno=seqno from t_seqno where seqtype='" + seqnoType + "';");
            strSql.Append("select @vouno='" + seqnoType + "'+@vouno;");
            strSql.Append("select @vouno;");
            DataSet ds=SqlHelper.Query(strSql.ToString());
            return ds.Tables[0].Rows[0][0].ToString();

        }
    }
}