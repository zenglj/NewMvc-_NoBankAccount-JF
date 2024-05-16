using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetASRConsoleApp.Models
{
    class AsrMsgBody
    {
        public string id { get; set; }
        public string bizId { get; set; }
        public string lang { get; set; }
        public string callback { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
        public string delUriLis { get; set; }
        public string props { get; set; }
        public int status { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }       
    }
}
