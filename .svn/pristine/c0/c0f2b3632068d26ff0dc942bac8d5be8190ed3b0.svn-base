using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_BONUS
    public partial class T_BONUSBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_BONUSDAL dal = new SelfhelpOrderMgr.DAL.T_BONUSDAL();
        public T_BONUSBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BID)
        {
            return dal.Exists(BID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_BONUS model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_BONUS model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string BID)
        {

            return dal.Delete(BID);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_BONUS GetModel(string BID)
        {

            return dal.GetModel(BID);
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
        public List<SelfhelpOrderMgr.Model.T_BONUS> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_BONUS> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_BONUS> modelList = new List<SelfhelpOrderMgr.Model.T_BONUS>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_BONUS model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_BONUS();
                    model.BID = dt.Rows[n]["BID"].ToString();
                    if (dt.Rows[n]["CheckDate"].ToString() != "")
                    {
                        model.CheckDate = DateTime.Parse(dt.Rows[n]["CheckDate"].ToString());
                    }
                    if (dt.Rows[n]["FLAG"].ToString() != "")
                    {
                        model.FLAG = int.Parse(dt.Rows[n]["FLAG"].ToString());
                    }
                    model.fareaName = dt.Rows[n]["fareaName"].ToString();
                    model.Frealareacode = dt.Rows[n]["Frealareacode"].ToString();
                    model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
                    if (dt.Rows[n]["udate"].ToString() != "")
                    {
                        model.udate = DateTime.Parse(dt.Rows[n]["udate"].ToString());
                    }
                    model.ptype = dt.Rows[n]["ptype"].ToString();
                    if (dt.Rows[n]["cnt"].ToString() != "")
                    {
                        model.cnt = int.Parse(dt.Rows[n]["cnt"].ToString());
                    }
                    model.auditby = dt.Rows[n]["auditby"].ToString();
                    if (dt.Rows[n]["auditflag"].ToString() != "")
                    {
                        model.auditflag = int.Parse(dt.Rows[n]["auditflag"].ToString());
                    }
                    model.FAREACODE = dt.Rows[n]["FAREACODE"].ToString();
                    if (dt.Rows[n]["auditdate"].ToString() != "")
                    {
                        model.auditdate = DateTime.Parse(dt.Rows[n]["auditdate"].ToString());
                    }
                    if (dt.Rows[n]["Fdbcheckflag"].ToString() != "")
                    {
                        model.Fdbcheckflag = int.Parse(dt.Rows[n]["Fdbcheckflag"].ToString());
                    }
                    if (dt.Rows[n]["Fdbcheckdate"].ToString() != "")
                    {
                        model.Fdbcheckdate = DateTime.Parse(dt.Rows[n]["Fdbcheckdate"].ToString());
                    }
                    model.FPostBy = dt.Rows[n]["FPostBy"].ToString();
                    if (dt.Rows[n]["FPostDate"].ToString() != "")
                    {
                        model.FPostDate = DateTime.Parse(dt.Rows[n]["FPostDate"].ToString());
                    }
                    if (dt.Rows[n]["FPostFlag"].ToString() != "")
                    {
                        model.FPostFlag = int.Parse(dt.Rows[n]["FPostFlag"].ToString());
                    }
                    model.FDbCheckBY = dt.Rows[n]["FDbCheckBY"].ToString();
                    if (dt.Rows[n]["FCheckFlag"].ToString() != "")
                    {
                        model.FCheckFlag = int.Parse(dt.Rows[n]["FCheckFlag"].ToString());
                    }
                    if (dt.Rows[n]["fAMOUNT"].ToString() != "")
                    {
                        model.fAMOUNT = decimal.Parse(dt.Rows[n]["fAMOUNT"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.ApplyBy = dt.Rows[n]["ApplyBy"].ToString();
                    if (dt.Rows[n]["Applydt"].ToString() != "")
                    {
                        model.Applydt = DateTime.Parse(dt.Rows[n]["Applydt"].ToString());
                    }
                    model.Crtby = dt.Rows[n]["Crtby"].ToString();
                    if (dt.Rows[n]["crtdt"].ToString() != "")
                    {
                        model.crtdt = DateTime.Parse(dt.Rows[n]["crtdt"].ToString());
                    }
                    model.CHECKBY = dt.Rows[n]["CHECKBY"].ToString();


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