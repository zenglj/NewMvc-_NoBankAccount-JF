using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public class BaseDapperBLLExtend:BaseDapperBLL
    {
        /// <summary>
        /// 检测用户是否有该监区的管理权限
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="areaCode">监区号</param>
        /// <returns></returns>
        public bool CheckAreaPower(string userCode,string areaCode)
        {
            List<T_Czy_area> userareas = this.QueryList<T_Czy_area>("select * from T_Czy_area where FCode=@FCode and FFlag=2 and FAreaCode=@FAreaCode", new { FCode = userCode, FAreaCode = areaCode });
            if (userareas.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检测管理卡是否有该监区的管理权限
        /// </summary>
        /// <param name="fmanagerCard"></param>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        public bool CheckManagerCardAreaPower(string fmanagerCard, string areaCode)
        {
            T_CZY user = this.QueryModel<T_CZY>("FManagerCard", fmanagerCard);
            if (user == null)
            {
                return false;
            }

            return CheckAreaPower(user.FCode, areaCode);
        }
    }
}