using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_BankReturnList
    public partial class T_SHO_BankReturnListDAL
    {

        public bool Exists(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_BankReturnList");
            strSql.Append(" where ");
            strSql.Append(" BID = @BID and  ");
            strSql.Append(" BankCard = @BankCard and  ");
            strSql.Append(" UserName = @UserName and  ");
            strSql.Append(" UserMoney = @UserMoney and  ");
            strSql.Append(" ResultInfo = @ResultInfo and  ");
            strSql.Append(" Flag = @Flag  ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@BankCard", SqlDbType.VarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ResultInfo", SqlDbType.VarChar,50),
					new SqlParameter("@Flag", SqlDbType.Int,4)			};
            parameters[0].Value = BID;
            parameters[1].Value = BankCard;
            parameters[2].Value = UserName;
            parameters[3].Value = UserMoney;
            parameters[4].Value = ResultInfo;
            parameters[5].Value = Flag;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_SHO_BankReturnList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_BankReturnList(");
            strSql.Append("BID,BankCard,UserName,UserMoney,ResultInfo,Flag");
            strSql.Append(") values (");
            strSql.Append("@BID,@BankCard,@UserName,@UserMoney,@ResultInfo,@Flag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankCard", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ResultInfo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.BID;
            parameters[1].Value = model.BankCard;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.UserMoney;
            parameters[4].Value = model.ResultInfo;
            parameters[5].Value = model.Flag;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_BankReturnList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_BankReturnList set ");

            strSql.Append(" BID = @BID , ");
            strSql.Append(" BankCard = @BankCard , ");
            strSql.Append(" UserName = @UserName , ");
            strSql.Append(" UserMoney = @UserMoney , ");
            strSql.Append(" ResultInfo = @ResultInfo , ");
            strSql.Append(" Flag = @Flag  ");
            strSql.Append(" where BID=@BID and BankCard=@BankCard and UserName=@UserName and UserMoney=@UserMoney and ResultInfo=@ResultInfo and Flag=@Flag  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@BID", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@BankCard", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@UserName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@UserMoney", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ResultInfo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.BID;
            parameters[1].Value = model.BankCard;
            parameters[2].Value = model.UserName;
            parameters[3].Value = model.UserMoney;
            parameters[4].Value = model.ResultInfo;
            parameters[5].Value = model.Flag;
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
        public bool Delete(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_BankReturnList ");
            strSql.Append(" where BID=@BID and BankCard=@BankCard and UserName=@UserName and UserMoney=@UserMoney and ResultInfo=@ResultInfo and Flag=@Flag ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@BankCard", SqlDbType.VarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ResultInfo", SqlDbType.VarChar,50),
					new SqlParameter("@Flag", SqlDbType.Int,4)			};
            parameters[0].Value = BID;
            parameters[1].Value = BankCard;
            parameters[2].Value = UserName;
            parameters[3].Value = UserMoney;
            parameters[4].Value = ResultInfo;
            parameters[5].Value = Flag;


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
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_BankReturnList GetModel(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BID, BankCard, UserName, UserMoney, ResultInfo, Flag  ");
            strSql.Append("  from T_SHO_BankReturnList ");
            strSql.Append(" where BID=@BID and BankCard=@BankCard and UserName=@UserName and UserMoney=@UserMoney and ResultInfo=@ResultInfo and Flag=@Flag ");
            SqlParameter[] parameters = {
					new SqlParameter("@BID", SqlDbType.VarChar,20),
					new SqlParameter("@BankCard", SqlDbType.VarChar,20),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@UserMoney", SqlDbType.Decimal,9),
					new SqlParameter("@ResultInfo", SqlDbType.VarChar,50),
					new SqlParameter("@Flag", SqlDbType.Int,4)			};
            parameters[0].Value = BID;
            parameters[1].Value = BankCard;
            parameters[2].Value = UserName;
            parameters[3].Value = UserMoney;
            parameters[4].Value = ResultInfo;
            parameters[5].Value = Flag;


            SelfhelpOrderMgr.Model.T_SHO_BankReturnList model = new SelfhelpOrderMgr.Model.T_SHO_BankReturnList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.BID = ds.Tables[0].Rows[0]["BID"].ToString();
                model.BankCard = ds.Tables[0].Rows[0]["BankCard"].ToString();
                model.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                if (ds.Tables[0].Rows[0]["UserMoney"].ToString() != "")
                {
                    model.UserMoney = decimal.Parse(ds.Tables[0].Rows[0]["UserMoney"].ToString());
                }
                model.ResultInfo = ds.Tables[0].Rows[0]["ResultInfo"].ToString();
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
            strSql.Append(" FROM T_SHO_BankReturnList ");
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
            strSql.Append(" FROM T_SHO_BankReturnList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

