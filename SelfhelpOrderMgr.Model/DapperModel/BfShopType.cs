using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class BfShopType : BaseModel
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string FName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string FUnit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string FRemark { get; set; }
        
    }
}