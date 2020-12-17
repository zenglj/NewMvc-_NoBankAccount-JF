using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SHO_SaleDayListBLL
    {
        //验证指定日期是否是可以购物
        public int SaleDayExists(int saleTypeId, DateTime saleDay)
        {
            return new T_SHO_SaleDayListDAL().SaleDayExists(saleTypeId, saleDay);
        }

        public string SaleDayTimeArea(int saleTypeId, DateTime saleDay)
        {
            return new T_SHO_SaleDayListDAL().SaleDayTimeArea(saleTypeId, saleDay);
        }
    }
}