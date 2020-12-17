using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_TreeMenu_User
		public partial class t_TreeMenu_UserDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.t_TreeMenu_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_TreeMenu_User(");			
            strSql.Append("fcode,TreeId,flag");
			strSql.Append(") values (");
            strSql.Append("@fcode,@TreeId,@flag");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@TreeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.fcode;                        
            parameters[1].Value = model.TreeId;                        
            parameters[2].Value = model.flag;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.t_TreeMenu_User model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_TreeMenu_User set ");
			                                                
            strSql.Append(" fcode = @fcode , ");                                    
            strSql.Append(" TreeId = @TreeId , ");                                    
            strSql.Append(" flag = @flag  ");            			
			strSql.Append(" where id=@id ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@TreeId", SqlDbType.Int,4) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.id;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.TreeId;                        
            parameters[3].Value = model.flag;                        
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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_TreeMenu_User ");
			strSql.Append(" where id=@id");
						SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;


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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from t_TreeMenu_User ");
			strSql.Append(" where ID in ("+idlist + ")  ");
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
		public SelfhelpOrderMgr.Model.t_TreeMenu_User GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, fcode, TreeId, flag  ");			
			strSql.Append("  from t_TreeMenu_User ");
			strSql.Append(" where id=@id");
						SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			
			SelfhelpOrderMgr.Model.t_TreeMenu_User model=new SelfhelpOrderMgr.Model.t_TreeMenu_User();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
																																				model.fcode= ds.Tables[0].Rows[0]["fcode"].ToString();
																												if(ds.Tables[0].Rows[0]["TreeId"].ToString()!="")
				{
					model.TreeId=int.Parse(ds.Tables[0].Rows[0]["TreeId"].ToString());
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
			strSql.Append("select id, fcode, TreeId, flag  ");
			strSql.Append(" FROM t_TreeMenu_User ");
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
			strSql.Append(" id, fcode, TreeId, flag  ");
			strSql.Append(" FROM t_TreeMenu_User ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

