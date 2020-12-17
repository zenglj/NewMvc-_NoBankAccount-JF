using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Savetype
    public partial class T_SavetypeDAL
    {

        public bool Exists(int fcode, int typeflag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Savetype");
            strSql.Append(" where ");
            strSql.Append(" fcode = @fcode and  ");
            strSql.Append(" typeflag = @typeflag  ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.Int,4),
					new SqlParameter("@typeflag", SqlDbType.Int,4)			};
            parameters[0].Value = fcode;
            parameters[1].Value = typeflag;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Savetype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Savetype(");
            strSql.Append("fcode,fname,typeflag,PLXE_Flag,ZZKK_Flag,AccType,FuShuFlag");
            strSql.Append(") values (");
            strSql.Append("@fcode,@fname,@typeflag,@PLXE_Flag,@ZZKK_Flag,@AccType,@FuShuFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PLXE_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ZZKK_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@FuShuFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.fcode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.typeflag;
            parameters[3].Value = model.PLXE_Flag;
            parameters[4].Value = model.ZZKK_Flag;
            parameters[5].Value = model.AccType;
            parameters[6].Value = model.FuShuFlag;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Savetype model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Savetype set ");

            strSql.Append(" fcode = @fcode , ");
            strSql.Append(" fname = @fname , ");
            strSql.Append(" typeflag = @typeflag , ");
            strSql.Append(" PLXE_Flag = @PLXE_Flag , ");
            strSql.Append(" ZZKK_Flag = @ZZKK_Flag , ");
            strSql.Append(" AccType = @AccType , ");
            strSql.Append(" FuShuFlag = @FuShuFlag  ");
            strSql.Append(" where fcode=@fcode and typeflag=@typeflag  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@PLXE_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@ZZKK_Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccType", SqlDbType.Int,4) ,            
                        new SqlParameter("@FuShuFlag", SqlDbType.Int,4)             
              
            };

            parameters[0].Value = model.fcode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.typeflag;
            parameters[3].Value = model.PLXE_Flag;
            parameters[4].Value = model.ZZKK_Flag;
            parameters[5].Value = model.AccType;
            parameters[6].Value = model.FuShuFlag;
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
        public bool Delete(int fcode, int typeflag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Savetype ");
            strSql.Append(" where fcode=@fcode and typeflag=@typeflag ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.Int,4),
					new SqlParameter("@typeflag", SqlDbType.Int,4)			};
            parameters[0].Value = fcode;
            parameters[1].Value = typeflag;


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
        public SelfhelpOrderMgr.Model.T_Savetype GetModel(int fcode, int typeflag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fcode, fname, typeflag, PLXE_Flag, ZZKK_Flag, AccType, FuShuFlag  ");
            strSql.Append("  from T_Savetype ");
            strSql.Append(" where fcode=@fcode and typeflag=@typeflag ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.Int,4),
					new SqlParameter("@typeflag", SqlDbType.Int,4)			};
            parameters[0].Value = fcode;
            parameters[1].Value = typeflag;


            SelfhelpOrderMgr.Model.T_Savetype model = new SelfhelpOrderMgr.Model.T_Savetype();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["fcode"].ToString() != "")
                {
                    model.fcode = int.Parse(ds.Tables[0].Rows[0]["fcode"].ToString());
                }
                model.fname = ds.Tables[0].Rows[0]["fname"].ToString();
                if (ds.Tables[0].Rows[0]["typeflag"].ToString() != "")
                {
                    model.typeflag = int.Parse(ds.Tables[0].Rows[0]["typeflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PLXE_Flag"].ToString() != "")
                {
                    model.PLXE_Flag = int.Parse(ds.Tables[0].Rows[0]["PLXE_Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZZKK_Flag"].ToString() != "")
                {
                    model.ZZKK_Flag = int.Parse(ds.Tables[0].Rows[0]["ZZKK_Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AccType"].ToString() != "")
                {
                    model.AccType = int.Parse(ds.Tables[0].Rows[0]["AccType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FuShuFlag"].ToString() != "")
                {
                    model.FuShuFlag = int.Parse(ds.Tables[0].Rows[0]["FuShuFlag"].ToString());
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
            strSql.Append(" FROM T_Savetype ");
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
            strSql.Append(" FROM T_Savetype ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

