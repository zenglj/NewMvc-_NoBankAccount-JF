using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class BfOrderMain : BaseModel
    {
        /// <summary>
        /// 罪犯编号
        /// </summary>
        public string FCrimeCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string FCrimeName { get; set; }

        /// <summary>
        /// 队别编号
        /// </summary>
        public string FAreaCode { get; set; }
        /// <summary>
        /// 队别名称
        /// </summary>
        public string FAreaName { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string CrtBy { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string FDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }
        /// <summary>
        /// 标志，0是为审核，1是已审核
        /// </summary>
        public string FFlag { get; set; }

    }
}