using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetASRConsoleApp.Models
{
    class AsrLattice
    {
        public int lid { get; set; }
        public int spk { get; set; }
        public int begin { get; set; }
        public int end { get; set; }
        public decimal sc { get; set; }
        public string onebest { get; set; }


    }
}
