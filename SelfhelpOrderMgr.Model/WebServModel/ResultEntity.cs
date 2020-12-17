using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelfhelpOrderMgr.Model
{
    public class ResultEntity
    {
        private string _reMsg;

        public string ReMsg
        {
            get { return _reMsg; }
            set { _reMsg = value; }
        }
        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

    }
}
