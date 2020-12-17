using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_CommonTypeTab
		public partial class T_CommonTypeTabDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(SelfhelpOrderMgr.Model.T_CommonTypeTab model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_CommonTypeTab(");			
            strSql.Append("FType,FCode,FName,FRemark");
			strSql.Append(") values (");
            strSql.Append("@FType,@FCode,@FName,@FRemark");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FRemark", SqlDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.FType;                        
            parameters[1].Value = model.FCode;                        
            parameters[2].Value = model.FName;                        
            parameters[3].Value = model.FRemark;                        
			   
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
		public bool Update(SelfhelpOrderMgr.Model.T_CommonTypeTab model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_CommonTypeTab set ");
			                                                
            strSql.Append(" FType = @FType , ");                                    
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FRemark = @FRemark  ");            			
			strSql.Append(" where ID=@ID ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        new SqlParameter("@FType", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FCode", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@FRemark", SqlDbType.VarChar,50)             
              
            };
						            
            parameters[0].Value = model.ID;                        
            parameters[1].Value = model.FType;                        
            parameters[2].Value = model.FCode;                        
            parameters[3].Value = model.FName;                        
            parameters[4].Value = model.FRemark;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_CommonTypeTab ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_CommonTypeTab ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public SelfhelpOrderMgr.Model.T_CommonTypeTab GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, FType, FCode, FName, FRemark  ");			
			strSql.Append("  from T_CommonTypeTab ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			SelfhelpOrderMgr.Model.T_CommonTypeTab model=new SelfhelpOrderMgr.Model.T_CommonTypeTab();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.FType= ds.Tables[0].Rows[0]["FType"].ToString();
																																model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.FRemark= ds.Tables[0].Rows[0]["FRemark"].ToString();
																										
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
			strSql.Append("select ID, FType, FCode, FName, FRemark  ");
			strSql.Append(" FROM T_CommonTypeTab ");
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
			strSql.Append(" ID, FType, FCode, FName, FRemark  ");
			strSql.Append(" FROM T_CommonTypeTab ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

