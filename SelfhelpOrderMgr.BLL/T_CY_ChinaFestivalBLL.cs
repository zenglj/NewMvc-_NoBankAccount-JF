using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_CY_ChinaFestival
    public partial class T_CY_ChinaFestivalBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_CY_ChinaFestivalDAL dal = new SelfhelpOrderMgr.DAL.T_CY_ChinaFestivalDAL();
        public T_CY_ChinaFestivalBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_CY_ChinaFestival model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_CY_ChinaFestival model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            return dal.Delete(id);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            return dal.DeleteList(idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_CY_ChinaFestival GetModel(int id)
        {

            return dal.GetModel(id);
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
        public List<SelfhelpOrderMgr.Model.T_CY_ChinaFestival> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_CY_ChinaFestival> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_CY_ChinaFestival> modelList = new List<SelfhelpOrderMgr.Model.T_CY_ChinaFestival>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_CY_ChinaFestival model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_CY_ChinaFestival();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.FName = dt.Rows[n]["FName"].ToString();
                    if (dt.Rows[n]["FDate"].ToString() != "")
                    {
                        model.FDate = DateTime.Parse(dt.Rows[n]["FDate"].ToString());
                    }
                    model.Festival_Name = dt.Rows[n]["Festival_Name"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();


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