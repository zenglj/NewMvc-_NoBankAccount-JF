using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Dto
{
    public class StockQtyQueryDto
    {	
        public string GTXM { get; set; }
        public string SPShortCode { get; set; }
        public string GName { get; set; }
        public string GType { get; set; }
        public int? SaleTypeId { get; set; }

        
    }
}
