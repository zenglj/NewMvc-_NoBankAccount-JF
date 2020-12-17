using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Goods
    public partial class T_GoodsBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_GoodsDAL dal = new SelfhelpOrderMgr.DAL.T_GoodsDAL();
        public T_GoodsBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string GTXM)
        {
            return dal.Exists(GTXM);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SelfhelpOrderMgr.Model.T_Goods model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Goods model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string GTXM)
        {

            return dal.Delete(GTXM);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Goods GetModel(string GTXM)
        {

            return dal.GetModel(GTXM);
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
        public List<SelfhelpOrderMgr.Model.T_Goods> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Goods> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Goods> modelList = new List<SelfhelpOrderMgr.Model.T_Goods>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Goods model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Goods();
                    model.GCODE = dt.Rows[n]["GCODE"].ToString();
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["Crtdt"].ToString() != "")
                    {
                        model.Crtdt = dt.Rows[n]["Crtdt"].ToString();
                    }
                    model.ModBy = dt.Rows[n]["ModBy"].ToString();
                    if (dt.Rows[n]["Moddt"].ToString() != "")
                    {
                        model.Moddt = dt.Rows[n]["Moddt"].ToString();
                    }
                    if (dt.Rows[n]["GBalance"].ToString() != "")
                    {
                        model.GBalance = decimal.Parse(dt.Rows[n]["GBalance"].ToString());
                    }
                    model.ACTIVE = dt.Rows[n]["ACTIVE"].ToString();
                    if (dt.Rows[n]["COMBFLAG"].ToString() != "")
                    {
                        model.COMBFLAG = int.Parse(dt.Rows[n]["COMBFLAG"].ToString());
                    }
                    if (dt.Rows[n]["gindj"].ToString() != "")
                    {
                        model.gindj = decimal.Parse(dt.Rows[n]["gindj"].ToString());
                    }
                    if (dt.Rows[n]["subflag"].ToString() != "")
                    {
                        model.subflag = int.Parse(dt.Rows[n]["subflag"].ToString());
                    }
                    model.madein = dt.Rows[n]["madein"].ToString();
                    model.GNAME = dt.Rows[n]["GNAME"].ToString();
                    if (dt.Rows[n]["Ffreeflag"].ToString() != "")
                    {
                        model.Ffreeflag = int.Parse(dt.Rows[n]["Ffreeflag"].ToString());
                    }
                    if (dt.Rows[n]["balflag"].ToString() != "")
                    {
                        model.balflag = int.Parse(dt.Rows[n]["balflag"].ToString());
                    }
                    if (dt.Rows[n]["Serviceflag"].ToString() != "")
                    {
                        model.Serviceflag = int.Parse(dt.Rows[n]["Serviceflag"].ToString());
                    }
                    model.gjm = dt.Rows[n]["gjm"].ToString();
                    model.src = dt.Rows[n]["src"].ToString();
                    model.data = dt.Rows[n]["data"].ToString();
                    if (dt.Rows[n]["Xgsl"].ToString() != "")
                    {
                        model.Xgsl = int.Parse(dt.Rows[n]["Xgsl"].ToString());
                    }
                    if (dt.Rows[n]["XgMode"].ToString() != "")
                    {
                        model.XgMode = int.Parse(dt.Rows[n]["XgMode"].ToString());
                    }
                    model.GTYPE = dt.Rows[n]["GTYPE"].ToString();
                    model.GUnit = dt.Rows[n]["GUnit"].ToString();
                    model.GStandard = dt.Rows[n]["GStandard"].ToString();
                    if (dt.Rows[n]["GDJ"].ToString() != "")
                    {
                        model.GDJ = decimal.Parse(dt.Rows[n]["GDJ"].ToString());
                    }
                    model.GSupplyer = dt.Rows[n]["GSupplyer"].ToString();
                    model.GTXM = dt.Rows[n]["GTXM"].ToString();
                    model.SPShortCode = dt.Rows[n]["SPShortCode"].ToString();


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