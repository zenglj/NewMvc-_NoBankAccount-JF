using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_SHO_FinancePayBLL
    {

        //增加Pay主单并更新Vcrd记录
        public T_SHO_FinancePay AddPayOrder(T_SHO_FinancePay paymodel, string strWhere)
        {
            return new T_SHO_FinancePayDAL().AddPayOrder(paymodel, strWhere);
        }

        //==============================================================
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney(string pid)
        {
            return new T_ProvideDAL().UpdateByCountMoney(pid);
        }

        /// <summary>
        /// 根据用户'Bid'确认提交主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCheckFlag(T_Provide model)
        {
            return new T_ProvideDAL().UpdateByCheckFlag(model);
        }

        /// <summary>
        /// 按Bid删除一批DTL数据
        /// </summary>
        public bool DeleteDtlByPid(string pid)
        {
            return new T_ProvideDAL().DeleteDtlByPid(pid);
        }

        /// <summary>
        /// 获得所在dtl数据列表DataTable
        /// </summary>
        public DataTable GetDtlDataTableByPid(string pid)
        {
            return new T_ProvideDAL().GetDtlDataTableByPid(pid);
        }

        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable(string pid)
        {
            return new T_ProvideDAL().GetErrListDataTable(pid);
        }

        public int GetSendCountByPid(string fcrimecode, string pdate)
        {
            return new T_ProvideDAL().GetSendCountByPid(fcrimecode, pdate);
        }

        public bool Add(T_Provide model, string partParameter)
        {
            return new T_ProvideDAL().Add(model, partParameter);
        }

        /// <summary>
        /// 获得劳报主单当月发放次数
        /// </summary>
        public int GetSendCountByArea(string areaName, string udate)
        {
            return new T_ProvideDAL().GetSendCountByArea(areaName, udate);
        }

        /// <summary>
        /// 创建主单号
        /// </summary>
        public string CreateOrderId(string seqnoType)
        {
            return new T_ProvideDAL().CreateOrderId(seqnoType);
        }

        //批量生成报酬金额明细
        public bool BatchCreateAreaList(string pid, string crtby)
        {
            return new T_ProvideDAL().BatchCreateAreaList(pid, crtby);
        }

        //财务过账
        public bool UpdateInDbData(string pid, string crtby, int intAcctype=0)
        {
            return new T_ProvideDAL().UpdateInDbData(pid, crtby, intAcctype);
        }
    }
}