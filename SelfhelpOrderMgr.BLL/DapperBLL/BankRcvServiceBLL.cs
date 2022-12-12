using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class BankRcvServiceBLL : BaseDapperBLL
    {
        private BankRcvServiceDAL dal = new BankRcvServiceDAL();

        /// <summary>
        /// 获取日期分类统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IEnumerable<T_Bank_Rcv> GetDateClassStatistics(DateTime startDate, DateTime endDate)
        {
            return dal.GetDateClassStatistics(startDate, endDate);
        }

        /// <summary>
        /// 获取月分类统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public IEnumerable<T_Bank_Rcv> GetYearClassStatistics(DateTime startDate, DateTime endDate)
        {
            return dal.GetYearClassStatistics(startDate, endDate);
        }
    }
}