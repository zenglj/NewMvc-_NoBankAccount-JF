using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_EdiListBLL
    {
        /// <summary>
        /// 改变T_ediList总单的状态，如为成功，或是失败，一般是用于处理失败后的复位处理
        /// </summary>
        /// <param name="op"></param>
        public int UpdateByMainSeqno( int mainSeqno, int succFlag, int resetFlag)
        {
            return new T_EdiListDAL().UpdateByMainSeqno( mainSeqno, succFlag, resetFlag);

        }

        public IEnumerable<T_EdiMainOrder> GetAll()
        {
            return new T_EdiListDAL().GetAll();
        }

        public IEnumerable<T_EdiMainOrder> GetListByMainSeqno( int mainSeqno)
        {
            return new T_EdiListDAL().GetListByMainSeqno( mainSeqno);
        }

        /// <summary>
        /// 按登录用户的Code查询
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        public IEnumerable<T_EdiMainOrder> GetListByDate(DateTime startDate, DateTime endDate, string rcvpay,string succflag,string remark)
        {
            return new T_EdiListDAL().GetListByDate(startDate, endDate, rcvpay,succflag,remark);
        }
    }
}