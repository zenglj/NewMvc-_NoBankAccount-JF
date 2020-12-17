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

        public bool Exists( int seqno)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from T_InvoiceDTL");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;

            return SqlHelper.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(SelfhelpOrderMgr.Model.T_InvoiceDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_InvoiceDTL(");
            strSql.Append("AMOUNT,Servamount,GTXM,FCrimecode,GORGDJ,GORGAMT,StockSeqno,Typeflag,Cardtype,AmountA,INVOICENO,AmountB,Fifoflag,Backqty,FreeFlag,Remark,SPShortCode,FTZSP_TypeFlag,GCODE,GNAME,OrderDate,PayDATE,Flag,GDJ,QTY");
            strSql.Append(") values (");
            strSql.Append("@AMOUNT,@Servamount,@GTXM,@FCrimecode,@GORGDJ,@GORGAMT,@StockSeqno,@Typeflag,@Cardtype,@AmountA,@INVOICENO,@AmountB,@Fifoflag,@Backqty,@FreeFlag,@Remark,@SPShortCode,@FTZSP_TypeFlag,@GCODE,@GNAME,@OrderDate,@PayDATE,@Flag,@GDJ,@QTY");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@AMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Servamount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GORGDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GORGAMT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@StockSeqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Cardtype", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@INVOICENO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Backqty", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@QTY", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.AMOUNT;
            parameters[1].Value = model.Servamount;
            parameters[2].Value = model.GTXM;
            parameters[3].Value = model.FCrimecode;
            parameters[4].Value = model.GORGDJ;
            parameters[5].Value = model.GORGAMT;
            parameters[6].Value = model.StockSeqno;
            parameters[7].Value = model.Typeflag;
            parameters[8].Value = model.Cardtype;
            parameters[9].Value = model.AmountA;
            parameters[10].Value = model.INVOICENO;
            parameters[11].Value = model.AmountB;
            parameters[12].Value = model.Fifoflag;
            parameters[13].Value = model.Backqty;
            parameters[14].Value = model.FreeFlag;
            parameters[15].Value = model.Remark;
            parameters[16].Value = model.SPShortCode;
            parameters[17].Value = model.FTZSP_TypeFlag;
            parameters[18].Value = model.GCODE;
            parameters[19].Value = model.GNAME;
            parameters[20].Value = model.OrderDate;
            parameters[21].Value = model.PayDATE;
            parameters[22].Value = model.Flag;
            parameters[23].Value = model.GDJ;
            parameters[24].Value = model.QTY;

            object obj = SqlHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SelfhelpOrderMgr.Model.T_InvoiceDTL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_InvoiceDTL set ");

            strSql.Append(" AMOUNT = @AMOUNT , ");
            strSql.Append(" Servamount = @Servamount , ");
            strSql.Append(" GTXM = @GTXM , ");
            strSql.Append(" FCrimecode = @FCrimecode , ");
            strSql.Append(" GORGDJ = @GORGDJ , ");
            strSql.Append(" GORGAMT = @GORGAMT , ");
            strSql.Append(" StockSeqno = @StockSeqno , ");
            strSql.Append(" Typeflag = @Typeflag , ");
            strSql.Append(" Cardtype = @Cardtype , ");
            strSql.Append(" AmountA = @AmountA , ");
            strSql.Append(" INVOICENO = @INVOICENO , ");
            strSql.Append(" AmountB = @AmountB , ");
            strSql.Append(" Fifoflag = @Fifoflag , ");
            strSql.Append(" Backqty = @Backqty , ");
            strSql.Append(" FreeFlag = @FreeFlag , ");
            strSql.Append(" Remark = @Remark , ");
            strSql.Append(" SPShortCode = @SPShortCode , ");
            strSql.Append(" FTZSP_TypeFlag = @FTZSP_TypeFlag , ");
            strSql.Append(" GCODE = @GCODE , ");
            strSql.Append(" GNAME = @GNAME , ");
            strSql.Append(" OrderDate = @OrderDate , ");
            strSql.Append(" PayDATE = @PayDATE , ");
            strSql.Append(" Flag = @Flag , ");
            strSql.Append(" GDJ = @GDJ , ");
            strSql.Append(" QTY = @QTY  ");
            strSql.Append(" where seqno=@seqno ");

            SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@AMOUNT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@Servamount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GTXM", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GORGDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@GORGAMT", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@StockSeqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@Typeflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Cardtype", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@INVOICENO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@Fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Backqty", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@SPShortCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FTZSP_TypeFlag", SqlDbType.Int,4) ,            
                        new SqlParameter("@GCODE", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@GNAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@GDJ", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@QTY", SqlDbType.Decimal,9)             
              
            };

            parameters[0].Value = model.seqno;
            parameters[1].Value = model.AMOUNT;
            parameters[2].Value = model.Servamount;
            parameters[3].Value = model.GTXM;
            parameters[4].Value = model.FCrimecode;
            parameters[5].Value = model.GORGDJ;
            parameters[6].Value = model.GORGAMT;
            parameters[7].Value = model.StockSeqno;
            parameters[8].Value = model.Typeflag;
            parameters[9].Value = model.Cardtype;
            parameters[10].Value = model.AmountA;
            parameters[11].Value = model.INVOICENO;
            parameters[12].Value = model.AmountB;
            parameters[13].Value = model.Fifoflag;
            parameters[14].Value = model.Backqty;
            parameters[15].Value = model.FreeFlag;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.SPShortCode;
            parameters[18].Value = model.FTZSP_TypeFlag;
            parameters[19].Value = model.GCODE;
            parameters[20].Value = model.GNAME;
            parameters[21].Value = model.OrderDate;
            parameters[22].Value = model.PayDATE;
            parameters[23].Value = model.Flag;
            parameters[24].Value = model.GDJ;
            parameters[25].Value = model.QTY;
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
        public bool Delete(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_InvoiceDTL ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string seqnolist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_InvoiceDTL ");
            strSql.Append(" where ID in (" + seqnolist + ")  ");
            int rows = SqlHelper.ExecuteSql(strSql.ToString());
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
        public SelfhelpOrderMgr.Model.T_InvoiceDTL GetModel(int seqno)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select seqno, AMOUNT, Servamount, GTXM, FCrimecode, GORGDJ, GORGAMT, StockSeqno, Typeflag, Cardtype, AmountA, INVOICENO, AmountB, Fifoflag, Backqty, FreeFlag, Remark, SPShortCode, FTZSP_TypeFlag, GCODE, GNAME, OrderDate, PayDATE, Flag, GDJ, QTY  ");
            strSql.Append("  from T_InvoiceDTL ");
            strSql.Append(" where seqno=@seqno");
            SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
            parameters[0].Value = seqno;


            SelfhelpOrderMgr.Model.T_InvoiceDTL model = new SelfhelpOrderMgr.Model.T_InvoiceDTL();
            DataSet ds = SqlHelper.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["seqno"].ToString() != "")
                {
                    model.seqno = int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AMOUNT"].ToString() != "")
                {
                    model.AMOUNT = decimal.Parse(ds.Tables[0].Rows[0]["AMOUNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Servamount"].ToString() != "")
                {
                    model.Servamount = decimal.Parse(ds.Tables[0].Rows[0]["Servamount"].ToString());
                }
                model.GTXM = ds.Tables[0].Rows[0]["GTXM"].ToString();
                model.FCrimecode = ds.Tables[0].Rows[0]["FCrimecode"].ToString();
                if (ds.Tables[0].Rows[0]["GORGDJ"].ToString() != "")
                {
                    model.GORGDJ = decimal.Parse(ds.Tables[0].Rows[0]["GORGDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GORGAMT"].ToString() != "")
                {
                    model.GORGAMT = decimal.Parse(ds.Tables[0].Rows[0]["GORGAMT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["StockSeqno"].ToString() != "")
                {
                    model.StockSeqno = int.Parse(ds.Tables[0].Rows[0]["StockSeqno"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Typeflag"].ToString() != "")
                {
                    model.Typeflag = int.Parse(ds.Tables[0].Rows[0]["Typeflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cardtype"].ToString() != "")
                {
                    model.Cardtype = int.Parse(ds.Tables[0].Rows[0]["Cardtype"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AmountA"].ToString() != "")
                {
                    model.AmountA = decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
                }
                model.INVOICENO = ds.Tables[0].Rows[0]["INVOICENO"].ToString();
                if (ds.Tables[0].Rows[0]["AmountB"].ToString() != "")
                {
                    model.AmountB = decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Fifoflag"].ToString() != "")
                {
                    model.Fifoflag = int.Parse(ds.Tables[0].Rows[0]["Fifoflag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Backqty"].ToString() != "")
                {
                    model.Backqty = decimal.Parse(ds.Tables[0].Rows[0]["Backqty"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FreeFlag"].ToString() != "")
                {
                    model.FreeFlag = int.Parse(ds.Tables[0].Rows[0]["FreeFlag"].ToString());
                }
                model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                model.SPShortCode = ds.Tables[0].Rows[0]["SPShortCode"].ToString();
                if (ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString() != "")
                {
                    model.FTZSP_TypeFlag = int.Parse(ds.Tables[0].Rows[0]["FTZSP_TypeFlag"].ToString());
                }
                model.GCODE = ds.Tables[0].Rows[0]["GCODE"].ToString();
                model.GNAME = ds.Tables[0].Rows[0]["GNAME"].ToString();
                if (ds.Tables[0].Rows[0]["OrderDate"].ToString() != "")
                {
                    model.OrderDate = DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PayDATE"].ToString() != "")
                {
                    model.PayDATE = DateTime.Parse(ds.Tables[0].Rows[0]["PayDATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Flag"].ToString() != "")
                {
                    model.Flag = int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GDJ"].ToString() != "")
                {
                    model.GDJ = decimal.Parse(ds.Tables[0].Rows[0]["GDJ"].ToString());
                }
                if (ds.Tables[0].Rows[0]["QTY"].ToString() != "")
                {
                    model.QTY = decimal.Parse(ds.Tables[0].Rows[0]["QTY"].ToString());
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM T_InvoiceDTL ");
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
            strSql.Append(" FROM T_InvoiceDTL ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SqlHelper.Query(strSql.ToString());
        }


    }
}

