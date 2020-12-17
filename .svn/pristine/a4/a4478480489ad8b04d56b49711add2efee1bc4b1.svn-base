using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_FinancePay
    public partial class T_SHO_FinancePayBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_FinancePayDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_FinancePayDAL();
        public T_SHO_FinancePayBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_FinancePay model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_FinancePay model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_FinancePay GetModel(int Id)
        {

            return dal.GetModel(Id);
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
        public List<SelfhelpOrderMgr.Model.T_SHO_FinancePay> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_FinancePay> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_FinancePay> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_FinancePay>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_FinancePay model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_FinancePay();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.PayBy = dt.Rows[n]["PayBy"].ToString();
                    if (dt.Rows[n]["PayDate"].ToString() != "")
                    {
                        model.PayDate = DateTime.Parse(dt.Rows[n]["PayDate"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.FType = dt.Rows[n]["FType"].ToString();
                    model.FTitle = dt.Rows[n]["FTitle"].ToString();
                    if (dt.Rows[n]["FCount"].ToString() != "")
                    {
                        model.FCount = int.Parse(dt.Rows[n]["FCount"].ToString());
                    }
                    if (dt.Rows[n]["FMoney"].ToString() != "")
                    {
                        model.FMoney = decimal.Parse(dt.Rows[n]["FMoney"].ToString());
                    }
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDt"].ToString() != "")
                    {
                        model.CrtDt = DateTime.Parse(dt.Rows[n]["CrtDt"].ToString());
                    }
                    model.PosName = dt.Rows[n]["PosName"].ToString();
                    model.BankCard = dt.Rows[n]["BankCard"].ToString();


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