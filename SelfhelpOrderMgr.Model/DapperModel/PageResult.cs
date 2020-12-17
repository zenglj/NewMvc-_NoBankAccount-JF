using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class PageResult<T>
    {
        public int total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> rows { get; set; }
    }
}