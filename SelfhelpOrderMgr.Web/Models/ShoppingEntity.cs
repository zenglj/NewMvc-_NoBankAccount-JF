using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.Web
{
    //执行结果的返回状态
    public class rtnStatus
    {
        public int orderId { get; set; }//订单号
        public string FName { get; set; }//姓名
        public string cyName { get; set; }//处遇名
        public decimal NoXiaofeimoney { get; set; }//本月可消费金额

        public decimal OkUseAllMoney { get; set; }//可用总余额

        public decimal orderMoney { get; set; }//订单金额

        public decimal Xiaofeimoney { get; set; }//本月已消费金额

        public string FAreaName { get; set; }

        public string FCrimeCode { get; set; }//狱号

        public decimal AccPoints { get; set; }//账户积分
        public decimal XiaoFeiPoints { get; set; }//本月已消费积分

        public List<T_SHO_OrderDTL> lists { get; set; }//订单列表

    }
    public class rtnDelResult
    {
        public int delDetailId { get; set; }
        public int orderId { get; set; }
        public decimal currXFmoney { get; set; }
        public decimal orderMoney { get; set; }
    }

    //结算返回的信息
    public class rtnPaySubmitInfo<T, DTL>
    {
        //public T_Invoice invoice { get; set; }
        public T invoice { get; set; }
        //public List<T_InvoiceDTL> details { get; set; }
        public List<DTL> details { get; set; }
        public T_Criminal criminal { get; set; }
    }



    public class rtnTrades
    {
        public T_BatchMoneyTrade trade { get; set; }
        public List<T_BatchMoneyTrade_DTL> dtls { get; set; }
    }


    public class PrintInvoices<T, DTL>
    {
        public T invoice { get; set; }
        public List<DTL> details { get; set; }

        public T_Criminal criminal { get; set; }
    }
}
