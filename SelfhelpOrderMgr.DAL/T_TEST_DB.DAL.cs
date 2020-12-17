using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_TEST_DB
		public partial class T_TEST_DBDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_TEST_DB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_TEST_DB(");			
            strSql.Append("name,picdata1,picdata2,picdata3");
			strSql.Append(") values (");
            strSql.Append("@name,@picdata1,@picdata2,@picdata3");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@picdata1", SqlDbType.Image) ,            
                        new SqlParameter("@picdata2", SqlDbType.Binary,1000) ,            
                        new SqlParameter("@picdata3", SqlDbType.NText)             
              
            };
			            
            parameters[0].Value = model.name;                        
            parameters[1].Value = model.picdata1;                        
            parameters[2].Value = model.picdata2;                        
            parameters[3].Value = model.picdata3;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_TEST_DB model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_TEST_DB set ");
			                        
            strSql.Append(" name = @name , ");                                    
            strSql.Append(" picdata1 = @picdata1 , ");                                    
            strSql.Append(" picdata2 = @picdata2 , ");                                    
            strSql.Append(" picdata3 = @picdata3  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@name", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@picdata1", SqlDbType.Image) ,            
                        new SqlParameter("@picdata2", SqlDbType.Binary,1000) ,            
                        new SqlParameter("@picdata3", SqlDbType.NText)             
              
            };
						            
            parameters[0].Value = model.name;                        
            parameters[1].Value = model.picdata1;                        
            parameters[2].Value = model.picdata2;                        
            parameters[3].Value = model.picdata3;                        
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
			strSql.Append("delete from T_TEST_DB ");
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
		public SelfhelpOrderMgr.Model.T_TEST_DB GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select name, picdata1, picdata2, picdata3  ");			
			strSql.Append("  from T_TEST_DB ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_TEST_DB model=new SelfhelpOrderMgr.Model.T_TEST_DB();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.name= ds.Tables[0].Rows[0]["name"].ToString();
																																				if(ds.Tables[0].Rows[0]["picdata1"].ToString()!="")
				{
					model.picdata1= (byte[])ds.Tables[0].Rows[0]["picdata1"];
				}
																																if(ds.Tables[0].Rows[0]["picdata2"].ToString()!="")
				{
					model.picdata2= (byte[])ds.Tables[0].Rows[0]["picdata2"];
				}
																												model.picdata3= ds.Tables[0].Rows[0]["picdata3"].ToString();
																										
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
			strSql.Append("select name, picdata1, picdata2, picdata3  ");
			strSql.Append(" FROM T_TEST_DB ");
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
			strSql.Append(" name, picdata1, picdata2, picdata3  ");
			strSql.Append(" FROM T_TEST_DB ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

