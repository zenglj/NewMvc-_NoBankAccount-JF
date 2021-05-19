using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_BONUSDAL
    {
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update t_bonus  set cnt=b.fcount,famount=b.fmoney from 
                (select bid,count(*) fcount,sum(famount) fmoney from t_Bonusdtl where bid=@BID
                group by bid) b
                where t_bonus.bid=b.bid
                ");

            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20)};
            parameters[0].Value = bid;

            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户'Bid'确认提交主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCheckFlag( T_BONUS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BONUS set ");
            strSql.Append("CHECKBY=@CHECKBY,");
            strSql.Append("CheckDate=@CheckDate,");
            strSql.Append("FCheckFlag=@FCheckFlag");
            strSql.Append(",TargetExaminerBy=@TargetExaminerBy");
            strSql.Append(",MainStatus=@MainStatus ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@CHECKBY", SqlDbType.VarChar,20),
					new SqlParameter("@CheckDate", SqlDbType.DateTime),
					new SqlParameter("@FCheckFlag", SqlDbType.Int,4),
                    new SqlParameter("@TargetExaminerBy",SqlDbType.VarChar,50),
                    new SqlParameter("@MainStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.BID;
            parameters[1].Value = model.CHECKBY;
            parameters[2].Value = model.CheckDate;
            parameters[3].Value = model.FCheckFlag;
            parameters[4].Value = model.TargetExaminerBy;
            parameters[5].Value = model.MainStatus;
            int rows = SqlHelper.ExecuteSql( strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户'Bid'审核主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByAuditFlag(T_BONUS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BONUS set ");
            strSql.Append("AuditBy=@AuditBy,");
            strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("AuditFlag=@AuditFlag");
            strSql.Append(",TargetExaminerBy=@TargetExaminerBy");
            strSql.Append(",MainStatus=@MainStatus ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@AuditBy", SqlDbType.VarChar,20),
					new SqlParameter("@AuditDate", SqlDbType.DateTime),
					new SqlParameter("@AuditFlag", SqlDbType.Int,4),
                    new SqlParameter("@TargetExaminerBy", SqlDbType.VarChar,50),
                    new SqlParameter("@MainStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.BID;
            parameters[1].Value = model.auditby;
            parameters[2].Value = model.auditdate;
            parameters[3].Value = model.auditflag;
            parameters[4].Value = model.TargetExaminerBy;
            parameters[5].Value = model.MainStatus;
            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CheckOutPrisonList(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            string rtnStr = "";
            strSql.Append(@"select * from t_bonusdtl where bid='"+ bid +"'");
            strSql.Append("and fcrimecode in(select fcode from t_criminal where fflag=1)");
            DataSet ds=SqlHelper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                rtnStr = "审核时离监人数：" + ds.Tables[0].Rows.Count.ToString();
                decimal errMoney = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    errMoney = errMoney+Convert.ToDecimal(row["FAMOUNT"].ToString());
                }
                rtnStr = rtnStr + ",金额：" + errMoney.ToString();

                strSql = new StringBuilder();
                strSql.Append(@"insert into [T_ImportList] (
                    [ImportType],[fcrimecode],[fname],[Amount],[Crtdt],[CrtBy],[Remark],[pc],[notes])
                    select 4,fcrimecode,fcriminal fname,FAMOUNT Amount,getdate(),crtby,'审核时:该用户已经办理离监了，不能导入' Remark,bid pc,'审核时:该用户已经办理离监了，不能导入' notes 
                    from t_bonusdtl where bid='"+ bid +@"'
                    and fcrimecode in(select fcode from t_criminal where fflag=1);");
                strSql.Append(@"delete from t_bonusdtl where bid='" + bid + @"'
                    and fcrimecode in(select fcode from t_criminal where fflag=1);");
                strSql.Append(@"update t_Bonus set cnt=b.fcount,FAmount=b.FAmount from (
                    select bid,count(*) fcount,sum(FAmount) FAmount from t_bonusdtl where bid='" + bid + @"'
                    group by bid) b
                    where t_Bonus.bid=b.bid;");
                

                int rows = SqlHelper.ExecuteSql(strSql.ToString());
                
            }
            return rtnStr;
            
        }

        //检查是否有重复写的记录
        public string CheckReWriteInfo(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            string rtnStr = "";
            strSql.Append(@"select * from t_bonusdtl where bid='" + bid + "'");
            strSql.Append("and fcrimecode in(select fcode from t_criminal where fflag=1)");
            DataSet ds = SqlHelper.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                rtnStr = "审核时离监人数：" + ds.Tables[0].Rows.Count.ToString();
                decimal errMoney = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    errMoney = errMoney + Convert.ToDecimal(row["FAMOUNT"].ToString());
                }
                rtnStr = rtnStr + ",金额：" + errMoney.ToString();

                strSql = new StringBuilder();
                strSql.Append(@"insert into [T_ImportList] (
                    [ImportType],[fcrimecode],[fname],[Amount],[Crtdt],[CrtBy],[Remark],[pc],[notes])
                    select 4,fcrimecode,fcriminal fname,FAMOUNT Amount,getdate(),crtby,'审核时:该用户已经办理离监了，不能导入' Remark,bid pc,'审核时:该用户已经办理离监了，不能导入' notes 
                    from t_bonusdtl where bid='" + bid + @"'
                    and fcrimecode in(select fcode from t_criminal where fflag=1);");
                strSql.Append(@"delete from t_bonusdtl where bid='" + bid + @"'
                    and fcrimecode in(select fcode from t_criminal where fflag=1);");
                strSql.Append(@"update t_Bonus set cnt=b.fcount,FAmount=b.FAmount from (
                    select bid,count(*) fcount,sum(FAmount) FAmount from t_bonusdtl where bid='" + bid + @"'
                    group by bid) b
                    where t_Bonus.bid=b.bid;");


                int rows = SqlHelper.ExecuteSql(strSql.ToString());

            }
            return rtnStr;

        }

        /// <summary>
        /// 根据用户'Bid'财务复核主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByDbCheckFlag(T_BONUS model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BONUS set ");
            strSql.Append("FDbCheckBY=@FDbCheckBY,");
            strSql.Append("Fdbcheckdate=@Fdbcheckdate,");
            strSql.Append("Fdbcheckflag=@Fdbcheckflag");
            strSql.Append(",TargetExaminerBy=@TargetExaminerBy");
            strSql.Append(",MainStatus=@MainStatus ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@FDbCheckBY", SqlDbType.VarChar,20),
					new SqlParameter("@Fdbcheckdate", SqlDbType.DateTime),
					new SqlParameter("@Fdbcheckflag", SqlDbType.Int,4),
                    new SqlParameter("@TargetExaminerBy", SqlDbType.VarChar,50),
                    new SqlParameter("@MainStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.BID;
            parameters[1].Value = model.FDbCheckBY;
            parameters[2].Value = model.Fdbcheckdate;
            parameters[3].Value = model.Fdbcheckflag;
            parameters[4].Value = model.TargetExaminerBy;
            parameters[5].Value = model.MainStatus;

            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账'
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(string bid, string crtby,int checkflag)
        {
            #region 根据配置文件DepositInVcrdFlag设定项，获取Flag的值
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("DepositInVcrdFlag");            
            int iflag = 0;//默认值VCRD Flag为0
            if (mset != null)
            {
                if (mset.MgrValue == "-2")
                {
                    iflag = -2;
                }
            } 
            #endregion
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    #region 增加SQL脚本
                    strSql.Append("declare @curYM varchar(10);");
                    strSql.Append("declare @curYear varchar(10);");
                    strSql.Append("declare @curMonth varchar(10);");
                    strSql.Append("select  @curMonth='00'+ convert(varchar(10), month(getdate()));");
                    strSql.Append("select  @curYear=substring( convert(varchar(20), year(getdate())),3,2);");
                    strSql.Append("select  @curYM=@curYear+substring(@curMonth,len(@curMonth)-1,2);");
                    strSql.Append(@"insert into t_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                      ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
	                  ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                      ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                      ,[AccType],[CheckFlag],[CheckDate]
                      ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount])
                select 'VOUB'+substring( '00000'+convert(varchar(20),a.seqno),len('00000'+convert(varchar(20),a.seqno))-5,6)
                ,a.CardCode,a.FCrimeCode,isnull(a.AmountA,0) DAmount,0 CAmount,@CrtBy CrtBy
                ,getDate() CrtDate,b.DType as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                ,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount]
                 from t_bonusdtl a,t_bonus b,t_Criminal_Card c  
                where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.Amount,0)<>0 and c.cardflaga<>4;");
                    if (iflag == 0)
                    {
                        strSql.Append(@"update t_Criminal_Card set AMountA=AMountA+b.Fmoney from (
                            select FCrimeCode,sum(isnull(AmountA,0)) Fmoney from t_bonusdtl where BID=@BID  and isnull(a.Amount,0)<>0 group by fcrimecode 
                            ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode  and t_Criminal_Card.cardflaga<>4;");
                    }
                    strSql.Append(@"insert into t_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                      ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
	                  ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                      ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                      ,[AccType],[CheckFlag],[CheckDate]
                      ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount])
                select 'VOUB'+substring( '00000'+convert(varchar(20),a.seqno),len('00000'+convert(varchar(20),a.seqno))-5,6)
                ,a.CardCode,a.FCrimeCode,(a.FAmount-isnull(a.AmountA,0)-isnull(a.AmountC,0)) DAmount,0 CAmount,@CrtBy CrtBy
                ,getDate() CrtDate,b.DType as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                ,1 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount]
                 from t_bonusdtl a,t_bonus b,t_Criminal_Card c  
                where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and (a.FAmount-isnull(a.AmountA,0)-isnull(a.AmountC,0))<>0 and c.cardflaga<>4;");
                    if (iflag == 0)
                    {
                        strSql.Append(@"update t_Criminal_Card set AMountB=AMountB+b.Fmoney from (
                select FCrimeCode,sum((FAmount-isnull(AmountA,0)-isnull(AmountC,0))) Fmoney from t_bonusdtl where BID=@BID and (FAmount-isnull(AmountA,0)-isnull(AmountC,0)) group by fcrimecode 
                ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode  and t_Criminal_Card.cardflaga<>4;");
                    }
                        strSql.Append(@"insert into t_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                  ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
	              ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                  ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                  ,[AccType],[CheckFlag],[CheckDate]
                  ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount])
            select 'VOUB'+substring( '00000'+convert(varchar(20),a.seqno),len('00000'+convert(varchar(20),a.seqno))-5,6)
                ,a.CardCode,a.FCrimeCode,isnull(a.AmountC,0) DAmount,0 CAmount,@CrtBy CrtBy
                ,getDate() CrtDate,b.DType as DType,'' Depositer,a.Remark,@vcrdFlag as  Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                ,2 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount]
                 from t_bonusdtl a,t_bonus b,t_Criminal_Card c  
                where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.AmountC,0)<>0 and c.cardflaga<>4;");
                    if (iflag == 0)
                    {
                        strSql.Append(@"update t_Criminal_Card set AMountC=AMountC+b.Fmoney from (
                select FCrimeCode,sum(isnull(AMountC,0)) Fmoney from t_bonusdtl where BID=@BID and isnull(AmountC,0)<>0
                group by fcrimecode
                ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode  and t_Criminal_Card.cardflaga<>4;");
                    }
                    strSql.Append(@"update t_bonusDTL set Flag=1 where BID=@BID 
                    and fcrimecode in(select fcrimecode from t_Criminal_Card where cardflaga<>4);");
                    strSql.Append(@"update t_bonus set cnt=b.fcount,famount=b.fmoney,Flag=1,FPostBy=@CrtBy,FPostDate=getdate(),FPostFlag=1,MainStatus=9,TargetExaminerBy='' from (
                select bid,count(*) fcount,sum(famount) fmoney from t_bonusdtl 
                where BID=@BID and flag=1 group by bid) b
                where t_bonus.BID=@BID and t_bonus.BID=b.BID;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"insert into T_ImportList(ImportType,fcrimecode,fname,amount,crtdt,crtby,remark,pc,notes)
                select 4 ImportType,fcrimecode,fcriminal fname,famount amount,getdate() crtdate
                ,crtby ,'该记录财务入账时，已离监销户了' remark,bid pc,fareaName notes
                  from t_bonusdtl where flag=0 and BID=@BID;");
                    strSql.Append(@"delete from t_bonusdtl where BID=@BID and flag=0;");
                    #endregion

                    //SqlParameter[] parameters = {
                    //    new SqlParameter("@BID", SqlDbType.VarChar,20),
                    //    new SqlParameter("@CrtBy", SqlDbType.VarChar,20)};
                    //parameters[0].Value = bid;
                    //parameters[1].Value = crtby;

                    //int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
                    //if (rows > 0)
                    //{
                    //    return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}

                    object param = new { BID = bid, CrtBy = crtby, CheckFlag=checkflag };
                    conn.Execute(strSql.ToString(),param, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                }
            }
            return false;
        }

        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账' 漳州格式的入账
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag_Model2(string bid, string crtby,int checkflag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                //采用存储过程方式
                var param = new DynamicParameters();
                param.Add("@bid", bid);
                param.Add("@crtby", crtby);
                param.Add("@checkflag", checkflag);
                param.Add("@rstFlag", 0, DbType.Int32, ParameterDirection.Output);
                var res3 = conn.Execute("P_LaoBaoUpdateInDb", param, null, null, CommandType.StoredProcedure); //0
                int rstFlag = (int)param.Get<object>("@rstFlag");
                if (rstFlag == 1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账',只写入到VCrd，T_Criminal_Card 没有增加
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbOnlyVcrd(string bid, string crtby)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    #region 增加SQL脚本
                    strSql.Append("declare @curYM varchar(10);");
                    strSql.Append("declare @curYear varchar(10);");
                    strSql.Append("declare @curMonth varchar(10);");
                    strSql.Append("select  @curMonth='00'+ convert(varchar(10), month(getdate()));");
                    strSql.Append("select  @curYear=substring( convert(varchar(20), year(getdate())),3,2);");
                    strSql.Append("select  @curYM=@curYear+substring(@curMonth,len(@curMonth)-1,2);");
                    strSql.Append(@"insert into t_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                      ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
	                  ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                      ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag]
                      ,[AccType],[SendDate],[BankFlag],[CheckFlag],[CheckDate]
                      ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount])
                select 'VOUB'+substring( '00000'+convert(varchar(20),a.seqno),len('00000'+convert(varchar(20),a.seqno))-5,6)
                ,a.CardCode,a.FCrimeCode,a.FAmount-a.AmountC DAmount,0 CAmount,@CrtBy CrtBy
                ,getDate() CrtDate,'劳动报酬' DType,'莆田模式' Depositer,a.Remark,0 Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,4 TypeFlag
                ,1 AccType,getdate() SendDate,1 BankFlag,1 [CheckFlag],getDate() [CheckDate]
                ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount]
                 from t_bonusdtl a,t_bonus b,t_Criminal_Card c  
                where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID;");
