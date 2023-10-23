using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Vcrd
    public partial class T_VcrdDAL
    {

        public bool Exists(int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Vcrd");
            strSql.Append(" where ");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Vcrd model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Vcrd(");
            strSql.Append("Vouno,Remark,Flag,DelBy,DelDate,FAreaCode,FAreaName,FCriminal,Frealareacode,FrealAreaName,PType,CardCode,UDate,OrigId,CardType,TypeFlag,AccType,SendDate,BankFlag,CheckFlag,CheckDate,CheckBy,FCrimeCode,pc,SubTypeFlag,RcvDate,CurUserAmount,CurAllAmount,bankRcvFlag,FinancePayId,FinancePayFlag,BankInterfaceFlag,DAmount,PayAuditFlag,CAmount,CrtBy,CrtDate,DType,Depositer,PayMode");
            strSql.Append(") values (");
            strSql.Append("@Vouno,@Remark,@Flag,@DelBy,@DelDate,@FAreaCode,@FAreaName,@FCriminal,@Frealareacode,@FrealAreaName,@PType,@CardCode,@UDate,@OrigId,@CardType,@TypeFlag,@AccType,@SendDate,@BankFlag,@CheckFlag,@CheckDate,@CheckBy,@FCrimeCode,@pc,@SubTypeFlag,@RcvDate,@CurUserAmount,@CurAllAmount,@bankRcvFlag,@FinancePayId,@FinancePayFlag,@BankInterfaceFlag,@DAmount,@PayAuditFlag,@CAmount,@CrtBy,@CrtDate,@DType,@Depositer,@PayMode");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DelBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DelDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@OrigId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@BankFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@SubTypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@RcvDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CurUserAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CurAllAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@bankRcvFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FinancePayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FinancePayFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@BankInterfaceFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@PayAuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Depositer", SqlDbType.VarChar,20) ,
                        new SqlParameter("@PayMode", SqlDbType.Int,4)

            };

            parameters[0].Value = model.Vouno;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Flag;
            parameters[3].Value = model.DelBy;
            parameters[4].Value = model.DelDate;
            parameters[5].Value = model.FAreaCode;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FCriminal;
            parameters[8].Value = model.Frealareacode;
            parameters[9].Value = model.FrealAreaName;
            parameters[10].Value = model.PType;
            parameters[11].Value = model.CardCode;
            parameters[12].Value = model.UDate;
            parameters[13].Value = model.OrigId;
            parameters[14].Value = model.CardType;
            parameters[15].Value = model.TypeFlag;
            parameters[16].Value = model.AccType;
            parameters[17].Value = model.SendDate;
            parameters[18].Value = model.BankFlag;
            parameters[19].Value = model.CheckFlag;
            parameters[20].Value = model.CheckDate;
            parameters[21].Value = model.CheckBy;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.pc;
            parameters[24].Value = model.SubTypeFlag;
            parameters[25].Value = model.RcvDate;
            parameters[26].Value = model.CurUserAmount;
            parameters[27].Value = model.CurAllAmount;
            parameters[28].Value = model.bankRcvFlag;
            parameters[29].Value = model.FinancePayId;
            parameters[30].Value = model.FinancePayFlag;
            parameters[31].Value = model.BankInterfaceFlag;
            parameters[32].Value = model.DAmount;
            parameters[33].Value = model.PayAuditFlag;
            parameters[34].Value = model.CAmount;
            parameters[35].Value = model.CrtBy;
            parameters[36].Value = model.CrtDate;
            parameters[37].Value = model.DType;
            parameters[38].Value = model.Depositer;
            parameters[39].Value = model.PayMode;

            object obj = SqlHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Vcrd model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Vcrd set ");

            strSql.Append(" Vouno = @Vouno , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" DelBy = @DelBy , ");
            strSql.Append(" DelDate = @DelDate , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FAreaName = @FAreaName , ");
            strSql.Append(" FCriminal = @FCriminal , ");
            strSql.Append(" Frealareacode = @Frealareacode , ");
            strSql.Append(" FrealAreaName = @FrealAreaName , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" CardCode = @CardCode , ");
            strSql.Append(" UDate = @UDate , ");
            strSql.Append(" OrigId = @OrigId , ");
            strSql.Append(" CardType = @CardType , ");
            strSql.Append(" TypeFlag = @TypeFlag , ");
            strSql.Append(" AccType = @AccType , ");
            strSql.Append(" SendDate = @SendDate , ");
            strSql.Append(" BankFlag = @BankFlag , ");
            strSql.Append(" CheckFlag = @CheckFlag , ");
            strSql.Append(" CheckDate = @CheckDate , ");
            strSql.Append(" CheckBy = @CheckBy , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" pc = @pc , ");
            strSql.Append(" SubTypeFlag = @SubTypeFlag , ");
            strSql.Append(" RcvDate = @RcvDate , ");
            strSql.Append(" CurUserAmount = @CurUserAmount , ");
            strSql.Append(" CurAllAmount = @CurAllAmount , ");
            strSql.Append(" bankRcvFlag = @bankRcvFlag , ");
            strSql.Append(" FinancePayId = @FinancePayId , ");
            strSql.Append(" FinancePayFlag = @FinancePayFlag , ");
            strSql.Append(" BankInterfaceFlag = @BankInterfaceFlag , ");
            strSql.Append(" DAmount = @DAmount , ");
            strSql.Append(" PayAuditFlag = @PayAuditFlag , ");
            strSql.Append(" CAmount = @CAmount , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDate = @CrtDate , ");
            strSql.Append(" DType = @DType , ");
            strSql.Append(" Depositer = @Depositer  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DelBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@DelDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@OrigId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@SendDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@BankFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CheckDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CheckBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pc", SqlDbType.Int,4) ,            
                        new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@SubTypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@RcvDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CurUserAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CurAllAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@bankRcvFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FinancePayId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FinancePayFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@BankInterfaceFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@DAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@PayAuditFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@DType", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Depositer", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.Vouno;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.Flag;
            parameters[3].Value = model.DelBy;
            parameters[4].Value = model.DelDate;
            parameters[5].Value = model.FAreaCode;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FCriminal;
            parameters[8].Value = model.Frealareacode;
            parameters[9].Value = model.FrealAreaName;
            parameters[10].Value = model.PType;
            parameters[11].Value = model.CardCode;
            parameters[12].Value = model.UDate;
            parameters[13].Value = model.OrigId;
            parameters[14].Value = model.CardType;
            parameters[15].Value = model.TypeFlag;
            parameters[16].Value = model.AccType;
            parameters[17].Value = model.SendDate;
            parameters[18].Value = model.BankFlag;
            parameters[19].Value = model.CheckFlag;
            parameters[20].Value = model.CheckDate;
            parameters[21].Value = model.CheckBy;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.pc;
            parameters[24].Value = model.seqno;
            parameters[25].Value = model.SubTypeFlag;
            parameters[26].Value = model.RcvDate;
            parameters[27].Value = model.CurUserAmount;
            parameters[28].Value = model.CurAllAmount;
            parameters[29].Value = model.bankRcvFlag;
            parameters[30].Value = model.FinancePayId;
            parameters[31].Value = model.FinancePayFlag;
            parameters[32].Value = model.BankInterfaceFlag;
            parameters[33].Value = model.DAmount;
            parameters[34].Value = model.PayAuditFlag;
            parameters[35].Value = model.CAmount;
            parameters[36].Value = model.CrtBy;
            parameters[37].Value = model.CrtDate;
            parameters[38].Value = model.DType;
            parameters[39].Value = model.Depositer;
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Vcrd ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string seqnolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Vcrd ");
            strSql.Append(" where ID in (" + seqnolist + ")  ");
            int rows = SqlHelper.ExecuteSql(strSql.ToString());
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
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Vcrd GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Vouno, Remark, Flag, DelBy, DelDate, FAreaCode, FAreaName, FCriminal, Frealareacode, FrealAreaName, PType, CardCode, UDate, OrigId, CardType, TypeFlag, AccType, SendDate, BankFlag, CheckFlag, CheckDate, CheckBy, FCrimeCode, pc, seqno, SubTypeFlag, RcvDate, CurUserAmount, CurAllAmount, bankRcvFlag, FinancePayId, FinancePayFlag, BankInterfaceFlag, DAmount, PayAuditFlag, CAmount, CrtBy, CrtDate, DType, Depositer,PayMode  ");
            strSql.Append("  from T_Vcrd ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.T_Vcrd model = new SelfhelpOrderMgr.Model.T_Vcrd();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Vouno = ds.Tables[0].Rows[0]["Vouno"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.DelBy = ds.Tables[0].Rows[0]["DelBy"].ToString();
                if (ds.Tables[0].Rows[0]["DelDate"].ToString() != "")
                {
                    model.DelDate = DateTime.Parse(ds.Tables[0].Rows[0]["DelDate"].ToString());
                }
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                model.FAreaName = ds.Tables[0].Rows[0]["FAreaName"].ToString();
                model.FCriminal = ds.Tables[0].Rows[0]["FCriminal"].ToString();
                model.Frealareacode = ds.Tables[0].Rows[0]["Frealareacode"].ToString();
                model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                model.CardCode = ds.Tables[0].Rows[0]["CardCode"].ToString();
                if (ds.Tables[0].Rows[0]["UDate"].ToString() != "")
                {
                    model.UDate = DateTime.Parse(ds.Tables[0].Rows[0]["UDate"].ToString());
                }
                model.OrigId = ds.Tables[0].Rows[0]["OrigId"].ToString();
                if (ds.Tables[0].Rows[0]["CardType"].ToString() != "")
                {
                    model.CardType = int.Parse(ds.Tables[0].Rows[0]["CardType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TypeFlag"].ToString() != "")
                {
                    model.TypeFlag = int.Parse(ds.Tables[0].Rows[0]["TypeFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccType"].ToString() != "")
                {
                    model.AccType = int.Parse(ds.Tables[0].Rows[0]["AccType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SendDate"].ToString() != "")
                {
                    model.SendDate = DateTime.Parse(ds.Tables[0].Rows[0]["SendDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankFlag"].ToString() != "")
                {
                    model.BankFlag = int.Parse(ds.Tables[0].Rows[0]["BankFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckFlag"].ToString() != "")
                {
                    model.CheckFlag = int.Parse(ds.Tables[0].Rows[0]["CheckFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CheckDate"].ToString() != "")
                {
                    model.CheckDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString());
                }
                model.CheckBy = ds.Tables[0].Rows[0]["CheckBy"].ToString();
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                if (ds.Tables[0].Rows[0]["pc"].ToString() != "")
                {
                    model.pc = int.Parse(ds.Tables[0].Rows[0]["pc"].ToString());
                }
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SubTypeFlag"].ToString() != "")
                {
                    model.SubTypeFlag = int.Parse(ds.Tables[0].Rows[0]["SubTypeFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RcvDate"].ToString() != "")
                {
                    model.RcvDate = DateTime.Parse(ds.Tables[0].Rows[0]["RcvDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CurUserAmount"].ToString() != "")
                {
                    model.CurUserAmount = decimal.Parse(ds.Tables[0].Rows[0]["CurUserAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CurAllAmount"].ToString() != "")
                {
                    model.CurAllAmount = decimal.Parse(ds.Tables[0].Rows[0]["CurAllAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["bankRcvFlag"].ToString() != "")
                {
                    model.bankRcvFlag = int.Parse(ds.Tables[0].Rows[0]["bankRcvFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FinancePayId"].ToString() != "")
                {
                    model.FinancePayId = int.Parse(ds.Tables[0].Rows[0]["FinancePayId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FinancePayFlag"].ToString() != "")
                {
                    model.FinancePayFlag = int.Parse(ds.Tables[0].Rows[0]["FinancePayFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BankInterfaceFlag"].ToString() != "")
                {
                    model.BankInterfaceFlag = int.Parse(ds.Tables[0].Rows[0]["BankInterfaceFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["DAmount"].ToString() != "")
                {
                    model.DAmount = decimal.Parse(ds.Tables[0].Rows[0]["DAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayAuditFlag"].ToString() != "")
                {
                    model.PayAuditFlag = int.Parse(ds.Tables[0].Rows[0]["PayAuditFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CAmount"].ToString() != "")
                {
                    model.CAmount = decimal.Parse(ds.Tables[0].Rows[0]["CAmount"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDate"].ToString() != "")
                {
                    model.CrtDate = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDate"].ToString());
                }
                model.DType = ds.Tables[0].Rows[0]["DType"].ToString();
                model.Depositer = ds.Tables[0].Rows[0]["Depositer"].ToString();
                model.PayMode = int.Parse(ds.Tables[0].Rows[0]["PayMode"].ToString());

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Vcrd ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM T_Vcrd ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

