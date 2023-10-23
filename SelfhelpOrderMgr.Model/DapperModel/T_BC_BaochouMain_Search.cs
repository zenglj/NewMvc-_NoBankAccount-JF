using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_BC_BaochouMain_Search: T_BC_BaochouMain
    {
        public DateTime CreateDate_Start { get; set; }
        public DateTime CreateDate_End { get; set; }
    }
}