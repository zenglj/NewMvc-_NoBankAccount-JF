using Dapper;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public  class BaseDapperDAL
    {

        public  T Insert<T>(T t) where T:BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetInsertSql() + ";select @@IDENTITY;";
                //获取所有参数
                //Type type=typeof(T);                
                //var paraArray=type.GetProperties().Where(p=>!p.Name.Equals("Id")).Select(p=>new SqlParameter("@"+p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                
                //param 直接传实体进去就可以
                int _sid= SqlMapper.Query<int>(conn, sql,t).ToList()[0];//得出自增长的Id
                sql = SqlBuilder<T>.GetFindSql() + _sid;
                var list = SqlMapper.Query<T>(conn, sql).ToList();
                if (list.Count > 0)
                {
                    return list[0];//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
                }
                return null;
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

        public  bool Update<T>( T t) where T:BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql =SqlBuilder<T>.GetUpdateSql();
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, sql, t) == 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }
        }

        public bool Update<T>(T t, string strUpdateJson, string otherStrWhere = "",bool isIdFlag=true) where T : BaseModel
        {
            string _update = SetUpdateSqlAndParam<T>(strUpdateJson, otherStrWhere, isIdFlag);

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, _update, t) >= 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }

        }

        private static string SetUpdateSqlAndParam<T>(string strUpdateJson, string otherStrWhere,bool isIdFlag=true) where T : BaseModel
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
            else{
                if (!string.IsNullOrWhiteSpace(otherStrWhere))
                {
                    _update +=  otherStrWhere;
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


        public bool Update<T>(IEnumerable<T> list, string strUpdateJson, string otherStrWhere = "",bool isIdFlag=true) where T : BaseModel
        {
            string _update = SetUpdateSqlAndParam<T>(strUpdateJson, otherStrWhere,isIdFlag);

            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //Type type = typeof(T);
                //var paraArray = type.GetProperties().Select(p => new SqlParameter("@" + p.Name, p.GetValue(t) ?? DBNull.Value)).ToArray();
                return SqlMapper.Execute(conn, _update, list) >= 1;//判断更新的数量是否等于1，实际使用可以根据情况返回数组
            }
        }

        public bool Delete<T>(int id) where T:BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetDeleteSql() + id.ToString();
                return SqlMapper.Execute(conn, sql)==1;//
            }
        }


        public   T GetModel<T>( int id) where T:BaseModel
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                string sql = SqlBuilder<T>.GetFindSql() + id;
                return SqlMapper.Query<T>(conn, sql).AsList<T>()[0];//这里的【0】可以去掉，因为我这个只是返回一条记录，实际使用可以根据情况返回数组
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
        public List<T> GetModelList<T, S>( string strJsonWhere,string orderField,int topNum) where T : BaseModel
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
                            } break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            } break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            } break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            } break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            } break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            } break;
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
                        else if(p.Name.EndsWith("_In"))
                        {
                            stringWhere = SetWhereString(stringWhere, " in ", p.Name.Substring(0, p.Name.Length-3), "( " + p.Name + " )");
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
                    sql = "select top "+ topNum +@" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name
                    + @"] ) as t";
                }
                else
                {
                    sql = "select top " + topNum + @" * from
                    (
                    select ROW_NUMBER() over(order by " + orderField + ") as rowNumber,* from [" + type.Name + @"] where " + stringWhere
                    + @" ) as t ";
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
        public string GetParamString<T, S>( string strJsonWhere) where T : BaseModel
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
                            } break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            } break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            } break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            } break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            } break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            } break;
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

        public PageResult<T> GetPageList<T,S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageSize = 10,string OtherQuery="", string columnInfo = "*") where T :  BaseModel
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


        public DataTable GetPageDataTable<T, S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageSize = 10, string OtherQuery = "",string columnInfo="*") where T : BaseModel
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
                            } break;
                        case "String":
                            {
                                dbtypeInt = System.Data.DbType.String;
                            } break;
                        case "int":
                            {
                                dbtypeInt = System.Data.DbType.Int32;
                            } break;
                        case "decimal":
                            {
                                dbtypeInt = System.Data.DbType.Decimal;
                            } break;
                        case "DateTime":
                            {
                                dbtypeInt = System.Data.DbType.DateTime;
                            } break;
                        case "long":
                            {
                                dbtypeInt = System.Data.DbType.Int64;
                            } break;
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
                        if (p.Name.EndsWith("Name") || p.Name.EndsWith("Remark"))
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

        protected static string SetWhereString(string stringWhere, string strOperatorChar, string paramName,string paramValue)
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
        public int GetTableCount<T, S>( string strJsonWhere,  string OtherQuery = "") where T : BaseModel
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
                    sql = @"select isnull(sum("+ sumField + @"),0) from [" + type.Name + @"] ";
                }
                else
                {
                    sql = @"select isnull(sum(" + sumField + @"),0) from [" + type.Name + @"]  where " + stringWhere ;
                }

                List<decimal> _s = SqlMapper.Query<decimal>(conn, sql, param).AsList<decimal>();
                if (_s.Count > 0)
                {
                    return _s[0];
                }

                return 0;
                
            }
        }


        public List<R> GetSumOfFieldList<T, S,R>(string sumField,string groupField, string strJsonWhere, string OtherQuery = "") where T : BaseModel
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
                    sql = @"select "+ groupField + ", isnull(sum(" + sumField + @"),0) as sumField from [" + type.Name + @"] " + " group by " + groupField;
                }
                else
                {
                    sql = @"select "+ groupField + ", isnull(sum(" + sumField + @"), 0) as sumField from[" + type.Name + @"]   where " + stringWhere + " group by " + groupField;
                }

                List<R> _s = SqlMapper.Query<R>(conn, sql, param).AsList<R>();
                //if (_s.Count > 0)
                //{
                //    return _s;
                //}
                return _s;
            }
        }
    }
}