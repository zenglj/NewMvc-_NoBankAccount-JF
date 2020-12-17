using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
    //T_AREA
    public partial class T_AREABLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_AREADAL dal = new SelfhelpOrderMgr.DAL.T_AREADAL();
        public T_AREABLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string FCode)
        {
            return dal.Exists(FCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_AREA model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_AREA model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string FCode)
        {

            return dal.Delete(FCode);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_AREA GetModel(string FCode)
        {

            return dal.GetModel(FCode);
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
        public List<SelfhelpOrderMgr.Model.T_AREA> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_AREA> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_AREA> modelList = new List<SelfhelpOrderMgr.Model.T_AREA>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_AREA model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_AREA();
                    model.FCode = dt.Rows[n]["FCode"].ToString();
                    model.FName = dt.Rows[n]["FName"].ToString();
                    model.ID = dt.Rows[n]["ID"].ToString();
                    model.FID = dt.Rows[n]["FID"].ToString();
                    model.URL = dt.Rows[n]["URL"].ToString();
                    if (dt.Rows[n]["FTZSP_Money"].ToString() != "")
                    {
                        model.FTZSP_Money = decimal.Parse(dt.Rows[n]["FTZSP_Money"].ToString());
                    }
                    if (dt.Rows[n]["SaleCloseFlag"].ToString() != "")
                    {
                        model.SaleCloseFlag = int.Parse(dt.Rows[n]["SaleCloseFlag"].ToString());
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