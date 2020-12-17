using SelfhelpOrderMgr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_StockDAL
    {
        //盘点入库 StockFlag 1
        //盘点出库 StockFlag 2
        //超市消费 StockFlag 5
        //消费退货 StockFlag 6
        /// <summary>
        /// 增加一个库存主单
        /// </summary>
        /// <param name="crtby"></param>
        /// <param name="stockType"></param>
        /// <param name="stockflag"></param>
        /// <param name="inoutflag"></param>
        /// <returns></returns>
        public T_Stock NewStock(string crtby,string stockType,int stockflag,int inoutflag)//增加一个库存主单
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                string stockid = "";
                string idTop = "S";
                StringBuilder strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  '" + idTop + "',1,@vouno output;");
                strSql.Append("select @vouno='" + idTop + "'+@vouno;");
                strSql.Append("select @vouno;");
                List<string> dd = (List<string>)conn.Query<string>(strSql.ToString());
                stockid = dd[0].ToString();

                T_Stock model = new T_Stock() { 
                    StockId=stockid,
                    Flag = inoutflag,
                    StockType = stockType,
                    Stockflag = stockflag,
                    CrtBy=crtby,
                    CrtDt=DateTime.Now,
                    CheckFlag=2,
                    CheckBy=crtby,
                    CheckDt=DateTime.Now,
                    Remark="盘点模块直接输入库存量产生的变化",
                    InvoiceNo="",
                    InOutFlag=inoutflag,
                    InOutDate=DateTime.Now                    
                };
                Add(model);//增加一条库存主单

                return model;
            }
            
        }

    }
}