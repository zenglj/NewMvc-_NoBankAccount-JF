using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public class BaseInfoMgrDAL
    {
        public string LeavePrisonCheckUserMoney(string fcode)
        {
            string rs = "";//返回的结果
            string strSql = @"select fcrimecode,sum(damount) damount,sum(camount) camount from t_vcrd where flag=0 and isnull(bankflag,0)<2
                            and fcrimecode=@fcrimecode
                            group by fcrimecode
                            ";

            SqlParameter[] parametersNew = {
					new SqlParameter("@fcrimecode", SqlDbType.VarChar,20)};
            parametersNew[0].Value = fcode;
            DataSet ds=SqlHelper.Query(strSql.ToString(), parametersNew);
            if (ds == null || ds.Tables[0].Rows.Count == 0)
            {
                rs = "";
            }
            else
            {
                if (Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString()) > 0 && Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()) > 0)
                {
                    rs = string.Format("【扣款】未成功金额:{0:C}元，【存款】未成功金额:{1:C}元", ds.Tables[0].Rows[0][1], ds.Tables[0].Rows[0][2]);
                }
                else if (Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString()) > 0 && Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()) == 0)
                {
                    rs = string.Format("【存款】未成功金额:{0:C}元", ds.Tables[0].Rows[0][1], ds.Tables[0].Rows[0][2]);
                }
                else if (Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString()) == 0 && Convert.ToDecimal(ds.Tables[0].Rows[0][2].ToString()) > 0)
                {
                    rs = string.Format("【扣款】未成功金额:{0:C}元", ds.Tables[0].Rows[0][2]);
                }
            }
            return rs;
        }
    }
}