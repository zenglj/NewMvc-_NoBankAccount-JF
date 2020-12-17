using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_bank_regList
		public partial class t_bank_regListDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.t_bank_regList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_bank_regList(");			
            strSql.Append("AccNo,fcrimecode,fname,PrisonNo,Flag,LoadDate");
			strSql.Append(") values (");
            strSql.Append("@AccNo,@fcrimecode,@fname,@PrisonNo,@Flag,@LoadDate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PrisonNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.AccNo;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.fname;                        
            parameters[3].Value = model.PrisonNo;                        
            parameters[4].Value = model.Flag;                        
            parameters[5].Value = model.LoadDate;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.t_bank_regList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_bank_regList set ");
			                                                
            strSql.Append(" AccNo = @AccNo , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" fname = @fname , ");                                    
            strSql.Append(" PrisonNo = @PrisonNo , ");                                    
            strSql.Append(" Flag = @Flag , ");                                    
            strSql.Append(" LoadDate = @LoadDate  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@AccNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@PrisonNo", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@Flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@LoadDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.AccNo;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.fname;                        
            parameters[4].Value = model.PrisonNo;                        
            parameters[5].Value = model.Flag;                        
            parameters[6].Value = model.LoadDate;                        
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
			strSql.Append("delete from t_bank_regList ");
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
			strSql.Append("delete from t_bank_regList ");
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
		public SelfhelpOrderMgr.Model.t_bank_regList GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, AccNo, fcrimecode, fname, PrisonNo, Flag, LoadDate  ");			
			strSql.Append("  from t_bank_regList ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.t_bank_regList model=new SelfhelpOrderMgr.Model.t_bank_regList();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.AccNo= ds.Tables[0].Rows[0]["AccNo"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																																model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																																model.PrisonNo= ds.Tables[0].Rows[0]["PrisonNo"].ToString();
																												if(ds.Tables[0].Rows[0]["Flag"].ToString()!="")
				{
					model.Flag=int.Parse(ds.Tables[0].Rows[0]["Flag"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["LoadDate"].ToString()!="")
				{
					model.LoadDate=DateTime.Parse(ds.Tables[0].Rows[0]["LoadDate"].ToString());
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
			strSql.Append("select seqno, AccNo, fcrimecode, fname, PrisonNo, Flag, LoadDate  ");
			strSql.Append(" FROM t_bank_regList ");
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
			strSql.Append(" seqno, AccNo, fcrimecode, fname, PrisonNo, Flag, LoadDate  ");
			strSql.Append(" FROM t_bank_regList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

