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
    public partial class T_VcrdDAL
    {
        public List<T_Vcrd> GetPageList(int page, int pageRow, string strWhere,string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append("select ROW_NUMBER() OVER (ORDER BY seqno) AS RowNumber,* from T_Vcrd");
                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                if (string.IsNullOrEmpty(orderByField)==false)
                {
                    strSql.Append(" Order by "+orderByField);
                }
                
                return (List<T_Vcrd>)conn.Query<T_Vcrd>(strSql.ToString(), new { startNumber = startNumber, endNumber = endNumber });
            }
        }

        public decimal[] GetListCount(string strWhere)
        {
                StringBuilder strSql = new StringBuilder();

                strSql.Append(@"select isnull(count(*),0) FCount,isnull(sum(Camount),0) FMoney,isnull(sum(Damount),0) FDMoney from T_Vcrd");
                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                else
                {
                    strSql.Append(" where  Flag in(0,-2) and CAmount<>0 ");
                }
            decimal[] rs={0,0,0};
            decimal fcount = Convert.ToDecimal( SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][0]);
            decimal fmoney = Convert.ToDecimal( SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][1]);
            decimal fDmoney = Convert.ToDecimal(SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][2]);
            rs[0] = fcount;
            rs[1] = fmoney;
            rs[2] = fDmoney;
                return rs;
        }

        //获取财务存款科目类型
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iType">存取类型</param>
        /// <returns></returns>
        public List<T_CommonTypeTab> GetFinaType(int iType)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                List<T_CommonTypeTab> saveTypes = new List<T_CommonTypeTab>();
                List<T_CommonTypeTab> mainType;
                //List<T_CommonTypeTab> subType;
                int i = 0;
                if (iType == 0)
                {
                    strSql.Append(@"select ID,FType,FCode,FName,FRemark from T_CommonTypeTab where FTYpe='CWKM' and FRemark='支';");
                    mainType= (List<T_CommonTypeTab>)conn.Query<T_CommonTypeTab>(strSql.ToString());
                    //strSql = new StringBuilder();
                    //strSql.Append(@"select 0 ID ,'' FType,FCode,FName,'' FRemark from T_SaveType where TypeFlag=1;");
                    //subType= (List<T_CommonTypeTab>)conn.Query<T_CommonTypeTab>(strSql.ToString());              
                }
                else
                {
                    strSql.Append(@"select ID,FType,FCode,FName,FRemark from T_CommonTypeTab where FTYpe='CWKM' and FRemark='收';");
                    mainType = (List<T_CommonTypeTab>)conn.Query<T_CommonTypeTab>(strSql.ToString());
                    //strSql = new StringBuilder();
                    //strSql.Append(@"select 0 ID ,'' FType,FCode,FName,'' FRemark from T_SaveType where TypeFlag=0;");
                    //subType = (List<T_CommonTypeTab>)conn.Query<T_CommonTypeTab>(strSql.ToString());
                }
                foreach (T_CommonTypeTab savetype in mainType)
                {
                    savetype.FCode = i.ToString();
                    saveTypes.Add(savetype);
                    i++;
                }
                //foreach (T_CommonTypeTab savetype in subType)
                //{
                //    savetype.FCode = i.ToString();
                //    saveTypes.Add(savetype);
                //    i++;
                //}
                return saveTypes;
            }            
        }


        /// <summary>
        /// 用户存扣款
        /// </summary>
        /// <param name="criminal">用户信息</param>
        /// <param name="flag">1是存款，-1是扣款</param>
        /// <param name="fmoney">存扣款金额</param>
        /// <param name="savetype">存取款的类型</param>
        /// <param name="crtby">操作员</param>
        /// <param name="remark">备注</param>
        /// <param name="apply">申请人</param>
        /// <returns></returns>
        public List<T_Vcrd> UserCunKouKuan(T_Criminal criminal, int flag, decimal fmoney, T_Savetype savetype, string crtby, string remark, string apply, string pkId, int checkFlag=0)
        {
            int ivcrdflag = 0;
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetDAL().GetModel("DepositInVcrdFlag");
            if (mset != null)
            {
                if (mset.MgrValue == "-2")
                {
                    ivcrdflag = -2;
                }
            }
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                string invoiceno = pkId;
                StringBuilder strSql = new StringBuilder();
                StringBuilder strInvo = new StringBuilder();
                IDbTransaction myTran = conn.BeginTransaction();

                string seqnos = "";
                //插入VCrd记录及更新余额
                strSql = new StringBuilder();
                T_Criminal_card card = new T_Criminal_cardDAL().GetModel(criminal.FCode);
                    
                    strSql.Append("declare @VOUNO varchar(30);");
                    strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                    strSql.Append("select @VOUNO='VOU'+@VOUNO;");
                    strSql.Append("insert into T_Vcrd(");
                    strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount,SubTypeFlag");
                    strSql.Append(") values (");
                    strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@Bankflag,@checkflag,@checkby,@pc,@curUserAmount,@curAllAmount,@SubTypeFlag");
                    strSql.Append(");select @@IDENTITY");


                    strInvo.Append("declare @INVNO varchar(30);");
                    strInvo.Append("exec  CREATESEQNO  'INV',1,@INVNO output;");
                    strInvo.Append("select @INVNO='INVPK'+@INVNO;");
                    strInvo.Append("insert into T_Invoice(");
                    strInvo.Append(@"INVOICENO,cardcode,fcrimecode,amount,OrderDate,PayDATE
                                    ,PTYPE,Flag,REMARK,servamount,crtby,crtdate
                                    ,fsn,fareacode,fareaName,fcriminal,Frealareacode
                                    ,FrealAreaName,TYPEFLAG,CardType,AmountA,AmountB
                                    ,fifoflag,FreeAmountA,FreeAmountB,checkflag
                                    ,RoomNo,OrderId");
                    strInvo.Append(") values (");
                    strInvo.Append(@"@INVNO,@cardcode,@fcrimecode,@amount,@OrderDate,@PayDATE
                                    ,@PTYPE,@Flag,@REMARK,@servamount,@crtby,@crtdate
                                    ,@fsn,@fareacode,@fareaName,@fcriminal,@Frealareacode
                                    ,@FrealAreaName,@TYPEFLAG,@CardType,@AmountA,@AmountB
                                    ,@fifoflag,@FreeAmountA,@FreeAmountB,@checkflag
                                    ,@RoomNo,@OrderId");
                    strInvo.Append(");select @INVNO");

                    object paramVcrd;
                    int seq = 0;
                    List<int> seqs;
                T_Criminal_card _uCard = new T_Criminal_cardDAL().GetModel(criminal.FCode);
                if (savetype.AccType == 1)
                {

                }

                if (flag==1)
                    {
                        //存款都在A账户
                        //2020修改为根据savetype的 acctype 值
                        if (savetype.AccType == null)
                        {
                            savetype.AccType = 0;
                        }
                        
                    paramVcrd = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = fmoney, CAMOUNT = 0, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = ivcrdflag, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 0, acctype = savetype.AccType, Bankflag = 0, checkflag = 0, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                        seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrd, myTran);
                        seq = Convert.ToInt32(seqs[0]);
                        seqnos = seq.ToString();
                    }
                    else
                    {
                        decimal fmoneyA = 0;
                        decimal fmoneyB = 0;
                        decimal fmoneyC = 0;

                            //A账户扣款
                            //fmoneyA = card.AmountA;
                            /*
                             *2018-06-21修正  曾林进
                             * 如果fmoneyA金额为0，则不写入记录
                             */

                            switch (savetype.AccType)
                            {
                                case 1://先B，再A，最后C
                                    {
                                        if (card.AmountB + card.AmountA < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            fmoneyB = card.AmountB;
                                            fmoneyA = card.AmountA ;
                                            fmoneyC = (fmoney - card.AmountA - card.AmountB) ;

                                        }
                                        else if (card.AmountB < fmoney)
                                        {//分别从三个账户扣款
                                            fmoneyB = card.AmountB ;
                                            fmoneyA = (fmoney - card.AmountB) ; 
                                            fmoneyC = 0;
                                        }
                                        else
                                        {                                            
                                            fmoneyA = 0;
                                            fmoneyB = fmoney;
                                            fmoneyC = 0;
                                        }
                                    } break;
                                case 2://先C，再A，最后B
                                    {
                                        if (card.AmountC + card.AmountA < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            fmoneyC = card.AmountC ;
                                            fmoneyA = card.AmountA ;
                                            fmoneyB = (fmoney - card.AmountC - card.AmountA) ;
                                        }
                                        else if (card.AmountC < fmoney)
                                        {//分别从三个账户扣款
                                            fmoneyC = card.AmountC ; 
                                            fmoneyA = (fmoney - card.AmountC) ; 
                                            fmoneyB = 0;
                                        }
                                        else
                                        {
                                            fmoneyA = 0;
                                            fmoneyB = 0;                                            
                                            fmoneyC = fmoney;
                                        }
                                    } break;
                                default://先A，再B，最后C
                                    {
                                        if (card.AmountA + card.AmountB < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            fmoneyA = card.AmountA ;
                                            fmoneyB = card.AmountB ;
                                            fmoneyC = (fmoney - card.AmountA - card.AmountB) ;
                                        }
                                        else if (card.AmountA < fmoney)
                                        {//分别从三个账户扣款
                                            fmoneyA = card.AmountA ; 
                                            fmoneyB = (fmoney - card.AmountA) ;
                                            fmoneyC = 0;
                                        }
                                        else
                                        {
                                            fmoneyA = fmoney;
                                            fmoneyB = 0;                                            
                                            fmoneyC = 0;
                                        }
                                    } break;
                            }

                            //if (fmoneyA != 0)
                            //{
                            //    //checkFlag是根据系统参数设定：YibanQukouKuanShenhe_Flag(一般取扣款是否需要审核)的MgrValue值来控制
                            //    object paramVcrdA = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = card.AmountA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 2, acctype = 0, Bankflag = 0, checkflag = checkFlag, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                            //    seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrdA, myTran);
                            //    seq = Convert.ToInt32(seqs[0]);
                            //    seqnos = seq.ToString();
                            //    //写入明细记录
                            //    WriteInvoice(criminal, fmoneyA, savetype, crtby, remark, conn, strInvo, myTran, seqnos, fmoneyA, 0, seq);

                            //}

                            
                            //B账户扣款
                            //fmoneyB = fmoney - card.AmountA;
                            ////checkFlag是根据系统参数设定：YibanQukouKuanShenhe_Flag(一般取扣款是否需要审核)的MgrValue值来控制
                            //object paramVcrdB = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = fmoney-card.AmountA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 2, acctype = 1, Bankflag = 0, checkflag = checkFlag, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                            //seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrdB, myTran);
                            //seq = Convert.ToInt32(seqs[0]);
                            //if (seqnos == "")
                            //{
                            //    seqnos = seq.ToString();
                            //}else
                            //{
                            //    seqnos = seqnos + "," + seq.ToString();
                            //}

                            //========================================================================
                            /*===========================
                             2020-01-09 zeng 改为从多(3)个账户扣款
                             */
                            //=========================================================================
                            if (fmoneyA > 0)
                            {
                                //checkFlag是根据系统参数设定：YibanQukouKuanShenhe_Flag(一般取扣款是否需要审核)的MgrValue值来控制
                                object paramVcrdA = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = fmoneyA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 2, acctype = 0, Bankflag = 0, checkflag = checkFlag, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                                seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrdA, myTran);
                                seq = Convert.ToInt32(seqs[0]);
                                seqnos = seq.ToString();
                                //写入明细记录
                                WriteInvoice(criminal, fmoneyA, savetype, crtby, remark, conn, strInvo, myTran, seqnos, fmoneyA, 0, seq);
                            }
                            if (fmoneyB > 0)
                            {
                                //checkFlag是根据系统参数设定：YibanQukouKuanShenhe_Flag(一般取扣款是否需要审核)的MgrValue值来控制
                                object paramVcrdB = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = fmoneyB, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 2, acctype = 1, Bankflag = 0, checkflag = checkFlag, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                                seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrdB, myTran);
                                seq = Convert.ToInt32(seqs[0]);
                                if (seqnos == "")
                                {
                                    seqnos = seq.ToString();
                                }
                                else
                                {
                                    seqnos = seqnos + "," + seq.ToString();
                                }
                            }
                            
                            if (fmoneyC > 0)
                            {
                                object paramVcrdC = new { cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = fmoneyC, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = savetype.fname, DEPOSITER = apply, REMARK = remark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = 2, acctype = 2, Bankflag = 0, checkflag = checkFlag, checkby = crtby, pc = 0, curUserAmount = criminal.OkUseAllMoney, curAllAmount = criminal.AmountAmoney + criminal.AmountBmoney + criminal.AmountCmoney, SubTypeFlag = savetype.fcode };
                                seqs = (List<int>)conn.Query<int>(strSql.ToString(), paramVcrdC, myTran);
                                seq = Convert.ToInt32(seqs[0]);
                                if (seqnos == "")
                                {
                                    seqnos = seq.ToString();
                                }
                                else
                                {
                                    seqnos = seqnos + "," + seq.ToString();
                                }
                            }
                            
                            
                            //写入明细记录
                            //2020-01-09 消费记录，如果C账户大于0 则消费账户B的金额为B+C之和。
                            WriteInvoice(criminal, fmoney, savetype, crtby, remark, conn, strInvo, myTran, seqnos, fmoneyA, fmoneyB + fmoneyC, seq);
                    }
                    if(seq>0)
                    {
                        //更新账户T_Criminal_Card金额
                        strSql = new StringBuilder();
                        strSql.Append("update t_criminal_card set amounta=amounta+@fmoneyA,amountb=amountb+@fmoneyB,amountC=amountC+@fmoneyC where fcrimecode=@fcrimecode;");
                        //2020修改为根据savetype的 acctype 值
                        if (savetype.AccType == null)
                        {
                            savetype.AccType = 0;
                        }

                        if (ivcrdflag == -2 && flag==1)//如果需要审核后才可以入账的话，变动金额就全部置为0
                        {
                            paramVcrd = new { fmoneyA = 0, fmoneyB = 0, fmoneyC = 0, fcrimecode = criminal.FCode };
                            
                        }
                        else
                        {
                            switch (savetype.AccType)
                            {
                                case 1:
                                    {
                                        paramVcrd = new { fmoneyA = 0, fmoneyB = fmoney * flag, fmoneyC = 0, fcrimecode = criminal.FCode };
                                    }
                                    break;
                                case 2:
                                    {
                                        paramVcrd = new { fmoneyA = 0, fmoneyB = 0, fmoneyC = fmoney * flag, fcrimecode = criminal.FCode };
                                    }
                                    break;
                                default:
                                    {
                                        paramVcrd = new { fmoneyA = fmoney * flag, fmoneyB = 0, fmoneyC = 0, fcrimecode = criminal.FCode };
                                    }
                                    break;
                            }
                        }
                        
                        

                        if(flag==-1)
                        {
                            switch (savetype.AccType)
                            {
                                case 1://先B，再A，最后C
                                    {
                                        if (card.AmountB + card.AmountA < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyB = card.AmountB * flag, fmoneyA = card.AmountA * flag, fmoneyC = (fmoney - card.AmountA - card.AmountB) * flag, fcrimecode = criminal.FCode };
                                        }
                                        else if (card.AmountB < fmoney)
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyB = card.AmountB * flag, fmoneyA = (fmoney - card.AmountB) * flag, fmoneyC = 0, fcrimecode = criminal.FCode };
                                        }
                                    }break;
                                case 2://先C，再A，最后B
                                    {
                                        if (card.AmountC + card.AmountA < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyC = card.AmountC * flag, fmoneyA = card.AmountA * flag, fmoneyB = (fmoney - card.AmountC - card.AmountA) * flag, fcrimecode = criminal.FCode };
                                        }
                                        else if (card.AmountC < fmoney)
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyC = card.AmountC * flag, fmoneyA = (fmoney - card.AmountC) * flag, fmoneyB = 0, fcrimecode = criminal.FCode };
                                        }

                                    }break;
                                default://先A，再A，最后C
                                    {
                                        if (card.AmountA + card.AmountB < fmoney)//A+B钱不够扣
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyA = card.AmountA * flag, fmoneyB = card.AmountB * flag, fmoneyC = (fmoney - card.AmountA - card.AmountB) * flag, fcrimecode = criminal.FCode };
                                        }
                                        else if (card.AmountA < fmoney)
                                        {//分别从三个账户扣款
                                            paramVcrd = new { fmoneyA = card.AmountA * flag, fmoneyB = (fmoney - card.AmountA) * flag, fmoneyC = 0, fcrimecode = criminal.FCode };
                                        }
                                    }break;
                            }
                            
                            
                        }
                        
                        int x = conn.Execute(strSql.ToString(), paramVcrd, myTran);

                        if (x > 0)
                        {
                            myTran.Commit();
                            strSql = new StringBuilder();
                            strSql.Append("select * from t_vcrd where seqno in(" + seqnos + ");");
                            //paramVcrd = new { seqnos = seqnos };
                            List<T_Vcrd> vards = (List<T_Vcrd>)conn.Query<T_Vcrd>(strSql.ToString(), myTran);

                            return vards;
                        }
                        myTran.Rollback();
                    }
                    
                }
                
                return null;
            
        }

        private static void WriteInvoice(T_Criminal criminal, decimal fmoney, T_Savetype savetype, string crtby, string remark, IDbConnection conn, StringBuilder strInvo, IDbTransaction myTran, string seqnos, decimal fmoneyA, decimal fmoneyB,int seq)
        {
            //如果批量限额标志为1（真）,则判断可消费余额是否有够
            if (savetype.PLXE_Flag == 1)
            {
                object paramInvoice = new
                {
                    cardcode = criminal.CardCode,
                    fcrimecode = criminal.FCode,
                    amount = fmoney,
                    OrderDate = DateTime.Now.ToString(),
                    PayDATE = DateTime.Now.ToString(),
                    PTYPE = savetype.fname,
                    Flag = 1,
                    REMARK = remark + "(批扣)",
                    servamount = criminal.OkUseAllMoney,
                    crtby = crtby,
                    crtdate = DateTime.Now.ToString(),
                    fsn = "",
                    fareacode = criminal.FAreaCode,
                    fareaName = criminal.FAreaName,
                    fcriminal = criminal.FName,
                    Frealareacode = "",
                    FrealAreaName = "",
                    TYPEFLAG = 2,
                    CardType = 0,
                    AmountA = fmoneyA,
                    AmountB = fmoneyB,
                    fifoflag = -1,
                    FreeAmountA = 0,
                    FreeAmountB = 0,
                    checkflag = 0,
                    RoomNo = "",
                    OrderId = 0
                };
                List<string> rstInvoiceNos = (List<string>)conn.Query<string>(strInvo.ToString(), paramInvoice, myTran);
                //取得InvoiceNo
                string rstInvoiceNo = Convert.ToString(rstInvoiceNos[0]);
                //更新Origid为InvoiceNo
                object paramSetVcrd = new { Origid = rstInvoiceNo, seqno =seq};
                conn.Execute("update t_Vcrd set Origid=@Origid where seqno =@seqno", paramSetVcrd, myTran);
            }
        }

        public bool UpdateCheckFlag(string OutFsn)//更新消费单的配货状态
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update t_vcrd set CheckFlag=0 where OrigId in(
                select InvoiceNo from t_invoice_outdtl where fsn=@fsn
                ) and CheckFlag=-1 and Flag=0");
            SqlParameter[] parameters = {
					new SqlParameter("@fsn", SqlDbType.VarChar,20)};
            parameters[0].Value = OutFsn;
            int i = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if(i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        public bool SoftDeleteVcrd(string fcode, int seqno,string delUserName)//软删除存/取款记录
        {
            StringBuilder strSql = new StringBuilder();
            T_Vcrd vcrd = new T_VcrdDAL().GetModel(seqno);
            if(vcrd.BankFlag!=0)//银行标志状态不正确
            {
                return false;
            }
            if(!(vcrd.Flag==0 || vcrd.Flag==-2))//记录标志状态不正确
            {
                return false;
            }
            //如果是存款记录，金额是正的，如果是扣款记，金额就是负的
            decimal ChangeMoney = vcrd.DAmount - vcrd.CAmount;

            strSql.Append("update t_Invoice set flag=0,Remark=Remark+',该记录被删除了',FrealAreaName='删除人员:'+@DelBy where flag=1 and FCrimeCode=@FCrimeCode and TypeFlag=2 ");
            strSql.Append(" and invoiceno=(select origid from t_vcrd where flag in(0,-2) and seqno=@seqno);");
            strSql.Append("update t_vcrd set flag=1,Remark=Remark+',该记录被删除了',DelBy=@DelBy,DelDate=getdate() where flag in(0,-2) and FCrimeCode=@FCrimeCode and seqno=@seqno;");
            if (vcrd.Flag == 0)
            {
                if (vcrd.AccType == 0 && vcrd.Flag==0)
                {
                    strSql.Append("update t_Criminal_card set AMountA=AMountA-@ChangeMoney where FCrimeCode=@FCrimeCode;");
                }
                else if (vcrd.AccType == 1 && vcrd.Flag == 0)
                {
                    strSql.Append("update t_Criminal_card set AMountB=AMountB-@ChangeMoney where FCrimeCode=@FCrimeCode;");
                }
                else if (vcrd.AccType == 2 && vcrd.Flag == 0)
                {
                    strSql.Append("update t_Criminal_card set AMountC=AMountC-@ChangeMoney where FCrimeCode=@FCrimeCode;");
                }
                else
                {
                    return false;
                }
            }
            

            SqlParameter[] parameters = {
					new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20),
                    new SqlParameter("@seqno", SqlDbType.Int,4),
                    new SqlParameter("@ChangeMoney", SqlDbType.Decimal,9),
                    new SqlParameter("@DelBy", SqlDbType.VarChar,20)};
            parameters[0].Value = fcode;
            parameters[1].Value = seqno;
            parameters[2].Value = ChangeMoney;
            parameters[3].Value = delUserName;
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

        /// <summary>
        /// 自定义查询直接传入SQL
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<T_Vcrd> CustomerQuery(string strSql)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                List<T_Vcrd> vcrds = (List<T_Vcrd>)conn.Query<T_Vcrd>(strSql);
                return vcrds;
            }
        }


        public bool ChangeVcrdListType(string invoiceNo, string dtype,int subTypeFlag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                StringBuilder strSql=new StringBuilder();
                strSql.Append("update t_invoice set flag=0,remark=@remark where flag=1 and invoiceno=@invoiceno;");
                strSql.Append("delete from t_invoicedtl where invoiceno=@invoiceno;");
                strSql.Append("update t_vcrd set dtype=@dtype,remark='特殊情况消费退货,从银行退回该款，从'+dtype +',改为当前类型',typeflag=2,subTypeFlag=@subTypeFlag where flag=0 and origid=@invoiceno");
                var param = new { remark = "该记录银行已扣款，手动退货删除,改为" + dtype, invoiceno = invoiceNo, dtype = dtype, subTypeFlag = subTypeFlag };
                try{
                    int rs = conn.Execute(strSql.ToString(),param);
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }
        }


        public bool DeleteDtlByVcrdSeqno(T_Vcrd vcrd)
        {
            bool rflag = false;
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                StringBuilder strSql = new StringBuilder();
                decimal amountA = 0, amountB = 0, amountC = 0;
                switch (vcrd.TypeFlag)
                {
                    case 3://零用金
                        {
                            //1.删除Vcrd记录
                            //2.删除T_ProvideDTL
                            //3.更新T_Provide金额
                            //4.更新T_Criminal_Card余额

                            //更新零用金明细为失败记录
                            strSql.Append(@"update t_providedtl set flag=0 from  t_providedtl a left join 
                                (select origid,fcrimecode,damount from t_vcrd where seqno=@seqno) b 
                                on a.fcrimecode=b.fcrimecode 
                                where b.damount=a.famount and ( convert(varchar(20), a.seqno)=b.origid or a.pid=b.origid);");

                            //更新零用金主单的余额
                            strSql.Append(@"update t_provide set FAmount=FAmount-d.damount from t_provide c inner join (
                                select pid,damount from  t_providedtl a left join 
                                (select origid,fcrimecode,damount from t_vcrd where seqno=@seqno) b 
                                on a.fcrimecode=b.fcrimecode 
                                where b.damount=a.famount and ( convert(varchar(20), a.seqno)=b.origid or a.pid=b.origid) )d
                                on c.pid=d.pid ;");

                        } break;
                    case 4://劳动报酬
                        {
                            //1.删除Vcrd记录
                            //2.删除T_BonusDTL
                            //3.更新T_Bonus金额
                            //4.更新T_Criminal_Card余额
                            strSql.Append(@"update t_bonusdtl set flag=0 from t_bonusdtl a left join 
                                (select a.origid,a.fcrimecode,sum(Damount) Damount from t_vcrd a,(
                                select origid,fcrimecode from t_vcrd where seqno=@seqno) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) b 
                                on a.fcrimecode=b.fcrimecode 
                                where b.damount=a.famount and ( convert(varchar(20), a.seqno)=b.origid or a.bid=b.origid);");

                            strSql.Append(@"update t_bonus set cnt=cnt-1,FAmount=FAmount-b.Damount from t_bonus a left join 
                                (select a.origid,a.fcrimecode,sum(Damount) Damount from t_vcrd a,(
                                select origid,fcrimecode from t_vcrd where seqno=@seqno) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) b 
                                on a.bid=b.origid ");

                            
                        } break;
                    case 7://超市消费
                        {
                            //软删除T_invoice记录
                            strSql.Append(@"update t_invoice set Flag=0,Remark=Remark+',银行无法扣款手动删除' from t_invoice c inner join (
                                select a.origid,a.fcrimecode,sum(Camount) Camount from t_vcrd a,(
                                select origid,fcrimecode from t_vcrd where seqno=@seqno) b
                                where a.origid=b.origid and a.fcrimecode=b.fcrimecode
                                group by a.origid,a.fcrimecode) d on c.invoiceno=d.origid;");
                            strSql.Append(@"delete from t_invoicedtl where invoiceno=
                                (select origid from t_vcrd where seqno=@seqno);");

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

                            

                        } break;
                    default:
                        break;
                }


                DataSet ds = GetList("flag=0 and bankflag=-1 and fcrimecode='" + vcrd.FCrimeCode + "' and origid='" + vcrd.OrigId + "'");
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    switch (vcrd.AccType)
                    {
                        case 0://A账户
                            {
                                amountA = Convert.ToDecimal(row["Damount"]) - Convert.ToDecimal(row["Camount"]);
                            } break;
                        case 1://B账户
                            {
                                amountB = Convert.ToDecimal(row["Damount"]) - Convert.ToDecimal(row["Camount"]);
                            } break;
                        case 2://C账户
                            {
                                amountC = Convert.ToDecimal(row["Damount"]) - Convert.ToDecimal(row["Camount"]);
                            } break;
                        default:
                            {
                                amountA = Convert.ToDecimal(row["Damount"]) - Convert.ToDecimal(row["Camount"]);
                            }
                            break;
                    }
                }

                //更新余额表
                strSql.Append(@"update t_Criminal_Card set amounta=amounta-@amounta,amountb=amountb-@amountb,amountc=amountc-@amountc
                    where fcrimecode=@fcrimecode;");
                //删除余额记录
                strSql.Append(@"update t_Vcrd set flag=1 , BankFlag=-1 , Remark=Remark+',手动删除' where fcrimecode=@fcrimecode and origid=@origid;");
                
                
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    object param = new { seqno = vcrd.seqno, amounta = amountA, amountb = amountB, amountc = amountC, fcrimecode = vcrd.FCrimeCode,origid=vcrd.OrigId };
                    conn.Execute(strSql.ToString(), param, myTran);
                    myTran.Commit();
                    rflag = true;
                }
                catch (Exception )
                {
                    myTran.Rollback();
                    throw;
                }

            }
            return rflag;
        }
    }
}