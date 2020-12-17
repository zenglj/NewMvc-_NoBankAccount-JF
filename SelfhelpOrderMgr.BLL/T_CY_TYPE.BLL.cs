using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_CY_TYPE
    public partial class T_CY_TYPEBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_CY_TYPEDAL dal = new SelfhelpOrderMgr.DAL.T_CY_TYPEDAL();
        public T_CY_TYPEBLL()
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
        public void Add(SelfhelpOrderMgr.Model.T_CY_TYPE model)
        {
            dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_CY_TYPE model)
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
        public SelfhelpOrderMgr.Model.T_CY_TYPE GetModel(string FCode)
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
        public List<SelfhelpOrderMgr.Model.T_CY_TYPE> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_CY_TYPE> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_CY_TYPE> modelList = new List<SelfhelpOrderMgr.Model.T_CY_TYPE>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_CY_TYPE model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_CY_TYPE();
                    model.FCode = dt.Rows[n]["FCode"].ToString();
                    if (dt.Rows[n]["FdayLimitflag"].ToString() != "")
                    {
                        model.FdayLimitflag = int.Parse(dt.Rows[n]["FdayLimitflag"].ToString());
                    }
                    if (dt.Rows[n]["FdaylimitAmt"].ToString() != "")
                    {
                        model.FdaylimitAmt = decimal.Parse(dt.Rows[n]["FdaylimitAmt"].ToString());
                    }
                    if (dt.Rows[n]["FBamtMonth"].ToString() != "")
                    {
                        model.FBamtMonth = decimal.Parse(dt.Rows[n]["FBamtMonth"].ToString());
                    }
                    if (dt.Rows[n]["FbamtmonthFlag"].ToString() != "")
                    {
                        model.FbamtmonthFlag = int.Parse(dt.Rows[n]["FbamtmonthFlag"].ToString());
                    }
                    if (dt.Rows[n]["FAamtmonthflag"].ToString() != "")
                    {
                        model.FAamtmonthflag = int.Parse(dt.Rows[n]["FAamtmonthflag"].ToString());
                    }
                    if (dt.Rows[n]["bpct"].ToString() != "")
                    {
                        model.bpct = decimal.Parse(dt.Rows[n]["bpct"].ToString());
                    }
                    if (dt.Rows[n]["Fbonusflag"].ToString() != "")
                    {
                        model.Fbonusflag = int.Parse(dt.Rows[n]["Fbonusflag"].ToString());
                    }
                    if (dt.Rows[n]["cpct"].ToString() != "")
                    {
                        model.cpct = decimal.Parse(dt.Rows[n]["cpct"].ToString());
                    }
                    if (dt.Rows[n]["ftotamtmonthflag"].ToString() != "")
                    {
                        model.ftotamtmonthflag = int.Parse(dt.Rows[n]["ftotamtmonthflag"].ToString());
                    }
                    if (dt.Rows[n]["ftotamtmonth"].ToString() != "")
                    {
                        model.ftotamtmonth = decimal.Parse(dt.Rows[n]["ftotamtmonth"].ToString());
                    }
                    model.FName = dt.Rows[n]["FName"].ToString();
                    if (dt.Rows[n]["totpct"].ToString() != "")
                    {
                        model.totpct = decimal.Parse(dt.Rows[n]["totpct"].ToString());
                    }
                    model.FPower = dt.Rows[n]["FPower"].ToString();
                    if (dt.Rows[n]["FDinnerAFlag"].ToString() != "")
                    {
                        model.FDinnerAFlag = int.Parse(dt.Rows[n]["FDinnerAFlag"].ToString());
                    }
                    if (dt.Rows[n]["FDinnerBFlag"].ToString() != "")
                    {
                        model.FDinnerBFlag = int.Parse(dt.Rows[n]["FDinnerBFlag"].ToString());
                    }
                    if (dt.Rows[n]["payaccount"].ToString() != "")
                    {
                        model.payaccount = int.Parse(dt.Rows[n]["payaccount"].ToString());
                    }
                    if (dt.Rows[n]["FTZSP_Money"].ToString() != "")
                    {
                        model.FTZSP_Money = decimal.Parse(dt.Rows[n]["FTZSP_Money"].ToString());
                    }
                    if (dt.Rows[n]["FTZSP_Zero_Flag"].ToString() != "")
                    {
                        model.FTZSP_Zero_Flag = int.Parse(dt.Rows[n]["FTZSP_Zero_Flag"].ToString());
                    }
                    if (dt.Rows[n]["JaRi_Cy_Money"].ToString() != "")
                    {
                        model.JaRi_Cy_Money = decimal.Parse(dt.Rows[n]["JaRi_Cy_Money"].ToString());
                    }
                    model.FDesc = dt.Rows[n]["FDesc"].ToString();
                    if (dt.Rows[n]["famtmonth"].ToString() != "")
                    {
                        model.famtmonth = decimal.Parse(dt.Rows[n]["famtmonth"].ToString());
                    }
                    if (dt.Rows[n]["FamtLimit"].ToString() != "")
                    {
                        model.FamtLimit = decimal.Parse(dt.Rows[n]["FamtLimit"].ToString());
                    }
                    if (dt.Rows[n]["fcamtlimit"].ToString() != "")
                    {
                        model.fcamtlimit = decimal.Parse(dt.Rows[n]["fcamtlimit"].ToString());
                    }
                    if (dt.Rows[n]["flag"].ToString() != "")
                    {
                        model.flag = int.Parse(dt.Rows[n]["flag"].ToString());
                    }
                    if (dt.Rows[n]["FLimittype"].ToString() != "")
                    {
                        model.FLimittype = int.Parse(dt.Rows[n]["FLimittype"].ToString());
                    }
                    if (dt.Rows[n]["pct"].ToString() != "")
                    {
                        model.pct = decimal.Parse(dt.Rows[n]["pct"].ToString());
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