using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SEQNOBLL
    {
        public string GetSeqTypeNo(string seqnoType)
        {
            return new T_SEQNODAL().GetSeqTypeNo(seqnoType);
        }
    }
}