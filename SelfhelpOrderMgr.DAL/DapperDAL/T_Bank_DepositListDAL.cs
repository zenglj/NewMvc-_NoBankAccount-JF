using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SelfhelpOrderMgr.DAL
{
    public class T_Bank_DepositListDAL: BaseDapperDAL 
    {
        /// <summary>
        /// 银行存款人工审补录
        /// </summary>
        /// <param name="crtby"></param>
        /// <param name="vchnum"></param>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        public string SetBankArtificialAddRecForProc( string crtby, string vchnum, string fcrimecode,string auditRemark)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                var parems = new DynamicParameters();//建立一个parem对象
                parems.Add("@crtby", crtby);
                parems.Add("@vchnum", vchnum);
                parems.Add("@fcrimecode", fcrimecode);
                parems.Add("@auditRemark", auditRemark);
                parems.Add("@result", "", DbType.String, ParameterDirection.Output);//输出返回值
                //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                SqlMapper.Execute(conn, "P_BankArtificialAddNotImportRec", parems, null, null, CommandType.StoredProcedure);
                string res = parems.Get<string>("@result");//获取数据库输出的值
                return res;
            }

        }
    }
}