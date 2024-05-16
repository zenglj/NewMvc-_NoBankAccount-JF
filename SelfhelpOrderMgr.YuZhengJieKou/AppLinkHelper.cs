using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.YuZhengJieKou
{
    public class AppLinkHelper
    {
        //System.Configuration.ConfigurationSettings.AppSettings["connectionstring"];
        private readonly static string apiUrl = ConfigurationSettings.AppSettings["ApiUrl"];
        private readonly static string apiUser = ConfigurationSettings.AppSettings["ApiUser"];
        private readonly static string apiPwd = ConfigurationSettings.AppSettings["ApiPwd"];
        private readonly static string zfzt = ConfigurationSettings.AppSettings["zfzt"];
        private readonly static string searchDicts = ConfigurationSettings.AppSettings["searchDicts"];
        private readonly static string pageSize = ConfigurationSettings.AppSettings["pageSize"];

        //获取apiUrl
        public static string GetUrl()
        {
            return apiUrl;
        }

        //获取apiUser
        public static string GetUser()
        {
            return apiUser;
        }

        //获取apiPwd
        public static string GetPwd()
        {
            return apiPwd;
        }
        //罪犯状态
        public static string GetZfzt()
        {
            return zfzt;
        }

        //字典类型码
        public static string GetDicts()
        {
            return searchDicts;
        }

        //字典类型码
        public static string GetPageSize()
        {
            return pageSize;
        }
    }


    
}