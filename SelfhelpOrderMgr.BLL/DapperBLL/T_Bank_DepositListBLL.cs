using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class T_Bank_DepositListBLL:BaseDapperBLL
    {
        /// <summary>
        /// 银行存款人工审补录
        /// </summary>
        /// <param name="crtby"></param>
        /// <param name="vchnum"></param>
        /// <param name="fcrimecode"></param>
        /// <returns></returns>
        public string SetBankArtificialAddRecForProc(string crtby, string vchnum, string fcrimecode, string auditRemark)
        {
            return new T_Bank_DepositListDAL().SetBankArtificialAddRecForProc(crtby, vchnum, fcrimecode, auditRemark);
        }
    }
}