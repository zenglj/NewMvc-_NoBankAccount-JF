using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public static class FilterExtend
    {

        /// <summary>
        /// 就是通过json字符串找出更新的字段
        /// </summary>
        /// <param name="type"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetPropertiesInJson(this Type type, string json)
        {
            return type.GetProperties().Where(p => json.Contains("'"+ p.Name +"':") || json.Contains("\""+ p.Name +"\":")).Select(p=>p);
        }
    }
}