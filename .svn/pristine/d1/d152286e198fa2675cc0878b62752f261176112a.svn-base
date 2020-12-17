using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Invoice_out
		public partial class T_Invoice_outDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_Invoice_out model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Invoice_out(");			
            strSql.Append("fsn,Amount,CrtBy,crtdt,RcvBy,Flag,remark,typeflag");
			strSql.Append(") values (");
            strSql.Append("@fsn,@Amount,@CrtBy,@crtdt,@RcvBy,@Flag,@remark,@typeflag");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@RcvBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.fsn;                        
            parameters[1].Value = model.Amount;                        
            parameters[2].Value = model.CrtBy;                        
            parameters[3].Value = model.crtdt;                        
            parameters[4].Value = model.RcvBy;                        
            parameters[5].Value = model.Flag;                        
            parameters[6].Value = model.remark;                        
            parameters[7].Value = model.typeflag;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_Invoice_out model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Invoice_out set ");
			                                                
            strSql.Append(" fsn = @fsn , ");                                    
            strSql.Append(" Amount = @Amount , ");                                    
            strSql.Append(" CrtBy = @CrtBy , ");                                    
            strSql.Append(" crtdt = @crtdt , ");                                    
            strSql.Append(" RcvBy = @RcvBy , ");                                    
            strSql.Append(" Flag = @Flag , ");                                    
            strSql.Append(" remark = @remark , ");                                    
            strSql.Append(" typeflag = @typeflag  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@CrtBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime) ,            
                        new SqlParameter("@RcvBy", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,256) ,            
                        new SqlParameter("@typeflag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.fsn;                        
            parameters[2].Value = model.Amount;                        
            parameters[3].Value = model.CrtBy;                        
            parameters[4].Value = model.crtdt;                        
            parameters[5].Value = model.RcvBy;                        
            parameters[6].Value = model.Flag;                        
            parameters[7].Value = model.remark;                        
            parameters[8].Value = model.typeflag;                        
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
			strSql.Append("delete from T_Invoice_out ");
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
			strSql.Append("delete from T_Invoice_out ");
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
		public SelfhelpOrderMgr.Model.T_Invoice_out GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, fsn, Amount, CrtBy, crtdt, RcvBy, Flag, remark, typeflag  ");			
			strSql.Append("  from T_Invoice_out ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.T_Invoice_out model=new SelfhelpOrderMgr.Model.T_Invoice_out();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.fsn= ds.Tables[0].Rows[0]["fsn"].ToString();
																												if(ds.Tables[0].Rows[0]["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(ds.Tables[0].Rows[0]["Amount"].ToString());
				}
																																				model.CrtBy= ds.Tables[0].Rows[0]["CrtBy"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdt"].ToString()!="")
				{
					model.crtdt=DateTime.Parse(ds.Tables[0].Rows[0]["crtdt"].ToString());
				}
																																				model.RcvBy= ds.Tables[0].Rows[0]["RcvBy"].ToString();
																												if(ds.Tables[0].Rows[0]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
				}
																																				model.remark= ds.Tables[0].Rows[0]["remark"].ToString();
																												if(ds.Tables[0].Rows[0]["typeflag"].ToString()!="")
				{
					model.typeflag=int.Parse(ds.Tables[0].Rows[0]["typeflag"].ToString());
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
			strSql.Append("select seqno, fsn, Amount, CrtBy, crtdt, RcvBy, Flag, remark, typeflag  ");
			strSql.Append(" FROM T_Invoice_out ");
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
			strSql.Append(" seqno, fsn, Amount, CrtBy, crtdt, RcvBy, Flag, remark, typeflag  ");
			strSql.Append(" FROM T_Invoice_out ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

