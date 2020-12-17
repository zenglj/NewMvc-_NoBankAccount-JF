using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SHO_OrderDTLBLL
    {
        public int AddDetailAndUpdateMain(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model, string strFreeFlag)
        {
            return new T_SHO_OrderDTLDAL().AddDetailAndUpdateMain( model,  strFreeFlag);
        }
        //增加明细验证库存量是否足够卖
        public bool AddDetailCheckStock(decimal buyCount, string gcode)
        {
            List<T_SHO_OrderDTL> dtls = new T_SHO_OrderDTLBLL().GetModelList("GCode='" + gcode + "' and Flag=0");
            decimal dtlCount = 0;
            decimal goodStockCount = 0; //商品库存数量
            if (dtls.Count > 0)
            {
                foreach (T_SHO_OrderDTL dtl in dtls)
                {
                    dtlCount += dtl.GCount;
                }
            }
            List<T_GOODSSTOCKMAIN> goodsStocks = new T_GOODSSTOCKMAINBLL().GetModelList("GCode='" + gcode + "'");
            if (goodsStocks.Count > 0)
            {
                goodStockCount = goodsStocks[0].BALANCE;
            }
            if (dtlCount + buyCount > goodStockCount)
            {
                return false;//库存不足
            }
            else
            {
                return true;//库存有够
            }
        }
    }
}