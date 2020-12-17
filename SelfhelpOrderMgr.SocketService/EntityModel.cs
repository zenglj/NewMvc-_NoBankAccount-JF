using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfhelpOrderMgr.SocketService
{
    class UserEntity
    {
        public string FCode { get; set; }

    }

    class ResultEntity
    {
        public string ResultCode { get; set; }
        public object ResultData { get; set; }
    }
    class CriminalEntity
    {
        public string FCode { get; set; }
        public string FName { get; set; }
        public int FFlag { get; set; }
        public string BankAccNo { get; set; }
        public decimal AmountA { get; set; }
        public decimal AmountB { get; set; }
        public decimal AmountC { get; set; }
        public decimal BankAmount { get; set; }
    }
}
