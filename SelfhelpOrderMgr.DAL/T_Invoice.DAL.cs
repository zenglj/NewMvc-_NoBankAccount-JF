using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
namespace SelfhelpOrderMgr.DAL
{
    //T_Invoice
    public partial class T_InvoiceDAL
    {

        public bool Exists(string InvoiceNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_Invoice");
            strSql.Append(" where ");
            strSql.Append(" InvoiceNo = @InvoiceNo  ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20)			};
            parameters[0].Value = InvoiceNo;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SelfhelpOrderMgr.Model.T_Invoice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_Invoice(");
            strSql.Append("InvoiceNo,ServAmount,Crtby,Crtdate,fsn,FAreaCode,FAreaName,FCriminal,Frealareacode,FrealAreaName,TypeFlag,CardCode,CardType,AmountA,AmountB,Fifoflag,FreeAmountA,FreeAmountB,Checkflag,RoomNo,OrderId,FTZSP_Money,FCrimeCode,printCount,OrderStatus,UserCyDesc,Amount,OrderDate,PayDate,PType,Flag,Remark");
            strSql.Append(") values (");
            strSql.Append("@InvoiceNo,@ServAmount,@Crtby,@Crtdate,@fsn,@FAreaCode,@FAreaName,@FCriminal,@Frealareacode,@FrealAreaName,@TypeFlag,@CardCode,@CardType,@AmountA,@AmountB,@Fifoflag,@FreeAmountA,@FreeAmountB,@Checkflag,@RoomNo,@OrderId,@FTZSP_Money,@FCrimeCode,@printCount,@OrderStatus,@UserCyDesc,@Amount,@OrderDate,@PayDate,@PType,@Flag,@Remark");
            strSql.Append(") ");

            SqlParameter[] parameters = {
			            new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ServAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeAmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeAmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Checkflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoomNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@printCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserCyDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512)             
              
            };

            parameters[0].Value = model.InvoiceNo;
            parameters[1].Value = model.ServAmount;
            parameters[2].Value = model.Crtby;
            parameters[3].Value = model.Crtdate;
            parameters[4].Value = model.fsn;
            parameters[5].Value = model.FAreaCode;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FCriminal;
            parameters[8].Value = model.Frealareacode;
            parameters[9].Value = model.FrealAreaName;
            parameters[10].Value = model.TypeFlag;
            parameters[11].Value = model.CardCode;
            parameters[12].Value = model.CardType;
            parameters[13].Value = model.AmountA;
            parameters[14].Value = model.AmountB;
            parameters[15].Value = model.Fifoflag;
            parameters[16].Value = model.FreeAmountA;
            parameters[17].Value = model.FreeAmountB;
            parameters[18].Value = model.Checkflag;
            parameters[19].Value = model.RoomNo;
            parameters[20].Value = model.OrderId;
            parameters[21].Value = model.FTZSP_Money;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.printCount;
            parameters[24].Value = model.OrderStatus;
            parameters[25].Value = model.UserCyDesc;
            parameters[26].Value = model.Amount;
            parameters[27].Value = model.OrderDate;
            parameters[28].Value = model.PayDate;
            parameters[29].Value = model.PType;
            parameters[30].Value = model.Flag;
            parameters[31].Value = model.Remark;
            SqlHelper.ExecuteSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_Invoice model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_Invoice set ");

            strSql.Append(" InvoiceNo = @InvoiceNo , ");
            strSql.Append(" ServAmount = @ServAmount , ");
            strSql.Append(" Crtby = @Crtby , ");
            strSql.Append(" Crtdate = @Crtdate , ");
            strSql.Append(" fsn = @fsn , ");
            strSql.Append(" FAreaCode = @FAreaCode , ");
            strSql.Append(" FAreaName = @FAreaName , ");
            strSql.Append(" FCriminal = @FCriminal , ");
            strSql.Append(" Frealareacode = @Frealareacode , ");
            strSql.Append(" FrealAreaName = @FrealAreaName , ");
            strSql.Append(" TypeFlag = @TypeFlag , ");
            strSql.Append(" CardCode = @CardCode , ");
            strSql.Append(" CardType = @CardType , ");
            strSql.Append(" AmountA = @AmountA , ");
            strSql.Append(" AmountB = @AmountB , ");
            strSql.Append(" Fifoflag = @Fifoflag , ");
            strSql.Append(" FreeAmountA = @FreeAmountA , ");
            strSql.Append(" FreeAmountB = @FreeAmountB , ");
            strSql.Append(" Checkflag = @Checkflag , ");
            strSql.Append(" RoomNo = @RoomNo , ");
            strSql.Append(" OrderId = @OrderId , ");
            strSql.Append(" FTZSP_Money = @FTZSP_Money , ");
            strSql.Append(" FCrimeCode = @FCrimeCode , ");
            strSql.Append(" printCount = @printCount , ");
            strSql.Append(" OrderStatus = @OrderStatus , ");
            strSql.Append(" UserCyDesc = @UserCyDesc , ");
            strSql.Append(" Amount = @Amount , ");
            strSql.Append(" OrderDate = @OrderDate , ");
            strSql.Append(" PayDate = @PayDate , ");
            strSql.Append(" PType = @PType , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" Remark = @Remark  ");
            strSql.Append(" where InvoiceNo=@InvoiceNo  ");

            SqlParameter[] parameters = {
			            new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@ServAmount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FCriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeAmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeAmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Checkflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@RoomNo", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@OrderId", SqlDbType.Int,4) ,            
                        new SqlParameter("@FTZSP_Money", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FCrimeCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@printCount", SqlDbType.Int,4) ,            
                        new SqlParameter("@OrderStatus", SqlDbType.Int,4) ,            
                        new SqlParameter("@UserCyDesc", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PType", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,512)             
              
            };

            parameters[0].Value = model.InvoiceNo;
            parameters[1].Value = model.ServAmount;
            parameters[2].Value = model.Crtby;
            parameters[3].Value = model.Crtdate;
            parameters[4].Value = model.fsn;
            parameters[5].Value = model.FAreaCode;
            parameters[6].Value = model.FAreaName;
            parameters[7].Value = model.FCriminal;
            parameters[8].Value = model.Frealareacode;
            parameters[9].Value = model.FrealAreaName;
            parameters[10].Value = model.TypeFlag;
            parameters[11].Value = model.CardCode;
            parameters[12].Value = model.CardType;
            parameters[13].Value = model.AmountA;
            parameters[14].Value = model.AmountB;
            parameters[15].Value = model.Fifoflag;
            parameters[16].Value = model.FreeAmountA;
            parameters[17].Value = model.FreeAmountB;
            parameters[18].Value = model.Checkflag;
            parameters[19].Value = model.RoomNo;
            parameters[20].Value = model.OrderId;
            parameters[21].Value = model.FTZSP_Money;
            parameters[22].Value = model.FCrimeCode;
            parameters[23].Value = model.printCount;
            parameters[24].Value = model.OrderStatus;
            parameters[25].Value = model.UserCyDesc;
            parameters[26].Value = model.Amount;
            parameters[27].Value = model.OrderDate;
            parameters[28].Value = model.PayDate;
            parameters[29].Value = model.PType;
            parameters[30].Value = model.Flag;
            parameters[31].Value = model.Remark;
            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string InvoiceNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_Invoice ");
            strSql.Append(" where InvoiceNo=@InvoiceNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20)			};
            parameters[0].Value = InvoiceNo;


            int rows = SqlHelper.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SelfhelpOrderMgr.Model.T_Invoice GetModel(string InvoiceNo)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select InvoiceNo, ServAmount, Crtby, Crtdate, fsn, FAreaCode, FAreaName, FCriminal, Frealareacode, FrealAreaName, TypeFlag, CardCode, CardType, AmountA, AmountB, Fifoflag, FreeAmountA, FreeAmountB, Checkflag, RoomNo, OrderId, FTZSP_Money, FCrimeCode, printCount, OrderStatus, UserCyDesc, Amount, OrderDate, PayDate, PType, Flag, Remark  ");
            strSql.Append("  from T_Invoice ");
            strSql.Append(" where InvoiceNo=@InvoiceNo ");
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
                if (ds.Tables[0].Rows[0]["FTZSP_Money"].ToString() != "")
                {
                    model.FTZSP_Money = decimal.Parse(ds.Tables[0].Rows[0]["FTZSP_Money"].ToString());
                }
                model.FCrimeCode = ds.Tables[0].Rows[0]["FCrimeCode"].ToString();
                if (ds.Tables[0].Rows[0]["printCount"].ToString() != "")
                {
                    model.printCount = int.Parse(ds.Tables[0].Rows[0]["printCount"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderStatus"].ToString() != "")
                {
                    model.OrderStatus = int.Parse(ds.Tables[0].Rows[0]["OrderStatus"].ToString());
                }
                model.UserCyDesc = ds.Tables[0].Rows[0]["UserCyDesc"].ToString();
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_Invoice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SqlHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM T_Invoice ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

