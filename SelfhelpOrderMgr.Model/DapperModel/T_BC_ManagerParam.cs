using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    /// <summary>
    /// 报酬管理参数表
    /// </summary>
    public class T_BC_ManagerParam : BaseModel
    {
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string FCode { get; set; }
        public string FName { get; set; }
        public string MgrValue { get; set; }
        public string RatioValue { get; set; }
        public string AreaStart { get; set; }
        public string AreaEnd { get; set; }
        public string Remark { get; set; }
    }
}