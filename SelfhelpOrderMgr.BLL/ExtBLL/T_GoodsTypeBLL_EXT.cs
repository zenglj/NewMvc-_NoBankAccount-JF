using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_GoodsTypeBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IEnumerable<T_GoodsType> GetListOfIEnumerable(string strWhere)
        {
            return new T_GoodsTypeDAL().GetListOfIEnumerable(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public IEnumerable<T_GoodsType> GetListOfIEnumerable(int Top, string strWhere, string filedOrder)
        {
            return new T_GoodsTypeDAL().GetListOfIEnumerable(Top, strWhere, filedOrder);
        }
    }
}