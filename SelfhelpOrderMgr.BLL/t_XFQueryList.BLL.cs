using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //t_XFQueryList
    public partial class t_XFQueryListBLL
    {

        private readonly SelfhelpOrderMgr.DAL.t_XFQueryListDAL dal = new SelfhelpOrderMgr.DAL.t_XFQueryListDAL();
        public t_XFQueryListBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {
            return dal.Exists(fcrimecode, fname, CDate, Cmoney, Dtype);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.t_XFQueryList model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.t_XFQueryList model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {

            return dal.Delete(fcrimecode, fname, CDate, Cmoney, Dtype);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.t_XFQueryList GetModel(string fcrimecode, string fname, DateTime CDate, decimal Cmoney, string Dtype)
        {

            return dal.GetModel(fcrimecode, fname, CDate, Cmoney, Dtype);
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
        public List<SelfhelpOrderMgr.Model.t_XFQueryList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.t_XFQueryList> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.t_XFQueryList> modelList = new List<SelfhelpOrderMgr.Model.t_XFQueryList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.t_XFQueryList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.t_XFQueryList();
                    model.fcrimecode = dt.Rows[n]["fcrimecode"].ToString();
                    model.fname = dt.Rows[n]["fname"].ToString();
                    if (dt.Rows[n]["CDate"].ToString() != "")
                    {
                        model.CDate = DateTime.Parse(dt.Rows[n]["CDate"].ToString());
                    }
                    if (dt.Rows[n]["Cmoney"].ToString() != "")
                    {
                        model.Cmoney = decimal.Parse(dt.Rows[n]["Cmoney"].ToString());
                    }
                    model.Dtype = dt.Rows[n]["Dtype"].ToString();

                    model.BankCard = dt.Rows[n]["BankCard"].ToString();
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();


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