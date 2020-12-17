using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Stock
    public partial class T_StockBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_StockDAL dal = new SelfhelpOrderMgr.DAL.T_StockDAL();
        public T_StockBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string StockId)
        {
            return dal.Exists(StockId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Stock model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Stock model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string StockId)
        {

            return dal.Delete(StockId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Stock GetModel(string StockId)
        {

            return dal.GetModel(StockId);
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
        public List<SelfhelpOrderMgr.Model.T_Stock> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Stock> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Stock> modelList = new List<SelfhelpOrderMgr.Model.T_Stock>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Stock model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Stock();
                    model.StockId = dt.Rows[n]["StockId"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.InvoiceNo = dt.Rows[n]["InvoiceNo"].ToString();
                    if (dt.Rows[n]["Stockflag"].ToString() != "")
                    {
                        model.Stockflag = int.Parse(dt.Rows[n]["Stockflag"].ToString());
                    }
                    if (dt.Rows[n]["InOutFlag"].ToString() != "")
                    {
                        model.InOutFlag = int.Parse(dt.Rows[n]["InOutFlag"].ToString());
                    }
                    model.WareHouseCode = dt.Rows[n]["WareHouseCode"].ToString();
                    if (dt.Rows[n]["InOutDate"].ToString() != "")
                    {
                        model.InOutDate = DateTime.Parse(dt.Rows[n]["InOutDate"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.StockType = dt.Rows[n]["StockType"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDt"].ToString() != "")
                    {
                        model.CrtDt = DateTime.Parse(dt.Rows[n]["CrtDt"].ToString());
                    }
                    if (dt.Rows[n]["CheckFlag"].ToString() != "")
                    {
                        model.CheckFlag = int.Parse(dt.Rows[n]["CheckFlag"].ToString());
                    }
                    model.CheckBy = dt.Rows[n]["CheckBy"].ToString();
                    if (dt.Rows[n]["CheckDt"].ToString() != "")
                    {
                        model.CheckDt = DateTime.Parse(dt.Rows[n]["CheckDt"].ToString());
                    }


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