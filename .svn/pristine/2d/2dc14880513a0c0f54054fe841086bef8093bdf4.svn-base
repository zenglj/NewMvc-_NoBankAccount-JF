using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//t_bankFeeList
		public partial class t_bankFeeListDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.t_bankFeeList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into t_bankFeeList(");			
            strSql.Append("AccCode,typeid,typename,subtypeid");
			strSql.Append(") values (");
            strSql.Append("@AccCode,@typeid,@typename,@subtypeid");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@typeid", SqlDbType.Int,4) ,            
                        new SqlParameter("@typename", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@subtypeid", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.AccCode;                        
            parameters[1].Value = model.typeid;                        
            parameters[2].Value = model.typename;                        
            parameters[3].Value = model.subtypeid;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.t_bankFeeList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update t_bankFeeList set ");
			                        
            strSql.Append(" AccCode = @AccCode , ");                                    
            strSql.Append(" typeid = @typeid , ");                                    
            strSql.Append(" typename = @typename , ");                                    
            strSql.Append(" subtypeid = @subtypeid  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@AccCode", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@typeid", SqlDbType.Int,4) ,            
                        new SqlParameter("@typename", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@subtypeid", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.AccCode;                        
            parameters[1].Value = model.typeid;                        
            parameters[2].Value = model.typename;                        
            parameters[3].Value = model.subtypeid;                        
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
			strSql.Append("delete from t_bankFeeList ");
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
		public SelfhelpOrderMgr.Model.t_bankFeeList GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AccCode, typeid, typename, subtypeid  ");			
			strSql.Append("  from t_bankFeeList ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.t_bankFeeList model=new SelfhelpOrderMgr.Model.t_bankFeeList();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.AccCode= ds.Tables[0].Rows[0]["AccCode"].ToString();
																												if(ds.Tables[0].Rows[0]["typeid"].ToString()!="")
				{
					model.typeid=int.Parse(ds.Tables[0].Rows[0]["typeid"].ToString());
				}
																																				model.typename= ds.Tables[0].Rows[0]["typename"].ToString();
																												if(ds.Tables[0].Rows[0]["subtypeid"].ToString()!="")
				{
					model.subtypeid=int.Parse(ds.Tables[0].Rows[0]["subtypeid"].ToString());
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
			strSql.Append("select AccCode, typeid, typename, subtypeid  ");
			strSql.Append(" FROM t_bankFeeList ");
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
			strSql.Append(" AccCode, typeid, typename, subtypeid  ");
			strSql.Append(" FROM t_bankFeeList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

