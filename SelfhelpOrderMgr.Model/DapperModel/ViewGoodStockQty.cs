using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewGoodStockQty:BaseModel
    {
        public string GCode { get; set; }
        public string GName { get; set; }
        public string GTXM { get; set; }
        public string SPShortCode { get; set; }
        public decimal Balance { get; set; }
        public string GUnit { get; set; }
        public decimal GDJ { get; set; }
        public string GType { get; set; }
        public string TypeName { get; set; }
        public int SaleTypeId { get; set; }
        public string PType { get; set; }
    }
}