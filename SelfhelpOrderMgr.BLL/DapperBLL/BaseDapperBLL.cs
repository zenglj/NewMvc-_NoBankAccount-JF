using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Dto;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class BaseDapperBLL
    {
        protected readonly BaseDapperDAL dapperDal = new BaseDapperDAL();


        #region 基本增删改查



        public T Query<T>(string strWhere) where T : BaseModel
        {
            return dapperDal.Query<T>(strWhere);
        }

        public T QueryModel<T>(string fieldName, string whereValue)
        {
            return dapperDal.QueryModel<T>(fieldName, whereValue);
        }

        public T QueryModel<T>(string fieldName, string whereValue, string orderStr)
        {
            return dapperDal.QueryModel<T>(fieldName, whereValue, orderStr);
        }
        public List<T> QueryList<T>(string strWhere) where T : BaseModel
        {
            return dapperDal.QueryList<T>(strWhere);
        }

        public List<T> QueryList<T>(string sqlStr, object parameter = null)
        {
            return dapperDal.QueryList<T>(sqlStr, parameter);
        }
        public List<T> QueryList<T>(string sqlStr, int page, int pageSize, string orderField = "Id asc", object parameter = null)
        {
            return dapperDal.QueryList<T>(sqlStr, page, pageSize, orderField, parameter);
        }


        public List<T> QueryListByTableName<T>(string strWhere, object paramObj)
        {
            return dapperDal.QueryListByTableName<T>(strWhere, paramObj);
        }

        /// <summary>
        /// 核心方法：根据 DTO 自动构建查询条件
        /// </summary>
        /// <param name="tableName">数据库表名 (例如 "T_Stock")</param>
        /// <param name="dto">查询条件 DTO (例如 StockSearchDto)</param>
        /// <returns>符合条件的实体列表</returns>
        public List<T> QueryListByDto<T, S>(string tableName, S dto)
        {
            return dapperDal.QueryListByDto<T,S>(tableName, dto);
        }

        /// <summary>
        /// 简化版，直接传入 DTO 实例 (不需要指定泛型参数 S)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public List<T> QueryListByDto<T>(string tableName, T dto)
        {
            // 调用泛型方法，传入 DTO 的类型和实例
            return dapperDal.QueryListByDto<T>(tableName, dto);
        }

        /// <summary>
        /// 分页查询，根据 DTO 自动构建查询条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="dto"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderField"></param>
        /// <returns></returns>
        public PageResult<T> QueryPageListByDto<T,S>(string tableName, S dto, int pageIndex = 1, int pageSize = 10, string orderField = "", string otherWhere = "")
        {
            // 调用泛型方法，传入 DTO 的类型和实例
            return dapperDal.QueryPageListByDto<T, S>(tableName, dto, pageIndex, pageSize, orderField,otherWhere);
        }
        public PageResult<T> QueryPageListByDto<T>(string tableName, T dto, int pageIndex = 1, int pageSize = 10, string orderField = "",string otherWhere = "")
        {
            // 调用泛型方法，传入 DTO 的类型和实例
            return dapperDal.QueryPageListByDto<T, T>(tableName, dto, pageIndex, pageSize, orderField,otherWhere);
        }

        public T Insert<T>(T t) where T : BaseModel
        {
            return dapperDal.Insert<T>(t);
        }

        public bool Insert<T>(List<T> list) where T : BaseModel
        {
            return dapperDal.Insert<T>(list);
        }
        /// <summary>
        /// 更新，单个实体更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update<T>(T t) where T : BaseModel
        {
            return dapperDal.Update<T>(t);
        }

        /// <summary>
        /// 部分更新，单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="strUpdateJson"></param>
        /// <param name="otherStrWhere"></param>
        /// <returns></returns>
        public bool Update<T>(T t, string strUpdateJson, string otherStrWhere = "", bool isIdFlag = true) where T : BaseModel
        {
            return dapperDal.Update<T>(t, strUpdateJson, otherStrWhere, isIdFlag);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> list) where T : BaseModel
        {
            return dapperDal.Update<T>(list);
        }

        /// <summary>
        /// 部分更新，支持列方式表多个更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">需要更新的列表</param>
        /// <param name="strUpdateJson">部分更新的Json</param>
        /// <param name="otherStrWhere">其他查询条件</param>
        /// <returns></returns>
        public bool Update<T>(IEnumerable<T> list, string strUpdateJson, string otherStrWhere = "", bool isIdFlag = true) where T : BaseModel
        {
            return dapperDal.Update<T>(list, strUpdateJson, otherStrWhere, isIdFlag);
        }

        public bool Delete<T>(int id) where T : BaseModel
        {
            return dapperDal.Delete<T>(id);
        }

        public bool Delete<T>(List<int> idsToDelete) where T : BaseModel
        {
            return dapperDal.Delete<T>(idsToDelete);
        }

        public bool Delete<T>(string fieldName, string fieldValue) where T : BaseModel
        {
            return dapperDal.Delete<T>(fieldName, fieldValue);
        }


        public T GetModel<T>(int id) where T : BaseModel
        {
            return dapperDal.GetModel<T>(id);
        }

        public List<T> GetModelList<T>(string strJsonWhere) where T : BaseModel
        {
            return dapperDal.GetModelList<T, T>(strJsonWhere);
        }
        public List<T> GetModelList<T, S>(string strJsonWhere) where T : BaseModel
        {
            return dapperDal.GetModelList<T, S>(strJsonWhere);
        }

        public List<T> GetModelList<T>(string strJsonWhere, string orderField, int topNum = 10) where T : BaseModel
        {
            return dapperDal.GetModelList<T, T>(strJsonWhere, orderField, topNum);
        }
        public List<T> GetModelList<T, S>(string strJsonWhere, string orderField, int topNum = 10) where T : BaseModel
        {
            return dapperDal.GetModelList<T, S>(strJsonWhere, orderField, topNum);
        }

        public List<T> GetModelList<T, S>(string strJsonWhere, string orderField, int topNum = 10, string otherWhere = "") where T : BaseModel
        {
            return dapperDal.GetModelList<T, S>(strJsonWhere, orderField, topNum, otherWhere);
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
            return dapperDal.GetParamString<T, S>(strJsonWhere);
        }

        public T GetModelFirst<T>(string strJsonWhere) where T : BaseModel
        {
            return dapperDal.GetModelFirst<T, T>(strJsonWhere);
        }
        public T GetModelFirst<T, S>(string strJsonWhere) where T : BaseModel
        {
            return dapperDal.GetModelFirst<T, S>(strJsonWhere);
        }

        /// <summary>
        /// 按查询条件获取数据表相应的实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="orderField"></param>
        /// <param name="strJsonWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRows"></param>
        /// <param name="OtherQuery"></param>
        /// <returns></returns>
        public PageResult<T> GetPageList<T>(string orderField, string strJsonWhere, int pageIndex = 1, int pageRows = 10, string OtherQuery = "") where T : BaseModel
        {
            return dapperDal.GetPageList<T, T>(orderField, strJsonWhere, pageIndex, pageRows, OtherQuery);
        }

        /// <summary>
        /// 按查询条件获取数据表相应的实体数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="orderField"></param>
        /// <param name="strJsonWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRows"></param>
        /// <param name="OtherQuery"></param>
        /// <returns></returns>
        public PageResult<T> GetPageList<T, S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageRows = 10, string OtherQuery = "") where T : BaseModel
        {
            return dapperDal.GetPageList<T, S>(orderField, strJsonWhere, pageIndex, pageRows, OtherQuery);
        }

        /// <summary>
        /// 按查询条件获取数据表DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="orderField"></param>
        /// <param name="strJsonWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OtherQuery"></param>
        /// <param name="columnInfo"></param>
        /// <returns></returns>
        public DataTable GetPageDataTable<T, S>(string orderField, string strJsonWhere, int pageIndex = 1, int pageSize = 10, string OtherQuery = "", string columnInfo = "*") where T : BaseModel
        {
            return dapperDal.GetPageDataTable<T, S>(orderField, strJsonWhere, pageIndex, pageSize, OtherQuery, columnInfo);

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
            return dapperDal.GetTableCount<T, S>(strJsonWhere, OtherQuery);
        }

        public decimal GetListSumAmount<T, S>(string sumField, string strJsonWhere, string OtherQuery = "") where T : BaseModel
        {
            return dapperDal.GetListSumAmount<T, S>(sumField, strJsonWhere, OtherQuery);
        }

        public List<R> GetSumOfFieldList<T, S, R>(string sumField, string groupField, string strJsonWhere, string OtherQuery = "") where T : BaseModel
        {
            return dapperDal.GetSumOfFieldList<T, S, R>(sumField, groupField, strJsonWhere, OtherQuery);
        }


        /// <summary>
        /// 执行SQL脚本
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, object param)
        {
            return dapperDal.ExecuteSql(sql, param);
        }


        /// <summary>
        /// 将Datatable写入到数据库的指定表中
        /// </summary>
        /// <param name="source"></param>
        /// <param name="TargetTableName"></param>
        /// <returns></returns>
        public string AddDataTableToDB(DataTable source, string TargetTableName)
        {
            return dapperDal.AddDataTableToDB(source, TargetTableName);
        }

        #endregion

        /// <summary>
        /// 获取订单号Vou
        /// </summary>
        /// <returns></returns>
        public string GetVouNo()
        {
            return dapperDal.GetVouNo();
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
            return dapperDal.UpdateNumericPart<T>(dict, strwhere);
        }
        public bool UpdatePartInfo<T>(Dictionary<string, string> dict, string strwhere)
        {
            return dapperDal.UpdatePartInfo<T>(dict, strwhere);
        }

        public bool UpdatePartInfo<T>(object paramUpdateInfo, string strwhere, object paramWhere)
        {
            return dapperDal.UpdatePartInfo<T>(paramUpdateInfo, strwhere, paramWhere);
        }


        /// <summary>
        /// 部分字段更新数值
        /// </summary>
        /// <typeparam name="T">表名</typeparam>
        /// <param name="fieldName">字段名</param>
        /// <param name="objUpdates">更新的对象列表</param>
        /// <returns></returns>
        public bool UpdatePartValueInfo<T>(string changeField,string whereField, List<PartChangeDto> objUpdates)
        {
            return dapperDal.UpdatePartValueInfo<T>(changeField, whereField, objUpdates);

        }

        public string ExecuteProc(string procName, Dictionary<string, string> dict)
        {
            return dapperDal.ExecuteProc(procName, dict);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dict">参数字典</param>
        /// <param name="outputParam">OutPut输出参数</param>
        /// <returns></returns>
        public string ExecuteProcByOutput(string procName, Dictionary<string, string> dict, string outputParam)
        {
            return dapperDal.ExecuteProcByOutput(procName, dict, outputParam);
        }


        /// <summary>
        /// 创建订单Id
        /// </summary>
        /// <param name="seqnoType"></param>
        /// <returns></returns>
        public string CreateOrderId(string seqnoType)
        {
            return dapperDal.CreateOrderId(seqnoType);
        }
    }
}