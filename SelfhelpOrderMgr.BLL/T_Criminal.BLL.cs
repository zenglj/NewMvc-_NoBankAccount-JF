﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Criminal
    public partial class T_CriminalBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_CriminalDAL dal = new SelfhelpOrderMgr.DAL.T_CriminalDAL();
        public T_CriminalBLL()
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
        public void Add(SelfhelpOrderMgr.Model.T_Criminal model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Criminal model)
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
        public SelfhelpOrderMgr.Model.T_Criminal GetModel(string FCode)
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
        public List<SelfhelpOrderMgr.Model.T_Criminal> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Criminal> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Criminal> modelList = new List<SelfhelpOrderMgr.Model.T_Criminal>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Criminal model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Criminal();
                    model.FCode = dt.Rows[n]["FCode"].ToString();
                    if (dt.Rows[n]["FInDate"].ToString() != "")
                    {
                        model.FInDate = DateTime.Parse(dt.Rows[n]["FInDate"].ToString());
                    }
                    if (dt.Rows[n]["FOuDate"].ToString() != "")
                    {
                        model.FOuDate = DateTime.Parse(dt.Rows[n]["FOuDate"].ToString());
                    }
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    model.FSubArea = dt.Rows[n]["FSubArea"].ToString();
                    model.FDesc = dt.Rows[n]["FDesc"].ToString();
                    if (dt.Rows[n]["FStatus"].ToString() != "")
                    {
                        model.FStatus = int.Parse(dt.Rows[n]["FStatus"].ToString());
                    }
                    if (dt.Rows[n]["FStatus2"].ToString() != "")
                    {
                        model.FStatus2 = int.Parse(dt.Rows[n]["FStatus2"].ToString());
                    }
                    model.FAddr_tmp = dt.Rows[n]["FAddr_tmp"].ToString();
                    model.FCZY = dt.Rows[n]["FCZY"].ToString();
                    if (dt.Rows[n]["fflag"].ToString() != "")
                    {
                        model.fflag = int.Parse(dt.Rows[n]["fflag"].ToString());
                    }
                    model.FName = dt.Rows[n]["FName"].ToString();
                    if (dt.Rows[n]["flimitflag"].ToString() != "")
                    {
                        model.flimitflag = int.Parse(dt.Rows[n]["flimitflag"].ToString());
                    }
                    if (dt.Rows[n]["flimitamt"].ToString() != "")
                    {
                        model.flimitamt = decimal.Parse(dt.Rows[n]["flimitamt"].ToString());
                    }
                    model.Frealareacode = dt.Rows[n]["Frealareacode"].ToString();
                    if (dt.Rows[n]["amount"].ToString() != "")
                    {
                        model.amount = decimal.Parse(dt.Rows[n]["amount"].ToString());
                    }
                    if (dt.Rows[n]["TP_YingYangCan_Money"].ToString() != "")
                    {
                        model.TP_YingYangCan_Money = decimal.Parse(dt.Rows[n]["TP_YingYangCan_Money"].ToString());
                    }
                    if (dt.Rows[n]["RSB_Flag"].ToString() != "")
                    {
                        model.RSB_Flag = int.Parse(dt.Rows[n]["RSB_Flag"].ToString());
                    }
                    model.FIdenNo = dt.Rows[n]["FIdenNo"].ToString();
                    if (dt.Rows[n]["FAge"].ToString() != "")
                    {
                        model.FAge = int.Parse(dt.Rows[n]["FAge"].ToString());
                    }
                    model.FSex = dt.Rows[n]["FSex"].ToString();
                    model.FAddr = dt.Rows[n]["FAddr"].ToString();
                    model.FCrimeCode = dt.Rows[n]["FCrimeCode"].ToString();
                    model.FCYCode = dt.Rows[n]["FCYCode"].ToString();
                    model.FTerm = dt.Rows[n]["FTerm"].ToString();


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