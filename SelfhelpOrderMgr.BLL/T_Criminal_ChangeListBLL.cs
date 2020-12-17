using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Criminal_ChangeList
    public partial class T_Criminal_ChangeListBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_Criminal_ChangeListDAL dal = new SelfhelpOrderMgr.DAL.T_Criminal_ChangeListDAL();
        public T_Criminal_ChangeListBLL()
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
        public int Add(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal_ChangeList model)
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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string Seqnolist)
        {
            return dal.DeleteList(Seqnolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Criminal_ChangeList GetModel(int Seqno)
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
        public List<SelfhelpOrderMgr.Model.T_Criminal_ChangeList> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Criminal_ChangeList> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Criminal_ChangeList> modelList = new List<SelfhelpOrderMgr.Model.T_Criminal_ChangeList>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Criminal_ChangeList model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Criminal_ChangeList();
                    if (dt.Rows[n]["Seqno"].ToString() != "")
                    {
                        model.Seqno = int.Parse(dt.Rows[n]["Seqno"].ToString());
                    }
                    model.ChangeInfo = dt.Rows[n]["ChangeInfo"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDate"].ToString() != "")
                    {
                        model.CrtDate = DateTime.Parse(dt.Rows[n]["CrtDate"].ToString());
                    }
                    model.AuditBy = dt.Rows[n]["AuditBy"].ToString();
                    model.AuditArea = dt.Rows[n]["AuditArea"].ToString();
                    if (dt.Rows[n]["AuditDate"].ToString() != "")
                    {
                        model.AuditDate = DateTime.Parse(dt.Rows[n]["AuditDate"].ToString());
                    }
                    model.AuditInfo = dt.Rows[n]["AuditInfo"].ToString();
                    if (dt.Rows[n]["AuditFlag"].ToString() != "")
                    {
                        model.AuditFlag = int.Parse(dt.Rows[n]["AuditFlag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.FCode = dt.Rows[n]["FCode"].ToString();
                    model.FName = dt.Rows[n]["FName"].ToString();
                    model.ChangeType = dt.Rows[n]["ChangeType"].ToString();
                    model.ChangeTypeName = dt.Rows[n]["ChangeTypeName"].ToString();
                    model.OldCode = dt.Rows[n]["OldCode"].ToString();
                    model.OldName = dt.Rows[n]["OldName"].ToString();
                    model.NewCode = dt.Rows[n]["NewCode"].ToString();
                    model.NewName = dt.Rows[n]["NewName"].ToString();


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