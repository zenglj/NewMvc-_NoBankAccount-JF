using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_GoodsType
    public partial class T_GoodsTypeBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_GoodsTypeDAL dal = new SelfhelpOrderMgr.DAL.T_GoodsTypeDAL();
        public T_GoodsTypeBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string Fcode)
        {
            return dal.Exists(Fcode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_GoodsType model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_GoodsType model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Fcode)
        {

            return dal.Delete(Fcode);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_GoodsType GetModel(string Fcode)
        {

            return dal.GetModel(Fcode);
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
        public List<SelfhelpOrderMgr.Model.T_GoodsType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_GoodsType> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_GoodsType> modelList = new List<SelfhelpOrderMgr.Model.T_GoodsType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_GoodsType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_GoodsType();
                    model.Fcode = dt.Rows[n]["Fcode"].ToString();
                    if (dt.Rows[n]["CtrlMode"].ToString() != "")
                    {
                        model.CtrlMode = int.Parse(dt.Rows[n]["CtrlMode"].ToString());
                    }
                    model.Fname = dt.Rows[n]["Fname"].ToString();
                    if (dt.Rows[n]["flag"].ToString() != "")
                    {
                        model.flag = int.Parse(dt.Rows[n]["flag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    if (dt.Rows[n]["SaleTypeId"].ToString() != "")
                    {
                        model.SaleTypeId = int.Parse(dt.Rows[n]["SaleTypeId"].ToString());
                    }
                    if (dt.Rows[n]["LevelNo"].ToString() != "")
                    {
                        model.LevelNo = int.Parse(dt.Rows[n]["LevelNo"].ToString());
                    }
                    model.FTypeCode = dt.Rows[n]["FTypeCode"].ToString();
                    if (dt.Rows[n]["FTZSP_TypeFlag"].ToString() != "")
                    {
                        model.FTZSP_TypeFlag = int.Parse(dt.Rows[n]["FTZSP_TypeFlag"].ToString());
                    }
                    if (dt.Rows[n]["MaxBuyCount"].ToString() != "")
                    {
                        model.MaxBuyCount = int.Parse(dt.Rows[n]["MaxBuyCount"].ToString());
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