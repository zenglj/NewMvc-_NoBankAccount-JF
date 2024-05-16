using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetASRConsoleApp.Models
{
    class AsrSendResponeEntity
    {
        public string msgType { get; set; }
        public string msgStatus  { get; set; }
        public AsrSendRspMsgBody msgBody { get; set; }

    }

    class AsrSendRspMsgBody
    {
        public string bizId { get; set; }
        public string uuid { get; set; }
        public string callback { get; set; }
        public string resultMsg { get; set; }
        public string notCallbackUrl { get; set; }

    }
}
