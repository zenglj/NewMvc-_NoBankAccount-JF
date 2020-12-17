using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_GoodsBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IEnumerable<T_Goods> GetListOfIEnumerable(string strWhere)
        {
            return new T_GoodsDAL().GetListOfIEnumerable(strWhere);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public IEnumerable<T_Goods> GetListOfIEnumerable(int Top, string strWhere, string filedOrder)
        {
            return new T_GoodsDAL().GetListOfIEnumerable(Top,strWhere,filedOrder);
        }


        /// <summary>
        /// 获得分页数据
        /// </summary>
        public IEnumerable<T_Goods> GetPageListOfIEnumerable(int page,int pageRow, string strWhere)
        {
            return new T_GoodsDAL().GetPageListOfIEnumerable(page, pageRow, strWhere);
        }

        public int GetPageNumber(int pageRow,string strWhere)
        {
            List<T_Goods> goods = (List<T_Goods>) new T_GoodsDAL().GetListOfIEnumerable(strWhere);
            double r = (double)goods.Count / pageRow;
            double i= Math.Ceiling(r);
            return Convert.ToInt32(i);
        }


        public bool UpdateByShortCode(SelfhelpOrderMgr.Model.T_Goods model)
        {
            return new T_GoodsDAL().UpdateByShortCode(model);
        }
    }
}