using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Trans_FeeList:BaseModel
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public string AccCode { get; set; }
        /// <summary>
        /// 财务科目编号
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 财务科目名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 财务科目子编号
        /// </summary>
        public int? SubTypeId { get; set; }
    }
}