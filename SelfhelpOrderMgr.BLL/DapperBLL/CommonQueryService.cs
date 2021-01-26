using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.BLL
{
    public static class CommonQueryService
    {
        /// <summary>
        /// 获取用户队别查询权限的查询条件脚本
        /// </summary>
        /// <param name="userLoginCode"></param>
        /// <returns></returns>
        public static string GetUserAreaPowerString(string userLoginCode)
        {
            T_SHO_ManagerSet mfzyset = new T_SHO_ManagerSetBLL().GetModel("VcrdCheckUserManagerAarea");
            string otherQuery = "";
            if (mfzyset != null)
            {
                if (mfzyset.MgrValue == "1")
                {
                    otherQuery = " FAreaCode in (  select fareaCode from t_czy_area where fflag=2 and fcode='" + userLoginCode + "')";
                }
            }
            return otherQuery;
        }

        /// <summary>
        /// 获取监狱单位名称
        /// </summary>
        /// <returns></returns>
        public static string GetPrisonUnitName()
        {
            string unitName = "";
            T_SHO_ManagerSet mset = new T_SHO_ManagerSetBLL().GetModel("back-stageMgrTitle");

            if (mset != null)
            {
                unitName = mset.MgrValue.Substring(0, 4);
            }
            return unitName;
        }

        public static string SavePostExcelFile(HttpPostedFileBase f)
        {            
            string fname = f.FileName;
            /* startIndex */
            int index = fname.LastIndexOf("\\") + 1;
            /* length */
            int len = fname.Length - index;
            fname = fname.Substring(index, len);
            /* save to server */

            //string savePath = Server.MapPath("~/Upload/" + fname);
            string savePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase+"/Upload/ " + fname;
            f.SaveAs(savePath);
            return savePath;
        }

    }
}