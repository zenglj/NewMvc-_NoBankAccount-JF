using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_SaleDayList
    public partial class T_SHO_SaleDayListBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_SaleDayListDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_SaleDayListDAL();
        public T_SHO_SaleDayListBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Seqno)
        {
            return dal.Exists(Seqno);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_SHO_SaleDayList model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_SaleDayList model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Seqno)
        {

            return dal.Delete(Seqno);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_SaleDayList GetModel(int Seqno)
        {

            return dal.GetModel(Seqno);
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
        public List<SelfhelpOrderMgr.Model.T_SHO_SaleDayList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_SaleDayList> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_SaleDayList> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_SaleDayList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_SaleDayList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_SaleDayList();
                    if (dt.Rows[n]["Seqno"].ToString() != "")
                    {
                        model.Seqno = int.Parse(dt.Rows[n]["Seqno"].ToString());
                    }
                    if (dt.Rows[n]["SaleTypeId"].ToString() != "")
                    {
                        model.SaleTypeId = int.Parse(dt.Rows[n]["SaleTypeId"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    model.StartDay = dt.Rows[n]["StartDay"].ToString();
                    model.EndDay = dt.Rows[n]["EndDay"].ToString();
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    if (dt.Rows[n]["LevelId"].ToString() != "")
                    {
                        model.LevelId = int.Parse(dt.Rows[n]["LevelId"].ToString());
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