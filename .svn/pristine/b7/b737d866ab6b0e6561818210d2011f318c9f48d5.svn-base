using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_ProvideDTL
    public partial class T_ProvideDTLDAL
    {

        public bool Exists(int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_ProvideDTL");
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
        public int Add(SelfhelpOrderMgr.Model.T_ProvideDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_ProvideDTL(");
            strSql.Append("VouNo,FrealareaCode,FrealareaName,FSex,AccType,CardType,PDate,PId,FCrimeCode,CardCode,FAmount,Flag,FAreaCode,FAreaName,FCriminal");
            strSql.Append(") values (");
            strSql.Append("@VouNo,@FrealareaCode,@FrealareaName,@FSex,@AccType,@CardType,@PDate,@PId,@FCrimeCode,@CardCode,@FAmount,@Flag,@FAreaCode,@FAreaName,@FCriminal");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@VouNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@PDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.VouNo;
            parameters[1].Value = model.FrealareaCode;
            parameters[2].Value = model.FrealareaName;
            parameters[3].Value = model.FSex;
            parameters[4].Value = model.AccType;
            parameters[5].Value = model.CardType;
            parameters[6].Value = model.PDate;
            parameters[7].Value = model.PId;
            parameters[8].Value = model.FCrimeCode;
            parameters[9].Value = model.CardCode;
            parameters[10].Value = model.FAmount;
            parameters[11].Value = model.Flag;
            parameters[12].Value = model.FAreaCode;
            parameters[13].Value = model.FAreaName;
            parameters[14].Value = model.FCriminal;

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
        public bool Update(SelfhelpOrderMgr.Model.T_ProvideDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_ProvideDTL set ");

            strSql.Append(" VouNo = @VouNo , ");
            strSql.Append(" FrealareaCode = @FrealareaCode , ");
            strSql.Append(" FrealareaName = @FrealareaName , ");
            strSql.Append(" FSex = @FSex , ");
            strSql.Append(" AccType = @AccType , ");
            strSql.Append(" CardType = @CardType , ");
            strSql.Append(" PDate = @PDate , ");
            strSql.Append(" PId = @PId , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" CardCode = @CardCode , ");
            strSql.Append(" FAmount = @FAmount , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FAreaName = @FAreaName , ");
            strSql.Append(" FCriminal = @FCriminal  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@VouNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FSex", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@PDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.seqno;
            parameters[1].Value = model.VouNo;
            parameters[2].Value = model.FrealareaCode;
            parameters[3].Value = model.FrealareaName;
            parameters[4].Value = model.FSex;
            parameters[5].Value = model.AccType;
            parameters[6].Value = model.CardType;
            parameters[7].Value = model.PDate;
            parameters[8].Value = model.PId;
            parameters[9].Value = model.FCrimeCode;
            parameters[10].Value = model.CardCode;
            parameters[11].Value = model.FAmount;
            parameters[12].Value = model.Flag;
            parameters[13].Value = model.FAreaCode;
            parameters[14].Value = model.FAreaName;
            parameters[15].Value = model.FCriminal;
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
            strSql.Append("delete from T_ProvideDTL ");
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
            strSql.Append("delete from T_ProvideDTL ");
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
        public SelfhelpOrderMgr.Model.T_ProvideDTL GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select seqno, VouNo, FrealareaCode, FrealareaName, FSex, AccType, CardType, PDate, PId, FCrimeCode, CardCode, FAmount, Flag, FAreaCode, FAreaName, FCriminal  ");
            strSql.Append("  from T_ProvideDTL ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.T_ProvideDTL model = new SelfhelpOrderMgr.Model.T_ProvideDTL();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                model.VouNo = ds.Tables[0].Rows[0]["VouNo"].ToString();
                model.FrealareaCode = ds.Tables[0].Rows[0]["FrealareaCode"].ToString();
                model.FrealareaName = ds.Tables[0].Rows[0]["FrealareaName"].ToString();
                model.FSex = ds.Tables[0].Rows[0]["FSex"].ToString();
                if (ds.Tables[0].Rows[0]["AccType"].ToString() != "")
                {
                    model.AccType = int.Parse(ds.Tables[0].Rows[0]["AccType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CardType"].ToString() != "")
                {
                    model.CardType = int.Parse(ds.Tables[0].Rows[0]["CardType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PDate"].ToString() != "")
                {
                    model.PDate = DateTime.Parse(ds.Tables[0].Rows[0]["PDate"].ToString());
                }
                model.PId = ds.Tables[0].Rows[0]["PId"].ToString();
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
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                model.FAreaName = ds.Tables[0].Rows[0]["FAreaName"].ToString();
                model.FCriminal = ds.Tables[0].Rows[0]["FCriminal"].ToString();

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
            strSql.Append(" FROM T_ProvideDTL ");
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
            strSql.Append(" FROM T_ProvideDTL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

