using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SelfhelpOrderMgr.DAL
{
    public class T_EdiBankSumListDAL
    {
        public IEnumerable<T_EdiBankSumList> GetModelListByPage(int page, int pageRow, DateTime startDate, DateTime endDate, string Code, string Remark, string succflag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
                strSql.Append(@"select ROW_NUMBER() OVER (ORDER BY a.mainseqno desc) AS RowNumber,a.mainseqno,a.remark,substring(convert(varchar(20),a.crtdate,120),1,10) crtdate,a.UpLoadDate,a.DetailDownLoadDate
                ,DfMoney=sum( b.damount )
                ,DfSuccMoney=sum(case when b.succflag=1 then b.damount else 0 end)
                ,DfFailMoney=sum(case when b.succflag=-1 then b.damount else 0 end)
                ,DsSuccMoney=sum(case when b.succflag=1 then b.camount else 0 end)
                ,NodoMoney=abs(sum(case when b.succflag=0 then b.damount-camount else 0 end))
                ,case DetailDownloadflag* a.succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end succflag
                                        ,case isnull(resetflag,0) when 1 then '已复位' else '' end resetflag
                 From t_edilist a left join t_edidetail b on a.mainseqno=b.mainseqno");
                strSql.Append(@" where a.crtdate>=@startDate and a.crtdate<@endDate
                      and a.remark LIKE (@Remark) ");                
                strSql.Append(@" and a.Code in("+ Code +")");
                if (succflag != "")
                {
                    strSql.Append(" and DetailDownloadflag* a.succflag="+succflag);
                }
                strSql.Append(@" group by a.mainseqno,a.remark,substring(convert(varchar(20),a.crtdate,120),1,10) ,a.UpLoadDate,a.DetailDownLoadDate
                ,case DetailDownloadflag* a.succflag when 1 then '成功' when -1 then '失败' when -2 then '手工复位' else '已发送' end 
                ,case isnull(resetflag,0) when 1 then '已复位' else '' end ");
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                


                var parame = new { startDate = startDate, endDate = endDate, Code = Code, Remark = "%" + Remark + "%", startNumber = startNumber, endNumber = endNumber, succflag = succflag };
                return conn.Query<T_EdiBankSumList>(strSql.ToString(), parame);
            }

        }
    
    }
}