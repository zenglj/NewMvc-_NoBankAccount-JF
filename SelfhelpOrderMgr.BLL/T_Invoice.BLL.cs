using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Invoice
    public partial class T_InvoiceBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_InvoiceDAL dal = new SelfhelpOrderMgr.DAL.T_InvoiceDAL();
        public T_InvoiceBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string InvoiceNo)
        {
            return dal.Exists(InvoiceNo);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Invoice model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Invoice model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string InvoiceNo)
        {

            return dal.Delete(InvoiceNo);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Invoice GetModel(string InvoiceNo)
        {

            return dal.GetModel(InvoiceNo);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Invoice> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Invoice> DataTableToList(DataTable dt)
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
                    if (dt.Rows[n]["FTZSP_Money"].ToString() != "")
                    {
                        model.FTZSP_Money = decimal.Parse(dt.Rows[n]["FTZSP_Money"].ToString());
                    }
                    model.FCrimeCode = dt.Rows[n]["FCrimeCode"].ToString();
                    if (dt.Rows[n]["printCount"].ToString() != "")
                    {
                        model.printCount = int.Parse(dt.Rows[n]["printCount"].ToString());
                    }
                    if (dt.Rows[n]["OrderStatus"].ToString() != "")
                    {
                        model.OrderStatus = int.Parse(dt.Rows[n]["OrderStatus"].ToString());
                    }
                    model.UserCyDesc = dt.Rows[n]["UserCyDesc"].ToString();
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


                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        #endregion

    }
}