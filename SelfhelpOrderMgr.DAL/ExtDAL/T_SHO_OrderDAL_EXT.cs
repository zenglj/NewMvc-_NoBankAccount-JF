/**  版本信息模板在安装目录下，可自行修改。
* T_SHO_OrderDAL.cs
*
* 功 能： N/A
* 类 名： T_SHO_OrderDAL
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015-11-05 10:19:15   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using SelfhelpOrderMgr.Model;
using Dapper;
using System.Configuration;
//using Maticsoft.DBUtility;//Please add references
namespace SelfhelpOrderMgr.DAL
{
	/// <summary>
	/// 数据访问类:T_SHO_OrderDAL
	/// </summary>
	public partial class T_SHO_OrderDAL
	{

		#region  BasicMethod





		#endregion  BasicMethod
		#region  ExtensionMethod

        /// <summary>
        /// 获取订单列表信息
        /// </summary>
        public IEnumerable<T_SHO_Order> GetListOfIEnumerable(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderID,FCrimecode,FCriminal,CrtDate,FAmount,Flag,InvoiceNo,IPAddr ");
            strSql.Append(" FROM T_SHO_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            using(SqlConnection conn=new SqlConnection(SqlHelper.getConnstr())){
                return conn.Query<T_SHO_Order>(strSql.ToString());
            }            
        }


        /// <summary>
        /// 按订单号OrderId更新订单金额Amount
        /// </summary>
        public bool UpdateMoney(int orderId, decimal famount, string strFreeFlag)
        {
            decimal freeAmount = famount * Convert.ToInt32(strFreeFlag);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update T_SHO_Order ");
            strSql.Append("set FAmount=[FAmount]+@FAmount ,FreeAmount=isnull(FreeAmount,0)+@FreeAmount ");
            strSql.Append(" Where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@FAmount", SqlDbType.Decimal,9),
					new SqlParameter("@FreeAmount", SqlDbType.Decimal,9),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
            parameters[0].Value = famount;
            parameters[1].Value = freeAmount;
            parameters[2].Value = orderId;
            object obj = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return (Convert.ToInt32(obj) > 0);
            }
        }
        /// <summary>
        /// 根据OrderId以T_Sho_OrderDTL为准，更新订单的金额
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>

        public bool DelOrderDetailAndUpdateMoney(int orderId,int detailId)
        {
            using (IDbConnection conn = OpenConnection())
            {
                IDbTransaction myTran = conn.BeginTransaction();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from T_SHO_OrderDTL where Id=@Id and OrderId=@OrderId;");
                strSql.Append("Update T_SHO_Order set FAmount=0 ,FreeAmount=0,FTZSP_Money=0 where OrderId=@OrderId;");
                strSql.Append("Update T_SHO_Order ");
                strSql.Append("set FAmount=b.famount ,FreeAmount=b.freeamount,FTZSP_Money=b.FTZSP_Money from (");
                strSql.Append(" select OrderId,Sum(GAmount) famount,Sum(GAmount* Freeflag) freeamount,Sum(GAmount* FTZSP_TypeFlag) FTZSP_Money from T_SHO_OrderDTL where OrderId=@OrderId group by OrderId");
                strSql.Append(") b");
                strSql.Append(" where T_SHO_Order.OrderId=b.OrderId");
                SqlParameter[] parameters = {
				    new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
                object param = new { Id = detailId, OrderId = orderId };
                int i = conn.Execute(strSql.ToString(), param, myTran);
                if (i <= 0)
                {
                    myTran.Rollback();
                    return false;
                }
                else
                {

                    myTran.Commit();
                    return true;
                }
            }
            
        }

        /// <summary>
        /// 按订单号OrderId更新订单状态FLag
        /// </summary>
        public bool UpdateFlag(int orderId, int flag)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Update T_SHO_Order(");
            strSql.Append("set Flag=@Flag ");
            strSql.Append(" Where OrderId=@OrderId");
            SqlParameter[] parameters = {
					new SqlParameter("@Flag", SqlDbType.Int,4),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
            parameters[0].Value = flag;
            parameters[1].Value = orderId;
            object obj = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (obj == null)
            {
                return false;
            }
            else
            {
                return (Convert.ToInt32(obj) > 0);
            }
        }

        //删除犯人的临时订单信息
        public bool DeleteOrderInfoByFCrimecode(string fcrimecode,string saleTypeId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                int i = conn.Execute(@"update t_sho_order set Flag=0 where Flag=1 and isnull(InvoiceNo,'')='' and FCrimeCode=@fcrimecode;
                        delete from t_sho_orderdtl
                        where orderid in(select orderid from t_sho_order where fcrimecode=@fcrimecode and flag=0 
                            and PType in(select PType from T_SHO_SaleType Where Id=@saleTypeId));
                        delete from t_sho_order where fcrimecode=@fcrimecode and flag=0 
                            and PType in(select PType from T_SHO_SaleType Where Id=@saleTypeId);", new { fcrimecode = fcrimecode, saleTypeId = saleTypeId });
                return i > 0;
            }
        }

        public SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(SqlHelper.getConnstr());
            connection.Open();
            return connection;
        }

        public string SubmitOrder(int orderId, string crtby, string ipLastCode, T_Criminal criminal, T_SHO_Order order, List<T_SHO_OrderDTL> details, string userRoomNo)
        {
            
            //IDbConnection conn = new SqlConnection(SqlHelper.getConnstr());
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                //===================下面是程序处理消费结算=================================================
                StringBuilder strSql;
                List<T_SHO_SaleType> saleTypes = (List<T_SHO_SaleType>)conn.Query<T_SHO_SaleType>("select * from T_SHO_SaleType where PType='" + order.PType + "'");
                int saleTypeId = 7;
                int firstPaymentAccount = 0;
                int canconsumeAccount = 0;
                if (saleTypes.Count > 0)
                {
                    saleTypeId = saleTypes[0].TypeFlagId;
                    firstPaymentAccount = saleTypes[0].FirstPaymentAccount;
                    canconsumeAccount = saleTypes[0].CanconsumeAccount;
                }

                //更正处遇可用金额相减会产生A账户问题，将处遇的金额还原为系统设定的金额
                //避免第二次购买时计算处遇可用金额不正确
                T_CY_TYPE cy = new T_CY_TYPEDAL().GetModel(criminal.FCYCode);
                if (cy != null)
                {
                    if (cy.FTZSP_Zero_Flag == 1)
                    {
                        criminal.TZSP_cyMoney = 0;
                    }
                    else
                    {
                        criminal.TZSP_cyMoney = cy.FTZSP_Money;
                    }
                }


                #region 折分用户名
                string strRemark = "";
                T_SHO_ManagerSet mOpenMode = new T_SHO_ManagerSetDAL().GetModel("OpenMode");
                if (mOpenMode.KeyMode == 1)
                {
                    string cc = "_";
                    string[] users = crtby.Split(cc.ToCharArray());
                    //string ipLastCode = string.Format("000", ips[3]);
                    string strUser = users[0];
                    strRemark = crtby + ",自助消费";
                    crtby = strUser;
                }
                else
                {
                    strRemark = crtby + ",自助消费";
                }


                #endregion

                string invoiceno = "";

                strSql = new StringBuilder();
                strSql.Append("Update T_SHO_Order ");
                strSql.Append("set FAmount=b.famount ,FreeAmount=b.freeamount,FTZSP_Money=b.FTZSP_Money from (");
                strSql.Append(" select OrderId,Sum(GAmount) famount,Sum(GAmount* Freeflag) freeamount,Sum(GAmount*FTZSP_TypeFlag) FTZSP_Money from T_SHO_OrderDTL where OrderId=@OrderId group by OrderId");
                strSql.Append(") b");
                strSql.Append(" where T_SHO_Order.OrderId=b.OrderId;");
                strSql.Append("declare @vouno varchar(30);");
                strSql.Append("exec  CREATESEQNO  'Inv',1,@vouno output;");
                strSql.Append("select @vouno='INV'+@vouno +@IpLastCode;");
                strSql.Append("select @vouno;");
                object paramInv = new { OrderId = orderId, IpLastCode = ipLastCode };
                List<string> dd = (List<string>)conn.Query<string>(strSql.ToString(), paramInv);
                invoiceno = dd[0].ToString();




                IDbTransaction myTran = conn.BeginTransaction();
                try
                {
                    T_Vcrd vcrd = new T_Vcrd();

                    int checkflag = -1;//配货检测状态，-1为未配货，0为已配货,是默认是-1
                    //从管理表中获取VCRD CheckFlag 的设定值
                    T_SHO_ManagerSet mgrSet = new T_SHO_ManagerSetDAL().GetModel("JsVcrdCheckFlag");
                    if (mgrSet != null)
                    {
                        checkflag = Convert.ToInt32(mgrSet.MgrValue);
                    }

                    //检测库存量是否足够
                    #region 检测库存量是否足够
                    bool blCheckStockFlag = false;
                    string rtnResult = "";
                    CheckCurrBalance(orderId, conn, ref strSql, myTran, ref blCheckStockFlag, ref rtnResult);

                    //是否需要检测库存,根据Web.config里的配置值定
                    if (SqlHelper.checkStock == "1")
                    {
                        if (rtnResult != "")
                        {
                            myTran.Rollback();
                            return rtnResult;
                        }
                    }
                    #endregion


                    //更新订单表t_sho_order及t_sho_orderdtl两个表
                    #region 更新订单Flag标记   Flag=0表示未提交，Flag=1表示已提交中，Flag=2表示结算成功
                    strSql = new StringBuilder();
                    strSql.Append("update t_sho_order set Flag=2,InvoiceNo=@InvoiceNo,RoomNo=@RoomNo where OrderId=@OrderId;");
                    strSql.Append("update t_sho_orderdtl set Flag=2 where OrderId=@OrderId;");

                    object paramOrder = new { InvoiceNo = invoiceno, OrderId = orderId, RoomNo = userRoomNo };
                    int p = conn.Execute(strSql.ToString(), paramOrder, myTran);
                    if (p <= 0)
                    {
                        myTran.Rollback();
                        return ("Error|更新订单Flag标志出错，已回滚。");
                    }
                    #endregion


                    #region 设定订单的4个金额信息
                    decimal dAmountA = 0;
                    decimal dAmountB = 0;
                    decimal dFreeAmountA = 0;
                    decimal dFreeAmountB = 0;

                    decimal tzxf_Money = order.FTZSP_Money;//特种消费金额
                    decimal tzsp_AreaMoney = criminal.TZSP_AreaMoney;//监区特种商消费金额

                    if (firstPaymentAccount == 0)//存款账户优先
                    {
                        //注意事项 decimal 在比较带小数和不带小数的会出问题
                        if (Convert.ToDouble(order.FAmount) <= Convert.ToDouble(criminal.AmountAmoney))
                        {//A账户的金额大于订单消费金额
                            if (Convert.ToDouble(order.FAmount) <= Convert.ToDouble(criminal.CanUseMoneyA))
                            {
                                if (Convert.ToDouble(criminal.CanUseMoneyA + order.FreeAmount) > Convert.ToDouble(order.FAmount))
                                {
                                    dAmountA = order.FAmount;
                                }
                                else
                                {
                                    dAmountA = criminal.CanUseMoneyA + order.FreeAmount;
                                }
                                dAmountB = order.FAmount - dAmountA;
                                dFreeAmountA = order.FreeAmount;
                                dFreeAmountB = 0;
                            }
                            else
                            {
                                dFreeAmountA = order.FreeAmount;
                                dFreeAmountB = 0;

                                if (order.FAmount > criminal.CanUseMoneyA)
                                {
                                    dAmountA = criminal.CanUseMoneyA;
                                    dAmountB = order.FAmount - dAmountA;
                                }
                                else
                                {
                                    dAmountA = order.FAmount;
                                    dAmountB = order.FAmount - dAmountA;
                                }




                                //如果出现订单总额大于criminal.CanUseMoneyA + criminal.CanUseMoneyB
                                //则扣光criminal.CanUseMoneyB，余下的扣criminal.CanUseMoneyA
                                if (order.FAmount > (criminal.CanUseMoneyA + criminal.CanUseMoneyB))
                                {
                                    dAmountB = criminal.CanUseMoneyB;
                                    dAmountA = order.FAmount - dAmountB;
                                }

                                //如果A账户不够dAmountA，则扣光criminal.AmountAmoney（最大可用金额），余下的扣criminal.CanUseMoneyB
                                if (criminal.AmountAmoney < dAmountA)
                                {
                                    dAmountA = criminal.AmountAmoney;
                                    dAmountB = order.FAmount - dAmountA;
                                }
                            }

                        }
                        else//否则，A账户的金额小于订单消费金额
                        {

                            if (Convert.ToDouble(criminal.CanUseMoneyA + order.FreeAmount) <= Convert.ToDouble(criminal.AmountAmoney))
                            {
                                dAmountA = criminal.CanUseMoneyA + order.FreeAmount;
                                dAmountB = order.FAmount - dAmountA;

                                dFreeAmountA = order.FreeAmount;
                                dFreeAmountB = 0;

                            }
                            else
                            {
                                if (Convert.ToDouble(order.FreeAmount) >= Convert.ToDouble(criminal.AmountAmoney))
                                {
                                    dAmountA = criminal.AmountAmoney;
                                    dAmountB = order.FAmount - dAmountA;
                                    dFreeAmountA = dAmountA;
                                    dFreeAmountB = order.FreeAmount - dFreeAmountA;
                                }
                                else
                                {
                                    if (Convert.ToDouble(order.FreeAmount + criminal.CanUseMoneyA) > Convert.ToDouble(criminal.AmountAmoney))
                                    {
                                        dAmountA = criminal.AmountAmoney;
                                    }
                                    else
                                    {
                                        dAmountA = order.FreeAmount + criminal.CanUseMoneyA;
                                    }
                                    dAmountB = order.FAmount - dAmountA;
                                    dFreeAmountA = order.FreeAmount;
                                    dFreeAmountB = 0;
                                }

                            }

                        }
                    }
                    else//报酬账户优先
                    {
                        if (Convert.ToDouble(order.FAmount) <= Convert.ToDouble(criminal.AmountBmoney))
                        {//B账户的金额大于订单消费金额
                            if (Convert.ToDouble(order.FAmount) <= Convert.ToDouble(criminal.CanUseMoneyB + order.FreeAmount))
                            {
                                dAmountB = order.FAmount;
                                dAmountA = 0;
                                dFreeAmountB = order.FreeAmount;
                                dFreeAmountA = 0;
                            }
                            else
                            {
                                dFreeAmountB = order.FreeAmount;
                                dFreeAmountA = 0;

                                dAmountB = criminal.CanUseMoneyB + order.FreeAmount;
                                dAmountA = order.FAmount - dAmountB;
                            }

                        }
                        else//否则，B账户的金额小于订单消费金额
                        {
                            //如果AmountBmoney，小于或等于CanUseMoneyB+免限额部份
                            if (Convert.ToDouble(criminal.AmountBmoney) <= Convert.ToDouble(criminal.CanUseMoneyB + order.FreeAmount))
                            {
                                dAmountB = criminal.AmountBmoney; //B账户优先全扣
                                dAmountA = order.FAmount - dAmountB;//余下的在A账户扣
                            }
                            else  //就是AmountBmoney 大于 criminal.CanUseMoneyB + order.FreeAmount 的和
                            {
                                //dFreeAmountB = order.FreeAmount;
                                //dFreeAmountA = 0;

                                dAmountB = criminal.CanUseMoneyB + order.FreeAmount;//B账户扣款的金额是最大可用金额+免限金额
                                dAmountA = order.FAmount - dAmountB;//A账户为余下的部分
                            }


                            if (Convert.ToDouble(order.FreeAmount) > Convert.ToDouble(criminal.AmountBmoney))//如果非限额的金额大于A账户可用金额
                            {
                                dFreeAmountB = criminal.AmountBmoney;
                                dFreeAmountA = order.FreeAmount - criminal.AmountBmoney;
                            }
                            else//否则
                            {
                                dFreeAmountB = order.FreeAmount;
                                dFreeAmountA = 0;
                            }
                        }
                    }

                    #endregion


                    #region 调整特种商品（消费金额）AB账户金额及非限额商品的金额
                    //调整特种商品（消费金额）AB账户金额及非限额商品的金额
                    /*
                     因为特种商品只能劳酬金额或后勤队别可用，所以只有扣B账户金额+监区特种金额 <（小于）特种商品的购买金额
                     * FTZSP_AreaXFMoney监区特种商品已经消费的金额
                     */


                    decimal tmpAmountBTzsp = 0;
                    decimal tmpCanUseTzspMoney = 0;
                    tmpCanUseTzspMoney = dAmountB + tzsp_AreaMoney - criminal.FTZSP_AreaXFMoney + criminal.TZSP_cyMoney + criminal.TP_YingYangCan_Money > 0 ? dAmountB + tzsp_AreaMoney - criminal.FTZSP_AreaXFMoney + criminal.TZSP_cyMoney + criminal.TP_YingYangCan_Money : 0;
                    tmpAmountBTzsp = order.FTZSP_Money - tzsp_AreaMoney + criminal.FTZSP_AreaXFMoney - criminal.TZSP_cyMoney - criminal.TP_YingYangCan_Money;
                    //2018-12-17 zenglj 应宁德监狱要求更改
                    //如果节假日金额设定为可以购买特种商品，则金额加上节日金额
                    T_SHO_ManagerSet jjrMSet = new T_SHO_ManagerSetDAL().GetModel("JieJiaRi_Money_TZSPFlag");
                    if (jjrMSet != null)
                    {
                        if (jjrMSet.MgrValue == "1")
                        {
                            tmpCanUseTzspMoney = dAmountB + tzsp_AreaMoney - criminal.FTZSP_AreaXFMoney + criminal.TZSP_cyMoney + criminal.TP_YingYangCan_Money + criminal.JaRi_Cy_Money > 0 ? dAmountB + tzsp_AreaMoney - criminal.FTZSP_AreaXFMoney + criminal.TZSP_cyMoney + criminal.TP_YingYangCan_Money + criminal.JaRi_Cy_Money : 0;
                            tmpAmountBTzsp = order.FTZSP_Money - tzsp_AreaMoney + criminal.FTZSP_AreaXFMoney - criminal.TZSP_cyMoney - criminal.TP_YingYangCan_Money-criminal.JaRi_Cy_Money;
                        }
                    }

                    if (tmpCanUseTzspMoney < order.FTZSP_Money)
                    {
                        if (tzsp_AreaMoney + criminal.TZSP_cyMoney + criminal.TP_YingYangCan_Money + criminal.JaRi_Cy_Money - criminal.FTZSP_AreaXFMoney > 0)
                        {
                            dAmountB = tmpAmountBTzsp;
                        }
                        else
                        {
                            dAmountB = order.FTZSP_Money;
                        }
                        


                        //2018-11-28修正个人别处遇变更后，处遇金额特种商品金额减少
                        if (dAmountB > order.FAmount)
                        {
                            dAmountB = order.FAmount;
                        }
                        if (dAmountB > criminal.AmountB)
                        {
                            dAmountB = criminal.AmountB;
                        }


                        //关于宁德消费出现A或B账户负数的问题，增加个判断
                        if(order.FAmount - dAmountB<0)
                        {
                            dAmountA = 0;
                            dAmountB = order.FAmount - dAmountA;
                        }
                        else
                        {
                            dAmountA = order.FAmount - dAmountB;
                        }
                        if (dFreeAmountA > dAmountA)
                        {
                            dFreeAmountA = dAmountA;
                            dFreeAmountB = order.FreeAmount - dAmountA;
                        }

                        

                    }
                    #endregion


                    #region 获得Invoice消费订单号

                    #endregion
                    #region 插入消费主单
                    strSql = new StringBuilder();
                    strSql.Append("insert into T_Invoice(");
                    strSql.Append("InvoiceNo,CardCode,FCrimeCode,Amount,OrderDate,PayDate,PType,Flag,Remark,ServAmount,Crtby,Crtdate,fsn,FAreaCode,FAreaName,FCriminal,Frealareacode,FrealAreaName,TypeFlag,CardType,AmountA,AmountB,Fifoflag,FreeAmountA,FreeAmountB,Checkflag,RoomNo,OrderId,FTZSP_Money,UserCyDesc");
                    strSql.Append(") values (");
                    strSql.Append("@InvoiceNo,@CardCode,@FCrimeCode,@Amount,@OrderDate,@PayDate,@PType,@Flag,@Remark,@ServAmount,@Crtby,@Crtdate,@fsn,@FAreaCode,@FAreaName,@FCriminal,@Frealareacode,@FrealAreaName,@TypeFlag,@CardType,@AmountA,@AmountB,@Fifoflag,@FreeAmountA,@FreeAmountB,@Checkflag,@RoomNo,@OrderId,@FTZSP_Money,@UserCyDesc");
                    strSql.Append(") ");
                    object paramInvoice = new { InvoiceNo = invoiceno, CardCode = criminal.CardCode, FCrimeCode = criminal.FCode, Amount = order.FAmount, OrderDate = DateTime.Now, PayDate = DateTime.Now, PType = order.PType, Flag = 1, Remark = "", ServAmount = criminal.OkUseAllMoney, Crtby = crtby, Crtdate = order.CrtDate, fsn = "", FAreaCode = criminal.FAreaCode, FAreaName = criminal.FAreaName, FCriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", TypeFlag = saleTypeId, CardType = 0, AmountA = dAmountA, AmountB = dAmountB, Fifoflag = -1, FreeAmountA = dFreeAmountA, FreeAmountB = dFreeAmountB, Checkflag = 0, RoomNo = userRoomNo, OrderId = orderId, FTZSP_Money = order.FTZSP_Money, UserCyDesc=criminal.UserCyDesc };

                    #endregion
                    int i = conn.Execute(strSql.ToString(), paramInvoice, myTran);
                    if (i <= 0)//如果成功，开始插入明细
                    {
                        myTran.Rollback();
                        return ("Error|插入消费主单记录出错，已回滚。");
                    }
                    #region 插入消费明细
                    strSql = new StringBuilder();
                    strSql.Append("insert into T_InvoiceDTL(");
                    strSql.Append("INVOICENO,GCODE,GNAME,OrderDate,PayDATE,Flag,GDJ,QTY,AMOUNT,Servamount,GTXM,FCrimecode,GORGDJ,GORGAMT,StockSeqno,Typeflag,Cardtype,AmountA,AmountB,Fifoflag,Backqty,FreeFlag,Remark,SPShortCode,FTZSP_TypeFlag");
                    strSql.Append(") select ");
                    strSql.Append("@INVOICENO,GCODE,GNAME,@OrderDate,@PayDATE,@Flag,GPrice,GCount,GAmount,0,GTXM,@FCrimecode,0,0,0,@Typeflag,@Cardtype,0,0,@Fifoflag,@Backqty,FreeFlag,Remark,SPShortCode,FTZSP_TypeFlag");
                    strSql.Append(" from T_SHO_OrderDTL where OrderId=@OrderId");
                    object paramDetail = new { INVOICENO = invoiceno, OrderDate = DateTime.Now, PayDATE = DateTime.Now, Flag = 1, FCrimecode = criminal.FCode, TypeFlag = saleTypeId, Cardtype = 0, Fifoflag = -1, Backqty = 0, OrderId = orderId };

                    #endregion
                    int j = conn.Execute(strSql.ToString(), paramDetail, myTran);
                    if (j <= 0)//如果插入明细成功；
                    {
                        myTran.Rollback();
                        return ("Error|插入消费明细记录出错，已回滚。");
                    }
                    //插入库存主单
                    #region 插入库存主单
                    strSql = new StringBuilder();
                    strSql.Append("declare @StockId varchar(30);");
                    strSql.Append("exec  CREATESEQNO  'S',1,@StockId output;");
                    strSql.Append("select @StockId='S'+@StockId + @IpLastCode;");
                    strSql.Append("insert into T_Stock(");
                    strSql.Append("StockId,InOutDate,FLAG,StockType,CrtBy,Crtdt,CHECKFLAG,CHECKBY,CheckDt,Remark,invoiceno,stockflag,InOutFlag");
                    strSql.Append(") values (");
                    strSql.Append("@StockId,@InOutDate,@FLAG,@StockType,@CrtBy,@Crtdt,@CHECKFLAG,@CHECKBY,@CheckDt,@Remark,@invoiceno,@stockflag,@InOutFlag");
                    strSql.Append(") ;");
                    strSql.Append("insert into T_StockDTL(");
                    strSql.Append("StockId,GCODE,GTXM,GCOUNT,GDJ,flag,stockflag,InOutFlag,Remark");
                    strSql.Append(") select ");
                    strSql.Append("@StockId,GCODE,GTXM,GCOUNT,GPrice GDJ,flag,5,-1,Remark");
                    strSql.Append(" from T_SHO_OrderDTL where OrderId=@OrderId");
                    object paramStock = new { IpLastCode = ipLastCode, InOutDate = DateTime.Now, FLAG = -1, StockType = order.PType, CrtBy = crtby, Crtdt = DateTime.Now, CHECKFLAG = 1, CHECKBY = crtby, CheckDt = DateTime.Now, Remark = "", invoiceno = invoiceno, stockflag = 5, InOutFlag = -1, OrderId = order.OrderID };

                    #endregion
                    int k = conn.Execute(strSql.ToString(), paramStock, myTran);
                    if (k <= 0)//如果库存成功；
                    {
                        myTran.Rollback();
                        return ("Error|插入更新库存记录出错，已回滚。");
                    }

                    //更新库存表
                    #region 更新库存量表
                    strSql = new StringBuilder();
                    strSql.Append("update T_GOODSSTOCKMAIN Set balance=balance-b.gcount from(");
                    strSql.Append("select gcode,gname,sum(gcount) gcount from t_sho_orderdtl where OrderId=@OrderId group by gcode,gname");
                    strSql.Append(") b");
                    strSql.Append(" where T_GOODSSTOCKMAIN.gcode=b.gcode");
                    object paramStockmain = new { OrderId = orderId };
                    int ii = conn.Execute(strSql.ToString(), paramStockmain, myTran);
                    if (ii <= 0)
                    {
                        myTran.Rollback();
                        return ("Error|更新库存量出错，已回滚。");
                    }
                    #endregion


                    #region 处理插入Vcrd记及更新余额表
                    //decimal invAmountA = 0;
                    //decimal invAmountB = 0;
                    ////先获取当前AB账户余额,实现插入消费记录时，将当前的账户余额一并写入到curUserAmout
                    //T_Criminal_card tcard = new T_Criminal_cardDAL().GetModel(criminal.FCode);
                    //decimal curUserAmountA = tcard.AmountA;
                    //decimal curUserAmountB = tcard.AmountB;
                    //decimal curAllAmount = tcard.AmountA + tcard.AmountB + tcard.AmountC;
                    ////插入VCrd记录及更新余额
                    //strSql = new StringBuilder();
                    //strSql.Append("declare @VOUNO varchar(30);");
                    //strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                    //strSql.Append("select @VOUNO='VOU'+@VOUNO + @IpLastCode;");
                    //strSql.Append("insert into T_Vcrd(");
                    //strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount");
                    //strSql.Append(") values (");
                    //strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@Bankflag,@checkflag,@checkby,@pc,@curUserAmount,@curAllAmount");
                    //strSql.Append(");");
                    //object paramVcrd;
                    //int x = 0;
                    //if (criminal.AmountAmoney >= order.FAmount)//一个账户就够扣的情况
                    //{
                    //    invAmountA = order.FAmount;                        
                    //    invAmountB = 0;
                    //    //设定账户额
                    //    curUserAmountA = curUserAmountA - invAmountA;
                    //    curUserAmountB= curUserAmountB - invAmountB;
                    //    curAllAmount = curAllAmount - invAmountA - invAmountB;
                    //    paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = order.PType, DEPOSITER = "", REMARK = "", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 0, Bankflag = 0, checkflag = checkflag, checkby = crtby, pc = 0, curUserAmount = curUserAmountA, curAllAmount = curAllAmount };
                    //    x = conn.Execute(strSql.ToString(), paramVcrd,myTran);
                    //    if (x <= 0)
                    //    {
                    //        myTran.Rollback();
                    //        return ("Error|插入VCrd记录出错，已回滚。");
                    //    }
                    //}
                    //else//需要2个账户才够扣的情况
                    //{
                    //    invAmountA = criminal.AmountAmoney;
                    //    invAmountB = order.FAmount - criminal.AmountAmoney;
                    //    //设定账户额
                    //    curUserAmountA = curUserAmountA - invAmountA;
                    //    curUserAmountB = curUserAmountB - invAmountB;
                    //    curAllAmount = curAllAmount - invAmountA - invAmountB;
                    //    if(invAmountA>0)//金额必须是大于0，才执行操作
                    //    {
                    //        paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = order.PType, DEPOSITER = "", REMARK = "", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 0, Bankflag = 0, checkflag = checkflag, checkby = crtby, pc = 0, curUserAmount = curUserAmountA, curAllAmount = curAllAmount };
                    //        x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                    //        if (x <= 0)
                    //        {
                    //            myTran.Rollback();
                    //            return ("Error|插入VCrd记录出错，已回滚。");
                    //        }
                    //    }
                    //    //B账户扣款
                    //    paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountB, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = order.PType, DEPOSITER = "", REMARK = "", flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 1, Bankflag = 0, checkflag = checkflag, checkby = crtby, pc = 0, curUserAmount = curUserAmountB, curAllAmount = curAllAmount };
                    //    x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                    //    if (x <= 0)
                    //    {
                    //        myTran.Rollback();
                    //        return ("Error|插入VCrd记录出错，已回滚。");
                    //    }
                    //}


                    decimal invAmountA = dAmountA;
                    decimal invAmountB = dAmountB;

                    //先获取当前AB账户余额,实现插入消费记录时，将当前的账户余额一并写入到curUserAmout
                    T_Criminal_card tcard = new T_Criminal_cardDAL().GetModel(criminal.FCode);
                    decimal curUserAmountA = tcard.AmountA;
                    decimal curUserAmountB = tcard.AmountB;
                    decimal curAllAmount = tcard.AmountA + tcard.AmountB + tcard.AmountC;
                    //插入VCrd记录及更新余额
                    strSql = new StringBuilder();
                    strSql.Append("declare @VOUNO varchar(30);");
                    strSql.Append("exec  CREATESEQNO  'VOU',1,@vouno output;");
                    strSql.Append("select @VOUNO='VOU'+@VOUNO + @IpLastCode;");
                    strSql.Append("insert into T_Vcrd(");
                    strSql.Append("VOUNO,cardcode,fcrimecode,DAMOUNT,CAMOUNT,crtBy,CRTDATE,DTYPE,DEPOSITER,REMARK,flag,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,ptype,udate,origid,cardtype,TYPEFLAG,acctype,Bankflag,checkflag,checkby,pc,curUserAmount,curAllAmount");
                    strSql.Append(") values (");
                    strSql.Append("@VOUNO,@cardcode,@fcrimecode,@DAMOUNT,@CAMOUNT,@crtBy,@CRTDATE,@DTYPE,@DEPOSITER,@REMARK,@flag,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@ptype,@udate,@origid,@cardtype,@TYPEFLAG,@acctype,@Bankflag,@checkflag,@checkby,@pc,@curUserAmount,@curAllAmount");
                    strSql.Append(");");
                    object paramVcrd;
                    int x = 0;
                    if (invAmountA > 0)//如果A账户大于0
                    {
                        //设定账户额
                        curUserAmountA = curUserAmountA - invAmountA;
                        //curUserAmountB = curUserAmountB - invAmountB;
                        curAllAmount = curAllAmount - invAmountA;
                        paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountA, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = order.PType, DEPOSITER = "", REMARK = strRemark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 0, Bankflag = 0, checkflag = checkflag, checkby = crtby, pc = 0, curUserAmount = curUserAmountA, curAllAmount = curAllAmount };
                        x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                        if (x <= 0)
                        {
                            myTran.Rollback();
                            return ("Error|插入VCrd记录出错，已回滚。");
                        }
                    }
                    if (invAmountB > 0)//如果A账户大于0
                    {
                        //设定账户额
                        //curUserAmountA = curUserAmountA - invAmountA;
                        curUserAmountB = curUserAmountB - invAmountB;
                        curAllAmount = curAllAmount - invAmountB;
                        paramVcrd = new { ipLastCode = ipLastCode, cardcode = criminal.CardCode, fcrimecode = criminal.FCode, DAMOUNT = 0, CAMOUNT = invAmountB, crtBy = crtby, CRTDATE = DateTime.Now, DTYPE = order.PType, DEPOSITER = "", REMARK = strRemark, flag = 0, fareacode = criminal.FAreaCode, fareaName = criminal.FAreaName, fcriminal = criminal.FName, Frealareacode = "", FrealAreaName = "", ptype = "", udate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-1", origid = invoiceno, cardtype = 0, TypeFlag = saleTypeId, acctype = 1, Bankflag = 0, checkflag = checkflag, checkby = crtby, pc = 0, curUserAmount = curUserAmountB, curAllAmount = curAllAmount };
                        x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                        if (x <= 0)
                        {
                            myTran.Rollback();
                            return ("Error|插入VCrd记录出错，已回滚。");
                        }
                    }

                    //更新金额
                    strSql = new StringBuilder();
                    strSql.Append("Update t_criminal_card set AmountA=AmountA-@AmountA,AmountB=AmountB-@AmountB where fcrimecode=@fcrimecode");
                    paramVcrd = new { fcrimecode = criminal.FCode, AmountA = invAmountA, AmountB = invAmountB };
                    x = conn.Execute(strSql.ToString(), paramVcrd, myTran);
                    if (x <= 0)
                    {
                        myTran.Rollback();
                        return ("Error|更新余额出错，已回滚。");
                    }
                    #endregion
                    myTran.Commit();
                    myTran.Dispose();
                    return ("OK|结算成功。");
                }
                catch (Exception ex)
                {
                    myTran.Rollback();
                    myTran.Dispose();
                    //订单状态复位为0“未提交”
                    T_SHO_Order orderModel = new T_SHO_OrderDAL().GetModel(Convert.ToInt32(orderId));
                    orderModel.Flag = 0;
                    if (new T_SHO_OrderDAL().Update(orderModel))
                    {
                        return ("Error|数据执行过程中异常，已回滚。" + ex.ToString());
                    }
                    return ("Error|数据执行过程中异常，已回滚。但订单状态没有复位" + ex.ToString());

                }
            } 
        }

        public string SubmitOrderForProc(int orderId, string crtby, string ipLastCode, T_Criminal criminal, T_SHO_Order order, List<T_SHO_OrderDTL> details, string userRoomNo)
        {
            using (IDbConnection conn = new SqlConnection(SqlHelper.getConnstr()))
            {
                conn.Open();
                var parems = new DynamicParameters();//建立一个parem对象
                parems.Add("@OrderId", orderId);
                parems.Add("@IpLastCode", ipLastCode);                               
                parems.Add("@crtby", crtby);
                parems.Add("@userRoomNo", userRoomNo);
                parems.Add("@UserCyDesc", criminal.UserCyDesc);
                parems.Add("@result", "", DbType.String, ParameterDirection.Output);//输出返回值
                //注意 parems.Add("@res",ParameterDirection.Output);//这样写返回值可能会出错，切记！！！
                SqlMapper.Execute(conn, "PayAndCrtCustomerList", parems, null, null, CommandType.StoredProcedure);
                string res = parems.Get<string>("@result");//获取数据库输出的值
                return res;
            }
            
        }

        //检测库存量是否够扣
        private static void CheckCurrBalance(int orderId, IDbConnection conn, ref StringBuilder strSql, IDbTransaction myTran, ref bool blCheckStockFlag, ref string rtnResult)
        {
            strSql = new StringBuilder();
            strSql.Append("select gcode,gname,sum(gcount) gcount from t_sho_orderdtl where OrderId=@OrderId group by gcode,gname");
            object paramCheck = new { OrderId = orderId };
            var chks = conn.Query(strSql.ToString(), paramCheck, myTran);
            foreach (var chk in chks)
            {
                blCheckStockFlag = false;
                decimal stockBalance = 0;
                decimal dStockNum = Convert.ToDecimal(chk.gcount);
                strSql = new StringBuilder();
                strSql.Append("select top 1 gcode,balance from T_GOODSSTOCKMAIN where gcode=@gcode");
                object paramBalance = new { gcode = chk.gcode };
                var balances = conn.Query(strSql.ToString(), paramBalance, myTran).AsList();
                if (balances.Count > 0)
                {
                    if (Convert.ToDecimal(balances[0].balance) >= dStockNum)
                    {
                        blCheckStockFlag = true;
                        stockBalance = Convert.ToDecimal(balances[0].balance);
                    }
                }
                else//如果没有库存量记录，则插入一条记录
                {
                    strSql = new StringBuilder();
                    strSql.Append("insert into T_GOODSSTOCKMAIN (Gcode,Balance,TmpBalance)");
                    strSql.Append(" values(@Gcode,@Balance,@TmpBalance)");
                    object paramAddStockTable = new { Gcode = chk.gcode, Balance = 0, TmpBalance = 0 };
                    int kk = conn.Execute(strSql.ToString(), paramAddStockTable, myTran);
                    if (kk <= 0)
                    {
                        //myTran.Rollback();
                        rtnResult = "Error|增加[" + chk.gcode + "]" + chk.gname + ",到库存表失败";
                    }

                }
                if (blCheckStockFlag == false)
                {
                    if (rtnResult == "")
                    {
                        rtnResult = "Error|[" + chk.gcode + "]" + chk.gname + ",库存量不足，当前库量为：" + stockBalance.ToString();
                    }
                    else
                    {
                        rtnResult = rtnResult+ "；[" + chk.gcode + "]" + chk.gname + ",库存量不足，当前库量为：" + stockBalance.ToString();
                    }
                }
            }
        }

        //按商品查询次本月购买的数量
        public decimal GetMonthBuyCount(string gtxm,string fcrimecode)
        {
            using(IDbConnection conn=OpenConnection())
            {
                StringBuilder strSql;
                strSql = new StringBuilder();
                
                strSql.Append("select isnull(sum(GCount),0) GCount from t_sho_order a,t_sho_orderDTL b");
                strSql.Append(" where a.orderId=b.OrderId");
                strSql.Append(" and GTXM=@GTXM and crtDate>=@StartDate and crtDate<@EndDate and a.FCrimeCode=@FCrimeCode");
                T_Goods good = new T_GoodsDAL().GetModel(gtxm);
                string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                string endDate = DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-1";

                DateTime dt = DateTime.Now;  //当前时间 

                switch (good.XgMode)
                {
                    case 0: //按日
                        {
                            startDate = dt.ToString("yyyy-MM-dd");
                            endDate = dt.ToString("yyyy-MM-dd") +" 23:59:00";
                        }
                        break;
                    case 1://按周
                        {
                            DateTime  startWeek=dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));//本周周一
                            startDate = startWeek.ToString();  //本周周一  
                            endDate = startWeek.AddDays(7).ToString();  //下周周一
                        }
                        break;
                    case 2://按月
                        {
                            startDate = dt.Year.ToString() + "-" + dt.Month.ToString() + "-1";
                            endDate = dt.AddMonths(1).Year.ToString() + "-" + dt.AddMonths(1).Month.ToString() + "-1";
                            #region 判断是否是在特殊的消费时段里
                            //判断是否在消费限额之内
                            T_SHO_ManagerSet xfDateRect = new T_SHO_ManagerSetDAL().GetModel("XFDateRect");
                            if (xfDateRect.MgrValue == "1")
                            {
                                /*
                                 由于出现闽西提前设定消费时段，并且消费起始日期大于当天，倒致小于起始日期的消费不受系统限额控制。
                                 * 现改为：凡是大于或小于的都进入标准处遇控制
                                 */
                                if (DateTime.Today >= new T_SHO_ManagerSetDAL().GetModel("XFDateStart").StartTime && DateTime.Today < new T_SHO_ManagerSetDAL().GetModel("XFDateEnd").StartTime.AddDays(1))
                                {
                                    startDate = new T_SHO_ManagerSetDAL().GetModel("XFDateStart").StartTime.ToString();
                                    endDate = new T_SHO_ManagerSetDAL().GetModel("XFDateEnd").StartTime.ToString();
                                }
                            }
                            #endregion
                        }
                        break;
                    case 3://按季度
                        {
                            int m = dt.Month;
                            if (m >= 10)
                            {
                                startDate = dt.Year.ToString() + "-10-01";
                                endDate = dt.AddYears(1).Year.ToString() + "-01-01";
                            }
                            else if (m >= 7 && m < 10)
                            {
                                startDate = dt.Year.ToString() + "-07-01";
                                endDate = dt.Year.ToString() + "-10-01";
                            }
                            else if (m >= 4 && m < 7)
                            {
                                startDate = dt.Year.ToString() + "-04-01";
                                endDate = dt.Year.ToString() + "-7-01";
                            }
                            else if(m <=3)
                            {
                                startDate = dt.Year.ToString() + "-01-01";
                                endDate = dt.Year.ToString() + "-04-01";
                            }
                        }
                        break;
                    case 4://按年
                        {
                            startDate = dt.Year.ToString() + "-01-01";
                            endDate = dt.AddYears(1).Year.ToString() + "-01-01";
                        }
                        break;
                    
                    default:
                        break;
                }
                //string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                //string endDate = DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-1";

                object paramGtxm = new { GTXM = gtxm, StartDate = startDate, EndDate = endDate, FCrimeCode =fcrimecode};
                List<string> gcount=(List<string>) conn.Query<string>(strSql.ToString(),paramGtxm);
                string buyCount = gcount[0].ToString();
                return Convert.ToDecimal(buyCount);
            }
        }


        //按商品类型查询次本月购买的数量
        public decimal GetTypeBuyCount(int ctrlMode,string goodType,string gtxm, string fcrimecode)
        {
            using (IDbConnection conn = OpenConnection())
            {
                StringBuilder strSql;
                strSql = new StringBuilder();                
                strSql.Append("select isnull(sum(GCount),0) GCount from t_sho_order a,t_sho_orderDTL b,t_goods c ");
                strSql.Append(" where a.orderId=b.OrderId and b.GTXM=c.GTXM ");
                strSql.Append(" and c.GType=@GType and crtDate>=@StartDate and crtDate<@EndDate and a.FCrimeCode=@FCrimeCode");
                if (ctrlMode == 1)
                {
                    strSql.Append(" and c.GTXM=@GTXM");
                }

                //T_Goods good = new T_GoodsDAL().GetModel(gtype.Fcode);


                //初始相关参数的值
                decimal beiShu = 1;//默认消费的倍数为1，如果在特定时段内再根据相关值调整
                string startDate = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-1";
                string endDate = DateTime.Now.AddMonths(1).Year.ToString() + "-" + DateTime.Now.AddMonths(1).Month.ToString() + "-1";

                #region 判断是否是在特殊的消费时段里
                //判断是否在消费限额之内
                T_SHO_ManagerSet xfDateRect = new T_SHO_ManagerSetDAL().GetModel("XFDateRect");
                if (xfDateRect.MgrValue == "1")
                {
                    /*
                     由于出现闽西提前设定消费时段，并且消费起始日期大于当天，倒致小于起始日期的消费不受系统限额控制。
                     * 现改为：凡是大于或小于的都进入标准处遇控制
                     */
                    if (DateTime.Today >= new T_SHO_ManagerSetDAL().GetModel("XFDateStart").StartTime && DateTime.Today < new T_SHO_ManagerSetDAL().GetModel("XFDateEnd").StartTime.AddDays(1))
                    {
                        startDate = new T_SHO_ManagerSetDAL().GetModel("XFDateStart").StartTime.ToString();
                        endDate = new T_SHO_ManagerSetDAL().GetModel("XFDateEnd").StartTime.ToString();
                        T_SHO_ManagerSet xfBeiShu = new T_SHO_ManagerSetDAL().GetModel("XFBeiShu");
                        beiShu = Convert.ToDecimal(xfBeiShu.MgrValue);
                    }
                }
                #endregion

                object paramGtxm = new { GType = goodType, StartDate = startDate, EndDate = endDate, FCrimeCode = fcrimecode,GTXM=gtxm };
                List<string> gcount = (List<string>)conn.Query<string>(strSql.ToString(), paramGtxm);
                string buyCount = gcount[0].ToString();

                //判断是否倍数是否大于1
                if (beiShu > 1)
                {
                    return Convert.ToDecimal(buyCount) * beiShu;
                }
                else
                {
                    return Convert.ToDecimal(buyCount);
                }                
            }
        }




		#endregion  ExtensionMethod
	}
}

