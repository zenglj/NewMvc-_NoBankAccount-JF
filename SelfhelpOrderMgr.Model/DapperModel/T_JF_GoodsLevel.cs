using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_JF_GoodsLevel:BaseModel
    {
        public string LevelName { get; set; }
        public decimal CompletionRate { get; set; }
        public int UseType { get; set; }
        public string Remark { get; set; }
        public DateTime CreateDate { get; set; }
        public string CrtBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModBy { get; set; }

    }
}