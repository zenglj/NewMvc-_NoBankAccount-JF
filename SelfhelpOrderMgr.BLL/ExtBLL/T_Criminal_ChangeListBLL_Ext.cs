using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_Criminal_ChangeListBLL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model,int flag)
        {
            return dal.Add(model,flag);

        }
    }
}