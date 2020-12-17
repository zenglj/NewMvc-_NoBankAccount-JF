using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_BatchMoneyTrade
    public partial class T_BatchMoneyTradeBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_BatchMoneyTradeDAL dal = new SelfhelpOrderMgr.DAL.T_BatchMoneyTradeDAL();
        public T_BatchMoneyTradeBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Bid)
        {
            return dal.Exists(Bid);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_BatchMoneyTrade model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BatchMoneyTrade model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Bid)
        {

            return dal.Delete(Bid);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_BatchMoneyTrade GetModel(string Bid)
        {

            return dal.GetModel(Bid);
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
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade> modelList = new List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_BatchMoneyTrade model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_BatchMoneyTrade();
                    model.Bid = dt.Rows[n]["Bid"].ToString();
                    model.Crtby = dt.Rows[n]["Crtby"].ToString();
                    if (dt.Rows[n]["crtdt"].ToString() != "")
                    {
                        model.crtdt = DateTime.Parse(dt.Rows[n]["crtdt"].ToString());
                    }
                    model.CheckBy = dt.Rows[n]["CheckBy"].ToString();
                    if (dt.Rows[n]["CheckDate"].ToString() != "")
                    {
                        model.CheckDate = DateTime.Parse(dt.Rows[n]["CheckDate"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();
                    model.FrealAreaCode = dt.Rows[n]["FrealAreaCode"].ToString();
                    model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
                    if (dt.Rows[n]["UDate"].ToString() != "")
                    {
                        model.UDate = DateTime.Parse(dt.Rows[n]["UDate"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    model.FCourseType = dt.Rows[n]["FCourseType"].ToString();
                    if (dt.Rows[n]["cnt"].ToString() != "")
                    {
                        model.cnt = int.Parse(dt.Rows[n]["cnt"].ToString());
                    }
                    model.AuditBy = dt.Rows[n]["AuditBy"].ToString();
                    if (dt.Rows[n]["AuditFlag"].ToString() != "")
                    {
                        model.AuditFlag = int.Parse(dt.Rows[n]["AuditFlag"].ToString());
                    }
                    if (dt.Rows[n]["AuditDate"].ToString() != "")
                    {
                        model.AuditDate = DateTime.Parse(dt.Rows[n]["AuditDate"].ToString());
                    }
                    if (dt.Rows[n]["FdbCheckFlag"].ToString() != "")
                    {
                        model.FdbCheckFlag = int.Parse(dt.Rows[n]["FdbCheckFlag"].ToString());
                    }
                    if (dt.Rows[n]["FdbCheckDate"].ToString() != "")
                    {
                        model.FdbCheckDate = DateTime.Parse(dt.Rows[n]["FdbCheckDate"].ToString());
                    }
                    model.FPostBy = dt.Rows[n]["FPostBy"].ToString();
                    if (dt.Rows[n]["FPostDate"].ToString() != "")
                    {
                        model.FPostDate = DateTime.Parse(dt.Rows[n]["FPostDate"].ToString());
                    }
                    if (dt.Rows[n]["FPoestFlag"].ToString() != "")
                    {
                        model.FPoestFlag = int.Parse(dt.Rows[n]["FPoestFlag"].ToString());
                    }
                    model.FDbCheckBY = dt.Rows[n]["FDbCheckBY"].ToString();
                    if (dt.Rows[n]["FCourseCode"].ToString() != "")
                    {
                        model.FCourseCode = int.Parse(dt.Rows[n]["FCourseCode"].ToString());
                    }
                    if (dt.Rows[n]["FCheckFlag"].ToString() != "")
                    {
                        model.FCheckFlag = int.Parse(dt.Rows[n]["FCheckFlag"].ToString());
                    }
                    if (dt.Rows[n]["FMoneyInOutFlag"].ToString() != "")
                    {
                        model.FMoneyInOutFlag = int.Parse(dt.Rows[n]["FMoneyInOutFlag"].ToString());
                    }
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    if (dt.Rows[n]["FAmount"].ToString() != "")
                    {
                        model.FAmount = decimal.Parse(dt.Rows[n]["FAmount"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.ApplyBy = dt.Rows[n]["ApplyBy"].ToString();
                    if (dt.Rows[n]["Applydt"].ToString() != "")
                    {
                        model.Applydt = DateTime.Parse(dt.Rows[n]["Applydt"].ToString());
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