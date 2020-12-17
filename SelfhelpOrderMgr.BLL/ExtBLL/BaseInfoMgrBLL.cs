using SelfhelpOrderMgr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class BaseInfoMgrBLL
    {
        public string LeavePrisonCheckUserMoney(string fcode)
        {

            string rs= new BaseInfoMgrDAL().LeavePrisonCheckUserMoney(fcode);
            if (rs == "")
            {
                return "OK|";
            }
            else
            {
                return "Err|"+ rs;
            }
        }
    }
}