using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_SHO_AreaGoodMaxCountDAL
    {
        public DataSet GetGoodsByType(string goodtype)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select 0 Id,'' FAreaCode,'' FAreaName ,a.gtxm FGtxm,a.GName FGoodName,b.FName FGoodType,-1 FGoodMaxCount from t_goods a,t_goodstype b where a.gtype=b.fcode and a.Active='Y' and b.FName=@GoodType");
            SqlParameter[] parameters = {
			            new SqlParameter("@GoodType", SqlDbType.VarChar,10)           
              
            };

            parameters[0].Value = goodtype;
              
            
            return SqlHelper.Query(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 获得指定分页数据
        /// </summary>
        public DataSet GetPageRowList(int page,int pageSize, string strWhere)
        {
            int startNumber = (page - 1) * pageSize + 1;
            int endNumber = page * pageSize;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from T_SHO_AreaGoodMaxCount where Id in(
                select Id from (
                select ROW_NUMBER() OVER (ORDER BY Id) AS RowNumber,* from T_SHO_AreaGoodMaxCount a ");
            if (strWhere != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(@" ) b where RowNumber >=@startNumber and RowNumber<=@endNumber
                )");
            
            strSql.Append("  ORDER BY Id");
            SqlParameter[] parameters = {
			            new SqlParameter("@startNumber", SqlDbType.Int,4)
                        ,new SqlParameter("@endNumber", SqlDbType.Int,4)
              
            };

            parameters[0].Value = startNumber;
            parameters[1].Value = endNumber;
            return SqlHelper.Query(strSql.ToString(),parameters);
        }

        public bool CopyInfoToAears(string srcArea)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SHO_AreaGoodMaxCount where not FAreaCode=@FAreaCode;");
            strSql.Append(@"insert into T_SHO_AreaGoodMaxCount (FAreaCode,FAreaName,[FGtxm],[FGoodName],[FGoodType],[FGoodMaxCount])
                select b.FCode FAreaCode,b.FName FAreaName,[FGtxm],[FGoodName],[FGoodType],[FGoodMaxCount] 
                from T_SHO_AreaGoodMaxCount a,t_Area b 
                where b.FID>=0 and not b.fcode=@FAreaCode and a.FAreaCode=@FAreaCode;");
            SqlParameter[] parameters = {
			            new SqlParameter("@FAreaCode", SqlDbType.VarChar,20)              
            };
            parameters[0].Value = srcArea;

            int i= SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            return i > 0;
        }
    }
}