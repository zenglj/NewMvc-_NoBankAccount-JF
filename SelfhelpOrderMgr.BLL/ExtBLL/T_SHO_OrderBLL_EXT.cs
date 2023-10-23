using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SHO_OrderBLL
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        BaseDapperBLL _baseDapperBLL = new BaseDapperBLL();
        /// <summary>
        /// 获取订单列表信息
        /// </summary>
        public IEnumerable<T_SHO_Order> GetListOfIEnumerable(string strWhere)
        {
            return new T_SHO_OrderDAL().GetListOfIEnumerable( strWhere);
        }

        /// <summary>
		/// 按订单号OrderId更新订单金额Amount
		/// </summary>
        public bool UpdateMoney(int orderId, decimal gamount, string strFreeFlag)
        {
            return new T_SHO_OrderDAL().UpdateMoney(orderId, gamount, strFreeFlag);
        }

        public bool DelOrderDetailAndUpdateMoney(int orderId,int detailId)
        {
            return new T_SHO_OrderDAL().DelOrderDetailAndUpdateMoney(orderId,detailId);
        }

        /// <summary>
        /// 按订单号OrderId更新订单状态Flag
        /// </summary>
        public bool UpdateFlag(int orderId, int flag)
        {
            return new T_SHO_OrderDAL().UpdateFlag(orderId, flag);
        }

        //删除犯人的临时订单信息
        public bool DeleteOrderInfoByFCrimecode(string fcrimecode,string saleTypeId)
        {
            return new T_SHO_OrderDAL().DeleteOrderInfoByFCrimecode(fcrimecode,saleTypeId);
        }

        //订单结算
        public string SubmitOrder(int orderId, string crtby, string ipLastCode, string fcrimecode, string userRoomNo)
        {
            if (new T_InvoiceBLL().Exists(orderId))
            {
                return "Error|该订单号已经结算过了，不能再重复结算（结算提示）";
            }
            //取得当前犯人的余额信息
            T_SHO_Order order = new T_SHO_OrderDAL().GetModel(orderId);
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + order.PType + "'");

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, saleTypes[0].Id);
            if (criminal.ErrInfo != "")
            {
                return "Error|" + criminal.ErrInfo + "，请与管理人员联系";
            }
            if (criminal.CanUseMoneyA < 0)
            {
                return "Error|A账户已经超过本月最大可消费额度了，请与管理人员联系";
            }

            if (criminal.CanUseMoneyB < 0)
            {
                return "Error|B账户已经超过本月最大可消费额度了，请与管理人员联系";
            }

            List<T_SHO_OrderDTL> details = new T_SHO_OrderDTLBLL().GetModelList("OrderId='"+orderId.ToString()+"'");
                
            if(order.FAmount<=criminal.OkUseAllMoney)
            {
                if(order.FAmount-order.FreeAmount>criminal.NoXiaofeimoney)
                {
                    return "Error|可消费的金额不足";
                }
                else
                {
                    T_SHO_ManagerSet payMode = new T_SHO_ManagerSetBLL().GetModel("PayForProcFlag");
                    if(payMode!=null)
                    {
                        if(payMode.MgrValue=="1")
                        {
                            //优先从存储过程结算
                            return new T_SHO_OrderDAL().SubmitOrderForProc(order.OrderID, crtby, ipLastCode, criminal, order, details, userRoomNo); 
                        }
                    }
                    //否则从程序结算
                    return new T_SHO_OrderDAL().SubmitOrder(order.OrderID, crtby, ipLastCode, criminal, order, details, userRoomNo); 

                }
            }
            else
            {
                return "Error|账户余额不足";
            }
        }



        //积分订单结算
        public string SubmitJFOrder(int orderId, string crtby, string ipLastCode, string fcrimecode, string userRoomNo)
        {
            if (_baseDapperBLL.GetModelFirst<T_JF_Invoice>(jss.Serialize(new { OrderId = orderId })) != null)
            {
                return "Error|该订单号已经结算过了，不能再重复结算（结算提示）";
            }

            //取得当前犯人的余额信息
            T_SHO_Order order = new T_SHO_OrderDAL().GetModel(orderId);
            List<T_SHO_SaleType> saleTypes = new T_SHO_SaleTypeBLL().GetModelList("PType='" + order.PType + "'");

            T_Criminal criminal = new T_CriminalBLL().GetCriminalXE_info(fcrimecode, saleTypes[0].Id);
            if (criminal.ErrInfo != "")
            {
                return "Error|" + criminal.ErrInfo + "，请与管理人员联系";
            }

            if (criminal.AccPoints <= 0 || order.FAmount > criminal.AccPoints)
            {
                return "Error|当前积分账户余额不足....";
            }
            //if (criminal.CanUseMoneyA < 0)
            //{
            //    return "Error|A账户已经超过本月最大可消费额度了，请与管理人员联系";
            //}

            //if (criminal.CanUseMoneyB < 0)
            //{
            //    return "Error|B账户已经超过本月最大可消费额度了，请与管理人员联系";
            //}

            return DoingJFSubmitOrder(orderId, crtby, ipLastCode, userRoomNo, order, criminal);
        }

        private string DoingJFSubmitOrder(int orderId, string crtby, string ipLastCode, string userRoomNo, T_SHO_Order order, T_Criminal criminal)
        {
            List<T_SHO_OrderDTL> details = new T_SHO_OrderDTLBLL().GetModelList("OrderId='" + orderId.ToString() + "'");

            if (order.FAmount <= criminal.AccPoints)
            {
                if (order.FAmount - order.FreeAmount > criminal.AccPoints)
                {
                    return "Error|可消费的积分不足";
                }
                else
                {
                    T_SHO_ManagerSet payMode = new T_SHO_ManagerSetBLL().GetModel("PayForProcFlag");
                    //优先从存储过程结算
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    dict.Add("OrderId", order.OrderID.ToString());
                    dict.Add("IpLastCode", ipLastCode);
                    dict.Add("crtby", crtby);
                    dict.Add("userRoomNo", userRoomNo);
                    dict.Add("UserCyDesc", criminal.UserCyDesc);

                    return _baseDapperBLL.ExecuteProc("JF_PayAndCrtCustomerList", dict);

                }
            }
            else
            {
                return "Error|账户余额不足";
            }
        }



        //按商品查询次本月购买的数量
        public decimal GetMonthBuyCount(string gtxm, string fcrimecode)
        {
            return new T_SHO_OrderDAL().GetMonthBuyCount(gtxm,fcrimecode);
        }

        //按商品【类型】查询次本月购买的数量
        public decimal GetTypeBuyCount(int ctrlMode,string goodType,string gtxm, string fcrimecode)
        {
            return new T_SHO_OrderDAL().GetTypeBuyCount(ctrlMode, goodType, gtxm, fcrimecode);
        }



    }
}