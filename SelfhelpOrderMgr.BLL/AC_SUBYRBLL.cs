using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //AC_SUBYR
    public partial class AC_SUBYRBLL
    {

        private readonly SelfhelpOrderMgr.DAL.AC_SUBYRDAL dal = new SelfhelpOrderMgr.DAL.AC_SUBYRDAL();
        public AC_SUBYRBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string AccPeriod)
        {
            return dal.Exists(AccPeriod);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.AC_SUBYR model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.AC_SUBYR model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string AccPeriod)
        {

            return dal.Delete(AccPeriod);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.AC_SUBYR GetModel(string AccPeriod)
        {

            return dal.GetModel(AccPeriod);
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
        public List<SelfhelpOrderMgr.Model.AC_SUBYR> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.AC_SUBYR> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.AC_SUBYR> modelList = new List<SelfhelpOrderMgr.Model.AC_SUBYR>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.AC_SUBYR model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.AC_SUBYR();
                    model.AccYear = dt.Rows[n]["AccYear"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDt"].ToString() != "")
                    {
                        model.CrtDt = DateTime.Parse(dt.Rows[n]["CrtDt"].ToString());
                    }
                    model.ModBy = dt.Rows[n]["ModBy"].ToString();
                    if (dt.Rows[n]["ModDt"].ToString() != "")
                    {
                        model.ModDt = DateTime.Parse(dt.Rows[n]["ModDt"].ToString());
                    }
                    model.AccPeriod = dt.Rows[n]["AccPeriod"].ToString();
                    if (dt.Rows[n]["FromDt"].ToString() != "")
                    {
                        model.FromDt = DateTime.Parse(dt.Rows[n]["FromDt"].ToString());
                    }
                    if (dt.Rows[n]["ToDt"].ToString() != "")
                    {
                        model.ToDt = DateTime.Parse(dt.Rows[n]["ToDt"].ToString());
                    }
                    if (dt.Rows[n]["LockDt"].ToString() != "")
                    {
                        model.LockDt = DateTime.Parse(dt.Rows[n]["LockDt"].ToString());
                    }
                    if (dt.Rows[n]["CloseDt"].ToString() != "")
                    {
                        model.CloseDt = DateTime.Parse(dt.Rows[n]["CloseDt"].ToString());
                    }
                    model.Status = dt.Rows[n]["Status"].ToString();
                    model.AccStatus = dt.Rows[n]["AccStatus"].ToString();
                    model.TransFlag = dt.Rows[n]["TransFlag"].ToString();


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