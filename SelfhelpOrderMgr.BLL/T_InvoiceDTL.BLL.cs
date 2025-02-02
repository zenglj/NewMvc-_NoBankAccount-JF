﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.BLL
{
    //T_InvoiceDTL
    public partial class T_InvoiceDTLBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_InvoiceDTLDAL dal = new SelfhelpOrderMgr.DAL.T_InvoiceDTLDAL();
        public T_InvoiceDTLBLL()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int seqno)
        {
            return dal.Exists(seqno);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_InvoiceDTL model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_InvoiceDTL model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int seqno)
        {

            return dal.Delete(seqno);
        }
        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string seqnolist)
        {
            return dal.DeleteList(seqnolist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_InvoiceDTL GetModel(int seqno)
        {

            return dal.GetModel(seqno);
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
        public List<SelfhelpOrderMgr.Model.T_InvoiceDTL> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_InvoiceDTL> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_InvoiceDTL> modelList = new List<SelfhelpOrderMgr.Model.T_InvoiceDTL>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_InvoiceDTL model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_InvoiceDTL();
                    if (dt.Rows[n]["seqno"].ToString() != "")
                    {
                        model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
                    }
                    if (dt.Rows[n]["AMOUNT"].ToString() != "")
                    {
                        model.AMOUNT = decimal.Parse(dt.Rows[n]["AMOUNT"].ToString());
                    }
                    if (dt.Rows[n]["Servamount"].ToString() != "")
                    {
                        model.Servamount = decimal.Parse(dt.Rows[n]["Servamount"].ToString());
                    }
                    model.GTXM = dt.Rows[n]["GTXM"].ToString();
                    model.FCrimecode = dt.Rows[n]["FCrimecode"].ToString();
                    if (dt.Rows[n]["GORGDJ"].ToString() != "")
                    {
                        model.GORGDJ = decimal.Parse(dt.Rows[n]["GORGDJ"].ToString());
                    }
                    if (dt.Rows[n]["GORGAMT"].ToString() != "")
                    {
                        model.GORGAMT = decimal.Parse(dt.Rows[n]["GORGAMT"].ToString());
                    }
                    if (dt.Rows[n]["StockSeqno"].ToString() != "")
                    {
                        model.StockSeqno = int.Parse(dt.Rows[n]["StockSeqno"].ToString());
                    }
                    if (dt.Rows[n]["Typeflag"].ToString() != "")
                    {
                        model.Typeflag = int.Parse(dt.Rows[n]["Typeflag"].ToString());
                    }
                    if (dt.Rows[n]["Cardtype"].ToString() != "")
                    {
                        model.Cardtype = int.Parse(dt.Rows[n]["Cardtype"].ToString());
                    }
                    if (dt.Rows[n]["AmountA"].ToString() != "")
                    {
                        model.AmountA = decimal.Parse(dt.Rows[n]["AmountA"].ToString());
                    }
                    model.INVOICENO = dt.Rows[n]["INVOICENO"].ToString();
                    if (dt.Rows[n]["AmountB"].ToString() != "")
                    {
                        model.AmountB = decimal.Parse(dt.Rows[n]["AmountB"].ToString());
                    }
                    if (dt.Rows[n]["Fifoflag"].ToString() != "")
                    {
                        model.Fifoflag = int.Parse(dt.Rows[n]["Fifoflag"].ToString());
                    }
                    if (dt.Rows[n]["Backqty"].ToString() != "")
                    {
                        model.Backqty = decimal.Parse(dt.Rows[n]["Backqty"].ToString());
                    }
                    if (dt.Rows[n]["FreeFlag"].ToString() != "")
                    {
                        model.FreeFlag = int.Parse(dt.Rows[n]["FreeFlag"].ToString());
                    }
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    model.SPShortCode = dt.Rows[n]["SPShortCode"].ToString();
                    if (dt.Rows[n]["FTZSP_TypeFlag"].ToString() != "")
                    {
                        model.FTZSP_TypeFlag = int.Parse(dt.Rows[n]["FTZSP_TypeFlag"].ToString());
                    }
                    model.GCODE = dt.Rows[n]["GCODE"].ToString();
                    model.GNAME = dt.Rows[n]["GNAME"].ToString();
                    if (dt.Rows[n]["OrderDate"].ToString() != "")
                    {
                        model.OrderDate = DateTime.Parse(dt.Rows[n]["OrderDate"].ToString());
                    }
                    if (dt.Rows[n]["PayDATE"].ToString() != "")
                    {
                        model.PayDATE = DateTime.Parse(dt.Rows[n]["PayDATE"].ToString());
                    }
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    if (dt.Rows[n]["GDJ"].ToString() != "")
                    {
                        model.GDJ = decimal.Parse(dt.Rows[n]["GDJ"].ToString());
                    }
                    if (dt.Rows[n]["QTY"].ToString() != "")
                    {
                        model.QTY = decimal.Parse(dt.Rows[n]["QTY"].ToString());
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