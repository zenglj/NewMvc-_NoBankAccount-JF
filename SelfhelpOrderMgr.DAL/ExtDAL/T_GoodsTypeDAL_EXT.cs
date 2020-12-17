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
    public partial class T_GoodsTypeDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IEnumerable<T_GoodsType> GetListOfIEnumerable(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_GoodsType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return conn.Query<T_GoodsType>(strSql.ToString());
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public IEnumerable<T_GoodsType> GetListOfIEnumerable(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM T_GoodsType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return conn.Query<T_GoodsType>(strSql.ToString());
            }
        }

    }
}