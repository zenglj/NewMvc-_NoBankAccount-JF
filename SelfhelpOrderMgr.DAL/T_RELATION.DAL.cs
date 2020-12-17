using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_RELATION
		public partial class T_RELATIONDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_RELATION model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_RELATION(");			
            strSql.Append("FCode,FName");
			strSql.Append(") values (");
            strSql.Append("@FCode,@FName");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20)             
              
            };
			            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_RELATION model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_RELATION set ");
			                        
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" FName = @FName  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,8) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20)             
              
            };
						            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
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
			strSql.Append("delete from T_RELATION ");
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
		public SelfhelpOrderMgr.Model.T_RELATION GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCode, FName  ");			
			strSql.Append("  from T_RELATION ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_RELATION model=new SelfhelpOrderMgr.Model.T_RELATION();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																										
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
			strSql.Append("select FCode, FName  ");
			strSql.Append(" FROM T_RELATION ");
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
			strSql.Append(" FCode, FName  ");
			strSql.Append(" FROM T_RELATION ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

