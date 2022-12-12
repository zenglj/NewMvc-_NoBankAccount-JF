using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;
using SelfhelpOrderMgr.Model;

namespace SelfhelpOrderMgr.DAL
{
    public partial class T_InvoiceDAL
    {
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRow"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderByField"></param>
        /// <returns></returns>
        public List<T_Invoice> GetPageList(int page, int pageRow, string strWhere,string startTime,string endTime, string orderByField)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int startNumber = (page - 1) * pageRow + 1;
                int endNumber = page * pageRow;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select *");
                strSql.Append(" from (");
//                strSql.Append(@"select ROW_NUMBER() OVER (ORDER BY InvoiceNo) AS RowNumber,a.*,b.BankFlag from T_Invoice a left join (
//                        select origid,isnull(bankflag,0) BankFlag from t_vcrd where flag=0 and crtdate between @startTime and @endTime  group by origid,bankflag
//                        ) b on a.invoiceno=b.origid 
//                        where 1=1");


                strSql.Append(@"select ROW_NUMBER() OVER (ORDER BY InvoiceNo) AS RowNumber,a.*,b.BankFlag from (select * from T_Invoice ");

                if (strWhere != "")
                {
                    strSql.Append(" where " + strWhere);
                }
                
                strSql.Append(@") a left join (
                        select origid,isnull(bankflag,0) BankFlag from t_vcrd where flag=0 and crtdate between @startTime and @endTime  group by origid,bankflag
                        ) b on a.invoiceno=b.origid ");
                strSql.Append(") b");
                strSql.Append(" where RowNumber>=@startNumber and RowNumber<=@endNumber");
                if (string.IsNullOrEmpty(orderByField) == false)
                {
                    strSql.Append(" Order by " + orderByField);
                }

