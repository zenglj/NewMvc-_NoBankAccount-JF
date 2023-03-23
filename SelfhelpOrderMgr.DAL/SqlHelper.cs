using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
//using SqlDataBase;



//using System.Collections;

//using System.Web.Services;
//using System.Web.Services.Protocols;

//using System.Web.Script.Serialization;
//using System.Collections.Generic;

namespace SelfhelpOrderMgr.DAL
{

    class  SqlHelper
    {
        //public static string connstr =
        //    ConfigurationManager.ConnectionStrings["PrisonConnectionString"].ConnectionString;// + SqlDataBase.strDbName + ConfigurationManager.ConnectionStrings["DUserPwd"].ConnectionString;

        private readonly static string connstr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private readonly static string connPrec = ConfigurationManager.ConnectionStrings["connPrec"].ConnectionString;
        public readonly static string checkStock = ConfigurationManager.ConnectionStrings["checkStock"].ConnectionString;


        public static string getConnstr()
        {
            //Cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings.ToString());
            //string dd = ConnectionString;
            //string LoginDbName = context.Request.Cookies["person_Users"]["sysLoginName"];
            return connstr;
        }
        public static string getConnstr(string dbname)
        {
            //Cn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings.ToString());
            //string dd = ConnectionString;
            //string LoginDbName = context.Request.Cookies["person_Users"]["sysLoginName"];
            return connPrec + dbname;
        }

        /// <summary>
        /// 执行插入insert,删除delete,更新update等
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public  int ExcuteNonQuery(string dbname, string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr(dbname);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    var ss= cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return ss;
                }
            }
        }

        /// <summary>
        /// 返回单行的查询结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public  object ExcuteScalar(string dbname,string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr(dbname);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    var ss= cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return ss;
                }
            }
        }

        /// <summary>
        /// 返回多条记录的数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public DataTable ExcuteDataTable(string dbname, string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr(dbname);
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    SqlDataAdapter dp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dp.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds.Tables[0];
                }
            }
        }


        /// <summary>
        /// 执行插入insert,删除delete,更新update等,用于动软代码生成器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public static int ExecuteSql( string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    var ss= cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return ss;
                }
            }
        }

        /// <summary>
        /// 返回单行的查询结果,用于动软代码生成器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public static object GetSingle( string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    var ss= cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return ss;
                }
            }
        }

        /// <summary>
        /// 返回多条记录的数据集,用于动软代码生成器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public static DataSet Query(string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //cmd.Parameters.Clear();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    SqlDataAdapter dp = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    dp.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
        }

        /// <summary>
        /// 判断某条记录是否存在,用于动软代码生成器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Paramters"></param>
        /// <returns></returns>
        public static bool Exists( string sql, params SqlParameter[] Paramters)
        {
            string connstr = getConnstr();
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(Paramters);
                    object i= cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return Convert.ToInt32(i) > 0;
                }
            }
        }

        public static object ToDbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            else
            {
                return value;
            }
        }

        public static object FromDbNull(object value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return value;
            }
        }
    }
}
