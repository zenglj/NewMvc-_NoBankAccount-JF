using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using SelfhelpOrderMgr.Model;
namespace SelfhelpOrderMgr.BLL
{
    //T_Vcrd
    public partial class T_VcrdBLL
    {

        private readonly SelfhelpOrderMgr.DAL.T_VcrdDAL dal = new SelfhelpOrderMgr.DAL.T_VcrdDAL();
        public T_VcrdBLL()
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
        public int Add(SelfhelpOrderMgr.Model.T_Vcrd model)
        {
            return dal.Add(model);

        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Vcrd model)
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
        public SelfhelpOrderMgr.Model.T_Vcrd GetModel(int seqno)
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
        public List<SelfhelpOrderMgr.Model.T_Vcrd> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Vcrd> DataTableToList(DataTable dt)
        {
            List<SelfhelpOrderMgr.Model.T_Vcrd> modelList = new List<SelfhelpOrderMgr.Model.T_Vcrd>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SelfhelpOrderMgr.Model.T_Vcrd model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new SelfhelpOrderMgr.Model.T_Vcrd();
                    model.Vouno = dt.Rows[n]["Vouno"].ToString();
                    model.Remark = dt.Rows[n]["Remark"].ToString();
                    if (dt.Rows[n]["Flag"].ToString() != "")
                    {
                        model.Flag = int.Parse(dt.Rows[n]["Flag"].ToString());
                    }
                    model.DelBy = dt.Rows[n]["DelBy"].ToString();
                    if (dt.Rows[n]["DelDate"].ToString() != "")
                    {
                        model.DelDate = DateTime.Parse(dt.Rows[n]["DelDate"].ToString());
                    }
                    model.FAreaCode = dt.Rows[n]["FAreaCode"].ToString();
                    model.FAreaName = dt.Rows[n]["FAreaName"].ToString();
                    model.FCriminal = dt.Rows[n]["FCriminal"].ToString();
                    model.Frealareacode = dt.Rows[n]["Frealareacode"].ToString();
                    model.FrealAreaName = dt.Rows[n]["FrealAreaName"].ToString();
                    model.PType = dt.Rows[n]["PType"].ToString();
                    model.CardCode = dt.Rows[n]["CardCode"].ToString();
                    if (dt.Rows[n]["UDate"].ToString() != "")
                    {
                        model.UDate = DateTime.Parse(dt.Rows[n]["UDate"].ToString());
                    }
                    model.OrigId = dt.Rows[n]["OrigId"].ToString();
                    if (dt.Rows[n]["CardType"].ToString() != "")
                    {
                        model.CardType = int.Parse(dt.Rows[n]["CardType"].ToString());
                    }
                    if (dt.Rows[n]["TypeFlag"].ToString() != "")
                    {
                        model.TypeFlag = int.Parse(dt.Rows[n]["TypeFlag"].ToString());
                    }
                    if (dt.Rows[n]["AccType"].ToString() != "")
                    {
                        model.AccType = int.Parse(dt.Rows[n]["AccType"].ToString());
                    }
                    if (dt.Rows[n]["SendDate"].ToString() != "")
                    {
                        model.SendDate = DateTime.Parse(dt.Rows[n]["SendDate"].ToString());
                    }
                    if (dt.Rows[n]["BankFlag"].ToString() != "")
                    {
                        model.BankFlag = int.Parse(dt.Rows[n]["BankFlag"].ToString());
                    }
                    if (dt.Rows[n]["CheckFlag"].ToString() != "")
                    {
                        model.CheckFlag = int.Parse(dt.Rows[n]["CheckFlag"].ToString());
                    }
                    if (dt.Rows[n]["CheckDate"].ToString() != "")
                    {
                        model.CheckDate = DateTime.Parse(dt.Rows[n]["CheckDate"].ToString());
                    }
                    model.CheckBy = dt.Rows[n]["CheckBy"].ToString();
                    model.FCrimeCode = dt.Rows[n]["FCrimeCode"].ToString();
                    if (dt.Rows[n]["pc"].ToString() != "")
                    {
                        model.pc = int.Parse(dt.Rows[n]["pc"].ToString());
                    }
                    if (dt.Rows[n]["seqno"].ToString() != "")
                    {
                        model.seqno = int.Parse(dt.Rows[n]["seqno"].ToString());
                    }
                    if (dt.Rows[n]["SubTypeFlag"].ToString() != "")
                    {
                        model.SubTypeFlag = int.Parse(dt.Rows[n]["SubTypeFlag"].ToString());
                    }
                    if (dt.Rows[n]["RcvDate"].ToString() != "")
                    {
                        model.RcvDate = DateTime.Parse(dt.Rows[n]["RcvDate"].ToString());
                    }
                    if (dt.Rows[n]["CurUserAmount"].ToString() != "")
                    {
                        model.CurUserAmount = decimal.Parse(dt.Rows[n]["CurUserAmount"].ToString());
                    }
                    if (dt.Rows[n]["CurAllAmount"].ToString() != "")
                    {
                        model.CurAllAmount = decimal.Parse(dt.Rows[n]["CurAllAmount"].ToString());
                    }
                    if (dt.Rows[n]["bankRcvFlag"].ToString() != "")
                    {
                        model.bankRcvFlag = int.Parse(dt.Rows[n]["bankRcvFlag"].ToString());
                    }
                    if (dt.Rows[n]["FinancePayId"].ToString() != "")
                    {
                        model.FinancePayId = int.Parse(dt.Rows[n]["FinancePayId"].ToString());
                    }
                    if (dt.Rows[n]["FinancePayFlag"].ToString() != "")
                    {
                        model.FinancePayFlag = int.Parse(dt.Rows[n]["FinancePayFlag"].ToString());
                    }
                    if (dt.Rows[n]["BankInterfaceFlag"].ToString() != "")
                    {
                        model.BankInterfaceFlag = int.Parse(dt.Rows[n]["BankInterfaceFlag"].ToString());
                    }
                    if (dt.Rows[n]["DAmount"].ToString() != "")
                    {
                        model.DAmount = decimal.Parse(dt.Rows[n]["DAmount"].ToString());
                    }
                    if (dt.Rows[n]["PayAuditFlag"].ToString() != "")
                    {
                        model.PayAuditFlag = int.Parse(dt.Rows[n]["PayAuditFlag"].ToString());
                    }
                    if (dt.Rows[n]["CAmount"].ToString() != "")
                    {
                        model.CAmount = decimal.Parse(dt.Rows[n]["CAmount"].ToString());
                    }
                    model.CrtBy = dt.Rows[n]["CrtBy"].ToString();
                    if (dt.Rows[n]["CrtDate"].ToString() != "")
                    {
                        model.CrtDate = DateTime.Parse(dt.Rows[n]["CrtDate"].ToString());
                    }

                    model.DType = dt.Rows[n]["DType"].ToString();
                    model.Depositer = dt.Rows[n]["Depositer"].ToString();

                    if (dt.Rows[n]["PayMode"].ToString() != "")
                    {
                        model.PayMode = int.Parse(dt.Rows[n]["PayMode"].ToString());
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