using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.Dto
{
    public class StockAddPostDto
    {
        public StockAddDto stock { get; set; }
        public List<StockDetailAddDto> details { get; set; }
    }
}
