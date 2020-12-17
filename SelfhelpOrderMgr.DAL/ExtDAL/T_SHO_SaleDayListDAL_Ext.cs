using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_SHO_SaleDayListDAL
    {
        //验证指定日期是否是可以购物
        public int SaleDayExists(int saleTypeId,DateTime saleDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from (");
            strSql.Append(" select id,SaleTypeId, substring(convert(varchar(20),getdate(),120),1,8) + convert(varchar(2),startDay) as startDay ");
            strSql.Append(" ,substring(convert(varchar(20),getdate(),120),1,8) + convert(varchar(2),EndDay) as EndDay   ");
            strSql.Append(" ,case a.remark when '' then '00:00-23:59' else a.remark end remark,a.LevelId ");
            strSql.Append(" from t_SHO_SaleDayList a,t_SHO_SaleType b ");
            strSql.Append(" where a.Ptype=b.id and a.PType=@SaleTypeId)c  ");
            strSql.Append(" where convert(datetime, @SaleDate)>=startDay and convert(datetime, @SaleDate)<dateadd(day,1,EndDay) ");
            strSql.Append(" order by  LevelId desc");

            SqlParameter[] parameters = {
					new SqlParameter("@SaleTypeId", SqlDbType.Int,4),
                    new SqlParameter("@SaleDate", SqlDbType.DateTime)
			};
            parameters[0].Value = saleTypeId;
            parameters[1].Value = saleDay;
            SqlParameter[] newParameters = {
					new SqlParameter("@SaleTypeId", SqlDbType.Int,4),
                    new SqlParameter("@SaleDate", SqlDbType.DateTime)
			};
            newParameters[0].Value = saleTypeId;
            newParameters[1].Value = saleDay;
            bool existFlag = SqlHelper.Exists(strSql.ToString(), parameters);
            if (existFlag)
            {
                DataSet ds = SqlHelper.Query(strSql.ToString(), newParameters);
                string dataArea=ds.Tables[0].Rows[0]["Remark"].ToString();
                if (dataArea.Length == 11)
                {
                    string startTime = dataArea.Substring(0, 5);
                    string endTime = dataArea.Substring(6, 5);
                    if (DateTime.Now < Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + startTime) || DateTime.Now > Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd") + " " + endTime))
                    {
                        return -1;
                    }
                }
                
                return 1;
            }
            else{
                return 0;
            }           
            
        }

        public string SaleDayTimeArea(int saleTypeId, DateTime saleDay)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from (");
            strSql.Append(" select id,SaleTypeId, substring(convert(varchar(20),getdate(),120),1,8) + convert(varchar(2),startDay) as startDay ");
            strSql.Append(" ,substring(convert(varchar(20),getdate(),120),1,8) + convert(varchar(2),EndDay) as EndDay   ");
            strSql.Append(" ,case a.remark when '' then '00:00-23:59' else a.remark end remark,a.LevelId ");
            strSql.Append(" from t_SHO_SaleDayList a,t_SHO_SaleType b ");
            strSql.Append(" where a.Ptype=b.id and a.PType=@SaleTypeId)c  ");
            strSql.Append(" where convert(datetime, @SaleDate)>=startDay and convert(datetime, @SaleDate)<dateadd(day,1,EndDay) ");
            strSql.Append(" order by  LevelId desc");

            SqlParameter[] newParameters = {
					new SqlParameter("@SaleTypeId", SqlDbType.Int,4),
                    new SqlParameter("@SaleDate", SqlDbType.DateTime)
			};
            newParameters[0].Value = saleTypeId;
            newParameters[1].Value = saleDay;
            DataSet ds = SqlHelper.Query(strSql.ToString(), newParameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["Remark"].ToString();
            }
            else
            {
                return "00:00-23:59";
            }
            
            

        }
    }
}