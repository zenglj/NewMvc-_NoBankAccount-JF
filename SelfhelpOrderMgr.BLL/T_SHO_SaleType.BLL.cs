﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_SHO_SaleType
    public partial class T_SHO_SaleTypeBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SHO_SaleTypeDAL dal = new SelfhelpOrderMgr.DAL.T_SHO_SaleTypeDAL();
        public T_SHO_SaleTypeBLL()
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
        public void Add(SelfhelpOrderMgr.Model.T_SHO_SaleType model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_SHO_SaleType model)
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
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_SHO_SaleType GetModel(int ID)
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
        public List<SelfhelpOrderMgr.Model.T_SHO_SaleType> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_SHO_SaleType> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_SHO_SaleType> modelList = new List<SelfhelpOrderMgr.Model.T_SHO_SaleType>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_SHO_SaleType model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_SHO_SaleType();
                    if (dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    model.PType = dt.Rows[n]["PType"].ToString();
                    if (dt.Rows[n]["TypeFlagId"].ToString() != "")
                    {
                        model.TypeFlagId = int.Parse(dt.Rows[n]["TypeFlagId"].ToString());
                    }
                    if (dt.Rows[n]["CanconsumeAccount"].ToString() != "")
                    {
                        model.CanconsumeAccount = int.Parse(dt.Rows[n]["CanconsumeAccount"].ToString());
                    }
                    if (dt.Rows[n]["FirstPaymentAccount"].ToString() != "")
                    {
                        model.FirstPaymentAccount = int.Parse(dt.Rows[n]["FirstPaymentAccount"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    if (dt.Rows[n]["ShoppingFlag"].ToString() != "")
                    {
                        model.ShoppingFlag = int.Parse(dt.Rows[n]["ShoppingFlag"].ToString());
                    }
                    if (dt.Rows[n]["Fifoflag"].ToString() != "")
                    {
                        model.Fifoflag = int.Parse(dt.Rows[n]["Fifoflag"].ToString());
                    }

                    if (dt.Rows[n]["UseType"].ToString() != "")
                    {
                        model.UseType = int.Parse(dt.Rows[n]["UseType"].ToString());
                    }

                    model.ControlName = dt.Rows[n]["ControlName"].ToString();

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