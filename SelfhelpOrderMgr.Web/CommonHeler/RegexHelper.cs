using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;//正则表达式
using SelfhelpOrderMgr.Model;
using SelfhelpOrderMgr.BLL;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public static class RegexHelper
    {
        /// <summary>
        /// 正则验证密码的复杂度
        /// </summary>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public static string  RegexPasswordCheck(string newPwd)
        {
            // 判断密码长度不小于8位
            if (string.IsNullOrEmpty(newPwd) || newPwd.Length < 8)
            {
                return ("密码长度最少要8位");
            }

            // 统计包含的字符种类：小写字母、大写字母、数字、特殊字符
            int charTypes = 0;
            if (Regex.IsMatch(newPwd, @"[a-z]")) charTypes++;       // 小写字母
            if (Regex.IsMatch(newPwd, @"[A-Z]")) charTypes++;       // 大写字母
            if (Regex.IsMatch(newPwd, @"[0-9]")) charTypes++;       // 数字
            if (Regex.IsMatch(newPwd, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?`~]")) charTypes++; // 特殊字符

            // 至少包含上述四种中的三种
            if (charTypes < 3)
            {
                return ("密码需至少包含小写字母、大写字母、数字、特殊字符中的三种");
            }

            return "";
        }

        public static string RegexIpAddressCheck(string ipAddress)
        {
            //ipAddress = "192.168.20.20";
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("AccessIpAddress");
            if (mset == null || mset.KeyMode != 1)
                return "";

            //string pattern = @"192\\.168\\.2\\.(2((5[0-5])|([0-4]\\d)))|([0-1]?\\d{1,2})";

            string pattern = mset.MgrValue;

            //if (!Regex.IsMatch(ipAddress, pattern)) //判断是否包含数字
            //    return ($"您登录IP地址{ipAddress}未授权，请与管理员联系！");

            string ss = new Regex(pattern).Match(ipAddress).Value;

            if (!Regex.IsMatch(ipAddress, pattern)) //判断是否包含数字
                return ($"您登录IP地址{ipAddress}未授权，请与管理员联系！");

            return "";

        }
    }
}