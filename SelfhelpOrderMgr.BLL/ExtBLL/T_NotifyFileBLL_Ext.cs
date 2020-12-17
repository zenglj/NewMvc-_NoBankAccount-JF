using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_NotifyFileBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>

        public IEnumerable<T_NotifyFile> GetPageListOfIEnumerable(int stratId,int page, int pageRow, string strWhere)
        {
            return new T_NotifyFileDAL().GetPageListOfIEnumerable(stratId,page, pageRow, strWhere);
        }
    }
}