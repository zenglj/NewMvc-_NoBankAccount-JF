using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class CommTableInfoBLL
    {
        public DataTable GetDataTable(string sql)
        {
            return new CommTableInfoDAL().GetDataTable(sql);
        }

        public DataTable GetDataTable(string sql,object p=null)
        {
            return new CommTableInfoDAL().GetDataTable(sql, p);            
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
            return (List <T>) new CommTableInfoDAL().ExtSqlGetList<T>(sql, p);
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
            return new CommTableInfoDAL().ExtSqlGetModel<T>(sql,parameter);
        }


        /// <summary>
        /// 获取泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IList<T> GetList<T>(string sqlStr, object parameter)
        {
            return new CommTableInfoDAL().GetList<T>(sqlStr, parameter);
        }

        public List<T_ICCARD_LIST> GetICCardListData(string sql)
        {
            return new CommTableInfoDAL().GetICCardListData(sql);
        }
        public List<PeihuoDanPrintList> GetListData(string sql)
        {
            return new CommTableInfoDAL().GetListData(sql);
        }

        public List<xfMingxi> GetXfMingxi(string sql)
        {
            return new CommTableInfoDAL().GetXfMingxi(sql);
        }

        public List<T_XFGSList> GetXFGSList(string sql, object param)
        {
            return new CommTableInfoDAL().GetXFGSList(sql, param);
        }

        public string AddDataTableToDB(DataTable source, string TargetTableName)
        {
            return new CommTableInfoDAL().AddDataTableToDB(source, TargetTableName);
        }

        public int ExecSql(string Sql)
        {
            return new CommTableInfoDAL().ExecSql(Sql);
        }

        public bool InsertListToDB<T>(List<T> list) where T : BaseModel
        {
            return new BaseDapperDAL().Insert<T>(list);
        }

        #region 通过方法的封装
        /// <summary>
        /// 判断用户是否有犯人的管理权限
        /// </summary>
        /// <param name="fcode"></param>
        /// <param name="loginUserCode"></param>
        /// <returns></returns>
        public bool CheckUserManagerrPower(string fcode, string loginUserCode)
        {
            //验证是否具有管辖权
            string strsql = "";
            strsql = "select b.fcode,b.fname,isnull(b.fflag,0) as fflag from t_czy_area a,T_CRIMINAL b where a.fareacode=b.FAreaCode and b.FCode=@fcrimecode and a.fcode = @loginCode";
            DataTable dtUser = this.GetDataTable(strsql, new { fcrimecode = fcode, loginCode = loginUserCode });
            if (dtUser.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        } 
        #endregion
    }
}