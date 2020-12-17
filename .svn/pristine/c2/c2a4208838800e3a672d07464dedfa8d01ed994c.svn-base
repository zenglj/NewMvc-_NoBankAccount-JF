using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_BatchMoneyTrade_DTL
    public partial class T_BatchMoneyTrade_DTLDAL
    {

        public bool Exists(int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_BatchMoneyTrade_DTL");
            strSql.Append(" where ");
            strSql.Append(" seqno = @seqno  ");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_BatchMoneyTrade_DTL(");
            strSql.Append("FCriminal,Vouno,FrealareaCode,FrealAreaName,Remark,PType,UDate,CrtBy,CrtDt,ApplyBy,Bid,AccType,CardType,AmountC,cqbt,gwjt,ldjx,tbbz,grkj,FMoneyInOutFlag,FCrimeCode,CardCode,FAmount,Flag,FareaCode,FareaName");
            strSql.Append(") values (");
            strSql.Append("@FCriminal,@Vouno,@FrealareaCode,@FrealAreaName,@Remark,@PType,@UDate,@CrtBy,@CrtDt,@ApplyBy,@Bid,@AccType,@CardType,@AmountC,@cqbt,@gwjt,@ldjx,@tbbz,@grkj,@FMoneyInOutFlag,@FCrimeCode,@CardCode,@FAmount,@Flag,@FareaCode,@FareaName");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Bid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@cqbt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gwjt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ldjx", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@tbbz", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@grkj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FMoneyInOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FareaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FareaName", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.FCriminal;
            parameters[1].Value = model.Vouno;
            parameters[2].Value = model.FrealareaCode;
            parameters[3].Value = model.FrealAreaName;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.PType;
            parameters[6].Value = model.UDate;
            parameters[7].Value = model.CrtBy;
            parameters[8].Value = model.CrtDt;
            parameters[9].Value = model.ApplyBy;
            parameters[10].Value = model.Bid;
            parameters[11].Value = model.AccType;
            parameters[12].Value = model.CardType;
            parameters[13].Value = model.AmountC;
            parameters[14].Value = model.cqbt;
            parameters[15].Value = model.gwjt;
            parameters[16].Value = model.ldjx;
            parameters[17].Value = model.tbbz;
            parameters[18].Value = model.grkj;
            parameters[19].Value = model.FMoneyInOutFlag;
            parameters[20].Value = model.FCrimeCode;
            parameters[21].Value = model.CardCode;
            parameters[22].Value = model.FAmount;
            parameters[23].Value = model.Flag;
            parameters[24].Value = model.FareaCode;
            parameters[25].Value = model.FareaName;

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
        public bool Update(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_BatchMoneyTrade_DTL set ");

            strSql.Append(" FCriminal = @FCriminal , ");
            strSql.Append(" Vouno = @Vouno , ");
            strSql.Append(" FrealareaCode = @FrealareaCode , ");
            strSql.Append(" FrealAreaName = @FrealAreaName , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" UDate = @UDate , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDt = @CrtDt , ");
            strSql.Append(" ApplyBy = @ApplyBy , ");
            strSql.Append(" Bid = @Bid , ");
            strSql.Append(" AccType = @AccType , ");
            strSql.Append(" CardType = @CardType , ");
            strSql.Append(" AmountC = @AmountC , ");
            strSql.Append(" cqbt = @cqbt , ");
            strSql.Append(" gwjt = @gwjt , ");
            strSql.Append(" ldjx = @ldjx , ");
            strSql.Append(" tbbz = @tbbz , ");
            strSql.Append(" grkj = @grkj , ");
            strSql.Append(" FMoneyInOutFlag = @FMoneyInOutFlag , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" CardCode = @CardCode , ");
            strSql.Append(" FAmount = @FAmount , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" FareaCode = @FareaCode , ");
            strSql.Append(" FareaName = @FareaName  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Vouno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@UDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ApplyBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Bid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountC", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@cqbt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@gwjt", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ldjx", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@tbbz", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@grkj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FMoneyInOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FareaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FareaName", SqlDbType.VarChar,100)             
              
            };

            parameters[0].Value = model.seqno;
            parameters[1].Value = model.FCriminal;
            parameters[2].Value = model.Vouno;
            parameters[3].Value = model.FrealareaCode;
            parameters[4].Value = model.FrealAreaName;
            parameters[5].Value = model.Remark;
            parameters[6].Value = model.PType;
            parameters[7].Value = model.UDate;
            parameters[8].Value = model.CrtBy;
            parameters[9].Value = model.CrtDt;
            parameters[10].Value = model.ApplyBy;
            parameters[11].Value = model.Bid;
            parameters[12].Value = model.AccType;
            parameters[13].Value = model.CardType;
            parameters[14].Value = model.AmountC;
            parameters[15].Value = model.cqbt;
            parameters[16].Value = model.gwjt;
            parameters[17].Value = model.ldjx;
            parameters[18].Value = model.tbbz;
            parameters[19].Value = model.grkj;
            parameters[20].Value = model.FMoneyInOutFlag;
            parameters[21].Value = model.FCrimeCode;
            parameters[22].Value = model.CardCode;
            parameters[23].Value = model.FAmount;
            parameters[24].Value = model.Flag;
            parameters[25].Value = model.FareaCode;
            parameters[26].Value = model.FareaName;
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
            strSql.Append("delete from T_BatchMoneyTrade_DTL ");
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
            strSql.Append("delete from T_BatchMoneyTrade_DTL ");
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
        public SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select seqno, FCriminal, Vouno, FrealareaCode, FrealAreaName, Remark, PType, UDate, CrtBy, CrtDt, ApplyBy, Bid, AccType, CardType, AmountC, cqbt, gwjt, ldjx, tbbz, grkj, FMoneyInOutFlag, FCrimeCode, CardCode, FAmount, Flag, FareaCode, FareaName  ");
            strSql.Append("  from T_BatchMoneyTrade_DTL ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model = new SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                model.FCriminal = ds.Tables[0].Rows[0]["FCriminal"].ToString();
                model.Vouno = ds.Tables[0].Rows[0]["Vouno"].ToString();
                model.FrealareaCode = ds.Tables[0].Rows[0]["FrealareaCode"].ToString();
                model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                if (ds.Tables[0].Rows[0]["UDate"].ToString() != "")
                {
                    model.UDate = DateTime.Parse(ds.Tables[0].Rows[0]["UDate"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDt"].ToString() != "")
                {
                    model.CrtDt = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDt"].ToString());
                }
                model.ApplyBy = ds.Tables[0].Rows[0]["ApplyBy"].ToString();
                model.Bid = ds.Tables[0].Rows[0]["Bid"].ToString();
                if (ds.Tables[0].Rows[0]["AccType"].ToString() != "")
                {
                    model.AccType = int.Parse(ds.Tables[0].Rows[0]["AccType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardType"].ToString() != "")
                {
                    model.CardType = int.Parse(ds.Tables[0].Rows[0]["CardType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountC"].ToString() != "")
                {
                    model.AmountC = decimal.Parse(ds.Tables[0].Rows[0]["AmountC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["cqbt"].ToString() != "")
                {
                    model.cqbt = decimal.Parse(ds.Tables[0].Rows[0]["cqbt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["gwjt"].ToString() != "")
                {
                    model.gwjt = decimal.Parse(ds.Tables[0].Rows[0]["gwjt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ldjx"].ToString() != "")
                {
                    model.ldjx = decimal.Parse(ds.Tables[0].Rows[0]["ldjx"].ToString());
                }
                if (ds.Tables[0].Rows[0]["tbbz"].ToString() != "")
                {
                    model.tbbz = decimal.Parse(ds.Tables[0].Rows[0]["tbbz"].ToString());
                }
                if (ds.Tables[0].Rows[0]["grkj"].ToString() != "")
                {
                    model.grkj = decimal.Parse(ds.Tables[0].Rows[0]["grkj"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMoneyInOutFlag"].ToString() != "")
                {
                    model.FMoneyInOutFlag = int.Parse(ds.Tables[0].Rows[0]["FMoneyInOutFlag"].ToString());
                }
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                model.CardCode = ds.Tables[0].Rows[0]["CardCode"].ToString();
                if (ds.Tables[0].Rows[0]["FAmount"].ToString() != "")
                {
                    model.FAmount = decimal.Parse(ds.Tables[0].Rows[0]["FAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.FareaCode = ds.Tables[0].Rows[0]["FareaCode"].ToString();
                model.FareaName = ds.Tables[0].Rows[0]["FareaName"].ToString();

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
            strSql.Append(" FROM T_BatchMoneyTrade_DTL ");
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
            strSql.Append(" FROM T_BatchMoneyTrade_DTL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

