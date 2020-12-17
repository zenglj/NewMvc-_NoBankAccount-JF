using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_MEET_TYPE
		public partial class T_MEET_TYPEDAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_MEET_TYPE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MEET_TYPE(");			
            strSql.Append("FCode,FName,FPeriord,FSpecial,FAddrCode,FDesc");
			strSql.Append(") values (");
            strSql.Append("@FCode,@FName,@FPeriord,@FSpecial,@FAddrCode,@FDesc");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPeriord", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSpecial", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAddrCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,64)             
              
            };
			            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FPeriord;                        
            parameters[3].Value = model.FSpecial;                        
            parameters[4].Value = model.FAddrCode;                        
            parameters[5].Value = model.FDesc;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MEET_TYPE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MEET_TYPE set ");
			                        
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FPeriord = @FPeriord , ");                                    
            strSql.Append(" FSpecial = @FSpecial , ");                                    
            strSql.Append(" FAddrCode = @FAddrCode , ");                                    
            strSql.Append(" FDesc = @FDesc  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FPeriord", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSpecial", SqlDbType.Int,4) ,            
                        new SqlParameter("@FAddrCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FDesc", SqlDbType.VarChar,64)             
              
            };
						            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FPeriord;                        
            parameters[3].Value = model.FSpecial;                        
            parameters[4].Value = model.FAddrCode;                        
            parameters[5].Value = model.FDesc;                        
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
			strSql.Append("delete from T_MEET_TYPE ");
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
		public SelfhelpOrderMgr.Model.T_MEET_TYPE GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCode, FName, FPeriord, FSpecial, FAddrCode, FDesc  ");			
			strSql.Append("  from T_MEET_TYPE ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_MEET_TYPE model=new SelfhelpOrderMgr.Model.T_MEET_TYPE();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																												if(ds.Tables[0].Rows[0]["FPeriord"].ToString()!="")
				{
					model.FPeriord=int.Parse(ds.Tables[0].Rows[0]["FPeriord"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FSpecial"].ToString()!="")
				{
					model.FSpecial=int.Parse(ds.Tables[0].Rows[0]["FSpecial"].ToString());
				}
																																				model.FAddrCode= ds.Tables[0].Rows[0]["FAddrCode"].ToString();
																																model.FDesc= ds.Tables[0].Rows[0]["FDesc"].ToString();
																										
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
			strSql.Append("select FCode, FName, FPeriord, FSpecial, FAddrCode, FDesc  ");
			strSql.Append(" FROM T_MEET_TYPE ");
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
			strSql.Append(" FCode, FName, FPeriord, FSpecial, FAddrCode, FDesc  ");
			strSql.Append(" FROM T_MEET_TYPE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

