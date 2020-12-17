using SelfhelpOrderMgr.DAL;
using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public partial class T_TempLeavePrisonBLL
    {
        public IEnumerable<T_TempLeavePrison> GetLeavePrison(string czyCode, string startDate, string endDate, string FAreaCode,string fcode,string fname)
        {
            return new T_TempLeavePrisonDAL().GetLeavePrison(czyCode, startDate, endDate, FAreaCode,fcode,fname);
        }



        public string ExcuteStoredProcedure(string fcrimecode, string crtby)
        {
            return new T_TempLeavePrisonDAL().ExcuteStoredProcedure( fcrimecode,  crtby);
        }

        //中行无卡结算模式
        public string ExcuteStoredProc_NoBankCard(string fcrimecode, string crtby, int modeFlag)
        {
            return new T_TempLeavePrisonDAL().ExcuteStoredProc_NoBankCard(fcrimecode, crtby,modeFlag);
        }
        public string InsertBankProve(string fcode,int payMode)
        {
            return new T_TempLeavePrisonDAL().InsertBankProve(fcode,payMode);
        }

        //挂失并插入结算记录
        public string SetLossAndInsertBankProve(string fcode)
        {
            return new T_TempLeavePrisonDAL().SetLossAndInsertBankProve(fcode);
        }
    }
}