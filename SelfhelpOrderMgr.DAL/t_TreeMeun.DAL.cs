using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_TreeMeun
		public partial class t_TreeMeunDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_TreeMeun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_TreeMeun(");			
            strSql.Append("id,fcode,flag,Text,FId,URL");
			strSql.Append(") values (");
            strSql.Append("@id,@fcode,@flag,@Text,@FId,@URL");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Text", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FId", SqlDbType.Int,4) ,            
                        new SqlParameter("@URL", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.id;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.flag;                        
            parameters[3].Value = model.Text;                        
            parameters[4].Value = model.FId;                        
            parameters[5].Value = model.URL;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_TreeMeun model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_TreeMeun set ");
			                        
            strSql.Append(" id = @id , ");                                    
            strSql.Append(" fcode = @fcode , ");                                    
            strSql.Append(" flag = @flag , ");                                    
            strSql.Append(" Text = @Text , ");                                    
            strSql.Append(" FId = @FId , ");                                    
            strSql.Append(" URL = @URL  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@id", SqlDbType.Int,4) ,            
                        new SqlParameter("@fcode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@flag", SqlDbType.Int,4) ,            
                        new SqlParameter("@Text", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FId", SqlDbType.Int,4) ,            
                        new SqlParameter("@URL", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.id;                        
            parameters[1].Value = model.fcode;                        
            parameters[2].Value = model.flag;                        
            parameters[3].Value = model.Text;                        
            parameters[4].Value = model.FId;                        
            parameters[5].Value = model.URL;                        
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
			strSql.Append("delete from t_TreeMeun ");
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
		public SelfhelpOrderMgr.Model.t_TreeMeun GetModel( int treeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id, fcode, flag, Text, FId, URL  ");			
			strSql.Append("  from t_TreeMeun ");
			strSql.Append(" where id=@id");
			    SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = treeId;
			SelfhelpOrderMgr.Model.t_TreeMeun model=new SelfhelpOrderMgr.Model.t_TreeMeun();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["id"].ToString()!="")
				{
					model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
				}
																																				model.fcode= ds.Tables[0].Rows[0]["fcode"].ToString();
																												if(ds.Tables[0].Rows[0]["flag"].ToString()!="")
				{
					model.flag=int.Parse(ds.Tables[0].Rows[0]["flag"].ToString());
				}
																																				model.Text= ds.Tables[0].Rows[0]["Text"].ToString();
																												if(ds.Tables[0].Rows[0]["FId"].ToString()!="")
				{
					model.FId=int.Parse(ds.Tables[0].Rows[0]["FId"].ToString());
				}
																																				model.URL= ds.Tables[0].Rows[0]["URL"].ToString();
																										
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
			strSql.Append("select id, fcode, flag, Text, FId, URL  ");
			strSql.Append(" FROM t_TreeMeun ");
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
			strSql.Append(" id, fcode, flag, Text, FId, URL  ");
			strSql.Append(" FROM t_TreeMeun ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

