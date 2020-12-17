using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_GoodsType
    public partial class T_GoodsTypeDAL
    {

        public bool Exists(string Fcode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_GoodsType");
            strSql.Append(" where ");
            strSql.Append(" Fcode = @Fcode  ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcode", SqlDbType.VarChar,20)			};
            parameters[0].Value = Fcode;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_GoodsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_GoodsType(");
            strSql.Append("Fcode,CtrlMode,Fname,flag,Remark,SaleTypeId,LevelNo,FTypeCode,FTZSP_TypeFlag,MaxBuyCount");
            strSql.Append(") values (");
            strSql.Append("@Fcode,@CtrlMode,@Fname,@flag,@Remark,@SaleTypeId,@LevelNo,@FTypeCode,@FTZSP_TypeFlag,@MaxBuyCount");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CtrlMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@Fname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@SaleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@LevelNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTypeCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@MaxBuyCount", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Fcode;
            parameters[1].Value = model.CtrlMode;
            parameters[2].Value = model.Fname;
            parameters[3].Value = model.flag;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.SaleTypeId;
            parameters[6].Value = model.LevelNo;
            parameters[7].Value = model.FTypeCode;
            parameters[8].Value = model.FTZSP_TypeFlag;
            parameters[9].Value = model.MaxBuyCount;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_GoodsType model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_GoodsType set ");

            strSql.Append(" Fcode = @Fcode , ");
            strSql.Append(" CtrlMode = @CtrlMode , ");
            strSql.Append(" Fname = @Fname , ");
            strSql.Append(" flag = @flag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" SaleTypeId = @SaleTypeId , ");
            strSql.Append(" LevelNo = @LevelNo , ");
            strSql.Append(" FTypeCode = @FTypeCode , ");
            strSql.Append(" FTZSP_TypeFlag = @FTZSP_TypeFlag , ");
            strSql.Append(" MaxBuyCount = @MaxBuyCount  ");
            strSql.Append(" where Fcode=@Fcode  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@Fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CtrlMode", SqlDbType.Int,4) ,            
                        new SqlParameter("@Fname", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@SaleTypeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@LevelNo", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTypeCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@MaxBuyCount", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.Fcode;
            parameters[1].Value = model.CtrlMode;
            parameters[2].Value = model.Fname;
            parameters[3].Value = model.flag;
            parameters[4].Value = model.Remark;
            parameters[5].Value = model.SaleTypeId;
            parameters[6].Value = model.LevelNo;
            parameters[7].Value = model.FTypeCode;
            parameters[8].Value = model.FTZSP_TypeFlag;
            parameters[9].Value = model.MaxBuyCount;
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
        public bool Delete(string Fcode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_GoodsType ");
            strSql.Append(" where Fcode=@Fcode ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcode", SqlDbType.VarChar,20)			};
            parameters[0].Value = Fcode;


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
        public SelfhelpOrderMgr.Model.T_GoodsType GetModel(string Fcode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Fcode, CtrlMode, Fname, flag, Remark, SaleTypeId, LevelNo, FTypeCode, FTZSP_TypeFlag, MaxBuyCount  ");
            strSql.Append("  from T_GoodsType ");
            strSql.Append(" where Fcode=@Fcode ");
            SqlParameter[] parameters = {
					new SqlParameter("@Fcode", SqlDbType.VarChar,20)			};
            parameters[0].Value = Fcode;


            SelfhelpOrderMgr.Model.T_GoodsType model = new SelfhelpOrderMgr.Model.T_GoodsType();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Fcode = ds.Tables[0].Rows[0]["Fcode"].ToString();
                if (ds.Tables[0].Rows[0]["CtrlMode"].ToString() != "")
                {
                    model.CtrlMode = int.Parse(ds.Tables[0].Rows[0]["CtrlMode"].ToString());
                }
                model.Fname = ds.Tables[0].Rows[0]["Fname"].ToString();
                if (ds.Tables[0].Rows[0]["flag"].ToString() != "")
                {
                    model.flag = int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["SaleTypeId"].ToString() != "")
                {
                    model.SaleTypeId = int.Parse(ds.Tables[0].Rows[0]["SaleTypeId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LevelNo"].ToString() != "")
                {
                    model.LevelNo = int.Parse(ds.Tables[0].Rows[0]["LevelNo"].ToString());
                }
                model.FTypeCode = ds.Tables[0].Rows[0]["FTypeCode"].ToString();
                if (ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString() != "")
                {
                    model.FTZSP_TypeFlag = int.Parse(ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MaxBuyCount"].ToString() != "")
                {
                    model.MaxBuyCount = int.Parse(ds.Tables[0].Rows[0]["MaxBuyCount"].ToString());
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
            strSql.Append(" FROM T_GoodsType ");
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
            strSql.Append(" FROM T_GoodsType ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

