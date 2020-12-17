using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_TmpRcvPayBLL
    {
        /// <summary>
        /// 查询到将要离监用户的名单
        /// </summary>
        /// <param name="dbname"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<T_TmpRcvPay> GetTmpRcvPay( DateTime startDate, DateTime endDate, string rcvpay)
        {

            return new T_TmpRcvPayDAL().GetTmpRcvPay( startDate, endDate, rcvpay);
        }
    }
}