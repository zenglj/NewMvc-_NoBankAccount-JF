using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Inicard
		public partial class T_InicardDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Inicard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Inicard(");			
            strSql.Append("cardid,pwd,crtdt");
			strSql.Append(") values (");
            strSql.Append("@cardid,@pwd,@crtdt");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@cardid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pwd", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime)             
              
            };
			            
            parameters[0].Value = model.cardid;                        
            parameters[1].Value = model.pwd;                        
            parameters[2].Value = model.crtdt;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Inicard model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Inicard set ");
			                        
            strSql.Append(" cardid = @cardid , ");                                    
            strSql.Append(" pwd = @pwd , ");                                    
            strSql.Append(" crtdt = @crtdt  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@cardid", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@pwd", SqlDbType.VarChar,50) ,            
                        new SqlParameter("@crtdt", SqlDbType.DateTime)             
              
            };
						            
            parameters[0].Value = model.cardid;                        
            parameters[1].Value = model.pwd;                        
            parameters[2].Value = model.crtdt;                        
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
			strSql.Append("delete from T_Inicard ");
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
		public SelfhelpOrderMgr.Model.T_Inicard GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select cardid, pwd, crtdt  ");			
			strSql.Append("  from T_Inicard ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Inicard model=new SelfhelpOrderMgr.Model.T_Inicard();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.cardid= ds.Tables[0].Rows[0]["cardid"].ToString();
																																model.pwd= ds.Tables[0].Rows[0]["pwd"].ToString();
																												if(ds.Tables[0].Rows[0]["crtdt"].ToString()!="")
				{
					model.crtdt=DateTime.Parse(ds.Tables[0].Rows[0]["crtdt"].ToString());
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
			strSql.Append("select cardid, pwd, crtdt  ");
			strSql.Append(" FROM T_Inicard ");
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
			strSql.Append(" cardid, pwd, crtdt  ");
			strSql.Append(" FROM T_Inicard ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