//                    strSql.Append(@"update t_Criminal_Card set AMountB=AMountB+b.Fmoney from (
//                select FCrimeCode,(FAmount-AMountC) Fmoney from t_bonusdtl where BID=@BID
//                ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");
                    strSql.Append(@"insert into t_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                  ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
	              ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                  ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag]
                  ,[AccType],[SendDate],[BankFlag],[CheckFlag],[CheckDate]
                  ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount])
            select 'VOUB'+substring( '00000'+convert(varchar(20),a.seqno),len('00000'+convert(varchar(20),a.seqno))-5,6)
                ,a.CardCode,a.FCrimeCode,a.AmountC DAmount,0 CAmount,@CrtBy CrtBy
                ,getDate() CrtDate,'劳动报酬' DType,'莆田模式' Depositer,a.Remark,0 Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,4 TypeFlag
                ,2 AccType,getdate() SendDate,1 BankFlag,1 [CheckFlag],getDate() [CheckDate]
                ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount]
                 from t_bonusdtl a,t_bonus b,t_Criminal_Card c  
                where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID;");
//                    strSql.Append(@"update t_Criminal_Card set AMountC=AMountC+b.Fmoney from (
//                select FCrimeCode,(AMountC) Fmoney from t_bonusdtl where BID=@BID
//                ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");
//                    strSql.Append("update t_bonus set Flag=1 where BID=@BID;");
//                    strSql.Append("update t_bonusDTL set Flag=1 where BID=@BID;");
                    #endregion


                    object param = new { BID = bid, CrtBy = crtby };
                    conn.Execute(strSql.ToString(), param, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                }
            }
            return false;
        }

        

        /// <summary>
        /// 主单退账
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public bool UndoMainOrder(string bid)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    #region 增加SQL脚本
                    //四步走：1减少IC卡余额，2删除Vcrd表，3更新明细，4更新主单
                    strSql.Append(@"update t_criminal_Card set amounta=amounta-t.A,amountb=amountb-t.B,amountc=amountc-t.C from (
                                    select  fcrimecode,sum(case acctype when 0 then (Damount-Camount) else 0 end) A 
                                    ,sum(case acctype when 1 then (Damount-Camount) else 0 end) B
                                    ,sum(case acctype when 2 then (Damount-Camount) else 0 end) C
                                    from t_vcrd
                                    where flag=0
                                    and origid=@bid
                                    group by fcrimecode) t
                                    where t_criminal_Card.fcrimecode=t.fcrimecode;");
                    strSql.Append("delete from t_Vcrd where flag in(0,-2) and origid=@bid;");
                    strSql.Append("update t_BonusDtl set flag=0 where Bid=@bid;");
                    strSql.Append("update t_Bonus set flag=0,FDBCheckFlag=0,auditflag=0,FPostFlag=0,fcheckflag=0 where Bid=@bid;");

                    
                    #endregion


                    object param = new { BID = bid };
                    conn.Execute(strSql.ToString(), param, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                }
            }
            return false;

        }


        public bool PTUpdateVcrdAndWriteCard(string bid,string crtby,List<T_SHO_BankReturnList> brl)
        {
            DynamicParameters dp = new DynamicParameters();
            dp.Add("@BID", bid);
            dp.Add("@CrtBy", crtby);
            dp.Add("@RES", "", DbType.Int32, ParameterDirection.Output);
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                conn.Execute("P_PtBankReturnList", dp, null, 600, CommandType.StoredProcedure);
                int res = dp.Get<Int32>("@RES");
                conn.Close();
                if(res>0)
                {
                    return true;
                }                
            }
            return false;
        }

        /// <summary>
        /// 批量写入劳动报酬明细记录，标准格式：编号、姓名、金额、备注
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="crtby"></param>
        /// <param name="resaveFlag">可以重复发放的标志</param>
        /// <returns></returns>
        public bool PLWriteBonusDtl(string bid, string crtby,int reSaveFlag=0)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                T_SETTINGS sets=new T_SETTINGSDAL().GetModel(169);
                T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("LyjBankCardCheckFlag");

                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    //更新姓名不一致的
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户名:'+FCriminal+'与'+B.FName+'，不一致' from t_Criminal b
                        where t_bonus_Temp.FCrimeCode=B.fcode and t_bonus_Temp.FCriminal<>b.FName and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");
                    //更新 该用户已离监无法导入
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户已离监无法导入' from t_Criminal b
                        where t_bonus_Temp.FCrimeCode=B.fcode and isnull(b.FFlag,0)=1 and t_bonus_Temp.Bid=@Bid;");
                    //该用户狱号不存在
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户狱号不存在' where FCrimeCode not in(
                        select distinct fcode from t_Criminal ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    //更新 金额为0的不导入
                    strSql.Append(@"update t_bonus_Temp set Notes ='金额不能为0' 
                        where Notes='' and FMoney=0;");
                    
                    //更新 该用户没有办狱内IC卡
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户没有办狱内IC卡' where FCrimeCode not in(
                        select FCrimeCode from t_Criminal_Card where Cardflaga<>4 )  and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");
                    //更新 该用户没有办狱内IC卡
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户IC卡已经停用' where FCrimeCode in(
                        select FCrimeCode from t_Criminal_Card where Cardflaga=3 )  and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //更新 该用户本月已经发放了,只能发一次
                    if (reSaveFlag == 0)
                    {
                        strSql.Append(@"update t_bonus_Temp set notes='该犯该月('+left(convert(varchar(20),c.udate,120),10)+')已经发放过了，不可以重复发放' from t_bonusdtl c,
						(select b.udate,a.* from t_bonus_Temp a left join t_bonus b on a.bid=b.bid
						where a.bid=@bid) d
						where t_bonus_Temp.bid=d.bid and t_bonus_Temp.fcrimecode=d.fcrimecode and c.udate=d.udate and c.fcrimecode=d.fcrimecode;");
                    }

                    if(mset.MgrValue=="1")
                    {
                        //该用户银行卡不存在
                        strSql.Append(@"update t_bonus_Temp set Notes ='该用户银行卡不存在' where FCrimeCode not in(
                        select distinct FCrimeCode from t_Criminal_Card where isnull(regFlag,0)=1 and isnull(BankAccNo,'')<>'' ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    }

                    //更新 该用户本月已经发放了
//                    strSql.Append(@"update t_bonus_Temp set Notes='该用户本月已经发放了'+Convert(varchar(20), b.FCount)+'次，不能再发放'  from ( 
//                        select FCrimeCode,Count(*) FCount from t_BonusDtl,t_Bonus where t_BonusDtl.Udate=t_Bonus.Udate and t_Bonus.Bid=@Bid
//                        group by FCrimeCode
//                        having Count(*)>@FCount) b
//                        where t_bonus_Temp.FCrimeCode=B.FCrimeCode and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //更新 有两条及以上相同金额的记录
                    strSql.Append(@"update t_bonus_Temp set Notes='有两条及以上相同金额的记录'  from ( 
                        select bid,FCrimeCode,Fmoney,Count(*) FCount from t_bonus_Temp
						group by bid,FCrimeCode,Fmoney having Count(*)>1) b
                        where t_bonus_Temp.FCrimeCode=B.FCrimeCode and t_bonus_Temp.bid=B.bid and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //写入到失败记录表
                    strSql.Append(@"insert into T_ImportList 
                        (ImportType,FCrimeCode,FName,Amount,Crtdt,CrtBy,Remark,Pc,Notes)
                        select 4 ImportType,FCrimeCode,FCriminal FName,FMoney Amount,GetDate() Crtdt,@CrtBy CrtBy,FRemark+':' +Notes Remark,@Bid Pc,Notes Notes
                        from t_Bonus_Temp where Bid=@Bid and Notes<>'';");
                    //写入成功记录
                    strSql.Append(@"insert into  t_bonusdtl
                         (BID,FCRIMECODE,CARDCODE,FAMOUNT,FLAG,fareacode,fareaName,fcriminal,vouno
                        ,Frealareacode,FrealAreaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,cardtype
                        ,AmountC,cqbt,gwjt,ldjx,tbbz,grkj)
                        select 
                        a.Bid BID,a.FCrimeCode FCRIMECODE,b.CardCodeA CARDCODE,a.FMoney FAMOUNT,0 FLAG,c.FAreaCode fareacode,d.FName fareaName,FCriminal fcriminal,'' vouno
                        ,'' Frealareacode,'' FrealAreaName,FRemark remark,e.Remark ptype,e.UDate udate,e.CrtBy crtby,getDate() crtdt,e.applyby applyby,1 acctype,0 cardtype
                        ,a.FMoney*f.cpct/100 AmountC,0 cqbt,0 gwjt,0 ldjx,0 tbbz,0 grkj
                        from t_bonus_Temp a,t_Criminal_Card b,t_Criminal c,t_Area d,T_Bonus e,t_Cy_Type f
                        where a.Bid=@Bid and a.Notes='' 
                        and a.FCrimeCode=C.FCode and B.FCrimeCode=C.FCode and C.FAreaCode=D.FCode
                        and a.Bid=e.Bid and f.FCode=C.FCyCode;");
                    //更新主单金额
                    strSql.Append(@"update t_bonus set FPostBy=@CrtBy,FPostDate=getDate(),FPostFlag=1,cnt=b.FCount,FAmount=B.FAmount from (
            select BID,sum(FAmount) FAmount,count(*) FCount from t_bonusdtl where Bid=@Bid
            group by Bid ) b where t_bonus.Bid=@Bid and isnull(Flag,0)=0;");
                    //删除t_bonus_Temp过渡表记录
                    strSql.Append("delete from t_bonus_Temp where Bid=@Bid");
                    object param = new { Bid = bid, CrtBy = crtby, FCount =sets.VALUE};
                    conn.Execute(strSql.ToString(), param, myTran,300);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                }
            }
            return false;
        }


        /// <summary>
        /// 批量写入劳动报酬明细记录，漳州标准格式：编号、姓名、一般报酬、超常报酬、留存金额、备注
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool PLWriteBonusDtl_Model2(string bid, string crtby,int reSaveFlag=0)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //采用存储过程方式
                conn.Open();
                var param = new DynamicParameters();
                param.Add("@bid", bid);
                param.Add("@crtby", crtby);                
                param.Add("@rstFlag", 0, DbType.Int32, ParameterDirection.Output);
                if (reSaveFlag == 1)
                {
                    param.Add("@reSaveFlag", reSaveFlag);
                }                
                var res3 = conn.Execute("P_PLWriteBonusDtl", param, null,300, CommandType.StoredProcedure); //0
                int rstFlag =(int) param.Get<object>("@rstFlag");
                if (rstFlag == 1)
                {
                    return true;
                }                
            }
            return false;
        }



        /// <summary>
        /// 按Bid删除一批DTL数据
        /// </summary>
        public bool DeleteDtlByBid(string BID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_BONUSDTL ");
            strSql.Append(" where BID=@BID ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20)			};
            parameters[0].Value = BID;

            int rows = SqlHelper.ExecuteSql( strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获得所在dtl数据列表
        /// </summary>
        public DataTable GetDtlDataTableByBid( string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCRIMECODE 编号,fcriminal 姓名,FAMOUNT 金额,fareaName 队别,remark 备注");
            strSql.Append(" FROM T_BONUSDTL ");
            strSql.Append(" where Bid=@Bid");
            SqlParameter[] parameters = {
					new SqlParameter("@Bid", SqlDbType.VarChar,20)};
            parameters[0].Value = bid;

            DataSet ds= SqlHelper.Query( strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取明细记录Dtl的分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRow"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderByField"></param>
        /// <returns></returns>
        public List<T_BONUSDTL> GetDtlPageList(int page, int pageRow, string strWhere, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY seqno) AS RowNumber,* from T_BonusDTL");
                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                if (string.IsNullOrEmpty(orderByField) == false)
                {
                    strSql.Append(" Order by " + orderByField);
                }

                return (List<T_BONUSDTL>)conn.Query<T_BONUSDTL>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }


        public decimal[] GetDtlListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(count(*),0) FCount,isnull(sum(Famount),0) FMoney from T_BonusDtl");
            if (strWhere != "")
            {
                strSql.Append(" where " + strWhere);
            }
            else
            {
                strSql.Append(" where Flag=0 and CAmount<>0 ");
            }
            decimal[] rs = { 0, 0 };
            decimal fcount = Convert.ToDecimal(SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][0]);
            decimal fmoney = Convert.ToDecimal(SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][1]);
            rs[0] = fcount;
            rs[1] = fmoney;
            return rs;
        }


        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable( string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pc 主单流水号,fcrimecode 编号,fname 姓名,Amount 金额 ,Crtdt 导入日期,Remark 失败原因 ");
            strSql.Append(" FROM t_ImportList where pc=@bid ");

            SqlParameter[] parameters = {
					new SqlParameter("@bid", SqlDbType.VarChar,20)
			};
            parameters[0].Value = bid;

            DataSet ds= SqlHelper.Query( strSql.ToString(), parameters);
            return ds.Tables[0];

        }

        
        /// <summary>
        /// 获得指定犯人当月劳报发放次数
        /// </summary>
        public int GetSendCountByBid(string fcrimecode, string udate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(count(*),0) fcount");
            strSql.Append(" FROM T_BONUSDTL ");
            strSql.Append(" where FCrimeCode=@FCrimeCode and UDate=@UDate");
            SqlParameter[] parameters = {
					new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20),
                    new SqlParameter("@UDate", SqlDbType.VarChar,20)};
            parameters[0].Value = fcrimecode;
            parameters[1].Value = udate;
            DataTable dt = SqlHelper.Query(strSql.ToString(), parameters).Tables[0];
            return (int)dt.Rows[0][0];
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add( T_BONUS model, string partParameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_BONUS(");
            strSql.Append("BID,FAREACODE,fAMOUNT,Remark,ApplyBy,Applydt,Crtby,crtdt,fareaName,udate,ptype,cnt,FCheckFlag)");
            strSql.Append(" values (");
            strSql.Append("@BID,@FAREACODE,@fAMOUNT,@Remark,@ApplyBy,@Applydt,@Crtby,@crtdt,@fareaName,@udate,@ptype,@cnt,@FCheckFlag)");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@FAREACODE", SqlDbType.VarChar,3),
					new SqlParameter("@fAMOUNT", SqlDbType.Decimal,9),
					new SqlParameter("@Remark", SqlDbType.VarChar,512),
					new SqlParameter("@ApplyBy", SqlDbType.VarChar,20),
					new SqlParameter("@Applydt", SqlDbType.DateTime),
					new SqlParameter("@Crtby", SqlDbType.VarChar,20),
					new SqlParameter("@crtdt", SqlDbType.DateTime),					
					new SqlParameter("@fareaName", SqlDbType.VarChar,100),					
					new SqlParameter("@udate", SqlDbType.DateTime),
					new SqlParameter("@ptype", SqlDbType.VarChar,100),
					new SqlParameter("@cnt", SqlDbType.Int,4),
					new SqlParameter("@FCheckFlag", SqlDbType.Int,4)
					};
            parameters[0].Value = model.BID;
            parameters[1].Value = model.FAREACODE;
            parameters[2].Value = model.fAMOUNT;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.ApplyBy;
            parameters[5].Value = model.Applydt;
            parameters[6].Value = model.Crtby;
            parameters[7].Value = model.crtdt;
            parameters[8].Value = model.fareaName;
            parameters[9].Value = model.udate;
            parameters[10].Value = model.ptype;
            parameters[11].Value = model.cnt;
            parameters[12].Value = model.FCheckFlag;

            int rows = SqlHelper.ExecuteSql( strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 获得劳报主单当月发放次数
        /// </summary>
        public int GetSendCountByArea(string areaName, string udate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) fcount");
            strSql.Append(" FROM T_BONUS ");
            strSql.Append(" where fareaName=@fareaName and UDate=@UDate");
            SqlParameter[] parameters = {
					new SqlParameter("@fareaName", SqlDbType.VarChar,20),
                    new SqlParameter("@UDate", SqlDbType.VarChar,20)};
            parameters[0].Value = areaName;
            parameters[1].Value = udate;
            DataSet ds = SqlHelper.Query( strSql.ToString(), parameters);
            return (int)ds.Tables[0].Rows[0][0];
        }


        public string CreateOrderId(string seqnoType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @vouno varchar(30);");
            strSql.Append("exec  CREATESEQNO  '" + seqnoType + "',1,@vouno output;");
            strSql.Append("select @vouno='" + seqnoType + "'+@vouno;");
            strSql.Append("select @vouno;");

            DataSet ds = SqlHelper.Query( strSql.ToString());
            return ds.Tables[0].Rows[0][0].ToString();
        }

        //批量生成报酬金额明细
        public bool BatchCreateAreaList(string bid,string crtby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into t_bonusdtl (bid,fcrimecode,cardcode,famount,flag,fareacode,fareaname,fcriminal,vouno,frealareacode,frealareaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,cardtype,amountc)
                    select a.bid,c.fcrimecode,c.cardcodea,0 famount,0 flag,a.fareacode,a.fareaname,b.fname fcriminal,'' vouno,'' frealareacode,'' frealareaName,'' remark,'' ptype,a.udate,@crtby,@crtdt,@applyby,1 acctype,0 cardtype,0 amountc
                     from t_bonus a,t_criminal b,t_criminal_card c 
                    where a.fareacode=b.fareacode and b.fcode=c.fcrimecode
                    and a.bid=@bid and isnull(b.fflag,0)=0");

            SqlParameter[] parameters = {
                    new SqlParameter("@crtby", SqlDbType.VarChar,20),
                    new SqlParameter("@crtdt", SqlDbType.DateTime,8),
                    new SqlParameter("@applyby", SqlDbType.VarChar,20),
                    new SqlParameter("@bid", SqlDbType.VarChar,20)};
            parameters[0].Value = crtby;
            parameters[1].Value = DateTime.Today;
            parameters[2].Value = crtby;
            parameters[3].Value = bid;
            int rs = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            return rs>0;
        }
    }
}