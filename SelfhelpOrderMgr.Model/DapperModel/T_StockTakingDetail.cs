using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_StockTakingDetail:BaseModel
    {
        public string StockTakingNo { get; set; }
        public string GCode { get; set; }
        public string GName { get; set; }
        public string GTXM { get; set; }
        public decimal Balance { get; set; }
        public decimal? RealCount { get; set; }
        public decimal? DiffCount { get; set; }
        public string WareHouseCode { get; set; }
        public string StockId { get; set; }
    }
}