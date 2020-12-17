using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    public partial class t_XFQueryListDAL
    {
        public DataSet GetFCrimeXFInfo(string action, string fcrimecode, DateTime startDate, DateTime endDate)
        {
            string sql;
            if ("OldSystem" == action)
            {
                sql = "exec P_XFOldQueryList '" + fcrimecode + "','" + startDate.ToShortDateString() + "', '" + endDate.ToShortDateString() + "'";
            }
            else
            {
                sql = "exec P_New_XFQueryList '" + fcrimecode + "','" + startDate.ToShortDateString() + "', '" + endDate.ToShortDateString() + "'";
            }

            int i=SqlHelper.ExecuteSql(sql);
            if(i>0)
            {
                sql = @"select e.Fcrimecode,fname,CDate,Cmoney,Dtype,f.fareaName,f.BankCard from t_XFQueryList e 
                        left outer join 
                        (select c.fcrimecode,b.fname fareaName,c.BankAccNo BankCard from t_criminal a,t_area b,t_criminal_card c
                        where a.fareacode=b.fcode and a.fcode=c.fcrimecode
                        and a.fcode='"+ fcrimecode +@"') f
                        on e.fcrimecode=f.fcrimecode
                         order by cdate;";
                return SqlHelper.Query(sql);                
            }
            else
            {
                return new DataSet();
            }
        }
    }
}