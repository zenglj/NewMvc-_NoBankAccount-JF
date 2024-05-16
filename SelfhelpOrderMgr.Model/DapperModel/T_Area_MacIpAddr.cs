using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Area_MacIpAddr:BaseModel
    {
        public string IpAddr { get; set; }
        public string FAreaCode { get; set; }
        public string FAreaName { get; set; }
        public int FCount { get; set; }
        public DateTime NewTime { get; set; }

    }
}