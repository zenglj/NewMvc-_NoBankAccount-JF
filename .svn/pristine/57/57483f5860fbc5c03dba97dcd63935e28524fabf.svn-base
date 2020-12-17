using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //t_XFQueryList
    public partial class t_XFQueryListDAL
    {

        public bool Exists(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_XFQueryList");
            strSql.Append(" where ");
            strSql.Append(" fcrimecode = @fcrimecode and  ");
            strSql.Append(" fname = @fname and  ");
            strSql.Append(" CDate = @CDate and  ");
            strSql.Append(" Cmoney = @Cmoney and  ");
            strSql.Append(" Dtype = @Dtype  ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcrimecode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@CDate", SqlDbType.DateTime),
					new SqlParameter("@Cmoney", SqlDbType.Decimal,17),
					new SqlParameter("@Dtype", SqlDbType.VarChar,20)			};
            parameters[0].Value = fcrimecode;
            parameters[1].Value = fname;
            parameters[2].Value = CDate;
            parameters[3].Value = Cmoney;
            parameters[4].Value = Dtype;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.t_XFQueryList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_XFQueryList(");
            strSql.Append("fcrimecode,fname,CDate,Cmoney,Dtype");
            strSql.Append(") values (");
            strSql.Append("@fcrimecode,@fname,@CDate,@Cmoney,@Dtype");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Cmoney", SqlDbType.Decimal,17) ,            
                        new SqlParameter("@Dtype", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.fcrimecode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.CDate;
            parameters[3].Value = model.Cmoney;
            parameters[4].Value = model.Dtype;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.t_XFQueryList model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_XFQueryList set ");

            strSql.Append(" fcrimecode = @fcrimecode , ");
            strSql.Append(" fname = @fname , ");
            strSql.Append(" CDate = @CDate , ");
            strSql.Append(" Cmoney = @Cmoney , ");
            strSql.Append(" Dtype = @Dtype  ");
            strSql.Append(" where fcrimecode=@fcrimecode and fname=@fname and CDate=@CDate and Cmoney=@Cmoney and Dtype=@Dtype  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Cmoney", SqlDbType.Decimal,17) ,            
                        new SqlParameter("@Dtype", SqlDbType.VarChar,20)             
              
            };

            parameters[0].Value = model.fcrimecode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.CDate;
            parameters[3].Value = model.Cmoney;
            parameters[4].Value = model.Dtype;
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
        public bool Delete(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_XFQueryList ");
            strSql.Append(" where fcrimecode=@fcrimecode and fname=@fname and CDate=@CDate and Cmoney=@Cmoney and Dtype=@Dtype ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcrimecode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@CDate", SqlDbType.DateTime),
					new SqlParameter("@Cmoney", SqlDbType.Decimal,17),
					new SqlParameter("@Dtype", SqlDbType.VarChar,20)			};
            parameters[0].Value = fcrimecode;
            parameters[1].Value = fname;
            parameters[2].Value = CDate;
            parameters[3].Value = Cmoney;
            parameters[4].Value = Dtype;


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
        public SelfhelpOrderMgr.Model.t_XFQueryList GetModel(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fcrimecode, fname, CDate, Cmoney, Dtype  ");
            strSql.Append("  from t_XFQueryList ");
            strSql.Append(" where fcrimecode=@fcrimecode and fname=@fname and CDate=@CDate and Cmoney=@Cmoney and Dtype=@Dtype ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcrimecode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@CDate", SqlDbType.DateTime),
					new SqlParameter("@Cmoney", SqlDbType.Decimal,17),
					new SqlParameter("@Dtype", SqlDbType.VarChar,20)			};
            parameters[0].Value = fcrimecode;
            parameters[1].Value = fname;
            parameters[2].Value = CDate;
            parameters[3].Value = Cmoney;
            parameters[4].Value = Dtype;


            SelfhelpOrderMgr.Model.t_XFQueryList model = new SelfhelpOrderMgr.Model.t_XFQueryList();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.fcrimecode = ds.Tables[0].Rows[0]["fcrimecode"].ToString();
                model.fname = ds.Tables[0].Rows[0]["fname"].ToString();
                if (ds.Tables[0].Rows[0]["CDate"].ToString() != "")
                {
                    model.CDate = DateTime.Parse(ds.Tables[0].Rows[0]["CDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cmoney"].ToString() != "")
                {
                    model.Cmoney = decimal.Parse(ds.Tables[0].Rows[0]["Cmoney"].ToString());
                }
                model.Dtype = ds.Tables[0].Rows[0]["Dtype"].ToString();

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
            strSql.Append(" FROM t_XFQueryList ");
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
            strSql.Append(" FROM t_XFQueryList ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

