using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetASRConsoleApp.Models
{
    class AsrMessage
    {
        public AsrMsgState state { get; set; }
        public string aid { get; set; }
        public List<AsrLattice> lattices { get; set; }
    }
}
