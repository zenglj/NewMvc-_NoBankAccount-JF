using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class EasyUiPageResult<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
        public decimal sumMoney { get; set; }
    }
}