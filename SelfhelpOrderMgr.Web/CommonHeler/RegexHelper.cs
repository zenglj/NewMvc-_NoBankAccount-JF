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
            string pattern = @"^[0-9]+$";

            if (Regex.IsMatch(newPwd, pattern))
            {
                return ("密码不能全是数字");
            }

            string pattern1 = @"^[A-Z]+$";

            if (Regex.IsMatch(newPwd, pattern1))
            {
                return ("密码不能全是大写字母");
            }

            string pattern2 = @"^[a-z]+$";
            if (Regex.IsMatch(newPwd, pattern2))
            {
                return ("密码不能全是小写字母");
            }

            string pattern3 = @"^.{8,20}$";
            if (!Regex.IsMatch(newPwd, pattern3)) //判断是否包含数字
                return ("密码长度最少要8位");

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