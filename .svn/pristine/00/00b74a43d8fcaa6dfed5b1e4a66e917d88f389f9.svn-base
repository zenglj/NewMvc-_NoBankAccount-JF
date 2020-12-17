using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_AREABLL
    {
        //按用户的监区的节点数
        public IEnumerable<T_AREA> GetAreaByNodeNames(params string[] nodes)
        {
            return new T_AREADAL().GetAreaByNodeNames(nodes);
        }
    }
}