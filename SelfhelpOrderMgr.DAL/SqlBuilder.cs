using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public class SqlBuilder<T> where T : BaseModel
    {
        private static string _FindSql = null;
        private static string _InsertSql = null;
        private static string _DeleteSql = null;
        private static string _UpdateSql = null;
        static SqlBuilder()
        {
            Type type = typeof(T);
            {
                string columnsString = string.Join(",", type.GetProperties().Select(p => @"["+p.Name+"]"));
                _FindSql = @"SELECT "+ columnsString +" FROM ["+ type.Name +"] WHERE ID= ";
            }
            {
                string columnsString = string.Join(",", type.GetProperties().Where(p=>!p.Name.Equals("Id")).Select(p => @"["+ p.Name +"]"));
                string valuesString = string.Join(",", type.GetProperties().Where(p => !p.Name.Equals("Id")).Select(p => @"@" + p.Name + ""));
                _InsertSql = @"INSERT INTO ["+ type.Name +"] ("+ columnsString +") VALUES("+ valuesString +");";

            }
            {
                string columnsString = string.Join(",", type.GetProperties().Where(p => !p.Name.Equals("Id")).Select(p => @"[" + p.Name + "]"));
                string valuesString = string.Join(",", type.GetProperties().Where(p => !p.Name.Equals("Id")).Select(p => @"@" + p.Name + ""));
                _DeleteSql = @"Delete From [" + type.Name + "] where Id=  ";

            }
            {
                string columnsString = string.Join(",", type.GetProperties().Where(p => !p.Name.Equals("Id")).Select(p => @"[" + p.Name + "]=@"+p.Name+""));
                _UpdateSql = @"Update [" + type.Name + "] set " + columnsString + "  where Id=@Id ";

            }
        }
        /// <summary>
        /// 以Id= 结尾，可以直接添加参数
        /// </summary>
        /// <returns></returns>
        public static string GetFindSql()
        {
            return _FindSql;
        }
        public static string GetInsertSql()
        {
            return _InsertSql;
        }
        public static string GetDeleteSql()
        {
            return _DeleteSql;
        }
        public static string GetUpdateSql()
        {
            return _UpdateSql;
        }        
    }
}