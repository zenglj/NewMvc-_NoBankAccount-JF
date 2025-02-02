﻿using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    //T_SHO_OrderDTL
    public partial class T_SHO_OrderDTLDAL
    {

        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_SHO_OrderDTL");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SHO_OrderDTL(");
            strSql.Append("FreeFlag,Remark,SPShortCode,FTZSP_TypeFlag,OrderID,GCode,GTXM,GName,GCount,GPrice,GAmount,Flag");
            strSql.Append(") values (");
            strSql.Append("@FreeFlag,@Remark,@SPShortCode,@FTZSP_TypeFlag,@OrderID,@GCode,@GTXM,@GName,@GCount,@GPrice,@GAmount,@Flag");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@FreeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderID", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCode", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GTXM", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GCount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.FreeFlag;
            parameters[1].Value = model.Remark;
            parameters[2].Value = model.SPShortCode;
            parameters[3].Value = model.FTZSP_TypeFlag;
            parameters[4].Value = model.OrderID;
            parameters[5].Value = model.GCode;
            parameters[6].Value = model.GTXM;
            parameters[7].Value = model.GName;
            parameters[8].Value = model.GCount;
            parameters[9].Value = model.GPrice;
            parameters[10].Value = model.GAmount;
            parameters[11].Value = model.Flag;

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
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SHO_OrderDTL set ");

            strSql.Append(" FreeFlag = @FreeFlag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" SPShortCode = @SPShortCode , ");
            strSql.Append(" FTZSP_TypeFlag = @FTZSP_TypeFlag , ");
            strSql.Append(" OrderID = @OrderID , ");
            strSql.Append(" GCode = @GCode , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" GName = @GName , ");
            strSql.Append(" GCount = @GCount , ");
            strSql.Append(" GPrice = @GPrice , ");
            strSql.Append(" GAmount = @GAmount , ");
            strSql.Append(" Flag = @Flag  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderID", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCode", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GTXM", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@GCount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GPrice", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@GAmount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.ID;
            parameters[1].Value = model.FreeFlag;
            parameters[2].Value = model.Remark;
            parameters[3].Value = model.SPShortCode;
            parameters[4].Value = model.FTZSP_TypeFlag;
            parameters[5].Value = model.OrderID;
            parameters[6].Value = model.GCode;
            parameters[7].Value = model.GTXM;
            parameters[8].Value = model.GName;
            parameters[9].Value = model.GCount;
            parameters[10].Value = model.GPrice;
            parameters[11].Value = model.GAmount;
            parameters[12].Value = model.Flag;
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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_OrderDTL ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_OrderDTL ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_SHO_OrderDTL GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, FreeFlag, Remark, SPShortCode, FTZSP_TypeFlag, OrderID, GCode, GTXM, GName, GCount, GPrice, GAmount, Flag  ");
            strSql.Append("  from T_SHO_OrderDTL ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            SelfhelpOrderMgr.Model.T_SHO_OrderDTL model = new SelfhelpOrderMgr.Model.T_SHO_OrderDTL();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FreeFlag"].ToString() != "")
                {
                    model.FreeFlag = int.Parse(ds.Tables[0].Rows[0]["FreeFlag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.SPShortCode = ds.Tables[0].Rows[0]["SPShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString() != "")
                {
                    model.FTZSP_TypeFlag = int.Parse(ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                model.GCode = ds.Tables[0].Rows[0]["GCode"].ToString();
                model.GTXM = ds.Tables[0].Rows[0]["GTXM"].ToString();
                model.GName = ds.Tables[0].Rows[0]["GName"].ToString();
                if (ds.Tables[0].Rows[0]["GCount"].ToString() != "")
                {
                    model.GCount = decimal.Parse(ds.Tables[0].Rows[0]["GCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GPrice"].ToString() != "")
                {
                    model.GPrice = decimal.Parse(ds.Tables[0].Rows[0]["GPrice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GAmount"].ToString() != "")
                {
                    model.GAmount = decimal.Parse(ds.Tables[0].Rows[0]["GAmount"].ToString());
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
            strSql.Append(" FROM T_SHO_OrderDTL ");
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
            strSql.Append(" FROM T_SHO_OrderDTL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

