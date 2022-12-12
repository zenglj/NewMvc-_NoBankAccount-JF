using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web.CommonHeler
{
    public class ConfigHelper
    {
        private readonly string termid = ConfigurationManager.ConnectionStrings["termid"].ConnectionString;
        private readonly string trnid = ConfigurationManager.ConnectionStrings["trnid"].ConnectionString;
        private readonly string custid = ConfigurationManager.ConnectionStrings["custid"].ConnectionString;
        private readonly string cusopr = ConfigurationManager.ConnectionStrings["cusopr"].ConnectionString;
        private readonly string token = ConfigurationManager.ConnectionStrings["token"].ConnectionString;
        private readonly string ibknum = ConfigurationManager.ConnectionStrings["ibknum"].ConnectionString;
        private readonly string actacn = ConfigurationManager.ConnectionStrings["actacn"].ConnectionString;
        private readonly string postServerUrl = ConfigurationManager.ConnectionStrings["postServerUrl"].ConnectionString;

        public YinqiSetting GetYinQiSetting()
        {
            YinqiSetting set = new YinqiSetting() {
                termid=termid,
                trnid=trnid,
                custid=custid,
                cusopr=cusopr,
                token=token,
                ibknum=ibknum,
                actacn=actacn,
                postServerUrl= postServerUrl
            };
            return set;
        }

    }

    public class YinqiSetting
    {
        public string termid { get; set; }
	    public string trnid { get; set; }
        public string custid { get; set; }
        public string cusopr { get; set; }
        public string token { get; set; }
        public string ibknum { get; set; }
        public string actacn { get; set; }
        /// <summary>
        /// 银企直连服务器的地址
        /// </summary>
        public string postServerUrl { get; set; }
    }
}