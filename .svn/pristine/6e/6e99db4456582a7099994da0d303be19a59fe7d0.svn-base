using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Provide
    public partial class T_ProvideBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_ProvideDAL dal = new SelfhelpOrderMgr.DAL.T_ProvideDAL();
        public T_ProvideBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string PId)
        {
            return dal.Exists(PId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Provide model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Provide model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string PId)
        {

            return dal.Delete(PId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Provide GetModel(string PId)
        {

            return dal.GetModel(PId);
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
        public List<SelfhelpOrderMgr.Model.T_Provide> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Provide> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Provide> modelList = new List<SelfhelpOrderMgr.Model.T_Provide>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Provide model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Provide();
                    model.PId = dt.Rows[n]["PId"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDt"].ToString() != "")
                    {
                        model.CrtDt = DateTime.Parse(dt.Rows[n]["CrtDt"].ToString());
                    }
                    model.FrealareaCode = dt.Rows[n]["FrealareaCode"].ToString();
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.CheckBy = dt.Rows[n]["CheckBy"].ToString();
                    if (dt.Rows[n]["CheckDt"].ToString() != "")
                    {
                        model.CheckDt = DateTime.Parse(dt.Rows[n]["CheckDt"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    if (dt.Rows[n]["PFlag"].ToString() != "")
                    {
                        model.PFlag = int.Parse(dt.Rows[n]["PFlag"].ToString());
                    }
                    model.ApplyBy = dt.Rows[n]["ApplyBy"].ToString();
                    if (dt.Rows[n]["ApplyDt"].ToString() != "")
                    {
                        model.ApplyDt = DateTime.Parse(dt.Rows[n]["ApplyDt"].ToString());
                    }
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    model.FRealareaName = dt.Rows[n]["FRealareaName"].ToString();
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();
                    if (dt.Rows[n]["PTag"].ToString() != "")
                    {
                        model.PTag = int.Parse(dt.Rows[n]["PTag"].ToString());
                    }
                    model.PTagName = dt.Rows[n]["PTagName"].ToString();
                    if (dt.Rows[n]["FManNb"].ToString() != "")
                    {
                        model.FManNb = int.Parse(dt.Rows[n]["FManNb"].ToString());
                    }
                    if (dt.Rows[n]["FWomNb"].ToString() != "")
                    {
                        model.FWomNb = int.Parse(dt.Rows[n]["FWomNb"].ToString());
                    }
                    if (dt.Rows[n]["FManAmount"].ToString() != "")
                    {
                        model.FManAmount = decimal.Parse(dt.Rows[n]["FManAmount"].ToString());
                    }
                    if (dt.Rows[n]["FWomAmount"].ToString() != "")
                    {
                        model.FWomAmount = decimal.Parse(dt.Rows[n]["FWomAmount"].ToString());
                    }
                    if (dt.Rows[n]["PDate"].ToString() != "")
                    {
                        model.PDate = DateTime.Parse(dt.Rows[n]["PDate"].ToString());
                    }
                    if (dt.Rows[n]["FAmount"].ToString() != "")
                    {
                        model.FAmount = decimal.Parse(dt.Rows[n]["FAmount"].ToString());
                    }
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