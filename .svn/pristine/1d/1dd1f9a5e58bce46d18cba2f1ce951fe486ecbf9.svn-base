using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_MainInv
		public partial class T_MainInvDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_MainInv model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MainInv(");			
            strSql.Append("fsn,fareacode,fsubareacode,fAgent,fOrderDate");
			strSql.Append(") values (");
            strSql.Append("@fsn,@fareacode,@fsubareacode,@fAgent,@fOrderDate");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fsubareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fAgent", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fOrderDate", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.fsn;                        
            parameters[1].Value = model.fareacode;                        
            parameters[2].Value = model.fsubareacode;                        
            parameters[3].Value = model.fAgent;                        
            parameters[4].Value = model.fOrderDate;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MainInv model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MainInv set ");
			                        
            strSql.Append(" fsn = @fsn , ");                                    
            strSql.Append(" fareacode = @fareacode , ");                                    
            strSql.Append(" fsubareacode = @fsubareacode , ");                                    
            strSql.Append(" fAgent = @fAgent , ");                                    
            strSql.Append(" fOrderDate = @fOrderDate  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@fsn", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fsubareacode", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fAgent", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@fOrderDate", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.fsn;                        
            parameters[1].Value = model.fareacode;                        
            parameters[2].Value = model.fsubareacode;                        
            parameters[3].Value = model.fAgent;                        
            parameters[4].Value = model.fOrderDate;                        
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
			strSql.Append("delete from T_MainInv ");
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
		public SelfhelpOrderMgr.Model.T_MainInv GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select fsn, fareacode, fsubareacode, fAgent, fOrderDate  ");			
			strSql.Append("  from T_MainInv ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_MainInv model=new SelfhelpOrderMgr.Model.T_MainInv();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.fsn= ds.Tables[0].Rows[0]["fsn"].ToString();
																																model.fareacode= ds.Tables[0].Rows[0]["fareacode"].ToString();
																																model.fsubareacode= ds.Tables[0].Rows[0]["fsubareacode"].ToString();
																																model.fAgent= ds.Tables[0].Rows[0]["fAgent"].ToString();
																												if(ds.Tables[0].Rows[0]["fOrderDate"].ToString()!="")
				{
					model.fOrderDate=DateTime.Parse(ds.Tables[0].Rows[0]["fOrderDate"].ToString());
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
			strSql.Append("select fsn, fareacode, fsubareacode, fAgent, fOrderDate  ");
			strSql.Append(" FROM T_MainInv ");
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
			strSql.Append(" fsn, fareacode, fsubareacode, fAgent, fOrderDate  ");
			strSql.Append(" FROM T_MainInv ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

