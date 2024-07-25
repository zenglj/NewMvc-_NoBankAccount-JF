using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_AREA
    public partial class T_AREADAL
    {

        public bool Exists(string FCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_AREA");
            strSql.Append(" where ");
            strSql.Append(" FCode = @FCode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,3)			};
            parameters[0].Value = FCode;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_AREA model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_AREA(");
            strSql.Append("FCode,FName,ID,FID,URL,FTZSP_Money,SaleCloseFlag");
            strSql.Append(") values (");
            strSql.Append("@FCode,@FName,@ID,@FID,@URL,@FTZSP_Money,@SaleCloseFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@ID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@URL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SaleCloseFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FName;
            parameters[2].Value = model.ID;
            parameters[3].Value = model.FID;
            parameters[4].Value = model.URL;
            parameters[5].Value = model.FTZSP_Money;
            parameters[6].Value = model.SaleCloseFlag;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_AREA model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_AREA set ");

            strSql.Append(" FCode = @FCode , ");
            strSql.Append(" FName = @FName , ");
            strSql.Append(" ID = @ID , ");
            strSql.Append(" FID = @FID , ");
            strSql.Append(" URL = @URL , ");
            strSql.Append(" FTZSP_Money = @FTZSP_Money , ");
            strSql.Append(" SaleCloseFlag = @SaleCloseFlag , ");
            strSql.Append(" JiFenCloseFlag = @JiFenCloseFlag  ");
            strSql.Append(" where FCode=@FCode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@ID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FID", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@URL", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@SaleCloseFlag", SqlDbType.Int,4),
                        new SqlParameter("@JiFenCloseFlag", SqlDbType.Int,4)

            };

            parameters[0].Value = model.FCode;
            parameters[1].Value = model.FName;
            parameters[2].Value = model.ID;
            parameters[3].Value = model.FID;
            parameters[4].Value = model.URL;
            parameters[5].Value = model.FTZSP_Money;
            parameters[6].Value = model.SaleCloseFlag;
            parameters[7].Value = model.JiFenCloseFlag;
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
        public bool Delete(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_AREA ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,3)			};
            parameters[0].Value = FCode;


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
        public SelfhelpOrderMgr.Model.T_AREA GetModel(string FCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FCode, FName, ID, FID, URL, FTZSP_Money, SaleCloseFlag ,JiFenCloseFlag ");
            strSql.Append("  from T_AREA ");
            strSql.Append(" where FCode=@FCode ");
            SqlParameter[] parameters = {
					new SqlParameter("@FCode", SqlDbType.VarChar,20)			};
            parameters[0].Value = FCode;


            SelfhelpOrderMgr.Model.T_AREA model = new SelfhelpOrderMgr.Model.T_AREA();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.FCode = ds.Tables[0].Rows[0]["FCode"].ToString();
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                model.ID = ds.Tables[0].Rows[0]["ID"].ToString();
                model.FID = ds.Tables[0].Rows[0]["FID"].ToString();
                model.URL = ds.Tables[0].Rows[0]["URL"].ToString();
                if (ds.Tables[0].Rows[0]["FTZSP_Money"].ToString() != "")
                {
                    model.FTZSP_Money = decimal.Parse(ds.Tables[0].Rows[0]["FTZSP_Money"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SaleCloseFlag"].ToString() != "")
                {
                    model.SaleCloseFlag = int.Parse(ds.Tables[0].Rows[0]["SaleCloseFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JiFenCloseFlag"].ToString() != "")
                {
                    model.JiFenCloseFlag = int.Parse(ds.Tables[0].Rows[0]["JiFenCloseFlag"].ToString());
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
            strSql.Append(" FROM T_AREA ");
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
            strSql.Append(" FROM T_AREA ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

