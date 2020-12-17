using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace SelfhelpOrderMgr.BLL
{
    public partial class T_BONUSBLL
    {
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney( string bid)
        {
            return new T_BONUSDAL().UpdateByCountMoney( bid);
        }

        /// <summary>
        /// 根据用户'Bid'确认提交主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCheckFlag( T_BONUS model)
        {
            return new T_BONUSDAL().UpdateByCheckFlag(model);
        }

        /// <summary>
        /// 根据用户'Bid'审核主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByAuditFlag(T_BONUS model)
        {
            return new T_BONUSDAL().UpdateByAuditFlag(model);
        }

        public string CheckOutPrisonList(string bid)
        {
            return new T_BONUSDAL().CheckOutPrisonList(bid);
        }
        /// <summary>
        /// 根据用户'Bid'财务复核主单
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByDbCheckFlag(T_BONUS model)
        {
            return new T_BONUSDAL().UpdateByDbCheckFlag(model);
        }

        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账'
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(int excelModel_Id,string bid, string crtby,int checkflag)
        {
            if (excelModel_Id == 1)
            {
                return new T_BONUSDAL().UpdateInDbFlag(bid, crtby,checkflag);
            }
            else if (excelModel_Id == 2)
            {
                return new T_BONUSDAL().UpdateInDbFlag_Model2(bid, crtby,checkflag);
            }
            else if (excelModel_Id == 3)
            {
                return new T_BONUSDAL().UpdateInDbFlag(bid, crtby, checkflag);
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账',只写入到VCrd，T_Criminal_Card 没有增加
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbOnlyVcrd(string bid, string crtby)
        {
            return new T_BONUSDAL().UpdateInDbOnlyVcrd(bid, crtby);
        }

        /// <summary>
        /// 主单退账
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        public bool UndoMainOrder(string bid)
        {
            return new T_BONUSDAL().UndoMainOrder(bid);

        }

        /// <summary>
        /// 批量更新莆田劳动报酬的银行返回金额
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool PTUpdateVcrdAndWriteCard(string bid, string crtby)
        {
            List<T_SHO_BankReturnList> brl = new T_SHO_BankReturnListBLL().GetModelList("bid='" + bid + "'");
            return new T_BONUSDAL().PTUpdateVcrdAndWriteCard(bid, crtby, brl);
        }

        public bool PLWriteBonusDtl(string bid, string crtby, int reSaveFlag = 0)
        {
            return new T_BONUSDAL().PLWriteBonusDtl(bid, crtby, reSaveFlag);
        }
        public bool PLWriteBonusDtl_Model2(string bid, string crtby, int reSaveFlag = 0)
        {
            return new T_BONUSDAL().PLWriteBonusDtl_Model2(bid, crtby, reSaveFlag);
        }

        /// <summary>
        /// 按Bid删除一批DTL数据
        /// </summary>
        public bool DeleteDtlByBid(string bid)
        {
            return new T_BONUSDAL().DeleteDtlByBid( bid);
        }

        /// <summary>
        /// 获得所在dtl数据列表DataTable
        /// </summary>
        public DataTable GetDtlDataTableByBid( string bid)
        {
            return new T_BONUSDAL().GetDtlDataTableByBid( bid);
        }

        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable(string bid)
        {
            return new T_BONUSDAL().GetErrListDataTable(bid);
        }

        public int GetSendCountByBid( string fcrimecode, string udate)
        {
            return new T_BONUSDAL().GetSendCountByBid( fcrimecode, udate);
        }

        public bool Add( T_BONUS model, string partParameter)
        {
            return new T_BONUSDAL().Add(model, partParameter);
        }

        /// <summary>
        /// 获得劳报主单当月发放次数
        /// </summary>
        public int GetSendCountByArea( string areaName, string udate)
        {
            return new T_BONUSDAL().GetSendCountByArea( areaName, udate);
        }

        /// <summary>
        /// 创建主单号
        /// </summary>
        public string CreateOrderId( string seqnoType)
        {
            return new T_BONUSDAL().CreateOrderId( seqnoType);
        }



        //批量生成报酬金额明细
        public bool BatchCreateAreaList(string bid, string crtby)
        {
            return new T_BONUSDAL().BatchCreateAreaList(bid,crtby);
        }

        public List<T_BONUSDTL> GetDtlPageList(int page, int pageRow, string strWhere, string orderByField)
        {
            return new T_BONUSDAL().GetDtlPageList(page,pageRow,strWhere,orderByField);
        }


        public decimal[] GetDtlListCount(string strWhere)
        {
            return new T_BONUSDAL().GetDtlListCount(strWhere);
        }

    }
}