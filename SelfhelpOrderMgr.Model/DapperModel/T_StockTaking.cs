using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_StockTaking:BaseModel
    {
        public string StockTakingNo { get; set; }
        public string WareHouseCode { get; set; }
        public DateTime CrtDate { get; set; }
        public int CheckFlag { get; set; }
        public string Remark { get; set; }
    }
}