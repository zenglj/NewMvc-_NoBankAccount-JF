using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Invoice_outdtl
		public partial class T_Invoice_outdtlDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Invoice_outdtl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Invoice_outdtl(");			
            strSql.Append("fsn,InvoiceNo,Fcrimecode,fcriminal,fareaName,OrderDate,Amount");
			strSql.Append(") values (");
            strSql.Append("@fsn,@InvoiceNo,@Fcrimecode,@fcriminal,@fareaName,@OrderDate,@Amount");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9)             
              
            };
			            
            parameters[0].Value = model.fsn;                        
            parameters[1].Value = model.InvoiceNo;                        
            parameters[2].Value = model.Fcrimecode;                        
            parameters[3].Value = model.fcriminal;                        
            parameters[4].Value = model.fareaName;                        
            parameters[5].Value = model.OrderDate;                        
            parameters[6].Value = model.Amount;                        
			   
			object obj = SqlHelper.GetSingle(strSql.ToString(),parameters);			
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
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_outdtl model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Invoice_outdtl set ");
			                                                
            strSql.Append(" fsn = @fsn , ");                                    
            strSql.Append(" InvoiceNo = @InvoiceNo , ");                                    
            strSql.Append(" Fcrimecode = @Fcrimecode , ");                                    
            strSql.Append(" fcriminal = @fcriminal , ");                                    
            strSql.Append(" fareaName = @fareaName , ");                                    
            strSql.Append(" OrderDate = @OrderDate , ");                                    
            strSql.Append(" Amount = @Amount  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@InvoiceNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcriminal", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareaName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@OrderDate", SqlDbType.DateTime) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fsn;                        
            parameters[2].Value = model.InvoiceNo;                        
            parameters[3].Value = model.Fcrimecode;                        
            parameters[4].Value = model.fcriminal;                        
            parameters[5].Value = model.fareaName;                        
            parameters[6].Value = model.OrderDate;                        
            parameters[7].Value = model.Amount;                        
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
		public bool Delete(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Invoice_outdtl ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;


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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string seqnolist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_Invoice_outdtl ");
			strSql.Append(" where ID in ("+seqnolist + ")  ");
			int rows=SqlHelper.ExecuteSql(strSql.ToString());
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
		public SelfhelpOrderMgr.Model.T_Invoice_outdtl GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fsn, InvoiceNo, Fcrimecode, fcriminal, fareaName, OrderDate, Amount  ");			
			strSql.Append("  from T_Invoice_outdtl ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Invoice_outdtl model=new SelfhelpOrderMgr.Model.T_Invoice_outdtl();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fsn= ds.Tables[0].Rows[0]["fsn"].ToString();
																																model.InvoiceNo= ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
																																model.Fcrimecode= ds.Tables[0].Rows[0]["Fcrimecode"].ToString();
																																model.fcriminal= ds.Tables[0].Rows[0]["fcriminal"].ToString();
																																model.fareaName= ds.Tables[0].Rows[0]["fareaName"].ToString();
																												if(ds.Tables[0].Rows[0]["OrderDate"].ToString()!="")
				{
					model.OrderDate=DateTime.Parse(ds.Tables[0].Rows[0]["OrderDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
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
			strSql.Append("select seqno, fsn, InvoiceNo, Fcrimecode, fcriminal, fareaName, OrderDate, Amount  ");
			strSql.Append(" FROM T_Invoice_outdtl ");
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
			strSql.Append(" seqno, fsn, InvoiceNo, Fcrimecode, fcriminal, fareaName, OrderDate, Amount  ");
			strSql.Append(" FROM T_Invoice_outdtl ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

