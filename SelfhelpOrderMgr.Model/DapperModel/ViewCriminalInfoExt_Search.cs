using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class ViewCriminalInfoExt_Search : ViewCriminalInfo
    {
        public DateTime FInDate_Start { get; set; }//入监时间__开始
        public DateTime FInDate_End { get; set; }//入监时间__开始
        public DateTime FOuDate_Start { get; set; }//出监时间_开始
        public DateTime FOuDate_End { get; set; }//出监时间_结束
        public string FCode_Start { get; set; }//犯人编号_开始
        public string FCode_End { get; set; }//犯人编号_结束
    }
}