using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_VcrdBLL
    {
        public List<T_Vcrd> GetPageList(int page, int pageRow, string strWhere,string orderByField)
        {
            return new T_VcrdDAL().GetPageList(page, pageRow, strWhere, orderByField);
        }
        public decimal[] GetListCount(string strWhere)
        {
            return new T_VcrdDAL().GetListCount(strWhere);
        }

        public List<T_CommonTypeTab> GetFinaType(int iType)
        {
            return new T_VcrdDAL().GetFinaType(iType);
        }

        public List<T_Vcrd> UserCunKouKuan(string fcrimecode, int flag, decimal fmoney, T_Savetype savetype, string crtby, string remark, string apply,string pkId,int checkFlag =0)
        {
            T_Criminal criminal =new T_CriminalBLL().GetCriminalXE_info(fcrimecode,1);
            return new T_VcrdDAL().UserCunKouKuan(criminal, flag, fmoney, savetype, crtby, remark, apply, pkId, checkFlag);
        }

        public bool UpdateCheckFlag(string OutFsn)//更新消费单的配货状态
        {
            return new T_VcrdDAL().UpdateCheckFlag(OutFsn);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<SelfhelpOrderMgr.Model.T_Vcrd> GetModelList(int topNumber, string strWhere, string filedOrder)
        {
            DataSet ds = dal.GetList(topNumber, strWhere, filedOrder);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 软删除存/取款记录
        /// </summary>
        /// <param name="fcode"></param>
        /// <param name="seqno"></param>
        /// <param name="delUserName"></param>
        /// <returns></returns>
        public bool SoftDeleteVcrd(string fcode, int seqno, string delUserName)//软删除存/取款记录
        {
            return dal.SoftDeleteVcrd(fcode, seqno, delUserName);
        }

        /// <summary>
        /// 自定义查询直接传入SQL
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public List<T_Vcrd> CustomerQuery(string strSql, object paramInfo)
        {
            return dal.CustomerQuery(strSql,paramInfo);
        }

        public bool ChangeVcrdListType(string invoiceNo, string dtype,int subTypeFlag)
        {
            return dal.ChangeVcrdListType(invoiceNo, dtype, subTypeFlag);
        }
        
        public bool DeleteDtlByVcrdSeqno(T_Vcrd vcrd)
        {
            return dal.DeleteDtlByVcrdSeqno(vcrd);
        }
    }
}