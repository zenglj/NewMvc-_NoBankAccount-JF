using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
namespace SelfhelpOrderMgr.DAL
{
    public partial class T_SHO_OrderDTLDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int AddDetailAndUpdateMain(SelfhelpOrderMgr.Model.T_SHO_OrderDTL model, string strFreeFlag)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                //conn.Open();
                //IDbTransaction myTran = conn.BeginTransaction();
                //try
                //{
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("declare @id int;");
                    strSql.Append("insert into T_SHO_OrderDTL(");
                    strSql.Append("FreeFlag,Remark,SPShortCode,OrderID,GCode,GTXM,GName,GCount,GPrice,GAmount,Flag,FTZSP_TypeFlag");
                    strSql.Append(") values (");
                    strSql.Append("@FreeFlag,@Remark,@SPShortCode,@OrderID,@GCode,@GTXM,@GName,@GCount,@GPrice,@GAmount,@Flag,@FTZSP_TypeFlag");
                    strSql.Append(") ;");
                    strSql.Append("select @id=@@IDENTITY;");
                    strSql.Append("Update T_SHO_Order ");
                    strSql.Append("set FAmount=b.famount ,FreeAmount=b.freeamount,FTZSP_Money=b.FTZSP_Money from (");
                    strSql.Append(" select OrderId,Sum(GAmount) famount,Sum(GAmount* Freeflag) freeamount,Sum(GAmount*FTZSP_TypeFlag) FTZSP_Money from T_SHO_OrderDTL where OrderId=@OrderId group by OrderId");
                    strSql.Append(") b");
                    strSql.Append(" where T_SHO_Order.OrderId=b.OrderId;");
                    strSql.Append("select @id;");
                    var paramDtl = new { FreeFlag = model.FreeFlag, Remark = model.Remark, SPShortCode = model.SPShortCode, OrderID = model.OrderID, GCode = model.GCode, GTXM = model.GTXM, GName = model.GName, GCount = model.GCount, GPrice = model.GPrice, GAmount = model.GAmount, Flag = model.Flag, FTZSP_TypeFlag=model.FTZSP_TypeFlag };
                    List<int> rs = (List<int>)conn.Query<int>(strSql.ToString(), paramDtl);
                    int x = rs[0];
                    //myTran.Commit();
                    if(x>0)
                    {                        
                        return x; 
                    }
                    else
                    {
                        return 0;
                    }                    
                //}
                //catch
                //{
                //    //myTran.Rollback();
                //    return 0;
                //}
            }
            

        }
    }
}