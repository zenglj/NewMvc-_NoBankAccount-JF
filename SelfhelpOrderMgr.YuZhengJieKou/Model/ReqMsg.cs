using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.YuZhengJieKou.Model
{
    public class ReqMsg <T>
    {
        public string msg { get; set; }
        public int code { get; set; }
        public List<T> data { get; set; }

    }
}
