using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_FinancePay
    public partial class T_SHO_FinancePayDAL
    {

        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_FinancePay");
            strSql.Append(" where ");
            strSql.Append(" Id = @Id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_FinancePay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_FinancePay(");
            strSql.Append("Flag,PayBy,PayDate,Remark,FType,FTitle,FCount,FMoney,CrtBy,CrtDt,PosName,BankCard");
            strSql.Append(") values (");
            strSql.Append("@Flag,@PayBy,@PayDate,@Remark,@FType,@FTitle,@FCount,@FMoney,@CrtBy,@CrtDt,@PosName,@BankCard");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PayBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FType", SqlDbType.NChar,10) ,            
                        new SqlParameter("@FTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@FMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.NChar,10) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@PosName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankCard", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Flag;
            parameters[1].Value = model.PayBy;
            parameters[2].Value = model.PayDate;
            parameters[3].Value = model.Remark;
            parameters[4].Value = model.FType;
            parameters[5].Value = model.FTitle;
            parameters[6].Value = model.FCount;
            parameters[7].Value = model.FMoney;
            parameters[8].Value = model.CrtBy;
            parameters[9].Value = model.CrtDt;
            parameters[10].Value = model.PosName;
            parameters[11].Value = model.BankCard;

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
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_FinancePay model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_FinancePay set ");

            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" PayBy = @PayBy , ");
            strSql.Append(" PayDate = @PayDate , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" FType = @FType , ");
            strSql.Append(" FTitle = @FTitle , ");
            strSql.Append(" FCount = @FCount , ");
            strSql.Append(" FMoney = @FMoney , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDt = @CrtDt , ");
            strSql.Append(" PosName = @PosName , ");
            strSql.Append(" BankCard = @BankCard  ");
            strSql.Append(" where Id=@Id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PayBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FType", SqlDbType.NChar,10) ,            
                        new SqlParameter("@FTitle", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@FMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.NChar,10) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@PosName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@BankCard", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.Id;
            parameters[1].Value = model.Flag;
            parameters[2].Value = model.PayBy;
            parameters[3].Value = model.PayDate;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.FType;
            parameters[6].Value = model.FTitle;
            parameters[7].Value = model.FCount;
            parameters[8].Value = model.FMoney;
            parameters[9].Value = model.CrtBy;
            parameters[10].Value = model.CrtDt;
            parameters[11].Value = model.PosName;
            parameters[12].Value = model.BankCard;
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
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_FinancePay ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;


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
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_FinancePay ");
            strSql.Append(" where ID in (" + Idlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_SHO_FinancePay GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id, Flag, PayBy, PayDate, Remark, FType, FTitle, FCount, FMoney, CrtBy, CrtDt, PosName, BankCard  ");
            strSql.Append("  from T_SHO_FinancePay ");
            strSql.Append(" where Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
            parameters[0].Value = Id;


            SelfhelpOrderMgr.Model.T_SHO_FinancePay model = new SelfhelpOrderMgr.Model.T_SHO_FinancePay();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.PayBy = ds.Tables[0].Rows[0]["PayBy"].ToString();
                if (ds.Tables[0].Rows[0]["PayDate"].ToString() != "")
                {
                    model.PayDate = DateTime.Parse(ds.Tables[0].Rows[0]["PayDate"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.FType = ds.Tables[0].Rows[0]["FType"].ToString();
                model.FTitle = ds.Tables[0].Rows[0]["FTitle"].ToString();
                if (ds.Tables[0].Rows[0]["FCount"].ToString() != "")
                {
                    model.FCount = int.Parse(ds.Tables[0].Rows[0]["FCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FMoney"].ToString() != "")
                {
                    model.FMoney = decimal.Parse(ds.Tables[0].Rows[0]["FMoney"].ToString());
                }
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDt"].ToString() != "")
                {
                    model.CrtDt = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDt"].ToString());
                }
                model.PosName = ds.Tables[0].Rows[0]["PosName"].ToString();
                model.BankCard = ds.Tables[0].Rows[0]["BankCard"].ToString();

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
            strSql.Append(" FROM T_SHO_FinancePay ");
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
            strSql.Append(" FROM T_SHO_FinancePay ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

