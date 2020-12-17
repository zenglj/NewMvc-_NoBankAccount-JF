using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using SelfhelpOrderMgr.Model; 
using Dapper;
namespace SelfhelpOrderMgr.DAL  
{
	 	//T_MEET_AREA
		public partial class T_MEET_AREADAL
	{			
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(SelfhelpOrderMgr.Model.T_MEET_AREA model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_MEET_AREA(");			
            strSql.Append("FCode,FName,FFlag,FMain,FSub");
			strSql.Append(") values (");
            strSql.Append("@FCode,@FName,@FFlag,@FMain,@FSub");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FFlag", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FMain", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSub", SqlDbType.Int,4)             
              
            };
			            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FFlag;                        
            parameters[3].Value = model.FMain;                        
            parameters[4].Value = model.FSub;                        
			            SqlHelper.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SelfhelpOrderMgr.Model.T_MEET_AREA model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_MEET_AREA set ");
			                        
            strSql.Append(" FCode = @FCode , ");                                    
            strSql.Append(" FName = @FName , ");                                    
            strSql.Append(" FFlag = @FFlag , ");                                    
            strSql.Append(" FMain = @FMain , ");                                    
            strSql.Append(" FSub = @FSub  ");            			
			strSql.Append(" where  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FCode", SqlDbType.VarChar,3) ,            
                        new SqlParameter("@FName", SqlDbType.VarChar,20) ,            
                        new SqlParameter("@FFlag", SqlDbType.VarChar,10) ,            
                        new SqlParameter("@FMain", SqlDbType.Int,4) ,            
                        new SqlParameter("@FSub", SqlDbType.Int,4)             
              
            };
						            
            parameters[0].Value = model.FCode;                        
            parameters[1].Value = model.FName;                        
            parameters[2].Value = model.FFlag;                        
            parameters[3].Value = model.FMain;                        
            parameters[4].Value = model.FSub;                        
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
			strSql.Append("delete from T_MEET_AREA ");
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
		public SelfhelpOrderMgr.Model.T_MEET_AREA GetModel()
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FCode, FName, FFlag, FMain, FSub  ");			
			strSql.Append("  from T_MEET_AREA ");
			strSql.Append(" where ");
						SqlParameter[] parameters = {
			};

			
			SelfhelpOrderMgr.Model.T_MEET_AREA model=new SelfhelpOrderMgr.Model.T_MEET_AREA();
			DataSet ds=SqlHelper.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FCode= ds.Tables[0].Rows[0]["FCode"].ToString();
																																model.FName= ds.Tables[0].Rows[0]["FName"].ToString();
																																model.FFlag= ds.Tables[0].Rows[0]["FFlag"].ToString();
																												if(ds.Tables[0].Rows[0]["FMain"].ToString()!="")
				{
					model.FMain=int.Parse(ds.Tables[0].Rows[0]["FMain"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["FSub"].ToString()!="")
				{
					model.FSub=int.Parse(ds.Tables[0].Rows[0]["FSub"].ToString());
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
			strSql.Append("select FCode, FName, FFlag, FMain, FSub  ");
			strSql.Append(" FROM T_MEET_AREA ");
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
			strSql.Append(" FCode, FName, FFlag, FMain, FSub  ");
			strSql.Append(" FROM T_MEET_AREA ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return SqlHelper.Query(strSql.ToString());
		}

   
	}
}

