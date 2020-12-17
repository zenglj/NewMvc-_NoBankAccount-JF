using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_StockDTL
    public partial class T_StockDTLBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_StockDTLDAL dal = new SelfhelpOrderMgr.DAL.T_StockDTLDAL();
        public T_StockDTLBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int SeqId)
        {
            return dal.Exists(SeqId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_StockDTL model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_StockDTL model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SeqId)
        {

            return dal.Delete(SeqId);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string SeqIdlist)
        {
            return dal.DeleteList(SeqIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_StockDTL GetModel(int SeqId)
        {

            return dal.GetModel(SeqId);
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
        public List<SelfhelpOrderMgr.Model.T_StockDTL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_StockDTL> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_StockDTL> modelList = new List<SelfhelpOrderMgr.Model.T_StockDTL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_StockDTL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_StockDTL();
                    if (dt.Rows[n]["SeqId"].ToString() != "")
                    {
                        model.SeqId = int.Parse(dt.Rows[n]["SeqId"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.WareHouseCode = dt.Rows[n]["WareHouseCode"].ToString();
                    model.StockId = dt.Rows[n]["StockId"].ToString();
                    model.GCode = dt.Rows[n]["GCode"].ToString();
                    model.GTXM = dt.Rows[n]["GTXM"].ToString();
                    if (dt.Rows[n]["GCount"].ToString() != "")
                    {
                        model.GCount = decimal.Parse(dt.Rows[n]["GCount"].ToString());
                    }
                    if (dt.Rows[n]["GDJ"].ToString() != "")
                    {
                        model.GDJ = decimal.Parse(dt.Rows[n]["GDJ"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    if (dt.Rows[n]["StockFlag"].ToString() != "")
                    {
                        model.StockFlag = int.Parse(dt.Rows[n]["StockFlag"].ToString());
                    }
                    if (dt.Rows[n]["InOutFlag"].ToString() != "")
                    {
                        model.InOutFlag = int.Parse(dt.Rows[n]["InOutFlag"].ToString());
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