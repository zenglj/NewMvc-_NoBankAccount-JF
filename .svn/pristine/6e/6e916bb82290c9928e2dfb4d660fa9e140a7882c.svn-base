using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_Menu
		public partial class T_MenuDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_Menu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_Menu(");			
            strSql.Append("code,fname,dj,ffreeflag");
			strSql.Append(") values (");
            strSql.Append("@code,@fname,@dj,@ffreeflag");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@code", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@dj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ffreeflag", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.code;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.dj;                        
            parameters[3].Value = model.ffreeflag;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_Menu model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_Menu set ");
			                        
            strSql.Append(" code = @code , ");                                    
            strSql.Append(" fname = @fname , ");                                    
            strSql.Append(" dj = @dj , ");                                    
            strSql.Append(" ffreeflag = @ffreeflag  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@code", SqlDbType.Int,4) ,            
                        new SqlParameter("@fname", SqlDbType.VarChar,30) ,            
                        new SqlParameter("@dj", SqlDbType.Decimal,9) ,            
                        new SqlParameter("@ffreeflag", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.code;                        
            parameters[1].Value = model.fname;                        
            parameters[2].Value = model.dj;                        
            parameters[3].Value = model.ffreeflag;                        
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
			strSql.Append("delete from T_Menu ");
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
		public SelfhelpOrderMgr.Model.T_Menu GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select code, fname, dj, ffreeflag  ");			
			strSql.Append("  from T_Menu ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_Menu model=new SelfhelpOrderMgr.Model.T_Menu();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["code"].ToString()!="")
				{
					model.code=int.Parse(ds.Tables[0].Rows[0]["code"].ToString());
				}
																																				model.fname= ds.Tables[0].Rows[0]["fname"].ToString();
																												if(ds.Tables[0].Rows[0]["dj"].ToString()!="")
				{
					model.dj=decimal.Parse(ds.Tables[0].Rows[0]["dj"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["ffreeflag"].ToString()!="")
				{
					model.ffreeflag=int.Parse(ds.Tables[0].Rows[0]["ffreeflag"].ToString());
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
			strSql.Append("select code, fname, dj, ffreeflag  ");
			strSql.Append(" FROM T_Menu ");
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
			strSql.Append(" code, fname, dj, ffreeflag  ");
			strSql.Append(" FROM T_Menu ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

