using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_CY_ChinaFestival
    public partial class T_CY_ChinaFestivalDAL
    {

        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_CY_ChinaFestival");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_CY_ChinaFestival model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_CY_ChinaFestival(");
            strSql.Append("FName,FDate,Festival_Name,Remark");
            strSql.Append(") values (");
            strSql.Append("@FName,@FDate,@Festival_Name,@Remark");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Festival_Name", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.FName;
            parameters[1].Value = model.FDate;
            parameters[2].Value = model.Festival_Name;
            parameters[3].Value = model.Remark;

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
        public bool Update(SelfhelpOrderMgr.Model.T_CY_ChinaFestival model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_CY_ChinaFestival set ");

            strSql.Append(" FName = @FName , ");
            strSql.Append(" FDate = @FDate , ");
            strSql.Append(" Festival_Name = @Festival_Name , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Festival_Name", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };

            parameters[0].Value = model.id;
            parameters[1].Value = model.FName;
            parameters[2].Value = model.FDate;
            parameters[3].Value = model.Festival_Name;
            parameters[4].Value = model.Remark;
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
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_CY_ChinaFestival ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


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
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_CY_ChinaFestival ");
            strSql.Append(" where ID in (" + idlist + ")  ");
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
        public SelfhelpOrderMgr.Model.T_CY_ChinaFestival GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id, FName, FDate, Festival_Name, Remark  ");
            strSql.Append("  from T_CY_ChinaFestival ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;


            SelfhelpOrderMgr.Model.T_CY_ChinaFestival model = new SelfhelpOrderMgr.Model.T_CY_ChinaFestival();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.FName = ds.Tables[0].Rows[0]["FName"].ToString();
                if (ds.Tables[0].Rows[0]["FDate"].ToString() != "")
                {
                    model.FDate = DateTime.Parse(ds.Tables[0].Rows[0]["FDate"].ToString());
                }
                model.Festival_Name = ds.Tables[0].Rows[0]["Festival_Name"].ToString();
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();

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
            strSql.Append(" FROM T_CY_ChinaFestival ");
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
            strSql.Append(" FROM T_CY_ChinaFestival ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

