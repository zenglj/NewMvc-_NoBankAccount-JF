using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_DocType
		public partial class T_DocTypeDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_DocType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_DocType(");			
            strSql.Append("code,fname");
			strSql.Append(") values (");
            strSql.Append("@code,@fname");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@code", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,128)             
              
            };
			            
            parameters[0].Value = model.code;                        
            parameters[1].Value = model.fname;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_DocType model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_DocType set ");
			                        
            strSql.Append(" code = @code , ");                                    
            strSql.Append(" fname = @fname  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@code", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,128)             
              
            };
						            
            parameters[0].Value = model.code;                        
            parameters[1].Value = model.fname;                        
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
			strSql.Append("delete from T_DocType ");
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
		public SelfhelpOrderMgr.Model.T_DocType GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select code, fname  ");			
			strSql.Append("  from T_DocType ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_DocType model=new SelfhelpOrderMgr.Model.T_DocType();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.code= ds.Tables[0].Rows[0]["code"].ToString();
																																model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																										
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
			strSql.Append("select code, fname  ");
			strSql.Append(" FROM T_DocType ");
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
			strSql.Append(" code, fname  ");
			strSql.Append(" FROM T_DocType ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

