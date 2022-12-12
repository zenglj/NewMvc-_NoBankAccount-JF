using Dapper;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public class BankRcvServiceDAL : BaseDapperDAL
    {

        /// <summary>
        /// 获取日期分类统计
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<T_Bank_Rcv> GetDateClassStatistics( DateTime startDate, DateTime endDate)
        {
            string sql = @"select valdat as createdate,a.direction,isnull(b.ImportFlag,0) ImportFlag
                ,sum( case when (a.transtype in('10','11')) then 0-txnamt  when (a.transtype not in('10','11')) then txnamt else 0 end ) RcvAmount 
                from 
                (select distinct [fractnactacn],[fractncardno],[fractnacntname],[fractnibkname]
                ,[toactntoibkn],[toactnactacn],[toactncardno],[toactntoname]
                ,[toactntobank],[mactibkn],[mactacn],[nactcardno],[mactname]
                ,[mactbank],[vchnum],[transid],[insid],[txndate],[txntime]
                ,[txnamt],[acctbal],[avlbal],[frzamt],[overdramt],[avloverdramt]
                ,[useinfo],[furinfo],[transtype],[bustype],[trncur],[direction]
                ,[feeact],[feeamt],[feecur],[valdat],[vouchtp],[vouchnum]
                ,[fxrate],[interinfo]
                        from t_bank_transDetail
		                where valdat between @startDate and @endDate) a
		                left outer join t_bank_rcv b on a.vchnum=b.VchNum
		                group by valdat,a.direction,isnull(b.ImportFlag,0)";
            var param = new { startDate = startDate, endDate = endDate};
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                IEnumerable<T_Bank_Rcv> _s = SqlMapper.Query<T_Bank_Rcv>(conn, sql, param).AsEnumerable<T_Bank_Rcv>();
                return _s;
            }

        }

        /// <summary>
        /// 获取月分类统计
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<T_Bank_Rcv> GetYearClassStatistics(DateTime startDate, DateTime endDate)
        {
            string sql = @"select substring(txndate,1,6) as tnxdate,a.direction,isnull(b.ImportFlag,0) ImportFlag
                ,sum( case when (a.transtype in('10','11')) then 0-txnamt  when (a.transtype not in('10','11')) then txnamt else 0 end ) RcvAmount 
                from 
                (select distinct [fractnactacn],[fractncardno],[fractnacntname],[fractnibkname]
                ,[toactntoibkn],[toactnactacn],[toactncardno],[toactntoname]
                ,[toactntobank],[mactibkn],[mactacn],[nactcardno],[mactname]
                ,[mactbank],[vchnum],[transid],[insid],[txndate],[txntime]
                ,[txnamt],[acctbal],[avlbal],[frzamt],[overdramt],[avloverdramt]
                ,[useinfo],[furinfo],[transtype],[bustype],[trncur],[direction]
                ,[feeact],[feeamt],[feecur],[valdat],[vouchtp],[vouchnum]
                ,[fxrate],[interinfo]
                        from t_bank_transDetail
		                where valdat between @startDate and @endDate) a
		                left outer join t_bank_rcv b on a.vchnum=b.VchNum
		                group by substring(txndate,1,6),a.direction,isnull(b.ImportFlag,0)";
            var param = new { startDate = startDate, endDate = endDate };
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                IEnumerable<T_Bank_Rcv> _s = SqlMapper.Query<T_Bank_Rcv>(conn, sql, param).AsEnumerable<T_Bank_Rcv>();
                return _s;
            }

        }
    }
}