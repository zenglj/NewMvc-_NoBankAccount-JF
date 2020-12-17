using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_ye
		public partial class t_yeDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.t_ye model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_ye(");			
            strSql.Append("accno,fcrimecode,amount,flag");
			strSql.Append(") values (");
            strSql.Append("@accno,@fcrimecode,@amount,@flag");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@accno", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.accno;                        
            parameters[1].Value = model.fcrimecode;                        
            parameters[2].Value = model.amount;                        
            parameters[3].Value = model.flag;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.t_ye model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_ye set ");
			                                                
            strSql.Append(" accno = @accno , ");                                    
            strSql.Append(" fcrimecode = @fcrimecode , ");                                    
            strSql.Append(" amount = @amount , ");                                    
            strSql.Append(" flag = @flag  ");            			
			strSql.Append(" where seqno=@seqno ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@seqno", SqlDbType.Int,4) ,            
                        new SqlParameter("@accno", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@fcrimecode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@amount", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.seqno;                        
            parameters[1].Value = model.accno;                        
            parameters[2].Value = model.fcrimecode;                        
            parameters[3].Value = model.amount;                        
            parameters[4].Value = model.flag;                        
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
			strSql.Append("delete from t_ye ");
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
			strSql.Append("delete from t_ye ");
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
		public SelfhelpOrderMgr.Model.t_ye GetModel(int seqno)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select seqno, accno, fcrimecode, amount, flag  ");			
			strSql.Append("  from t_ye ");
			strSql.Append(" where seqno=@seqno");
						SqlParameter[] parameters = {
					new SqlParameter("@seqno", SqlDbType.Int,4)
			};
			parameters[0].Value = seqno;

			
			SelfhelpOrderMgr.Model.t_ye model=new SelfhelpOrderMgr.Model.t_ye();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["seqno"].ToString()!="")
				{
					model.seqno=int.Parse(ds.Tables[0].Rows[0]["seqno"].ToString());
				}
																																				model.accno= ds.Tables[0].Rows[0]["accno"].ToString();
																																model.fcrimecode= ds.Tables[0].Rows[0]["fcrimecode"].ToString();
																												if(ds.Tables[0].Rows[0]["amount"].ToString()!="")
				{
					model.amount=decimal.Parse(ds.Tables[0].Rows[0]["amount"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
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
			strSql.Append("select seqno, accno, fcrimecode, amount, flag  ");
			strSql.Append(" FROM t_ye ");
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
			strSql.Append(" seqno, accno, fcrimecode, amount, flag  ");
			strSql.Append(" FROM t_ye ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

