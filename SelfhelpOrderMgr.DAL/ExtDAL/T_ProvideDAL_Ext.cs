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
    public partial class T_ProvideDAL
    {
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney(string pid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_Provide  set FManNb=b.fcount,famount=b.fmoney from 
                (select pid,isnull(count(*),0) fcount,isnull(sum(famount),0) fmoney from T_Providedtl where pid=@pid and FSex='男'
                group by pid) b
                where T_Provide.pid=b.pid;
                ");
            strSql.Append(@"update T_Provide  set FWomNb=b.fcount,famount=famount+b.fmoney from 
                (select pid,isnull(count(*),0) fcount,isnull(sum(famount),0) fmoney from T_Providedtl where pid=@pid and FSex='女'
                group by pid) b
                where T_Provide.pid=b.pid;
                ");

            SqlParameter[] parameters = {
					new SqlParameter("@pid", SqlDbType.VarChar,20)};
            parameters[0].Value = pid;

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
        /// 根据用户'Pid'确认提交主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCheckFlag(T_Provide model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Provide set ");
            strSql.Append("CheckBy=@CheckBy,");
            strSql.Append("CheckDt=@CheckDt,");
            strSql.Append("PFlag=@PFlag");
            strSql.Append(" where Pid=@Pid ");
            SqlParameter[] parameters = {
					new SqlParameter("@Pid", SqlDbType.VarChar,20),
					new SqlParameter("@CheckBy", SqlDbType.VarChar,20),
					new SqlParameter("@CheckDt", SqlDbType.DateTime),
					new SqlParameter("@PFlag", SqlDbType.Int,4)};
            parameters[0].Value = model.PId;
            parameters[1].Value = model.CheckBy;
            parameters[2].Value = model.CheckDt;
            parameters[3].Value = model.PFlag;

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
        /// 按Pid删除一批DTL数据
        /// </summary>
        public bool DeleteDtlByPid(string PId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_ProvideDTL ");
            strSql.Append(" where PId=@PId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)			};
            parameters[0].Value = PId;

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
        /// 获得所在dtl数据列表
        /// </summary>
        public DataTable GetDtlDataTableByPid(string PId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.FCRIMECODE 编号,a.fcriminal 姓名,a.FAMOUNT 金额,a.fareaName 队别,b.remark 备注");
            strSql.Append(" FROM T_ProvideDTL a,T_Provide b");
            strSql.Append(" where a.PId=@PId and a.PId=b.PId");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)};
            parameters[0].Value = PId;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable(string PId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select pc 主单流水号,fcrimecode 编号,fname 姓名,Amount 金额 ,Crtdt 导入日期,Remark 失败原因 ");
            strSql.Append(" FROM t_ImportList where pc=@PId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PId", SqlDbType.VarChar,20)
			};
            parameters[0].Value = PId;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);
            return ds.Tables[0];

        }


        /// <summary>
        /// 获得指定犯人当月零用金发放次数
        /// </summary>
        public int GetSendCountByPid(string fcrimecode, string pdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(count(*),0) fcount");
            strSql.Append(" FROM T_ProvideDTL a,T_Provide b ");
            strSql.Append(" where a.FCrimeCode=@FCrimeCode and b.PDate=@PDate and a.PId=b.PId");
            SqlParameter[] parameters = {
					new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20),
                    new SqlParameter("@PDate", SqlDbType.VarChar,20)};
            parameters[0].Value = fcrimecode;
            parameters[1].Value = pdate;
            DataTable dt = SqlHelper.Query(strSql.ToString(), parameters).Tables[0];
            return (int)dt.Rows[0][0];
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(T_Provide model, string partParameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Provide(");
            strSql.Append("PId,CrtBy,CrtDt,Flag,PType,PFlag,FAreaCode,FAreaName,FManNb,FWomNb,FManAmount,FWomAmount,PDate,FAmount,Remark,ApplyBy,PTag,PTagName");
            strSql.Append(") values (");
            strSql.Append("@PId,@CrtBy,@CrtDt,@Flag,@PType,@PFlag,@FAreaCode,@FAreaName,@FManNb,@FWomNb,@FManAmount,@FWomAmount,@PDate,@FAmount,@Remark,@ApplyBy,@PTag,@PTagName");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@PId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,                       
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,                       
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,                       
                        new SqlParameter("@FManNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FWomNb", SqlDbType.Int,4) ,            
                        new SqlParameter("@FManAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@FWomAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@PDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,250),            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,250),           
                        new SqlParameter("@PTag", SqlDbType.VarChar,250),            
                        new SqlParameter("@PTagName", SqlDbType.VarChar,250)              
              
            };

            parameters[0].Value = model.PId;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDt;
            parameters[3].Value = model.Flag;
            parameters[4].Value = model.PType;
            parameters[5].Value = model.PFlag;
            parameters[6].Value = model.FAreaCode;
            parameters[7].Value = model.FAreaName;
            parameters[8].Value = model.FManNb;
            parameters[9].Value = model.FWomNb;
            parameters[10].Value = model.FManAmount;
            parameters[11].Value = model.FWomAmount;
            parameters[12].Value = model.PDate;
            parameters[13].Value = model.FAmount;
            parameters[14].Value = model.Remark;
            parameters[15].Value = model.ApplyBy;
            parameters[16].Value = model.PTag;
            parameters[17].Value = model.PTagName;
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
        /// 获得零用金主单当月发放次数
        /// </summary>
        public int GetSendCountByArea(string areaName, string pdate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) fcount");
            strSql.Append(" FROM T_Provide ");
            strSql.Append(" where fareaName=@fareaName and PDate=@PDate");
            SqlParameter[] parameters = {
					new SqlParameter("@fareaName", SqlDbType.VarChar,20),
                    new SqlParameter("@PDate", SqlDbType.VarChar,20)};
            parameters[0].Value = areaName;
            parameters[1].Value = pdate;
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);
            return (int)ds.Tables[0].Rows[0][0];
        }


        public string CreateOrderId(string seqnoType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @vouno varchar(30);");
            strSql.Append("exec  CREATESEQNO  '" + seqnoType + "',1,@vouno output;");
            strSql.Append("select @vouno='" + seqnoType + "'+@vouno;");
            strSql.Append("select @vouno;");

            DataSet ds = SqlHelper.Query(strSql.ToString());
            return ds.Tables[0].Rows[0][0].ToString();
        }

        //批量按队别生成零用金明细
        public bool BatchCreateAreaList(string pid, string crtby)
        {
            //要求必须有银行卡户的 1是有开户才可以，0是都可以
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("LyjBankCardCheckFlag");
            int regFlag = 0;
            if(mset!=null)
            {
                regFlag = Convert.ToInt32(mset.MgrValue);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into T_ProvideDTL (PId,FCrimeCode,CardCode,FAmount,Flag,FAreaCode,FAreaName,FCriminal,VouNo,FrealareaCode,FrealareaName,FSex,AccType,CardType)
                    select a.pid,c.fcrimecode,c.cardcodea,d.value famount,0 flag,a.fareacode,a.fareaname,b.fname fcriminal,'' vouno,'' frealareacode,'' frealareaName,b.Fsex Fsex,0 acctype,0 cardtype
                     from T_Provide a,t_criminal b,t_criminal_card c ,t_settings d 
                    where a.fareacode=b.fareacode and b.fcode=c.fcrimecode and b.fsex=substring(d.name,1,1) and d.name like '%零花钱' 
                    and a.pid=@pid and isnull(b.fflag,0)=0 and c.cardflaga in(1,2) ");
            if (regFlag == 1)
            {
                strSql.Append(" and isnull(c.BankAccNo,'')<>'' and isnull(RegFlag,0)=1");
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@pid", SqlDbType.VarChar,20)};
            parameters[0].Value = pid;
            int rs = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            return rs > 0;
        }

        //批量全监生成零用金明细
        public bool BatchCreateAllList(string pid, string crtby)
        {
            //要求必须有银行卡户的 1是有开户才可以，0是都可以
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("LyjBankCardCheckFlag");
            int regFlag = 0;
            if (mset != null)
            {
                regFlag = Convert.ToInt32(mset.MgrValue);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into T_ProvideDTL (PId,FCrimeCode,CardCode,FAmount,Flag,FAreaCode,FAreaName,FCriminal,VouNo,FrealareaCode,FrealareaName,FSex,AccType,CardType) 
                    select @pid pid,c.fcrimecode,c.cardcodea,d.value famount,0 flag,a.fcode fareacode,a.fname fareaname,b.fname fcriminal,'' vouno,'' frealareacode,'' frealareaName,b.Fsex Fsex,0 acctype,0 cardtype
                     from T_Area a,t_criminal b,t_criminal_card c ,t_settings d 
                    where a.fcode=b.fareaCode and b.fcode=c.fcrimecode and b.fsex=substring(d.name,1,1) and d.name like '%零花钱' 
                     and isnull(b.fflag,0)=0  and c.cardflaga in(1,2)");
            if (regFlag == 1)
            {
                strSql.Append(" and isnull(c.BankAccNo,'')<>'' and isnull(c.RegFlag,0)=1");
            }

            SqlParameter[] parameters = {
                    new SqlParameter("@pid", SqlDbType.VarChar,20)};
            parameters[0].Value = pid;
            int rs = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            return rs > 0;
        }
        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(SqlHelper.getConnstr());
            connection.Open();
            return connection;
        }
        //财务过账
        public bool UpdateInDbData(string pid, string crtby, int intAcctype = 0)
        {
            int iflag = 0;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("DepositInVcrdFlag");
            if (mset != null)
            {
                if (mset.MgrValue == "-2")
                {
                    iflag = -2;
                }
            }

            StringBuilder strSql = new StringBuilder();
            //要一条条的导入
            /*首先创建一个事务
             * 接下来用循环来进行插入
             * 全部完成后，提交事务，否则回滚             
             */
            string invoiceno = "";
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                strSql = new StringBuilder();
                strSql.Append("select * from T_ProvideDTL where PId=@PId");
                object paramPrvDtl = new { PId = pid };
                List<T_ProvideDTL> prvDtls = (List<T_ProvideDTL>)conn.Query<T_ProvideDTL>(strSql.ToString(), paramPrvDtl);
                

                T_Provide provide = new T_ProvideDAL().GetModel(pid);
                string pidRemark = provide.Remark;

                IDbTransaction myTran = conn.BeginTransaction();
                try 
                {
                    foreach (T_ProvideDTL dtl in prvDtls)
                    {
                        invoiceno = "";
                        strSql = new StringBuilder();
                        strSql.Append("declare @vouno varchar(30);");
                        strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                        strSql.Append("select @vouno='VOU'+@vouno;");
                        strSql.Append("select @vouno;");
                        DataSet ds = SqlHelper.Query(strSql.ToString());
                        invoiceno = ds.Tables[0].Rows[0][0].ToString();

                        //T_Criminal criminal = new T_CriminalDAL().GetModel(dtl.FCrimeCode);
                        #region 循环写入数据                        
                        //插入数据
                        strSql = new StringBuilder();
                        //strSql.Append("declare @VOUNO varchar(30);");
                        //strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                        //strSql.Append("select @VOUNO='VOU'+@VOUNO;");
                        strSql.Append("insert into T_Vcrd(");
                        strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount");
                        strSql.Append(") values (");
                        strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@Bankflag,@checkflag,@checkby,@pc,@curUserAmount,@curAllAmount");
                        strSql.Append(");");
                        strSql.Append("update t_ProvideDTL set Flag=1,Vouno=@Vouno where PId=@PId and Seqno=@Seqno;");

                        object paramVcrd;
                        //设定账户额
                        paramVcrd = new { VOUNO = invoiceno, cardcode = dtl.CardCode, fcrimecode = dtl.FCrimeCode, DAMOUNT = dtl.FAmount, CAMOUNT = 0, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = "零花钱发放", DEPOSITER = "", REMARK = pidRemark, flag = iflag, fareacode = dtl.FAreaCode, fareaName = dtl.FAreaName, fcriminal = dtl.FCriminal, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = pid, cardtype = 0, TypeFlag = 3, acctype = intAcctype, Bankflag = 0, checkflag = 0, checkby = crtby, pc = 0, curUserAmount = 0, curAllAmount = 0, PId = dtl.PId, Seqno = dtl.seqno };
                        int x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                        #endregion

                        //判断执行结果情况
                        if (x <= 0)
                        {
                            myTran.Rollback();
                            return false;
                        }
                    }
                    #region 提交更新零花钱记录标志
                    strSql = new StringBuilder();
                    strSql.Append("update t_Provide set Flag=1,CheckBy=@CheckBy,CheckDt=@CheckDt ");
                    strSql.Append("where PId=@PId;");
                    //if (intAcctype == 1)
                    //{
                    //    strSql.Append("update t_criminal_card set amountb=amountb+b.damount from ");                        
                    //}
                    //else
                    //{
                    //    strSql.Append("update t_criminal_card set amounta=amounta+b.damount from ");
                    //}
                    //strSql.Append("(select fcrimecode,damount from t_vcrd where Origid=@InvoiceNo and flag=0) b ");
                    //strSql.Append("where t_criminal_card.fcrimecode=b.fcrimecode;");

                    //分析可能出现更新账户余额变成负数的原因
                    if (intAcctype == 1)
                    {
                        strSql.Append("update t_criminal_card set amountb=amountb+isnull(b.damount,0) from t_criminal_card a");
                    }
                    else
                    {
                        strSql.Append("update t_criminal_card set amounta=amounta+isnull(b.damount,0) from t_criminal_card a");
                    }
                    strSql.Append(",(select fcrimecode,damount from t_vcrd where Origid=@InvoiceNo and flag=0) b where a.fcrimecode=b.fcrimecode;");

                    object paramProvide;
                    //设定账户额
                    paramProvide = new { CheckBy = crtby, CheckDt = DateTime.Today, InvoiceNo = pid, PId = pid };
                    int j = conn.Execute(strSql.ToString(), paramProvide, myTran);
                    #endregion

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


        public List<T_ProvideDTL> GetDtlPageList(int page, int pageRow, string strWhere, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY seqno) AS RowNumber,* from T_ProvideDTL");
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

                return (List<T_ProvideDTL>)conn.Query<T_ProvideDTL>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

        public bool PLWriteProvideDtl(string bid, string crtby)
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
                    if (mset.MgrValue == "1")
                    {
                        //该用户银行卡不存在
                        strSql.Append(@"update t_bonus_Temp set Notes ='该用户银行卡不存在' where FCrimeCode not in(
                        select distinct FCrimeCode from t_Criminal_Card where isnull(regFlag,0)=1 and isnull(BankAccNo,'')<>'' ) and Notes='' and t_bonus_Temp.Bid=@Bid;");
                    }              
                    //更新 该用户本月已经发放了
                    strSql.Append(@"update t_bonus_Temp set Notes='该用户本月已经发放了'+Convert(varchar(20), b.FCount)+'次，不能再发放'  from ( 
                        select FCrimeCode,Count(*) FCount from t_ProvideDtl,t_Provide where t_ProvideDtl.Pdate=t_Provide.Pdate and t_Provide.Pid=@Bid
                        group by FCrimeCode
                        having Count(*)>@FCount) b
                        where t_bonus_Temp.FCrimeCode=B.FCrimeCode and t_bonus_Temp.Notes='' and t_bonus_Temp.Bid=@Bid;");

                    //写入到失败记录表
                    strSql.Append(@"insert into T_ImportList 
                        (ImportType,FCrimeCode,FName,Amount,Crtdt,CrtBy,Remark,Pc,Notes)
                        select 3 ImportType,FCrimeCode,FCriminal FName,FMoney Amount,GetDate() Crtdt,@CrtBy CrtBy,FRemark+':' +Notes Remark,@Bid Pc,Notes Notes
                        from t_Bonus_Temp where Bid=@Bid and Notes<>'';");
                    //写入成功记录
                    strSql.Append(@"insert into  t_ProvideDtl
                         (PId,FCrimeCode,CardCode,FAmount,Flag,FAreaCode,FAreaName,FCriminal,VouNo
                            ,FrealareaCode,FrealareaName,FSex,AccType,CardType,PDate)
                        select 
                        @Bid PId,a.FCrimeCode FCrimeCode,b.CardCodea CardCode,a.FMoney FAmount,0 Flag,c.FAreaCode FAreaCode,d.FName FAreaName,a.FCriminal FCriminal,'' VouNo
                        ,'' FrealareaCode,'' FrealareaName,c.FSex FSex,0 AccType,0 CardType,e.Pdate PDate
                        from t_bonus_Temp a,t_Criminal_Card b,t_Criminal c,t_Area d,t_Provide e,t_Cy_Type f
                        where a.Bid=@Bid and a.Notes='' 
                        and a.FCrimeCode=C.FCode and B.FCrimeCode=C.FCode and C.FAreaCode=D.FCode
                        and a.Bid=e.Pid and f.FCode=C.FCyCode;");
                    //更新主单金额 男性标准
                    strSql.Append(@"update t_Provide set FManNb=b.FCount,FManAmount=10,FAmount=B.FAmount from (
            select PId,sum(FAmount) FAmount,count(*) FCount from t_Providedtl where PId=@Bid
            group by PId ) b where t_Provide.PId=@Bid and isnull(Flag,0)=0;");
                    //写入到失败记录表
                    strSql.Append(@"insert into T_ImportList 
                        (ImportType,FCrimeCode,FName,Amount,Crtdt,CrtBy,Remark,Pc,Notes)
                        select 3 ImportType,FCrimeCode,FCriminal FName,FMoney Amount,GetDate() Crtdt,@CrtBy CrtBy,FRemark+':' +Notes Remark,@Bid Pc,Notes Notes
                        from t_Bonus_Temp where Bid=@Bid and Notes=''and fcrimecode not in(select fcrimecode from t_Providedtl where pid=@Bid);");

                    //删除t_bonus_Temp过渡表记录
                    strSql.Append("delete from t_bonus_Temp where Bid=@Bid");
                    object param = new { Bid = bid, CrtBy = crtby, FCount = sets.VALUE };
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


        public decimal[] GetDtlListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(count(*),0) FCount,isnull(sum(Famount),0) FMoney from T_ProvideDtl");
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
        /// 按主单号零用金批量退账，从Vcrd中删除
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool plDelLingYongJinByPId(string PId, string crtby)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    StringBuilder strSql = new StringBuilder();


                    #region 增加SQL脚本
                    //第一行增加了ISNULL函数
                    strSql.Append(@"update t_Criminal_Card set amounta=a.amounta-isnull(b.amounta,0),amountb=a.amountb-isnull(b.amountb,0),amountc=a.amountc-isnull(b.amountc,0) from  t_Criminal_card a,(
                                select fcrimecode,sum(case acctype when 0 then(damount-camount) else 0 end) amounta,sum(case acctype when 1 then(damount-camount) else 0 end) amountb,sum(case acctype when 2 then(damount-camount) else 0 end) amountc from t_Vcrd 
                                where flag=0 and isnull(bankflag,0)<=0 and typeflag=3 and origid=@PId
                                group by fcrimecode) b
                                where a.fcrimecode=b.fcrimecode;");
                    //strSql.Append("update t_bonusdtl set Remark='该记录财务入账时，已离监销户了' where flag=0 and bid=@BID;");
                    strSql.Append(@"update t_Vcrd set flag=1,delby=@crtby,deldate=getdate(),remark='已被财务退账，批量删除:' +isnull(remark,'') 
                                    where flag in(0,-2) and isnull(bankflag,0)<=0 and typeflag=3 and origid=@PId;");
                    strSql.Append(@"update  T_Providedtl set flag=0 where pid=@PId;");
                    strSql.Append(@"update T_Provide set flag=0,pflag=0 where pid=@PId;");
                    #endregion
                    


                    object param = new { PId = PId, crtBy = crtby };
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

    }
}