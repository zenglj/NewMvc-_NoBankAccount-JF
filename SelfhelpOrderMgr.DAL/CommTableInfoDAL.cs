using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using SelfhelpOrderMgr.Model;
using System.Text;
using Dapper;
using System.Reflection;
using SelfhelpOrderMgr.Common;

namespace SelfhelpOrderMgr.DAL
{
    public class CommTableInfoDAL
    {
        //public DataTable GetDataTable(string sql, params SqlParameter[] Paramters)
        //{
        //    DataSet ds = SqlHelper.Query(sql, Paramters);
        //    return ds.Tables[0];
        //}
        public DataTable GetDataTable(string sql)
        {
            SqlParameter[] parameters = new SqlParameter[] { };
            DataSet ds = SqlHelper.Query(sql,  parameters);
            return ds.Tables[0];
        }

        //Dapper方式返回DataTable 数据
        public DataTable GetDataTable(string sql, object p=null)
        {            
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                DataTable table = new DataTable("MyTable");
                var reader = conn.ExecuteReader(sql,p);
                table.Load(reader);
                return table;
            }
        }

        /// <summary>
        /// 执行sql返回list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<T> ExtSqlGetList<T>(string sql, object p = null)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                var result = conn.Query<T>(sql, p).ToList();
                return result;
            }
        }

        /// <summary>
        /// 执行Sql语句返回模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T ExtSqlGetModel<T>(string sql, object parameter)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var result = conn.Query<T>(sql, parameter).ToList().FirstOrDefault();
                return result;
            }
        }



        /// <summary>
        /// 获取泛型集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="connStr">数据库连接字符串</param>
        /// <param name="sqlStr">要查询的T-SQL</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string sqlStr, object parameter = null)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return (IList<T>)conn.Query<T>(sqlStr, parameter);
            }
        }

        /// <summary>
        /// DataSetToList
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataSet">数据源</param>
        /// <param name="tableIndex">需要转换表的索引</param>
        /// <returns></returns>
        public IList<T> DataSetToList<T>(DataSet dataSet, int tableIndex)
        {
            //确认参数有效
            if (dataSet == null || dataSet.Tables.Count <= 0 || tableIndex < 0)
                return null;

            DataTable dt = dataSet.Tables[tableIndex];

            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }

        public List<PeihuoDanPrintList> GetListData(string sql)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                return (List<PeihuoDanPrintList>)conn.Query<PeihuoDanPrintList>(sql);
            }        
        }

        public List<T_ICCARD_LIST> GetICCardListData(string sql)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                return (List<T_ICCARD_LIST>)conn.Query<T_ICCARD_LIST>(sql);
            }
        }

        public List<xfMingxi> GetXfMingxi(string sql)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                return (List<xfMingxi>)conn.Query<xfMingxi>(sql);
            }
        }

        public List<T_XFGSList> GetXFGSList(string sql, object param)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                List<T_XFGSList> list = (List<T_XFGSList>)conn.Query<T_XFGSList>(sql, param);
                return list;
            }
        }


        public string AddDataTableToDB(DataTable source, string TargetTableName)
        {
            SqlTransaction tran = null;//声明一个事务对象  
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
                {
                    conn.Open();//打开链接  
                    using (tran = conn.BeginTransaction())
                    {
                        using (SqlBulkCopy copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tran))
                        {
                            //TargetTableName 表名格式如："AnDequan.dbo.[User]"
                            copy.DestinationTableName = TargetTableName;  //指定服务器上目标表的名称  
                            copy.WriteToServer(source);                      //执行把DataTable中的数据写入DB  
                            tran.Commit();                                      //提交事务  
                            return "1";                                        //返回True 执行成功！  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (null != tran)
                    tran.Rollback();
                //LogHelper.Add(ex);  
                return ex.ToString();//返回False 执行失败！  
            }
        }




        //用于直接执行SQL语句
        public int ExecSql(string Sql)
        {
            try
            {
                return SqlHelper.ExecuteSql(Sql);
            }
            catch(Exception e)
            {
                string ss = e.Message;
                Console.WriteLine(ss);
                Log4NetHelper.logger.Error(ss);
                return 0;
            }            
        }

        

        
    }
}