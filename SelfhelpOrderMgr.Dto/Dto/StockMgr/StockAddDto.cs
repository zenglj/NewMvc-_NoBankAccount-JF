using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Dto
{
    public class StockAddDto
    {
        public string StockId { get; set; }
        public string StockType { get; set; }
        public string Remark { get; set; }
        public DateTime InOutDate { get; set; }

    }
}