                return (List<T_Invoice>)conn.Query<T_Invoice>(strSql.ToString(), new {startTime=startTime,endTime=endTime, startNumber = startNumber, endNumber = endNumber });
            }
        }

        /// <summary>
        /// 返回查询记录数和总金额
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public decimal[] GetListCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(count(*),0) FCount,isnull(sum(amount),0) FMoney from T_Invoice");
            if (strWhere != "")
            {
                strSql.Append(" where " + strWhere);
            }
            else
            {
                strSql.Append(" where Flag=1 and Amount<>0 ");
            }
            decimal[] rs = { 0, 0 };
            decimal fcount = Convert.ToDecimal(SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][0]);
            decimal fmoney = Convert.ToDecimal(SqlHelper.Query(strSql.ToString()).Tables[0].Rows[0][1]);
            rs[0] = fcount;
            rs[1] = fmoney;
            return rs;
        }

        public decimal GetAreaBuyGoodCount(string fcrimecode,string gtxm,string fareaCode, string startDate, string endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(sum(b.qty),0) buyCount from t_Invoice a,t_invoiceDtl b
                        where a.invoiceno=b.invoiceno and a.flag=1 
                        and a.orderdate>=@startDate and a.orderdate<@endDate
                        and a.FAreaCode=@fareaCode
                        and b.gtxm=@gtxm");
            SqlParameter[] parameters = {
					new SqlParameter("@startDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@endDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@fareaCode", SqlDbType.VarChar,20)
                    ,new SqlParameter("@gtxm", SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = fareaCode;
            parameters[3].Value = gtxm;

            DataSet ds= SqlHelper.Query(strSql.ToString(),parameters);

            //最新未结算订单中该商品的数量
            strSql = new StringBuilder();
            strSql.Append(@"select isnull(sum(GCount),0) GCount from t_sho_orderDTL where GTXM=@GTXM and OrderId in(
                    select top 1 OrderId from t_sho_order where FCrimeCode=@FCrimeCode and flag=0 
                    order by orderid desc )");
            SqlParameter[] param2 = {
					new SqlParameter("@GTXM", SqlDbType.VarChar,20)
                    ,new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20)
                    };
            param2[0].Value = gtxm;
            param2[1].Value = fcrimecode;

            DataSet ds2 = SqlHelper.Query(strSql.ToString(), param2);

            return Convert.ToDecimal(ds.Tables[0].Rows[0][0]) + Convert.ToDecimal(ds2.Tables[0].Rows[0][0]);
        }

        //按登录名来统计商品已经购买的数量
        public decimal GetLoginNameBuyGoodCount(string crtby,string gtxm,  string startDate, string endDate)
        {
            StringBuilder strSql = new StringBuilder();
            /*
             *2017-09-06  修改人：曾林进
             *修改原因：由于闽西监狱出现一个分监区用两个账号进行刷卡，所以会超出队别限购数量
             *修改方法：在SQL中把原根据Crtby 操用员名称进行判断，
             *          改为按操作员名称所对应的队别所有的操作员购买数量来统计
             *
             */

            strSql = new StringBuilder();
            strSql.Append(@"select isnull(sum(gcount),0) sumCount from(
                select a.crtby,spshortcode,gcount From t_sho_order a,t_sho_orderdtl b where a.orderid=b.orderid and a.flag<2 and gtxm=@gtxm
                and substring(a.crtby,1, CHARINDEX('_',a.crtby)-1) in(
					select fname from t_czy where fuserarea=(select fuserArea from t_czy where fname='" + crtby + @"'))
                union all
                select a.crtby,spshortcode,b.qty gcount From t_invoice a,t_invoicedtl b where a.invoiceno=b.invoiceno and a.flag=1 and gtxm=@gtxm
                and a.orderdate>=@startDate and a.orderdate<@endDate
                and a.crtby='" + crtby + @"') y;");



                //"and a.crtby in(select fname from t_czy where fuserarea=(select fuserArea from t_czy where fname='"+ crtby +"'))) y;");
            SqlParameter[] parameters = {
                     new SqlParameter("@startDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@endDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@crtby", SqlDbType.VarChar,20)
                    ,new SqlParameter("@gtxm",  SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = crtby;
            parameters[3].Value = gtxm;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            return Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

        }

        //系统提交时验证登录名来统计商品是否超过限购买的数量
        public bool GetLoginNameCheckBuyGoodCountStatus(string crtby, string startDate, string endDate, int orderId, decimal beiShu)
        {
            StringBuilder strSql = new StringBuilder();

            /*
             *2018-06-08 修改人：曾林进
             *修改原因：判断是否超过最大限购数量时，采用单个商品循环太慢了
             *修改方法：首先判断商品中是否有限购商品，没有就跳过
             *           接来采用批量查询判断的方法
             */
            strSql.Append(@"select * From t_sho_orderdtl where orderid=" + orderId.ToString() + @"
                and gtxm in (select distinct FGtxm From T_SHO_AreaGoodMaxCount);");

            DataSet s1 = SqlHelper.Query(strSql.ToString());

            if (s1.Tables[0].Rows.Count == 0)
            {
                return true;
            }

            strSql = new StringBuilder();

            strSql.Append(@"select c.* From T_SHO_AreaGoodMaxCount a,t_area b,(
                select gtxm,isnull(sum(gcount),0) sumCount from(
                select a.crtby,GTXM,gcount From t_sho_order a,t_sho_orderdtl b where a.orderid=b.orderid and a.flag<2 and gtxm in(select gtxm From t_sho_orderdtl where orderid=@orderId)
                and substring(a.crtby,1, CHARINDEX('_',a.crtby)-1) in(
					select fname from t_czy where fuserarea=(select fuserArea from t_czy where fname=@crtby))
                union all
                select a.crtby,GTXM,b.qty gcount From t_invoice a,t_invoicedtl b where a.invoiceno=b.invoiceno and a.flag=1 and gtxm in(select gtxm From t_sho_orderdtl where orderid=@orderId)
                and a.orderdate>=@startDate and a.orderdate<@endDate
                and a.crtby=@crtby) y group by gtxm) c
                where a.FGtxm=c.GTXM and a.FAreaName=b.FName
                and b.FName in(select distinct FUserArea from t_czy where fuserarea=(select fuserArea from t_czy where fname=@crtby))
                and a.FGoodMaxCount*@beiShu<c.sumCount
                ;");

            //"and a.crtby in(select fname from t_czy where fuserarea=(select fuserArea from t_czy where fname='"+ crtby +"'))) y;");
            SqlParameter[] parameters = {
                     new SqlParameter("@startDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@endDate", SqlDbType.VarChar,20)
                    ,new SqlParameter("@crtby", SqlDbType.VarChar,20)
                    ,new SqlParameter("@orderId", SqlDbType.Int,4)
                     ,new SqlParameter("@beiShu", SqlDbType.Decimal,8)
                                        };
            parameters[0].Value = startDate;
            parameters[1].Value = endDate;
            parameters[2].Value = crtby;
            parameters[3].Value = orderId;
            parameters[4].Value = beiShu;

            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            //return Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CancelOrderById(string InvoiceNo,string DelBy)//取消订单
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_GOODSSTOCKMAIN set BALANCE=BALANCE+b.qty from (
                select GCode,sum(qty) qty from t_InvoiceDTL where invoiceNo=@InvoiceNo
                group by GCode) b
                where T_GOODSSTOCKMAIN.GCode=b.GCode;");//加回库存量
            strSql.Append(@"delete from t_InvoiceDtl where InvoiceNo=@InvoiceNo;");//删除消费明细
            strSql.Append(@"Update t_Invoice set Flag=0,Remark=@DelBy+':订单被手动取消了' where InvoiceNo=@InvoiceNo;");//软删除消费主单
            strSql.Append(@"delete from t_Sho_orderDTL where orderId in(
                select OrderId from t_Sho_order where InvoiceNo=@InvoiceNo);");//删除订单明细
            strSql.Append(@"Delete from t_Sho_order where InvoiceNo=@InvoiceNo;");//删除主订单
            strSql.Append(@"delete from t_stockDtl where stockid in(
                select stockid from t_stock where invoiceno=@InvoiceNo);");//删除出库单明细
            strSql.Append(@"Delete from t_stock where InvoiceNo=@InvoiceNo;");//删除主出库单
            strSql.Append(@"update t_Criminal_Card set AmountA=AmountA+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId=@InvoiceNo and Flag=0 and isnull(BankFlag,0)=0 and AccType=0
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;
                update t_Criminal_Card set AmountB=AmountB+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId=@InvoiceNo and Flag=0  and isnull(BankFlag,0)=0 and AccType=1
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");//更新账户余额
            strSql.Append(@"Update t_Vcrd set Flag=1,DelDate=getdate(),DelBy=@DelBy,Remark=Remark+'前台管理卡取消订单' where OrigId=@InvoiceNo and Flag=0  and isnull(BankFlag,0)=0;");//软删除VCrd的记录


            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();            
                IDbTransaction myTran = conn.BeginTransaction();
                object paramInvoice = new { InvoiceNo = InvoiceNo,DelBy=DelBy };
                try
                {
                    int i = conn.Execute(strSql.ToString(), paramInvoice,myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                    return false;
                }
            }

            
        }

        //ChangeOrderById 修改订单，将订单取消费结算，可重新修改再结算
        /// <summary>
        /// 修改订单 与取消订单的差别是，没有删除T_SHO_Order 和T_SHO_OrderDTL
        /// 只是把状态改为0，到未提交的状态
        /// </summary>
        /// <param name="InvoiceNo"></param>
        /// <param name="DelBy"></param>
        /// <returns></returns>
        public bool ChangeOrderById(string InvoiceNo, string DelBy)//修改订单，将订单取消费结算，可重新修改再结算
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_GOODSSTOCKMAIN set BALANCE=BALANCE+b.qty from (
                select GCode,sum(qty) qty from t_InvoiceDTL where invoiceNo=@InvoiceNo
                group by GCode) b
                where T_GOODSSTOCKMAIN.GCode=b.GCode;");//加回库存量
            strSql.Append(@"delete from t_InvoiceDtl where InvoiceNo=@InvoiceNo;");//删除消费明细
            strSql.Append(@"delete from t_Invoice where InvoiceNo=@InvoiceNo;");//删除消费主单
            strSql.Append(@"Update t_Sho_orderDTL set Flag=0 where orderId in(
                select OrderId from t_Sho_order where InvoiceNo=@InvoiceNo);");//删除订单明细
            strSql.Append(@"Update t_Sho_order set Flag=0 where InvoiceNo=@InvoiceNo;");//删除主订单
            strSql.Append(@"delete from t_stockDtl where stockid in(
                select stockid from t_stock where invoiceno=@InvoiceNo);");//删除出库单明细
            strSql.Append(@"Delete from t_stock where InvoiceNo=@InvoiceNo;");//删除主出库单
            strSql.Append(@"update t_Criminal_Card set AmountA=AmountA+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId=@InvoiceNo and Flag=0  and isnull(BankFlag,0)=0 and AccType=0
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;
                update t_Criminal_Card set AmountB=AmountB+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId=@InvoiceNo and Flag=0  and isnull(BankFlag,0)=0 and AccType=1
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");//更新账户余额
            strSql.Append(@"delete t_Vcrd where OrigId=@InvoiceNo and Flag=0  and isnull(BankFlag,0)=0;");//删除VCrd的记录
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                object paramInvoice = new { InvoiceNo = InvoiceNo, DelBy = DelBy };
                try
                {
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                    return false;
                }
            }


        }

        //根据OrderId进行判断
        public bool Exists(int OrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Invoice");
            strSql.Append(" where ");
            strSql.Append(" OrderId = @OrderId  ");
            SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)			};
            parameters[0].Value = OrderId;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Invoice GetModel(string InvoiceNo,int vcrdBankFlag)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct InvoiceNo, ServAmount, Crtby, Crtdate, fsn, FAreaCode, FAreaName, FCriminal, Frealareacode, FrealAreaName, TypeFlag, CardCode, CardType, AmountA, AmountB, Fifoflag, FreeAmountA, FreeAmountB, Checkflag, RoomNo, OrderId, FCrimeCode, Amount, OrderDate, PayDate, PType, Flag, Remark,isnull(BankFlag,0) BankFlag  ");
            strSql.Append("  from T_Invoice a,t_Vcrd b  ");
            strSql.Append(" where a.InvoiceNo=b.Origid and InvoiceNo=@InvoiceNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20)			};
            parameters[0].Value = InvoiceNo;


            SelfhelpOrderMgr.Model.T_Invoice model = new SelfhelpOrderMgr.Model.T_Invoice();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                model.InvoiceNo = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
                if (ds.Tables[0].Rows[0]["ServAmount"].ToString() != "")
                {
                    model.ServAmount = decimal.Parse(ds.Tables[0].Rows[0]["ServAmount"].ToString());
                }
                model.Crtby = ds.Tables[0].Rows[0]["Crtby"].ToString();
                if (ds.Tables[0].Rows[0]["Crtdate"].ToString() != "")
                {
                    model.Crtdate = DateTime.Parse(ds.Tables[0].Rows[0]["Crtdate"].ToString());
                }
                model.fsn = ds.Tables[0].Rows[0]["fsn"].ToString();
                model.FAreaCode = ds.Tables[0].Rows[0]["FAreaCode"].ToString();
                model.FAreaName = ds.Tables[0].Rows[0]["FAreaName"].ToString();
                model.FCriminal = ds.Tables[0].Rows[0]["FCriminal"].ToString();
                model.Frealareacode = ds.Tables[0].Rows[0]["Frealareacode"].ToString();
                model.FrealAreaName = ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
                if (ds.Tables[0].Rows[0]["TypeFlag"].ToString() != "")
                {
                    model.TypeFlag = int.Parse(ds.Tables[0].Rows[0]["TypeFlag"].ToString());
                }
                model.CardCode = ds.Tables[0].Rows[0]["CardCode"].ToString();
                if (ds.Tables[0].Rows[0]["CardType"].ToString() != "")
                {
                    model.CardType = int.Parse(ds.Tables[0].Rows[0]["CardType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountA"].ToString() != "")
                {
                    model.AmountA = decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountB"].ToString() != "")
                {
                    model.AmountB = decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fifoflag"].ToString() != "")
                {
                    model.Fifoflag = int.Parse(ds.Tables[0].Rows[0]["Fifoflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FreeAmountA"].ToString() != "")
                {
                    model.FreeAmountA = decimal.Parse(ds.Tables[0].Rows[0]["FreeAmountA"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FreeAmountB"].ToString() != "")
                {
                    model.FreeAmountB = decimal.Parse(ds.Tables[0].Rows[0]["FreeAmountB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Checkflag"].ToString() != "")
                {
                    model.Checkflag = int.Parse(ds.Tables[0].Rows[0]["Checkflag"].ToString());
                }
                model.RoomNo = ds.Tables[0].Rows[0]["RoomNo"].ToString();
                if (ds.Tables[0].Rows[0]["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(ds.Tables[0].Rows[0]["OrderId"].ToString());
                }
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderDate"].ToString() != "")
                {
                    model.OrderDate = DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayDate"].ToString() != "")
                {
                    model.PayDate = DateTime.Parse(ds.Tables[0].Rows[0]["PayDate"].ToString());
                }
                model.PType = ds.Tables[0].Rows[0]["PType"].ToString();
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();


                if (ds.Tables[0].Rows[0]["BankFlag"].ToString() != "")
                {
                    model.BankFlag = int.Parse(ds.Tables[0].Rows[0]["BankFlag"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,int vcrdBankFlag,DateTime startDate,DateTime endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.*,b.BankFlag from (");
            strSql.Append("select * ");
            strSql.Append(" FROM T_Invoice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(") a left join (select distinct origid,isnull(Bankflag,0) BankFlag from t_vcrd where isnull(origid,'')<>'' and crtDate>=@startDate and crtDate<@endDate ) b");
            strSql.Append(" on a.invoiceno=b.origid");

            SqlParameter[] parameters = {
			            new SqlParameter("@startDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@endDate", SqlDbType.DateTime)
            };

            parameters[0].Value = startDate;
            parameters[1].Value = endDate;

            return SqlHelper.Query(strSql.ToString(), parameters);
        }


        public bool UpdatePrintCount(string invoices)
        {
            string strSql = "update T_Invoice set printCount=printCount +1 where invoiceno in(" + invoices + ")";
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                int i = conn.Execute(strSql.ToString());
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }



        public bool CancelInvoiceOrder(string strInvoices, string crtby)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"update T_GOODSSTOCKMAIN set BALANCE=BALANCE+b.qty from (
                select GCode,sum(qty) qty from t_InvoiceDTL where invoiceNo in(" + strInvoices + @")
                group by GCode) b
                where T_GOODSSTOCKMAIN.GCode=b.GCode;");//加回库存量
            strSql.Append(@"update t_InvoiceDtl set Flag=0 where InvoiceNo in(" + strInvoices + ");");//删除消费明细
            strSql.Append(@"update t_Invoice set flag=0,remark=@crtby +',后台撤单'+'" + DateTime.Now.ToString() + "' where InvoiceNo in(" + strInvoices + ");");//删除消费主单
            strSql.Append(@"Update t_Sho_orderDTL set Flag=0 where orderId in(
                select OrderId from t_Sho_order where InvoiceNo in(" + strInvoices + "));");//删除订单明细
            strSql.Append(@"Update t_Sho_order set Flag=0 where InvoiceNo in(" + strInvoices + ");");//删除主订单
            strSql.Append(@"delete from t_stockDtl where stockid in(
                select stockid from t_stock where invoiceno in(" + strInvoices + "));");//删除出库单明细
            strSql.Append(@"Delete from t_stock where InvoiceNo in (" + strInvoices + ");");//删除主出库单
            strSql.Append(@"update t_Criminal_Card set AmountA=AmountA+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId in (" + strInvoices + @") and Flag=0  and isnull(BankFlag,0)<=0 and AccType=0
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;
                update t_Criminal_Card set AmountB=AmountB+b.Fmoney from
                (select FcrimeCode,sum(Camount) Fmoney from t_Vcrd where OrigId in(" + strInvoices + @") and Flag=0  and isnull(BankFlag,0)<=0 and AccType=1
                group by FcrimeCode) b
                where t_Criminal_Card.FCrimeCode=b.FCrimeCode;");//更新账户余额
            strSql.Append(@"update t_Vcrd set flag=1,delby=@crtby,deldate=getdate() where OrigId in(" + strInvoices + ") and Flag=0  and isnull(BankFlag,0)=0;");//删除VCrd的记录
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                IDbTransaction myTran = conn.BeginTransaction();
                object paramInvoice = new { InvoiceNo = strInvoices, crtby = crtby };
                try
                {
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    myTran.Commit();
                    return true;
                }
                catch
                {
                    myTran.Rollback();
                    return false;
                }
            }
        }

        public string AddTuiHuoOrder(List<T_InvoiceDTL> models, string crtby, string ipLastCode)
        {         

            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();

                T_Criminal criminal = new T_CriminalDAL().GetModel(models[0].FCrimecode);
                T_AREA area = new T_AREADAL().GetModel(criminal.FAreaCode);
                T_Criminal_card tcard = new T_Criminal_cardDAL().GetModel(models[0].FCrimecode);

                StringBuilder strSql = new StringBuilder();
                string invoiceno = "";
                string stockid = "";
                string vouno = "";

                strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'Inv',1,@vouno output;");
                strSql.Append("select @vouno='INV'+@vouno +@IpLastCode;");
                strSql.Append("select @vouno;");
                object paramInv = new { IpLastCode = ipLastCode };
                List<string> dd = (List<string>)conn.Query<string>(strSql.ToString(), paramInv);
                invoiceno = dd[0].ToString();


                strSql = new StringBuilder();
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'S',1,@vouno output;");
                strSql.Append("select @vouno='S'+@vouno +@IpLastCode;");
                strSql.Append("select @vouno;");
                object paramInv2 = new { IpLastCode = ipLastCode };
                List<string> dd2 = (List<string>)conn.Query<string>(strSql.ToString(), paramInv2);
                stockid = dd2[0].ToString();

                //strSql = new StringBuilder();
                //strSql.Append("declare @vouno varchar(30);");
                //strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                //strSql.Append("select @vouno='VOU'+@vouno +@IpLastCode;");
                //strSql.Append("select @vouno;");
                //object paramInv3 = new { IpLastCode = ipLastCode };
                //List<string> dd3 = (List<string>)conn.Query<string>(strSql.ToString(), paramInv3);
                //vouno = dd3[0].ToString();


                strSql = new StringBuilder();
                int typeflag = 11;//消费退货
                string remark = "对应的消费主单：" + models[0].INVOICENO;
                decimal sumAmount = 0;
                decimal sumTzspMoney = 0;
                foreach (T_InvoiceDTL dtl in models)
                {
                    sumAmount = sumAmount + dtl.AMOUNT;//汇总金额
                    if (dtl.FTZSP_TypeFlag == 1)
                    {
                        sumTzspMoney = sumTzspMoney + dtl.AMOUNT;
                    }

                    strSql.Append(@"INSERT INTO [T_InvoiceDTL]
                               ([INVOICENO],[GCODE],[GNAME],[OrderDate],[PayDATE],[Flag],[GDJ]
                    ,[QTY],[AMOUNT],[Servamount],[GTXM],[FCrimecode],[GORGDJ],[GORGAMT],[StockSeqno]
                    ,[Typeflag],[Cardtype],[AmountA],[AmountB],[Fifoflag],[Backqty],[FreeFlag]
                    ,[Remark],[SPShortCode],[FTZSP_TypeFlag])
                         VALUES ('"+ invoiceno +"','"+ dtl.GCODE +"','"+ dtl.GNAME +"',getdate(),getdate(),1,"+dtl.GDJ +@"
                    ,"+ dtl.QTY +","+ dtl.AMOUNT +",0,'"+ dtl.GTXM +"','"+ dtl.FCrimecode +"',"+ dtl.GDJ +","+ dtl.AMOUNT +@",0
                    ,"+ typeflag +",0,"+ dtl.AMOUNT +",0,1,0,0,'"+ remark +"','"+ dtl.SPShortCode +"',"+ dtl.FTZSP_TypeFlag.ToString()+");");


                }

                strSql.Append(@"INSERT INTO [T_Invoice]
                     ([InvoiceNo],[CardCode],[FCrimeCode],[Amount],[OrderDate],[PayDate],[PType],[Flag],[Remark],[ServAmount]
                    ,[Crtby],[Crtdate],[fsn],[FAreaCode],[FAreaName],[FCriminal],[Frealareacode],[FrealAreaName],[TypeFlag]
                    ,[CardType],[AmountA],[AmountB],[Fifoflag],[FreeAmountA],[FreeAmountB],[Checkflag],[RoomNo]
                    ,[OrderId],[FTZSP_Money],[printCount],[UserCyDesc],[OrderStatus]) 
                    select '" + invoiceno + "' [InvoiceNo],'"+ tcard.cardcodea +"' [CardCode],a.fcode as  [FCrimeCode]," + sumAmount + " as  [Amount],getdate() [OrderDate],getdate() [PayDate],'消费退货',1 as [Flag],'" + remark + @"' [Remark],0
                    ,'" + crtby +"' [Crtby],getdate() [Crtdate],'',[FAreaCode],b.fname [FAreaName],a.fname [FCriminal],'','',"+ typeflag.ToString() +@" as [TypeFlag]
                    ,0," + sumAmount.ToString() + " as [AmountA],0,1,0,0,0,0,0," + sumTzspMoney.ToString() + ",0,'',0 from t_criminal a,t_area b where a.fareacode=b.fcode and a.fcode='" + models[0].FCrimecode + "';");


                //写入库存记录
                strSql.Append(@"INSERT INTO [T_Stock] ([StockId],[InOutDate],[FLAG],[StockType],[CrtBy],[Crtdt],
                        [CHECKFLAG],[CHECKBY],[CheckDt],[Remark],[invoiceno],[stockflag],[InOutFlag])
                         VALUES('"+ stockid +"',getdate(),-1,'消费退货','"+ crtby +@"',getdate()
                    ,1,'"+ crtby +"',getdate(),'"+remark +"','"+ invoiceno +"',6,1);");

                foreach (T_InvoiceDTL dtl in models)
                {
                    strSql.Append(@"INSERT INTO [T_StockDTL]
                           ([StockId],[GCODE],[GTXM],[GCOUNT],[GDJ],[flag],[stockflag],[InOutFlag],[Remark])
                            values('"+stockid+"','"+dtl.GCODE+"','"+dtl.GTXM+"','"+dtl.QTY+"','"+dtl.GDJ+"',1,6,1,'"+ remark +"');");

                    strSql.Append(@"UPDATE [T_GOODSSTOCKMAIN] SET balance =balance+" + dtl.QTY + " WHERE gcode='" + dtl.GCODE + "';");
                }




                decimal invAmountA = sumAmount;
                decimal invAmountB = 0;

                //先获取当前AB账户余额,实现插入消费记录时，将当前的账户余额一并写入到curUserAmout
                
                decimal curUserAmountA = tcard.AmountA;
                decimal curUserAmountB = tcard.AmountB;
                decimal curAllAmount = tcard.AmountA + tcard.AmountB + tcard.AmountC;

                //插入VCrd记录及更新余额

                strSql.Append("insert into T_Vcrd(");
                strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount");
                strSql.Append(") values (");
                strSql.Append("'" + invoiceno + "','" + tcard.cardcodea + "','" + tcard.fcrimecode + "'," + sumAmount.ToString() + ",0,'" + crtby + "',getdate(),'消费退货','','" + remark + "',0,'" + area.FCode + "','" + area.FName + "','"+ criminal.FName +"','','','',getdate(),'"+invoiceno+"',0,11,0,0,1,'"+ crtby +"',0,0,0");
                strSql.Append(");");
        
                //更新金额
                strSql.Append("Update t_criminal_card set AmountA=AmountA+"+ sumAmount +" where fcrimecode='"+ criminal.FCode +"'");                
                
                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    int i = conn.Execute(strSql.ToString(), null, myTran);
                    myTran.Commit();
                    return "OK|退货成功";
                }
                catch
                {
                    myTran.Rollback();
                    return "Err|失败,数据已经回滚";
                }
            }
        }
    }
}