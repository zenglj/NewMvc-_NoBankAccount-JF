using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //AC_SUBYR
    public partial class AC_SUBYRDAL
    {

        public bool Exists(string AccPeriod)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AC_SUBYR");
            strSql.Append(" where ");
            strSql.Append(" AccPeriod = @AccPeriod  ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccPeriod", SqlDbType.VarChar,10)			};
            parameters[0].Value = AccPeriod;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.AC_SUBYR model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AC_SUBYR(");
            strSql.Append("AccYear,CrtBy,CrtDt,ModBy,ModDt,AccPeriod,FromDt,ToDt,LockDt,CloseDt,Status,AccStatus,TransFlag");
            strSql.Append(") values (");
            strSql.Append("@AccYear,@CrtBy,@CrtDt,@ModBy,@ModDt,@AccPeriod,@FromDt,@ToDt,@LockDt,@CloseDt,@Status,@AccStatus,@TransFlag");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AccYear", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@CrtBy", SqlDbType.Char,10) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModBy", SqlDbType.Char,10) ,            
                        new SqlParameter("@ModDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@AccPeriod", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FromDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ToDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@LockDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CloseDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@Status", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@AccStatus", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@TransFlag", SqlDbType.Char,1)             
              
            };

            parameters[0].Value = model.AccYear;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDt;
            parameters[3].Value = model.ModBy;
            parameters[4].Value = model.ModDt;
            parameters[5].Value = model.AccPeriod;
            parameters[6].Value = model.FromDt;
            parameters[7].Value = model.ToDt;
            parameters[8].Value = model.LockDt;
            parameters[9].Value = model.CloseDt;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.AccStatus;
            parameters[12].Value = model.TransFlag;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.AC_SUBYR model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AC_SUBYR set ");

            strSql.Append(" AccYear = @AccYear , ");
            strSql.Append(" CrtBy = @CrtBy , ");
            strSql.Append(" CrtDt = @CrtDt , ");
            strSql.Append(" ModBy = @ModBy , ");
            strSql.Append(" ModDt = @ModDt , ");
            strSql.Append(" AccPeriod = @AccPeriod , ");
            strSql.Append(" FromDt = @FromDt , ");
            strSql.Append(" ToDt = @ToDt , ");
            strSql.Append(" LockDt = @LockDt , ");
            strSql.Append(" CloseDt = @CloseDt , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" AccStatus = @AccStatus , ");
            strSql.Append(" TransFlag = @TransFlag  ");
            strSql.Append(" where AccPeriod=@AccPeriod  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@AccYear", SqlDbType.VarChar,4) ,            
                        new SqlParameter("@CrtBy", SqlDbType.Char,10) ,            
                        new SqlParameter("@CrtDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ModBy", SqlDbType.Char,10) ,            
                        new SqlParameter("@ModDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@AccPeriod", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FromDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@ToDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@LockDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@CloseDt", SqlDbType.DateTime) ,            
                        new SqlParameter("@Status", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@AccStatus", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@TransFlag", SqlDbType.Char,1)             
              
            };

            parameters[0].Value = model.AccYear;
            parameters[1].Value = model.CrtBy;
            parameters[2].Value = model.CrtDt;
            parameters[3].Value = model.ModBy;
            parameters[4].Value = model.ModDt;
            parameters[5].Value = model.AccPeriod;
            parameters[6].Value = model.FromDt;
            parameters[7].Value = model.ToDt;
            parameters[8].Value = model.LockDt;
            parameters[9].Value = model.CloseDt;
            parameters[10].Value = model.Status;
            parameters[11].Value = model.AccStatus;
            parameters[12].Value = model.TransFlag;
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
        public bool Delete(string AccPeriod)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from AC_SUBYR ");
            strSql.Append(" where AccPeriod=@AccPeriod ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccPeriod", SqlDbType.VarChar,10)			};
            parameters[0].Value = AccPeriod;


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
        public SelfhelpOrderMgr.Model.AC_SUBYR GetModel(string AccPeriod)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AccYear, CrtBy, CrtDt, ModBy, ModDt, AccPeriod, FromDt, ToDt, LockDt, CloseDt, Status, AccStatus, TransFlag  ");
            strSql.Append("  from AC_SUBYR ");
            strSql.Append(" where AccPeriod=@AccPeriod ");
            SqlParameter[] parameters = {
					new SqlParameter("@AccPeriod", SqlDbType.VarChar,10)			};
            parameters[0].Value = AccPeriod;


            SelfhelpOrderMgr.Model.AC_SUBYR model = new SelfhelpOrderMgr.Model.AC_SUBYR();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.AccYear = ds.Tables[0].Rows[0]["AccYear"].ToString();
                model.CrtBy = ds.Tables[0].Rows[0]["CrtBy"].ToString();
                if (ds.Tables[0].Rows[0]["CrtDt"].ToString() != "")
                {
                    model.CrtDt = DateTime.Parse(ds.Tables[0].Rows[0]["CrtDt"].ToString());
                }
                model.ModBy = ds.Tables[0].Rows[0]["ModBy"].ToString();
                if (ds.Tables[0].Rows[0]["ModDt"].ToString() != "")
                {
                    model.ModDt = DateTime.Parse(ds.Tables[0].Rows[0]["ModDt"].ToString());
                }
                model.AccPeriod = ds.Tables[0].Rows[0]["AccPeriod"].ToString();
                if (ds.Tables[0].Rows[0]["FromDt"].ToString() != "")
                {
                    model.FromDt = DateTime.Parse(ds.Tables[0].Rows[0]["FromDt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ToDt"].ToString() != "")
                {
                    model.ToDt = DateTime.Parse(ds.Tables[0].Rows[0]["ToDt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LockDt"].ToString() != "")
                {
                    model.LockDt = DateTime.Parse(ds.Tables[0].Rows[0]["LockDt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CloseDt"].ToString() != "")
                {
                    model.CloseDt = DateTime.Parse(ds.Tables[0].Rows[0]["CloseDt"].ToString());
                }
                model.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                model.AccStatus = ds.Tables[0].Rows[0]["AccStatus"].ToString();
                model.TransFlag = ds.Tables[0].Rows[0]["TransFlag"].ToString();

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
            strSql.Append(" FROM AC_SUBYR ");
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
            strSql.Append(" FROM AC_SUBYR ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

