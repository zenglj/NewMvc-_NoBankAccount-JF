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
    public partial class T_Invoice_outDAL
    {
        public int LoadInvs(string outId, string strInvs,int checkVcrdBankFlag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_invoice_outdtl (fsn,invoiceno,FcrimeCode,FCriminal,FAreaName,OrderDate,Amount) ");
            strSql.Append("select @fsn,invoiceno,FcrimeCode,FCriminal,FAreaName,OrderDate,Amount ");
            strSql.Append("from t_invoice where isnull(CheckFlag,0)=0 and invoiceno in(" + strInvs + ")");

            //配货时要验证已经是否成功回款了 T_Sho_ManagerSet 表设定
            if (checkVcrdBankFlag==1)
            {
                strSql.Append(" and invoiceno in(select distinct origid from t_vcrd where isnull(Origid,'')<>'' and isnull(BankFlag,0)>1)");
            }
            strSql.Append(";");

            SqlParameter[] parameters = {
					new SqlParameter("@fsn", SqlDbType.VarChar,20)};
            parameters[0].Value = outId;
            //try 
            //{
            //    SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
            int i=SqlHelper.ExecuteSql(strSql.ToString(), parameters);

            strSql = new StringBuilder();
            strSql.Append(@"update t_invoice_out set amount=b.fmoney from (
                select fsn,isnull(sum(amount),0) fmoney from t_invoice_outdtl 
                where  fsn=@fsn group by fsn) b
                where t_invoice_out.fsn=b.fsn;");
            strSql.Append(@"update t_invoice set checkflag=1 where invoiceno in(
                select invoiceno from t_invoice_outdtl where fsn=@fsn);");

            SqlParameter[] parametersNew = {
					new SqlParameter("@fsn", SqlDbType.VarChar,20)};
            parametersNew[0].Value = outId;
            SqlHelper.ExecuteSql(strSql.ToString(), parametersNew);

            return i; 
        }

        public bool DeleteMainInfo(string strseqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update t_invoice set checkflag=0 where invoiceno in (
                select invoiceNo from t_invoice_outdtl a, t_invoice_out b
                where a.fsn=b.fsn and b.seqno=@seqno);");
            strSql.Append(@"delete from t_invoice_outdtl where fsn in(
                select fsn From t_invoice_out where seqno=@seqno
                );");
            strSql.Append(@"delete From t_invoice_out where seqno=@seqno;");

            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.VarChar,20)};
            parameters[0].Value = strseqno;
            int i=SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if(i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteDetailInfo(string strSeqno)
        {
            T_Invoice_outdtl dtl=new T_Invoice_outdtlDAL().GetModel(Convert.ToInt32(strSeqno));
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update t_invoice set CheckFlag=0 where invoiceno in(
                select InvoiceNo from t_invoice_OutDtl where seqno=@seqno
                );");
            strSql.Append(@"delete from t_invoice_outDtl where seqno=@seqno;");
            strSql.Append(@"update t_Invoice_Out set Amount=b.fmoney from(
                select fsn,isnull(sum(Amount),0) fmoney from t_Invoice_Outdtl where fsn=@fsn group by fsn
                ) b
                where t_Invoice_Out.fsn=b.fsn;");

            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.VarChar,20),
                    new SqlParameter("@fsn", SqlDbType.VarChar,20)};
            parameters[0].Value = strSeqno;
            parameters[1].Value = dtl.fsn;
            int i = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAllDetailInfo(string strSbid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update t_invoice set CheckFlag=0 where invoiceno in(
                select InvoiceNo from t_invoice_OutDtl where fsn=@fsn
                );");
            strSql.Append(@"delete from t_invoice_outDtl where fsn=@fsn;");
            strSql.Append(@"update t_Invoice_Out set Amount=0 where t_Invoice_Out.fsn=@fsn;");

            SqlParameter[] parameters = {new SqlParameter("@fsn", SqlDbType.VarChar,20)};
            parameters[0].Value = strSbid;
            int i = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (i > 0)
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