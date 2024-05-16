using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetASRConsoleApp.Models
{
    class AsrResponeEntity
    {
        public string msgType { get; set; }
        public int msgStatus { get; set; }
        public AsrMsgBody msgBody { get; set; }



    }
}
