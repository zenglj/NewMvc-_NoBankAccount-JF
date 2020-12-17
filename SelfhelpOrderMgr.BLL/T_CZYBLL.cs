using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_CZY
    public partial class T_CZYBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_CZYDAL dal = new SelfhelpOrderMgr.DAL.T_CZYDAL();
        public T_CZYBLL()
        { }

        #region  Method


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_CZY model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_CZY model)
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
        public SelfhelpOrderMgr.Model.T_CZY GetModel(string FCode)
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
        public List<SelfhelpOrderMgr.Model.T_CZY> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_CZY> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_CZY> modelList = new List<SelfhelpOrderMgr.Model.T_CZY>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_CZY model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_CZY();
                    model.FCode = dt.Rows[n]["FCode"].ToString();
                    model.FRole = dt.Rows[n]["FRole"].ToString();
                    if (string.IsNullOrEmpty(model.FRole) == false)
                    {
                        model.TreeRole = new t_TreeRoleBLL().GetModel(Convert.ToInt32(model.FRole));
                    }
                    else
                    { 
                        t_TreeRole treerole=new t_TreeRole();
                        treerole.RoleID = 0;
                        treerole.RoleName = "";
                        model.TreeRole = treerole;
                    }

                    if (dt.Rows[n]["fauditflag"].ToString() != "")
                    {
                        model.fauditflag = int.Parse(dt.Rows[n]["fauditflag"].ToString());
                    }
                    if (dt.Rows[n]["fBonusPost"].ToString() != "")
                    {
                        model.fBonusPost = int.Parse(dt.Rows[n]["fBonusPost"].ToString());
                    }
                    model.ver = dt.Rows[n]["ver"].ToString();
                    model.FUserChinaName = dt.Rows[n]["FUserChinaName"].ToString();
                    model.FManagerCard = dt.Rows[n]["FManagerCard"].ToString();
                    model.FName = dt.Rows[n]["FName"].ToString();
                    model.FPwd = dt.Rows[n]["FPwd"].ToString();
                    if (dt.Rows[n]["FFlag"].ToString() != "")
                    {
                        model.FFlag = int.Parse(dt.Rows[n]["FFlag"].ToString());
                    }
                    if (dt.Rows[n]["FPRIVATE"].ToString() != "")
                    {
                        model.FPRIVATE = int.Parse(dt.Rows[n]["FPRIVATE"].ToString());
                    }
                    if (dt.Rows[n]["FSTOCKCHK"].ToString() != "")
                    {
                        model.FSTOCKCHK = int.Parse(dt.Rows[n]["FSTOCKCHK"].ToString());
                    }
                    if (dt.Rows[n]["FINVCHK"].ToString() != "")
                    {
                        model.FINVCHK = int.Parse(dt.Rows[n]["FINVCHK"].ToString());
                    }
                    model.rolecode = dt.Rows[n]["rolecode"].ToString();
                    model.FUserArea = dt.Rows[n]["FUserArea"].ToString();


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