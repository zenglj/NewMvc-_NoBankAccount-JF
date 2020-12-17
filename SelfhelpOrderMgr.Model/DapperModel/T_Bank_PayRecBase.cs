using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_PayRecBase : BaseModel
    {
        public int TranType {get;set;}
        public int PayMode {get;set;}
        public string PurposeInfo {get;set;}


    }
}