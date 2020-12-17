using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_OrderDTL
    public partial class T_SHO_OrderDTLBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_OrderDTLDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_OrderDTLDAL();
        public T_SHO_OrderDTLBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_OrderDTL GetModel(int ID)
        {

            return dal.GetModel(ID);
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
        public List<SelfhelpOrderMgr.Model.T_SHO_OrderDTL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_OrderDTL> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_OrderDTL> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_OrderDTL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_OrderDTL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_OrderDTL();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["FreeFlag"].ToString() != "")
                    {
                        model.FreeFlag = int.Parse(dt.Rows[n]["FreeFlag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.SPShortCode = dt.Rows[n]["SPShortCode"].ToString();
                    if (dt.Rows[n]["FTZSP_TypeFlag"].ToString() != "")
                    {
                        model.FTZSP_TypeFlag = int.Parse(dt.Rows[n]["FTZSP_TypeFlag"].ToString());
                    }
                    if (dt.Rows[n]["OrderID"].ToString() != "")
                    {
                        model.OrderID = int.Parse(dt.Rows[n]["OrderID"].ToString());
                    }
                    model.GCode = dt.Rows[n]["GCode"].ToString();
                    model.GTXM = dt.Rows[n]["GTXM"].ToString();
                    model.GName = dt.Rows[n]["GName"].ToString();
                    if (dt.Rows[n]["GCount"].ToString() != "")
                    {
                        model.GCount = decimal.Parse(dt.Rows[n]["GCount"].ToString());
                    }
                    if (dt.Rows[n]["GPrice"].ToString() != "")
                    {
                        model.GPrice = decimal.Parse(dt.Rows[n]["GPrice"].ToString());
                    }
                    if (dt.Rows[n]["GAmount"].ToString() != "")
                    {
                        model.GAmount = decimal.Parse(dt.Rows[n]["GAmount"].ToString());
                    }
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