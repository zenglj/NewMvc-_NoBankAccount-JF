using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_Order
    public partial class T_SHO_OrderBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_OrderDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_OrderDAL();
        public T_SHO_OrderBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int OrderID)
        {
            return dal.Exists(OrderID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_Order model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_Order model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int OrderID)
        {

            return dal.Delete(OrderID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string OrderIDlist)
        {
            return dal.DeleteList(OrderIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_Order GetModel(int OrderID)
        {

            return dal.GetModel(OrderID);
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
        public List<SelfhelpOrderMgr.Model.T_SHO_Order> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_Order> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_Order> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_Order>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_Order model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_Order();
                    if (dt.Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(dt.Rows[n]["OrderID"].ToString());
                    }
                    if (dt.Rows[n]["FreeAmount"].ToString() != "")
                    {
                        model.FreeAmount = decimal.Parse(dt.Rows[n]["FreeAmount"].ToString());
                    }
                    model.RoomNO = dt.Rows[n]["RoomNO"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["FTZSP_Money"].ToString() != "")
                    {
                        model.FTZSP_Money = decimal.Parse(dt.Rows[n]["FTZSP_Money"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    model.FCrimecode = dt.Rows[n]["FCrimecode"].ToString();
                    model.FCriminal = dt.Rows[n]["FCriminal"].ToString();
                    if (dt.Rows[n]["CrtDate"].ToString() != "")
                    {
                        model.CrtDate = DateTime.Parse(dt.Rows[n]["CrtDate"].ToString());
                    }
                    if (dt.Rows[n]["FAmount"].ToString() != "")
                    {
                        model.FAmount = decimal.Parse(dt.Rows[n]["FAmount"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.InvoiceNo = dt.Rows[n]["InvoiceNo"].ToString();
                    model.IPAddr = dt.Rows[n]["IPAddr"].ToString();


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