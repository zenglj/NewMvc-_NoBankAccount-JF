using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_Goods_ChangeListBLL
    {
        public int Add(SelfhelpOrderMgr.Model.T_Goods_ChangeList model, int flag)
        {
            return new T_Goods_ChangeListDAL().Add(model, flag);
        }
    }
}