using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_StockBLL
    {
        //盘点入库 StockFlag 1
        //盘点出库 StockFlag 2
        //超市消费 StockFlag 5
        //消费退货 StockFlag 6
        /// <summary>
        /// 增加一个库存主单
        /// </summary>
        /// <param name="crtby"></param>
        /// <param name="stockType"></param>
        /// <param name="stockflag"></param>
        /// <param name="inoutflag"></param>
        /// <returns></returns>
        public T_Stock NewStock(string crtby, string stockType, int stockflag, int inoutflag)//增加一个库存主单
        {
            return new T_StockDAL().NewStock(crtby, stockType, stockflag, inoutflag);
        }

    }
}