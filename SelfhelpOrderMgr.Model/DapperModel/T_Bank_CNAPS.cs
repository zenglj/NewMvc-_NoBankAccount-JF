using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_CNAPS:BaseModel
    {
        /// <summary>
        /// 开户行编号
        /// </summary>
        public string CNAPS { get; set; }

        /// <summary>
        /// 开户行网点
        /// </summary>
        public string BankOpenName { get; set; }

        
        /// <summary>
        /// 地址
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// 银行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区/县
        /// </summary>
        public string County { get; set; }
    }
}