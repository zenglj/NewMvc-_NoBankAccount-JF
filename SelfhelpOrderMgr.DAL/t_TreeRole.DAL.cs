using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    //t_TreeRole
    public partial class t_TreeRoleDAL
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.t_TreeRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_TreeRole(");
            strSql.Append("RoleID,RoleName");
            strSql.Append(") values (");
            strSql.Append("@RoleID,@RoleName");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RoleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleName", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.t_TreeRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_TreeRole set ");

            strSql.Append(" RoleID = @RoleID , ");
            strSql.Append(" RoleName = @RoleName  ");
            strSql.Append(" where RoleID=@RoleID  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@RoleID", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoleName", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
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
        public bool Delete(int RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_TreeRole ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)			};
            parameters[0].Value = RoleID;


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
        public SelfhelpOrderMgr.Model.t_TreeRole GetModel(int RoleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select RoleID, RoleName  ");
            strSql.Append("  from t_TreeRole ");
            strSql.Append(" where RoleID=@RoleID ");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)			};
            parameters[0].Value = RoleID;


            SelfhelpOrderMgr.Model.t_TreeRole model = new SelfhelpOrderMgr.Model.t_TreeRole();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                model.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();

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
            strSql.Append("select RoleID, RoleName  ");
            strSql.Append(" FROM t_TreeRole ");
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
            strSql.Append(" RoleID, RoleName  ");
            strSql.Append(" FROM t_TreeRole ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

