using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewGoodStockQty:BaseModel
    {
        [System.ComponentModel.Description("商品编码")]
        public string GCode { get; set; }
        [System.ComponentModel.Description("品名")]
        public string GName { get; set; }
        [System.ComponentModel.Description("条码")]
        public string GTXM { get; set; }
        [System.ComponentModel.Description("简码")]
        public string SPShortCode { get; set; }
        [System.ComponentModel.Description("库存数量")]
        public decimal Balance { get; set; }
        [System.ComponentModel.Description("单位")]
        public string GUnit { get; set; }
        [System.ComponentModel.Description("单价")]
        public decimal GDJ { get; set; }
        [System.ComponentModel.Description("类别编码")]
        public string GType { get; set; }
        [System.ComponentModel.Description("商品类别")]
        public string TypeName { get; set; }
        [System.ComponentModel.Description("消费Id")]
        public int SaleTypeId { get; set; }
        [System.ComponentModel.Description("消费类型")]
        public string PType { get; set; }
        [System.ComponentModel.Description("仓库")]
        public string WareHouseCode { get; set; }
    }
}