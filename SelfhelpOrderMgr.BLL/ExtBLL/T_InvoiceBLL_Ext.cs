using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_InvoiceBLL
    {
        public List<T_Invoice> GetPageList(int page, int pageRow, string strWhere, string startTime, string endTime, string orderByField)
        {
            return new T_InvoiceDAL().GetPageList(page, pageRow, strWhere,startTime,endTime, orderByField);
        }
        public decimal[] GetListCount(string strWhere)
        {
            return new T_InvoiceDAL().GetListCount( strWhere);        
        }
        public decimal GetAreaBuyGoodCount(string fcrimecode, string gtxm, string fareaCode, string startDate, string endDate)
        { 
            return  new T_InvoiceDAL().GetAreaBuyGoodCount(fcrimecode,gtxm,fareaCode,startDate,endDate);
        }

        //按登录名来统计商品已经购买的数量
        public decimal GetLoginNameBuyGoodCount(string crtby, string gtxm, string startDate, string endDate)
        {
            return new T_InvoiceDAL().GetLoginNameBuyGoodCount(crtby, gtxm,  startDate, endDate);
        }
        //系统提交时验证登录名来统计商品是否超过限购买的数量
        public bool GetLoginNameCheckBuyGoodCountStatus(string crtby, string startDate, string endDate, int orderId, decimal beiShu)
        {
            return new T_InvoiceDAL().GetLoginNameCheckBuyGoodCountStatus( crtby,  startDate,  endDate,  orderId,  beiShu);

        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Invoice> GetModelList(int topNumber,string strWhere,string filedOrder)
        {
            DataSet ds = dal.GetList(topNumber, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }

        public bool CancelOrderById(string InvoiceNo,string DelBy)//取消订单
        {
            return dal.CancelOrderById(InvoiceNo, DelBy);
        }
        //ChangeOrderById 修改订单，将订单取消费结算，可重新修改再结算
        /// <summary>
        /// 修改订单 与取消订单的差别是，没有删除T_SHO_Order 和T_SHO_OrderDTL
        /// 只是把状态改为0，到未提交的状态
        /// </summary>
        /// <param name="InvoiceNo"></param>
        /// <param name="DelBy"></param>
        /// <returns></returns>
        public bool ChangeOrderById(string InvoiceNo, string DelBy)//修改订单，将订单取消费结算，可重新修改再结算
        {
            return dal.ChangeOrderById(InvoiceNo, DelBy);
        }


        //根据OrderId进行判断
        public bool Exists(int OrderId)
        {
            return dal.Exists(OrderId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Invoice GetModel(string InvoiceNo,int vcrdBankFlag)
        {

            return dal.GetModel(InvoiceNo,vcrdBankFlag);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Invoice> GetModelList(string strWhere,int vcrdBankFlag,DateTime startDate,DateTime endDate)
        {
            DataSet ds = dal.GetList(strWhere,vcrdBankFlag, startDate, endDate);
            return DataTableToList(ds.Tables[0],vcrdBankFlag);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Invoice> DataTableToList(DataTable dt,int vcrdBankFlag)
        {
            List<SelfhelpOrderMgr.Model.T_Invoice> modelList = new List<SelfhelpOrderMgr.Model.T_Invoice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Invoice model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Invoice();
                    model.InvoiceNo = dt.Rows[n]["InvoiceNo"].ToString();
                    if (dt.Rows[n]["ServAmount"].ToString() != "")
                    {
                        model.ServAmount = decimal.Parse(dt.Rows[n]["ServAmount"].ToString());
                    }
                    model.Crtby = dt.Rows[n]["Crtby"].ToString();
                    if (dt.Rows[n]["Crtdate"].ToString() != "")
                    {
                        model.Crtdate = DateTime.Parse(dt.Rows[n]["Crtdate"].ToString());
                    }
                    model.fsn = dt.Rows[n]["fsn"].ToString();
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();
                    model.FCriminal = dt.Rows[n]["FCriminal"].ToString();
                    model.Frealareacode = dt.Rows[n]["Frealareacode"].ToString();
                    model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
                    if (dt.Rows[n]["TypeFlag"].ToString() != "")
                    {
                        model.TypeFlag = int.Parse(dt.Rows[n]["TypeFlag"].ToString());
                    }
                    model.CardCode = dt.Rows[n]["CardCode"].ToString();
                    if (dt.Rows[n]["CardType"].ToString() != "")
                    {
                        model.CardType = int.Parse(dt.Rows[n]["CardType"].ToString());
                    }
                    if (dt.Rows[n]["AmountA"].ToString() != "")
                    {
                        model.AmountA = decimal.Parse(dt.Rows[n]["AmountA"].ToString());
                    }
                    if (dt.Rows[n]["AmountB"].ToString() != "")
                    {
                        model.AmountB = decimal.Parse(dt.Rows[n]["AmountB"].ToString());
                    }
                    if (dt.Rows[n]["Fifoflag"].ToString() != "")
                    {
                        model.Fifoflag = int.Parse(dt.Rows[n]["Fifoflag"].ToString());
                    }
                    if (dt.Rows[n]["FreeAmountA"].ToString() != "")
                    {
                        model.FreeAmountA = decimal.Parse(dt.Rows[n]["FreeAmountA"].ToString());
                    }
                    if (dt.Rows[n]["FreeAmountB"].ToString() != "")
                    {
                        model.FreeAmountB = decimal.Parse(dt.Rows[n]["FreeAmountB"].ToString());
                    }
                    if (dt.Rows[n]["Checkflag"].ToString() != "")
                    {
                        model.Checkflag = int.Parse(dt.Rows[n]["Checkflag"].ToString());
                    }
                    model.RoomNo = dt.Rows[n]["RoomNo"].ToString();
                    if (dt.Rows[n]["OrderId"].ToString() != "")
                    {
                        model.OrderId = int.Parse(dt.Rows[n]["OrderId"].ToString());
                    }
                    model.FCrimeCode = dt.Rows[n]["FCrimeCode"].ToString();
                    if (dt.Rows[n]["Amount"].ToString() != "")
                    {
                        model.Amount = decimal.Parse(dt.Rows[n]["Amount"].ToString());
                    }
                    if (dt.Rows[n]["OrderDate"].ToString() != "")
                    {
                        model.OrderDate = DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
                    }
                    if (dt.Rows[n]["PayDate"].ToString() != "")
                    {
                        model.PayDate = DateTime.Parse(dt.Rows[n]["PayDate"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();

                    if (dt.Rows[n]["BankFlag"].ToString() != "")
                    {
                        model.BankFlag = int.Parse(dt.Rows[n]["BankFlag"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }


        public bool UpdatePrintCount(string invoices)
        {
            return new T_InvoiceDAL().UpdatePrintCount(invoices);
        }

        public bool CancelInvoiceOrder(string strInvoices, string crtby)
        {
            return new T_InvoiceDAL().CancelInvoiceOrder(strInvoices,crtby);
        }


        public string AddTuiHuoOrder(List<T_InvoiceDTL> models, string crtby, string ipLastCode)
        {
            return new T_InvoiceDAL().AddTuiHuoOrder(models, crtby, ipLastCode);
        }
    }
}