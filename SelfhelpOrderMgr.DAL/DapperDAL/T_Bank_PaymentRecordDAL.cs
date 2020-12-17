using Dapper;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public class T_Bank_PaymentRecordDAL:BaseDapperDAL
    {
        /// <summary>
        /// 直接失败的记录复位重发
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string ResetCheckSend(int id,string remark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @newId int;");
            
            strSql.Append($"update T_Bank_PaymentRecord set TranStatus=4, BankResultInfo='复位重发_{remark}_'+BankResultInfo where TranStatus=3 and Id=@id;");
            strSql.Append("update T_Bank_PaymentDetail set SuccFlag=-1 where MainId=@id;");
            strSql.Append(@"update T_Vcrd  set Bankflag=1 where Seqno in(
                select Vcrdseqno from T_Bank_PaymentDetail where MainId=@id
                ); ");
            strSql.Append(@"insert into T_Bank_PaymentRecord(
                FCrimeCode,TranType,PayMode,Amount,ToBankId,AuditFlag,AuditBy, AuditDate,TranMoney,PurposeInfo
                ,TranDate,TranStatus, Crtdate,ReturnTime,BankObssid,BankResultInfo,WithdrawalPassword)
                select FCrimeCode,TranType,PayMode,Amount,ToBankId,0 as AuditFlag,AuditBy,Convert(varchar(10),getdate(),120) as AuditDate,TranMoney,PurposeInfo
                ,null as TranDate,0 as TranStatus,getdate() as Crtdate,null as  ReturnTime,null as BankObssid,'' as BankResultInfo,WithdrawalPassword
                from T_Bank_PaymentRecord where Id=@id;");
            strSql.Append("select @newId=@@IDENTITY;");
            strSql.Append(@"insert into T_Bank_PaymentDetail(
                MainId, Vcrdseqno, SuccFlag)
                select @newId as MainId,Vcrdseqno,0 as SuccFlag
                from T_Bank_PaymentDetail where Id = @id;");
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                try
                {
                    conn.Open();
                    var parems = new DynamicParameters();//建立一个parem对象
                    parems.Add("@id", id);
                    //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                    int i = conn.Execute(strSql.ToString(), parems);

                if (i >= 1)
                    {
                        return "OK|复位成功";
                    }
                    else
                    {
                        return "Err|复位异常";
                    }
                }
                catch (Exception)
                {
                    return "Err|复位异常，已回滚";
                }
                
            }
        }


        /// <summary>
        /// 转账成功后退款的复位重发
        /// </summary>
        /// <param name="id"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public string ResetRefund(int id, string remark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @newId int;");

            strSql.Append($"update T_Bank_PaymentRecord set TranStatus=4, BankResultInfo='复位重发_{remark}_'+BankResultInfo where TranStatus=2 and Id=@id;");
            strSql.Append("update T_Bank_PaymentDetail set SuccFlag=-1 where MainId=@id;");
            strSql.Append(@"update T_Vcrd  set Bankflag=1 where Seqno in(
                select Vcrdseqno from T_Bank_PaymentDetail where MainId=@id
                ); ");
            strSql.Append(@"insert into T_Bank_PaymentRecord(
                FCrimeCode,TranType,PayMode,Amount,ToBankId,AuditFlag,AuditBy, AuditDate,TranMoney,PurposeInfo
                ,TranDate,TranStatus, Crtdate,ReturnTime,BankObssid,BankResultInfo,WithdrawalPassword)
                select FCrimeCode,TranType,PayMode,Amount,ToBankId,0 as AuditFlag,AuditBy,Convert(varchar(10),getdate(),120) as AuditDate,TranMoney,PurposeInfo
                ,null as TranDate,0 as TranStatus,getdate() as Crtdate,null as  ReturnTime,null as BankObssid,'' as BankResultInfo,WithdrawalPassword
                from T_Bank_PaymentRecord where Id=@id;");
            strSql.Append("select @newId=@@IDENTITY;");
            strSql.Append(@"insert into T_Bank_PaymentDetail(
                MainId, Vcrdseqno, SuccFlag)
                select @newId as MainId,Vcrdseqno,0 as SuccFlag
                from T_Bank_PaymentDetail where Id = @id;");
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                try
                {
                    conn.Open();
                    var parems = new DynamicParameters();//建立一个parem对象
                    parems.Add("@id", id);
                    //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                    int i = conn.Execute(strSql.ToString(), parems);

                    if (i >= 1)
                    {
                        return "OK|复位成功";
                    }
                    else
                    {
                        return "Err|复位异常";
                    }
                }
                catch (Exception)
                {
                    return "Err|复位异常，已回滚";
                }

            }
        }
    }
}