using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_TempAmount_CardDAL
    {
        public List<T_TempAmount_Card> GetSearchCardAmount(string strFCrimeCode, string strFCrimeName, string strAreaName, string cardStatus)
        {
            //string sql = "";
            ////删除临时表记录
            //sql = "delete from t_tempAmount_Card";
            //SqlHelper.ExecuteSql(sql);
            ////插入新记录
            //sql = "insert into t_tempAmount_Card select a.fcrimecode,b.fname,c.fname fareaName,a.bankaccno,a.amounta,a.amountb,a.amountc,(a.amounta+a.amountb+a.amountc) fmoney from t_criminal_card a,t_criminal b,t_area c where a.fcrimecode=b.fcode and b.fAreaCode=c.fcode and isnull(b.fflag,0)=0";
            //if ("" != strFCrimeCode)
            //{
            //    sql = sql + " and a.fcrimecode='" + strFCrimeCode + "'";
            //}
            //else if ("" != strFCrimeName)
            //{
            //    sql = sql + " and b.fname like '%" + strFCrimeName + "%'";
            //}
            //else if ("" != strAreaName && "000" != strAreaName)
            //{
            //    sql = sql + " and c.fcode='" + strAreaName + "'";
            //}
            //SqlHelper.ExecuteSql(sql);

            ////查询结果
            //sql = "select a.fcrimecode,a.fname,a.fareaName,a.bankaccno,a.amounta,a.amountb,a.amountc,a.fmoney from t_tempAmount_Card a";
            //return SqlHelper.Query(sql);




            //2018-12-19 zenglj 改用直接查询

            

            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                string sql = "";
                //删除临时表记录
                //sql = "delete from t_tempAmount_Card";
                //SqlHelper.ExecuteSql(sql);
                //插入新记录
                var p = new DynamicParameters();
                sql = "select a.fcrimecode,b.fname,c.fname fareaName,a.cardflaga,a.SecondaryBankCard as bankaccno,a.amounta,a.amountb,a.amountc,(a.amounta+a.amountb+a.amountc) fmoney from t_criminal_card a,t_criminal b,t_area c where a.fcrimecode=b.fcode and b.fAreaCode=c.fcode and isnull(b.fflag,0)=0";
                if ("" != strFCrimeCode)
                {
                    sql = sql + " and (a.fcrimecode=@FCode or a.bankaccno =@FCode)";
                    p.Add("FCode", strFCrimeCode);
                }
                else if ("" != strFCrimeName)
                {
                    sql = sql + " and b.fname like @FName";
                    p.Add("FName", "%"+strFCrimeName+"%");
                }
                else if ("" != strAreaName && "000" != strAreaName)
                {
                    sql = sql + " and c.fcode=@FAreaName";
                    p.Add("FAreaName",  strAreaName );
                }
                else if ("" != cardStatus && "000" != cardStatus)
                {
                    sql = sql + " and a.cardflaga=@cardStatus";
                    p.Add("cardStatus", cardStatus );
                }
                //object param = new { FCode = strFCrimeCode, FName = string.Format("{0}%", strFCrimeName), FAreaName = strAreaName, cardStatus = cardStatus };

                List<T_TempAmount_Card> lists = (List<T_TempAmount_Card>)conn.Query<T_TempAmount_Card>(sql, p);

                return lists;

            }
        }
    }
}