using Dapper;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.DAL
{
    public class BaseDapperDAL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public T Query<T>(string strWhere)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                Type type = typeof(T);
                string sql = $"select * from {type.Name} ";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql = sql + "where " + strWhere;
                }
                var list = SqlMapper.Query<T>(conn, sql).AsList<T>();//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return default(T);
                }
            }
        }

        /// <summary>
        /// 查询Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="whereValue"></param>
        /// <returns></returns>
        public T QueryModel<T>(string fieldName, string whereValue)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                Type type = typeof(T);
                string sql = $"select * from {type.Name} where {fieldName}=@{fieldName}";

                string strObj = "{\"" + fieldName + "\":\"" + whereValue + "\"}";

                object obj = jss.DeserializeObject(strObj);

                var list = SqlMapper.Query<T>(conn, sql, obj).AsList<T>();//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return default(T);
                }
            }
        }

        public List<T> QueryList<T>(string strWhere)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                Type type = typeof(T);
                string sql = $"select * from {type.Name} ";
                if (!string.IsNullOrWhiteSpace(strWhere))
                {
                    sql = sql + "where " + strWhere;
                }

                var list = SqlMapper.Query<T>(conn, sql).AsList<T>();//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                return list;
            }
        }

        public List<T> QueryList<T>(string sqlStr, object parameter = null)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                return conn.Query<T>(sqlStr, parameter).ToList();
            }
        }
        public List<T> QueryList<T>(string sqlStr, int page, int pageSize, string orderField = "Id asc", object parameter = null)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startRow = 1;
                int endRow = 1;
                startRow = 1 + (page - 1) * pageSize;
                endRow = page * pageSize;
                string sql = @"select * from
                    (select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from (" + sqlStr + ") b ) as t where t.rowNumber>=" + startRow.ToString() + " and t.rowNumber<=" + endRow.ToString();
                return conn.Query<T>(sql, parameter).ToList();
            }
        }

        public T QueryBySql<T>(string sql)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {

                var list = SqlMapper.Query<T>(conn, sql).AsList<T>();//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return default(T);
                }
            }
        }



        public T Insert<T>(T t) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                Type type = typeof(T);
                string sql = SqlBuilder<T>.GetInsertSql() + " select @@IDENTITY;";
                //string columnsString = string.Join(",", type.GetProperties().Select(p => @"[" + p.Name + "]"));
                //string valuesString = string.Join(",", type.GetProperties().Select(p => @"@" + p.Name + ""));
                //string strInsertSql = @"INSERT INTO [" + type.Name + "] (" + columnsString + ") VALUES(" + valuesString + ");";
                //string sql = strInsertSql ;

                //获取所有参数
                //Type type=typeof(T);                
                //var paraArray=type.GetProperties().Where(p=>!p.Name.Equals("Id")).Select(p=>new SqlParameter("@"+p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();

                //param 直接传实体进去就可以
                //int _sid = SqlMapper.Execute(conn, sql, t);//得出自增长的Id
                int _sid = SqlMapper.Query<int>(conn, sql, t).FirstOrDefault();//得出自增长的Id

                sql = SqlBuilder<T>.GetFindSql() + _sid;
                var list = SqlMapper.Query<T>(conn, sql).ToList();
                //if (list.Count > 0)
                //{
                //    return list[0];//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                //}
                //return null;

                return list.FirstOrDefault();//换行默认第一条
            }
        }

        public bool Insert<T>(List<T> list) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetInsertSql() + ";";
                //获取所有参数
                //Type type=typeof(T);                
                //var paraArray=type.GetProperties().Where(p=>!p.Name.Equals("Id")).Select(p=>new SqlParameter("@"+p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();

                //param 直接传实体进去就可以
                int _sid = SqlMapper.Execute(conn, sql, list);//得出自增长的Id

                if (_sid > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool Update<T>(T t) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetUpdateSql();
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, sql, t) == 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }
        }

        public bool Update<T>(T t, string strUpdateJson, string otherStrWhere = "", bool isIdFlag = true) where T : BaseModel
        {
            string _update = SetUpdateSqlAndParam<T>(strUpdateJson, otherStrWhere, isIdFlag);

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, _update, t) >= 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }

        }

        private static string SetUpdateSqlAndParam<T>(string strUpdateJson, string otherStrWhere, bool isIdFlag = true) where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            string stringSet = "";

            if (string.IsNullOrWhiteSpace(strUpdateJson) == false)
            {
                T s = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strUpdateJson);
                var paraList = type.GetPropertiesInJson(strUpdateJson).Where(p => !p.Name.Equals("Id")).Select(p => p)
                    .ToList();

                #region Dapper的设定参数
                foreach (var p in paraList)
                {
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);

                    if (stringSet == "")
                    {
                        stringSet = "[" + p.Name + "]" + "=@" + p.Name + "";
                    }
                    else
                    {
                        stringSet = stringSet + "," + "[" + p.Name + "]" + "=@" + p.Name + "";
                    }
                }
                #endregion

            }
            string _update = "Update [" + type.Name + "] set " + stringSet + "  where ";
            if (isIdFlag == true)
            {
                _update += " Id=@Id";
                if (!string.IsNullOrWhiteSpace(otherStrWhere))
                {
                    _update = _update + " and " + otherStrWhere;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(otherStrWhere))
                {
                    _update += otherStrWhere;
                }
            }

            return _update;
        }


        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> list) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetUpdateSql();
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, sql, list) >= 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }
        }


        public bool Update<T>(IEnumerable<T> list, string strUpdateJson, string otherStrWhere = "", bool isIdFlag = true) where T : BaseModel
        {
            string _update = SetUpdateSqlAndParam<T>(strUpdateJson, otherStrWhere, isIdFlag);

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, _update, list) >= 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }
        }

        public bool Delete<T>(int id) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetDeleteSql() + id.ToString();
                return SqlMapper.Execute(conn, sql) == 1;//
            }
        }

        public bool Delete<T>(string fieldName, string fieldValue) where T : BaseModel
        {
            Type type = typeof(T);
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = $"delete from {type.Name} where {fieldName}=@{fieldName}";
                var param = new DynamicParameters();
                param.Add($"@{fieldName}", fieldValue, DbType.String);
                return SqlMapper.Execute(conn, sql, param) == 1;//
            }
        }


        public T GetModel<T>(int id) where T : BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetFindSql() + id;
                var list = SqlMapper.Query<T>(conn, sql).AsList<T>();//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                if (list.Count > 0)
                {
                    return list[0];
                }
                else
                {
                    return null;
                }
            }
        }

        public T GetModelFirst<T, S>(string strJsonWhere) where T : BaseModel
        {
            var list = this.GetModelList<T, S>(strJsonWhere, "Id", 1);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }
        public List<T> GetModelList<T, S>(string strJsonWhere) where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            {
                S s = Newtonsoft.Json.JsonConvert.DeserializeObject<S>(strJsonWhere);
                var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
                    .ToList();

                #region Dapper的设定参数

                foreach (var p in paraList)
                {
                    //new SqlParameter("@" + p.Name + "", p.GetValue(t) ?? DBNull.Value);
                    System.Data.DbType dbtypeInt;
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);
                    switch (ptype.Name)
                    {
                        case "string":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            }
                            break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            }
                            break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            }
                            break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            }
                            break;
                        default:
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                    }
                    if (System.Data.DbType.DateTime == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else if (System.Data.DbType.String == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<=", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("Name"))
                        {
                            stringWhere = SetWhereString(stringWhere, " like ", p.Name, p.Name);
                        }
                        else if (p.Name.EndsWith("_In"))
                        {
                            stringWhere = SetWhereString(stringWhere, " in ", p.Name.Substring(0, p.Name.Length - 3), "( " + p.Name + " )");
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                }
                #endregion

                //stringWhere = string.Join(" and ", type.GetPropertiesInJson(strJsonWhere).Select(p => 
                //"" + p.Name + "=@" + p.Name + ""
                //    ));
            }
            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = null;
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    sql = "select * from [" + type.Name + @"]";
                }
                else
                {
                    sql = "select * from[" + type.Name + @"] where " + stringWhere;
                }

                return SqlMapper.Query<T>(conn, sql, param).AsList<T>();

            }
        }

        public List<T> GetModelList<T, S>(string strJsonWhere, string orderField, int topNum) where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            {
                S s = Newtonsoft.Json.JsonConvert.DeserializeObject<S>(strJsonWhere);
                var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
                    .ToList();

                #region Dapper的设定参数

                foreach (var p in paraList)
                {
                    //new SqlParameter("@" + p.Name + "", p.GetValue(t) ?? DBNull.Value);
                    System.Data.DbType dbtypeInt;
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);
                    switch (ptype.Name)
                    {
                        case "string":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            }
                            break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            }
                            break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            }
                            break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            }
                            break;
                        default:
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                    }
                    if (System.Data.DbType.DateTime == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else if (System.Data.DbType.String == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<=", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("Name"))
                        {
                            stringWhere = SetWhereString(stringWhere, " like ", p.Name, p.Name);
                        }
                        else if (p.Name.EndsWith("_In"))
                        {
                            stringWhere = SetWhereString(stringWhere, " in ", p.Name.Substring(0, p.Name.Length - 3), "( " + p.Name + " )");
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                }
                #endregion

                //stringWhere = string.Join(" and ", type.GetPropertiesInJson(strJsonWhere).Select(p => 
                //"" + p.Name + "=@" + p.Name + ""
                //    ));
            }
            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = null;
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    if (string.IsNullOrWhiteSpace(orderField))
                    {
                        sql = "select top " + topNum + @" * from [" + type.Name + "] ";
                    }
                    else
                    {
                        sql = "select top " + topNum + @" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name
                    + @"] ) as t";
                    }

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(orderField))
                    {
                        sql = "select top " + topNum + @" * from [" + type.Name + @"] where " + stringWhere;
                    }
                    else
                    {
                        sql = "select top " + topNum + @" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name + @"] where " + stringWhere
                    + @" ) as t ";
                    }

                }

                return SqlMapper.Query<T>(conn, sql, param).AsList<T>();

            }
        }

        public List<T> GetModelList<T, S>(string strJsonWhere, string orderField, int topNum,string otherWhere) where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            {
                S s = Newtonsoft.Json.JsonConvert.DeserializeObject<S>(strJsonWhere);
                var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
                    .ToList();

                #region Dapper的设定参数

                foreach (var p in paraList)
                {
                    //new SqlParameter("@" + p.Name + "", p.GetValue(t) ?? DBNull.Value);
                    System.Data.DbType dbtypeInt;
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);
                    switch (ptype.Name)
                    {
                        case "string":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            }
                            break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            }
                            break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            }
                            break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            }
                            break;
                        default:
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                    }
                    if (System.Data.DbType.DateTime == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else if (System.Data.DbType.String == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<=", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("Name"))
                        {
                            stringWhere = SetWhereString(stringWhere, " like ", p.Name, p.Name);
                        }
                        else if (p.Name.EndsWith("_In"))
                        {
                            stringWhere = SetWhereString(stringWhere, " in ", p.Name.Substring(0, p.Name.Length - 3), "( " + p.Name + " )");
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                }
                #endregion

                //stringWhere = string.Join(" and ", type.GetPropertiesInJson(strJsonWhere).Select(p => 
                //"" + p.Name + "=@" + p.Name + ""
                //    ));
            }
            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = null;
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    if (string.IsNullOrWhiteSpace(orderField))
                    {
                        
                        sql = "select top " + topNum + @" * from [" + type.Name + "] ";
                        
                    }
                    else
                    {
                        sql = "select top " + topNum + @" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name
                    + @"] ) as t";
                    }

                    if (!string.IsNullOrWhiteSpace(otherWhere))
                    {
                        sql = sql + " where " + otherWhere;
                    }

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(otherWhere))
                    {
                        otherWhere =  " and " + otherWhere;
                    }
                    else
                    {
                        otherWhere = "";
                    }

                    if (string.IsNullOrWhiteSpace(orderField))
                    {
                        sql = "select top " + topNum + @" * from [" + type.Name + @"] where " + stringWhere + otherWhere;

                        
                    }
                    else
                    {
                        sql = "select top " + topNum + @" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name + @"] where " + stringWhere + otherWhere
                    + @" ) as t ";
                    }

                }

                return SqlMapper.Query<T>(conn, sql, param).AsList<T>();

            }
        }

        /// <summary>
        /// 获取查询Where条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="strJsonWhere"></param>
        /// <returns></returns>
        public string GetParamString<T, S>(string strJsonWhere) where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            {
                S s = Newtonsoft.Json.JsonConvert.DeserializeObject<S>(strJsonWhere);
                var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
                    .ToList();

                #region Dapper的设定参数

                foreach (var p in paraList)
                {
                    System.Data.DbType dbtypeInt;
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);
                    switch (ptype.Name)
                    {
                        case "string":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            }
                            break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            }
                            break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            }
                            break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            }
                            break;
                        default:
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                    }
                    if (System.Data.DbType.DateTime == dbtypeInt)
                    {
                        //param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereValue(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), (p.GetValue(s) ?? DBNull.Value).ToString());
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereValue(stringWhere, "<", p.Name.Substring(0, p.Name.Length - 4), (p.GetValue(s) ?? DBNull.Value).ToString());
                        }
                        else
                        {
                            stringWhere = SetWhereValue(stringWhere, "=", p.Name, (p.GetValue(s) ?? DBNull.Value).ToString());
                        }
                    }
                    else
                    {
                        //param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("Name"))
                        {
                            stringWhere = SetWhereValue(stringWhere, " like ", p.Name, (p.GetValue(s) ?? DBNull.Value).ToString());
                        }
                        else
                        {
                            stringWhere = SetWhereValue(stringWhere, "=", p.Name, (p.GetValue(s) ?? DBNull.Value).ToString());
                        }
                    }
                }
                #endregion

                //stringWhere = string.Join(" and ", type.GetPropertiesInJson(strJsonWhere).Select(p => 
                //"" + p.Name + "=@" + p.Name + ""
                //    ));
            }
            return stringWhere;
        }

        public PageResult<T> GetPageList<T, S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageSize = 10, string OtherQuery = "", string columnInfo = "*") where T : BaseModel
        {
            int iStartRow = (pageIndex - 1) * pageSize + 1;//起始行
            int iEndRow = pageIndex * pageSize;//结束行

            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            //获取SQL的查询条件及参数信息
            stringWhere = GetSearchWhereAndParam<S>(strJsonWhere, param, stype, stringWhere);

            param.Add("firstData", iStartRow, System.Data.DbType.Int32);
            param.Add("endData", iEndRow, System.Data.DbType.Int32);
            #region 没有封装前的查询脚本
            //查询Sql字符串
            //string sql = null;
            //if (string.IsNullOrWhiteSpace(OtherQuery) == false)
            //{
            //    if (string.IsNullOrWhiteSpace(stringWhere))
            //    {
            //        stringWhere += OtherQuery;
            //    }
            //    else
            //    {
            //        stringWhere += " and " + OtherQuery;
            //    }
            //}
            //if (string.IsNullOrWhiteSpace(stringWhere))
            //{
            //    sql = @"select " + columnInfo + @" from
            //        (
            //        select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name
            //    + @"] ) as t where t.rowNumber>=@firstData and t.rowNumber<=@endData";
            //}
            //else
            //{
            //    sql = @"select " + columnInfo + @" from
            //        (
            //        select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name + @"] where " + stringWhere
            //    + @" ) as t
            //        where t.rowNumber>=@firstData and t.rowNumber<=@endData";
            //}
            #endregion

            //获取SQL的查询脚本
            string sql = GetSearchSqlInfo(orderField, OtherQuery, columnInfo, type, ref stringWhere);
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {

                PageResult<T> ps = new PageResult<T>();
                ps.PageIndex = pageIndex;
                ps.PageSize = pageSize;
                ps.rows = SqlMapper.Query<T>(conn, sql, param).AsList<T>();

                string sqlCount = "select count(1) from [" + type.Name + "] ";
                if (!string.IsNullOrWhiteSpace(stringWhere))
                {
                    sqlCount = "select count(1) from [" + type.Name + "] where " + stringWhere;
                }
                ps.total = SqlMapper.Query<int>(conn, sqlCount, param).ToList()[0];//总行数
                return ps;
            }
        }

        //GetSqlWhereParamInfo


        public DataTable GetPageDataTable<T, S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageSize = 10, string OtherQuery = "", string columnInfo = "*") where T : BaseModel
        {
            int iStartRow = (pageIndex - 1) * pageSize + 1;//起始行
            int iEndRow = pageIndex * pageSize;//结束行

            var param = new DynamicParameters();

            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            stringWhere = GetSearchWhereAndParam<S>(strJsonWhere, param, stype, stringWhere);
            param.Add("firstData", iStartRow, System.Data.DbType.Int32);
            param.Add("endData", iEndRow, System.Data.DbType.Int32);
            //获取SQL的查询脚本
            string sql = GetSearchSqlInfo(orderField, OtherQuery, columnInfo, type, ref stringWhere);
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                DataTable table = new DataTable("MyTable");
                IDataReader reader = SqlMapper.ExecuteReader(conn, sql, param);
                table.Load(reader);
                return table;
            }
        }

        /// <summary>
        /// 获取SQL的查询脚本
        /// </summary>
        /// <param name="orderField"></param>
        /// <param name="OtherQuery"></param>
        /// <param name="columnInfo"></param>
        /// <param name="type"></param>
        /// <param name="stringWhere"></param>
        /// <returns></returns>
        private static string GetSearchSqlInfo(string orderField, string OtherQuery, string columnInfo, Type type, ref string stringWhere)
        {
            //查询Sql字符串
            string sql;
            if (string.IsNullOrWhiteSpace(OtherQuery) == false)
            {
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    stringWhere += OtherQuery;
                }
                else
                {
                    stringWhere += " and " + OtherQuery;
                }
            }
            if (string.IsNullOrWhiteSpace(stringWhere))
            {
                sql = @"select " + columnInfo + @" from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name
                + @"] ) as t where t.rowNumber>=@firstData and t.rowNumber<=@endData";
            }
            else
            {
                sql = @"select " + columnInfo + @" from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name + @"] where " + stringWhere
                + @" ) as t
                    where t.rowNumber>=@firstData and t.rowNumber<=@endData";
            }

            return sql;
        }


        /// <summary>
        /// 获取查询条件的SQL语句及参数
        /// </summary>
        /// <typeparam name="S">查询实体的泛型</typeparam>
        /// <param name="strJsonWhere">查询Json</param>
        /// <param name="param">参数</param>
        /// <param name="stype"></param>
        /// <param name="stringWhere"></param>
        /// <returns></returns>
        private static string GetSearchWhereAndParam<S>(string strJsonWhere, DynamicParameters param, Type stype, string stringWhere)
        {
            if (string.IsNullOrWhiteSpace(strJsonWhere) == false)
            {
                S s = Newtonsoft.Json.JsonConvert.DeserializeObject<S>(strJsonWhere);
                var paraList = stype.GetPropertiesInJson(strJsonWhere).Select(p => p)
                    .ToList();

                #region Dapper的设定参数

                foreach (var p in paraList)
                {
                    //new SqlParameter("@" + p.Name + "", p.GetValue(t) ?? DBNull.Value);
                    System.Data.DbType dbtypeInt;
                    var value = p.GetValue(s, null);//用pi.GetValue获得值
                    var ptype = value.GetType() ?? typeof(object);
                    switch (ptype.Name)
                    {
                        case "string":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            }
                            break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            }
                            break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            }
                            break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            }
                            break;
                        default:
                            {
                                dbtypeInt = System.Data.DbType.String;
                            }
                            break;
                    }
                    if (System.Data.DbType.DateTime == dbtypeInt)
                    {
                        param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                        if (p.Name.EndsWith("_Start"))
                        {
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            stringWhere = SetWhereString(stringWhere, "<", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }
                    }
                    else
                    {
                        if (p.Name.EndsWith("Name") || p.Name.EndsWith("Remark") || p.Name.EndsWith("Info") || p.Name.EndsWith("Desc") || p.Name.EndsWith("Json"))
                        {
                            string strlike = null;
                            if (p.GetValue(s) != null)
                            {
                                strlike = "%" + p.GetValue(s).ToString() + "%";
                                param.Add(p.Name, strlike, dbtypeInt);
                            }
                            else
                            {
                                param.Add(p.Name, DBNull.Value, dbtypeInt);
                            }
                            stringWhere = SetWhereString(stringWhere, " like ", p.Name, p.Name);
                        }
                        else if (p.Name.EndsWith("_Start"))
                        {
                            param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                            stringWhere = SetWhereString(stringWhere, ">=", p.Name.Substring(0, p.Name.Length - 6), p.Name);
                        }
                        else if (p.Name.EndsWith("_End"))
                        {
                            param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                            stringWhere = SetWhereString(stringWhere, "<=", p.Name.Substring(0, p.Name.Length - 4), p.Name);
                        }
                        else
                        {
                            param.Add(p.Name, p.GetValue(s) ?? DBNull.Value, dbtypeInt);
                            stringWhere = SetWhereString(stringWhere, "=", p.Name, p.Name);
                        }



                    }
                }
                #endregion

                //stringWhere = string.Join(" and ", type.GetPropertiesInJson(strJsonWhere).Select(p => 
                //"" + p.Name + "=@" + p.Name + ""
                //    ));
            }
            return stringWhere;
        }

        protected static string SetWhereString(string stringWhere, string strOperatorChar, string paramName, string paramValue)
        {
            if (string.IsNullOrWhiteSpace(stringWhere))
            {
                stringWhere = "" + paramName + strOperatorChar + "@" + paramValue + "";
            }
            else
            {
                stringWhere = stringWhere + " and " + paramName + strOperatorChar + "@" + paramValue + "";
            }
            return stringWhere;
        }

        protected static string SetWhereValue(string stringWhere, string strOperatorChar, string paramName, string paramValue)
        {
            if (string.IsNullOrWhiteSpace(stringWhere))
            {
                stringWhere = "" + paramName + strOperatorChar + "'" + paramValue + "'";
            }
            else
            {
                stringWhere = stringWhere + " and " + paramName + strOperatorChar + "'" + paramValue + "'";
            }
            return stringWhere;
        }

        /// <summary>
        /// 获取实体表的记录数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="strJsonWhere"></param>
        /// <param name="OtherQuery"></param>
        /// <returns></returns>
        public int GetTableCount<T, S>(string strJsonWhere, string OtherQuery = "") where T : BaseModel
        {
            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            stringWhere = GetSearchWhereAndParam<S>(strJsonWhere, param, stype, stringWhere);

            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {

                if (string.IsNullOrWhiteSpace(OtherQuery) == false)
                {
                    if (string.IsNullOrWhiteSpace(stringWhere))
                    {
                        stringWhere += OtherQuery;
                    }
                    else
                    {
                        stringWhere += " and " + OtherQuery;
                    }
                }
                string sqlCount = "select count(1) from [" + type.Name + "] ";
                if (!string.IsNullOrWhiteSpace(stringWhere))
                {
                    sqlCount = "select count(1) from [" + type.Name + "] where " + stringWhere;
                }
                int total = SqlMapper.Query<int>(conn, sqlCount, param).ToList()[0];//总行数
                return total;
            }
        }


        /// <summary>
        /// 根据查询条件获取字段的汇总金额
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="sumField"></param>
        /// <param name="strJsonWhere"></param>
        /// <param name="OtherQuery"></param>
        /// <returns></returns>
        public decimal GetListSumAmount<T, S>(string sumField, string strJsonWhere, string OtherQuery = "") where T : BaseModel
        {

            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            stringWhere = GetSearchWhereAndParam<S>(strJsonWhere, param, stype, stringWhere);

            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = null;


                if (string.IsNullOrWhiteSpace(OtherQuery) == false)
                {
                    if (string.IsNullOrWhiteSpace(stringWhere))
                    {
                        stringWhere += OtherQuery;
                    }
                    else
                    {
                        stringWhere += " and " + OtherQuery;
                    }
                }
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    sql = @"select isnull(sum(" + sumField + @"),0) from [" + type.Name + @"] ";
                }
                else
                {
                    sql = @"select isnull(sum(" + sumField + @"),0) from [" + type.Name + @"]  where " + stringWhere;
                }

                List<decimal> _s = SqlMapper.Query<decimal>(conn, sql, param).AsList<decimal>();
                if (_s.Count > 0)
                {
                    return _s[0];
                }

                return 0;

            }
        }


        public List<R> GetSumOfFieldList<T, S, R>(string sumField, string groupField, string strJsonWhere, string OtherQuery = "") where T : BaseModel
        {

            var param = new DynamicParameters();
            //获取查询参数列表
            Type type = typeof(T);
            Type stype = typeof(S);
            string stringWhere = "";
            stringWhere = GetSearchWhereAndParam<S>(strJsonWhere, param, stype, stringWhere);

            //查询Sql字符串
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = null;

                if (string.IsNullOrWhiteSpace(OtherQuery) == false)
                {
                    if (string.IsNullOrWhiteSpace(stringWhere))
                    {
                        stringWhere += OtherQuery;
                    }
                    else
                    {
                        stringWhere += " and " + OtherQuery;
                    }
                }
                if (string.IsNullOrWhiteSpace(stringWhere))
                {
                    sql = @"select " + groupField + ", isnull(sum(" + sumField + @"),0) as sumField from [" + type.Name + @"] " + " group by " + groupField;
                }
                else
                {
                    sql = @"select " + groupField + ", isnull(sum(" + sumField + @"), 0) as sumField from[" + type.Name + @"]   where " + stringWhere + " group by " + groupField;
                }

                List<R> _s = SqlMapper.Query<R>(conn, sql, param).AsList<R>();
                //if (_s.Count > 0)
                //{
                //    return _s;
                //}
                return _s;
            }
        }


        public int ExecuteSql(string sql, object param)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int _s = SqlMapper.Execute(conn, sql, param, null, 60);
                return _s;

            }
        }

        /// <summary>
        /// 把DataTable中的数据写入DB数据库中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="TargetTableName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <returns></returns>
        public string GetVouNo()
        {
            string ipaddr = System.Web.HttpContext.Current.Request.UserHostAddress;
            string cc = ".";
            string[] ips = ipaddr.Split(cc.ToCharArray());
            //string ipLastCode = string.Format("000", ips[3]);
            string ipLastCode = "001";
            if (ips.Length > 3)
            {
                ipLastCode = "000" + ips[3];
                ipLastCode = ipLastCode.Substring(ipLastCode.Length - 3);
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @VOUNO varchar(30);");
            strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
            strSql.Append("select @VOUNO='VOU'+@VOUNO + @IpLastCode;");
            strSql.Append("select @VOUNO;");

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var strVou = SqlMapper.Query<string>(conn, strSql.ToString(), new { IpLastCode = ipLastCode }).ToList();
                return strVou[0];

            }

        }

        /// <summary>
        /// 更新部分金额字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="strwhere"></param>
        /// <returns></returns>
        public bool UpdateNumericPart<T>(Dictionary<string, decimal> dict, string strwhere)
        {

            Type type = typeof(T);
            string strSql = $"Update {type.Name} set ";
            string strSetInfo = "";
            foreach (var item in dict)
            {
                if (string.IsNullOrWhiteSpace(strSetInfo))
                {
                    strSetInfo = $" {item.Key}={item.Value} ";
                }
                else
                {
                    strSetInfo = $"{strSetInfo} ,{item.Key}={item.Value} ";

                }
            }
            strSql = strSql + strSetInfo + " where " + strwhere;
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var irow = SqlMapper.Execute(conn, strSql.ToString());
                return irow == 1;

            }
        }

        public bool UpdatePartInfo<T>(Dictionary<string, string> dict, string strwhere)
        {

            Type type = typeof(T);
            string strSql = $"Update {type.Name} set ";
            string strSetInfo = "";
            foreach (var item in dict)
            {
                if (string.IsNullOrWhiteSpace(strSetInfo))
                {
                    strSetInfo = $" {item.Key}='{item.Value}' ";
                }
                else
                {
                    strSetInfo = strSetInfo+ $" ,{item.Key}='{item.Value}' ";

                }
            }
            strSql = strSql + strSetInfo + " where " + strwhere;
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var irow = SqlMapper.Execute(conn, strSql.ToString());
                return irow == 1;

            }
        }

        public bool UpdatePartInfo<T>(object paramUpdateInfo, string strwhere,object paramWhere)
        {
            Type type = typeof(T);

            var param = new DynamicParameters();
            //param.Add($"@{fieldName}", fieldValue, DbType.String);
            //return SqlMapper.Execute(conn, sql, param) == 1;//

            Type objUpdateType = paramUpdateInfo.GetType();
            PropertyInfo[] properties = objUpdateType.GetProperties();

            Type objParamWhereType = paramWhere.GetType();
            PropertyInfo[] whereProperties = objParamWhereType.GetProperties();


            string strSql = $"Update {type.Name} set ";
            string strSetInfo = "";
            //更新参数
            foreach (PropertyInfo property in properties)
            {
                //Console.WriteLine("Field Name: " + property.Name);
                //Console.WriteLine("Field Value: " + property.GetValue(dynamicObj, null));

                if (string.IsNullOrWhiteSpace(strSetInfo))
                {
                    strSetInfo = $" {property.Name}=@{property.Name} ";
                }
                else
                {
                    strSetInfo = strSetInfo + $" ,{property.Name}=@{property.Name} ";
                }
                param.Add($"@{property.Name}", property.GetValue(paramUpdateInfo, null));
            }


            strSql = strSql + strSetInfo + " where " + strwhere;

            //条件参数
            foreach (PropertyInfo whereProy in whereProperties)
            {

                param.Add($"@{whereProy.Name}", whereProy.GetValue(paramWhere, null));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var irow = SqlMapper.Execute(conn, strSql.ToString(),param);
                return irow == 1;

            }
        }

        /// <summary>
        /// 执行带输出的存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="dict"></param>
        /// <returns></returns>
        public string ExecuteProc(string procName, Dictionary<string, string> dict)
        {
            DynamicParameters dp = new DynamicParameters();
            foreach (var item in dict)
            {
                dp.Add($"@{item.Key}", item.Value, DbType.String, ParameterDirection.Input, 100);
            }
            dp.Add("@result", "", DbType.String, ParameterDirection.Output, 100);

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var irow = conn.Execute(procName, dp, null, null, CommandType.StoredProcedure);
                string result = dp.Get<string>("@result");
                return result;
            }
        }

        /// <summary>
        /// 创建订单Id
        /// </summary>
        /// <param name="seqnoType"></param>
        /// <returns></returns>
        public string CreateOrderId(string seqnoType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("declare @VOUNO varchar(30);");
            strSql.Append($"exec  CREATESEQNO  @seqnoType,1,@vouno output;");
            strSql.Append($"select @VOUNO=@seqnoType+@VOUNO;");
            strSql.Append($"select @VOUNO;");
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                var strVou = SqlMapper.Query<string>(conn, strSql.ToString(), new { seqnoType = seqnoType }).ToList();
                return strVou[0];

            }
        }
    }
}