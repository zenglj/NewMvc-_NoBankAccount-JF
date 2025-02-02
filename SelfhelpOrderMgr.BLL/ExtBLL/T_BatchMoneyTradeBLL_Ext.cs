﻿using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_BatchMoneyTradeBLL
    {
        /// <summary>
        /// 更新主单“人数”及“金额”
        /// </summary>
        /// <param name="op"></param>
        public bool UpdateByCountMoney(string bid)
        {
            return new T_BatchMoneyTradeDAL().UpdateByCountMoney(bid);
        }


        public bool PLWriteTradeDtl(string bid, string crtby)
        {
            return new T_BatchMoneyTradeDAL().PLWriteTradeDtl(bid, crtby);
        }


        public List<T_BatchMoneyTrade_DTL> GetDtlPageList(int page, int pageRow, string strWhere, string orderByField)
        {
            return new T_BatchMoneyTradeDAL().GetDtlPageList(page, pageRow, strWhere, orderByField);
        }


        public decimal[] GetDtlListCount(string strWhere)
        {
            return new T_BatchMoneyTradeDAL().GetDtlListCount(strWhere);
        }

        /// <summary>
        /// 获得所在dtl数据列表DataTable
        /// </summary>
        public DataTable GetDtlDataTableByBid(string bid)
        {
            return new T_BatchMoneyTradeDAL().GetDtlDataTableByBid(bid);
        }


        /// <summary>
        /// 获得所在ErrList数据列表DataTable
        /// </summary>
        public DataTable GetErrListDataTable(string bid)
        {
            return new T_BatchMoneyTradeDAL().GetErrListDataTable(bid);
        }


        /// <summary>
        /// 劳动报酬，根据 Bid '财务入账'
        /// </summary>
        /// <param name="bid">劳报主单号BID</param>
        /// <param name="crtby">操作员</param>
        /// <returns></returns>
        public bool UpdateInDbFlag(string bid, string crtby)
        {
            return new T_BatchMoneyTradeDAL().UpdateInDbFlag(bid, crtby);
        }


        /// <summary>
        /// 批量删除已经导入的取扣款记录
        /// </summary>
        /// <param name="pkId"></param>
        /// <param name="crtby"></param>
        /// <returns></returns>
        public bool plDeleteByPKId(string pkId, string crtby, int typeflag)
        {
            return new T_BatchMoneyTradeDAL().plDeleteByPKId(pkId, crtby, typeflag);
        }


        /// <summary>
        /// 批量Excel存取款数据导入，并写入数据库
        /// </summary>
        /// <param name="strFBid"></param>
        /// <param name="onlyCheckFlag">是否只是检测不导入Vcrd</param>
        /// <returns>执行的结果值</returns>
        public string PLExcelImport(string strFBid, string onlyCheckFlag)
        {
            return new T_BatchMoneyTradeDAL().PLExcelImport(strFBid, onlyCheckFlag);
        }
    }
}