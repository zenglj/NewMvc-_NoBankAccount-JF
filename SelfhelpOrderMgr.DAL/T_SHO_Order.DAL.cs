using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_Order
    public partial class T_SHO_OrderDAL
    {

        public bool Exists(int OrderID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_Order");
            strSql.Append(" where ");
            strSql.Append(" OrderID = @OrderID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderID;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_Order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_Order(");
            strSql.Append("FreeAmount,RoomNO,CrtBy,FTZSP_Money,PType,FCrimecode,FCriminal,CrtDate,FAmount,Flag,InvoiceNo,IPAddr");
            strSql.Append(") values (");
            strSql.Append("@FreeAmount,@RoomNO,@CrtBy,@FTZSP_Money,@PType,@FCrimecode,@FCriminal,@CrtDate,@FAmount,@Flag,@InvoiceNo,@IPAddr");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@FreeAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RoomNO", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCrimecode", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@FCriminal", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@IPAddr", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.FreeAmount;
            parameters[1].Value = model.RoomNO;
            parameters[2].Value = model.CrtBy;
            parameters[3].Value = model.FTZSP_Money;
            parameters[4].Value = model.PType;
            parameters[5].Value = model.FCrimecode;
            parameters[6].Value = model.FCriminal;
            parameters[7].Value = model.CrtDate;
            parameters[8].Value = model.FAmount;
            parameters[9].Value = model.Flag;
            parameters[10].Value = model.InvoiceNo;
            parameters[11].Value = model.IPAddr;

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
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_Order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_Order set ");

            strSql.Append(" FreeAmount = @FreeAmount , ");
            strSql.Append(" RoomNO = @RoomNO , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" FTZSP_Money = @FTZSP_Money , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" FCrimecode = @FCrimecode , ");
            strSql.Append(" FCriminal = @FCriminal , ");
            strSql.Append(" CrtDate = @CrtDate , ");
            strSql.Append(" FAmount = @FAmount , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" InvoiceNo = @InvoiceNo , ");
            strSql.Append(" IPAddr = @IPAddr  ");
            strSql.Append(" where OrderID=@OrderID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@OrderID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@RoomNO", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCrimecode", SqlDbType.NVarChar,20) ,            
                        new SqlParameter("@FCriminal", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@CrtDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@FAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@InvoiceNo", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@IPAddr", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.OrderID;
            parameters[1].Value = model.FreeAmount;
            parameters[2].Value = model.RoomNO;
            parameters[3].Value = model.CrtBy;
            parameters[4].Value = model.FTZSP_Money;
            parameters[5].Value = model.PType;
            parameters[6].Value = model.FCrimecode;
            parameters[7].Value = model.FCriminal;
            parameters[8].Value = model.CrtDate;
            parameters[9].Value = model.FAmount;
            parameters[10].Value = model.Flag;
            parameters[11].Value = model.InvoiceNo;
            parameters[12].Value = model.IPAddr;
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
        public bool Delete(int OrderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_Order ");
            strSql.Append(" where OrderID=@OrderID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderID;


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
        public bool DeleteList(string OrderIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_Order ");
            strSql.Append(" where ID in (" + OrderIDlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_SHO_Order GetModel(int OrderID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID, FreeAmount, RoomNO, CrtBy, FTZSP_Money, PType, FCrimecode, FCriminal, CrtDate, FAmount, Flag, InvoiceNo, IPAddr  ");
            strSql.Append("  from T_SHO_Order ");
            strSql.Append(" where OrderID=@OrderID");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderID", SqlDbType.Int,4)
			};
            parameters[0].Value = OrderID;


            SelfhelpOrderMgr.Model.T_SHO_Order model = new SelfhelpOrderMgr.Model.T_SHO_Order();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FreeAmount"].ToString() != "")
                {
                    model.FreeAmount = decimal.Parse(ds.Tables[0].Rows[0]["FreeAmount"].ToString());
                }
                model.RoomNO = ds.Tables[0].Rows[0]["RoomNO"].ToString();
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["FTZSP_Money"].ToString() != "")
                {
                    model.FTZSP_Money = decimal.Parse(ds.Tables[0].Rows[0]["FTZSP_Money"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                model.FCrimecode = ds.Tables[0].Rows[0]["FCrimecode"].ToString();
                model.FCriminal = ds.Tables[0].Rows[0]["FCriminal"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDate"].ToString() != "")
                {
                    model.CrtDate = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FAmount"].ToString() != "")
                {
                    model.FAmount = decimal.Parse(ds.Tables[0].Rows[0]["FAmount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.InvoiceNo = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
                model.IPAddr = ds.Tables[0].Rows[0]["IPAddr"].ToString();

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
            strSql.Append(" FROM T_SHO_Order ");
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
            strSql.Append(" FROM T_SHO_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

