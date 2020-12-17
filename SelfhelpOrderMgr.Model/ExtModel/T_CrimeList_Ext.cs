using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_CrimeList
    {
        public string FCode { get; set; }
        public string FName { get; set; }
        public string FSex { get; set; }
        public string FAddr { get; set; }
        public string FCYCode { get; set; }
        public string FAreaCode { get; set; }
        public string fflag { get; set; }
        public string flimitflag { get; set; }
        public decimal? flimitamt { get; set; }
    }
}