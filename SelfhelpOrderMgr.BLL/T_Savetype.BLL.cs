﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Savetype
    public partial class T_SavetypeBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_SavetypeDAL dal = new SelfhelpOrderMgr.DAL.T_SavetypeDAL();
        public T_SavetypeBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int fcode, int typeflag)
        {
            return dal.Exists(fcode, typeflag);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Savetype model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Savetype model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int fcode, int typeflag)
        {

            return dal.Delete(fcode, typeflag);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Savetype GetModel(int fcode, int typeflag)
        {

            return dal.GetModel(fcode, typeflag);
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
        public List<SelfhelpOrderMgr.Model.T_Savetype> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Savetype> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Savetype> modelList = new List<SelfhelpOrderMgr.Model.T_Savetype>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Savetype model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Savetype();
                    if (dt.Rows[n]["fcode"].ToString() != "")
                    {
                        model.fcode = int.Parse(dt.Rows[n]["fcode"].ToString());
                    }
                    model.fname = dt.Rows[n]["fname"].ToString();
                    if (dt.Rows[n]["typeflag"].ToString() != "")
                    {
                        model.typeflag = int.Parse(dt.Rows[n]["typeflag"].ToString());
                    }
                    if (dt.Rows[n]["PLXE_Flag"].ToString() != "")
                    {
                        model.PLXE_Flag = int.Parse(dt.Rows[n]["PLXE_Flag"].ToString());
                    }
                    if (dt.Rows[n]["ZZKK_Flag"].ToString() != "")
                    {
                        model.ZZKK_Flag = int.Parse(dt.Rows[n]["ZZKK_Flag"].ToString());
                    }
                    if (dt.Rows[n]["AccType"].ToString() != "")
                    {
                        model.AccType = int.Parse(dt.Rows[n]["AccType"].ToString());
                    }
                    if (dt.Rows[n]["FuShuFlag"].ToString() != "")
                    {
                        model.FuShuFlag = int.Parse(dt.Rows[n]["FuShuFlag"].ToString());
                    }
                    if (dt.Rows[n]["UseType"].ToString() != "")
                    {
                        model.UseType = int.Parse(dt.Rows[n]["UseType"].ToString());
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