using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_TransDetail_Ext: T_Bank_TransDetail
    {
        public string FName { get; set; }
        public string FCrimeCode { get; set; }
        public int? importFlag { get; set; }
    }
}