using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SelfhelpOrderMgr.DAL
{
    //T_InvoiceDTL
    public partial class T_InvoiceDTLDAL
    {


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, int printSumOption)
        {
            StringBuilder strSql = new StringBuilder();
            if (printSumOption == 1)
            {
                strSql.Append(@"SELECT 0 as [seqno],[INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag]
                    ,[GDJ],sum(QTY) as[QTY],sum(Amount) as[AMOUNT],0 as [Servamount],[GTXM],[FCrimecode],0 as [GORGDJ],0 as [GORGAMT]
                    ,[StockSeqno],[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag]
                    ,[Backqty],[FreeFlag],[Remark],[SPShortCode],[FTZSP_TypeFlag]
                      FROM [T_InvoiceDTL]");
                
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                strSql.Append(@" group by  [INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag]
                        ,[GDJ],[GTXM],[FCrimecode]
                        ,[StockSeqno],[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag]
                        ,[Backqty],[FreeFlag],[Remark],[SPShortCode],[FTZSP_TypeFlag] ");
            }
            else
            {
                strSql.Append("select * ");
                strSql.Append(" FROM T_InvoiceDTL ");
                if (strWhere.Trim() != "")
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            
            return SqlHelper.Query(strSql.ToString());
        }

    }
}

