using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ResultInfo
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Flag { get; set; }
        /// <summary>
        /// 结果信息文字描述
        /// </summary>
        public string ReMsg { get; set; }
       
        /// <summary>
        /// 信息的结果对象
        /// </summary>
        public object DataInfo { get; set; }

    }
}