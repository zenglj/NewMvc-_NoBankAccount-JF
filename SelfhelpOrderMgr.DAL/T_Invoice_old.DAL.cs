using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Invoice_old
		public partial class T_Invoice_oldDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Invoice_old model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Invoice_old(");			
            strSql.Append("INVOICENO,cardcode,fcrimecode,amount,OrderDate,PayDATE,PTYPE,Flag,REMARK,servamount,crtby,crtdate,fsn,fareacode,fareaName,fcriminal,Frealareacode,FrealAreaName,TYPEFLAG,CardType,AmountA,AmountB,fifoflag,FreeAmountA,FreeAmountB,checkflag");
			strSql.Append(") values (");
            strSql.Append("@INVOICENO,@cardcode,@fcrimecode,@amount,@OrderDate,@PayDATE,@PTYPE,@Flag,@REMARK,@servamount,@crtby,@crtdate,@fsn,@fareacode,@fareaName,@fcriminal,@Frealareacode,@FrealAreaName,@TYPEFLAG,@CardType,@AmountA,@AmountB,@fifoflag,@FreeAmountA,@FreeAmountB,@checkflag");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@INVOICENO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@PTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@servamount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeAmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeAmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@checkflag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.INVOICENO;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.amount;                        
            parameters[4].Value = model.OrderDate;                        
            parameters[5].Value = model.PayDATE;                        
            parameters[6].Value = model.PTYPE;                        
            parameters[7].Value = model.Flag;                        
            parameters[8].Value = model.REMARK;                        
            parameters[9].Value = model.servamount;                        
            parameters[10].Value = model.crtby;                        
            parameters[11].Value = model.crtdate;                        
            parameters[12].Value = model.fsn;                        
            parameters[13].Value = model.fareacode;                        
            parameters[14].Value = model.fareaName;                        
            parameters[15].Value = model.fcriminal;                        
            parameters[16].Value = model.Frealareacode;                        
            parameters[17].Value = model.FrealAreaName;                        
            parameters[18].Value = model.TYPEFLAG;                        
            parameters[19].Value = model.CardType;                        
            parameters[20].Value = model.AmountA;                        
            parameters[21].Value = model.AmountB;                        
            parameters[22].Value = model.fifoflag;                        
            parameters[23].Value = model.FreeAmountA;                        
            parameters[24].Value = model.FreeAmountB;                        
            parameters[25].Value = model.checkflag;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_old model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Invoice_old set ");
			                        
            strSql.Append(" INVOICENO = @INVOICENO , ");                                    
            strSql.Append(" cardcode = @cardcode , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" amount = @amount , ");                                    
            strSql.Append(" OrderDate = @OrderDate , ");                                    
            strSql.Append(" PayDATE = @PayDATE , ");                                    
            strSql.Append(" PTYPE = @PTYPE , ");                                    
            strSql.Append(" Flag = @Flag , ");                                    
            strSql.Append(" REMARK = @REMARK , ");                                    
            strSql.Append(" servamount = @servamount , ");                                    
            strSql.Append(" crtby = @crtby , ");                                    
            strSql.Append(" crtdate = @crtdate , ");                                    
            strSql.Append(" fsn = @fsn , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" Frealareacode = @Frealareacode , ");                                    
            strSql.Append(" FrealAreaName = @FrealAreaName , ");                                    
            strSql.Append(" TYPEFLAG = @TYPEFLAG , ");                                    
            strSql.Append(" CardType = @CardType , ");                                    
            strSql.Append(" AmountA = @AmountA , ");                                    
            strSql.Append(" AmountB = @AmountB , ");                                    
            strSql.Append(" fifoflag = @fifoflag , ");                                    
            strSql.Append(" FreeAmountA = @FreeAmountA , ");                                    
            strSql.Append(" FreeAmountB = @FreeAmountB , ");                                    
            strSql.Append(" checkflag = @checkflag  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@INVOICENO", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@cardcode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@PayDATE", SqlDbType.DateTime) ,            
                        new SqlParameter("@PTYPE", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@REMARK", SqlDbType.VarChar,512) ,            
                        new SqlParameter("@servamount", SqlDbType.Decimal,5) ,            
                        new SqlParameter("@crtby", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdate", SqlDbType.DateTime) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Frealareacode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FrealAreaName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@TYPEFLAG", SqlDbType.Int,4) ,            
                        new SqlParameter("@CardType", SqlDbType.Int,4) ,            
                        new SqlParameter("@AmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@AmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@fifoflag", SqlDbType.Int,4) ,            
                        new SqlParameter("@FreeAmountA", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@FreeAmountB", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@checkflag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.INVOICENO;                        
            parameters[1].Value = model.cardcode;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.amount;                        
            parameters[4].Value = model.OrderDate;                        
            parameters[5].Value = model.PayDATE;                        
            parameters[6].Value = model.PTYPE;                        
            parameters[7].Value = model.Flag;                        
            parameters[8].Value = model.REMARK;                        
            parameters[9].Value = model.servamount;                        
            parameters[10].Value = model.crtby;                        
            parameters[11].Value = model.crtdate;                        
            parameters[12].Value = model.fsn;                        
            parameters[13].Value = model.fareacode;                        
            parameters[14].Value = model.fareaName;                        
            parameters[15].Value = model.fcriminal;                        
            parameters[16].Value = model.Frealareacode;                        
            parameters[17].Value = model.FrealAreaName;                        
            parameters[18].Value = model.TYPEFLAG;                        
            parameters[19].Value = model.CardType;                        
            parameters[20].Value = model.AmountA;                        
            parameters[21].Value = model.AmountB;                        
            parameters[22].Value = model.fifoflag;                        
            parameters[23].Value = model.FreeAmountA;                        
            parameters[24].Value = model.FreeAmountB;                        
            parameters[25].Value = model.checkflag;                        
            int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Invoice_old ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};


			int rows=SqlHelper.ExecuteSql(strSql.ToString(),parameters);
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
		public SelfhelpOrderMgr.Model.T_Invoice_old GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select INVOICENO, cardcode, fcrimecode, amount, OrderDate, PayDATE, PTYPE, Flag, REMARK, servamount, crtby, crtdate, fsn, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, TYPEFLAG, CardType, AmountA, AmountB, fifoflag, FreeAmountA, FreeAmountB, checkflag  ");			
			strSql.Append("  from T_Invoice_old ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Invoice_old model=new SelfhelpOrderMgr.Model.T_Invoice_old();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.INVOICENO= ds.Tables[0].Rows[0]["INVOICENO"].ToString();
																																model.cardcode= ds.Tables[0].Rows[0]["cardcode"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																												if(ds.Tables[0].Rows[0]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["PayDATE"].ToString()!="")
				{
					model.PayDATE=DateTime.Parse(ds.Tables[0].Rows[0]["PayDATE"].ToString());
				}
																																				model.PTYPE= ds.Tables[0].Rows[0]["PTYPE"].ToString();
																												if(ds.Tables[0].Rows[0]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
				}
																																				model.REMARK= ds.Tables[0].Rows[0]["REMARK"].ToString();
																												if(ds.Tables[0].Rows[0]["servamount"].ToString()!="")
				{
					model.servamount=decimal.Parse(ds.Tables[0].Rows[0]["servamount"].ToString());
				}
																																				model.crtby= ds.Tables[0].Rows[0]["crtby"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdate"].ToString()!="")
				{
					model.crtdate=DateTime.Parse(ds.Tables[0].Rows[0]["crtdate"].ToString());
				}
																																				model.fsn= ds.Tables[0].Rows[0]["fsn"].ToString();
																																model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																																model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
																																model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
																																model.Frealareacode= ds.Tables[0].Rows[0]["Frealareacode"].ToString();
																																model.FrealAreaName= ds.Tables[0].Rows[0]["FrealAreaName"].ToString();
																												if(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString()!="")
				{
					model.TYPEFLAG=int.Parse(ds.Tables[0].Rows[0]["TYPEFLAG"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["CardType"].ToString()!="")
				{
					model.CardType=int.Parse(ds.Tables[0].Rows[0]["CardType"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["AmountA"].ToString()!="")
				{
					model.AmountA=decimal.Parse(ds.Tables[0].Rows[0]["AmountA"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["AmountB"].ToString()!="")
				{
					model.AmountB=decimal.Parse(ds.Tables[0].Rows[0]["AmountB"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["fifoflag"].ToString()!="")
				{
					model.fifoflag=int.Parse(ds.Tables[0].Rows[0]["fifoflag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FreeAmountA"].ToString()!="")
				{
					model.FreeAmountA=decimal.Parse(ds.Tables[0].Rows[0]["FreeAmountA"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FreeAmountB"].ToString()!="")
				{
					model.FreeAmountB=decimal.Parse(ds.Tables[0].Rows[0]["FreeAmountB"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["checkflag"].ToString()!="")
				{
					model.checkflag=int.Parse(ds.Tables[0].Rows[0]["checkflag"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select INVOICENO, cardcode, fcrimecode, amount, OrderDate, PayDATE, PTYPE, Flag, REMARK, servamount, crtby, crtdate, fsn, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, TYPEFLAG, CardType, AmountA, AmountB, fifoflag, FreeAmountA, FreeAmountB, checkflag  ");
			strSql.Append(" FROM T_Invoice_old ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return SqlHelper.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" INVOICENO, cardcode, fcrimecode, amount, OrderDate, PayDATE, PTYPE, Flag, REMARK, servamount, crtby, crtdate, fsn, fareacode, fareaName, fcriminal, Frealareacode, FrealAreaName, TYPEFLAG, CardType, AmountA, AmountB, fifoflag, FreeAmountA, FreeAmountB, checkflag  ");
			strSql.Append(" FROM T_Invoice_old ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

