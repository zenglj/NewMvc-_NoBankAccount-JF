using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Model
{
    public class T_Bank_TransDetail:BaseModel
    {
      public int MainID { get; set; }
      public string statusrspCod { get; set; }
        public string statusrspmsg { get; set; }
        public string fractnibknum { get; set; }
        public string fractnactacn { get; set; }
        public string fractncardno { get; set; }
        public string fractnacntname { get; set; }
        public string fractnibkname { get; set; }
        public string toactntoibkn { get; set; }
        public string toactnactacn { get; set; }
        public string toactncardno { get; set; }
        public string toactntoname { get; set; }
        public string toactntobank { get; set; }
        public string mactibkn { get; set; }
        public string mactacn { get; set; }
        public string nactcardno { get; set; }
        public string mactname { get; set; }
        public string mactbank { get; set; }
        public string vchnum { get; set; }
        public string transid { get; set; }
        public string insid { get; set; }
        public string txndate { get; set; }
        public string txntime { get; set; }
        public decimal txnamt { get; set; }
        public decimal acctbal { get; set; }
        public decimal avlbal { get; set; }
        public decimal frzamt { get; set; }
        public decimal overdramt { get; set; }
        public decimal avloverdramt { get; set; }
        public string useinfo { get; set; }
        public string furinfo { get; set; }
        public string transtype { get; set; }
        public string bustype { get; set; }
        public string trncur { get; set; }
        public int direction { get; set; }
        public string feeact { get; set; }
        public decimal feeamt { get; set; }
        public string feecur { get; set; }
        public DateTime valdat { get; set; }
        public string vouchtp { get; set; }
        public string vouchnum { get; set; }
        public decimal fxrate { get; set; }
        public string interinfo { get; set; }
        public string reserve1 { get; set; }
        public string reserve2 { get; set; }
        public string reserve3 { get; set; }
    }
}