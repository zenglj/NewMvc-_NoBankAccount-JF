using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_ProvideDTL
    public partial class T_ProvideDTLBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_ProvideDTLDAL dal = new SelfhelpOrderMgr.DAL.T_ProvideDTLDAL();
        public T_ProvideDTLBLL()
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
        public int Add(SelfhelpOrderMgr.Model.T_ProvideDTL model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_ProvideDTL model)
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
        public SelfhelpOrderMgr.Model.T_ProvideDTL GetModel(int seqno)
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
        public List<SelfhelpOrderMgr.Model.T_ProvideDTL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_ProvideDTL> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_ProvideDTL> modelList = new List<SelfhelpOrderMgr.Model.T_ProvideDTL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_ProvideDTL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_ProvideDTL();
                    if (dt.Rows[n]["seqno"].ToString() != "")
                    {
                        model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
                    }
                    model.VouNo = dt.Rows[n]["VouNo"].ToString();
                    model.FrealareaCode = dt.Rows[n]["FrealareaCode"].ToString();
                    model.FrealareaName = dt.Rows[n]["FrealareaName"].ToString();
                    model.FSex = dt.Rows[n]["FSex"].ToString();
                    if (dt.Rows[n]["AccType"].ToString() != "")
                    {
                        model.AccType = int.Parse(dt.Rows[n]["AccType"].ToString());
                    }
                    if (dt.Rows[n]["CardType"].ToString() != "")
                    {
                        model.CardType = int.Parse(dt.Rows[n]["CardType"].ToString());
                    }
                    if (dt.Rows[n]["PDate"].ToString() != "")
                    {
                        model.PDate = DateTime.Parse(dt.Rows[n]["PDate"].ToString());
                    }
                    model.PId = dt.Rows[n]["PId"].ToString();
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
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();
                    model.FCriminal = dt.Rows[n]["FCriminal"].ToString();


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