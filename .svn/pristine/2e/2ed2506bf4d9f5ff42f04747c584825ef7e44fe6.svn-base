using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_BatchMoneyTrade_DTL
    public partial class T_BatchMoneyTrade_DTLBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_BatchMoneyTrade_DTLDAL dal = new SelfhelpOrderMgr.DAL.T_BatchMoneyTrade_DTLDAL();
        public T_BatchMoneyTrade_DTLBLL()
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
        public int Add(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model)
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
        public SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL GetModel(int seqno)
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
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL> modelList = new List<SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_BatchMoneyTrade_DTL();
                    if (dt.Rows[n]["seqno"].ToString() != "")
                    {
                        model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
                    }
                    model.FCriminal = dt.Rows[n]["FCriminal"].ToString();
                    model.Vouno = dt.Rows[n]["Vouno"].ToString();
                    model.FrealareaCode = dt.Rows[n]["FrealareaCode"].ToString();
                    model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.PType = dt.Rows[n]["PType"].ToString();
                    if (dt.Rows[n]["UDate"].ToString() != "")
                    {
                        model.UDate = DateTime.Parse(dt.Rows[n]["UDate"].ToString());
                    }
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDt"].ToString() != "")
                    {
                        model.CrtDt = DateTime.Parse(dt.Rows[n]["CrtDt"].ToString());
                    }
                    model.ApplyBy = dt.Rows[n]["ApplyBy"].ToString();
                    model.Bid = dt.Rows[n]["Bid"].ToString();
                    if (dt.Rows[n]["AccType"].ToString() != "")
                    {
                        model.AccType = int.Parse(dt.Rows[n]["AccType"].ToString());
                    }
                    if (dt.Rows[n]["CardType"].ToString() != "")
                    {
                        model.CardType = int.Parse(dt.Rows[n]["CardType"].ToString());
                    }
                    if (dt.Rows[n]["AmountC"].ToString() != "")
                    {
                        model.AmountC = decimal.Parse(dt.Rows[n]["AmountC"].ToString());
                    }
                    if (dt.Rows[n]["cqbt"].ToString() != "")
                    {
                        model.cqbt = decimal.Parse(dt.Rows[n]["cqbt"].ToString());
                    }
                    if (dt.Rows[n]["gwjt"].ToString() != "")
                    {
                        model.gwjt = decimal.Parse(dt.Rows[n]["gwjt"].ToString());
                    }
                    if (dt.Rows[n]["ldjx"].ToString() != "")
                    {
                        model.ldjx = decimal.Parse(dt.Rows[n]["ldjx"].ToString());
                    }
                    if (dt.Rows[n]["tbbz"].ToString() != "")
                    {
                        model.tbbz = decimal.Parse(dt.Rows[n]["tbbz"].ToString());
                    }
                    if (dt.Rows[n]["grkj"].ToString() != "")
                    {
                        model.grkj = decimal.Parse(dt.Rows[n]["grkj"].ToString());
                    }
                    if (dt.Rows[n]["FMoneyInOutFlag"].ToString() != "")
                    {
                        model.FMoneyInOutFlag = int.Parse(dt.Rows[n]["FMoneyInOutFlag"].ToString());
                    }
                    model.FCrimeCode = dt.Rows[n]["FCrimeCode"].ToString();
                    model.CardCode = dt.Rows[n]["CardCode"].ToString();
                    if (dt.Rows[n]["FAmount"].ToString() != "")
                    {
                        model.FAmount = decimal.Parse(dt.Rows[n]["FAmount"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.FareaCode = dt.Rows[n]["FareaCode"].ToString();
                    model.FareaName = dt.Rows[n]["FareaName"].ToString();


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