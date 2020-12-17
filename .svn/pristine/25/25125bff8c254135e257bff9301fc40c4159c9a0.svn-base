using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SETTINGS
		public partial class T_SETTINGSDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_SETTINGS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SETTINGS(");			
            strSql.Append("NAME,VALUE,TYPE,remark");
			strSql.Append(") values (");
            strSql.Append("@NAME,@VALUE,@TYPE,@remark");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@NAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@VALUE", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@TYPE", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,512)             
              
            };
			            
            parameters[0].Value = model.NAME;                        
            parameters[1].Value = model.VALUE;                        
            parameters[2].Value = model.TYPE;                        
            parameters[3].Value = model.remark;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_SETTINGS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SETTINGS set ");
			                                                
            strSql.Append(" NAME = @NAME , ");                                    
            strSql.Append(" VALUE = @VALUE , ");                                    
            strSql.Append(" TYPE = @TYPE , ");                                    
            strSql.Append(" remark = @remark  ");            			
			strSql.Append(" where SEQ=@SEQ ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@SEQ", SqlDbType.Int,4) ,            
                        new SqlParameter("@NAME", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@VALUE", SqlDbType.VarChar,32) ,            
                        new SqlParameter("@TYPE", SqlDbType.Int,4) ,            
                        new SqlParameter("@remark", SqlDbType.VarChar,512)             
              
            };
						            
            parameters[0].Value = model.SEQ;                        
            parameters[1].Value = model.NAME;                        
            parameters[2].Value = model.VALUE;                        
            parameters[3].Value = model.TYPE;                        
            parameters[4].Value = model.remark;                        
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
		public bool Delete(int SEQ)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SETTINGS ");
			strSql.Append(" where SEQ=@SEQ");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQ", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQ;


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
		public bool DeleteList(string SEQlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SETTINGS ");
			strSql.Append(" where ID in ("+SEQlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_SETTINGS GetModel(int SEQ)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SEQ, NAME, VALUE, TYPE, remark  ");			
			strSql.Append("  from T_SETTINGS ");
			strSql.Append(" where SEQ=@SEQ");
						SqlParameter[] parameters = {
					new SqlParameter("@SEQ", SqlDbType.Int,4)
			};
			parameters[0].Value = SEQ;

			
			SelfhelpOrderMgr.Model.T_SETTINGS model=new SelfhelpOrderMgr.Model.T_SETTINGS();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["SEQ"].ToString()!="")
				{
					model.SEQ=int.Parse(ds.Tables[0].Rows[0]["SEQ"].ToString());
				}
																																				model.NAME= ds.Tables[0].Rows[0]["NAME"].ToString();
																																model.VALUE= ds.Tables[0].Rows[0]["VALUE"].ToString();
																												if(ds.Tables[0].Rows[0]["TYPE"].ToString()!="")
				{
					model.TYPE=int.Parse(ds.Tables[0].Rows[0]["TYPE"].ToString());
				}
																																				model.remark= ds.Tables[0].Rows[0]["remark"].ToString();
																										
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
			strSql.Append("select SEQ, NAME, VALUE, TYPE, remark  ");
			strSql.Append(" FROM T_SETTINGS ");
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
			strSql.Append(" SEQ, NAME, VALUE, TYPE, remark  ");
			strSql.Append(" FROM T_SETTINGS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

