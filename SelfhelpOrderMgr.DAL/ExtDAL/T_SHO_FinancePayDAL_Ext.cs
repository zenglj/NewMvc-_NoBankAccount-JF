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
    public partial class T_SHO_FinancePayDAL
    {
        //增加Pay主单并更新Vcrd记录
        public T_SHO_FinancePay AddPayOrder(T_SHO_FinancePay paymodel, string strWhere)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    string strSql = @"INSERT INTO T_SHO_FinancePay
                   (FType,FTitle,FCount,FMoney,CrtBy,CrtDt,PosName,BankCard,Flag,PayBy,PayDate,Remark)
                    VALUES
                   (@FType,@FTitle,@FCount,@FMoney,@CrtBy,@CrtDt,@PosName,@BankCard,@Flag,@PayBy,@PayDate,@Remark);
                    select @@IDENTITY as id";

                    object paramPay;
                    //设定账户额
                    paramPay = new { FType = paymodel.FType, FTitle = paymodel.FTitle, FCount = paymodel.FCount, FMoney = paymodel.FMoney, CrtBy = paymodel.CrtBy, CrtDt = paymodel.CrtDt, PosName = paymodel.PosName, BankCard = paymodel.BankCard, Flag = paymodel.Flag, PayBy = paymodel.PayBy, PayDate = paymodel.PayDate, Remark = paymodel.Remark };
                    List<payId> payid = conn.Query<payId>(strSql, paramPay, myTran).AsList<payId>();
                    int i = payid[0].id;

                    strSql = "update T_Vcrd set FinancePayFlag=1,FinancePayId=@FinancePayId where " + strWhere;
                    paramPay = new { FinancePayId = i };
                    int j = conn.Execute(strSql, paramPay, myTran);
                    if (j > 0)
                    {
                        strSql = "update T_SHO_FinancePay set FCount=@FCount where Id=@Id";
                        paramPay = new { FCount = j, Id = i };
                        j = conn.Execute(strSql, paramPay, myTran);
                        if (j > 0)
                        {
                            myTran.Commit();
                            return new T_SHO_FinancePayDAL().GetModel(i);
                        }
                    }
                    myTran.Rollback();
                }
                catch
                {
                    myTran.Rollback();
                }
            }
            return null;
        }

        //======================================================
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
            strSql.Append(" where PId=@PId and a.PId=b.PId");
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
            strSql.Append(" where a.FCrimeCode=@FCrimeCode and b.PDate=@PDate a.PId=b.PId");
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

        //批量生成零用金明细
        public bool BatchCreateAreaList(string pid, string crtby)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"insert into T_ProvideDTL (PId,FCrimeCode,CardCode,FAmount,Flag,FAreaCode,FAreaName,FCriminal,VouNo,FrealareaCode,FrealareaName,FSex,AccType,CardType)
                    select a.pid,c.fcrimecode,c.cardcodea,d.value famount,0 flag,a.fareacode,a.fareaname,b.fname fcriminal,'' vouno,'' frealareacode,'' frealareaName,b.Fsex Fsex,0 acctype,0 cardtype
                     from T_Provide a,t_criminal b,t_criminal_card c ,t_settings d 
                    where a.fareacode=b.fareacode and b.fcode=c.fcrimecode and b.fsex=substring(d.name,1,1) and d.name like '%用户零花钱' 
                    and a.pid=@pid and isnull(b.fflag,0)=0");

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
        public bool UpdateInDbData(string pid, string crtby)
        {
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
                T_Criminal criminal = new T_CriminalDAL().GetModel(prvDtls[0].FCrimeCode);

                invoiceno = "";
                strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                strSql.Append("select @vouno='VOU'+@vouno;");
                strSql.Append("select @vouno;");
                List<string> dd = (List<string>)conn.Query<string>(strSql.ToString());
                invoiceno = dd[0].ToString();

                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    foreach (T_ProvideDTL dtl in prvDtls)
                    {
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
                        object paramVcrd;
                        //设定账户额
                        paramVcrd = new { VOUNO = invoiceno, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = dtl.FAmount, CAMOUNT = 0, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = "零花钱发放", DEPOSITER = "", REMARK = "", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 3, acctype = 0, Bankflag = 0, checkflag = -1, checkby = crtby, pc = 0, curUserAmount = 0, curAllAmount = 0 };
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
                    strSql.Append("update t_ProvideDTL set Flag=1,Vouno=@Vouno where PId=@PId;");
                    strSql.Append("update t_criminal_card set amounta=amounta+b.damount from ");
                    strSql.Append("(select fcrimecode,damount from t_vcrd where vouno=@Vouno and flag=0) b ");
                    strSql.Append("where t_criminal_card.fcrimecode=b.fcrimecode;");
                    object paramProvide;
                    //设定账户额
                    paramProvide = new { CheckBy = crtby, CheckDt = DateTime.Today, Vouno = invoiceno, PId = pid };
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
    }
    public class payId
    {
        public int id { get; set; }
    }
}