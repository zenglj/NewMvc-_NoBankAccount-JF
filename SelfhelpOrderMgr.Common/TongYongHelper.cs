using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Common
{
    public class TongYongHelper
    {
        /// <summary>
        /// 8位文本日期转日期格式
        /// </summary>
        /// <param name="strDate">8位文本日期</param>
        /// <returns>日期格式</returns>
        public static DateTime StrToDate(string strDate)
        {
            string yyyy = strDate.Substring(0, 4); 
            string mm = strDate.Substring(4, 2); 
            string dd = strDate.Substring(6, 2); 
            //拼写符合日期格式的字符串
            string   riqi=yyyy+"-"+mm+"-"+dd;         
            //将符合日期格式的字符串转化为DateTime数据类型     
            DateTime   dt=Convert.ToDateTime(riqi);
            return dt;
        }

        /// <summary>
        /// 日期转换为文本格式的日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToString(DateTime dt)
        {
            //将符合日期格式的字符串转化为DateTime数据类型     
            string strDt = dt.ToString("yyyy-MM-dd");
            return strDt;
        }

    }
}