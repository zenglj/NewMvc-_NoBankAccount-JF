using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_SHO_AreaGoodMaxCount
		public partial class T_SHO_AreaGoodMaxCountDAL
	{
   		     
		public bool Exists(string FAreaCode,string FGtxm)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SHO_AreaGoodMaxCount");
			strSql.Append(" where ");
			                                                                   strSql.Append(" FAreaCode = @FAreaCode and  ");
                                                                   strSql.Append(" FGtxm = @FGtxm  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@FAreaCode", SqlDbType.NVarChar,50),
					new SqlParameter("@FGtxm", SqlDbType.NVarChar,50)			};
			parameters[0].Value = FAreaCode;
			parameters[1].Value = FGtxm;

			return SqlHelper.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SHO_AreaGoodMaxCount(");			
            strSql.Append("FAreaCode,FAreaName,FGtxm,FGoodName,FGoodType,FGoodMaxCount");
			strSql.Append(") values (");
            strSql.Append("@FAreaCode,@FAreaName,@FGtxm,@FGoodName,@FGoodType,@FGoodMaxCount");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FAreaCode", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FAreaName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGtxm", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGoodName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FGoodType", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGoodMaxCount", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.FAreaCode;                        
            parameters[1].Value = model.FAreaName;                        
            parameters[2].Value = model.FGtxm;                        
            parameters[3].Value = model.FGoodName;                        
            parameters[4].Value = model.FGoodType;                        
            parameters[5].Value = model.FGoodMaxCount;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SHO_AreaGoodMaxCount set ");
			                                                
            strSql.Append(" FAreaCode = @FAreaCode , ");                                    
            strSql.Append(" FAreaName = @FAreaName , ");                                    
            strSql.Append(" FGtxm = @FGtxm , ");                                    
            strSql.Append(" FGoodName = @FGoodName , ");                                    
            strSql.Append(" FGoodType = @FGoodType , ");                                    
            strSql.Append(" FGoodMaxCount = @FGoodMaxCount  ");            			
			strSql.Append(" where Id=@Id ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@Id", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAreaCode", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FAreaName", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGtxm", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGoodName", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@FGoodType", SqlDbType.NVarChar,50) ,            
                        new SqlParameter("@FGoodMaxCount", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.Id;                        
            parameters[1].Value = model.FAreaCode;                        
            parameters[2].Value = model.FAreaName;                        
            parameters[3].Value = model.FGtxm;                        
            parameters[4].Value = model.FGoodName;                        
            parameters[5].Value = model.FGoodType;                        
            parameters[6].Value = model.FGoodMaxCount;                        
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
			strSql.Append("delete from T_SHO_AreaGoodMaxCount ");
			strSql.Append(" where Id=@Id");
						SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SHO_AreaGoodMaxCount ");
			strSql.Append(" where ID in ("+Idlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id, FAreaCode, FAreaName, FGtxm, FGoodName, FGoodType, FGoodMaxCount  ");			
			strSql.Append("  from T_SHO_AreaGoodMaxCount ");
			strSql.Append(" where Id=@Id");
						SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			
			SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount model=new SelfhelpOrderMgr.Model.T_SHO_AreaGoodMaxCount();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
																																				model.FAreaCode= ds.Tables[0].Rows[0]["FAreaCode"].ToString();
																																model.FAreaName= ds.Tables[0].Rows[0]["FAreaName"].ToString();
																																model.FGtxm= ds.Tables[0].Rows[0]["FGtxm"].ToString();
																																model.FGoodName= ds.Tables[0].Rows[0]["FGoodName"].ToString();
																																model.FGoodType= ds.Tables[0].Rows[0]["FGoodType"].ToString();
																												if(ds.Tables[0].Rows[0]["FGoodMaxCount"].ToString()!="")
				{
					model.FGoodMaxCount=int.Parse(ds.Tables[0].Rows[0]["FGoodMaxCount"].ToString());
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
			strSql.Append("select * ");
			strSql.Append(" FROM T_SHO_AreaGoodMaxCount ");
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
			strSql.Append(" FROM T_SHO_AreaGoodMaxCount ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

