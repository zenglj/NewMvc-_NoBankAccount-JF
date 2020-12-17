using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_REP_TEMP
		public partial class T_REP_TEMPDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_REP_TEMP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_REP_TEMP(");			
            strSql.Append("FCCode,FCount");
			strSql.Append(") values (");
            strSql.Append("@FCCode,@FCount");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCount", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.FCCode;                        
            parameters[1].Value = model.FCount;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_REP_TEMP model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_REP_TEMP set ");
			                        
            strSql.Append(" FCCode = @FCCode , ");                                    
            strSql.Append(" FCount = @FCount  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FCount", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.FCCode;                        
            parameters[1].Value = model.FCount;                        
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
			strSql.Append("delete from T_REP_TEMP ");
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
		public SelfhelpOrderMgr.Model.T_REP_TEMP GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCCode, FCount  ");			
			strSql.Append("  from T_REP_TEMP ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_REP_TEMP model=new SelfhelpOrderMgr.Model.T_REP_TEMP();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FCCode= ds.Tables[0].Rows[0]["FCCode"].ToString();
																												if(ds.Tables[0].Rows[0]["FCount"].ToString()!="")
				{
					model.FCount=int.Parse(ds.Tables[0].Rows[0]["FCount"].ToString());
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
			strSql.Append("select FCCode, FCount  ");
			strSql.Append(" FROM T_REP_TEMP ");
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
			strSql.Append(" FCCode, FCount  ");
			strSql.Append(" FROM T_REP_TEMP ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

