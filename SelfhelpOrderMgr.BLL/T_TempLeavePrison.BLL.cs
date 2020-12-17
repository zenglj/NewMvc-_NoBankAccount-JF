using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_TempLeavePrison
    public partial class T_TempLeavePrisonBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_TempLeavePrisonDAL dal = new SelfhelpOrderMgr.DAL.T_TempLeavePrisonDAL();
        public T_TempLeavePrisonBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {
            return dal.Exists(fcode, fname, foudate, fareaname, FStatus, JSMoney);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_TempLeavePrison model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_TempLeavePrison model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {

            return dal.Delete(fcode, fname, foudate, fareaname, FStatus, JSMoney);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_TempLeavePrison GetModel(string fcode, string fname, DateTime foudate, string fareaname, string FStatus, decimal JSMoney)
        {

            return dal.GetModel(fcode, fname, foudate, fareaname, FStatus, JSMoney);
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
        public List<SelfhelpOrderMgr.Model.T_TempLeavePrison> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_TempLeavePrison> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_TempLeavePrison> modelList = new List<SelfhelpOrderMgr.Model.T_TempLeavePrison>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_TempLeavePrison model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_TempLeavePrison();
                    model.fcode = dt.Rows[n]["fcode"].ToString();
                    model.fname = dt.Rows[n]["fname"].ToString();
                    if (dt.Rows[n]["foudate"].ToString() != "")
                    {
                        model.foudate = DateTime.Parse(dt.Rows[n]["foudate"].ToString());
                    }
                    model.fareaname = dt.Rows[n]["fareaname"].ToString();
                    model.FStatus = dt.Rows[n]["FStatus"].ToString();
                    if (dt.Rows[n]["JSMoney"].ToString() != "")
                    {
                        model.JSMoney = decimal.Parse(dt.Rows[n]["JSMoney"].ToString());
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