using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_ICCARD_LISTDAL
    {
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateCardStatus(int fflag,string cardNo,string fcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ICCARD_LIST set ");
            strSql.Append(" FFlag = @FFlag  ");
            strSql.Append(" where  ");
            strSql.Append(" CardCode = @CardCode  ");
            strSql.Append(" and FCrimeCode = @FCrimeCode ;");
            strSql.Append("update t_Criminal_card set CardFlaga=@FFlag ");
            strSql.Append("where FCrimeCode = @FCrimeCode ;");

            SqlParameter[] parameters = {
			            new SqlParameter("@FFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20)       
              
            };

            parameters[0].Value =fflag;
            parameters[1].Value = cardNo;
            parameters[2].Value = fcode;

            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}