using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using SelfhelpOrderMgr.Model;

namespace SelfhelpOrderMgr.DAL
{
    public class JifenMgrDao:BaseDapperDAL
    {
        /// <summary>
        /// 批量删除已经导入的取扣款记录
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool plDeleteByPKId(string pkId, string crtby, int typeflag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();


                    #region 增加SQL脚本
                    strSql.Append(@"update t_Criminal_Card set AccPoints=a.AccPoints-b.AccPoints from  t_Criminal_card a,(
                                select fcrimecode,sum(damount-camount) AccPoints from t_JF_Vcrd 
                                where flag=0 and isnull(bankflag,0)<=0 and typeflag=@typeflag and origid=@pkId
                                group by fcrimecode) b
                                where a.fcrimecode=b.fcrimecode;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"update T_JF_Vcrd set flag=1,delby=@crtby,deldate=getdate(),remark='已补批量删除:' +isnull(remark,'') 
                                    where flag in(0,-2) and isnull(bankflag,0)<=0 and typeflag=@typeflag and origid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade_dtl where bid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade where bid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade_ErrList where pc=@pkId;");
                    #endregion



                    object param = new { pkId = pkId, crtBy = crtby, typeflag = typeflag };
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
        /// 劳动报酬，根据 Bid '财务入账'
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(string bid, string crtby, int checkflag)
        {
            #region 根据配置文件DepositInVcrdFlag设定项，获取Flag的值
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("DepositInVcrdFlag");
            int iflag = 0;//默认值VCRD Flag为0
            //===去除Flag标志zenglj 20230709==Start=====================
            //if (mset != null)
            //{
            //    if (mset.MgrValue == "-2")
            //    {
            //        iflag = -2;
            //    }
            //}

            //===去除Flag标志zenglj 20230709==End=====================
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
                    strSql.Append(@"insert into t_JF_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                        ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
                        ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                        ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                        ,[AccType],[CheckFlag],[CheckDate]
                        ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount],PayAuditFlag,BankInterfaceFlag)
                        select 'VOUB'+substring( '00000'+convert(varchar(20),a.Id),len('00000'+convert(varchar(20),a.Id))-5,6)
                        ,a.CardCode,a.FCrimeCode,isnull(a.FAMount,0) DAmount,0 CAmount,@CrtBy CrtBy
                        ,getDate() CrtDate,b.DType as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                        ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                        ,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                        ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                        ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount],0 as PayAuditFlag,0 as BankInterfaceFlag
                        from t_JF_bonusdtl a,t_JF_bonus b,t_Criminal_Card c  
                        where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.FAMount,0)<>0 and c.cardflaga<>4;");
                    if (iflag == 0)
                    {
                        strSql.Append(@"update t_Criminal_Card set AccPoints=AccPoints+b.Fmoney from (
                            select FCrimeCode,sum(isnull(FAMount,0)) Fmoney from t_JF_bonusdtl where BID=@BID  and isnull(FAMount,0)<>0 group by fcrimecode 
                            ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode  and t_Criminal_Card.cardflaga<>4;");
                    }

                    strSql.Append(@"update t_JF_bonusDTL set Flag=1 where BID=@BID 
                    and fcrimecode in(select fcrimecode from t_Criminal_Card where cardflaga<>4);");
                    strSql.Append(@"update t_JF_bonus set cnt=b.fcount,famount=b.fmoney,Flag=1,FPostBy=@CrtBy,FPostDate=getdate(),FPostFlag=1,MainStatus=9,TargetExaminerBy='' from (
                select bid,count(*) fcount,sum(famount) fmoney from t_JF_bonusdtl 
                where BID=@BID and flag=1 group by bid) b
                where t_JF_bonus.BID=@BID and t_JF_bonus.BID=b.BID;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"insert into T_ImportList(ImportType,fcrimecode,fname,amount,crtdt,crtby,remark,pc,notes)
                select 4 ImportType,fcrimecode,fcriminal fname,famount amount,getdate() crtdate
                ,crtby ,'该记录财务入账时，已离监销户了' remark,bid pc,fareaName notes
                  from t_JF_bonusdtl where flag=0 and BID=@BID;");
                    strSql.Append(@"delete from t_JF_bonusdtl where BID=@BID and flag=0;");
                    #endregion


                    object param = new { BID = bid, CrtBy = crtby, CheckFlag = checkflag, vcrdFlag = iflag };
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
        /// 积分导入，根据不同积分模板Id来导入数据, Bid '财务入账'
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="crtby"></param>
        /// <param name="checkflag"></param>
        /// <param name="modelJifenId">积分模板的Id</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(string bid, string crtby, int checkflag,int modelJifenId)
        {
            #region 根据配置文件DepositInVcrdFlag设定项，获取Flag的值
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("DepositInVcrdFlag");
            int iflag = 0;//默认值VCRD Flag为0
            //===去除Flag标志zenglj 20230709==Start=====================
            //if (mset != null)
            //{
            //    if (mset.MgrValue == "-2")
            //    {
            //        iflag = -2;
            //    }
            //}

            //===去除Flag标志zenglj 20230709==End=====================
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

                    if(modelJifenId==2)
                    {
                        //生产积分
                        strSql.Append(@"insert into t_JF_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                        ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
                        ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                        ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                        ,[AccType],[CheckFlag],[CheckDate]
                        ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount],PayAuditFlag,BankInterfaceFlag)
                        select 'VOUB'+substring( '00000'+convert(varchar(20),a.Id),len('00000'+convert(varchar(20),a.Id))-5,6)
                        ,a.CardCode,a.FCrimeCode,isnull(a.AmountA,0) DAmount,0 CAmount,@CrtBy CrtBy
                        ,getDate() CrtDate,'生产积分' as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                        ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                        ,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                        ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                        ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount],0 as PayAuditFlag,0 as BankInterfaceFlag
                        from t_JF_bonusdtl a,t_JF_bonus b,t_Criminal_Card c  
                        where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.FAMount,0)<>0 and c.cardflaga<>4;");
                        //改造积分
                        strSql.Append(@"insert into t_JF_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                        ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
                        ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                        ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                        ,[AccType],[CheckFlag],[CheckDate]
                        ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount],PayAuditFlag,BankInterfaceFlag)
                        select 'VOUB'+substring( '00000'+convert(varchar(20),a.Id),len('00000'+convert(varchar(20),a.Id))-5,6)
                        ,a.CardCode,a.FCrimeCode,isnull(a.AmountB,0) DAmount,0 CAmount,@CrtBy CrtBy
                        ,getDate() CrtDate,'改造积分' as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                        ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                        ,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                        ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                        ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount],0 as PayAuditFlag,0 as BankInterfaceFlag
                        from t_JF_bonusdtl a,t_JF_bonus b,t_Criminal_Card c  
                        where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.FAMount,0)<>0 and c.cardflaga<>4;");
                        //扣减积分
                        //strSql.Append(@"insert into t_JF_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                        //,[CrtDate],[DType],[Depositer],[Remark],[Flag]
                        //,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                        //,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                        //,[AccType],[CheckFlag],[CheckDate]
                        //,[CheckBy],[pc],[CurUserAmount],[CurAllAmount],PayAuditFlag,BankInterfaceFlag)
                        //select 'VOUB'+substring( '00000'+convert(varchar(20),a.Id),len('00000'+convert(varchar(20),a.Id))-5,6)
                        //,a.CardCode,a.FCrimeCode,0 DAmount,isnull(a.grkj,0) CAmount,@CrtBy CrtBy
                        //,getDate() CrtDate,'扣减积分' as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                        //,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                        //,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                        //,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                        //,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount],0 as PayAuditFlag,0 as BankInterfaceFlag
                        //from t_JF_bonusdtl a,t_JF_bonus b,t_Criminal_Card c  
                        //where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.FAMount,0)<>0 and c.cardflaga<>4;");

                    }
                    else
                    {
                        strSql.Append(@"insert into t_JF_Vcrd ([Vouno],[CardCode],[FCrimeCode],[DAmount],[CAmount],[CrtBy]
                        ,[CrtDate],[DType],[Depositer],[Remark],[Flag]
                        ,[FAreaCode],[FAreaName],[FCriminal],[Frealareacode]
                        ,[FrealAreaName],[PType],[UDate],[OrigId],[CardType],[TypeFlag],[SubTypeFlag]
                        ,[AccType],[CheckFlag],[CheckDate]
                        ,[CheckBy],[pc],[CurUserAmount],[CurAllAmount],PayAuditFlag,BankInterfaceFlag)
                        select 'VOUB'+substring( '00000'+convert(varchar(20),a.Id),len('00000'+convert(varchar(20),a.Id))-5,6)
                        ,a.CardCode,a.FCrimeCode,isnull(a.FAMount,0) DAmount,0 CAmount,@CrtBy CrtBy
                        ,getDate() CrtDate,b.DType as DType,'' Depositer,a.Remark,@vcrdFlag as Flag,a.FAreaCode,a.FAreaName,a.FCriminal,'' Frealareacode
                        ,'' FrealAreaName,'' PType,b.Udate,a.BID OrigId,0 CardType,b.TypeFlag as TypeFlag,b.SubTypeFlag as SubTypeFlag
                        ,0 AccType,@CheckFlag [CheckFlag],getDate() [CheckDate]
                        ,@CrtBy [CheckBy],0 [pc],(c.AmountA+c.AmountB) [CurUserAmount]
                        ,(c.AmountA+c.AmountB+c.AmountC)[CurAllAmount],0 as PayAuditFlag,0 as BankInterfaceFlag
                        from t_JF_bonusdtl a,t_JF_bonus b,t_Criminal_Card c  
                        where a.Bid=b.Bid and a.FCrimeCode=c.FCrimeCode and b.BID=@BID and isnull(a.FAMount,0)<>0 and c.cardflaga<>4;");
                    }


                    if (iflag == 0)
                    {
                        strSql.Append(@"update t_Criminal_Card set AccPoints=AccPoints+b.Fmoney from (
                            select FCrimeCode,sum(isnull(FAMount,0)) Fmoney from t_JF_bonusdtl where BID=@BID  and isnull(FAMount,0)<>0 group by fcrimecode 
                            ) b where t_Criminal_Card.FCrimeCode=b.FCrimeCode  and t_Criminal_Card.cardflaga<>4;");
                    }

                    strSql.Append(@"update t_JF_bonusDTL set Flag=1 where BID=@BID 
                    and fcrimecode in(select fcrimecode from t_Criminal_Card where cardflaga<>4);");
                    strSql.Append(@"update t_JF_bonus set cnt=b.fcount,famount=b.fmoney,Flag=1,FPostBy=@CrtBy,FPostDate=getdate(),FPostFlag=1,MainStatus=9,TargetExaminerBy='' from (
                select bid,count(*) fcount,sum(famount) fmoney from t_JF_bonusdtl 
                where BID=@BID and flag=1 group by bid) b
                where t_JF_bonus.BID=@BID and t_JF_bonus.BID=b.BID;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"insert into T_ImportList(ImportType,fcrimecode,fname,amount,crtdt,crtby,remark,pc,notes)
                select 4 ImportType,fcrimecode,fcriminal fname,famount amount,getdate() crtdate
                ,crtby ,'该记录财务入账时，已离监销户了' remark,bid pc,fareaName notes
                  from t_JF_bonusdtl where flag=0 and BID=@BID;");
                    strSql.Append(@"delete from t_JF_bonusdtl where BID=@BID and flag=0;");
                    #endregion


                    object param = new { BID = bid, CrtBy = crtby, CheckFlag = checkflag, vcrdFlag = iflag };
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
                    strSql.Append(@"update t_criminal_Card set AccPoints=AccPoints-t.A from (
                                    select  fcrimecode,sum(Damount-Camount) A 
                                    from t_JF_vcrd
                                    where flag=0
                                    and origid=@bid
                                    group by fcrimecode) t
                                    where t_criminal_Card.fcrimecode=t.fcrimecode;");
                    strSql.Append("delete from t_JF_Vcrd where flag in(0,-2) and origid=@bid;");
                    strSql.Append("update t_JF_BonusDtl set flag=0 where Bid=@bid;");
                    strSql.Append("update t_JF_Bonus set flag=0,FDBCheckFlag=0,auditflag=0,FPostFlag=0,fcheckflag=0 where Bid=@bid;");


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



        /// <summary>
        /// 批量写入劳动报酬明细记录，标准格式：编号、姓名、金额、备注
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="crtby"></param>
        /// <param name="resaveFlag">可以重复发放的标志</param>
        /// <returns></returns>
        public bool PLWriteBonusDtl(string bid, string crtby, int reSaveFlag = 0)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                T_SETTINGS sets = new T_SETTINGSDAL().GetModel(169);
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
                        where t_bonus_Temp.FCrimeCode=B.fcode and isnull(b.FFlag,0)=1  and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");
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
                        strSql.Append(@"update t_bonus_Temp set notes='该犯该月('+left(convert(varchar(20),c.udate,120),10)+')已经发放过了，不可以重复发放' from
                        (select e.bid,e.DType,e.TypeFlag, f.udate,FCRIMECODE,f.fcriminal from T_JF_BONUS e,T_JF_BONUSDTL f where e.BID=f.BID) c,
						(select b.udate,b.DType,a.* from t_bonus_Temp a left join t_jf_bonus b on a.bid=b.bid
						where a.bid=@bid) d
						where t_bonus_Temp.bid=d.bid and t_bonus_Temp.fcrimecode=d.fcrimecode and c.udate=d.udate and c.fcrimecode=d.fcrimecode
						and c.DType=d.DType;");
                    }

                    if (mset.MgrValue == "1")
                    {
                        //该用户银行卡不存在
                        strSql.Append(@"update t_bonus_Temp set Notes ='该用户银行卡不存在' where FCrimeCode not in(
                        select distinct FCrimeCode from t_Criminal_Card where isnull(regFlag,0)=1 and isnull(BankAccNo,'')<>'' ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    }


                    //更新 AmountA+AmountB<>FMoney不一致的记录
                    strSql.Append(@"update t_bonus_Temp set Notes ='生产积分+改造积分<>总积分' where AmountA+AmountB<>FMoney and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");


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
                    strSql.Append(@"insert into  t_JF_bonusdtl
                         (BID,FCRIMECODE,CARDCODE,FAMOUNT,FLAG,fareacode,fareaName,fcriminal,vouno
                        ,Frealareacode,FrealAreaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,cardtype
                        ,AmountA,AmountB,AmountC,cqbt,gwjt,ldjx,tbbz,grkj)
                        select 
                        a.Bid BID,a.FCrimeCode FCRIMECODE,b.CardCodeA CARDCODE,a.FMoney FAMOUNT,0 FLAG,c.FAreaCode fareacode,d.FName fareaName,FCriminal fcriminal,'' vouno
                        ,'' Frealareacode,'' FrealAreaName,FRemark remark,e.Remark ptype,e.UDate udate,e.CrtBy crtby,getDate() crtdt,e.applyby applyby,1 acctype,0 cardtype
                        ,a.AmountA as AmountC,a.AmountB as AmountB,0 as AmountC,0 cqbt,0 gwjt,0 ldjx,0 tbbz,0 grkj
                        from t_bonus_Temp a,t_Criminal_Card b,t_Criminal c,t_Area d,T_JF_Bonus e,t_Cy_Type f
                        where a.Bid=@Bid and a.Notes='' 
                        and a.FCrimeCode=C.FCode and B.FCrimeCode=C.FCode and C.FAreaCode=D.FCode
                        and a.Bid=e.Bid and f.FCode=C.FCyCode;");
                    //更新主单金额
                    strSql.Append(@"update t_JF_bonus set FPostBy=@CrtBy,FPostDate=getDate(),FPostFlag=1,cnt=b.FCount,FAmount=B.FAmount from (
            select BID,sum(FAmount) FAmount,count(*) FCount from t_JF_bonusdtl where Bid=@Bid
            group by Bid ) b where t_Jf_bonus.Bid=@Bid and isnull(Flag,0)=0;");
                    //删除t_bonus_Temp过渡表记录
                    strSql.Append("delete from t_bonus_Temp where Bid=@Bid");
                    object param = new { Bid = bid, CrtBy = crtby, FCount = sets.VALUE };
                    conn.Execute(strSql.ToString(), param, myTran, 300);
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



        //批量生成报酬金额明细
        public bool BatchCreateAreaList(string bid, string crtby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into t_JF_bonusdtl (bid,fcrimecode,cardcode,famount,flag,fareacode,fareaname,fcriminal,vouno,frealareacode,frealareaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,cardtype,amountc)
                    select a.bid,c.fcrimecode,c.cardcodea,0 famount,0 flag,a.fareacode,a.fareaname,b.fname fcriminal,'' vouno,'' frealareacode,'' frealareaName,'' remark,'' ptype,a.udate,@crtby,@crtdt,@applyby,1 acctype,0 cardtype,0 amountc
                     from t_JF_bonus a,t_criminal b,t_criminal_card c 
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
            return rs > 0;
        }


        #region 消费单打印的功能

        /// <summary>
        /// 小票合并统计打印
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="printSumOption"></param>
        /// <returns></returns>
        public List<T_JF_InvoiceDTL> GetInvoiceDtlList(string strWhere, int printSumOption)
        {
            StringBuilder strSql = new StringBuilder();
            if (printSumOption == 1)
            {
                strSql.Append(@"SELECT 0 as [seqno],[INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag]
                    ,[GDJ],sum(QTY) as[QTY],sum(Amount) as[AMOUNT],0 as [Servamount],[GTXM],[FCrimecode],0 as [GORGDJ],0 as [GORGAMT]
                    ,[StockSeqno],[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag]
                    ,[Backqty],[FreeFlag],[Remark],[SPShortCode],[FTZSP_TypeFlag]
                      FROM [T_JF_InvoiceDTL]");

                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(@" group by  [INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag]
                        ,[GDJ],[GTXM],[FCrimecode]
                        ,[StockSeqno],[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag]
                        ,[Backqty],[FreeFlag],[Remark],[SPShortCode],[FTZSP_TypeFlag] ");
            }
            else
            {
                strSql.Append("select * ");
                strSql.Append(" FROM T_InvoiceDTL ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //param 直接传实体进去就可以
                var rs = SqlMapper.Query<T_JF_InvoiceDTL>(conn, strSql.ToString(), null);//得出自增长的Id

                return rs.ToList();
            }

        }

        #endregion


        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="strInvoices"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool CancelInvoiceOrder(string strInvoices, string crtby)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_GOODSSTOCKMAIN set BALANCE=BALANCE+b.qty from (
                select GCode,sum(qty) qty from t_JF_InvoiceDTL where invoiceNo in(" + strInvoices + @")
                group by GCode) b
                where T_GOODSSTOCKMAIN.GCode=b.GCode;");//加回库存量
            strSql.Append(@"update t_JF_InvoiceDtl set Flag=0 where InvoiceNo in(" + strInvoices + ");");//删除消费明细
            strSql.Append(@"update t_JF_Invoice set flag=0,remark=@crtby +',后台撤单'+'" + DateTime.Now.ToString() + "' where InvoiceNo in(" + strInvoices + ");");//删除消费主单
            strSql.Append(@"Update t_Sho_orderDTL set Flag=0 where orderId in(
                select OrderId from t_Sho_order where InvoiceNo in(" + strInvoices + "));");//删除订单明细
            strSql.Append(@"Update t_Sho_order set Flag=0 where InvoiceNo in(" + strInvoices + ");");//删除主订单
            strSql.Append(@"delete from t_stockDtl where stockid in(
                select stockid from t_stock where invoiceno in(" + strInvoices + "));");//删除出库单明细
            strSql.Append(@"Delete from t_stock where InvoiceNo in (" + strInvoices + ");");//删除主出库单
            strSql.Append(@"update t_Criminal_Card set AccPoints=AccPoints+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_JF_Vcrd where OrigId in (" + strInvoices + @") and Flag=0  and isnull(BankFlag,0)<=0 
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");//更新账户余额
            strSql.Append(@"update t_JF_Vcrd set flag=1,delby=@crtby,deldate=getdate() where OrigId in(" + strInvoices + ") and Flag=0  and isnull(BankFlag,0)=0;");//删除VCrd的记录
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                object paramInvoice = new { InvoiceNo = strInvoices, crtby = crtby };
                try
                {
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 订单全部退货
        /// </summary>
        /// <param name="strInvoices"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool ReturnInvoiceOrder(string strInvoices, string crtby)
        {

            StringBuilder strSql = new StringBuilder();

            
            //退货主单
            strSql.Append(@"insert into [T_JF_Invoice] ([INVOICENO],[cardcode],[fcrimecode],[amount],[OrderDate]
                  ,[PayDATE],[PTYPE],[Flag],[REMARK],[servamount],[crtby],[crtdate]
                  ,[fsn],[fareacode],[fareaName],[fcriminal],[Frealareacode]
                  ,[FrealAreaName],[TYPEFLAG],[CardType],[AmountA],[AmountB]
                  ,[fifoflag],[FreeAmountA],[FreeAmountB],[checkflag],[RoomNo]
                  ,[OrderId],[FTZSP_Money],[printCount],[OrderStatus],[UserCyDesc])
            SELECT 'TH'+[INVOICENO] as [INVOICENO],[cardcode],[fcrimecode],[amount],getdate() as [OrderDate]
                  ,[PayDATE],'积分退货' as [PTYPE],[Flag],@CrtBy + ',批量退货,原单'+[INVOICENO]+'.'+[REMARK] as [REMARK],[servamount],@CrtBy as [crtby],getdate() as [crtdate]
                  ,[fsn],[fareacode],[fareaName],[fcriminal],[Frealareacode]
                  ,[FrealAreaName],11 as [TYPEFLAG],[CardType],[AmountA],[AmountB]
                  ,1 as [fifoflag],[FreeAmountA],[FreeAmountB],[checkflag],[RoomNo]
                  ,[OrderId],[FTZSP_Money],0 as [printCount],[OrderStatus],[UserCyDesc]
              FROM [dbo].[T_JF_Invoice]
              where INVOICENO in(" + strInvoices + ");");
            //退货明细
            strSql.Append(@"insert into [T_JF_InvoiceDTL] ([INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag]
                    ,[GDJ],[QTY],[AMOUNT],[servamount],[GTXM],[fcrimecode]
                    ,[GORGDJ],[GORGAMT],[StockSeqno],[typeflag],[cardtype]
                    ,[AmountA],[AmountB],[fifoflag],[backqty],[freeflag]
                    ,[Remark],[SPShortCode],[FTZSP_TypeFlag])
            SELECT 'TH'+[INVOICENO] as [INVOICENO],[GCODE],[GNAME],getdate() as [OrderDate],[PayDATE],[Flag]
                    ,[GDJ],[QTY],[AMOUNT],[servamount],[GTXM],[fcrimecode]
                    ,[GORGDJ],[GORGAMT],[StockSeqno],11 as [typeflag],[cardtype]
                    ,[AmountA],[AmountB],1 as [fifoflag],[QTY] as [backqty],[freeflag]
                    ,'对应的消费主单：'+[INVOICENO]  as [Remark],[SPShortCode],[FTZSP_TypeFlag]
                FROM [dbo].[T_JF_InvoiceDTL]
                where INVOICENO in(" + strInvoices + ");");


            strSql.Append(@"INSERT INTO [T_Stock] ([StockId],[InOutDate],[FLAG],[StockType],[CrtBy],[Crtdt],
                        [CHECKFLAG],[CHECKBY],[CheckDt],[Remark],[invoiceno],[stockflag],[InOutFlag])
                        select 'S'+InvoiceNo as [StockId],getdate()[InOutDate],[FLAG],'积分退货' as [StockType],@CrtBy as [CrtBy],getdate() as [Crtdt],
                        1 as [CHECKFLAG],@CrtBy as [CHECKBY],getdate() [CheckDt],'对应的消费主单：'+INVOICENO as [Remark],'TH'+InvoiceNo as[invoiceno],
                        6 as [stockflag],-1 as [InOutFlag] from T_JF_Invoice where InvoiceNo in(" + strInvoices + ");");

            strSql.Append(@"INSERT INTO [T_StockDTL]
	                    ([StockId],[GCODE],[GTXM],[GCOUNT],[GDJ],[flag],[stockflag],[InOutFlag],[Remark])
                    select 'S'+InvoiceNo as [StockId],[GCODE],[GTXM],QTY as [GCOUNT],[GDJ],[flag],6 as[stockflag],1 as [InOutFlag],'对应的消费主单：'+INVOICENO as  [Remark]
                    from [T_JF_InvoiceDTL] where INVOICENO in(" + strInvoices + ");");


            strSql.Append(@"update T_GOODSSTOCKMAIN set BALANCE=BALANCE+b.qty from (
                select GCode,sum(qty) qty from t_JF_InvoiceDTL where invoiceNo in(" + strInvoices + @")
                group by GCode) b
                where T_GOODSSTOCKMAIN.GCode=b.GCode;");//加回库存量

            strSql.Append(@"insert into T_JF_Vcrd(VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER
                    ,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype
                    ,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount
                    ,curAllAmount,PayAuditFlag,[FinancePayFlag],[BankInterfaceFlag])
                    select 'V'+InvoiceNo as VOUNO,cardcode,fcrimecode,amount as DAMOUNT,0 as CAMOUNT,@CrtBy as CrtBy,CRTDATE
                    ,'积分退货' as DTYPE,'' as DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName
                    ,ptype,getdate() as udate,'TH' + [InvoiceNo] as origid,cardtype,TYPEFLAG,0 as acctype,0 as Bankflag,checkflag
                    ,@CrtBy as checkby,0 as pc,0 as curUserAmount,0 as curAllAmount,0 as PayAuditFlag,0 as [FinancePayFlag],0 as [BankInterfaceFlag] 
                    from T_JF_Invoice where INVOICENO in(" + strInvoices + ");");

            strSql.Append(@"update t_criminal_card set AccPoints=AccPoints+b.fmoney from t_criminal_card a
                    ,(select fcrimecode,sum(Amount) as fmoney from T_JF_Invoice where  INVOICENO in(" + strInvoices + @") group by fcrimecode ) b 
                                    where a.fcrimecode=b.fcrimecode;");//更新账户余额

            //标记为退货
            strSql.Append("update T_JF_Invoice set Remark='该订单已全部退货.'+isnull(Remark,'') where INVOICENO in(" + strInvoices + ");");

            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                object paramInvoice = new { InvoiceNo = strInvoices, CrtBy = crtby };
                try
                {
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                    return false;
                }
            }
        }


        

        public string AddTuiHuoOrder(List<T_JF_InvoiceDTL> models, string crtby, string ipLastCode)
        {

            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                T_Criminal criminal = new T_CriminalDAL().GetModel(models[0].FCrimecode);
                T_AREA area = new T_AREADAL().GetModel(criminal.FAreaCode);
                T_Criminal_card tcard = new T_Criminal_cardDAL().GetModel(models[0].FCrimecode);

                StringBuilder strSql = new StringBuilder();
                string invoiceno = "";
                string stockid = "";
                string vouno = "";

                strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'Inv',1,@vouno output;");
                strSql.Append("select @vouno='INV'+@vouno +@IpLastCode;");
                strSql.Append("select @vouno;");
                object paramInv = new { IpLastCode = ipLastCode };
                List<string> dd = (List<string>)conn.Query<string>(strSql.ToString(), paramInv);
                invoiceno = dd[0].ToString();


                strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'S',1,@vouno output;");
                strSql.Append("select @vouno='S'+@vouno +@IpLastCode;");
                strSql.Append("select @vouno;");
                object paramInv2 = new { IpLastCode = ipLastCode };
                List<string> dd2 = (List<string>)conn.Query<string>(strSql.ToString(), paramInv2);
                stockid = dd2[0].ToString();


                strSql = new StringBuilder();
                int typeflag = 11;//消费退货
                string remark = "对应的消费主单：" + models[0].INVOICENO;
                decimal sumAmount = 0;
                decimal sumTzspMoney = 0;
                foreach (T_JF_InvoiceDTL dtl in models)
                {
                    sumAmount = sumAmount + dtl.AMOUNT;//汇总金额
                    if (dtl.FTZSP_TypeFlag == 1)
                    {
                        sumTzspMoney = sumTzspMoney + dtl.AMOUNT;
                    }

                    strSql.Append(@"INSERT INTO [T_JF_InvoiceDTL]
                               ([INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag],[GDJ]
                    ,[QTY],[AMOUNT],[Servamount],[GTXM],[FCrimecode],[GORGDJ],[GORGAMT],[StockSeqno]
                    ,[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag],[Backqty],[FreeFlag]
                    ,[Remark],[SPShortCode],[FTZSP_TypeFlag])
                         VALUES ('" + invoiceno + "','" + dtl.GCODE + "','" + dtl.GNAME + "',getdate(),getdate(),1," + dtl.GDJ + @"
                    ," + dtl.QTY + "," + dtl.AMOUNT + ",0,'" + dtl.GTXM + "','" + dtl.FCrimecode + "'," + dtl.GDJ + "," + dtl.AMOUNT + @",0
                    ," + typeflag + ",0," + dtl.AMOUNT + ",0,1,0,0,'" + remark + "','" + dtl.SPShortCode + "'," + dtl.FTZSP_TypeFlag.ToString() + ");");


                }

                strSql.Append(@"INSERT INTO [T_JF_Invoice]
                     ([InvoiceNo],[CardCode],[FCrimeCode],[Amount],[OrderDate],[PayDate],[PType],[Flag],[Remark],[ServAmount]
                    ,[Crtby],[Crtdate],[fsn],[FAreaCode],[FAreaName],[FCriminal],[Frealareacode],[FrealAreaName],[TypeFlag]
                    ,[CardType],[AmountA],[AmountB],[Fifoflag],[FreeAmountA],[FreeAmountB],[Checkflag],[RoomNo]
                    ,[OrderId],[FTZSP_Money],[printCount],[UserCyDesc],[OrderStatus]) 
                    select '" + invoiceno + "' [InvoiceNo],'" + tcard.cardcodea + "' [CardCode],a.fcode as  [FCrimeCode]," + sumAmount + " as  [Amount],getdate() [OrderDate],getdate() [PayDate],'积分退货',1 as [Flag],'" + remark + @"' [Remark],0
                    ,'" + crtby + "' [Crtby],getdate() [Crtdate],'',[FAreaCode],b.fname [FAreaName],a.fname [FCriminal],'',''," + typeflag.ToString() + @" as [TypeFlag]
                    ,0," + sumAmount.ToString() + " as [AmountA],0,1,0,0,0,0,0," + sumTzspMoney.ToString() + ",0,'',0 from t_criminal a,t_area b where a.fareacode=b.fcode and a.fcode='" + models[0].FCrimecode + "';");


                //写入库存记录
                strSql.Append(@"INSERT INTO [T_Stock] ([StockId],[InOutDate],[FLAG],[StockType],[CrtBy],[Crtdt],
                        [CHECKFLAG],[CHECKBY],[CheckDt],[Remark],[invoiceno],[stockflag],[InOutFlag])
                         VALUES('" + stockid + "',getdate(),-1,'积分退货','" + crtby + @"',getdate()
                    ,1,'" + crtby + "',getdate(),'" + remark + "','" + invoiceno + "',6,1);");

                foreach (T_JF_InvoiceDTL dtl in models)
                {
                    strSql.Append(@"INSERT INTO [T_StockDTL]
                           ([StockId],[GCODE],[GTXM],[GCOUNT],[GDJ],[flag],[stockflag],[InOutFlag],[Remark])
                            values('" + stockid + "','" + dtl.GCODE + "','" + dtl.GTXM + "','" + dtl.QTY + "','" + dtl.GDJ + "',1,6,1,'" + remark + "');");

                    strSql.Append(@"UPDATE [T_GOODSSTOCKMAIN] SET balance =balance+" + dtl.QTY + " WHERE gcode='" + dtl.GCODE + "';");
                }




                decimal invAmountA = sumAmount;
                decimal invAmountB = 0;

                //先获取当前AB账户余额,实现插入消费记录时，将当前的账户余额一并写入到curUserAmout

                decimal curUserAmountA = tcard.AmountA;
                decimal curUserAmountB = tcard.AmountB;
                decimal curAllAmount = tcard.AmountA + tcard.AmountB + tcard.AmountC;

                //插入VCrd记录及更新余额

                strSql.Append("insert into T_JF_Vcrd(");
                strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount,PayAuditFlag,[FinancePayFlag],[BankInterfaceFlag]");
                strSql.Append(") values (");
                strSql.Append("'" + invoiceno + "','" + tcard.cardcodea + "','" + tcard.fcrimecode + "'," + sumAmount.ToString() + ",0,'" + crtby + "',getdate(),'积分退货','','" + remark + "',0,'" + area.FCode + "','" + area.FName + "','" + criminal.FName + "','','','',getdate(),'" + invoiceno + "',0,11,0,0,1,'" + crtby + "',0,0,0,0,0,0");
                strSql.Append(");");

                //更新金额
                strSql.Append("Update t_criminal_card set AccPoints=AccPoints+" + sumAmount + " where fcrimecode='" + criminal.FCode + "'");

                //标记为退货
                strSql.Append($"update T_JF_Invoice set Remark='该订单已有部分退货.'+isnull(Remark,'') where INVOICENO ='{models[0].INVOICENO}';");

                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    int i = conn.Execute(strSql.ToString(), null, myTran);
                    myTran.Commit();
                    return "OK|退货成功";
                }
                catch
                {
                    myTran.Rollback();
                    return "Err|失败,数据已经回滚";
                }
            }
        }


        #region 总账清单相关函数

        public bool ChangeVcrdListType(string invoiceNo, string dtype, int subTypeFlag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update t_JF_invoice set flag=0,remark=@remark where flag=1 and invoiceno=@invoiceno;");
                strSql.Append("delete from t_JF_invoicedtl where invoiceno=@invoiceno;");
                strSql.Append("update t_JF_vcrd set dtype=@dtype,remark='特殊情况消费退货,从银行退回该款，从'+dtype +',改为当前类型',typeflag=2,subTypeFlag=@subTypeFlag where flag=0 and origid=@invoiceno");
                var param = new { remark = "该记录银行已扣款，手动退货删除,改为" + dtype, invoiceno = invoiceNo, dtype = dtype, subTypeFlag = subTypeFlag };
                try
                {
                    int rs = conn.Execute(strSql.ToString(), param);
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }


        public bool DeleteDtlByVcrdSeqno(T_JF_Vcrd vcrd)
        {
            bool rflag = false;
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                StringBuilder strSql = new StringBuilder();
                switch (vcrd.TypeFlag)
                {
                    case 4://劳动报酬
                        {
                            //1.删除Vcrd记录
                            //2.删除T_BonusDTL
                            //3.更新T_Bonus金额
                            //4.更新T_Criminal_Card余额
                            strSql.Append(@"update t_JF_bonusdtl set flag=0 from t_JF_bonusdtl a left join 
                                (select a.origid,a.fcrimecode,sum(Damount) Damount from t_JF_vcrd a,(
                                select origid,fcrimecode from t_JF_vcrd where Id=@Id) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) b 
                                on a.fcrimecode=b.fcrimecode 
                                where b.damount=a.famount and ( convert(varchar(20), a.Id)=b.origid or a.bid=b.origid);");

                            strSql.Append(@"update t_JF_bonus set cnt=cnt-1,FAmount=FAmount-b.Damount from t_JF_bonus a left join 
                                (select a.origid,a.fcrimecode,sum(Damount) Damount from t_JF_vcrd a,(
                                select origid,fcrimecode from t_JF_vcrd where Id=@Id) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) b 
                                on a.bid=b.origid ");


                        }
                        break;
                    case 7://超市消费
                        {
                            //软删除T_invoice记录
                            strSql.Append(@"update t_JF_invoice set Flag=0,Remark=Remark+',银行无法扣款手动删除' from t_JF_invoice c inner join (
                                select a.origid,a.fcrimecode,sum(Camount) Camount from t_JF_vcrd a,(
                                select origid,fcrimecode from t_JF_vcrd where Id=@Id) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) d on c.invoiceno=d.origid;");
                            strSql.Append(@"delete from t_JF_invoicedtl where invoiceno=
                                (select origid from t_JF_vcrd where Id=@Id);");

                            //不删除购物车记录，可以用于查询被删除的明细
                            //增加相应的库存量
                            strSql.Append(@"update T_GOODSSTOCKMAIN set balance=balance+b.gcount from T_GOODSSTOCKMAIN a inner join (
                                select gcode,gcount from t_stockdtl where stockid=
                                (select stockid from t_stock where invoiceno=@origid)) b
                                on a.gcode=b.gcode;");

                            //删除明细
                            strSql.Append(@"delete from t_stockdtl where StockID=
                                (select StockID from t_stock where InvoiceNo=@origid);");

                            //删除主单
                            strSql.Append(@"delete from t_stock where InvoiceNo=@origid;");



                        }
                        break;
                    default:
                        break;
                }


                List<T_JF_Vcrd> list = base.QueryList<T_JF_Vcrd>("flag=0 and bankflag=-1 and fcrimecode='" + vcrd.FCrimeCode + "' and origid='" + vcrd.OrigId + "'");

                decimal chgAmount = list.Sum(o => o.DAmount - o.CAmount);


                //更新余额表
                strSql.Append(@"update t_Criminal_Card set AccPoints=AccPoints-@chgAmount where fcrimecode=@fcrimecode;");
                //删除余额记录
                strSql.Append(@"update t_Vcrd set flag=1 , BankFlag=-1 , Remark=Remark+',手动删除' where fcrimecode=@fcrimecode and Id=@Id;");


                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    object param = new { origid = vcrd.OrigId, chgAmount = chgAmount, fcrimecode = vcrd.FCrimeCode, Id = vcrd.Id };
                    conn.Execute(strSql.ToString(), param, myTran);
                    myTran.Commit();
                    rflag = true;
                }
                catch (Exception)
                {
                    myTran.Rollback();
                    throw;
                }

            }
            return rflag;
        }

        #endregion

    }
}