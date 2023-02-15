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
    public class rtnPaySubmitInfo
    {
        public List<T_InvoiceDTL> details { get; set; }
        public T_Invoice invoice { get; set; }
        public T_Criminal criminal { get; set; }
    }
}
