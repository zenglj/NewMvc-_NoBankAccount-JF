using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_Criminal_cardDAL
    {


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateBankInfo(string fcode,string bankCard,int regflag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Criminal_card set ");

            strSql.Append(" BankAccNo = @BankAccNo , ");
            strSql.Append(" RegFlag = @RegFlag  ");            
            strSql.Append(" where fcrimecode = @fcrimecode ");

            SqlParameter[] parameters = {
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@RegFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = fcode;
            parameters[1].Value = bankCard;
            parameters[2].Value = regflag;

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