using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_Invoice_outBLL
    {
        public int LoadInvs(string outId, string strInvs, int checkVcrdBankFlag)
        {
            return new T_Invoice_outDAL().LoadInvs(outId, strInvs,checkVcrdBankFlag);
        }
        public bool DeleteMainInfo(string strseqno)
        {
            return new T_Invoice_outDAL().DeleteMainInfo(strseqno);
        }
        public bool DeleteDetailInfo(string strSeqno)
        {
            return new T_Invoice_outDAL().DeleteDetailInfo(strSeqno);
        }

        public bool DeleteAllDetailInfo(string strSbid)
        {
            return new T_Invoice_outDAL().DeleteAllDetailInfo(strSbid);
        }

    }
}