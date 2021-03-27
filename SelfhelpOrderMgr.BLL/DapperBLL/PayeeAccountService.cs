using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class PayeeAccountService : BaseDapperBLL
    {
        /// <summary>
        /// 同步更新未审核记录收款人银行账号信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public int UpdateNotAuditPayRecBankInfo(T_Criminal_OutBankAccount account)
        {
            string sql = @"update t_bank_paymentRecord set OutBankCard=@OutBankCard,BankUserName=@BankUserName,BankOrgName=@BankOrgName
                        ,OpeningBank=@OpeningBank,BankCNAPS=@BankCNAPS,OutBankRemark=@OutBankRemark 
                        where FCrimeCode=@FCrimeCode and auditFlag=0 and TranStatus=0";
            int _rs=this.ExecuteSql(sql, new
            {
                OutBankCard = account.OutBankCard,
                BankUserName = account.BankUserName,
                BankOrgName = account.BankOrgName
                ,
                OpeningBank = account.OpeningBank,
                BankCNAPS = account.BankCNAPS,
                OutBankRemark = account.OutBankRemark,
                FCrimeCode=account.FCrimecode
            });
            return _rs;
        }
    }
}