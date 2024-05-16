using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    class ReqResultInfo<T>
    {
        public int total { get; set; }
        public List<T> rows { get; set; }
        public string msg { get; set; }
        public int code { get; set; }

    }
}
