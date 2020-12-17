using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //t_bank_PayList
    public partial class t_bank_PayListDAL
    {

        public bool Exists(int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_bank_PayList");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.t_bank_PayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_bank_PayList(");
            strSql.Append("Remark,BankAccNo,BankRcvDate,AccNo,FCrimeCode,FName,Amount,SuccFlag,PayDate,LoadDate,Flag");
            strSql.Append(") values (");
            strSql.Append("@Remark,@BankAccNo,@BankRcvDate,@AccNo,@FCrimeCode,@FName,@Amount,@SuccFlag,@PayDate,@LoadDate,@Flag");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Remark", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankRcvDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SuccFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Remark;
            parameters[1].Value = model.BankAccNo;
            parameters[2].Value = model.BankRcvDate;
            parameters[3].Value = model.AccNo;
            parameters[4].Value = model.FCrimeCode;
            parameters[5].Value = model.FName;
            parameters[6].Value = model.Amount;
            parameters[7].Value = model.SuccFlag;
            parameters[8].Value = model.PayDate;
            parameters[9].Value = model.LoadDate;
            parameters[10].Value = model.Flag;

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
        public bool Update(SelfhelpOrderMgr.Model.t_bank_PayList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_bank_PayList set ");

            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" BankAccNo = @BankAccNo , ");
            strSql.Append(" BankRcvDate = @BankRcvDate , ");
            strSql.Append(" AccNo = @AccNo , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" SuccFlag = @SuccFlag , ");
            strSql.Append(" PayDate = @PayDate , ");
            strSql.Append(" LoadDate = @LoadDate , ");
            strSql.Append(" Flag = @Flag  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,128) ,            
                        new SqlParameter("@BankAccNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankRcvDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SuccFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.seqno;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.BankAccNo;
            parameters[3].Value = model.BankRcvDate;
            parameters[4].Value = model.AccNo;
            parameters[5].Value = model.FCrimeCode;
            parameters[6].Value = model.FName;
            parameters[7].Value = model.Amount;
            parameters[8].Value = model.SuccFlag;
            parameters[9].Value = model.PayDate;
            parameters[10].Value = model.LoadDate;
            parameters[11].Value = model.Flag;
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
            strSql.Append("delete from t_bank_PayList ");
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
            strSql.Append("delete from t_bank_PayList ");
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
        public SelfhelpOrderMgr.Model.t_bank_PayList GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select seqno, Remark, BankAccNo, BankRcvDate, AccNo, FCrimeCode, FName, Amount, SuccFlag, PayDate, LoadDate, Flag  ");
            strSql.Append("  from t_bank_PayList ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.t_bank_PayList model = new SelfhelpOrderMgr.Model.t_bank_PayList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.BankAccNo = ds.Tables[0].Rows[0]["BankAccNo"].ToString();
                if (ds.Tables[0].Rows[0]["BankRcvDate"].ToString() != "")
                {
                    model.BankRcvDate = DateTime.Parse(ds.Tables[0].Rows[0]["BankRcvDate"].ToString());
                }
                model.AccNo = ds.Tables[0].Rows[0]["AccNo"].ToString();
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SuccFlag"].ToString() != "")
                {
                    model.SuccFlag = int.Parse(ds.Tables[0].Rows[0]["SuccFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayDate"].ToString() != "")
                {
                    model.PayDate = DateTime.Parse(ds.Tables[0].Rows[0]["PayDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LoadDate"].ToString() != "")
                {
                    model.LoadDate = DateTime.Parse(ds.Tables[0].Rows[0]["LoadDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }

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
            strSql.Append(" FROM t_bank_PayList ");
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
            strSql.Append(" FROM t_bank_PayList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

