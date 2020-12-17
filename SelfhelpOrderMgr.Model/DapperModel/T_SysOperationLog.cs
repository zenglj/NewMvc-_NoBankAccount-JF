using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_SysOperationLog:BaseModel
    {
        public string ControlName {get;set;}
        public string ActionName {get;set;}
        public string ReqJson {get;set;}
        public string RtnJson {get;set;}
        public DateTime CrtDate { get; set; }
        public string UserCode { get; set; }
        public string Remark { get; set; }
    }
}