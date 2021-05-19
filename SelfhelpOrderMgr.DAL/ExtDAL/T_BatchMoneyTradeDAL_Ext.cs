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
    public partial class T_BatchMoneyTradeDAL
    {
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_BatchMoneyTrade  set cnt=b.fcount,famount=b.fmoney from 
                (select bid,count(*) fcount,sum(famount) fmoney from T_BatchMoneyTrade_DTL where bid=@BID
                group by bid) b
                where T_BatchMoneyTrade.bid=b.bid
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


        

        public bool PLWriteTradeDtl(string bid, string crtby)
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
                    //更新 该用户已离监无法导入
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户已离监无法导入' from t_Criminal b
                        where t_bonus_Temp.FCrimeCode=B.fcode and isnull(b.FFlag,0)=1 and t_bonus_Temp.Bid=@Bid;");
                    //该用户狱号不存在
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户狱号不存在' where FCrimeCode not in(
                        select distinct fcode from t_Criminal ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    //更新 金额为0的不导入
                    strSql.Append(@"update t_bonus_Temp set Notes ='金额不能为0' 
                        where Notes='' and FMoney=0;");
                    //更新姓名不一致的
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户名:'+FCriminal+'与'+B.FName+'，不一致' from t_Criminal b
                        where t_bonus_Temp.FCrimeCode=B.fcode and t_bonus_Temp.FCriminal<>b.FName and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");
                    //更新 该用户没有办狱内IC卡
                    strSql.Append(@"update t_bonus_Temp set Notes ='该用户没有办狱内IC卡' where FCrimeCode not in(
                        select FCrimeCode from t_Criminal_Card where Cardflaga<>4 )  and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    if (mset.MgrValue == "1")
                    {
                        //该用户银行卡不存在
                        strSql.Append(@"update t_bonus_Temp set Notes ='该用户银行卡不存在' where FCrimeCode not in(
                        select distinct FCrimeCode from t_Criminal_Card where isnull(regFlag,0)=1 and isnull(BankAccNo,'')<>'' ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    }

                    //更新 该用户本月已经发放了
                    strSql.Append(@"update t_bonus_Temp set Notes='该用户本月已经发放了'+Convert(varchar(20), b.FCount)+'次，不能再发放'  from ( 
                        select FCrimeCode,Count(*) FCount from t_BonusDtl,t_Bonus where t_BonusDtl.Udate=t_Bonus.Udate and t_Bonus.Bid=@Bid
                        group by FCrimeCode
                        having Count(*)>@FCount) b
                        where t_bonus_Temp.FCrimeCode=B.FCrimeCode and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //更新 有两条及以上相同金额的记录
                    strSql.Append(@"update t_bonus_Temp set Notes='有两条及以上相同金额的记录'  from ( 
                        select bid,FCrimeCode,Fmoney,Count(*) FCount from t_bonus_Temp
						group by bid,FCrimeCode,Fmoney having Count(*)>1) b
                        where t_bonus_Temp.FCrimeCode=B.FCrimeCode and t_bonus_Temp.bid=B.bid and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //写入到失败记录表
                    strSql.Append(@"insert into T_BatchMoneyTrade_ErrList 
                        (ImportType,FCrimeCode,FName,Amount,Crtdt,CrtBy,Remark,Pc,Notes)
                        select 4 ImportType,FCrimeCode,FCriminal FName,FMoney Amount,GetDate() Crtdt,@CrtBy CrtBy,FRemark+':' +Notes Remark,@Bid Pc,Notes Notes
                        from t_Bonus_Temp where Bid=@Bid and Notes<>'';");
                    //写入成功记录
                    strSql.Append(@"insert into  T_BatchMoneyTrade_DTL
                         (BID,fmoneyInOutFlag,FCRIMECODE,CARDCODE,FAMOUNT,FLAG,fareacode,fareaName,fcriminal,vouno
                        ,Frealareacode,FrealAreaName,remark,ptype,udate,crtby,crtdt,applyby,acctype,cardtype
                        ,AmountC,cqbt,gwjt,ldjx,tbbz,grkj)
                        select 
                        a.Bid BID,-1 FmoneyInOutFlag,a.FCrimeCode FCRIMECODE,b.CardCodeA CARDCODE,a.FMoney FAMOUNT,0 FLAG,c.FAreaCode fareacode,d.FName fareaName,FCriminal fcriminal,'' vouno
                        ,'' Frealareacode,'' FrealAreaName,FRemark remark,e.Remark ptype,e.UDate udate,e.CrtBy crtby,getDate() crtdt,e.applyby applyby,1 acctype,0 cardtype
                        ,0 AmountC,0 cqbt,0 gwjt,0 ldjx,0 tbbz,0 grkj
                        from t_bonus_Temp a,t_Criminal_Card b,t_Criminal c,t_Area d,T_BatchMoneyTrade e,t_Cy_Type f
                        where a.Bid=@Bid and a.Notes='' 
                        and a.FCrimeCode=C.FCode and B.FCrimeCode=C.FCode and C.FAreaCode=D.FCode
                        and a.Bid=e.Bid and f.FCode=C.FCyCode;");
                    //更新主单金额
                    strSql.Append(@"update T_BatchMoneyTrade set FPostBy=@CrtBy,FPostDate=getDate(),cnt=b.FCount,FAmount=B.FAmount from (
            select BID,sum(FAmount) FAmount,count(*) FCount from T_BatchMoneyTrade_DTL where Bid=@Bid
            group by Bid ) b where T_BatchMoneyTrade.Bid=@Bid and isnull(Flag,0)=0;");
                    //删除t_bonus_Temp过渡表记录
                    strSql.Append("delete from t_bonus_Temp where Bid=@Bid");
                    object param = new { Bid = bid, CrtBy = crtby, FCount = 2 };
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
        /// 获取明细记录Dtl的分页数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRow"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderByField"></param>
        /// <returns></returns>
        public List<T_BatchMoneyTrade_DTL> GetDtlPageList(int page, int pageRow, string strWhere, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY seqno) AS RowNumber,* from T_BatchMoneyTrade_DTL");
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

                return (List<T_BatchMoneyTrade_DTL>)conn.Query<T_BatchMoneyTrade_DTL>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }


        public decimal[] GetDtlListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(count(*),0) FCount,isnull(sum(Famount),0) FMoney from T_BatchMoneyTrade_DTL");
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
        /// 获得所在dtl数据列表
        /// </summary>
        public DataTable GetDtlDataTableByBid(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCRIMECODE 编号,fcriminal 姓名,FAMOUNT 金额,fareaName 队别,remark 备注");
            strSql.Append(" FROM T_BatchMoneyTrade_DTL ");
            strSql.Append(" where Bid=@Bid");
            SqlParameter[] parameters = {
					new SqlParameter("@Bid", SqlDbType.VarChar,20)};
            parameters[0].Value = bid;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable(string bid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pc 主单流水号,fcrimecode 编号,fname 姓名,Amount 金额 ,Crtdt 导入日期,Remark 失败原因 ");
            strSql.Append(" FROM T_BatchMoneyTrade_ErrList where pc=@bid ");

            SqlParameter[] parameters = {
					new SqlParameter("@bid", SqlDbType.VarChar,20)
			};
            parameters[0].Value = bid;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);
            return ds.Tables[0];

        }


        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账'
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(string bid, string crtby)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();

                    //var param = new DynamicParameters();
                    //param.Add("@fcrimecode", 0);
                    //param.Add("@PType", 0);
                    //param.Add("@TypeFlag", 0);
                    //param.Add("@fmoney", 0);
                    //param.Add("@Crtby", 0);
                    //param.Add("@remark", 0);
                    //param.Add("@resultInt", 0, DbType.Int32, ParameterDirection.Output);
                    //param.Add("@resultText", 0,DbType.String, ParameterDirection.Output);
                    //var res2 = conn.Execute("P_AddInvoiceXE_List", param, null, true, null, CommandType.StoredProcedure);//res2.Count = 80


                    #region 增加SQL脚本
                    strSql.Append(@"update t_bonusDTL set Flag=1 where BID=@BID 
                    and fcrimecode in(select fcrimecode from t_Criminal_Card where cardflaga<>4);");
                    strSql.Append(@"update t_bonus set cnt=b.fcount,famount=b.fmoney,Flag=1,FPostBy=@CrtBy,FPostDate=getdate(),FPostFlag=1 from (
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
        /// 批量删除已经导入的取扣款记录
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool plDeleteByPKId(string pkId, string crtby,int typeflag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();


                    #region 增加SQL脚本
                    strSql.Append(@"update t_Criminal_Card set amounta=a.amounta-b.amounta,amountb=a.amountb-b.amountb,amountc=a.amountc-b.amountc from  t_Criminal_card a,(
                                select fcrimecode,sum(case acctype when 0 then(damount-camount) else 0 end) amounta,sum(case acctype when 1 then(damount-camount) else 0 end) amountb,sum(case acctype when 2 then(damount-camount) else 0 end) amountc from t_Vcrd 
                                where flag=0 and isnull(bankflag,0)<=0 and typeflag=@typeflag and origid=@pkId
                                group by fcrimecode) b
                                where a.fcrimecode=b.fcrimecode;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"update t_Vcrd set flag=1,delby=@crtby,deldate=getdate(),remark='已补批量删除:' +isnull(remark,'') 
                                    where flag in(0,-2) and isnull(bankflag,0)<=0 and typeflag=@typeflag and origid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade_dtl where bid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade where bid=@pkId;");
                    strSql.Append(@"delete from T_BatchMoneyTrade_ErrList where pc=@pkId;");
                    #endregion



                    object param = new { pkId = pkId, crtBy = crtby ,typeflag=typeflag};
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
        /// 批量Excel存取款数据导入，并写入数据库
        /// </summary>
        /// <param name="strFBid"></param>
        /// <param name="onlyCheckFlag">是否只是检测不导入Vcrd</param>
        /// <returns>执行的结果值</returns>
        public string PLExcelImport(string strFBid, string onlyCheckFlag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                var parems = new DynamicParameters();//建立一个parem对象
                parems.Add("@strFBid", strFBid);
                parems.Add("@onlyCheckFlag", onlyCheckFlag);
                parems.Add("@result", "", DbType.String, ParameterDirection.Output);//输出返回值
                //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                SqlMapper.Execute(conn, "P_PLExcelFileImport", parems, null, null, CommandType.StoredProcedure);
                string res = parems.Get<string>("@result");//获取数据库输出的值
                return res;
            }
        }


    }
}