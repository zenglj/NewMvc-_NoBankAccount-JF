using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using Dapper;
using SelfhelpOrderMgr.Model; 
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SHO_GoodsAttribute
		public partial class T_SHO_GoodsAttributeDAL
	{
   		     
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SHO_GoodsAttribute");
			strSql.Append(" where ");
			                                       strSql.Append(" Id = @Id  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = Id;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_SHO_GoodsAttribute model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SHO_GoodsAttribute(");			
            strSql.Append("Id,AttributeName,Remark");
			strSql.Append(") values (");
            strSql.Append("@Id,@AttributeName,@Remark");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@AttributeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.Id;                        
            parameters[1].Value = model.AttributeName;                        
            parameters[2].Value = model.Remark;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_GoodsAttribute model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SHO_GoodsAttribute set ");
			                        
            strSql.Append(" Id = @Id , ");                                    
            strSql.Append(" AttributeName = @AttributeName , ");                                    
            strSql.Append(" Remark = @Remark  ");            			
			strSql.Append(" where Id=@Id  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@AttributeName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@Remark", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.Id;                        
            parameters[1].Value = model.AttributeName;                        
            parameters[2].Value = model.Remark;                        
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
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SHO_GoodsAttribute ");
			strSql.Append(" where Id=@Id ");
						SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = Id;


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
		public SelfhelpOrderMgr.Model.T_SHO_GoodsAttribute GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, AttributeName, Remark  ");			
			strSql.Append("  from T_SHO_GoodsAttribute ");
			strSql.Append(" where Id=@Id ");
						SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
			parameters[0].Value = Id;

			
			SelfhelpOrderMgr.Model.T_SHO_GoodsAttribute model=new SelfhelpOrderMgr.Model.T_SHO_GoodsAttribute();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.AttributeName= ds.Tables[0].Rows[0]["AttributeName"].ToString();
																																model.Remark= ds.Tables[0].Rows[0]["Remark"].ToString();
																										
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
			strSql.Append("select * ");
			strSql.Append(" FROM T_SHO_GoodsAttribute ");
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
			strSql.Append(" * ");
			strSql.Append(" FROM T_SHO_GoodsAttribute ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

