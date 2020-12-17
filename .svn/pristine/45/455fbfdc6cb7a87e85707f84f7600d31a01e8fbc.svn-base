using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_BatchMoneyTrade_ErrList
    public partial class T_BatchMoneyTrade_ErrListBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_BatchMoneyTrade_ErrListDAL dal = new SelfhelpOrderMgr.DAL.T_BatchMoneyTrade_ErrListDAL();
        public T_BatchMoneyTrade_ErrListBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int seqno)
        {
            return dal.Exists(seqno);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int seqno)
        {

            return dal.Delete(seqno);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string seqnolist)
        {
            return dal.DeleteList(seqnolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList GetModel(int seqno)
        {

            return dal.GetModel(seqno);
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
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList> modelList = new List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_BatchMoneyTrade_ErrList();
                    if (dt.Rows[n]["seqno"].ToString() != "")
                    {
                        model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
                    }
                    model.notes = dt.Rows[n]["notes"].ToString();
                    if (dt.Rows[n]["ImportType"].ToString() != "")
                    {
                        model.ImportType = int.Parse(dt.Rows[n]["ImportType"].ToString());
                    }
                    model.fcrimecode = dt.Rows[n]["fcrimecode"].ToString();
                    model.fname = dt.Rows[n]["fname"].ToString();
                    if (dt.Rows[n]["Amount"].ToString() != "")
                    {
                        model.Amount = decimal.Parse(dt.Rows[n]["Amount"].ToString());
                    }
                    if (dt.Rows[n]["Crtdt"].ToString() != "")
                    {
                        model.Crtdt = DateTime.Parse(dt.Rows[n]["Crtdt"].ToString());
                    }
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.pc = dt.Rows[n]["pc"].ToString();


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