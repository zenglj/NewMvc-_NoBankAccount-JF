using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //T_TempLeavePrison
    public partial class T_TempLeavePrisonDAL
    {

        public bool Exists(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_TempLeavePrison");
            strSql.Append(" where ");
            strSql.Append(" fcode = @fcode and  ");
            strSql.Append(" fname = @fname and  ");
            strSql.Append(" foudate = @foudate and  ");
            strSql.Append(" fareaname = @fareaname and  ");
            strSql.Append(" FStatus = @FStatus and  ");
            strSql.Append(" JSMoney = @JSMoney  ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@foudate", SqlDbType.DateTime),
					new SqlParameter("@fareaname", SqlDbType.VarChar,60),
					new SqlParameter("@FStatus", SqlDbType.VarChar,50),
					new SqlParameter("@JSMoney", SqlDbType.Decimal,9)			};
            parameters[0].Value = fcode;
            parameters[1].Value = fname;
            parameters[2].Value = foudate;
            parameters[3].Value = fareaname;
            parameters[4].Value = FStatus;
            parameters[5].Value = JSMoney;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_TempLeavePrison model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_TempLeavePrison(");
            strSql.Append("fcode,fname,foudate,fareaname,FStatus,JSMoney");
            strSql.Append(") values (");
            strSql.Append("@fcode,@fname,@foudate,@fareaname,@FStatus,@JSMoney");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@foudate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fareaname", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@FStatus", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JSMoney", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.fcode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.foudate;
            parameters[3].Value = model.fareaname;
            parameters[4].Value = model.FStatus;
            parameters[5].Value = model.JSMoney;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_TempLeavePrison model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_TempLeavePrison set ");

            strSql.Append(" fcode = @fcode , ");
            strSql.Append(" fname = @fname , ");
            strSql.Append(" foudate = @foudate , ");
            strSql.Append(" fareaname = @fareaname , ");
            strSql.Append(" FStatus = @FStatus , ");
            strSql.Append(" JSMoney = @JSMoney  ");
            strSql.Append(" where fcode=@fcode and fname=@fname and foudate=@foudate and fareaname=@fareaname and FStatus=@FStatus and JSMoney=@JSMoney  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@foudate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fareaname", SqlDbType.VarChar,60) ,            
                        new SqlParameter("@FStatus", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@JSMoney", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.fcode;
            parameters[1].Value = model.fname;
            parameters[2].Value = model.foudate;
            parameters[3].Value = model.fareaname;
            parameters[4].Value = model.FStatus;
            parameters[5].Value = model.JSMoney;
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
        public bool Delete(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_TempLeavePrison ");
            strSql.Append(" where fcode=@fcode and fname=@fname and foudate=@foudate and fareaname=@fareaname and FStatus=@FStatus and JSMoney=@JSMoney ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@foudate", SqlDbType.DateTime),
					new SqlParameter("@fareaname", SqlDbType.VarChar,60),
					new SqlParameter("@FStatus", SqlDbType.VarChar,50),
					new SqlParameter("@JSMoney", SqlDbType.Decimal,9)			};
            parameters[0].Value = fcode;
            parameters[1].Value = fname;
            parameters[2].Value = foudate;
            parameters[3].Value = fareaname;
            parameters[4].Value = FStatus;
            parameters[5].Value = JSMoney;


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
        public SelfhelpOrderMgr.Model.T_TempLeavePrison GetModel(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select fcode, fname, foudate, fareaname, FStatus, JSMoney  ");
            strSql.Append("  from T_TempLeavePrison ");
            strSql.Append(" where fcode=@fcode and fname=@fname and foudate=@foudate and fareaname=@fareaname and FStatus=@FStatus and JSMoney=@JSMoney ");
            SqlParameter[] parameters = {
					new SqlParameter("@fcode", SqlDbType.VarChar,20),
					new SqlParameter("@fname", SqlDbType.VarChar,20),
					new SqlParameter("@foudate", SqlDbType.DateTime),
					new SqlParameter("@fareaname", SqlDbType.VarChar,60),
					new SqlParameter("@FStatus", SqlDbType.VarChar,50),
					new SqlParameter("@JSMoney", SqlDbType.Decimal,9)			};
            parameters[0].Value = fcode;
            parameters[1].Value = fname;
            parameters[2].Value = foudate;
            parameters[3].Value = fareaname;
            parameters[4].Value = FStatus;
            parameters[5].Value = JSMoney;


            SelfhelpOrderMgr.Model.T_TempLeavePrison model = new SelfhelpOrderMgr.Model.T_TempLeavePrison();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.fcode = ds.Tables[0].Rows[0]["fcode"].ToString();
                model.fname = ds.Tables[0].Rows[0]["fname"].ToString();
                if (ds.Tables[0].Rows[0]["foudate"].ToString() != "")
                {
                    model.foudate = DateTime.Parse(ds.Tables[0].Rows[0]["foudate"].ToString());
                }
                model.fareaname = ds.Tables[0].Rows[0]["fareaname"].ToString();
                model.FStatus = ds.Tables[0].Rows[0]["FStatus"].ToString();
                if (ds.Tables[0].Rows[0]["JSMoney"].ToString() != "")
                {
                    model.JSMoney = decimal.Parse(ds.Tables[0].Rows[0]["JSMoney"].ToString());
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
            strSql.Append(" FROM T_TempLeavePrison ");
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
            strSql.Append(" FROM T_TempLeavePrison ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

