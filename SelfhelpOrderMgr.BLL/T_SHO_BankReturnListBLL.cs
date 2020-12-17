using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_BankReturnList
    public partial class T_SHO_BankReturnListBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_BankReturnListDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_BankReturnListDAL();
        public T_SHO_BankReturnListBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {
            return dal.Exists(BID, BankCard, UserName, UserMoney, ResultInfo, Flag);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_SHO_BankReturnList model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_BankReturnList model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {

            return dal.Delete(BID, BankCard, UserName, UserMoney, ResultInfo, Flag);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_BankReturnList GetModel(string BID, string BankCard, string UserName, decimal UserMoney, string ResultInfo, int Flag)
        {

            return dal.GetModel(BID, BankCard, UserName, UserMoney, ResultInfo, Flag);
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
        public List<SelfhelpOrderMgr.Model.T_SHO_BankReturnList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_BankReturnList> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_BankReturnList> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_BankReturnList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_BankReturnList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_BankReturnList();
                    model.BID = dt.Rows[n]["BID"].ToString();
                    model.BankCard = dt.Rows[n]["BankCard"].ToString();
                    model.UserName = dt.Rows[n]["UserName"].ToString();
                    if (dt.Rows[n]["UserMoney"].ToString() != "")
                    {
                        model.UserMoney = decimal.Parse(dt.Rows[n]["UserMoney"].ToString());
                    }
                    model.ResultInfo = dt.Rows[n]["ResultInfo"].ToString();
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
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