using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public partial class T_CZY
    {
        public t_TreeRole TreeRole { get; set; }
        public int ErrCount { get; set; }//错误次数
        public DateTime LastLoginTime { get; set; }//最后登录时间
        public DateTime PwdUpdateTime { get; set; }//最后一次修改时间

    }
}