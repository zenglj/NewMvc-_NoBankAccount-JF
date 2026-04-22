using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Dto
{
    public class StockQueryDto
    {
        public string StockId { get; set; }
        public string StockType { get; set; }
        public DateTime? CrtDt_Start { get; set; }
        public DateTime? CrtDt_End { get; set; }
        public string Remark { get; set; }
    }
}
