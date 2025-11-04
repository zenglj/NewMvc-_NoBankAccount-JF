using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 积分字典表
    /// </summary>
    public class T_JF_DictCode:BaseModel
    {
        public string FCode { get; set; }
        public string FName { get; set; }
        public string TypeName { get; set; }
        public string Remark { get; set; }
       public string CrtBy { get; set;  }
        public DateTime? CreateDate { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}