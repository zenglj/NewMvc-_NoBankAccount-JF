using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Stock
    public partial class T_StockDAL
    {

        public bool Exists(string StockId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Stock");
            strSql.Append(" where ");
            strSql.Append(" StockId = @StockId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockId", SqlDbType.VarChar,20)			};
            parameters[0].Value = StockId;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Stock model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Stock(");
            strSql.Append("StockId,Remark,invoiceno,stockflag,InOutFlag,InOutDate,FLAG,StockType,CrtBy,Crtdt,CHECKFLAG,CHECKBY,CheckDt");
            strSql.Append(") values (");
            strSql.Append("@StockId,@Remark,@invoiceno,@stockflag,@InOutFlag,@InOutDate,@FLAG,@StockType,@CrtBy,@Crtdt,@CHECKFLAG,@CHECKBY,@CheckDt");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@StockId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@invoiceno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@stockflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@StockType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CHECKFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@CHECKBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDt", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.StockId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.InvoiceNo;
            parameters[3].Value = model.Stockflag;
            parameters[4].Value = model.InOutFlag;
            parameters[5].Value = model.InOutDate;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.StockType;
            parameters[8].Value = model.CrtBy;
            parameters[9].Value = model.CrtDt;
            parameters[10].Value = model.CheckFlag;
            parameters[11].Value = model.CheckBy;
            parameters[12].Value = model.CheckDt;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Stock model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Stock set ");

            strSql.Append(" StockId = @StockId , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" invoiceno = @invoiceno , ");
            strSql.Append(" stockflag = @stockflag , ");
            strSql.Append(" InOutFlag = @InOutFlag , ");
            strSql.Append(" InOutDate = @InOutDate , ");
            strSql.Append(" FLAG = @FLAG , ");
            strSql.Append(" StockType = @StockType , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" Crtdt = @Crtdt , ");
            strSql.Append(" CHECKFLAG = @CHECKFLAG , ");
            strSql.Append(" CHECKBY = @CHECKBY , ");
            strSql.Append(" CheckDt = @CheckDt  ");
            strSql.Append(" where StockId=@StockId  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@StockId", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,250) ,            
                        new SqlParameter("@invoiceno", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@stockflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InOutDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@StockType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CHECKFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@CHECKBY", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CheckDt", SqlDbType.DateTime)             
              
            };

            parameters[0].Value = model.StockId;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.InvoiceNo;
            parameters[3].Value = model.Stockflag;
            parameters[4].Value = model.InOutFlag;
            parameters[5].Value = model.InOutDate;
            parameters[6].Value = model.Flag;
            parameters[7].Value = model.StockType;
            parameters[8].Value = model.CrtBy;
            parameters[9].Value = model.CrtDt;
            parameters[10].Value = model.CheckFlag;
            parameters[11].Value = model.CheckBy;
            parameters[12].Value = model.CheckDt;
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
        public bool Delete(string StockId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Stock ");
            strSql.Append(" where StockId=@StockId ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockId", SqlDbType.VarChar,20)			};
            parameters[0].Value = StockId;


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
        public SelfhelpOrderMgr.Model.T_Stock GetModel(string StockId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StockId, Remark, invoiceno, stockflag, InOutFlag, InOutDate, FLAG, StockType, CrtBy, Crtdt, CHECKFLAG, CHECKBY, CheckDt  ");
            strSql.Append("  from T_Stock ");
            strSql.Append(" where StockId=@StockId ");
            SqlParameter[] parameters = {
					new SqlParameter("@StockId", SqlDbType.VarChar,20)			};
            parameters[0].Value = StockId;


            SelfhelpOrderMgr.Model.T_Stock model = new SelfhelpOrderMgr.Model.T_Stock();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.StockId = ds.Tables[0].Rows[0]["StockId"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.InvoiceNo = ds.Tables[0].Rows[0]["invoiceno"].ToString();
                if (ds.Tables[0].Rows[0]["stockflag"].ToString() != "")
                {
                    model.Stockflag = int.Parse(ds.Tables[0].Rows[0]["stockflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InOutFlag"].ToString() != "")
                {
                    model.InOutFlag = int.Parse(ds.Tables[0].Rows[0]["InOutFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["InOutDate"].ToString() != "")
                {
                    model.InOutDate = DateTime.Parse(ds.Tables[0].Rows[0]["InOutDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FLAG"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["FLAG"].ToString());
                }
                model.StockType = ds.Tables[0].Rows[0]["StockType"].ToString();
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["Crtdt"].ToString() != "")
                {
                    model.CrtDt = DateTime.Parse(ds.Tables[0].Rows[0]["Crtdt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHECKFLAG"].ToString() != "")
                {
                    model.CheckFlag = int.Parse(ds.Tables[0].Rows[0]["CHECKFLAG"].ToString());
                }
                model.CheckBy = ds.Tables[0].Rows[0]["CHECKBY"].ToString();
                if (ds.Tables[0].Rows[0]["CheckDt"].ToString() != "")
                {
                    model.CheckDt = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDt"].ToString());
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
            strSql.Append(" FROM T_Stock ");
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
            strSql.Append(" FROM T_Stock ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

