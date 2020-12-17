using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.Common
{
    public class JsonHelper
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public object ToEntity<T>(string strText)
        {
            object rtn = jss.Deserialize<T>(strText);
            return rtn;
        }

        public string ToJson(object obj)
        {
            string rtn = jss.Serialize(obj);
            return rtn;
        }

    }
}