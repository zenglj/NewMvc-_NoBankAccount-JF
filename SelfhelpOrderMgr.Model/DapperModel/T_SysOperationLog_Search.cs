﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_SysOperationLog_Search : T_SysOperationLog
    {
        public DateTime CrtDate_Start { get; set; }//创建时间__开始
        public DateTime CrtDate_End { get; set; }//创建时间__开始

    }
}