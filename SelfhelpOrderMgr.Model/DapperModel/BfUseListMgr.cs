using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class BfUseListMgr : BaseModel
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FShopName { get; set; }

        /// <summary>
        /// 商品类型Id
        /// </summary>
        public string FTypeId { get; set; }

        
        /// <summary>
        /// 数量
        /// </summary>
        public decimal FCount { get; set; }

        /// <summary>
        /// 单位部门
        /// </summary>
        public string FUnit { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime FDate { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string CrtBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public int PcNo { get; set; }
        /// <summary>
        /// 犯人编号
        /// </summary>
        public string FCrimeCode { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public int FFlag { get; set; }
    }
}